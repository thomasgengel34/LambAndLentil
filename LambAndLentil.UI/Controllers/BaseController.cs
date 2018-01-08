using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;

namespace LambAndLentil.UI.Controllers
{
    public abstract class BaseController<T> : Controller
            where T : BaseEntity, IEntity, new()
    {
        public string ClassName { get; private set; }
        public int PageSize { get; set; }
        public static IRepository<T> Repo { get; set; }
        public static IRepository<Ingredient> IngredientRepo { get; set; }
        public static IRepository<Menu> MenuRepo { get; set; }

        public BaseController(IRepository<T> repository)
        {
            Repo = repository;
            ClassName = new RepositoryHelperMethods().GetClassName<T>();
        }



        //  TODO: filter

        public ActionResult BaseIndex(IRepository<T> Repo, int? page = 1)
        {
            PageSize = 8;
            int pageInt = page ?? 1;
            ListEntity<T> model = new ListEntity<T>
            {

                ListT = new BaseEntity().GetIndexedModel<T>(Repo, PageSize, pageInt),
                PagingInfo = new BaseEntity().PagingFunction<T>(Repo, page, PageSize)
            };
            return View(UIViewType.Index.ToString(), model);
        }

        public ActionResult BaseDetails(IRepository<T> Repo, UIControllerType uIController, int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            ViewBag.Title = actionMethod.ToString();
            switch (actionMethod)
            {
                case UIViewType.Delete: return BaseDelete(Repo, uIController, id);
                case UIViewType.DeleteConfirmed: return BaseDeleteConfirmed(Repo, uIController, id);
                case UIViewType.Details:
                    {
                        return BaseDetailsDealWithDetails(Repo, id);
                    }
                case UIViewType.Edit: return BaseEdit(Repo, uIController, id);
                case UIViewType.PostEdit:
                    {
                        T item = Repo.GetById(id);
                        return BasePostEdit(Repo, item);
                    }
                default: return View(UIViewType.Index);
            }
        }

        private ActionResult BaseDetailsDealWithDetails(IRepository<T> Repo, int id)
        {
            T item = Repo.GetById(id);
            if (item == null)
            {
                return RedirectToAction(UIViewType.BaseIndex.ToString()).WithError("No " + ClassName + " was found with that id.");
            }
            else
            {
                bool IsValid = IsModelValid(item);

                if (IsValid)
                {
                    return View(UIViewType.Details.ToString(), item).WithSuccess("Here it is!");
                }
                else
                {
                    return View(UIViewType.Details.ToString(), item).WithError("Something is wrong with the data!");
                }
            }
        }

        private ActionResult BaseEdit(IRepository<T> Repo, UIControllerType uIController, int id)
        {
            T item = Repo.GetById(id);
            if (item == null)
            {
                return (ViewResult)RedirectToAction(UIViewType.Index.ToString()).WithWarning(ClassName + " was not found");
            }
            else
            {
                return View(UIViewType.Details.ToString(), item);
            }
        }

        private bool IsModelValid(T item) => new ModelValidation().IsModelValid<T>(item);

        public ViewResult BaseCreate(UIViewType actionMethod)
        {
            ViewBag.ActionMethod = UIViewType.Create;
            T item = new T
            {
                CreationDate = DateTime.Now
            };
            return View(UIViewType.Details.ToString(), item);
        }



        public ActionResult BasePostEdit(IRepository<T> Repo, T entity)
        {
            T item = entity;
            bool isValid = IsModelValid(entity);
            if (isValid)
            {
                Repo.Update(entity, entity.ID);


                return RedirectToAction(UIViewType.BaseIndex.ToString()).WithSuccess(string.Format($"{entity.Name} has been saved or modified"));
            }
            else
            {
                return View(UIViewType.Details.ToString(), entity).WithWarning("Something is wrong with the data!");
            }
        }


        public ActionResult BaseDelete(IRepository<T> Repo, UIControllerType uiControllerType, int id = 1, UIViewType actionMethod = UIViewType.Delete)
        {
            T item = Repo.GetById(id);
            if (item == null)
            {
                return (ViewResult)RedirectToAction(UIViewType.Index.ToString()).WithWarning(ClassName + " was not found");
            }
            else
            {
                return View(UIViewType.Details.ToString(), item).WithSuccess("Here it is!");   // not exactly what we want! TODO: in view,   Need a button with an Are You SURE you want to delete this?
            }
        }

        public ActionResult BaseDeleteConfirmed(IRepository<T> Repo, UIControllerType controllerType, int id)
        {
            T item = Repo.GetById(id);
            if (item == null)
            {
                return RedirectToAction(UIViewType.BaseIndex.ToString()).WithWarning(ClassName + " was not found");
            }
            else
            {
                Repo.Remove(item);
                return RedirectToAction(UIViewType.BaseIndex.ToString()).WithSuccess(string.Format($"{item.Name} has been deleted"));
            }
        }





        public void BaseAddIngredientToIngredientsList(IRepository<T> repo, UIControllerType tController, int id, string addedIngredient)
        {
            T item = Repo.GetById(id);

            if (item != null)
            {
                item.IngredientsList += String.Concat(", ", addedIngredient);
                Repo.Update(item, item.ID);
            }
        } 

        protected ActionResult BaseDetachLastIngredientChild(IRepository<T> repo, int iD)
        {
            IEntityChildClassIngredients parent = (IEntityChildClassIngredients)Repo.GetById(iD);
            int count = parent.Ingredients.Count();
            parent.Ingredients.RemoveAt(count - 1);
            return View(UIViewType.Index);    // TODO: fix
        }
    }
}
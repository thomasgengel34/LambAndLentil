using System;
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
        string ClassName; 
       public int PageSize { get; set; } 
        public static IRepository<T> Repo { get; set; }
        public static IRepository<Ingredient> IngredientRepo { get; set; }   // TODO: convert to Parent/Child Repos
        public static IRepository<Menu> MenuRepo { get; set; }  // TODO: convert to Parent/Child Repos

        public BaseController(IRepository<T> repository)
        {
            Repo = repository;
            ClassName = new RepositoryHelperMethods().GetClassName<T>();
        }



        //  TODO: filter   
        public ActionResult BaseIndex(int? page = 1)
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

  

       public ActionResult BaseDetails(int id)
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

       

        public ActionResult BaseEdit( int id)
        {
            T item = Repo.GetById(id);
            if (item == null)
            {
                return  RedirectToAction(UIViewType.Index.ToString()).WithWarning(ClassName + " was not found");
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



        public ActionResult BasePostEdit(T entity)
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


        public ActionResult BaseDelete(int id = 1)
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

        public ActionResult BaseDeleteConfirmed( int id=1)
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

        public void BaseAddIngredientToIngredientsList( int id, string addedIngredient)
        {
            T item = Repo.GetById(id);

            if (item != null)
            {
                item.IngredientsList += String.Concat(", ", addedIngredient);
                Repo.Update(item, item.ID);
            }
        }  
    }
}
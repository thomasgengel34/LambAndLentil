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

        public ViewResult BaseIndex(IRepository<T> Repo, int page = 1)
        {
            PageSize = 8;
            ListEntity<T> model = new ListEntity<T>
            {
                ListT = new BaseEntity().GetIndexedModel<T>(Repo, PageSize, page),
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



        public ActionResult BasePostEdit(IRepository<T> Repo, T vm)
        {
            T item = vm;
            bool isValid = IsModelValid(vm);

            if (isValid)
            {
                if (vm.ID == 0)
                {
                    item = new T();
                }
                Repo.Update(item, item.ID);
                return RedirectToAction(UIViewType.BaseIndex.ToString()).WithSuccess(string.Format($"{vm.Name} has been saved or modified"));
            }
            else
            {
                return View(UIViewType.Details.ToString(), vm).WithWarning("Something is wrong with the data!");
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

        public ActionResult BaseAttach<TChild>(IRepository<T> Repo, int? parentID, TChild child, AttachOrDetach attachOrDetach = AttachOrDetach.Attach, int orderNumber = 0)
        where TChild : BaseEntity, IEntity // todo: expand
        {
           

            char[] charsToTrim = { 'V', 'M' };
            string entity = typeof(T).ToString().Split('.').Last();
            string childEntity = typeof(TChild).ToString().Split('.').Last().TrimEnd(charsToTrim);
         


            if (parentID == null)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning(entity + " was not found");
            }
            else
            {
                int parentNonNullID = (int)parentID;
                T parent = Repo.GetById(parentNonNullID);
                if (parent == null)
                {
                    // TODO: log error - this could be a developer problem
                    return RedirectToAction(UIViewType.Index.ToString()).WithWarning(entity + " was not found");
                }

                if (child == null)
                {
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithWarning(entity + " was not found"); ;
                }
                else
                {
                    if (child == null)
                    {
                        return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithWarning("Please choose a(n) " + childEntity);
                    }
                    else
                    {

                        if (attachOrDetach == AttachOrDetach.Detach)
                        {
                            if (typeof(TChild) == typeof(Ingredient))
                            {
                                Repo.DetachAnIndependentChild<Ingredient>(parent.ID, child as Ingredient, 0);
                            }
                            else if (typeof(TChild) == typeof(Recipe))
                            {
                                Repo.DetachAnIndependentChild<Recipe>(parent.ID, child as Recipe, 0);
                            }

                            return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithSuccess(childEntity + " was Successfully Detached!");
                        }
                        else
                        {
                            if (typeof(TChild) == typeof(Ingredient))
                            {
                                Repo.AttachAnIndependentChild<Ingredient>(parent.ID, child as Ingredient, 0);
                            }
                            else if (typeof(TChild) == typeof(Recipe))
                            {
                                Repo.AttachAnIndependentChild<Recipe>(parent.ID, child as Recipe, 0);
                            }
                            return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithSuccess(childEntity + " was Successfully Attached!");
                        }
                    }
                }
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

        protected void BaseDetachAllIngredientChildren(IRepository<T> Repo, int ID)
        {
            T parent = Repo.GetById(ID);
            parent.Ingredients.RemoveAll(match: x => x.ID >= 0);
            Repo.Update(parent, parent.ID);
        }

        protected void BaseDetachAllIngredientChildren(IRepository<T> repo, int iD, List<Ingredient> selected)
        {
            T parent = Repo.GetById(iD);
            var setToRemove = new HashSet<Ingredient>(selected);
            parent.Ingredients.RemoveAll(ContainsSelected  );
            Repo.Update(parent, parent.ID);


            bool ContainsSelected(Ingredient ingredient)
            {
                int ingredientID = ingredient.ID;
                var numbers =    from f in selected select f.ID;
                bool  foo=numbers.Contains(ingredientID);
                return foo;
            }
        }

        protected void BaseDetachLastIngredientChild(IRepository<T> repo, int iD)
        {
            T parent = Repo.GetById(iD);
            int count =parent.Ingredients.Count();
            parent.Ingredients.RemoveAt(count - 1);
        }  
    }
}
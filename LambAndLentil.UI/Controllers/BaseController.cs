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

        public ViewResult BaseIndex(IRepository<T> Repo, int? page = 1)
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
            int parentNonNullID = (int)parentID;
            T parent = Repo.GetById(parentNonNullID);
            if (parent == null)
            {
                // TODO: log error - this could be a developer problem
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning(entity + " was not found");
            }
            if (AttachOrDetach.Detach == attachOrDetach && orderNumber < 0)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning("Order Number Was Negative! Nothing was detached");
            }
            else if (child == null)
            {
                return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithWarning(entity + " was not found"); ;
            }
            else
            {
                if (attachOrDetach == AttachOrDetach.Detach)
                {
                    Repo.DetachAnIndependentChild(parent.ID, child, 0);
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithSuccess(childEntity + " was Successfully Detached!");
                }
                else
                {
                    Repo.AttachAnIndependentChild(parent.ID, child, 0);
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithSuccess(childEntity + " was Successfully Attached!");
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

        protected ActionResult BaseDetachAllIngredientChildren(IRepository<T> Repo, int? ID, List<Ingredient> selected)
        {
            string entity = typeof(T).ToString().Split('.').Last();
            if (ID == null)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning(entity + " was not found");
            }
            else
            {
                int parentNonNullID = (int)ID;
                IEntityChildClassIngredients parent = (IEntityChildClassIngredients)Repo.GetById(parentNonNullID);
                if (parent == null)
                {
                    // TODO: log error - this could be a developer problem
                    return RedirectToAction(UIViewType.Index.ToString()).WithWarning(entity + " was not found");
                }
                else
                if (parent.Ingredients.Count() > 0)
                {
                    if (selected == null)
                    {
                        parent.Ingredients.RemoveAll(match: x => x.ID >= 0);
                    }
                    else
                    {
                        var setToRemove = new HashSet<Ingredient>(selected);
                        parent.Ingredients.RemoveAll(ContainsSelected);
                    }

                    Repo.Update((T)parent, parent.ID);
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = ID, actionMethod = UIViewType.Edit }).WithSuccess("All Ingredients Were Successfully Detached!");
                }
                else
                {
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = ID, actionMethod = UIViewType.Edit }).WithWarning("No Ingredients Were Attached!");
                }
            }
            bool ContainsSelected(Ingredient ingredient)
            {
                int ingredientID = ingredient.ID;
                var numbers = from f in selected select f.ID;
                bool trueOrFalse = numbers.Contains(ingredientID);
                return trueOrFalse;
            }
        }

        protected ActionResult BaseDetachAllRecipeChildren(IRepository<T> Repo, int? ID, List<Recipe> selected)
        {
            string entity = typeof(T).ToString().Split('.').Last();
            if (ID == null)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning(entity + " was not found");
            }
            else
            {
                int parentNonNullID = (int)ID;
                IEntityChildClassRecipes parent = (IEntityChildClassRecipes)Repo.GetById(parentNonNullID);
                if (parent == null)
                {
                    // TODO: log error - this could be a developer problem
                    return RedirectToAction(UIViewType.Index.ToString()).WithWarning(entity + " was not found");
                }
                else
                if (parent.Recipes.Count() > 0)
                {
                    if (selected == null)
                    {
                        parent.Recipes.RemoveAll(match: x => x.ID >= 0);
                    }
                    else
                    {
                        var setToRemove = new HashSet<Recipe>(selected);
                        parent.Recipes.RemoveAll(ContainsSelected);
                    }

                    Repo.Update((T)parent, parent.ID);
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = ID, actionMethod = UIViewType.Edit }).WithSuccess("All Recipes Were Successfully Detached!");
                }
                else
                {
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = ID, actionMethod = UIViewType.Edit }).WithWarning("No Recipes Were Attached!");
                }
            }
            bool ContainsSelected(Recipe ingredient)
            {
                int ingredientID = ingredient.ID;
                var numbers = from f in selected select f.ID;
                bool trueOrFalse = numbers.Contains(ingredientID);
                return trueOrFalse;
            }
        }

        protected ActionResult BaseDetachAllPlanChildren(IRepository<T> Repo, int? ID, List<Plan> selected)
        {

            string entity = typeof(T).ToString().Split('.').Last();
            if (ID == null)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning(entity + " was not found");
            }
            else
            {
                int parentNonNullID = (int)ID;
                IEntityChildClassPlans parent = (IEntityChildClassPlans)Repo.GetById(parentNonNullID);
                if (parent == null)
                {
                    // TODO: log error - this could be a developer problem
                    return RedirectToAction(UIViewType.Index.ToString()).WithWarning(entity + " was not found");
                }
                else
                if (parent.Plans.Count() > 0)
                {
                    if (selected == null)
                    {
                        parent.Plans.RemoveAll(match: x => x.ID >= 0);
                    }
                    else
                    {
                        var setToRemove = new HashSet<Plan>(selected);
                        parent.Plans.RemoveAll(ContainsSelected);
                    }

                    Repo.Update((T)parent, parent.ID);
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = ID, actionMethod = UIViewType.Edit }).WithSuccess("All Plans Were Successfully Detached!");
                }
                else
                {
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = ID, actionMethod = UIViewType.Edit }).WithWarning("No Plans Were Attached!");
                }
            }
            bool ContainsSelected(Plan ingredient)
            {
                int ingredientID = ingredient.ID;
                var numbers = from f in selected select f.ID;
                bool trueOrFalse = numbers.Contains(ingredientID);
                return trueOrFalse;
            }
        }

        protected ActionResult BaseDetachAllMenuChildren(IRepository<T> Repo, int? ID, List<Menu> selected)
        {

            string entity = typeof(T).ToString().Split('.').Last();
            if (ID == null)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning(entity + " was not found");
            }
            else
            {
                int parentNonNullID = (int)ID;
                IEntityChildClassMenus parent = (IEntityChildClassMenus)Repo.GetById(parentNonNullID);
                if (parent == null)
                {
                    // TODO: log error - this could be a developer problem
                    return RedirectToAction(UIViewType.Index.ToString()).WithWarning(entity + " was not found");
                }
                else
                if (parent.Menus.Count() > 0)
                {
                    if (selected == null)
                    {
                        parent.Menus.RemoveAll(match: x => x.ID >= 0);
                    }
                    else
                    {
                        var setToRemove = new HashSet<Menu>(selected);
                        parent.Menus.RemoveAll(ContainsSelected);
                    }

                    Repo.Update((T)parent, parent.ID);
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = ID, actionMethod = UIViewType.Edit }).WithSuccess("All Menus Were Successfully Detached!");
                }
                else
                {
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = ID, actionMethod = UIViewType.Edit }).WithWarning("No Menus Were Attached!");
                }
            }
            bool ContainsSelected(Menu ingredient)
            {
                int ingredientID = ingredient.ID;
                var numbers = from f in selected select f.ID;
                bool trueOrFalse = numbers.Contains(ingredientID);
                return trueOrFalse;
            }
        }



        protected ActionResult BaseDetachAllShoppingListChildren(IRepository<T> Repo, int? ID, List<ShoppingList> selected)
        {

            string entity = typeof(T).ToString().Split('.').Last();
            if (ID == null)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning(entity + " was not found");
            }
            else
            {
                int parentNonNullID = (int)ID;
                IEntityChildClassShoppingLists parent = (IEntityChildClassShoppingLists)Repo.GetById(parentNonNullID);
                if (parent == null)
                {
                    // TODO: log error - this could be a developer problem
                    return RedirectToAction(UIViewType.Index.ToString()).WithWarning(entity + " was not found");
                }
                else
                if (parent.ShoppingLists.Count() > 0)
                {
                    if (selected == null)
                    {
                        parent.ShoppingLists.RemoveAll(match: x => x.ID >= 0);
                    }
                    else
                    {
                        var setToRemove = new HashSet<ShoppingList>(selected);
                        parent.ShoppingLists.RemoveAll(ContainsSelected);
                    }

                    Repo.Update((T)parent, parent.ID);
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = ID, actionMethod = UIViewType.Edit }).WithSuccess("All ShoppingLists Were Successfully Detached!");
                }
                else
                {
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = ID, actionMethod = UIViewType.Edit }).WithWarning("No ShoppingLists Were Attached!");
                }
            }
            bool ContainsSelected(ShoppingList ingredient)
            {
                int ingredientID = ingredient.ID;
                var numbers = from f in selected select f.ID;
                bool trueOrFalse = numbers.Contains(ingredientID);
                return trueOrFalse;
            }
        }


        protected void BaseDetachLastIngredientChild(IRepository<T> repo, int iD)
        {
            IEntityChildClassIngredients parent = (IEntityChildClassIngredients)Repo.GetById(iD);
            int count = parent.Ingredients.Count();
            parent.Ingredients.RemoveAt(count - 1);
        }


    }
}
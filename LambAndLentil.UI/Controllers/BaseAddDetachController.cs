using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.BusinessObjects;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.Alerts;

namespace LambAndLentil.UI.Controllers
{
    public class BaseAttachDetachController<T> : BaseController<T>, IGenericController<T>, IAttachDetachController<T>
     where T : BaseEntity, IEntity, new()

    {
        protected string EntityName { get; set; }

        public BaseAttachDetachController(IRepository<T> repository) : base(repository)
        {
            Repo = repository;
            EntityName = typeof(T).ToString().Split('.').Last();
        }



        int IGenericController<T>.PageSize { get; set; }


        ActionResult IGenericController<T>.Index(int? page) => BaseIndex(page);


        // GET: Recipes/Details/5
        ActionResult IGenericController<T>.Details(int id) => BaseDetails(id);


        // GET: Ingredients/Create 
        [ActionName("Create")]
        ActionResult IGenericController<T>.Create() => BaseCreate();

        [HttpGet]
        ActionResult IGenericController<T>.Edit(int id) => BaseEdit(id);

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("PostEdit")]
        ActionResult IGenericController<T>.PostEdit([Bind(Include = "ID, Name, Description, CreationDate,  ModifiedDate,  AddedByUser, ModifiedByUser, IngredientsList")]  T t) => BasePostEdit(t);


        // GET: Recipes/Delete/5
        [ActionName("Delete")]
        ActionResult IGenericController<T>.Delete(int id) => BaseDelete(id);


        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        ActionResult IGenericController<T>.DeleteConfirmed(int id) => BaseDeleteConfirmed(id);


        void IGenericController<T>.AddIngredientToIngredientsList(int id, string addedIngredient) => BaseAddIngredientToIngredientsList(id, addedIngredient);


        public ActionResult Attach(IEntity parent, IEntity child)
        {
            if (parent == null)
            {
                return HandleNullParent();
            }
            if (child == null)
            {
                return HandleNullChild(parent.ID);
            }
            if (!parent.CanHaveChild(child))
            {
                return HandleParentCannotAttachChild(parent);
            }
            parent = new ChildAttachment().AddChildToParent(parent, child);
            Repo.Save((T)parent);
            string childEntity = child.GetType().ToString().Split('.').Last();
            return HandleSuccessfulAttachment(parent.ID, child);
        }

        private ActionResult HandleNullParent() =>
              // TODO: log error - this could be a developer problem
            RedirectToAction(UIViewType.Index.ToString()).WithWarning((new T()).DisplayName + " was not found");
      
       


        private ActionResult HandleSuccessfulAttachment(int? parentID, IEntity child) =>
                        RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithSuccess(child.DisplayName + " was Successfully Attached!");

        public ActionResult Detach(IEntity parent, IEntity child)
        {

            ActionResult actionResult = GuardAttachAndDetachMethod(parent, child);
            if (actionResult is EmptyResult)
            {
                parent = new ChildDetachment().DetachAnIndependentChild(parent, child);
                Repo.Save((T)parent);
                string childEntity = child.GetType().ToString().Split('.').Last();
                return HandleSuccessfulDetachment(parent, child);
            }
            else return actionResult;
        }

        private ActionResult HandleSuccessfulDetachment(IEntity parent, IEntity child)
        {
            if (parent == null)
            {
                return HandleNullParent();
            }
            if (child == null)
            {
                return HandleNullChild(parent.ID);
            }
            string childName = child.GetType().ToString().Split('.').Last();

            bool IsChildAttached = CheckParentForAttachedChild(parent, child);
            if (IsChildAttached)
            {
                parent = new ChildDetachment().DetachAnIndependentChild(parent, child);
                return RedirectToAction(UIViewType.Details.ToString(), new { id = parent.ID, actionMethod = UIViewType.Edit }).WithSuccess(child.DisplayName + " was Successfully Detached!");
            }
            else
            {
                return RedirectToAction(UIViewType.Details.ToString(), new { id = parent.ID, actionMethod = UIViewType.Edit }).WithError("No child was attached!");
            }
        }

        private bool CheckParentForAttachedChild(IEntity parent, IEntity child)
        {
            bool isChildAttached = false;
            Type childType = child.GetType();
            if (childType == typeof(Ingredient))
            {
                Ingredient ingredient = parent.Ingredients.Where(m => m.ID == child.ID).FirstOrDefault();
                if (ingredient != null)
                {
                    isChildAttached = true;
                }
            }
            if (childType == typeof(Recipe))
            {
                Recipe recipe = parent.Recipes.Where(m => m.ID == child.ID).FirstOrDefault();
                if (recipe != null)
                {
                    isChildAttached = true;
                }
            }
            if (childType == typeof(Menu))
            {
                Menu menu = parent.Menus.Where(m => m.ID == child.ID).FirstOrDefault();
                if (menu != null)
                {
                    isChildAttached = true;
                }
            }
            if (childType == typeof(Plan))
            {
                Plan plan = parent.Plans.Where(m => m.ID == child.ID).FirstOrDefault();
                if (plan != null)
                {
                    isChildAttached = true;
                }
            }
            if (childType == typeof(ShoppingList))
            {
                ShoppingList shoppingList = parent.ShoppingLists.Where(m => m.ID == child.ID).FirstOrDefault();
                if (shoppingList != null)
                {
                    isChildAttached = true;
                }
            }
            return isChildAttached;
        }

 
        ActionResult GuardAttachAndDetachMethod(IEntity parent, IEntity child)
        {
            if (parent == null)
            {
                return HandleNullParent();
            }
            if (child == null)
            {
                return HandleNullChild(parent.ID);
            }
            if (!parent.CanHaveChild(child))
            {
                return HandleParentCannotAttachChild(parent);
            }
            else return new EmptyResult();
        }
 
        private ActionResult HandleParentCannotAttachChild(IEntity parent) => RedirectToAction(UIViewType.Details.ToString(), new { id = parent.ID, actionMethod = UIViewType.Edit }).WithWarning("Element Could not Be Attached - So It Could Not Be Detached!");

        private ActionResult HandleNullChild(int? parentID) =>
            RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithWarning("Child was not found");

         

        private ActionResult HandleNullParentID() => RedirectToAction(UIViewType.Index.ToString()).WithWarning(EntityName + " was not found");


        public ActionResult DetachASetOf(IEntity parent, List<IEntity> selected)
        {
            IEntity _parent = parent;
            string parentName = _parent.GetType().ToString().Split('.').Last();

            if (selected == null || selected.Count == 0)
            {
                return HandleNothingWasSelected(parent.ID);
            }


            IEntity child = selected.First();
            string childName = selected.First().GetType().ToString().Split('.').Last();

            if (child == null)
            {
                return HandleNullChild(0);
            }
            else if (!parent.CanHaveChild(child))
            {
                return HandleParentCannotAttachChild((T)parent);
            }


            if (selected == null || selected.Count == 0)
            {
                return DetachAll(parent, child);
            }
            else
            {
                parent = new ChildDetachment().DetachSelectionFromChildren(parent, selected);
                Repo.Save((T)parent);

                return HandleSuccessfulDetachment(parent, child);
            }
        }

        private ActionResult HandleNothingWasSelected(int id) =>
                    RedirectToAction(UIViewType.Details.ToString(), new { id, actionMethod = UIViewType.Edit }).WithWarning("Nothing was Selected!");

        public ActionResult DetachAll(IEntity parent, IEntity child)
        {
            if (parent == null)
            {
                return HandleNullParent();
            }
            bool canHaveChild = parent.CanHaveChild(child);
            if (canHaveChild)
            {
                parent = new ChildDetachment().DetachAllChildrenOfAType(parent, child);
                Repo.Save((T)parent);
                string childName = child.GetType().ToString().Split('.').Last();
                return HandleAllChildrenSuccessfullyDetached(parent.ID, childName);
            }
            else
            {
                return HandleParentCannotAttachChild(parent);
            }
        }

        private ActionResult HandleAllChildrenSuccessfullyDetached(int? ID, string childName) =>
                    RedirectToAction(UIViewType.Details.ToString(), new { ID, actionMethod = UIViewType.Edit }).WithSuccess("All " + childName + "s Were Successfully Detached!");


    }
}
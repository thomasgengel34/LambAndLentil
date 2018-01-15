using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LambAndLentil.BusinessObjects;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;

namespace LambAndLentil.UI.Controllers
{
    public class BaseAttachDetachController<T> : BaseController<T>, IGenericController<T>, IAttachDetachController<T>
     where T : BaseEntity, IEntity, new()

    {
        // TODO: get UIControllerType for the appropriate T
        protected string EntityName { get; set; }

        public BaseAttachDetachController(IRepository<T> repository) : base(repository)
        {
            Repo = repository;
            UIControllerType = GetUIControllerType();
            EntityName = typeof(T).ToString().Split('.').Last();
        }

        private UIControllerType GetUIControllerType()
        {
            if (typeof(T) == typeof(Ingredient)) return UIControllerType.Ingredients;
            if (typeof(T) == typeof(Menu)) return UIControllerType.Menus;
            if (typeof(T) == typeof(Person)) return UIControllerType.Persons;
            if (typeof(T) == typeof(Plan)) return UIControllerType.Plans;
            if (typeof(T) == typeof(ShoppingList)) return UIControllerType.ShoppingLists;
            if (typeof(T) == typeof(Recipe)) return UIControllerType.Recipes;
            return UIControllerType.Ingredients;
        }


        int IGenericController<T>.PageSize { get; set; }
        private static UIControllerType UIControllerType { get; set; }

        ActionResult IGenericController<T>.Index(int? page) => BaseIndex(Repo, page);


        // GET: Recipes/Details/5
        ActionResult IGenericController<T>.Details(int id, UIViewType actionMethod) => BaseDetails(Repo, UIControllerType, id, actionMethod);


        // GET: Ingredients/Create 
        [ActionName("Create")]
        ActionResult IGenericController<T>.Create(UIViewType actionMethod) => BaseCreate(actionMethod);

        [HttpGet]
        ActionResult IGenericController<T>.Edit(int id) => BaseDetails(Repo, UIControllerType, id, UIViewType.Edit);

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("PostEdit")]
        ActionResult IGenericController<T>.PostEdit([Bind(Include = "ID, Name, Description, CreationDate,  ModifiedDate,  AddedByUser, ModifiedByUser, IngredientsList")]  T t) => BasePostEdit(Repo, t);


        // GET: Recipes/Delete/5
        [ActionName("Delete")]
        ActionResult IGenericController<T>.Delete(int id, UIViewType actionMethod) => BaseDelete(Repo, UIControllerType, id);


        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        ActionResult IGenericController<T>.DeleteConfirmed(int id) => BaseDeleteConfirmed(Repo, UIControllerType, id);


        void IGenericController<T>.AddIngredientToIngredientsList(int id, string addedIngredient) => BaseAddIngredientToIngredientsList(Repo, UIControllerType, id, addedIngredient);


        public void DetachLastIngredientChild(int ID) => BaseDetachLastIngredientChild(Repo, ID);

        public ActionResult Attach<TChild>(IRepository<T> Repo, int? parentID, TChild child)
                 where TChild : BaseEntity, IEntity, IPossibleChildren, new()
        {
            ActionResult actionResult = GuardAttachAndDetachMethod(parentID, child);
            if (actionResult is EmptyResult)
            {
                T  parent = Repo.GetById((int)parentID);
                parent = new Connections().AddChildToParent(parent, child);
                Repo.Save(parent);
                string childEntity = typeof(TChild).ToString().Split('.').Last();

                return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithSuccess(childEntity + " was Successfully Attached!");
            }
            else return actionResult;
        }

        public ActionResult Detach<TChild>(IRepository<T> Repo, int? parentID, TChild child)
        where TChild : BaseEntity, IEntity, IPossibleChildren, new()
        {
            ActionResult actionResult = GuardAttachAndDetachMethod(parentID, child);
            if (actionResult is EmptyResult)
            {
                Repo.DetachAnIndependentChild((int)parentID, child);
                string childEntity = typeof(TChild).ToString().Split('.').Last();
                return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithSuccess(childEntity + " was Successfully Detached!");
            }
            else return actionResult;
        }


        ActionResult GuardAttachAndDetachMethod(int? parentID, IEntity child)
        {
            if (parentID == null) { return HandleNullParentID(); }

            int parentNonNullID = (int)parentID;
            T parent = Repo.GetById(parentNonNullID);

            if (parent == null)
            {
                return HandleNullParent();
            }

            else if (child == null)
            {
                return HandleNullChild(parentID);
            }
            else if (!BaseEntity.ParentCanAttachChild(parent, child))
            {
                return HandleParentCannotAttachChild(parent);
            }
            else return new EmptyResult();
        }

        private ActionResult HandleParentCannotAttachChild(T parent) => RedirectToAction(UIViewType.Details.ToString(), new { id = parent.ID, actionMethod = UIViewType.Edit }).WithError("Element Could not Be Attached - So It Could Not Be Detached");

        private ActionResult HandleNullChild(int? parentID) =>
            RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithWarning(EntityName + " was not found");


        private ActionResult HandleNullParent() =>// TODO: log error - this could be a developer problem
                        RedirectToAction(UIViewType.Index.ToString()).WithWarning(EntityName + " was not found");

        private ActionResult HandleNullParentID() => RedirectToAction(UIViewType.Index.ToString()).WithWarning(EntityName + " was not found");




        public ActionResult DetachASetOf<TChild>(int? ID, List<TChild> selected)
               where TChild : BaseEntity, IEntity, IPossibleChildren, new()
        {
            string parentName = typeof(T).ToString().Split('.').Last();
            string childName = typeof(TChild).ToString().Split('.').Last();

         
           TChild child = new TChild();

            if (ID == null) { return HandleNullParentID(); }

            IEntity parent = Repo.GetById((int)ID);

            if (parent == null)
            {
                return HandleNullParent();
            }

            else if (child == null)
            {
                return HandleNullChild(ID);
            }
            else if (!BaseEntity.ParentCanAttachChild( parent, child))
            {
                return HandleParentCannotAttachChild((T)parent);
            }


            if (selected == null || selected.Count == 0)
            {
                return DetachAll<TChild>(ID);
            }
            else
            {
                parent = child.RemoveSelectionFromChildren((IEntityChildClassIngredients)parent, selected);

                Repo.Save((T)parent);

                return RedirectToAction(UIViewType.Details.ToString(), new { ID, actionMethod = UIViewType.Edit }).WithSuccess("All " + childName + "s Were Successfully Detached!");
            }
        }



        public ActionResult DetachAll<TChild>(int? ID)
            where TChild : BaseEntity, IEntity, IPossibleChildren, new()
        {
            if (ID == null) { return HandleNullParentID(); }

            IEntity parent = Repo.GetById((int)ID);

            if (parent == null)
            {
                return HandleNullParent();
            }
            TChild child = new TChild();
            child.ParentRemoveAllChildrenOfAType(parent, child);
            Repo.Save((T)parent);
            string childName = typeof(TChild).ToString().Split('.').Last();

            return RedirectToAction(UIViewType.Details.ToString(), new { ID, actionMethod = UIViewType.Edit }).WithSuccess("All " + childName + "s Were Successfully Detached!");
        }
    }
}
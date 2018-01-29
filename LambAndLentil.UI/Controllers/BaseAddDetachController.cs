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
        ActionResult IGenericController<T>.Details(int id) => BaseDetails(  id );


        // GET: Ingredients/Create 
        [ActionName("Create")]
        ActionResult IGenericController<T>.Create() => BaseCreate();

        [HttpGet]
        ActionResult IGenericController<T>.Edit(int id) => BaseEdit(  id );

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
            ActionResult actionResult = GuardAttachAndDetachMethod(parent , child);
            if (actionResult is EmptyResult)
            { 
                parent =  new ChildAttachment().AddChildToParent(parent, child) ;
                Repo.Save((T)parent);
                string childEntity = child.GetType().ToString().Split('.').Last();
                return HandleSuccessfulAttachment(parent.ID , childEntity);
            }
            else return actionResult;
        }

        private ActionResult HandleSuccessfulAttachment(int? parentID, string childEntity) =>
                        RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithSuccess(childEntity + " was Successfully Attached!");

        public ActionResult Detach (IEntity parent, IEntity child) 
        { 
               
                ActionResult actionResult = GuardAttachAndDetachMethod(parent , child);
                if (actionResult is EmptyResult)
                {
                    parent = new ChildDetachment().DetachAnIndependentChild(parent, child);
                    Repo.Save( (T)parent);
                    string childEntity = child.GetType().ToString().Split('.').Last();
                    return HandleSuccessfulDetachment(parent.ID, childEntity);
                }
                else return actionResult;  
        }

        private ActionResult HandleSuccessfulDetachment(int? parentID, string childEntity) => RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithSuccess(childEntity + " was Successfully Detached!");

        ActionResult GuardAttachAndDetachMethod(IEntity parent, IEntity child)
        {
            if (!parent.CanHaveChild(child))
            {
                return HandleParentCannotAttachChild(parent);
            }
            else return new EmptyResult();
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
            else return GuardAttachAndDetachMethod(parent, child);
        }

        private ActionResult HandleParentCannotAttachChild(IEntity parent) => RedirectToAction(UIViewType.Details.ToString(), new { id = parent.ID, actionMethod = UIViewType.Edit }).WithError("Element Could not Be Attached - So It Could Not Be Detached");

        private ActionResult HandleNullChild(int? parentID) =>
            RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithWarning(EntityName + " was not found");


        private ActionResult HandleNullParent() =>// TODO: log error - this could be a developer problem
                        RedirectToAction(UIViewType.Index.ToString()).WithWarning(EntityName + " was not found");

        private ActionResult HandleNullParentID() => RedirectToAction(UIViewType.Index.ToString()).WithWarning(EntityName + " was not found");


        public ActionResult DetachASetOf(IEntity parent, List<IEntity> selected)
        {
            IEntity _parent =  parent;
            string parentName = _parent.GetType().ToString().Split('.').Last();
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
                return DetachAll(parent,child.GetType());
            }
            else
            {
                parent = new ChildDetachment().DetachSelectionFromChildren(parent, selected);
                Repo.Save((T)parent);

                return HandleSuccessfulDetachment(parent.ID,childName);
            }
        }



        public ActionResult DetachAll(IEntity parent, Type type ) 
        { 
            if (parent == null)
            {
                return HandleNullParent();
            }
             
            parent = new ChildDetachment().DetachAllChildrenOfAType(parent, type);
            Repo.Save((T)parent);
            string childName =  type.ToString().Split('.').Last();
            return HandleAllChildrenSuccessfullyDetached(parent.ID, childName);
        }

        private ActionResult HandleAllChildrenSuccessfullyDetached(int? ID, string childName) =>
                    RedirectToAction(UIViewType.Details.ToString(), new { ID, actionMethod = UIViewType.Edit }).WithSuccess("All " + childName + "s Were Successfully Detached!");

       
    }
}
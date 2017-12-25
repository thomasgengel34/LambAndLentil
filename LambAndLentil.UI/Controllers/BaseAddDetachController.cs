using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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


        //ActionResult  Attach<TChild>(int? iD, TChild child, int orderNumber)
        //    => BaseAttach(Repo, iD, child, AttachOrDetach.Attach, orderNumber);

        /*  ActionResult IAttachDetachController.Detach<TChild>(int? iD, TChild child, int orderNumber) => BaseAttach<TChild>(Repo, iD, child, AttachOrDetach.Detach, orderNumber);
       */


        public ActionResult Attach<TChild>(IRepository<T> Repo, int? parentID, TChild child, AttachOrDetach attachOrDetach = AttachOrDetach.Attach, int orderNumber = 0)
                 where TChild : BaseEntity, IEntity, IPossibleChildren,new() 
        {
            char[] charsToTrim = { 'V', 'M' };

            string childEntity = typeof(TChild).ToString().Split('.').Last().TrimEnd(charsToTrim);

            if (parentID == null)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning(EntityName + " was not found");
            }
            int parentNonNullID = (int)parentID;
            T parent = Repo.GetById(parentNonNullID);
            if (parent == null)
            {
                // TODO: log error - this could be a developer problem
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning(EntityName + " was not found");
            }
            if (AttachOrDetach.Detach == attachOrDetach && orderNumber < 0)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning("Order Number Was Negative! Nothing was detached");
            }
            else if (child == null)
            {
                return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithWarning(EntityName + " was not found"); ;
            }
            if (!BaseEntity.ParentCanAttachChild(parent, child))
            {
                if (attachOrDetach == AttachOrDetach.Detach)
                {// TODO: log. Probably a developer error. Or someone is playing games.
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = parent.ID, actionMethod = UIViewType.Edit }).WithError("Element Could not Be Attached - so it could not be detached");
                }
                else
                {
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = parent.ID, actionMethod = UIViewType.Edit }).WithError("Element Could not Be Attached!");
                }
            }
            if (attachOrDetach == AttachOrDetach.Detach)
            {
                Repo.DetachAnIndependentChild(parent.ID, child, orderNumber);
                return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithSuccess(childEntity + " was Successfully Detached!");
            }
            else
            {
                Repo.AttachAnIndependentChild(parent.ID, child, orderNumber);
                return RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithSuccess(childEntity + " was Successfully Attached!");
            }

        }


        public ActionResult Detach<TChild>(int? iD, TChild child)
             where TChild : BaseEntity, IEntity, IPossibleChildren,new()
              =>
         Attach(Repo, iD, child, AttachOrDetach.Detach, 0);




        public ActionResult DetachASetOf<TChild>(int? ID, List<TChild> selected)
               where TChild : BaseEntity, IEntity, IPossibleChildren, new()
        {
            string parentName = typeof(T).ToString().Split('.').Last();
            string childName = typeof(TChild).ToString().Split('.').Last();

            T tempParent = new T();
            TChild child = new TChild(); 

            if (ID == null)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning(parentName + " was not found");
            }

            IEntity  parent = Repo.GetById((int)ID);

            if (parent == null)
            {
                // TODO: log error - this could be a developer problem
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning(parentName + " was not found");
            }
            bool canAttachChild = BaseEntity.ParentCanAttachChild(tempParent,child);
            if (!canAttachChild)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithError("Cannot Attach that Child!");
            }
            if (selected == null || selected.Count == 0)
            {
                return DetachAll<TChild>(ID);
            }
            else
            { 
                parent=child.RemoveSelectionFromChildren( parent,selected);

                Repo.Save((T)parent);


                return RedirectToAction(UIViewType.Details.ToString(), new { ID, actionMethod = UIViewType.Edit }).WithSuccess("All " + childName + "s Were Successfully Detached!");
            } 
        }

    

        public ActionResult DetachAll<TChild>(int? ID)
            where TChild : BaseEntity, IEntity, IPossibleChildren, new()
        {
            if (ID == null)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning(EntityName + " was not found");
            }

            IEntity parent = Repo.GetById((int)ID);

            if (parent == null)
            {
                // TODO: log error - this could be a developer problem
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning(EntityName + " was not found");
            }
            TChild child = new TChild();
            child.ParentRemoveAllChildrenOfAType(parent, child);
            Repo.Save((T)parent);
            string childName = typeof(TChild).ToString().Split('.').Last();

            return RedirectToAction(UIViewType.Details.ToString(), new { ID, actionMethod = UIViewType.Edit }).WithSuccess("All " + childName + "s Were Successfully Detached!");
        }
    }
}
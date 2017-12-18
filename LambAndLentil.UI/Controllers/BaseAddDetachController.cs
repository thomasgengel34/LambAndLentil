using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;

namespace LambAndLentil.UI.Controllers
{
    public class BaseAttachDetachController<T> : BaseController<T>, IGenericController<T>, IAttachDetachController
     where T : BaseEntity, IEntity, new()
    {
        // TODO: get UIControllerType for the appropriate T


        public BaseAttachDetachController(IRepository<T> repository) : base(repository)
        {
            Repo = repository;
             UIControllerType = GetUIControllerType();
        }

        private UIControllerType GetUIControllerType()
        {
            if (typeof(T)==typeof(Ingredient)) return UIControllerType.Ingredients;
            if (typeof(T)==typeof(Menu)) return UIControllerType.Menus;
            if (typeof(T)==typeof(Person)) return UIControllerType.Persons;
            if (typeof(T)==typeof(Plan)) return UIControllerType.Plans;
            if (typeof(T)==typeof(ShoppingList)) return UIControllerType.ShoppingLists;
            if (typeof(T)==typeof(Recipe)) return UIControllerType.Recipes; 
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

        public ActionResult DetachAllIngredients(int ID) => BaseDetachAllIngredientChildren(Repo, ID, null); 

     

        public void DetachLastIngredientChild(int ID) => BaseDetachLastIngredientChild(Repo, ID); 

        
        //ActionResult  Attach<TChild>(int? iD, TChild child, int orderNumber)
        //    => BaseAttach(Repo, iD, child, AttachOrDetach.Attach, orderNumber);

      /*  ActionResult IAttachDetachController.Detach<TChild>(int? iD, TChild child, int orderNumber) => BaseAttach<TChild>(Repo, iD, child, AttachOrDetach.Detach, orderNumber);
     */
        ActionResult IAttachDetachController.DetachASetOf<TChild>(int ID, List<TChild> selected) => BaseAttachASetOf<TChild>(Repo, ID,AttachOrDetach.Detach,  selected) ;

        ActionResult IAttachDetachController.Attach<TChild>(int? iD, TChild child, int orderNumber) => BaseAttach<TChild>(Repo,iD, child, AttachOrDetach.Attach );

        ActionResult IAttachDetachController.Detach<TChild>(int? iD, TChild child, int orderNumber) => BaseAttach<TChild>(Repo, iD, child, AttachOrDetach.Detach);
    }
}
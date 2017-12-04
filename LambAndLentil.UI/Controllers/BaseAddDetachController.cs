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

        ViewResult IGenericController<T>.Index(int? page) => BaseIndex(Repo, page);


        // GET: Recipes/Details/5
        ActionResult IGenericController<T>.Details(int id, UIViewType actionMethod) => BaseDetails(Repo, UIControllerType, id, actionMethod);


        // GET: Ingredients/Create 
        [ActionName("Create")]
        ViewResult IGenericController<T>.Create(UIViewType actionMethod) => BaseCreate(actionMethod);

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



        ActionResult IAttachDetachController.AttachIngredient(int? iD, Ingredient child, int orderNumber) => BaseAttach(Repo, iD, child);

        ActionResult IAttachDetachController.DetachIngredient(int? iD, Ingredient child, int orderNumber) => BaseAttach(Repo, iD, child, AttachOrDetach.Detach, orderNumber);

        void IGenericController<T>.AddIngredientToIngredientsList(int id, string addedIngredient) => BaseAddIngredientToIngredientsList(Repo, UIControllerType, id, addedIngredient);

        public ActionResult DetachAllIngredients(int ID) => BaseDetachAllIngredientChildren(Repo, ID, null); 

        ActionResult IAttachDetachController.DetachAllIngredients(int ID, List<Ingredient> selected) => BaseDetachAllIngredientChildren(Repo, ID, selected); 

        public void DetachLastIngredientChild(int ID) => BaseDetachLastIngredientChild(Repo, ID);

        ActionResult IAttachDetachController.DetachASetOfIngredients(int ID, List<Ingredient> selected) => BaseDetachAllIngredientChildren(Repo, ID, selected);

        ActionResult IAttachDetachController.AttachRecipe(int? recipeID, Recipe child, int orderNumber) =>
         BaseAttach(Repo, recipeID, child); 

        ActionResult IAttachDetachController.DetachRecipe(int? recipeID, Recipe child, int orderNumber) => BaseAttach(Repo, recipeID, child, AttachOrDetach.Detach, orderNumber); 

        ActionResult IAttachDetachController.DetachAllRecipes(int ID, List<Recipe> selected) => BaseDetachAllRecipeChildren(Repo, ID, selected);

        ActionResult IAttachDetachController.DetachASetORecipes(int ID, List<Recipe> selected) => BaseDetachAllRecipeChildren(Repo, ID, selected);

        ActionResult IAttachDetachController.AttachMenu(int iD, Menu child, int orderNumber) => BaseAttach(Repo, iD, child, AttachOrDetach.Attach, orderNumber);

        ActionResult IAttachDetachController.DetachMenu(int iD, Menu child, int orderNumber) => BaseAttach(Repo, iD, child, AttachOrDetach.Detach, orderNumber);

        ActionResult IAttachDetachController.DetachAllMenus(int ID, List<Menu> selected) => BaseDetachAllMenuChildren(Repo, ID, selected);

        ActionResult IAttachDetachController.AttachPlan(int iD, Plan child, int orderNumber) => BaseAttach(Repo, iD, child, AttachOrDetach.Attach, orderNumber);

        ActionResult IAttachDetachController.DetachPlan(int iD, Menu child, int orderNumber) => BaseAttach(Repo, iD, child, AttachOrDetach.Detach, orderNumber); 

        ActionResult IAttachDetachController.AttachShoppingList(int iD, ShoppingList child, int orderNumber) => BaseAttach(Repo, iD, child, 0);

        ActionResult IAttachDetachController.DetachShoppingList(int iD, Menu child, int orderNumber) => BaseAttach(Repo, iD, child, AttachOrDetach.Detach, orderNumber);

        ActionResult IAttachDetachController.DetachAllShoppingLists(int ID, List<ShoppingList> selected) => BaseDetachAllShoppingListChildren(Repo, ID, selected); 

        public void DetachAllShoppingLists(int ID) => BaseDetachAllShoppingListChildren(Repo, ID, null); 

        ActionResult IAttachDetachController.DetachASetOMenus(int ID, List<Menu> selected) => BaseDetachAllMenuChildren(Repo, ID, selected);

        ActionResult IAttachDetachController.DetachAllPlans(int ID, List<Plan> selected) => BaseDetachAllPlanChildren(Repo, ID, selected);

        ActionResult IAttachDetachController.DetachASetOPlans(int ID, List<Plan> selected) => BaseDetachAllPlanChildren(Repo, ID, selected);

        ActionResult IAttachDetachController.DetachASetOShoppingLists(int ID, List<ShoppingList> selected) => BaseDetachAllShoppingListChildren(Repo, ID, selected);
    }
}
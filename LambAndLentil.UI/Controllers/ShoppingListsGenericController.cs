using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace LambAndLentil.UI.Controllers
{
    public class ShoppingListsGenericController<T> : BaseController<ShoppingList>, IGenericController<T>
          where T : ShoppingList
    {
        int IGenericController<T>.PageSize { get; set; }

        public ShoppingListsGenericController(IRepository<ShoppingList> repository) : base(repository) => Repo = repository;



        // GET: ShoppingList
        public ViewResult Index(int? page = 1)
        {
            ViewResult view = BaseIndex(Repo, page);
            return View(view.ViewName, view.Model);
        }

        // GET: ShoppingList/Details/5

        ActionResult IGenericController<T>.Details(int id=1, UIViewType actionMethod= UIViewType.Details) =>    BaseDetails(Repo, UIControllerType.ShoppingLists, id, actionMethod);

        // GET: ShoppingList/Create 
        ViewResult IGenericController<T>.Create(UIViewType actionMethod) =>  BaseCreate(actionMethod); 

        // GET: ShoppingList/Edit/5
        public ActionResult Edit(int id = 1) => BaseDetails(Repo, UIControllerType.ShoppingLists, id, UIViewType.Edit); 

        //POST: ShoppingList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,Name,Description, Author, CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser")] T t) => BasePostEdit(Repo, t);

        // GET: ShoppingList/Delete/5
        [ActionName("Delete")]
        ActionResult IGenericController<T>.Delete(int id=1, UIViewType actionMethod = UIViewType.Delete)   => BaseDelete(Repo, UIControllerType.ShoppingLists, id);

        // POST: ShoppingList/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        ActionResult IGenericController<T>.DeleteConfirmed(int id)   => BaseDeleteConfirmed(Repo, UIControllerType.ShoppingLists, id);

        public ActionResult AttachIngredient(int? shoppingListID, Ingredient ingredient, int orderNumber = 0) => BaseAttach(Repo, shoppingListID, ingredient, AttachOrDetach.Attach, orderNumber);


        public ActionResult DetachIngredient(int? shoppingListID, Ingredient ingredient, int orderNumber = 0) => BaseAttach(Repo, shoppingListID, ingredient, AttachOrDetach.Detach, orderNumber);

        void IGenericController<T>.AddIngredientToIngredientsList(int id, string addedIngredient)  => BaseAddIngredientToIngredientsList(Repo, UIControllerType.Recipes, id, addedIngredient);

        // public ActionResult DetachAllIngredients(int ID) => BaseDetachAllIngredientChildren(Repo, ID,null );

        public ActionResult DetachAllIngredients(int ID, List<Ingredient> selected=null) => BaseDetachAllIngredientChildren(Repo, ID, selected );
        

        public ActionResult AttachRecipe(int? recipeID, Recipe recipe, int orderNumber = 0) =>
         BaseAttach(Repo, recipeID, recipe);

        public ActionResult DetachRecipe(int? recipeID, Recipe child, int orderNumber = 0) => BaseAttach(Repo, recipeID, child, AttachOrDetach.Detach, orderNumber);

        public ActionResult AttachMenu(int iD, Menu child, int orderNumber = 0) =>    BaseAttach(Repo, iD, child, AttachOrDetach.Attach, orderNumber); 

        public ActionResult DetachMenu(int iD, Menu child, int orderNumber = 0) => BaseAttach(Repo, iD, child, AttachOrDetach.Detach, orderNumber);

        public ActionResult AttachPlan(int iD, Plan child, int orderNumber = 0) => BaseAttach(Repo, iD, child, AttachOrDetach.Attach, orderNumber);

        public ActionResult DetachPlan(int iD, Plan plan, int orderNumber = 0) => BaseAttach(Repo, iD, plan, AttachOrDetach.Detach, orderNumber);

        public void DetachAllMenus(int ID) => BaseDetachAllMenuChildren(Repo, ID, null);

        public void DetachAllMenus(int ID, List<Menu> selected) => BaseDetachAllMenuChildren(Repo, ID, selected);

        public void DetachAllShoppingLists(int ID) => BaseDetachAllShoppingListChildren(Repo, ID, null);

        public void DetachAllShoppingLists(int ID, List<ShoppingList> selected) => BaseDetachAllShoppingListChildren(Repo, ID, selected);
      
      
      
       
        ActionResult IGenericController<T>.Edit(int id) => throw new System.NotImplementedException();
      
        ActionResult IGenericController<T>.PostEdit(T t) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.AttachIngredient(int? iD, Ingredient child, int orderNumber) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.DetachIngredient(int? iD, Ingredient child, int orderNumber) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.DetachAllIngredients(int ID, List<Ingredient> selected) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.DetachASetOfIngredients(int ID, List<Ingredient> selected) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.AttachRecipe(int? recipeID, Recipe child, int orderNumber) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.DetachRecipe(int? recipeID, Recipe child, int orderNumber) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.DetachAllRecipes(int ID, List<Recipe> selected) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.DetachASetORecipes(int ID, List<Recipe> selected) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.AttachMenu(int iD, Menu child, int orderNumber) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.DetachMenu(int iD, Menu child, int orderNumber) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.DetachAllMenus(int ID, List<Menu> selected) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.DetachASetOMenus(int ID, List<Menu> selected) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.AttachPlan(int iD, Plan child, int orderNumber) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.DetachPlan(int iD, Menu child, int orderNumber) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.DetachAllPlans(int ID, List<Plan> selected) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.DetachASetOPlans(int ID, List<Plan> selected) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.AttachShoppingList(int iD, ShoppingList child, int orderNumber) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.DetachShoppingList(int iD, Menu child, int orderNumber) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.DetachAllShoppingLists(int ID, List<ShoppingList> selected) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.DetachASetOShoppingLists(int ID, List<ShoppingList> selected) => throw new System.NotImplementedException();
    }
}

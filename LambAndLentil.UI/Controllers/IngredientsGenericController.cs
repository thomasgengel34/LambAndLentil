using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;

namespace LambAndLentil.UI.Controllers
{  
    public class IngredientsGenericController<T> : BaseController<Ingredient>, IGenericController<T> where T : Ingredient
    {
        int IGenericController<T>.PageSize { get; set; }

        public IngredientsGenericController(IRepository<Ingredient> repository) : base(repository) => Repo = repository;

        // GET: Ingredients  
        ViewResult IGenericController<T>.Index(int? page ) => BaseIndex(Repo, page); 

        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details) => BaseDetails(Repo, UIControllerType.Ingredients, id, actionMethod);

        // GET: Ingredients/Create 
        public ViewResult Create(UIViewType actionMethod) => BaseCreate(actionMethod);

        // GET: Ingredients/Edit/5
        [HttpGet]
        public ActionResult Edit(int id = 1) => BaseDetails(Repo, UIControllerType.Ingredients, id, UIViewType.Edit);


        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID, Name, Description, CreationDate,  IngredientsList")]  T t) => BasePostEdit(Repo, t);

        // GET: Ingredients/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete)
        {
            ViewBag.ActionMethod = UIViewType.Delete;
            return BaseDelete(Repo, UIControllerType.Ingredients, id);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) => BaseDeleteConfirmed(Repo, UIControllerType.Ingredients, id);





        public ActionResult AttachIngredient(int? ingredientID, Ingredient ingredient, int orderNumber=0) => BaseAttach<Ingredient>(Repo, ingredientID, ingredient, 0);

        public ActionResult DetachIngredient(int? ingredientID, Ingredient ingredient, int orderNumber = 0) => BaseAttach<Ingredient>(Repo, ingredientID, ingredient, AttachOrDetach.Detach);



        public void AddIngredientToIngredientsList(int id = 1, string addedIngredient = "")=>
            BaseAddIngredientToIngredientsList(Repo, UIControllerType.Recipes, id, addedIngredient); 


       public ActionResult DetachAllIngredients(int ID) => BaseDetachAllIngredientChildren(Repo, ID,null);



        public ActionResult DetachAllIngredients(int ID, List<Ingredient> selected) => BaseDetachAllIngredientChildren(Repo, ID,selected);

        public void DetachLastIngredientChild(int ID) => BaseDetachLastIngredientChild(Repo, ID);
        public ActionResult AttachRecipe(int iD, Recipe recipe) => throw new NotToBeImplementedException();
        public ActionResult AttachRecipe(int? recipeID, Recipe recipe, int orderNumber = 0) => throw new NotImplementedException();
        public ActionResult DetachRecipe(int? recipeID, Recipe child, int orderNumber = 0) => throw new NotImplementedException();
        public ActionResult AttachMenu(int iD, Menu child, int orderNumber = 0) => throw new NotImplementedException();
        public ActionResult DetachMenu(int iD, Menu child, int orderNumber = 0) => throw new NotImplementedException();

        
        
       
        public ActionResult AttachPlan(int iD, Plan child, int orderNumber = 0) => throw new NotImplementedException();
        void IGenericController<T>.AddIngredientToIngredientsList(int id, string addedIngredient) => throw new NotImplementedException();
        ViewResult IGenericController<T>.Create(UIViewType actionMethod) => throw new NotImplementedException();
        ActionResult IGenericController<T>.Delete(int id, UIViewType actionMethod) => throw new NotImplementedException();
        ActionResult IGenericController<T>.DeleteConfirmed(int id) => throw new NotImplementedException();
        ActionResult IGenericController<T>.Details(int id, UIViewType actionMethod) => throw new NotImplementedException();
        ActionResult IGenericController<T>.Edit(int id) => throw new NotImplementedException(); 
        ActionResult IGenericController<T>.PostEdit(T t) => throw new NotImplementedException();
        ActionResult IAttachDetachController.AttachIngredient(int? iD, Ingredient child, int orderNumber) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachIngredient(int? iD, Ingredient child, int orderNumber) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachAllIngredients(int ID, List<Ingredient> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachASetOfIngredients(int ID, List<Ingredient> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.AttachRecipe(int? recipeID, Recipe child, int orderNumber) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachRecipe(int? recipeID, Recipe child, int orderNumber) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachAllRecipes(int ID, List<Recipe> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachASetORecipes(int ID, List<Recipe> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.AttachMenu(int iD, Menu child, int orderNumber) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachMenu(int iD, Menu child, int orderNumber) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachAllMenus(int ID, List<Menu> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachASetOMenus(int ID, List<Menu> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.AttachPlan(int iD, Plan child, int orderNumber) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachPlan(int iD, Menu child, int orderNumber) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachAllPlans(int ID, List<Plan> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachASetOPlans(int ID, List<Plan> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.AttachShoppingList(int iD, ShoppingList child, int orderNumber) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachShoppingList(int iD, Menu child, int orderNumber) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachAllShoppingLists(int ID, List<ShoppingList> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachASetOShoppingLists(int ID, List<ShoppingList> selected) => throw new NotImplementedException();
    }
}

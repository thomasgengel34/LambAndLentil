using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using LambAndLentil.UI.Infrastructure.Alerts;
using System;

namespace LambAndLentil.UI.Controllers
{ 
    public class RecipesGenericController<T > : BaseController<Recipe>, IGenericController<T>
          where T: Recipe  
    {
        int IGenericController<T>.PageSize { get; set; }

        public RecipesGenericController(IRepository<Recipe> repository) : base(repository) => Repo = repository;

        // GET: Recipes
        public ViewResult Index(int? page = 1) => BaseIndex(Repo, page);


        public void AddIngredientToIngredientsList(int id = 1, string addedIngredient = "") => BaseAddIngredientToIngredientsList(Repo, UIControllerType.Recipes, id, addedIngredient);





        // GET: Recipes/Details/5
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details) => BaseDetails(Repo, UIControllerType.Recipes, id, actionMethod);

        // GET: Ingredients/Create 
        public ViewResult Create(UIViewType actionMethod) => BaseCreate(actionMethod);


        // GET: Recipes/Edit/5
        public ActionResult Edit(int id = 1) => BaseDetails(Repo, UIControllerType.Recipes, id, UIViewType.Edit);



        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,Name,Description,Servings,MealType,Calories,CalsFromFat,CreationDate, ModifiedDate,AddedByUser, ModifiedByUser ")] T t) => BasePostEdit(Repo, t);

        // GET: Recipes/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete) => BaseDelete(Repo, UIControllerType.Recipes, id);

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        ActionResult IGenericController<T>.DeleteConfirmed(int id)  => BaseDeleteConfirmed(Repo, UIControllerType.Recipes, id);

        public ActionResult AttachIngredient(int? recipeID, Ingredient ingredient, int orderNumber = 0) => BaseAttach<Ingredient>(Repo, recipeID, ingredient);

        public ActionResult DetachIngredient(int? recipeID, Ingredient ingredient, int orderNumber = 0) => BaseAttach<Ingredient>(Repo, recipeID, ingredient, AttachOrDetach.Detach);

        public void DetachAllIngredients(int ID) => BaseDetachAllIngredientChildren(Repo, ID,null);

        public  ActionResult  DetachAllIngredients(int ID, List<Ingredient> selected) => BaseDetachAllIngredientChildren(Repo, ID, selected);
       
        public ActionResult AttachRecipe(int? recipeID, Recipe recipe, int orderNumber = 0) => BaseAttach(Repo, recipeID, recipe);

        public ActionResult DetachRecipe(int? recipeID, Recipe child, int orderNumber = 0) => BaseAttach(Repo, recipeID, child, AttachOrDetach.Detach, orderNumber);

        public ActionResult AttachPlan(int iD, Plan plan, int orderNumber) => BaseAttach(Repo, iD, plan);


        public ActionResult AttachMenu(int iD, Menu child, int orderNumber) => BaseAttach(Repo, iD, child);
        public ActionResult DetachMenu(int iD, Menu child, int orderNumber) => BaseAttach(Repo, iD, child,AttachOrDetach.Detach,orderNumber  );
        void IGenericController<T>.AddIngredientToIngredientsList(int id, string addedIngredient) => throw new NotImplementedException();
        ViewResult IGenericController<T>.Create(UIViewType actionMethod) => throw new NotImplementedException();
        ActionResult IGenericController<T>.Delete(int id, UIViewType actionMethod) => throw new NotImplementedException();
       
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

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
    public class RecipesGenericController<T> : BaseController<Recipe>, IGenericController<T>
          where T : Recipe
    {
        int IGenericController<T>.PageSize { get; set; }

        public RecipesGenericController(IRepository<Recipe> repository) : base(repository) => Repo = repository;

        // GET: Recipes
        public ViewResult Index(int? page = 1) => BaseIndex(Repo, page);

        void IGenericController<T>.AddIngredientToIngredientsList(int id, string addedIngredient) =>  BaseAddIngredientToIngredientsList(Repo, UIControllerType.Recipes, id, addedIngredient); 


        // GET: Recipes/Details/5
        ActionResult IGenericController<T>.Details(int id, UIViewType actionMethod) => BaseDetails(Repo, UIControllerType.Recipes, id, actionMethod);

        // GET: Ingredients/Create 
        ViewResult IGenericController<T>.Create(UIViewType actionMethod) => BaseCreate(actionMethod);


        // GET: Recipes/Edit/5 
        ActionResult IGenericController<T>.Edit(int id) => BaseDetails(Repo, UIControllerType.Recipes, id, UIViewType.Edit);

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        ActionResult IGenericController<T>.PostEdit([Bind(Include = "ID,Name,Description,Servings,MealType,Calories,CalsFromFat,CreationDate, ModifiedDate,AddedByUser, ModifiedByUser ")] T t) => BasePostEdit(Repo, t);

        // GET: Recipes/Delete/5
        [ActionName("Delete")]
        ActionResult IGenericController<T>.Delete(int id, UIViewType actionMethod) => BaseDelete(Repo, UIControllerType.Recipes, id);

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        ActionResult IGenericController<T>.DeleteConfirmed(int id) => BaseDeleteConfirmed(Repo, UIControllerType.Recipes, id);

        ActionResult IAttachDetachController.AttachIngredient(int? iD, Ingredient child, int orderNumber) => BaseAttach<Ingredient>(Repo, iD, child);

        ActionResult IAttachDetachController.DetachIngredient(int? iD, Ingredient child, int orderNumber) => BaseAttach<Ingredient>(Repo, iD, child, AttachOrDetach.Detach); 

        public ActionResult DetachAllIngredients(int ID, List<Ingredient> selected) => BaseDetachAllIngredientChildren(Repo, ID, selected);

        ActionResult IAttachDetachController.AttachRecipe(int? recipeID, Recipe child, int orderNumber) => BaseAttach(Repo, recipeID, child);

        ActionResult IAttachDetachController.DetachRecipe(int? recipeID, Recipe child, int orderNumber) => BaseAttach(Repo, recipeID, child, AttachOrDetach.Detach, orderNumber);

        ActionResult IAttachDetachController.AttachPlan(int iD, Plan child, int orderNumber) => BaseAttach(Repo, iD, child);

        ActionResult IAttachDetachController.AttachMenu(int iD, Menu child, int orderNumber) => BaseAttach(Repo, iD, child);

        ActionResult IAttachDetachController.DetachMenu(int iD, Menu child, int orderNumber) => BaseAttach(Repo, iD, child, AttachOrDetach.Detach, orderNumber);

      


        ActionResult IAttachDetachController.DetachASetOfIngredients(int ID, List<Ingredient> selected) => throw new NotImplementedException();


        ActionResult IAttachDetachController.DetachAllRecipes(int ID, List<Recipe> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachASetORecipes(int ID, List<Recipe> selected) => throw new NotImplementedException();


        ActionResult IAttachDetachController.DetachAllMenus(int ID, List<Menu> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachASetOMenus(int ID, List<Menu> selected) => throw new NotImplementedException();

        ActionResult IAttachDetachController.DetachPlan(int iD, Menu child, int orderNumber) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachAllPlans(int ID, List<Plan> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachASetOPlans(int ID, List<Plan> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.AttachShoppingList(int iD, ShoppingList child, int orderNumber) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachShoppingList(int iD, Menu child, int orderNumber) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachAllShoppingLists(int ID, List<ShoppingList> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachASetOShoppingLists(int ID, List<ShoppingList> selected) => throw new NotImplementedException();
    }
}

using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.UI.Controllers
{
    public class PersonsGenericController<T> : BaseController<Person>, IGenericController<T>
        where T : Person, new()
    {
        int IGenericController<T>.PageSize { get; set; }

        public PersonsGenericController(IRepository<Person> repository) : base(repository) => Repo = repository;

        // GET: Persons
        ViewResult IGenericController<T>.Index(int? page)
        {
            ViewResult view = BaseIndex(Repo, page);
            return View(view.ViewName, view.Model);
        }

        // GET: Persons/Details/5
        ActionResult IGenericController<T>.Details(int id, UIViewType actionMethod) =>   BaseDetails(Repo, UIControllerType.Persons, id, actionMethod);

       
        //GET: Persons/Create
        ViewResult IGenericController<T>.Create(UIViewType actionMethod)=> BaseCreate(actionMethod);

        // GET: Persons/Edit/5
        ActionResult IGenericController<T>.Edit(int id) =>   BaseDetails(Repo, UIControllerType.Persons, id, UIViewType.Edit);



        //POST: Persons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        ActionResult IGenericController<T>.PostEdit([Bind(Include = "ID,FirstName,LastName, Name, Description, Weight,  MinCalories, MaxCalories, NoGarlic, CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser")]T t) =>  BasePostEdit(Repo, t);


        // GET: Persons/Delete/5
        [ActionName("Delete")]
        ActionResult IGenericController<T>.Delete(int id, UIViewType actionMethod) =>  BaseDelete(Repo, UIControllerType.Persons, id, UIViewType.Delete);

        // POST: Persons/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        ActionResult IGenericController<T>.DeleteConfirmed(int id) => BaseDeleteConfirmed(Repo, UIControllerType.Persons, id);

        ActionResult IAttachDetachController.AttachIngredient(int? iD, Ingredient child, int orderNumber) => BaseAttach(Repo, iD, child);

        ActionResult IAttachDetachController.DetachIngredient(int? iD, Ingredient child, int orderNumber) =>  BaseAttach(Repo,  iD, child, AttachOrDetach.Detach);

        void IGenericController<T>.AddIngredientToIngredientsList(int id, string addedIngredient) => BaseAddIngredientToIngredientsList(Repo, UIControllerType.Recipes, id, addedIngredient = "");

       // public void DetachAllIngredients(int ID) => BaseDetachAllIngredientChildren(Repo, ID, null);

        public ActionResult DetachAllIngredients(int ID, List<Ingredient> selected) => BaseDetachAllIngredientChildren(Repo, ID, selected);

        public ActionResult AttachRecipe(int? recipeID, Recipe recipe, int orderNumber = 0) =>
         BaseAttach(Repo, recipeID, recipe);

        public ActionResult DetachRecipe(int? recipeID, Recipe child, int orderNumber = 0) => BaseAttach(Repo, recipeID, child, AttachOrDetach.Detach, orderNumber);


        public ActionResult AttachMenu(int iD, Menu child, int orderNumber = 0) => BaseAttach(Repo, iD, child, AttachOrDetach.Attach, orderNumber);

        public ActionResult DetachMenu(int iD, Menu child, int orderNumber = 0) => BaseAttach(Repo, iD, child, AttachOrDetach.Detach, orderNumber);

        public void DetachAllMenus(int ID) => BaseDetachAllMenuChildren(Repo, ID,null);

        public void DetachAllMenus(int ID, List<Menu> selected) => BaseDetachAllMenuChildren(Repo, ID, selected);

        ActionResult IAttachDetachController.DetachAllRecipes(int ID, List<Recipe> selected) =>   BaseDetachAllRecipeChildren(Repo, ID,selected); 

        public void DetachAllShoppingLists(int ID) => BaseDetachAllShoppingListChildren(Repo, ID,null);

        public void DetachAllShoppingLists(int ID, List<ShoppingList> selected) => BaseDetachAllShoppingListChildren(Repo, ID, selected);

        public void AttachShoppingList(int? ID, ShoppingList shoppingList, int orderNumber = 0) =>
         BaseAttach(Repo, ID, shoppingList);

        public ActionResult AttachPlan(int iD, Plan child, int orderNumber = 0) => BaseAttach(Repo, iD, child);



     

      
      
  
        ActionResult IAttachDetachController.DetachAllIngredients(int ID, List<Ingredient> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachASetOfIngredients(int ID, List<Ingredient> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.AttachRecipe(int? recipeID, Recipe child, int orderNumber) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachRecipe(int? recipeID, Recipe child, int orderNumber) => throw new NotImplementedException();
       
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

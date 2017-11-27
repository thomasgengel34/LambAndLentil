using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;

namespace LambAndLentil.UI.Controllers
{
    public class MenusGenericController<Menu> : BaseController<Menu>, IGenericController<Menu> 
            where Menu : BaseEntity, IEntity, new()
    {
        int IGenericController<Menu>.PageSize { get; set; }

        public MenusGenericController(IRepository<Menu> repository) : base(repository) => Repo = repository;
         
        // GET: Menus
         ViewResult IGenericController<Menu>.Index(int? page )
        {
            ViewResult view = BaseIndex(Repo,  page);
            return View(view.ViewName, view.Model);
        }

        // GET: Menus/Details/5 
        ActionResult IGenericController<Menu>.Details(int id, UIViewType actionMethod) =>  BaseDetails(Repo, UIControllerType.Menus, id, actionMethod);



        // GET: Menus/Create
        ViewResult IGenericController<Menu>.Create(UIViewType actionMethod) =>  BaseCreate(actionMethod);


        // GET: Menus/Edit/5

        ActionResult IGenericController<Menu>.Edit(int id) => BaseDetails(Repo, UIControllerType.Menus, id, UIViewType.Edit);

        //POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        ActionResult IGenericController<Menu>.PostEdit([Bind(Include = "ID,Name, DayOfWeek, Description,CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser, MealType,Diners, Recipes, Ingredients")]Menu t) => BasePostEdit(Repo, t);

        // GET: Menus/Delete/5
        [ActionName("Delete")]
        ActionResult IGenericController<Menu>.Delete(int id, UIViewType actionMethod) => BaseDelete(Repo, UIControllerType.Menus, id); 


        // POST: Ingredients/Delete/5 
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        ActionResult IGenericController<Menu>.DeleteConfirmed(int id) =>  BaseDeleteConfirmed(Repo, UIControllerType.Menus, id);


        public ActionResult AttachRecipe(int? menuID, Recipe recipe, int orderNumber)=>
            BaseAttach(Repo, menuID, recipe);


        ActionResult IAttachDetachController.DetachRecipe(int? recipeID, Recipe child, int orderNumber) =>  BaseAttach<Recipe>(Repo, recipeID, child, AttachOrDetach.Detach);

        public ActionResult DetachRecipe(int? recipeID, Recipe recipe, int orderNumber = 0) => BaseAttach(Repo, recipeID, recipe, AttachOrDetach.Detach, orderNumber);

        ActionResult IAttachDetachController.AttachIngredient(int? iD, Ingredient child, int orderNumber) => BaseAttach<Ingredient>(Repo, iD, child, AttachOrDetach.Attach, orderNumber);

        ActionResult IAttachDetachController.DetachIngredient(int? iD, Ingredient child, int orderNumber)   => BaseAttach<Ingredient>(Repo, iD, child, AttachOrDetach.Detach);

        public void AddIngredientToIngredientsList(int id = 1, string addedIngredient = "") => BaseAddIngredientToIngredientsList(Repo, UIControllerType.Recipes, id, addedIngredient);

        ActionResult IAttachDetachController.DetachAllIngredients(int ID, List<Ingredient> selected) =>  
            BaseDetachAllIngredientChildren(Repo, ID, selected); 

        ActionResult IAttachDetachController.AttachRecipe(int? recipeID, Recipe child, int orderNumber) => 
         BaseAttach(Repo, recipeID, child);

        public void AttachMenu(int iD, Domain.Entities.Menu child, int orderNumber = 0) => BaseAttach(Repo, iD, child, AttachOrDetach.Attach, orderNumber);
        public void DetachMenu(int iD, Domain.Entities.Menu child, int orderNumber = 0) => BaseAttach(Repo, iD, child, AttachOrDetach.Detach, orderNumber);
        public ActionResult AttachPlan(int iD, Plan plan, int orderNumber) => BaseAttach(Repo, iD, plan );

        void IGenericController<Menu>.AddIngredientToIngredientsList(int id, string addedIngredient) => BaseAddIngredientToIngredientsList(Repo, UIControllerType.Menus, id, addedIngredient);

        ActionResult IAttachDetachController.DetachASetOfIngredients(int ID, List<Ingredient> selected) => BaseDetachAllIngredientChildren(Repo, ID, selected);




        ActionResult IAttachDetachController.DetachAllRecipes(int ID, List<Recipe> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachASetORecipes(int ID, List<Recipe> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.AttachMenu(int iD, Domain.Entities.Menu child, int orderNumber) =>  BaseAttach(Repo, iD, child, AttachOrDetach.Attach, orderNumber);
        ActionResult IAttachDetachController.DetachMenu(int iD, Domain.Entities.Menu child, int orderNumber)  => BaseAttach(Repo, iD, child, AttachOrDetach.Detach, orderNumber);
        ActionResult IAttachDetachController.DetachAllMenus(int ID, List<Domain.Entities.Menu> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachASetOMenus(int ID, List<Domain.Entities.Menu> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.AttachPlan(int iD, Plan child, int orderNumber) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachPlan(int iD, Domain.Entities.Menu child, int orderNumber) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachAllPlans(int ID, List<Plan> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachASetOPlans(int ID, List<Plan> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.AttachShoppingList(int iD, ShoppingList child, int orderNumber) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachShoppingList(int iD, Domain.Entities.Menu child, int orderNumber) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachAllShoppingLists(int ID, List<ShoppingList> selected) => throw new NotImplementedException();
        ActionResult IAttachDetachController.DetachASetOShoppingLists(int ID, List<ShoppingList> selected) => throw new NotImplementedException();
    }
}

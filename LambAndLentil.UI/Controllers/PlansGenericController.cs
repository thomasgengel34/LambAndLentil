using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;

namespace LambAndLentil.UI.Controllers
{


    public class PlansGenericController<T > : BaseController<Plan>,IGenericController<T>
          where T: Plan 
    {
        int IGenericController<T>.PageSize { get; set; }

        public PlansGenericController(IRepository<Plan> repository) : base(repository) => Repo = repository;

        // GET: Plans
         ViewResult IGenericController<T>.Index(int? page )
        {
            ViewResult view = BaseIndex(Repo,  page);
            return View(view.ViewName, view.Model);
        }

        // GET: Plans/Details/5 
        ActionResult IGenericController<T>.Details(int id, UIViewType actionMethod) => BaseDetails(Repo, UIControllerType.Plans, id, actionMethod);

        //GET: Plans/Create
        ViewResult IGenericController<T>.Create(UIViewType actionMethod) =>   BaseCreate(actionMethod);

        // GET: Plans/Edit/5
        ActionResult IGenericController<T>.Edit(int id) =>  BaseDetails(Repo, UIControllerType.Plans, id, UIViewType.Edit);



        // POST: Plans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        ActionResult IGenericController<T>.PostEdit([Bind(Include = "ID,Name,Description, CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser")] T t) => BasePostEdit(Repo, t);


        // GET: Plans/Delete/5
        [ActionName("Delete")]
        ActionResult IGenericController<T>.Delete(int id, UIViewType actionMethod) =>  BaseDelete(Repo, UIControllerType.Plans, id); 

        // POST: Plans/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        ActionResult IGenericController<T>.DeleteConfirmed(int id)  => BaseDeleteConfirmed(Repo, UIControllerType.Plans, id);

        ActionResult IAttachDetachController.AttachIngredient(int? iD, Ingredient child, int orderNumber)   => BaseAttach(Repo, iD, child, AttachOrDetach.Attach, orderNumber);

        ActionResult IAttachDetachController.DetachIngredient(int? iD, Ingredient child, int orderNumber) =>  BaseAttach(Repo, iD, child, AttachOrDetach.Detach, orderNumber);

        public ActionResult AttachRecipe(int? planID, Recipe recipe) => BaseAttach(Repo, planID, recipe);


        public ActionResult DetachRecipe(int? planID, Recipe recipe) => BaseAttach(Repo, planID, recipe, AttachOrDetach.Detach);

        void IGenericController<T>.AddIngredientToIngredientsList(int id, string addedIngredient)  => BaseAddIngredientToIngredientsList(Repo, UIControllerType.Recipes, id, addedIngredient);

        ActionResult IAttachDetachController.DetachAllIngredients(int ID, List<Ingredient> selected) =>   BaseDetachAllIngredientChildren(Repo, ID, selected);

        public ActionResult  DetachAllIngredients(int ID, List<Ingredient> selected) => BaseDetachAllIngredientChildren(Repo, ID, selected);

        ActionResult IAttachDetachController.DetachASetOfIngredients(int ID, List<Ingredient> selected) => BaseDetachAllIngredientChildren(Repo,ID, selected);

        ActionResult IAttachDetachController.AttachRecipe(int? recipeID, Recipe child, int orderNumber)  =>   
         BaseAttach(Repo, recipeID, child );

        ActionResult IAttachDetachController.DetachRecipe(int? recipeID, Recipe child, int orderNumber)  => BaseAttach(Repo, recipeID, child, AttachOrDetach.Detach, orderNumber);

        ActionResult IAttachDetachController.DetachAllRecipes(int ID, List<Recipe> selected) => BaseDetachAllRecipeChildren(Repo, ID,null);

        public ActionResult AttachMenu(int iD, Menu child, int orderNumber = 0) => BaseAttach(Repo, iD, child, AttachOrDetach.Attach, orderNumber);

        public ActionResult DetachMenu(int iD, Menu child, int orderNumber = 0) => BaseAttach(Repo, iD, child, AttachOrDetach.Detach, orderNumber); 

        public void DetachAllMenus(int ID) => BaseDetachAllMenuChildren(Repo, ID, null);

        public void DetachAllMenus(int ID, List<Menu> selected) => BaseDetachAllMenuChildren(Repo, ID, selected);
        public ActionResult AttachPlan(int iD, Plan plan) => BaseAttach(Repo, iD, plan);
     
  
        public ActionResult AttachPlan(int iD, Plan child, int orderNumber = 0) => BaseAttach(Repo, iD, child, AttachOrDetach.Attach,orderNumber);
       
       
      
       
    

      
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

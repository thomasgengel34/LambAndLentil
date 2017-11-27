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
        ViewResult IGenericController<T>.Index(int? page) =>  
             BaseIndex(Repo, page); 

        // GET: ShoppingList/Details/5

        ActionResult IGenericController<T>.Details(int id , UIViewType actionMethod ) =>    BaseDetails(Repo, UIControllerType.ShoppingLists, id, actionMethod);

        // GET: ShoppingList/Create 
        ViewResult IGenericController<T>.Create(UIViewType actionMethod) =>  BaseCreate(actionMethod);

        // GET: ShoppingList/Edit/5
        ActionResult IGenericController<T>.Edit(int id) => BaseDetails(Repo, UIControllerType.ShoppingLists, id, UIViewType.Edit); 

        //POST: ShoppingList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        ActionResult IGenericController<T>.PostEdit([Bind(Include = "ID,Name,Description, Author, CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser")] T t) =>  BasePostEdit(Repo, t);

        // GET: ShoppingList/Delete/5
        [ActionName("Delete")]
        ActionResult IGenericController<T>.Delete(int id , UIViewType actionMethod )   => BaseDelete(Repo, UIControllerType.ShoppingLists, id);

        // POST: ShoppingList/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        ActionResult IGenericController<T>.DeleteConfirmed(int id)   => BaseDeleteConfirmed(Repo, UIControllerType.ShoppingLists, id);

        ActionResult IAttachDetachController.AttachIngredient(int? iD, Ingredient child, int orderNumber) => BaseAttach(Repo, iD, child, AttachOrDetach.Attach, orderNumber);

        ActionResult IAttachDetachController.DetachIngredient(int? iD, Ingredient child, int orderNumber) =>  BaseAttach(Repo, iD, child, AttachOrDetach.Detach, orderNumber);

        void IGenericController<T>.AddIngredientToIngredientsList(int id, string addedIngredient)  => BaseAddIngredientToIngredientsList(Repo, UIControllerType.Recipes, id, addedIngredient);

        ActionResult IAttachDetachController.DetachAllIngredients(int ID, List<Ingredient> selected) => BaseDetachAllIngredientChildren(Repo, ID, selected);

         

        ActionResult IAttachDetachController.AttachRecipe(int? recipeID, Recipe child, int orderNumber) =>  
         BaseAttach(Repo, recipeID, child);

        ActionResult IAttachDetachController.DetachRecipe(int? recipeID, Recipe child, int orderNumber) =>  BaseAttach(Repo, recipeID, child, AttachOrDetach.Detach, orderNumber);

        ActionResult IAttachDetachController.AttachMenu(int iD, Menu child, int orderNumber) =>     BaseAttach(Repo, iD, child, AttachOrDetach.Attach, orderNumber);

        ActionResult IAttachDetachController.DetachMenu(int iD, Menu child, int orderNumber) =>  BaseAttach(Repo, iD, child, AttachOrDetach.Detach, orderNumber);

        ActionResult IAttachDetachController.DetachAllRecipes(int ID, List<Recipe> selected) => BaseDetachAllRecipeChildren(Repo, ID, selected);

        ActionResult IAttachDetachController.AttachPlan(int iD, Plan child, int orderNumber) =>  BaseAttach(Repo, iD, child, AttachOrDetach.Attach, orderNumber);

        ActionResult IAttachDetachController.DetachPlan(int iD, Menu child, int orderNumber) =>  BaseAttach(Repo, iD, child, AttachOrDetach.Detach, orderNumber);

        ActionResult IAttachDetachController.DetachAllMenus(int ID, List<Menu> selected) => BaseDetachAllMenuChildren(Repo, ID, selected); 

        public void DetachAllShoppingLists(int ID) => BaseDetachAllShoppingListChildren(Repo, ID, null);

        ActionResult IAttachDetachController.DetachAllShoppingLists(int ID, List<ShoppingList> selected) =>   BaseDetachAllShoppingListChildren(Repo, ID, selected);
          
        ActionResult IAttachDetachController.DetachASetOfIngredients(int ID, List<Ingredient> selected) => BaseDetachAllIngredientChildren(Repo, ID, selected);

       
        
       
        ActionResult IAttachDetachController.DetachASetORecipes(int ID, List<Recipe> selected) => throw new System.NotImplementedException();
      
       
       
        ActionResult IAttachDetachController.DetachASetOMenus(int ID, List<Menu> selected) => throw new System.NotImplementedException();
       
      
        ActionResult IAttachDetachController.DetachAllPlans(int ID, List<Plan> selected) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.DetachASetOPlans(int ID, List<Plan> selected) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.AttachShoppingList(int iD, ShoppingList child, int orderNumber) => throw new System.NotImplementedException();
        ActionResult IAttachDetachController.DetachShoppingList(int iD, Menu child, int orderNumber) => throw new System.NotImplementedException();
       
        ActionResult IAttachDetachController.DetachASetOShoppingLists(int ID, List<ShoppingList> selected) => throw new System.NotImplementedException();
       
    
    }
}

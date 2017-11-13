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
        public ShoppingListsGenericController(IRepository<ShoppingList> repository) : base(repository) => Repo = repository;



        // GET: ShoppingList
        public ViewResult Index(int page = 1)
        {
            ViewResult view = BaseIndex(Repo, page);
            return View(view.ViewName, view.Model);
        }

        // GET: ShoppingList/Details/5
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            return BaseDetails(Repo, UIControllerType.ShoppingLists, id, actionMethod);
        }

        // GET: ShoppingList/Create 
        public ViewResult Create(UIViewType actionMethod) => BaseCreate(actionMethod);



        // GET: ShoppingList/Edit/5
        public ActionResult Edit(int id = 1) => BaseDetails(Repo, UIControllerType.ShoppingLists, id, UIViewType.Edit);



        //POST: ShoppingList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,Name,Description, Author, CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser")] T t)
        {
            return BasePostEdit(Repo, t);
        }

        // GET: ShoppingList/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete)
        {
            return BaseDelete(Repo, UIControllerType.ShoppingLists, id);
        }

        // POST: ShoppingList/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed(Repo, UIControllerType.ShoppingLists, id);
        }

        public ActionResult AttachIngredient(int? shoppingListID, Ingredient ingredient, int orderNumber = 0)
        {
            return BaseAttach(Repo, shoppingListID, ingredient, AttachOrDetach.Attach, orderNumber);
        }


        public ActionResult DetachIngredient(int? shoppingListID, Ingredient ingredient, int orderNumber = 0)
        {
            return BaseAttach(Repo, shoppingListID, ingredient, AttachOrDetach.Detach, orderNumber);
        }

        public void AddIngredientToIngredientsList(int id = 1, string addedIngredient = "")
        {
            BaseAddIngredientToIngredientsList(Repo, UIControllerType.Recipes, id, addedIngredient);
        }

        public void DetachAllIngredients(int ID) => BaseDetachAllIngredientChildren(Repo, ID);

        public void DetachAllIngredients(int ID, List<Ingredient> selected) => BaseDetachAllIngredientChildren(Repo, ID, selected);

        
        public ActionResult AttachRecipe(int? recipeID, Recipe recipe, int orderNumber = 0) =>
         BaseAttach(Repo, recipeID, recipe);

        public ActionResult DetachRecipe(int? recipeID, Recipe child, int orderNumber = 0) => BaseAttach(Repo, recipeID, child, AttachOrDetach.Detach, orderNumber);
    }
}

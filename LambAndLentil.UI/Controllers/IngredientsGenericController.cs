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

        public IngredientsGenericController(IRepository<Ingredient> repository) : base(repository) => Repo = repository;

        // GET: Ingredients  
        public ViewResult Index(int page = 1) => BaseIndex(Repo, page);







        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details) => BaseDetails(Repo, UIControllerType.Ingredients, id, actionMethod);

        // GET: Ingredients/Create 
        public ViewResult Create(UIViewType actionMethod) => BaseCreate(actionMethod);

        // GET: Ingredients/Edit/5
        [HttpGet]
        public ActionResult Edit(int id = 1)
        {
            return BaseDetails(Repo, UIControllerType.Ingredients, id, UIViewType.Edit);
        }


        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID, Name, Description, CreationDate,  IngredientsList")]  T t)
        {
            return BasePostEdit(Repo, t);
        }

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
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed(Repo, UIControllerType.Ingredients, id);
        }





        public ActionResult AttachIngredient(int? ingredientID, Ingredient ingredient, int orderNumber=0) => BaseAttach<Ingredient>(Repo, ingredientID, ingredient, 0);

        public ActionResult DetachIngredient(int? ingredientID, Ingredient ingredient, int orderNumber=0)
        {
            return BaseAttach<Ingredient>(Repo,  ingredientID, ingredient, AttachOrDetach.Detach);
        }

      

        public void AddIngredientToIngredientsList(int id = 1, string addedIngredient = "")=>
            BaseAddIngredientToIngredientsList(Repo, UIControllerType.Recipes, id, addedIngredient); 


        public void DetachAllIngredients(int ID) => BaseDetachAllIngredientChildren(Repo, ID);

        public void DetachAllIngredients(int ID, List<Ingredient> selected) => BaseDetachAllIngredientChildren(Repo, ID,selected);

        public void DetachLastIngredientChild(int ID) => BaseDetachLastIngredientChild(Repo, ID);
        public ActionResult AttachRecipe(int iD, Recipe recipe) => throw new NotImplementedException();
        public ActionResult AttachRecipe(int? recipeID, Recipe recipe, int orderNumber = 0) => throw new NotImplementedException();
        public ActionResult DetachRecipe(int? recipeID, Recipe child, int orderNumber = 0) => throw new NotImplementedException();
    }
}

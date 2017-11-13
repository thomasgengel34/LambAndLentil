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
        public RecipesGenericController(IRepository<Recipe> repository) : base(repository) => Repo = repository;

        // GET: Recipes
        public ViewResult Index(int page = 1)
        {
            return BaseIndex(Repo, page);
        }


        public void AddIngredientToIngredientsList(int id=1, string addedIngredient="")
        {
             BaseAddIngredientToIngredientsList(Repo, UIControllerType.Recipes, id, addedIngredient);
        }

       



        // GET: Recipes/Details/5
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            return BaseDetails(Repo,  UIControllerType.Recipes, id, actionMethod);

        }

        // GET: Ingredients/Create 
        public ViewResult Create(UIViewType actionMethod)
        {
            return BaseCreate(actionMethod);
        }


        // GET: Recipes/Edit/5
        public ActionResult Edit(int id = 1)
        {
            return BaseDetails(Repo, UIControllerType.Recipes, id, UIViewType.Edit);
        }



        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,Name,Description,Servings,MealType,Calories,CalsFromFat,CreationDate, ModifiedDate,AddedByUser, ModifiedByUser ")] T t)
        {
            return BasePostEdit(Repo, t);
        }

        // GET: Recipes/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete)
        {
            return BaseDelete(Repo, UIControllerType.Recipes, id);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed(Repo, UIControllerType.Recipes, id);
        }

        public ActionResult AttachIngredient(int? recipeID, Ingredient ingredient, int orderNumber = 0)
        {
            return BaseAttach<Ingredient>(Repo,recipeID, ingredient );
        }

        public ActionResult DetachIngredient(int? recipeID, Ingredient ingredient, int orderNumber = 0)
        {
            return BaseAttach<Ingredient>(Repo, recipeID, ingredient, AttachOrDetach.Detach);
        }

        public void DetachAllIngredients(int ID) => BaseDetachAllIngredientChildren(Repo, ID);

        public void DetachAllIngredients(int ID, List<Ingredient> selected) => BaseDetachAllIngredientChildren(Repo, ID, selected);
        public ActionResult AttachRecipe(int iD, Recipe recipe) => throw new NotImplementedException();
        public ActionResult AttachRecipe(int? recipeID, Recipe recipe, int orderNumber = 0) => throw new NotImplementedException();
        public ActionResult DetachRecipe(int? recipeID, Recipe child, int orderNumber = 0) => throw new NotImplementedException();
    }
}

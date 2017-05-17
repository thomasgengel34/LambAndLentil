using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using System.Collections.Generic;

namespace LambAndLentil.UI.Controllers
{
    public class RecipesController : BaseController
    { 
        public RecipesController(IRepository repo) : base(repo)
        {
            List<Recipe> recipes = repository.Recipes.ToList<Recipe>();
            Recipes = new Recipes(recipes);
            repository = repo;
        }

       

        // GET: Recipes
        public   ViewResult Index(int page = 1)
        { 
             ViewResult view = BaseIndex(UIControllerType.Recipes,page);
            return View(view.ViewName, view.Model);
        }

      

        // GET: Recipes/Details/5
        public   ActionResult  Details(int id = 1)
        {
           // return BaseDetails(id);
            return BaseDetails<Recipe, RecipesController, RecipeVM>(UIControllerType.Recipes, id);
        }

         // GET: Ingredients/Create 
        public ViewResult Create(UIViewType actionMethod)
        {
            return BaseCreate< RecipeVM>(actionMethod);
        }


        // GET: Recipes/Edit/5
        public ViewResult Edit(int id = 1)
        {
            return BaseEdit<Recipe,RecipesController, RecipeVM>(UIControllerType.Recipes, id);
        }



        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Servings,MealType,Calories,CalsFromFat")] RecipeVM recipeVM)
        { 
        return BasePostEdit<Recipe,RecipesController, RecipeVM>(recipeVM);
    }

        // GET: Recipes/Delete/5
        public ActionResult Delete(int id = 1)
        {
            return BaseDelete<Recipe,RecipesController, RecipeVM>(UIControllerType.Recipes, id);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] 
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed<Recipe,RecipesController>(UIControllerType.Recipes,id);
        }
    }
}

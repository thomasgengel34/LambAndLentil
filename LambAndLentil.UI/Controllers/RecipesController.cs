using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using LambAndLentil.UI.Infrastructure.Alerts;

namespace LambAndLentil.UI.Controllers
{
    public class RecipesController : BaseController
    {
         
       
        public RecipesController(IRepository<Recipe,RecipeVM> repo)  
        {
            
        }


        // GET: Recipes
        public ViewResult Index(int page = 1)
        {
            ViewResult view = BaseIndex<Recipe,RecipeVM>(page);
            return View(view.ViewName, view.Model);
        }

        // is this needed??
        [ChildActionOnly]
        public ViewResult RecipeIndexViewModel(string returnUrl)
        {
            Recipe recipe = GetRecipe();
            return View("Foo", new RecipeIndexViewModel()
            {
                Recipe = recipe,
                ID = recipe.ID,
                ReturnUrl = returnUrl
            });
        }



        // GET: Recipes/Details/5
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        { 
            return BaseDetails<Recipe,RecipeVM>(UIControllerType.Recipes, id, actionMethod);

        }

        // GET: Ingredients/Create 
        public ViewResult Create(UIViewType actionMethod)
        { 
            return BaseCreate<RecipeVM>(actionMethod);
        }


        // GET: Recipes/Edit/5
        public ViewResult Edit(int id = 1)
        { 
            return BaseEdit<Recipe,RecipeVM>(UIControllerType.Recipes, id);
        }



        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,Name,Description,Servings,MealType,Calories,CalsFromFat,CreationDate, ModifiedDate,AddedByUser, ModifiedByUser ")] RecipeVM recipeVM)
        {             
            return BasePostEdit<Recipe,RecipeVM>(recipeVM);
        }

        // GET: Recipes/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete)
        { 
            return BaseDelete<Recipe,RecipeVM>(UIControllerType.Recipes, id);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        { 
            return BaseDeleteConfirmed<Recipe,RecipeVM>(UIControllerType.Recipes, id);
        }

        public ActionResult AttachIngredient(int? recipeID, int? ingredientID)
        {
            return BaseAttach<Recipe, RecipeVM, Ingredient,IngredientVM >(recipeID, ingredientID);
        }

        public ActionResult RemoveIngredient(int? recipeID, int? ingredientID)
        {
            return BaseAttach<Recipe, RecipeVM, Ingredient, IngredientVM>(recipeID, ingredientID, AttachOrDetach.Detach);
        }
         

        internal Recipe GetRecipe()
        {
            Recipe recipe = (Recipe)Session["Recipe"];
            if (recipe == null)
            {
                recipe = new Recipe();
                Session["Recipe"] = recipe;
            }
            return recipe;
        } 
    }
}

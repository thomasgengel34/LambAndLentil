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
        public RecipesController(IRepository repo) : base(repo)
        {
            List<Recipe> recipes = repository.Recipes.ToList<Recipe>();
            Recipes = new Recipes(recipes);
            repository = repo;
        }



        // GET: Recipes
        public ViewResult Index(int page = 1)
        {
            ViewResult view = BaseIndex(UIControllerType.Recipes, page);
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
            ViewBag.listOfIngredients = GetListOfIngredients();
            return BaseDetails<Recipe, RecipesController, RecipeVM>(UIControllerType.Recipes, id, actionMethod);

        }

        // GET: Ingredients/Create 
        public ViewResult Create(UIViewType actionMethod)
        {
            ViewBag.listOfIngredients = GetListOfIngredients();
            return BaseCreate<RecipeVM>(actionMethod);
        }


        // GET: Recipes/Edit/5
        public ViewResult Edit(int id = 1)
        {
            ViewBag.listOfIngredients = GetListOfIngredients();
            return BaseEdit<Recipe, RecipesController, RecipeVM>(UIControllerType.Recipes, id);
        }



        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,Name,Description,Servings,MealType,Calories,CalsFromFat,CreationDate, ModifiedDate,AddedByUser, ModifiedByUser ")] RecipeVM recipeVM)
        {
            ViewBag.listOfIngredients = GetListOfIngredients();
            return BasePostEdit<Recipe, RecipesController, RecipeVM>(recipeVM);
        }

        // GET: Recipes/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete)
        {
            ViewBag.listOfIngredients = GetListOfIngredients();
            return BaseDelete<Recipe, RecipesController, RecipeVM>(UIControllerType.Recipes, id);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.listOfIngredients = GetListOfIngredients();
            return BaseDeleteConfirmed<Recipe, RecipesController>(UIControllerType.Recipes, id);
        }

        public ActionResult AttachIngredient(int? recipeID, int? ingredientID)
        {
            return BaseAttach<Recipe,Ingredient>(recipeID, ingredientID);
        }

        public ActionResult RemoveIngredient(int? recipeID, int? ingredientID)
        {   // conditions guard against people trying thngs out manually  
            ViewBag.listOfIngredients = GetListOfIngredients();
            if (recipeID == null)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning("Recipe was not found");
            }
            else
            {
                Recipe recipe = repository.Recipes.Where(m => m.ID == recipeID).SingleOrDefault();
                if (recipe == null)
                {
                    // todo: log error - this could be a developer problem
                    return RedirectToAction(UIViewType.Index.ToString()).WithWarning("Recipe was not found");
                }
                if (ingredientID == null)
                {     
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = recipeID, actionMethod = UIViewType.Details }).WithWarning("Ingredient was not found"); ;
                }
                else
                {
                    Ingredient ingredient = repository.Ingredients.Where(m => m.ID == ingredientID).SingleOrDefault();
                    if (ingredient == null)
                    {
                        return RedirectToAction(UIViewType.Edit.ToString(), new { id = recipeID, actionMethod = UIViewType.Details }).WithWarning("Please choose an ingredient");
                    }
                    else
                    { 
                        recipe.Ingredients.Remove(ingredient);
                        repository.Save<Recipe>(recipe);
                        return RedirectToAction(UIViewType.Details.ToString(), new { id = recipeID, actionMethod = UIViewType.Edit }).WithSuccess("Ingredient was Successfully Removed");
                    }
                }
            }
        }  

        private Recipe GetRecipe()
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

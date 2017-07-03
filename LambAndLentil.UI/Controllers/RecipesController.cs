using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;

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
            return BaseCreate<RecipeVM>(actionMethod);
        }


        // GET: Recipes/Edit/5
        public ViewResult Edit(int id = 1)
        {
            return BaseEdit<Recipe, RecipesController, RecipeVM>(UIControllerType.Recipes, id);
        }



        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,Name,Description,Servings,MealType,Calories,CalsFromFat,CreationDate, ModifiedDate,AddedByUser, ModifiedByUser ")] RecipeVM recipeVM)
        {
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

        //public RedirectToRouteResult AddIngredient(int iD, string returnUrl)
        //{
        //    Ingredient ingredient = repository.Ingredients.FirstOrDefault(p => p.ID == iD);

        //    if (ingredient != null)
        //    {
        //        GetRecipe().AddItem(ingredient, 1);
        //    }
        //    return RedirectToAction(UIViewType.Index.ToString(), new { returnUrl });
        //}


        public RedirectToRouteResult AddIngredient(int recipeID, int ingredientID  )
        {
            Recipe recipe = repository.Recipes.Where(m => m.ID == recipeID).Single(); 
            Ingredient ingredient = repository.Ingredients.Where(m => m.ID == ingredientID).Single();
            recipe.Ingredients.Add(ingredient);
            repository.Save<Recipe>(recipe);
            ViewBag.listOfIngredients = GetListOfIngredients();
            return RedirectToAction(UIViewType.Details.ToString(), new { id = recipeID, actionMethod = UIViewType.Details });
        }

        public RedirectToRouteResult RemoveIngredient(int recipeID, int ingredientID)
        {
            ViewBag.listOfIngredients = GetListOfIngredients();
            Recipe recipe = repository.Recipes.Where(m => m.ID == recipeID).Single();
            Ingredient ingredient = repository.Ingredients.Where(i => i.ID == ingredientID).Single();
            recipe.Ingredients.Remove(ingredient);
            repository.Save<Recipe>(recipe);
            return RedirectToAction(UIViewType.Details.ToString(), new { id = recipeID, actionMethod = UIViewType.Details });
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

        private SelectList GetListOfIngredients()
        {
            var result = from m in repository.Ingredients
                         orderby m.Name
                         select new SelectListItem
                         {
                             Text =m.Name,
                             Value =  m.ID.ToString()
                         };
            SelectList list = null;
            if (result.Count() == 0)
            {
                List<string> item = new List<string>();
                item.Add("Nothing was found");
                list = new SelectList(item);
            }
            else
            {
                list = new SelectList(result,  "Value","Text", result.First());
            }
            return list;
        }
    }
}

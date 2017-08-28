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
    public class RecipesController : RecipesGenericController<RecipeVM>
    {
        public RecipesController(IRepository<RecipeVM> repository) : base(repository)
        {
            repo = repository;
        } 
    }

    public class RecipesGenericController<T > : BaseController<RecipeVM>
          where T: RecipeVM  
    {
        public RecipesGenericController(IRepository<RecipeVM> repository) : base(repository)
        {
            repo = repository;
        }

        // GET: Recipes
        public ViewResult Index(int page = 1)
        {
            return BaseIndex(repo, page);
        }
         


        // GET: Recipes/Details/5
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            return BaseDetails(repo, UIControllerType.Recipes, id, actionMethod);

        }

        // GET: Ingredients/Create 
        public ViewResult Create(UIViewType actionMethod)
        {
            return BaseCreate(actionMethod);
        }


        // GET: Recipes/Edit/5
        public ViewResult Edit(int id = 1)
        {
            return BaseEdit(repo,UIControllerType.Recipes, id);
        }



        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,Name,Description,Servings,MealType,Calories,CalsFromFat,CreationDate, ModifiedDate,AddedByUser, ModifiedByUser ")] RecipeVM recipeVM)
        {
            return BasePostEdit(repo,recipeVM);
        }

        // GET: Recipes/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete)
        {
            return BaseDelete(repo,UIControllerType.Recipes, id);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed(repo,UIControllerType.Recipes, id);
        }

        public ActionResult AttachIngredient(int? recipeID, int? ingredientID)
        {
            return BaseAttach<RecipeVM,  IngredientVM>(recipeID, ingredientID);
        }

        public ActionResult RemoveIngredient(int? recipeID, int? ingredientID)
        {
            return BaseAttach<RecipeVM,   IngredientVM>(recipeID, ingredientID, AttachOrDetach.Detach);
        }

         
    }
}

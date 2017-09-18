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
    public class RecipesController : RecipesGenericController<Recipe>
    {
        public RecipesController(IRepository<Recipe> repository) : base(repository)
        {
            Repo = repository;
        } 
    }

    public class RecipesGenericController<T > : BaseController<Recipe>
          where T: Recipe  
    {
        public RecipesGenericController(IRepository<Recipe> repository) : base(repository)
        {
            Repo = repository;
        }

        // GET: Recipes
        public ViewResult Index(int page = 1)
        {
            return BaseIndex(Repo, page);
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
        public ActionResult PostEdit([Bind(Include = "ID,Name,Description,Servings,MealType,Calories,CalsFromFat,CreationDate, ModifiedDate,AddedByUser, ModifiedByUser ")] Recipe recipeVM)
        {
            return BasePostEdit(Repo, recipeVM);
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

        public ActionResult AttachIngredient(int? recipeID, Ingredient ingredient )
        {
            return BaseAttach<Ingredient>(Repo,recipeID, ingredient );
        }

        public ActionResult RemoveIngredient(int? recipeID, Ingredient ingredient)
        {
            return BaseAttach<Ingredient>(Repo, recipeID, ingredient, AttachOrDetach.Detach);
        }

         
    }
}

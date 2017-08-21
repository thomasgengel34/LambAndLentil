using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.UI.Controllers
{
    public class IngredientsController : IngredientsGenericController<Ingredient, IngredientVM> {
        public IngredientsController(IRepository<Ingredient, IngredientVM> repository) : base(repository)
        {
            repo=repository   ;
        }

        public static IRepository<Ingredient, IngredientVM> repo  { get; private set; }
    }

    public class IngredientsGenericController<T,TVM> : BaseController<Ingredient,IngredientVM>
          where T:Ingredient  
            where TVM:IngredientVM  
    {
        
        public IngredientsGenericController(IRepository<Ingredient, IngredientVM> repository) : base(repository)
        {
            repo = repository;
        }

        // GET: Ingredients  
        public ViewResult Index(int page = 1)
        {
            return BaseIndex(repo, page); 
        }
    
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            return BaseDetails(repo,UIControllerType.Ingredients, id,actionMethod);
        } 

        // GET: Ingredients/Create 
        public ViewResult Create(UIViewType actionMethod)
        {
            return BaseCreate(actionMethod);
        } 

        // GET: Ingredients/Edit/5
        [HttpGet]
        public ViewResult Edit(int id = 1)
        {
            return BaseEdit( repo, UIControllerType.Ingredients, id);
        }


        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID, Name, Description, CreationDate, ModifiedDate,  IngredientsList")]  IngredientVM ingredientVM)
        {
            return BasePostEdit(repo, ingredientVM);
        }

        // GET: Ingredients/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete)
        {
            ViewBag.ActionMethod = UIViewType.Delete;
            return BaseDelete(repo, UIControllerType.Ingredients, id);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed(repo,UIControllerType.Ingredients, id);
        }

    }
}

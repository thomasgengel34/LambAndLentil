using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.UI.Controllers
{
    public class IngredientsController : BaseController
    {
         

        public IngredientsController(IRepository<Ingredient,IngredientVM> repo) 
        {
             
        }


        // GET: Ingredients 
        //[OutputCache(CacheProfile = "IngredientIndexAsc")]
        public ViewResult Index(int page = 1)
        {
            return BaseIndex<Ingredient,IngredientVM>(page); 
        }
    
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            return BaseDetails<Ingredient,IngredientVM>(UIControllerType.Ingredients, id,actionMethod);
        }

        

        // GET: Ingredients/Create

        public ViewResult Create(UIViewType actionMethod)
        {
            return BaseCreate<IngredientVM>(actionMethod);
        }


        // GET: Ingredients/Edit/5
        [HttpGet]
        public ViewResult Edit(int id = 1)
        {
            return BaseEdit<Ingredient,IngredientVM>(UIControllerType.Ingredients, id);
        }


        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID, Name, Description, CreationDate, ModifiedDate,  IngredientsList")]  IngredientVM ingredientVM)
        {
            return BasePostEdit<Ingredient,IngredientVM>(ingredientVM);
        }

        // GET: Ingredients/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete)
        {
            ViewBag.ActionMethod = UIViewType.Delete;
            return BaseDelete<Ingredient,IngredientVM>(UIControllerType.Ingredients, id);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed<Ingredient,IngredientVM>(UIControllerType.Ingredients, id);
        }

    }
}

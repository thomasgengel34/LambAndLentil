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
        public IngredientsController(IRepository repo) : base(repo)
        {
            List<Ingredient> ingredients = repository.Ingredients.ToList<Ingredient>();
            Ingredients = new Ingredients(ingredients); 
        }


        // GET: Ingredients 
        //[OutputCache(CacheProfile = "IngredientIndexAsc")]
        public ViewResult Index(int page = 1)
        {
            ViewResult view = BaseIndex(UIControllerType.Ingredients, page);
            return View(view.ViewName, view.Model);
        }
        //try
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            return BaseDetails<Ingredient, IngredientsController, IngredientVM>(UIControllerType.Ingredients, id,actionMethod);
        }

        // GET: Ingredients/Details/5
        public ActionResult Detailsxxxxx(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            ViewBag.Title =actionMethod.ToString();
            if (actionMethod == UIViewType.Delete)
            { 
                return BaseDelete<Ingredient, IngredientsController, IngredientVM>(UIControllerType.Ingredients, id);
            }
            else if (actionMethod== UIViewType.DeleteConfirmed)
            {
                return BaseDeleteConfirmed<Ingredient, IngredientsController>(UIControllerType.Ingredients, id);
            }
            return BaseDetails<Ingredient, IngredientsController, IngredientVM>(UIControllerType.Ingredients, id);
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
            return BaseEdit<Ingredient, IngredientsController, IngredientVM>(UIControllerType.Ingredients, id);
        }


        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID, Name,  CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser,  Maker, Brand,   Description, ServingSize,  ServingSizeUnit,  ServingsPerContainer,  ContainerSize,  ContainerSizeUnit,   ContainerSizeInGrams,  Calories, CalFromFat,   TotalFat,  SaturatedFat, TransFat, PolyUnSaturatedFat,  MonoUnSaturatedFat, Cholesterol, Sodium, TotalCarbohydrates, Protein, Potassium, DietaryFiber, Sugars, VitaminA, VitaminC, Calcium, Iron, FolicAcid, Egg, Nuts , Milk , Wheat, Soy,  Category, Corn, Onion, Garlic , SodiumNitrite , UPC , Caffeine,  FoodGroup, StorageType, IngredientsList,  IsGMO, CountryOfOrigin,  Kosher, DataSource, Fish")]  IngredientVM ingredientVM)
        {
            return BasePostEdit<Ingredient, IngredientsController, IngredientVM>(ingredientVM);
        }

        // GET: Ingredients/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id = 1, UIViewType actionMethod = UIViewType.Delete)
        {
            ViewBag.ActionMethod = UIViewType.Delete;
            return BaseDelete<Ingredient, IngredientsController, IngredientVM>(UIControllerType.Ingredients, id);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed<Ingredient, IngredientsController>(UIControllerType.Ingredients, id);
        }

    }
}

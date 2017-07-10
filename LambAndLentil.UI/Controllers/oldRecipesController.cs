//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using LambAndLentil.Domain.Abstract;
//using LambAndLentil.Domain.Entities;
//using LambAndLentil.Domain.Concrete;

//namespace LambAndLentil.UI.Controllers
//{
//    public class xxRecipesController : Controller
//    {
//        public ILambAndLentilContext db;

//        public SelectList ingredientNames { get; set; }
// //needs to be revised with repository
//        public xxRecipesController()
//        {  
            
//        }
         
         

//        private SelectList BuildIngredientsFromName()
//        {
//            Dictionary<string, Ingredient> list = new Dictionary<string, Ingredient>();
//            foreach (Ingredient item in db.Ingredients)
//            {
//                list.Add(item.Name, item);
//            }

//            SelectList ingredientsFromShort = new SelectList(list);
//            return ingredientsFromShort;
//        }

//        // GET: Recipes
//        public ActionResult Index()
//        {
//            return View(db.Recipes.ToList());
//        }

//        // GET: Recipes/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Recipe recipe = db.Recipes.Find(id);
//            if (recipe == null)
//            {
//                return HttpNotFound();
//            }
//            return View(recipe);
//        }

//        // GET: Recipes/Create
//        public ActionResult Create()
//        {

//            ViewBag.ingredientNames = ingredientNames;
//            return View();
//        }

//        // POST: Recipes/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "ID,Name,Description,Servings,MealType")] Recipe recipe)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Recipes.Add(recipe);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(recipe);
//        }

//        // GET: Recipes/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Recipe recipe = db.Recipes.Find(id);
//            if (recipe == null)
//            {
//                return HttpNotFound();
//            }

//            ViewBag.ingredientNames = ingredientNames;
//            SelectList ingreds = BuildIngredientsFromName();
//            ViewBag.ingreds = ingreds;


//            recipe.Calories = Recipe.GetCalories(recipe);

//            return View(recipe);
//        }

//        // POST: Recipes/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "ID,Name,Description,Servings,MealType,Calories")] Recipe recipe)
//        {
//            Recipe existingRecipe = db.Recipes.Find(recipe.ID);
//            existingRecipe.Name = recipe.Name;
//            existingRecipe.Description = recipe.Description;
//            existingRecipe.Servings = recipe.Servings;
//            existingRecipe.MealType = recipe.MealType;
//            existingRecipe.Calories = recipe.Calories;
            
//            if (ModelState.IsValid)
//            {
//                db.Entry(existingRecipe).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            recipe.Calories = Recipe.GetCalories(recipe);
//            return View(recipe);
//        }

//        // GET: Recipes/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Recipe recipe = db.Recipes.Find(id);
//            if (recipe == null)
//            {
//                return HttpNotFound();
//            }
//            return View(recipe);
//        }

//        // POST: Recipes/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Recipe recipe = db.Recipes.Find(id);
//            db.Entry(recipe).State = EntityState.Deleted;
//            //if (recipe.RecipeIngredients.Count > 0)
//            //{
//            //    foreach (RecipeIngredient item in recipe.RecipeIngredients)
//            //    {
//            //        recipe.RecipeIngredients.Remove(item);
//            //    }
//            //}
//            db.Recipes.Remove(recipe);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        public ActionResult AttachIngredient(int? ID, string IngredientName, decimal? Quantity, Measurement? measurement)
//        {
//            if (ID == null || ID < 1)
//            {
//                return View("Index");
//            }

//            Recipe recipe = db.Recipes.Find(ID);



//            if (ID == null || IngredientName == null || IngredientName == "" || Quantity == null || Quantity == 0 || measurement == null)
//            {
//                ViewBag.ingreds = ingredientNames;
//                return View("Edit", recipe);
//            }


//            var result = from c in db.Ingredients
//                         where c.Name == IngredientName
//                         select c.ID;
//            int ingredientID = result.FirstOrDefault();
//            Ingredient ingredient = db.Ingredients.Find(ingredientID);

//            RecipeIngredient recipeIngredient = new RecipeIngredient();
//            recipeIngredient.Ingredient = ingredient;
//            recipeIngredient.Measurement = (Measurement)measurement;
//            recipeIngredient.Quantity = (decimal)Quantity;
//            // just do Calories for now.  Compute the number of calories in the Recipe
//            recipe.Calories = (int)(recipe.Calories == null ? 0 : recipe.Calories) + (int)RecipeIngredient.QuantityConverter((decimal)Quantity, ingredient, recipeIngredient) * ingredient.Calories;


//            recipe.RecipeIngredients.Add(recipeIngredient);

//            if (ModelState.IsValid)
//            {
//                db.Entry(recipe).State = EntityState.Modified;
//                db.SaveChanges();
//            }



//            ViewBag.ingredientNames = ingredientNames;
//            SelectList ingreds = BuildIngredientsFromName();
//            ViewBag.ingreds = ingreds;
//            recipe.Calories = Recipe.GetCalories(recipe);
//            return View("Edit", recipe);
//        }



//        public ActionResult RemoveIngredient(int? ID, int? RecipeIngredientID)
//        {
//            if (ID == null || ID < 1)
//            {
//                return View("Index");
//            }

//            Recipe recipe = db.Recipes.Find(ID);



//            if (ID == null || ID < 1)
//            {
//                ViewBag.ingredientNames = ingredientNames;
//                return View("Edit", recipe);
//            }


//            RecipeIngredient recipeIngredient = db.RecipeIngredients.Find((int)ID);

//            decimal Quantity = (decimal)recipeIngredient.Quantity;

//            var result = from d in db.RecipeIngredients
//                         where d.ID == ID
//                         select d.Ingredient;


//            Ingredient ingredient = result.FirstOrDefault();

//            // just do Calories for now.  Compute the adjusted number of calories in the Recipe

//            recipe.Calories = (int)(recipe.Calories == null ? 0 : recipe.Calories) - (int)RecipeIngredient.QuantityConverter(Quantity, ingredient, recipeIngredient) * ingredient.Calories;

//            recipe.RecipeIngredients.Remove(recipeIngredient);

//            if (ModelState.IsValid)
//            {
//                db.Entry(recipe).State = EntityState.Modified;
//                db.SaveChanges();
//            }


//            ViewBag.ingredientNames = ingredientNames;
//            SelectList ingreds = BuildIngredientsFromName();
//            ViewBag.ingreds = ingreds;
//            recipe.Calories = Recipe.GetCalories(recipe);
//            return View("Edit", recipe);
//        }

//        public ActionResult ReplaceIngredient(int? ID, int? oldID, string ingredientsd, decimal? Quantity, Measurement? measurement)
//        {
//            if (ID == null || ID < 1)
//            {
//                return View("Index");
//            }

//            Recipe recipe = db.Recipes.Find(ID);

//            // Bounce back if anything is null instead of throwing an exception
//            if (oldID == null || ingredientsd == null || ingredientsd == "" || Quantity == null || measurement == null)
//            {
//                ViewBag.ingredientNames = ingredientNames;
//                return View("Edit", recipe);
//            }



//            List<RecipeIngredient> recipeIngredientList = recipe.RecipeIngredients.ToList();



//            RecipeIngredient oldRecipeIngredient = recipeIngredientList.Find(a => a.ID == oldID);
//            int insertAt = recipeIngredientList.FindIndex(a => a.ID == oldID);
//            var result = from c in db.Ingredients.AsQueryable()
//                         where c.Name == ingredientsd
//                         select c.ID;
//            int newID = result.FirstOrDefault();
//            Ingredient newIngredient = db.Ingredients.Find(newID);
//            RecipeIngredient ri = new RecipeIngredient();
//            ri.Ingredient = newIngredient;
//            ri.Quantity = (decimal)Quantity;
//            ri.Measurement = (Measurement)measurement;

//            recipeIngredientList.Insert(insertAt, ri);
//            recipe.RecipeIngredients = recipeIngredientList;
//            recipe.RecipeIngredients.Remove(oldRecipeIngredient);
//            if (ModelState.IsValid)
//            {
//                db.Entry(recipe).State = EntityState.Modified;
//                db.SaveChanges();
//            }




//            ViewBag.ingredientNames = ingredientNames;
//            SelectList ingreds = BuildIngredientsFromName();
//            ViewBag.ingreds = ingreds;
//            recipe.Calories = Recipe.GetCalories(recipe);
//            return View("Edit", recipe);

//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}

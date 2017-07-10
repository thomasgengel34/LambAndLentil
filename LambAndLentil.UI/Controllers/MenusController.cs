using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.UI.Infrastructure.Alerts;


namespace LambAndLentil.UI.Controllers
{
    public class MenusController : BaseController
    { 
        public MenusController(IRepository repo) : base(repo)
        {  
            List<Menu> menus = repository.Menus.ToList<Menu>();
            Menus = new Menus(menus);
            repository = repo;  // is this needed???
        }

        // GET: Menus
        public ViewResult Index(int page = 1)
        { 
           ViewResult view = BaseIndex(UIControllerType.Menus, page);
           return View(view.ViewName, view.Model); 
        }

        // GET: Menus/Details/5 
        public ActionResult Details(int id = 1, UIViewType actionMethod = UIViewType.Details)
        {
            ViewBag.listOfRecipes =   GetListOfRecipes();
            ViewBag.listOfIngredients = GetListOfIngredients();
            return BaseDetails<Menu, MenusController, MenuVM>(UIControllerType.Menus, id, actionMethod);
        }



        // GET: Menus/Create 
        public ViewResult Create(UIViewType actionMethod)
        {
            ViewBag.listOfRecipes = GetListOfRecipes();
             ViewBag.listOfIngredients = GetListOfIngredients();
            return BaseCreate<MenuVM>(actionMethod);
        }


        // GET: Menus/Edit/5
        public ViewResult Edit(int id=1)
        {
            ViewBag.listOfRecipes = GetListOfRecipes();
             ViewBag.listOfIngredients = GetListOfIngredients();
            return BaseEdit<Menu, MenusController, MenuVM>(UIControllerType.Menus, id);  
        } 

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostEdit([Bind(Include = "ID,Name, DayOfWeek, Description,CreationDate, ModifiedDate,  AddedByUser, ModifiedByUser, MealType,Diners, Recipes, Ingredients")] MenuVM menuVM)
        {
            ViewBag.listOfRecipes = GetListOfRecipes();
             ViewBag.listOfIngredients = GetListOfIngredients();
            return BasePostEdit<Menu, MenusController, MenuVM>(menuVM);
    }

        // GET: Menus/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int  id=1, UIViewType actionMethod = UIViewType.Delete)
        {

            ViewBag.listOfRecipes = GetListOfRecipes();
            ViewBag.ActionMethod = UIViewType.Delete;
            return BaseDelete<Menu, MenusController, MenuVM>(UIControllerType.Menus, id);
        }

        

        // POST: Ingredients/Delete/5 
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed<Menu, MenusController>(UIControllerType.Menus, id);
        } 

       
          public ActionResult AddRecipe(int? menuID, int? recipeID)
        {  // conditions guard against people trying thngs out manually  
            ViewBag.listOfRecipes = GetListOfRecipes();
             ViewBag.listOfIngredients = GetListOfIngredients();
            if (menuID == null)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning("Menu was not found");
            }
            else
            {
                Menu menu = repository.Menus.Where(m => m.ID == menuID).Single();
                if (menu == null)
                {
                    // todo: log error - this could be a developer problem
                    return RedirectToAction(UIViewType.Index.ToString()).WithWarning("Menu was not found");
                }
                if (recipeID == null)
                {    // this should have a warning that the recipe is not in the db
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = menuID, actionMethod = UIViewType.Details }).WithWarning("Recipe was not found"); ;
                }
                else
                {
                    Recipe recipe = repository.Recipes.Where(m => m.ID == recipeID).Single();
                    if (recipe == null)
                    {
                        return RedirectToAction(UIViewType.Edit.ToString(), new { id = menuID, actionMethod = UIViewType.Details }).WithWarning("Please choose an recipe");
                    }
                    else
                    {
                        menu.Recipes.Add(recipe);
                        repository.Save<Menu>(menu);

                        return RedirectToAction(UIViewType.Details.ToString(), new { id = menuID, actionMethod = UIViewType.Details }).WithSuccess("Successfully added!");
                    }
                }
            }
        }

        public ActionResult RemoveRecipe(int? menuID, int? ingredientID)
        {   // conditions guard against people trying thngs out manually  
            ViewBag.listOfRecipes = GetListOfRecipes();
            ViewBag.listOfIngredients = GetListOfIngredients();
            if (menuID == null)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning("Menu was not found");
            }
            else
            {
                Menu menu = repository.Menus.Where(m => m.ID == menuID).Single();
                if (menu == null)
                {
                    // todo: log error - this could be a developer problem
                    return RedirectToAction(UIViewType.Index.ToString()).WithWarning("Menu was not found");
                }
                if (ingredientID == null)
                {    // this should have a warning that the ingredient is not in the db
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = menuID, actionMethod = UIViewType.Details }).WithWarning("Recipe was not found"); ;
                }
                else
                {
                    Recipe ingredient = repository.Recipes.Where(m => m.ID == ingredientID).Single();
                    if (ingredient == null)
                    {
                        return RedirectToAction(UIViewType.Edit.ToString(), new { id = menuID, actionMethod = UIViewType.Details }).WithWarning("Please choose an ingredient");
                    }
                    else
                    {
                        menu.Recipes.Remove(ingredient);
                        repository.Save<Menu>(menu);
                        return RedirectToAction(UIViewType.Details.ToString(), new { id = menuID, actionMethod = UIViewType.Details }).WithSuccess("Successfully Removed");
                    }
                }
            }
        }

        public ActionResult AttachIngredient(int? menuID, int? ingredientID)
        {  // conditions guard against people trying thngs out manually  
            ViewBag.listOfIngredients = GetListOfIngredients();
            ViewBag.listOfRecipes = GetListOfRecipes();
            if (menuID == null)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning("Menu was not found");
            }
            else
            {
                Menu menu = repository.Menus.Where(m => m.ID == menuID).Single();
                if (menu == null)
                {
                    // todo: log error - this could be a developer problem
                    return RedirectToAction(UIViewType.Index.ToString()).WithWarning("Menu was not found");
                }
                if (ingredientID == null)
                {    // this should have a warning that the ingredient is not in the db
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = menuID, actionMethod = UIViewType.Details }).WithWarning("Ingredient was not found"); ;
                }
                else
                {
                    Ingredient ingredient = repository.Ingredients.Where(m => m.ID == ingredientID).Single();
                    if (ingredient == null)
                    {
                        return RedirectToAction(UIViewType.Edit.ToString(), new { id = menuID, actionMethod = UIViewType.Details }).WithWarning("Please choose an ingredient");
                    }
                    else
                    {
                        menu.Ingredients.Add(ingredient);
                        repository.Save<Menu>(menu);

                        return RedirectToAction(UIViewType.Details.ToString(), new { id = menuID, actionMethod = UIViewType.Details }).WithSuccess("Successfully added!");
                    }
                }
            }
        }

        public ActionResult RemoveIngredient(int? menuID, int? ingredientID)
        {   // conditions guard against people trying thngs out manually  
            ViewBag.listOfIngredients = GetListOfIngredients();
            ViewBag.listOfRecipes = GetListOfRecipes();
            if (menuID == null)
            {
                return RedirectToAction(UIViewType.Index.ToString()).WithWarning("Menu was not found");
            }
            else
            {
                Menu menu = repository.Menus.Where(m => m.ID == menuID).Single();
                if (menu == null)
                {
                    // todo: log error - this could be a developer problem
                    return RedirectToAction(UIViewType.Index.ToString()).WithWarning("Menu was not found");
                }
                if (ingredientID == null)
                {    // this should have a warning that the ingredient is not in the db
                    return RedirectToAction(UIViewType.Details.ToString(), new { id = menuID, actionMethod = UIViewType.Details }).WithWarning("Ingredient was not found"); ;
                }
                else
                {
                    Ingredient ingredient = repository.Ingredients.Where(m => m.ID == ingredientID).Single();
                    if (ingredient == null)
                    {
                        return RedirectToAction(UIViewType.Edit.ToString(), new { id = menuID, actionMethod = UIViewType.Details }).WithWarning("Please choose an ingredient");
                    }
                    else
                    {
                        menu.Ingredients.Remove(ingredient);
                        repository.Save<Menu>(menu);
                        return RedirectToAction(UIViewType.Details.ToString(), new { id = menuID, actionMethod = UIViewType.Details }).WithSuccess("Successfully Removed");
                    }
                }
            }
        }

        private Menu GetMenu()
        {
            Menu recipe = (Menu)Session["Menu"];
            if (recipe == null)
            {
                recipe = new Menu();
                Session["Menu"] = recipe;
            }
            return recipe;
        }


    }
}

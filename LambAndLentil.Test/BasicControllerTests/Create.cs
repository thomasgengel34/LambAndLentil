using System;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.BasicTests;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicTests
{
    internal class Create<T> : BaseControllerTest<T>
       where T : BaseEntity, IEntity, new()
    {
        internal static void TestRunner()
        {
            CanCreate();
            CreateReturnsNonNull();
            ShouldCreate();
            ShouldCreateIngredient();
            ShouldCreateMenu();
            ShouldCreateRecipe(); 
            ShouldCreatePerson(); 
            ShouldCreatePlan(); 
            ShouldCreateShoppingList(); 
        }

        private static void ShouldCreatePlan()
        {
            IRepository<Plan> plansRepo = new TestRepository<Plan>();
            IGenericController<Plan> planController = new PlansController(plansRepo);

            ActionResult ar = planController.Create();
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult vr = (ViewResult)adr.InnerResult;
            Plan returnedItem = (Plan)vr.Model;

            Assert.AreEqual(0, returnedItem.ID);
            Assert.AreEqual("Plan", returnedItem.ClassName);
            Assert.AreEqual("Plan", returnedItem.DisplayName);
            Assert.IsNotNull(returnedItem.Ingredients);
            Assert.IsNotNull(returnedItem.Recipes);
            Assert.IsNotNull(returnedItem.Menus);
            Assert.IsNull(returnedItem.Plans);
            Assert.IsNull(returnedItem.ShoppingLists); 
        }
             

        private static void ShouldCreateIngredient()
        {
            IRepository<Ingredient> ingredientsRepo = new TestRepository<Ingredient>();
            IGenericController<Ingredient> ingredientController = new IngredientsController(ingredientsRepo);

            ActionResult ar = ingredientController.Create();
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult vr = (ViewResult)adr.InnerResult;
            Ingredient returnedItem = (Ingredient)vr.Model;

            Assert.AreEqual(0, returnedItem.ID);
            Assert.AreEqual("Ingredient", returnedItem.ClassName);
            Assert.AreEqual("Ingredient", returnedItem.DisplayName); 
            Assert.IsNotNull(returnedItem.Ingredients);
            Assert.IsNull(returnedItem.Recipes);
            Assert.IsNull(returnedItem.Menus);
            Assert.IsNull(returnedItem.Plans);
            Assert.IsNull(returnedItem.ShoppingLists);
        }

        private static void ShouldCreatePerson()
        {
            IRepository<Person> personsRepo = new TestRepository<Person>();
            IGenericController<Person> personController = new PersonsController(personsRepo);

            ActionResult ar = personController.Create();
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult vr = (ViewResult)adr.InnerResult;
            Person returnedItem = (Person)vr.Model;

            Assert.AreEqual(0, returnedItem.ID);
            Assert.AreEqual("Person", returnedItem.ClassName);
            Assert.AreEqual("Person", returnedItem.DisplayName);  
              Assert.AreEqual("Newly",returnedItem.FirstName);
             Assert.AreEqual("Created", returnedItem.LastName);
            Assert.AreEqual("Newly Created", returnedItem.FullName);
            Assert.AreEqual("Newly Created", returnedItem.Name);  

            Assert.IsNotNull(returnedItem.Ingredients);
            Assert.IsNotNull(returnedItem.Recipes);
            Assert.IsNotNull(returnedItem.Menus);
            Assert.IsNotNull(returnedItem.Plans);
            Assert.IsNotNull(returnedItem.ShoppingLists);
        }

        private static void ShouldCreateRecipe()
        {
            IRepository<Recipe> recipesRepo = new TestRepository<Recipe>();
            IGenericController<Recipe> recipeController = new RecipesController(recipesRepo);

            ActionResult ar = recipeController.Create();
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult vr = (ViewResult)adr.InnerResult;
            Recipe returnedItem = (Recipe)vr.Model;

            Assert.AreEqual(0, returnedItem.ID);
            Assert.AreEqual("Recipe", returnedItem.ClassName);
            Assert.AreEqual("Recipe", returnedItem.DisplayName);

            Assert.AreEqual(0, returnedItem.Servings); Assert.AreEqual(MealType.Breakfast, returnedItem.MealType);
            Assert.AreEqual(0, returnedItem.Calories);
            Assert.AreEqual(0, returnedItem.CalsFromFat);
           
            Assert.IsNotNull(returnedItem.Ingredients);
            Assert.IsNull(returnedItem.Recipes);
            Assert.IsNull(returnedItem.Menus);
            Assert.IsNull(returnedItem.Plans);
            Assert.IsNull(returnedItem.ShoppingLists);
        }

        private static void ShouldCreateShoppingList()
        {
            IRepository<ShoppingList> shoppingListsRepo = new TestRepository<ShoppingList>();
            IGenericController<ShoppingList> shoppingListController = new ShoppingListsController(shoppingListsRepo);

            ActionResult ar = shoppingListController.Create();
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult vr = (ViewResult)adr.InnerResult;
            ShoppingList returnedItem = (ShoppingList)vr.Model;

            Assert.AreEqual(0, returnedItem.ID);
            Assert.AreEqual("ShoppingList", returnedItem.ClassName);
            Assert.AreEqual("Shopping List", returnedItem.DisplayName);
            Assert.AreEqual("Anonymous", returnedItem.Author);
            Assert.AreEqual(DateTime.Now.ToShortTimeString(), returnedItem.CreationDate);


            Assert.IsNotNull(returnedItem.Ingredients);
            Assert.IsNotNull(returnedItem.Recipes);
            Assert.IsNotNull(returnedItem.Menus);
            Assert.IsNotNull(returnedItem.Plans);
            Assert.IsNull(returnedItem.ShoppingLists);
        }

        private static void CanCreate()
        {
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.Create();
            ViewResult view = (ViewResult)adr.InnerResult;

            Assert.AreEqual(UIViewType.Details.ToString(), view.ViewName);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("Here it is!", adr.Message);
        }


        private static void CreateReturnsNonNull()
        {
            ViewResult result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }


        private static void ShouldCreate()
        {
            SetUpForTests(out repo, out controller, out item);
            ActionResult ar = controller.Create();
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult vr = (ViewResult)adr.InnerResult;
            T t = (T)vr.Model;
            string modelName = t.Name;

            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created");
            // TODO:  Menu as child exclusive test
            //Assert.AreEqual(DayOfWeek.Sunday, Menu.DayOfWeek);
        }

        private static void ShouldCreateMenu()
        {
            IRepository<Menu> menusRepo = new TestRepository<Menu>();
            IGenericController<Menu> menuController = new MenusController(menusRepo);

            ActionResult ar = menuController.Create();
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult vr = (ViewResult)adr.InnerResult;
            Menu returnedItem = (Menu)vr.Model;

            Assert.AreEqual(0, returnedItem.ID);
            Assert.AreEqual(DayOfWeek.Sunday, returnedItem.DayOfWeek);
            Assert.AreEqual(MealType.Breakfast, returnedItem.MealType);
            Assert.AreEqual("Menu", returnedItem.ClassName);
            Assert.AreEqual("Menu", returnedItem.DisplayName);
            Assert.AreEqual(0, returnedItem.Diners);
            Assert.IsNotNull(returnedItem.Ingredients);
            Assert.IsNotNull(returnedItem.Recipes);

            Assert.IsNull(returnedItem.Menus);
            Assert.IsNull(returnedItem.Plans);
            Assert.IsNull(returnedItem.ShoppingLists);
        }


    }
}

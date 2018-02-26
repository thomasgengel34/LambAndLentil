using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.BaseControllerTests;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{

    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("MenusController")]
    internal class MenusControllerShould : MenusController_Test_Should
    {
        private static IGenericController<Menu> Controller1, Controller2, Controller3, Controller4, Controller5;


        public MenusControllerShould()
        {

            repo = new TestRepository<Menu>();
            Controller1 = new MenusController(repo);
            Controller2 = new MenusController(repo);
            Controller3 = new MenusController(repo);
            Controller4 = new MenusController(repo);
            Controller5 = new MenusController(repo);
            Menu = new Menu
            {
                Name = "0000 test",
                ID = 33
            };
            repo.Save((Menu)Menu);
            controller = new MenusController(repo);
        }






        [TestMethod]
        public void SaveAValidMenu()
        {
            Menu Menu = new Menu
            {
                Name = "test",
                ID = 2
            };
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit(Menu);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;

            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedMenuWithNameChange()
        {
            Menu menu = new Menu() { ID = 6001, Name = "test SaveEditedMenuWithNameChange" };
            repo.Save(menu);

            menu.Name = "0000 test Edited";
            ActionResult ar = controller.PostEdit(menu);
            Menu returnedMenu = repo.GetById(menu.ID);

            Assert.AreEqual("0000 test Edited", returnedMenu.Name);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedMenuWithNameAndDayOfWeekChange()
        {
            IRepository<Menu> repo = new TestRepository<Menu>();
            IGenericController<Menu> controller = new MenusController(repo);
            Menu menu = new Menu() { ID = 20020, DayOfWeek = DayOfWeek.Sunday };
            repo.Save(menu);
            menu.DayOfWeek = DayOfWeek.Friday;
            ActionResult ar2 = controller.PostEdit(menu);
            Menu returnedMenu = repo.GetById(menu.ID);
            Assert.AreEqual(DayOfWeek.Friday, returnedMenu.DayOfWeek);
        }
         

        [TestMethod]
        [TestCategory("Edit")]
        public void ShouldSaveTheCreationDateOnMenuCreationWithDateTimeParameter()
        {
            ClassCleanup();
            DateTime CreationDate = new DateTime(2010, 1, 1); 
            Menu menu = new Menu(CreationDate); 
            Assert.AreEqual(CreationDate, menu.CreationDate);
        }



        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnMenuCreationWithDateTimeParameter()
        {

            DateTime CreationDate = new DateTime(2010, 1, 1);


            Menu Menu = new Menu(CreationDate);


            Assert.AreEqual(CreationDate, Menu.CreationDate);
        }



        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexWithSuccessAttachAnExistingRecipeToAnExistingMenu()
        {
            IRepository<Recipe> repoRecipe = new TestRepository<Recipe>();
            IGenericController<Menu> ControllerAttach = new MenusController(repo);
            IGenericController<Recipe> ControllerAttachI = new RecipesController(repoRecipe);
            IGenericController<Menu> ControllerCleanup = new MenusController(repo);


            Menu menu = new Menu() { ID = 100, Description = "test AttachAnExistingRecipeToAnExistingMenu" };
            repo.Save(menu);

            Recipe recipeVM = new Recipe() { ID = 101, Description = "test AttachAnExistingRecipeToAnExistingMenu" };
            repoRecipe.Save(recipeVM);

            var x = ControllerAttach.Attach(menu, recipeVM);


            Assert.AreEqual(1, menu.Recipes.Count());

            Assert.AreEqual(recipeVM.ID, menu.Recipes.First().ID);
        }


        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void AttachAnExistingIngredientToAnExistingMenu()
        {
            IRepository<Menu> repository = new TestRepository<Menu>();
            Menu menu = new Menu { ID = 101, Description = "test AttachAnExistingIngredientToAnExistingMenu" };
            repository.Save(menu);

            Ingredient ingredient = new Ingredient { ID = 100, Description = "test AttachAnExistingIngredientToAnExistingMenu" };

            ActionResult ar = controller.Attach(menu, ingredient);
            Menu returnedMenu = repository.GetById(menu.ID);

            Assert.AreEqual(1, returnedMenu.Ingredients.Count());
            Assert.AreEqual(ingredient.ID, returnedMenu.Ingredients.First().ID);

        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void NotDeleteAnIngredientAfterIngredientIsDetachedFromMenu()
        {
            Ingredient ingredient = new Ingredient { ID = int.MaxValue - 100, Description = "test NotDeleteAnIngredientAfterIngredientIsDetachedFromMenu" };

            controller.Attach(Menu, ingredient);
            controller.Detach(Menu, ingredient);

            Assert.IsNotNull(ingredient);
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenDetachingNonExistingIngredientAttachedToANonExistingMenu()
        {
            Menu menu = new Menu() { ID = 700 };
            menu = null;
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.Detach(menu, (Ingredient)null);

            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Menu was not found", adr.Message);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        }




        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNonExistingMenu()
        {
            Menu menu = new Menu();
            menu = null;
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.Detach(menu, (Ingredient)null);

            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Menu was not found", adr.Message);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        }

    }
}

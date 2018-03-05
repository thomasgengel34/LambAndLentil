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

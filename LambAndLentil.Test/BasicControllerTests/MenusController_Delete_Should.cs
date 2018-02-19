////using System;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;

namespace  LambAndLentil.Test.BaseControllerTests
{
    [TestCategory("MenusController")]
    [TestCategory("Delete")]
    [TestClass]
    public class MenusController_Delete_Should:MenusController_Test_Should
    { 

          

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteConfirmed()
        { 
            ActionResult result = Controller.DeleteConfirmed(ListEntity.ListT.FirstOrDefault().ID) as ActionResult;
            // improve this test when I do some more route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { Controller = "Menus", Action = "Index" } } );
            // Assert 
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void CanDeleteValidMenu()
        { 
            Menu menu = new Menu { ID = 2, Name = "Test2", Description = "test MenusControllerTest.CanDeleteValidMenu" };
            Repo.Save(menu);
            int beginningCount = Repo.Count();
             
            ActionResult result = Controller.DeleteConfirmed(menu.ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)result;
             
            Assert.AreEqual(beginningCount - 1, Repo.Count());
            Assert.AreEqual("Test2 has been deleted", adr.Message);
        }
    }
}

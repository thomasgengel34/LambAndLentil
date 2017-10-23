////using System;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestCategory("MenusController")]
    [TestCategory("Delete")]
    [TestClass]
    public class MenusController_Delete_Should:MenusController_Test_Should
    {
        public MenusController_Delete_Should()
        {

        }
        [Ignore]
        [TestMethod]
        public void AllowUserToConfirmDeleteRequestAndCallConfirmDelete()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnIndexWithWarningWhenIDIsNotFound()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnIDetailsWhenIDIstFound()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }


        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteAFoundMenu()
        {
            // Arrange


            // Act 
            ActionResult ar = Controller.Details(ListEntity.ListT.FirstOrDefault().ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;


            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual(UIViewType.Details.ToString(), view.ViewName);
        }



        [TestMethod]
        [TestCategory("Delete")]
        public void MenusCtr_DeleteAnInvalidMenu()
        {
            // Arrange 

            // Act 
            var view = Controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Menu was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual(UIViewType.Index.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteConfirmed()
        {
            // Arrange

            // Act
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
            // Arrange 

            Menu menu = new Menu { ID = 2, Name = "Test2", Description = "test MenusControllerTest.CanDeleteValidMenu" };
            Repo.Add(menu);
            int beginningCount = Repo.Count();

            // Act - delete the menu
            ActionResult result = Controller.DeleteConfirmed(menu.ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)result;

            // Assert
            Assert.AreEqual(beginningCount - 1, Repo.Count());
            Assert.AreEqual("Test2 has been deleted", adr.Message);
        }
    }
}

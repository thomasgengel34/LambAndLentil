using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("ShoppingListsController")]

    public class ShoppingListsController_PostEdit_Should: ShoppingListsController_Test_Should
    {
        [TestMethod]
        public void ReturnIndexWithValidModelStateWithSuccessMessageWhenSaved()
        {
            // Arrange
            ShoppingList rvm = new ShoppingList
            {
                ID = -2
            };


            // Act
            ActionResult ar =Controller.PostEdit(rvm);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            // Assert
            Assert.AreEqual("Something is wrong with the data!",adr.Message);
            Assert.AreEqual("alert-warning",adr.AlertClass);
            Assert.AreEqual("Details", view.ViewName);


        }

        [TestMethod]
        public void ReturnIndexWithInValidModelStateWithWarningMessageWhenSaved()
        {

            // Arrange
            ShoppingList rvm = new ShoppingList
            {
                ID = -2
            };


            // Act
            ActionResult ar = Controller.PostEdit(rvm);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            // Assert
            Assert.AreEqual("Something is wrong with the data!", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Details", view.ViewName);
             
        }
    }
}

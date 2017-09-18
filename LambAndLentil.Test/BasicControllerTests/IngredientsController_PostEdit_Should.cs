using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Models;
using System.Web.Mvc;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    public class IngredientsController_PostEdit_Should:IngredientsController_Test_Should
    {
        [TestMethod]
        public void ReturnIndexWithValidModelStateWithSuccessMessageWhenSaved()
        {

            // Arrange
            Ingredient rvm = new Ingredient
            {
                ID = -2
            };


            // Act
            ActionResult ar = controller.PostEdit(rvm);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            // Assert
            Assert.AreEqual("Something is wrong with the data!", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Details", view.ViewName);
        }

        [TestMethod]
        public void ReturnIndexWithInValidModelStateWithWarningMessageWhenSaved()
        {

            // Arrange
            Ingredient rvm = new Ingredient
            {
                ID = -2
            };


            // Act
            ActionResult ar = controller.PostEdit(rvm);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            // Assert
            Assert.AreEqual("Something is wrong with the data!", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Details", view.ViewName); 
        }
    }
}

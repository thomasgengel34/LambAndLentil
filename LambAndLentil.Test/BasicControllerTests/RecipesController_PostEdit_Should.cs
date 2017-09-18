using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using LambAndLentil.UI.Models;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    public class RecipesController_PostEdit_Should: RecipesController_Test_Should
    {
        [TestMethod]
        public void ReturnIndexWithValidModelStateWithSuccessMessageWhenSaved()
        {
            // Arrange
            Recipe rvm = new Recipe
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
            Recipe rvm = new Recipe
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

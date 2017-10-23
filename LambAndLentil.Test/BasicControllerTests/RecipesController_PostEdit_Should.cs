using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestCategory("RecipesController")]
    [TestCategory("PostEdit")]

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

        [Ignore]
        [TestMethod]
        public void NotSaveLogicallyInvalidModel()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();

        }

        [Ignore]
        [TestMethod]
        public void NotSaveModelFlaggedInvalidByDataAnnotation()
        {  // see https://msdn.microsoft.com/en-us/library/cc668224(v=vs.98).aspx

            // Arrange

            // Act

            // Assert
            Assert.Fail();

        }
    }
} 

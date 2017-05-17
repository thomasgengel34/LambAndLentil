using LambAndLentil.UI.Controllers;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Tests.Controllers
{
    [TestClass]
    public class RecipesControllerTest
    {

        [TestMethod]
        public void RecipesControllerInheritsFromBaseController()
        {
            // Arrange
            RecipesController testController = SetUpRecipeController();

            // Act 
            Type baseType = typeof(BaseController);
            bool isBase = baseType.IsInstanceOfType(testController);

            // Assert 
            Assert.AreEqual(isBase, true);
        }



        [TestMethod]
        public void RecipesControllerIsPublic()
        {
            // Arrange
            RecipesController testController = SetUpRecipeController();

            // Act
            Type type = testController.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        }

        #region supporting methods

        private RecipesController SetUpRecipeController()
        {
            // - create the mock repository
            Mock<IRecipeRepository> mock = new Mock<IRecipeRepository>();
            mock.Setup(m => m.Recipes).Returns(new Recipe[5]  
            .AsQueryable());

            // Arrange - create a controller
           RecipesController controller = new RecipesController(mock.Object); 
            return controller;
        }


        #endregion
    }
}

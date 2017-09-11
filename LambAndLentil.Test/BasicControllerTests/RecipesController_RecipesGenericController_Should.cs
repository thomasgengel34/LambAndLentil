using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Models; 
using LambAndLentil.Tests.Controllers;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("RecipesController")]
    public class RecipesController_RecipesGenericController_Should : RecipesController_Test_Should
    {
        [TestMethod]
        public void InheritBaseControllerInRecipeVM()
        {
            // Arrange

            // Act 
            Type type = Type.GetType("LambAndLentil.UI.Controllers.RecipesController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(BaseController<RecipeVM>)));
        }

        [TestMethod]
        public void InheritRecipesGenericControllerInRecipeVM()
        {
            // Arrange

            // Act
            Type type = Type.GetType("LambAndLentil.UI.Controllers.RecipesController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(RecipesGenericController<RecipeVM>)));
        }

        [TestMethod]
        public void CallRepositoryInRecipeVM()
        {
            // Arrange

            // Act
            Type type = Repo.GetType();
           string name=  type.GenericTypeArguments[0].Name;
            // Assert
            Assert.AreEqual("RecipeVM", name);
        }
    }
}

using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("RecipesController")]
    public class RecipesController_RecipesGenericController_Should : RecipesController_Test_Should
    {
        [TestMethod]
        public void InheritBaseControllerInRecipe()
        {
            // Arrange

            // Act 
            Type type = Type.GetType("LambAndLentil.UI.Controllers.RecipesController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(BaseController<Recipe>)));
        }

        [TestMethod]
        public void InheritRecipesGenericControllerInRecipe()
        {
            // Arrange

            // Act
            Type type = Type.GetType("LambAndLentil.UI.Controllers.RecipesController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(RecipesGenericController<Recipe>)));
        }

        [TestMethod]
        public void CallRepositoryInRecipe()
        {
            // Arrange

            // Act
            Type type = Repo.GetType();
           string name=  type.GenericTypeArguments[0].Name;
            // Assert
            Assert.AreEqual("Recipe", name);
        }
    }
}

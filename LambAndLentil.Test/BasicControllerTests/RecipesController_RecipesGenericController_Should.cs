using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace  LambAndLentil.Test.BaseControllerTests
{

    [TestClass]
    [TestCategory("RecipesController")]
    public class RecipesController_RecipesGenericController_Should : RecipesController_Test_Should
    { 
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

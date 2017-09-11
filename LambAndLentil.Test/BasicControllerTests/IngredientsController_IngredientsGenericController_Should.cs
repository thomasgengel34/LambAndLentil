using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Models; 

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("IngredientsController")]
    public class IngredientsController_IngredientsGenericController_Should : IngredientsController_Test_Should
    {
        [TestMethod]
        public void InheritBaseControllerInIngredientVM()
        {
            // Arrange

            // Act 
            Type type = Type.GetType("LambAndLentil.UI.Controllers.IngredientsController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(BaseController<IngredientVM>)));
        }

        [TestMethod]
        public void InheritIngredientsGenericControllerInIngredientVM()
        {
            // Arrange

            // Act
            Type type = Type.GetType("LambAndLentil.UI.Controllers.IngredientsController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(IngredientsGenericController<IngredientVM>)));
        }

        [TestMethod]
        public void CallRepositoryInIngredientVM()
        {
            // Arrange

            // Act
            Type type = Repo.GetType();
           string name=  type.GenericTypeArguments[0].Name;
            // Assert
            Assert.AreEqual("IngredientVM", name);
        }
    }
}

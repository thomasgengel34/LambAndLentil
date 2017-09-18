using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Models;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("IngredientsController")]
    public class IngredientsController_IngredientsGenericController_Should : IngredientsController_Test_Should
    {
        [TestMethod]
        public void InheritBaseControllerInIngredient()
        {
            // Arrange

            // Act 
            Type type = Type.GetType("LambAndLentil.UI.Controllers.IngredientsController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(BaseController<Ingredient>)));
        }

        [TestMethod]
        public void InheritIngredientsGenericControllerInIngredient()
        {
            // Arrange

            // Act
            Type type = Type.GetType("LambAndLentil.UI.Controllers.IngredientsController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(IngredientsGenericController<Ingredient>)));
        }

        [TestMethod]
        public void CallRepositoryInIngredient()
        {
            // Arrange

            // Act
            Type type = Repo.GetType();
           string name=  type.GenericTypeArguments[0].Name;
            // Assert
            Assert.AreEqual("Ingredient", name);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Models;
using LambAndLentil.Tests.Controllers;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("ShoppingListsController")]
    public class ShoppingListsController_ShoppingListsGenericController_Should : ShoppingListsController_Test_Should
    {
        [TestMethod]
        public void InheritBaseControllerInShoppingList()
        {
            // Arrange

            // Act 
            Type type = Type.GetType("LambAndLentil.UI.Controllers.ShoppingListsController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(BaseController<ShoppingList>)));
        }

        [TestMethod]
        public void InheritShoppingListsGenericControllerInShoppingList()
        {
            // Arrange

            // Act
            Type type = Type.GetType("LambAndLentil.UI.Controllers.ShoppingListsController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(ShoppingListsGenericController<ShoppingList>)));
        }

        [TestMethod]
        public void CallRepositoryInShoppingList()
        {
            // Arrange

            // Act
            Type type = Repo.GetType();
           string name=  type.GenericTypeArguments[0].Name;
            // Assert
            Assert.AreEqual("ShoppingList", name);
        }
    }
}

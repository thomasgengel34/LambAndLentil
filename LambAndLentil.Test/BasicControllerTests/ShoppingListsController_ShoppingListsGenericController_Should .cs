using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Models;
using LambAndLentil.Tests.Controllers;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("ShoppingListsController")]
    public class ShoppingListsController_ShoppingListsGenericController_Should : ShoppingListsController_Test_Should
    {
        [TestMethod]
        public void InheritBaseControllerInShoppingListVM()
        {
            // Arrange

            // Act 
            Type type = Type.GetType("LambAndLentil.UI.Controllers.ShoppingListsController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(BaseController<ShoppingListVM>)));
        }

        [TestMethod]
        public void InheritShoppingListsGenericControllerInShoppingListVM()
        {
            // Arrange

            // Act
            Type type = Type.GetType("LambAndLentil.UI.Controllers.ShoppingListsController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(ShoppingListsGenericController<ShoppingListVM>)));
        }

        [TestMethod]
        public void CallRepositoryInShoppingListVM()
        {
            // Arrange

            // Act
            Type type = Repo.GetType();
           string name=  type.GenericTypeArguments[0].Name;
            // Assert
            Assert.AreEqual("ShoppingListVM", name);
        }
    }
}

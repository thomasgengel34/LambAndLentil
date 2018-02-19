using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Models;
using LambAndLentil.Tests.Controllers;
using LambAndLentil.Domain.Entities;

namespace  LambAndLentil.Test.BaseControllerTests
{

    [TestClass]
    [TestCategory("ShoppingListsController")]
    public class ShoppingListsController_ShoppingListsGenericController_Should : ShoppingListsController_Test_Should
    { 
        [TestMethod]
        public void CallRepositoryInShoppingList()
        { 
            Type type = Repo.GetType();
           string name=  type.GenericTypeArguments[0].Name;
            // Assert
            Assert.AreEqual("ShoppingList", name);
        }
    }
}

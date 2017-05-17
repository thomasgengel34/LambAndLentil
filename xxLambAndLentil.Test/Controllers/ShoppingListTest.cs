using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.Domain.Abstract;
using Moq;

namespace LambAndLentil.Tests.Controllers
{
    [TestClass]
    public class ShoppingListTest
    {
        [TestMethod]
        public void ShoppingListsControllerInheritsFromBaseControllerCorrectly()
        {

            // Arrange
            ShoppingListController controller = SetUpSimpleController();
            // Act 
            controller.PageSize = 4;

            var type = typeof(ShoppingListController);
            var DoesDisposeExist = type.GetMethod("Dispose");

            // Assert 
            Assert.AreEqual(4, controller.PageSize);
            Assert.IsNotNull(DoesDisposeExist);
        }

        private ShoppingListController SetUpSimpleController()
        {
            // - create the mock repository
            Mock<IShoppingListRepository> mock = new Mock<IShoppingListRepository>();


            // Arrange - create a controller
            ShoppingListController controller = new ShoppingListController(mock.Object);
            // controller.PageSize = 3;

            return controller;
        }
    }
}

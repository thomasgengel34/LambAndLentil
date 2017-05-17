using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using Moq;
using LambAndLentil.Domain.Abstract;

namespace LambAndLentil.Tests.Controllers
{
    [TestClass]
    public class MenusController
    {
        [TestMethod]
        public void MenusControllerInheritsFromBaseControllerCorrectly()
        {
             
            Assert.IsNotNull("this is a placeholder");
        }

        [TestMethod]
        public void MenusControllerIsPublic()
        {
            // Arrange
            MenusController testController = new MenusController();

            // Act
            Type type = testController.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(true, isPublic);
        }


        private MenusController SetUpSimpleController()
        {
            // - create the mock repository
            Mock<IMenuRepository> mock = new Mock<IMenuRepository>();


            // Arrange - create a controller
            MenusController controller = new MenusController();
            // controller.PageSize = 3;

            return controller;
        }
    }
}

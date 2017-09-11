using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Models; 
using LambAndLentil.Tests.Controllers;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("MenusController")]
    public class MenusController_MenusGenericController_Should : MenusController_Test_Should
    {
        [TestMethod]
        public void InheritBaseControllerInMenuVM()
        {
            // Arrange

            // Act 
            Type type = Type.GetType("LambAndLentil.UI.Controllers.MenusController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(BaseController<MenuVM>)));
        }

        [TestMethod]
        public void InheritMenusGenericControllerInMenuVM()
        {
            // Arrange

            // Act
            Type type = Type.GetType("LambAndLentil.UI.Controllers.MenusController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(MenusGenericController<MenuVM>)));
        }

        [TestMethod]
        public void CallRepositoryInMenuVM()
        {
            // Arrange

            // Act
            Type type = Repo.GetType();
           string name=  type.GenericTypeArguments[0].Name;
            // Assert
            Assert.AreEqual("MenuVM", name);
        }
    }
}

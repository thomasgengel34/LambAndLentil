using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Models; 
using LambAndLentil.Tests.Controllers;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("MenusController")]
    public class MenusController_MenusGenericController_Should : MenusController_Test_Should
    {
        [TestMethod]
        public void InheritBaseControllerInMenu()
        {
            // Arrange

            // Act 
            Type type = Type.GetType("LambAndLentil.UI.Controllers.MenusController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(BaseController<Menu>)));
        }

        [TestMethod]
        public void InheritBaseAttachDetachControllerInMenu()
        { 
            Type type = Type.GetType("LambAndLentil.UI.Controllers.MenusController, LambAndLentil.UI", true);
           
            Assert.IsTrue(type.IsSubclassOf(typeof(BaseAttachDetachController<Menu>)));
        }

        [TestMethod]
        public void CallRepositoryInMenu()
        {
            // Arrange

            // Act
            Type type = Repo.GetType();
           string name=  type.GenericTypeArguments[0].Name;
            // Assert
            Assert.AreEqual("Menu", name);
        }
    }
}

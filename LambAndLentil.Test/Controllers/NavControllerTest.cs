using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil;
using LambAndLentil.UI.Controllers;
using Moq;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using System.Collections;

namespace LambAndLentil.Tests.Controllers
{ 
    [TestClass]
    [TestCategory("NavController")]
    public class NavControllerTest
    { 

        [TestMethod]
        public void NavControllerInheritsFromBaseController()
        {
            // Arrange
            NavController testController = new NavController();

            // Act 
            Type baseType = typeof(IController);
            bool isBase = baseType.IsInstanceOfType(testController);

            // Assert 
            Assert.AreEqual(isBase, true);
        }

        [TestMethod]
        public void NavCtr_IsPublic()
        {
            // Arrange
            NavController testController = new NavController();

            // Act
            Type type = testController.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        }

      
 
        #region private methods
        private NavController SetUpController()
        {
            // - create the mock repository
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.Ingredients).Returns(new Ingredient[] {
                new Ingredient {ID = 1, Name = "P1" },
                new Ingredient {ID = 2, Name = "P2" },
                new Ingredient {ID = 3, Name = "P3"  },
                new Ingredient {ID = 4, Name = "P4" },
                new Ingredient {ID = 5, Name = "P5" }
            }.AsQueryable());

            // Arrange - create a controller
            NavController controller = new NavController(mock.Object);
            

            return controller;
        }
        #endregion
    }
}

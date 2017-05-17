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

         [TestMethod]
         public void NavCtr_CanCreateCategories()
        { 
            // Arrange - create a controller
            NavController target = SetUpController();

            // Act = get the set of categories
            string[] results = ((IEnumerable<string>)target.MakerMenu().Model).ToArray();

            // Assert
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0],"A");
            Assert.AreEqual(results[1],"B");
            Assert.AreEqual(results[2],"C"); 
        }

        [TestMethod]
        public void NavCtr_IndicatesSelectedMaker()
        {
            // Arrange
            NavController controller = SetUpController();
            string makerToSelect = "A";

            // Action
            string result = controller.MakerMenu(makerToSelect).ViewBag.SelectedMaker;

            // Assert
            Assert.AreEqual(makerToSelect, result);
        }

       

        #region private methods
        private NavController SetUpController()
        {
            // - create the mock repository
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.Ingredients).Returns(new Ingredient[] {
                new Ingredient {ID = 1, Name = "P1", Maker="A"},
                new Ingredient {ID = 2, Name = "P2", Maker="B"},
                new Ingredient {ID = 3, Name = "P3", Maker="A"},
                new Ingredient {ID = 4, Name = "P4", Maker="B"},
                new Ingredient {ID = 5, Name = "P5", Maker="C"}
            }.AsQueryable());

            // Arrange - create a controller
            NavController controller = new NavController(mock.Object);
            

            return controller;
        }
        #endregion
    }
}

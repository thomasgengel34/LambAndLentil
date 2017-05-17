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
        public void NavControllerIsPublic()
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
         public void NavControllerCanCreateCategories()
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
        public void NavControllerIndicatesSelectedMaker()
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
            Mock<IIngredientRepository> mock = new Mock<IIngredientRepository>();
            mock.Setup(m => m.Ingredients).Returns(new Ingredient[] {
                new Ingredient {IngredientID = 1, ShortDescription = "P1", Maker="A"},
                new Ingredient {IngredientID = 2, ShortDescription = "P2", Maker="B"},
                new Ingredient {IngredientID = 3, ShortDescription = "P3", Maker="A"},
                new Ingredient {IngredientID = 4, ShortDescription = "P4", Maker="B"},
                new Ingredient {IngredientID = 5, ShortDescription = "P5", Maker="C"}
            }.AsQueryable());

            // Arrange - create a controller
            NavController controller = new NavController(mock.Object);
            

            return controller;
        }
        #endregion
    }
}

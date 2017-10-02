using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Models;
using System.Web.Mvc;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    public class IngredientsController_PostEdit_Should:IngredientsController_Test_Should
    {
        [TestMethod]
        public void ReturnIndexWithValidModelStateWithSuccessMessageWhenSaved()
        {

            // Arrange
            Ingredient rvm = new Ingredient
            {
                ID = -2
            };


            // Act
            ActionResult ar = controller.PostEdit(rvm);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            // Assert
            Assert.AreEqual("Something is wrong with the data!", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Details", view.ViewName);
        }

        [TestMethod]
        public void ReturnIndexWithInValidModelStateWithWarningMessageWhenSaved()
        {

            // Arrange
            Ingredient rvm = new Ingredient
            {
                ID = -2
            };


            // Act
            ActionResult ar = controller.PostEdit(rvm);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            // Assert
            Assert.AreEqual("Something is wrong with the data!", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Details", view.ViewName); 
        }

        [Ignore]
        [TestMethod]
        public void BindCorrectIngredientsBoundInEdit()
        { // Bind(Include = "ID, Name, Description, CreationDate, ModifiedDate,  IngredientsList")] 
            // Arrange
            Ingredient ingredient = new Ingredient { ID = 1000, AddedByUser = "Not Changed", CreationDate = new DateTime(2014, 2, 2), Description = "Original Description", IngredientsList = "This, That, Those", ModifiedByUser = "See No Evil", ModifiedDate = new DateTime(2014, 2, 3), Name = "Punkin", Recipe = new Recipe { Name = "No Fat Flakes" } };
            Repo.Add(ingredient);
            // Act
            ingredient.ID = 2000;
            ingredient.AddedByUser = "Changed";
            ingredient.CreationDate = new DateTime(2016, 4, 5);
            ingredient.Description = "Changed";
            ingredient.IngredientsList = "none of the above";
            ingredient.ModifiedByUser = "Sam the Man";
            ingredient.ModifiedDate = new DateTime(1776, 7, 4);
            ingredient.Name = "Sweets";
            ingredient.Recipe = new Recipe { Name = "Mostly Fat Flakes" };
            ActionResult ar = controller.PostEdit(ingredient);


            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void NotBindIngredientsNotIdentifiedToBeBoundInEdit()
        {

        }

        [TestCleanup]
        [TestMethod]
        public void TestCleanup()
        {
            ClassCleanup();
        }
    }
}

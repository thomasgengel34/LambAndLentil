using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Models;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using System.Linq;
using LambAndLentil.UI;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    public class IngredientsController_Attach_Should : IngredientsController_Test_Should
    {
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void SuccessfullyAttachIngredientChild()
        {
            // Arrange
            Ingredient child = new Ingredient() { ID = 3000, Name = "SuccessfullyAttachIngredientChild" };
            Repo.Save(child);

            // Act
            Controller.AttachIngredient(Ingredient.ID, child);
            ReturnedIngredient = Repo.GetById(Ingredient.ID);
            // Assert
            //  Assert.AreEqual("Default", Ingredient.Ingredients.Last().Name);
            Assert.AreEqual("SuccessfullyAttachIngredientChild", ReturnedIngredient.Ingredients.Last().Name);
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void SuccessfullyDetachFirstIngredientChild()
        {
            IGenericController<Ingredient> DetachController = new IngredientsController(Repo);
            BaseSuccessfullyDetachIngredientChild(Repo,  Controller, DetachController, UIControllerType.ShoppingLists,0);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachARangeOfIngredientChildren()
        { // RemoveRange
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachTheLastIngredientChild()
        { // RemoveAt
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachAllIngredientChildren()
        { // RemoveAll
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}

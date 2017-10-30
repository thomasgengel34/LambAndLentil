using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Models;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using System.Linq;

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
            ReturnedIngredient=Repo.GetById(Ingredient.ID);
            // Assert
           //  Assert.AreEqual("Default", Ingredient.Ingredients.Last().Name);
             Assert.AreEqual("SuccessfullyAttachIngredientChild", ReturnedIngredient.Ingredients.Last().Name);
        }
         
        [TestMethod]
        public void SuccessfullyDetachIngredientChild()
        {
            // Arrange
            Ingredient child = new Ingredient() { ID = 3500, Name = "SuccessfullyAttachAndDetachhIngredientChild" };
            Repo.Save(child);
            Controller.AttachIngredient(Ingredient.ID, child);
            // Act
            Controller.DetachIngredient(Ingredient.ID, child);
            ReturnedIngredient = Repo.GetById(Ingredient.ID);
            // Assert
            //  Assert.AreEqual("Default", Ingredient.Ingredients.Last().Name);
            Assert.AreEqual("SuccessfullySuccessfullyAttachAndDetachhIngredientChildIngredientChild", ReturnedIngredient.Ingredients.Last().Name);
        } 
    }
}

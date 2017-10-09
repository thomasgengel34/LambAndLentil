using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Models;
using System.Web.Mvc;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    public class IngredientsController_PostEdit_Should : IngredientsController_Test_Should
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
            ActionResult ar = Controller.PostEdit(rvm);
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
            ActionResult ar = Controller.PostEdit(rvm);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            // Assert
            Assert.AreEqual("Something is wrong with the data!", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Details", view.ViewName);
        }


        [TestMethod]
        public void BindCorrectIngredientsBoundInEdit()
        { // Bind(Include = "ID, Name, Description, CreationDate,   IngredientsList")] 
          // Arrange
            DateTime creationDate = new DateTime(2014, 2, 2);
            DateTime modifiedDate = new DateTime(2014, 2, 3);
            Ingredient ingredient = new Ingredient { ID = 1000, AddedByUser = "Not Changed", CreationDate = creationDate, Description = "Original Description", IngredientsList = "This, That, Those", ModifiedByUser = "See No Evil", ModifiedDate = modifiedDate, Name = "Punkin", Recipe = new Recipe { Name = "No Fat Flakes" } };
            Repo.Add(ingredient);

            // Act 
            ActionResult ar = Controller.PostEdit(ingredient);

            Ingredient returnedIngredient = Repo.GetById(1000);
           

            // Assert  // Bind(Include = "ID, Name, Description, CreationDate,  IngredientsList")] 
            Assert.AreEqual(1000, returnedIngredient.ID);
            Assert.AreEqual("Punkin", returnedIngredient.Name);
            Assert.AreEqual("Original Description", returnedIngredient.Description);
            Assert.AreEqual(creationDate, returnedIngredient.CreationDate); 
            Assert.AreEqual("This, That, Those", returnedIngredient.IngredientsList);

          
        }
         
        [TestMethod]
        public void NotBindIngredientsNotIdentifiedToBeBoundInEdit()
        { // Bind(Include = "ID, Name, Description, CreationDate,  IngredientsList")] 
          // do not bind AddedByUser, ModifiedByUser

          // Arrange
            DateTime creationDate = new DateTime(2014, 2, 2);
            DateTime modifiedDate = new DateTime(2014, 2, 3);
            Ingredient ingredient = new Ingredient { ID = 1000, AddedByUser = "Not Changed", CreationDate = creationDate, Description = "Original Description", IngredientsList = "This, That, Those", ModifiedByUser = "See No Evil", ModifiedDate = modifiedDate, Name = "Punkin", Recipe = new Recipe { Name = "No Fat Flakes" } };
            Repo.Save(ingredient);

            // Act
            ingredient.AddedByUser = "Hermann Hesse  cxcxcxsr12212244443434";
            ingredient.ModifiedByUser = "Huck Finn gergtwtvkjtittjutjt-5258686545345";
            Controller.PostEdit(ingredient);
            Ingredient returnedIngredient = Repo.GetById(1000);

            // Assert
             Assert.AreNotEqual("Hermann Hesse  cxcxcxsr12212244443434", returnedIngredient.AddedByUser);
            Assert.AreNotEqual("Huck Finn gergtwtvkjtittjutjt-5258686545345", returnedIngredient.ModifiedByUser);
        }

        
        [TestMethod]
        public void ModifiedDateUpDatesInEdit()
        {
            // Arrange
            DateTime creationDate = new DateTime(2014, 2, 2);
            DateTime modifiedDate = new DateTime(2014, 2, 3);
            Ingredient ingredient = new Ingredient { ID = 1000, AddedByUser = "Not Changed", CreationDate = creationDate, Description = "Original Description", IngredientsList = "This, That, Those", ModifiedByUser = "See No Evil", ModifiedDate = modifiedDate, Name = "Punkin", Recipe = new Recipe { Name = "No Fat Flakes" } };
            Repo.Save(ingredient);


            // Act
            Controller.PostEdit(ingredient);
            Ingredient returnedIngredient = Repo.GetById(1000);
            // Assert
            Assert.AreNotEqual(modifiedDate, returnedIngredient.ModifiedDate);
        } 
    }
}

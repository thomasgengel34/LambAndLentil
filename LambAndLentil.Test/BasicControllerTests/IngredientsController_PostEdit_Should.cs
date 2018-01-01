﻿using System;
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
        private static DateTime CreationDate {get;set;}
        private static DateTime ModifiedDate {get;set; }

        public IngredientsController_PostEdit_Should()
        {

            CreationDate = new DateTime(2014, 2, 2);
            ModifiedDate = new DateTime(2014, 2, 3);
            Ingredient = new Ingredient { ID = 1000, AddedByUser = "Not Changed", CreationDate = CreationDate, Description = "Original Description", IngredientsList = "This, That, Those", ModifiedByUser = "See No Evil", ModifiedDate =ModifiedDate, Name = "Punkin" };
            Repo.Save((Ingredient)Ingredient);
        }


        [TestMethod]
        public void ReturnIndexWithValidModelStateWithSuccessMessageWhenSaved()
        {

            // Arrange
            Ingredient rvm = new Ingredient
            {
                ID = -2
            };


            // Act
            ActionResult ar =  Controller.PostEdit(rvm);
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
            Ingredient.ID = -2;


            // Act
            ActionResult ar =  Controller.PostEdit((Ingredient)Ingredient);
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

            // Act 
            ActionResult ar =  Controller.PostEdit((Ingredient)Ingredient);

            Ingredient returnedIngredient = Repo.GetById(Ingredient.ID);


            // Assert  // Bind(Include = "ID, Name, Description, CreationDate,  IngredientsList")] 
            Assert.AreEqual(1000, returnedIngredient.ID);
            Assert.AreEqual("Punkin", returnedIngredient.Name);
            Assert.AreEqual("Original Description", returnedIngredient.Description);
            Assert.AreEqual(CreationDate, returnedIngredient.CreationDate);
            Assert.AreEqual("This, That, Those", returnedIngredient.IngredientsList);


        }

        [TestMethod]
        public void NotBindIngredientPropertiesNotIdentifiedToBeBoundInEdit()
        { 
             
            IIngredient ingredient = new Ingredient { ID = 1000, AddedByUser = "Not Changed",    ModifiedByUser = "Original"  };
            Repo.Save((Ingredient)ingredient);

            ingredient.AddedByUser = "Changed";
            ingredient.ModifiedByUser = "Should Not Be Original";
            Controller.PostEdit((Ingredient)ingredient);
            IIngredient returnedIngredient = Repo.GetById(ingredient.ID);

            Assert.AreNotEqual("Changed", returnedIngredient.AddedByUser);
            Assert.AreNotEqual("Should Not Be Original", returnedIngredient.ModifiedByUser);
        }

        [TestMethod]
        public void ModifiedDateUpDatesInEdit()
        {
            // Arrange

            // Act
            Controller.PostEdit((Ingredient)Ingredient);
            Ingredient returnedIngredient = Repo.GetById(1000);
            // Assert
            Assert.AreNotEqual(ModifiedDate, returnedIngredient.ModifiedDate);
        }

        [Ignore]
        [TestMethod]
        public void NotSaveLogicallyInvalidModel() =>
            // Arrange

            // Act

            // Assert
            Assert.Fail();

        [Ignore]
        [TestMethod]
        public void NotSaveModelFlaggedInvalidByDataAnnotation()=>
          // see https://msdn.microsoft.com/en-us/library/cc668224(v=vs.98).aspx

            // Arrange

            // Act

            // Assert
            Assert.Fail();

        
    }
}

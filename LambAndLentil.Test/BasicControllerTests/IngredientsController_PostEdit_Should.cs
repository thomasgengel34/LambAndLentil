using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Models;
using System.Web.Mvc;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.Domain.Entities;

namespace  LambAndLentil.Test.BaseControllerTests
{
    [TestClass]
   internal class IngredientsController_PostEdit_Should : IngredientsController_Test_Should
    { 
         
        private static void BindCorrectIngredientsBoundInEdit()
        {
            DateTime creationDate = DateTime.Now;
          Ingredient ingredient = new Ingredient(creationDate) { ID = 1000, Name = "Punkin", Description = "Original Description", IngredientsList = "This, That, Those" };

            ActionResult ar =  controller.PostEdit((Ingredient)ingredient);

            Ingredient returnedIngredient = repo.GetById(ingredient.ID);

            Assert.IsNotNull(returnedIngredient); 
            Assert.AreEqual(1000, returnedIngredient.ID);
            Assert.AreEqual("Punkin", returnedIngredient.Name);
            Assert.AreEqual("Original Description", returnedIngredient.Description);
            Assert.AreEqual(creationDate, returnedIngredient.CreationDate);
            Assert.AreEqual("This, That, Those", returnedIngredient.IngredientsList); 
        }
         

     
        private static void  ModifiedDateUpDatesInEdit()
        {
            DateTime ModifiedDate = DateTime.Now;
            Ingredient.ModifiedDate = ModifiedDate;
            controller.PostEdit(Ingredient);
            Ingredient returnedIngredient = repo.GetById(Ingredient.ID);
            
            Assert.AreNotEqual(ModifiedDate, returnedIngredient.ModifiedDate);
        }

      
    }
}

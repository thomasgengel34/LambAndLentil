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
        public void BindCorrectIngredientsBoundInEdit()
        {
          Ingredient ingredient = new Ingredient { ID = 1000, Name = "Punkin", Description = "Original Description", IngredientsList = "This, That, Those" };

            ActionResult ar =  Controller.PostEdit((Ingredient)ingredient);

            Ingredient returnedIngredient = Repo.GetById(ingredient.ID);

            Assert.IsNotNull(returnedIngredient); 
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
            Controller.PostEdit((Ingredient)Ingredient);
            Ingredient returnedIngredient = Repo.GetById(1000);
            
            Assert.AreNotEqual(ModifiedDate, returnedIngredient.ModifiedDate);
        }

        [Ignore]
        [TestMethod]
        public void NotSaveLogicallyInvalidModel() =>
            
            Assert.Fail();

        [Ignore]
        [TestMethod]
        public void NotSaveModelFlaggedInvalidByDataAnnotation()=>
          // see https://msdn.microsoft.com/en-us/library/cc668224(v=vs.98).aspx
           
            Assert.Fail();

        
    }
}

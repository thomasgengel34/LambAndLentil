
using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace  LambAndLentil.Test.BaseControllerTests
{
    [TestClass]
    [TestCategory("RecipesController")]
    [TestCategory("Edit")]
   internal class RecipesController_Edit_Should:RecipesController_Test_Should
    {
        
        [Ignore]
        [TestMethod]
        public void CorrectRecipesAreBoundInEdit()
        {
            Assert.Fail();
        }



        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void EditRecipe()
        {  
            Assert.Fail();
        }

       
        [TestMethod]
        [TestCategory("Edit")]
        public void NotEditNonexistentRecipe()
        { 
            Recipe result = (Recipe)((ViewResult)controller.Edit(8)).ViewData.Model;
          
            Assert.IsNull(result);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void CorrectlyChangeIngredientMeasurementInARecipe()
        { 
            Assert.Fail();
        } 
    }
}

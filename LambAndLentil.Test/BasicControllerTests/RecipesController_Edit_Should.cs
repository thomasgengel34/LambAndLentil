
using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace  LambAndLentil.Test.BaseControllerTests
{
    [TestClass]
    [TestCategory("RecipesController")]
    [TestCategory("Edit")]
    public class RecipesController_Edit_Should:RecipesController_Test_Should
    {
        public RecipesController_Edit_Should():base()
        {

        }
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
            // Arrange 

            // Act  

            // Assert  
            Assert.Fail();
        }

       
        [TestMethod]
        [TestCategory("Edit")]
        public void NotEditNonexistentRecipe()
        {
            // Arrange

            // Act
            Recipe result = (Recipe)((ViewResult)Controller.Edit(8)).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void CorrectlyChangeIngredientMeasurementInARecipe()
        {
            // Arrange 

            // Act  

            // Assert  
            Assert.Fail();
        }

     
    }
}

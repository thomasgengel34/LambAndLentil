using System;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BaseControllerTests
{
    [TestClass]
    [TestCategory("RecipesController")]
    [TestCategory("Details")]
    internal class RecipesController_Detail_Should:RecipesController_Test_Should
    { 
        public RecipesController_Detail_Should() => Recipe = new Recipe(); 

        [TestMethod]
        public void BeSuccessfulWithValidIngredientID()
        {  
            ActionResult ar = controller.Details(int.MaxValue);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

          
            Assert.IsNotNull(ar);
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Recipe));
            Assert.AreEqual("Here it is!", adr.Message);
        }
         
         
        
        [Ignore]
        [TestMethod]
        public void ReturnDetailsViewActionTypeEdit_ValidID()
        {
             
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnDetailsViewActionTypeEdit_InValidID()
        { 
            Assert.Fail();
        }
        // the following are not really testable.  I am keeping them to remind me of that.
        //[TestMethod]
        //public void RecipesCtr_DetailsIngredientIDIsNotANumber() { }

        //[TestMethod]
        //public void RecipesCtr_DetailsIngredientIDIsNotAInteger() { } 

      

        [TestMethod]
        [TestCategory("Details")]
        public void ReturnDetailsWithSuccessWithValidRecipeID()
        { 
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.Details(int.MaxValue);
            ViewResult view = (ViewResult)adr.InnerResult;
          
            Assert.IsNotNull(view);

            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Recipe));
        }
          
    }
}

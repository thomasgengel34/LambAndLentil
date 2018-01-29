using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("RecipesController")]
    [TestCategory("Index")]
    public class RecipesController_Index_Test:RecipesController_Test_Should
    { 
        public RecipesController_Index_Test()
        {
            Controller.PageSize = 3;
        }

   
      

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsNotNull()
        { 
             ListEntity = ( ListEntity<Recipe>)((ViewResult)Controller.Index(1)).Model;
            Recipe[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;
             
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Recipe>)(view1.Model)).ListT.Count();
             
            Assert.IsNotNull(view1); 
        }
  
         
         
        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate_ArrayFirstItemNameIsCorrect()
        { 
            var result = ( ListEntity<Recipe>)((ViewResult)Controller.Index(1)).Model;
            Recipe[] ingrArray1 = result.ListT.ToArray();
             
            Assert.AreEqual("LambAndLentil.Domain.Entities.Recipe ControllerTest1", ingrArray1[0].Name); 
        }
         
         
        [Ignore]
        [TestMethod]
        public void FlagAnRecipeFlaggedInAPerson()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void FlagAnRecipeFlaggedInTwoPersons()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void WhenAFlagHasBeenRemovedFromOnePersonStillThereForSecondFlaggedPerson()
        {
            Assert.Fail();
        }
         
    }
}

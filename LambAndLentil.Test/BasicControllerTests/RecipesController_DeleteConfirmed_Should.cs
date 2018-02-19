using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BaseControllerTests
{
    [TestCategory("RecipesController")]
    [TestCategory("DeleteConfirmed")]
    [Ignore]
    [TestClass]
    public class RecipesController_DeleteConfirmed_Should:RecipesController_Test_Should
    {
        [TestMethod]
        public void ReturnIndexWithWarningWhenIDIsNotFound()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnIndexWithConfirmationWhenIDIsFound()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void DetachTheCorrectItemAndNotOtherItemsWhenIDIsFound()
        {
             
            Assert.Fail();
        }
    }
}

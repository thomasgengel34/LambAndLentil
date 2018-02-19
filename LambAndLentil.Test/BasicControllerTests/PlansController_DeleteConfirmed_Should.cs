using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace  LambAndLentil.Test.BaseControllerTests
{
    [TestCategory("PlansController")]
    [TestCategory("DeleteConfirmed")]
    [Ignore]
    [TestClass]
    public class PlansController_DeleteConfirmed_Should: PlansController_Test_Should
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

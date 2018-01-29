using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestCategory("MenusController")]
    [TestCategory("DeleteConfirmed")]
    [Ignore]
    [TestClass]
    public class MenusController_DeleteConfirmed_Should
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
        public void DeleteTheCorrectItemAndNotOtherItemsWhenIDIsFound()
        { 
            Assert.Fail();
        }
    }
}

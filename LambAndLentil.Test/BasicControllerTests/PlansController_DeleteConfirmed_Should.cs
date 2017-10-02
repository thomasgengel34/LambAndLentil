using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
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
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnIndexWithConfirmationWhenIDIsFound()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void RemoveTheCorrectItemAndNotOtherItemsWhenIDIsFound()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}

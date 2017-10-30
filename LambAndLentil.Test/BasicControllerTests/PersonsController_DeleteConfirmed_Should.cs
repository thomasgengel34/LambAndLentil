using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestCategory("PersonsController")]
    [TestCategory("DeleteConfirmed")]
    [Ignore]
    [TestClass]
    public class PersonsController_DeleteConfirmed_Should:PersonsController_Test_Should
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
        public void DetachTheCorrectItemAndNotOtherItemsWhenIDIsFound()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}

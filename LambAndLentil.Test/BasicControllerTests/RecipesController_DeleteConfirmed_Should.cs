using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
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

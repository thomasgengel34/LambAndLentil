using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [Ignore]
    [TestClass]
    [TestCategory("ShoppingListsController")]

    public class ShoppingListsController_DeleteConfirmed_Should:ShoppingListsController_Test_Should
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

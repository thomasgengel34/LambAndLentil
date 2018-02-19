using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BaseControllerTests
{
    [Ignore]
    [TestClass]
    [TestCategory("ShoppingListsController")]

    public class ShoppingListsController_DeleteConfirmed_Should:ShoppingListsController_Test_Should
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

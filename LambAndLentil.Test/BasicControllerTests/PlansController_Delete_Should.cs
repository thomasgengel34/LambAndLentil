 using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("PlansController")]
    [TestCategory("Delete")]
    public class PlansController_Delete_Should: PlansController_Test_Should
    {
        public PlansController_Delete_Should()
        {

        }
        [Ignore]
        [TestMethod]
        public void AllowUserToConfirmDeleteRequestAndCallConfirmDelete()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnIndexWithWarningWhenIDIsNotFound()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnIDetailsWhenIDIstFound()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}

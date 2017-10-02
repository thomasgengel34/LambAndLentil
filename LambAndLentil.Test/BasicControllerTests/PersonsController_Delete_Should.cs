 
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestCategory("PersonsController")]
    [TestCategory("Delete")]
    [TestClass]
    public class PersonsController_Delete_Should:PersonsController_Test_Should
    {
        public PersonsController_Delete_Should()
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

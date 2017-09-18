using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [Ignore]
    [TestClass]
    public class MenusController_Attach_Should:MenusController_Test_Should
    {
        [TestMethod]
        public void ReturnsErrorWithUnknownRepository()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnsIndexWithWarningWithUnknownParentID()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnsIndexWithWarningWithNullParent()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownChildID()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnsDetailWithWarningWithNullChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}

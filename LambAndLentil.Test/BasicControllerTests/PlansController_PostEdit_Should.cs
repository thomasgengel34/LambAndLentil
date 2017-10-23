using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [Ignore]
    [TestClass]
    [TestCategory("PlansController")]
    [TestCategory("PostEdit")] 
    public class PlansController_PostEdit_Should:PlansController_Test_Should
    {
        [TestMethod]
        public void ReturnIndexWithValidModelStateWithSuccessMessageWhenSaved()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnIndexWithInValidModelStateWithWarningMessageWhenSaved()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void NotSaveLogicallyInvalidModel()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();

        }

        [Ignore]
        [TestMethod]
        public void NotSaveModelFlaggedInvalidByDataAnnotation()
        {  // see https://msdn.microsoft.com/en-us/library/cc668224(v=vs.98).aspx

            // Arrange

            // Act

            // Assert
            Assert.Fail();

        }
    }
} 

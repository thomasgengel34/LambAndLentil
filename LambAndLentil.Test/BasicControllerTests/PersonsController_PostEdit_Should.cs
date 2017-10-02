using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestCategory("PersonsController")]
    [TestCategory("PostEdit")]
    [Ignore]
    [TestClass]
    public class PersonsController_PostEdit_Should:PersonsController_Test_Should
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
    }
}

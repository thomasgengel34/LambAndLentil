using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.ControllerSecurity
{
    [Ignore]
    [TestCategory("Security")]
    [TestCategory("PlansController")]
    [TestCategory("Attach")]
    [TestClass]
    public class PlansAttachShouldAllow
    {
       //  Anon no    Auth yes Admin yes


        [TestMethod]
        public void NoAccessToAnonymousUser()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void  AccessToAuthorizedUser()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void  AccessToAdmin()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}

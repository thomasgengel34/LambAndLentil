using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.AdminSecurity
{
    [Ignore]
    [TestClass]
    public class ErrorLogging
    {
        [TestMethod]
        public void UserLockoutDueToUnsuccessfulLoginIsLogged()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();

        }

        [TestMethod]
        public void UserLoginIsLogged()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void UserLogoutIsLogged()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void UserServedAServerErrorPageIsLogged()
        {   // either error in the view itself or in the code
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void UnhandledExceptionIsLogged()
        {  // intended to log a C# exception specifically
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void StackOverFlowExceptionIsLogged()
        {  // might indicate someone is playing games
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void DangerousRouteAttemptedIsLogged()
        {  // might indicate someone is playing games
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}

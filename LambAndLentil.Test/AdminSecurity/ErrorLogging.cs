using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.AdminSecurity
{
    [Ignore]
    [TestClass]
    internal  static class ErrorLogging
    {
        [TestMethod]
        private static void UserLockoutDueToUnsuccessfulLoginIsLogged()
        { 
            Assert.Fail();

        }

        [TestMethod]
        private static void UserLoginIsLogged()
        { 
            Assert.Fail();
        }

        [TestMethod]
        private static void UserLogoutIsLogged()
        { 
            Assert.Fail();
        }

        [TestMethod]
        private static void UserServedAServerErrorPageIsLogged()
        {   // either error in the view itself or in the code
             
            Assert.Fail();
        }

        [TestMethod]
        private static void UnhandledExceptionIsLogged()
        {  // intended to log a C# exception specifically 
            Assert.Fail();
        }

        [TestMethod]
        private static void StackOverFlowExceptionIsLogged()
        {  // might indicate someone is playing games 
            Assert.Fail();
        }

        [TestMethod]
        private static void DangerousRouteAttemptedIsLogged()
        {  // might indicate someone is playing games 
            Assert.Fail();
        }
    }
}

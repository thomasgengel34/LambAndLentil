using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;

// Freeman's code on p 291 is out of date

namespace LambAndLentil.Test.AdminSecurity
{
    [Ignore]
    [TestClass]
   public   class AdminSecurityTests
    {
        static AccountController Controller;

         AdminSecurityTests() => Controller = new AccountController();


        [TestMethod]
        private static void CanLogInWithValidCredentials()
        { 
            ActionResult ar = Controller.Login("user");
             
            Assert.Fail();   // need to finish writing this

        }

        [TestMethod]
        private static void CannotLogInWithInvalidCredentials()
        { 
            Assert.Fail();   // need to finish writing this
        }

        [TestMethod]
        private static void AuthorizedUserCanSeeUnRestrictedPage()
        { 
            Assert.Fail();   // need to finish writing this
        }

        [TestMethod]
        private static void AuthorizedUserCanSeeRestrictedPage()
        {
             
            Assert.Fail();   // need to finish writing this
        }

        [TestMethod]
        private static void UnAuthorizedUserCannotSeeRestrictedPage()
        { 
            Assert.Fail();   // need to finish writing this
        }

        [TestMethod]
        private static void UnAuthorizedUserCanSeeUnRestrictedPage()
        { 
            Assert.Fail();   // need to finish writing this
        }

        [TestMethod]
        private static void AnonymousUserCanSeeUnRestrictedPage()
        { 
            Assert.Fail();   // need to finish writing this
        }

        [TestMethod]
        private static void AnonymousUserCannotSeenRestrictedPage()
        { 
            Assert.Fail();   // need to finish writing this
        }

        [TestMethod]
        private static void AnonymousUserCannotSeenAdminPage()
        { 
            Assert.Fail();   // need to finish writing this
        }

        [TestMethod]
        private static void UnauthorizedUserCannotSeenAdminPage()
        { 
            Assert.Fail();   // need to finish writing this
        }

        [TestMethod]
        private static void AdminCanSeeAdminPage()
        { 
            Assert.Fail();   // need to finish writing this
        }
    }
}

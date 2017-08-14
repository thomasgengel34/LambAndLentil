using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;

// Freeman's code on p 291 is out of date

namespace LambAndLentil.Test.AdminSecurity
{
    [Ignore]
    [TestClass]
    public class AdminSecurityTests
    {
        static AccountController controller;

        public AdminSecurityTests()
        {
            controller = new AccountController();
        }


        [TestMethod]
        public void CanLogInWithValidCredentials()
        {
            // Arrange



            // Act
            ActionResult ar = controller.Login("user");

            // Assert
            Assert.Fail();   // need to finish writing this

        }

        [TestMethod]
        public void CannotLogInWithInvalidCredentials()
        {
            // Arrange
          


            // Act


            // Assert   
            Assert.Fail();   // need to finish writing this
        }
    }
}

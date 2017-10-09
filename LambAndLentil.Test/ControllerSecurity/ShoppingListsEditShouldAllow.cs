using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Web.Mvc;
using System.Linq;
using LambAndLentil.Test.BasicControllerTests;

namespace LambAndLentil.Test.ControllerSecurity
{
   
    [TestCategory("Security")]
    [TestCategory("ShoppingListsController")]
    [TestCategory("Edit")]
    [TestClass]
    public class ShoppingListsEditShouldAllow:ShoppingListsController_Test_Should
    {
        //  Anon no    Auth yes Admin yes

        [Ignore]
        [TestMethod]
        public void NoAccessToAnonymousUser()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void  AccessToAuthorizedUser()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void  AccessToAdmin()
        {
            // Arrange  // +		$exception	{"Sequence contains no matching element"}	System.InvalidOperationException

            var isAdmin = typeof(Controller).GetMethods().First(x => x.Name == "Create").GetCustomAttribute<AuthorizeAttribute>(false).Roles == "ADMIN";
            // Act

            // Assert
            Assert.IsTrue(!isAdmin);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Web.Mvc;
using System.Linq;
using LambAndLentil.Test.BaseControllerTests;

namespace LambAndLentil.Test.ControllerSecurity
{
   
    [TestCategory("Security")]
    [TestCategory("ShoppingListsController")]
    [TestCategory("Edit")]
    [TestClass]
  internal class ShoppingListsEditShouldAllow:ShoppingListsController_Test_Should
    {
        //  Anon no    Auth yes Admin yes

        [Ignore]
        [TestMethod]
        public void NoAccessToAnonymousUser()
        {
             
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void  AccessToAuthorizedUser()
        {
            
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void  AccessToAdmin()
        {
           // +		$exception	{"Sequence contains no matching element"}	System.InvalidOperationException

            var isAdmin = typeof(Controller).GetMethods().First(x => x.Name == "Create").GetCustomAttribute<AuthorizeAttribute>(false).Roles == "ADMIN";
             
            Assert.IsTrue(!isAdmin);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.ControllerSecurity
{
    [Ignore]
    [TestCategory("Security")]
    [TestCategory("ShoppingListsController")]
    [TestCategory("Create")]
    [TestClass]
    public class ShoppingListsCreateShouldAllow
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

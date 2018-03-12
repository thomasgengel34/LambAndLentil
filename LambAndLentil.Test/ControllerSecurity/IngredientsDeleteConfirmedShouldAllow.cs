using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.ControllerSecurity
{
    [Ignore]
    [TestClass]
    [TestCategory("Security")]
    [TestCategory("IngredientsController")]
    [TestCategory("DeleteConfirmed")]
    public class IngredientsDeleteConfirmedShouldAllow
    {
       //  Anon no    Auth yes Admin yes


        [TestMethod]
        public void NoAccessToAnonymousUser()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void  AccessToAuthorizedUser()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void  AccessToAdmin()
        { 
            Assert.Fail();
        }
    }
}

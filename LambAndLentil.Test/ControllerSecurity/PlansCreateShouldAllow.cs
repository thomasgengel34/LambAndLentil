﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.ControllerSecurity
{
    [Ignore]
    [TestClass]
    [TestCategory("Security")]
    [TestCategory("PlansController")]
    [TestCategory("Create")]
    public class PlansCreateShouldAllow
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

using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("MenusController")]
    [TestCategory("Index")]
    public class MenusController_Index_Test<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    { 
        [Ignore]
        [TestMethod]
        public void FlagAnMenuFlaggedInAPerson() => 
            Assert.Fail();

        [Ignore]
        [TestMethod]
        public void FlagAnMenuFlaggedInTwoPersons() =>
             
            Assert.Fail();

        [Ignore]
        [TestMethod]
        public void WhenAFlagHasBeenRemovedFromOnePersonStillThereForSecondFlaggedPerson() => 
            Assert.Fail();

         
    }
}

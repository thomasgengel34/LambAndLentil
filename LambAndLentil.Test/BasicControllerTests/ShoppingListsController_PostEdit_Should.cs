using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace LambAndLentil.Test.BaseControllerTests
{
    [TestClass]
    [TestCategory("ShoppingListsController")]

    public class ShoppingListsController_PostEdit_Should : ShoppingListsController_Test_Should
    {
        

        [Ignore]
        [TestMethod]
        public void NotSaveLogicallyInvalidModel() =>
            Assert.Fail();

        [Ignore]
        [TestMethod]
        public void NotSaveModelFlaggedInvalidByDataAnnotation() => Assert.Fail();
        // see https://msdn.microsoft.com/en-us/library/cc668224(v=vs.98).aspx    
    }  
} 

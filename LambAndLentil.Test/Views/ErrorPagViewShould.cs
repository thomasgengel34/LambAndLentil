using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Test.BasicControllerTests;
using System.Web.Mvc;
using System.Web.Routing;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure;

namespace LambAndLentil.Test.Views
{
  
    //[TestClass]
    public class ErrorPageViewShould: IngredientsController_Test_Should
    {
        
        [TestMethod]
        public void HaveTextLineUpOnPageLines()
        { 
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ProvideSomeGuidanceOnWhatToDoIfUserCanDoAnything()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ProvidesEmailFormForUserSupport()
        {
            Assert.Fail();
        }
    }
}
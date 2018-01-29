using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using LambAndLentil.UI;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("Create")]
    [TestCategory("RecipesController")]
    public class RecipesController_Create_Should:RecipesController_Test_Should
    {
        [TestMethod]
        public void  ReturnNonNull()
        { 
            ViewResult result = (ViewResult)Controller.Create() ; 
            Assert.IsNotNull(result);
        }

    }
}

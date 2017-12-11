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
            ViewResult result = (ViewResult)Controller.Create(UIViewType.Create) ; 
            Assert.IsNotNull(result);
        }

        [TestMethod] 
        public void Create()
        { 
            ViewResult view = (ViewResult)Controller.Create(UIViewType.Edit); 
            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
        } 
    }
}

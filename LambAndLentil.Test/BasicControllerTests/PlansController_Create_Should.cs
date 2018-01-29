using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestCategory("PlansController")]
    [TestCategory("Create")]
    [TestClass]
    public class PlansController_Create_Should:PlansController_Test_Should
    {
        [TestMethod]
        [TestCategory("Create")]
        public void Create()
        {  
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Create();
            ViewResult view = (ViewResult)adr.InnerResult;

            Assert.IsNotNull(view);
            Assert.AreEqual(UIViewType.Details.ToString(), view.ViewName);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("Here it is!", adr.Message); 
        }  
    }
}

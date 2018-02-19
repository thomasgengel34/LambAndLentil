using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BaseControllerTests
{
    [TestClass]
    [TestCategory("PlansController")]
    [TestCategory("Details")]
    public class PlansController_Detail_Should:PlansController_Test_Should
    {  

        [TestMethod]
        public void BeSuccessfulWithValidPlanID()
        {  
            ActionResult ar = Controller.Details(int.MaxValue);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;
             
            Assert.IsNotNull(ar);
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Plan));
            Assert.AreEqual("Here it is!", adr.Message);
        }
           
          
        [Ignore]
        [TestMethod]
        public void ReturnDetailsViewActionTypeEdit_ValidID()
        { 
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnDetailsViewActionTypeEdit_InValidID()
        { 
            Assert.Fail();
        }
          
        [TestMethod]
        [TestCategory("Details")]
        public void WorksWithValidPlanID()
        { 
            ActionResult ar = Controller.Details(int.MaxValue);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;
 
            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Plan));
            Assert.IsTrue(true);
        }  
    }
}

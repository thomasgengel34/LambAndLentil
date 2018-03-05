using System;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace  LambAndLentil.Test.BaseControllerTests
{
    [TestClass]
    [TestCategory("MenusController")]
    [TestCategory("Details")]
    internal class MenusController_Detail_Should:MenusController_Test_Should
    {   
         
         
        private static void DetailsWorksWithValidRecipeID()
        { 
            ActionResult ar = controller.Details(ListEntity.ListT.FirstOrDefault().ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;
             
            Assert.IsNotNull(ar);
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Menu));
        }
         
         
         
        // the following are not really testable.  I am keeping them to remind me of that.
        //[TestMethod]
        //private static  MenusCtr_DetailsMenuIDIsNotANumber() { }

        //[TestMethod]
        //private static  MenusCtr_DetailsMenuIDIsNotAInteger() { } 

    }
}

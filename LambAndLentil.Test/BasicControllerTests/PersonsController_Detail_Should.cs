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
    [TestCategory("PersonsController")]
    [TestCategory("Details")]
    internal class PersonsController_Detail_Should:PersonsController_Test_Should
    { 
        

        public PersonsController_Detail_Should()
        { 
        }
          
        [TestMethod]
        public void BeSuccessfulWithValidPersonID()
        {  
            ActionResult ar = controller.Details(int.MaxValue);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;
 
            Assert.IsNotNull(ar);
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Person));
            Assert.AreEqual("Here it is!", adr.Message);
        }

       


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsPersonIDPastIntLimit()
        {
            // I am not sure how I want this to operate.  Wait until UI is set up and see then.
             
            ViewResult result = controller.Details(int.MaxValue) as ViewResult;
             
            Assert.IsNotNull(result);
        }
         

        [TestMethod]
        [TestCategory("Details")]
        public void ReturnErrorIfResultIsNotFound_PersonIDIsZeroMessageIsCorrect()
        { 
            ActionResult ar = controller.Details(0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

             
            Assert.AreEqual("No Person was found with that id.", adr.Message);
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
        public void WorksWithValidRecipeID()
        {
              

            // Act 
            ActionResult ar = controller.Details(int.MaxValue);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            
            Assert.IsNotNull(view);

            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Person));
        }
          
    }
}

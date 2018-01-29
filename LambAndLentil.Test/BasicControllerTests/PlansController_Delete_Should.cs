using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("PlansController")]
    [TestCategory("Delete")]
    public class PlansController_Delete_Should: PlansController_Test_Should
    {
        public PlansController_Delete_Should()
        {

        }
        [Ignore]
        [TestMethod]
        public void AllowUserToConfirmDeleteRequestAndCallConfirmDelete()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnIndexWithWarningWhenIDIsNotFound()
        { 
            Assert.Fail();
        }

          
         
        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteConfirmed()
        {
          
            int count = Repo.Count();
          
            ActionResult result = Controller.DeleteConfirmed(int.MaxValue) as ActionResult;
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { Controller = " ListEntityTVM", Action = "Index" } } );
            
            Assert.IsNotNull(result);
            Assert.AreEqual(count - 1, Repo.Count());
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void CanDeleteValidPlan()
        {
           
            Plan pVM = new Plan() { ID = 6000, Name = "test CanDeleteValidPlan" };
            int count = Repo.Count();
            Repo.Save(pVM);
            int countPlus = Repo.Count();

            
            ActionResult result = Controller.DeleteConfirmed(pVM.ID);
            int countEnding = Repo.Count();
            AlertDecoratorResult adr = (AlertDecoratorResult)result;
             
            Assert.AreEqual("test CanDeleteValidPlan has been deleted", adr.Message);
            Assert.AreEqual(count, countEnding);
            Assert.AreEqual(count + 1, countPlus);
        }
    }
}

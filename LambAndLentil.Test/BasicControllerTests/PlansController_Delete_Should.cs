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
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnIDetailsWhenIDIstFound()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteAFoundPlan()
        {
            // Arrange


            // Act 
            var view = Controller.Delete(int.MaxValue) as ViewResult;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual(UIViewType.Details.ToString(), view.ViewName);
            Assert.AreEqual(6, Repo.Count());  // shows this does not actually delete anything
        }



        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteAnInvalidPlan()
        {
            // Arrange


            // Act 
            var view = Controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Plan was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual(UIViewType.Index.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
            //Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            //Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteConfirmed()
        {
            // Arrange
            int count = Repo.Count();
            // Act
            ActionResult result = Controller.DeleteConfirmed(int.MaxValue) as ActionResult;
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { Controller = " ListEntityTVM", Action = "Index" } } );
            // Assert 
            Assert.IsNotNull(result);
            Assert.AreEqual(count - 1, Repo.Count());
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void CanDeleteValidPlan()
        {
            // Arrange  
            Plan pVM = new Plan() { ID = 6000, Name = "test CanDeleteValidPlan" };
            int count = Repo.Count();
            Repo.Add(pVM);
            int countPlus = Repo.Count();

            // Act - delete the plan
            ActionResult result = Controller.DeleteConfirmed(pVM.ID);
            int countEnding = Repo.Count();
            AlertDecoratorResult adr = (AlertDecoratorResult)result;

            // Assert 
            Assert.AreEqual("test CanDeleteValidPlan has been deleted", adr.Message);
            Assert.AreEqual(count, countEnding);
            Assert.AreEqual(count + 1, countPlus);
        }
    }
}

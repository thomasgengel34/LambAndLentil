using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Infrastructure.Alerts;
using System.Web.Mvc;
using System.Linq;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestCategory("IngredientsController")]
    [TestCategory("DeleteConfirmed")]
   
    [TestClass]
    public class IngredientsController_DeleteConfirmed_Should:IngredientsController_Test_Should
    { 
        [TestMethod]
        public void ReturnIndexWithConfirmationWhenIDIsFound()
        { //        return RedirectToAction(UIViewType.BaseIndex.ToString()).WithSuccess(string.Format("{0} has been deleted", item.Name));
          // Arrange
          

            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.DeleteConfirmed(item.ID);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
            Assert.AreEqual(item.Name+ " has been deleted", adr.Message);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(1, rtrr.RouteValues.Count, 1);
            Assert.AreEqual("BaseIndex", rtrr.RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        public void DeleteTheCorrectItemAndNotOtherItemsWhenIDIsFound()
        {
            // Arrange
            int initialCount = Repo.Count();

            // Act
             Controller.DeleteConfirmed(int.MaxValue);
            int finalCount = Repo.Count();
            object shouldBeNull = Repo.GetById(int.MaxValue);

            // Assert

            Assert.AreEqual(initialCount - 1, finalCount);
            Assert.IsNull(shouldBeNull); 
        }
    }
}

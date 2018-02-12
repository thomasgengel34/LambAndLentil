using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    public class BasicDeleteConfirmed<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new() 
    {

        
        public static void ReturnIndexWithActionMethodDeleteConfirmedWithBadID()
        {
            ActionResult ar = Controller.DeleteConfirmed(-1);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            Assert.AreEqual(UIViewType.Index.ToString(), rdr.RouteValues.Values.ElementAt(0));
            Assert.AreEqual("No " + item.DisplayName + " was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }   

  
        public static void ReturnIndexWithConfirmationWhenIDIsFound()
        {  
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.DeleteConfirmed(item.ID);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
             
            Assert.AreEqual(item.Name + " has been deleted", adr.Message);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(1, rtrr.RouteValues.Count, 1);
            Assert.AreEqual(UIViewType.Index.ToString(), rtrr.RouteValues.Values.ElementAt(0).ToString());
        }

        public void DeleteTheCorrectItemAndNotOtherItemsWhenIDIsFound()
        {
            int initialCount = Repo.Count();

            Controller.DeleteConfirmed(int.MaxValue);
            int finalCount = Repo.Count();
            object shouldBeNull = Repo.GetById(int.MaxValue);

            Assert.AreEqual(initialCount - 1, finalCount);
            Assert.IsNull(shouldBeNull);
        }
    }
}

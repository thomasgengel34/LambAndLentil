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

        
        public void ReturnIndexWithActionMethodDeleteConfirmedWithBadID()
        {
            ActionResult ar = Controller.DeleteConfirmed(-1);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            Assert.AreEqual(UIViewType.BaseIndex.ToString(), rdr.RouteValues.Values.ElementAt(0));
            Assert.AreEqual(ClassName+" was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }

        public void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithFoundResult()
        {
            int count = Repo.Count();

            Controller.DeleteConfirmed(int.MaxValue);
            T item = Repo.GetById(int.MaxValue);

            Assert.AreEqual(count - 1, Repo.Count());
            Assert.IsNull(item);
            //   TODO: Assert.Fail();  // make sure the correct item was deleted before removing this line 
        }

        public void ReturnIndexWithConfirmationWhenIDIsFound()
        {  
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.DeleteConfirmed(item.ID);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
             
            Assert.AreEqual(item.Name + " has been deleted", adr.Message);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(1, rtrr.RouteValues.Count, 1);
            Assert.AreEqual(UIViewType.Index.ToString(), rtrr.RouteValues.Values.ElementAt(0).ToString());
        }
    }
}

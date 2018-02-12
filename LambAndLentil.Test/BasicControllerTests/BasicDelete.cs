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
    public class BasicDelete<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new() 
    {
        public void ReturnIndexWithWarningWhenIDIsNotFound()
        {
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Delete(400);
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;


            Assert.AreEqual(UIViewType.Index.ToString(), rdr.RouteValues.Values.ElementAt(0));
            Assert.AreEqual(item.DisplayName+" was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);

        }
    }
}

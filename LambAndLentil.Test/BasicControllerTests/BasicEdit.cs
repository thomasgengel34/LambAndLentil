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
    public class BasicEdit<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new() 
    {
        public void ReturnIndexWithWarningForNonexistentIngredient()
        {
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Edit(1000);
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            Assert.AreEqual(UIViewType.Index.ToString(), rdr.RouteValues.ElementAt(0).Value.ToString());
            Assert.AreEqual(ClassName+" was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }
    }
}

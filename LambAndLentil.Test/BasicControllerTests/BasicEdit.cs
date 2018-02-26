using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace  LambAndLentil.Test.BaseControllerTests
{
    internal  class BasicEdit<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new() 
    {
        private static void ReturnIndexWithWarningForNonexistentIngredient()
        {
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.Edit(1000);
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            Assert.AreEqual(UIViewType.Index.ToString(), rdr.RouteValues.ElementAt(0).Value.ToString());
            Assert.AreEqual(item.DisplayName+" was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }
         
       private static void CorrectPlansAreBoundInEdit()
        {
            Assert.Fail();
        }

         
        private static void CanSetUpToEditPlan()
        {
            ViewResult view1 = (ViewResult)controller.Edit(int.MaxValue);
            Plan p1 = (Plan)view1.Model;
            ViewResult view2 = (ViewResult)controller.Edit(int.MaxValue - 1);
            Plan p2 = (Plan)view2.Model;
            ViewResult view3 = (ViewResult)controller.Edit(int.MaxValue - 2);
            Plan p3 = (Plan)view3.Model;

            Assert.IsNotNull(view1);
            Assert.AreEqual(int.MaxValue, p1.ID);
            Assert.AreEqual(int.MaxValue - 1, p2.ID);
            Assert.AreEqual(int.MaxValue - 2, p3.ID);
            Assert.AreEqual("PlansController_Index_Test P1", p1.Name);
            Assert.AreEqual("PlansController_Index_Test P2", p2.Name);
            Assert.AreEqual("PlansController_Index_Test P3", p3.Name);
        }
  
 
        private static void CreateReturnsNonNull()
        {
            ViewResult result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}

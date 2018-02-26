using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BaseControllerTests
{
    internal class BasicDetails<T> : BaseControllerTest<T>
         where T : BaseEntity, IEntity, new()
    {
        internal static void TestRunner()
        {
            BeSuccessfulWithValidMenuID();
            BeSuccessfulWithValidPlanID();
            ReturnIndexWithErrorWhenIDIsNegative();
            IDIsZeroViewIsNotNull();
            ReturnIndexWithErrorWhenIDIsZero();
            ReturnIndexWithValidID();
        }

        private static void ReturnIndexWithErrorWhenIDIsNegative()
        {
            ViewResult view = controller.Details(-1) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            T t = new T() { ID = 100 };
            Assert.IsNotNull(view);
            Assert.AreEqual("No " + t.DisplayName + " was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.Index.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        private static void IDIsZeroViewIsNotNull()
        {
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            Assert.IsNotNull(view);
        }

        private static void ReturnIndexWithErrorWhenIDIsZero()
        {
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            Assert.AreEqual("No " + item.DisplayName + " was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.Index.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        private static void ReturnIndexWithValidID()
        {
            // sut = system.under.test 
            T sut = new T { ID = 60000 };
            repo.Save(sut);
            ActionResult ar = controller.Details(sut.ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            ViewResult viewResult = (ViewResult)adr.InnerResult;

            Assert.IsNotNull(ar);
            Assert.AreEqual(UIViewType.Details.ToString(), viewResult.ViewName);
            Assert.AreEqual("Here it is!", adr.Message);
            Assert.AreEqual("alert-success", adr.AlertClass);
        }

        private static void BeSuccessfulWithValidPlanID()
        {
            ActionResult ar = controller.Details(int.MaxValue);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            Assert.IsNotNull(ar);
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Plan));
            Assert.AreEqual("Here it is!", adr.Message);
        }



        private static void ReturnDetailsViewActionTypeEdit_ValidID()
        {
            Assert.Fail();
        }
        private static void ReturnDetailsViewActionTypeEdit_InValidID()
        {
            Assert.Fail();
        }

        private static void WorksWithValidPlanID()
        {
            ActionResult ar = controller.Details(int.MaxValue);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Plan));
            Assert.IsTrue(true);
        }

         
        private static void BeSuccessfulWithValidMenuID()
        {
            ActionResult ar = controller.Details(int.MaxValue);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            Assert.IsNotNull(ar);
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Menu));
            Assert.AreEqual("Here it is!", adr.Message);
        }
    }
}

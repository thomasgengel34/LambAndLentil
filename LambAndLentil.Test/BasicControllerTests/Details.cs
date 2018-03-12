using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicTests
{
    internal class Details<T> : BaseControllerTest<T>
         where T : BaseEntity, IEntity, new()
    {
        internal static void TestRunner()
        {
            BeSuccessfulWithValidIngredientID();
            ReturnIndexWithErrorWhenIDIsNegative();
            IDIsZeroViewIsNotNull();
            ReturnIndexWithErrorWhenIDIsZero();
            ReturnDetailsWithSuccessWithValidID();
            ReturnDetailsWhenIDIsFound();
        }


        private static void BeSuccessfulWithValidIngredientID()
        {
            SetUpForTests(out repo, out controller, out item);

            ActionResult ar = controller.Details(item.ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar; 
            ViewResult viewResult = (ViewResult)adr.InnerResult;

            Assert.IsNotNull(ar);
            Assert.AreEqual(UIViewType.Details.ToString(), viewResult.ViewName);
            Assert.AreEqual("Here it is!", adr.Message);
            Assert.AreEqual("alert-success", adr.AlertClass);
        }

        private static void ReturnIndexWithErrorWhenIDIsNegative()
        {
            SetUpForTests(out repo, out controller, out item);
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

        private static void ReturnDetailsWithSuccessWithValidID()
        {
            SetUpForTests(out repo, out controller, out item);
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.Details(item.ID);
            ViewResult view = (ViewResult)adr.InnerResult;

            Assert.IsNotNull(adr);
            Assert.IsNotNull(view);
            Assert.AreEqual(UIViewType.Details.ToString(), view.ViewName);
            Assert.AreEqual("Here it is!", adr.Message);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.IsInstanceOfType(view.Model, typeof(T));
        }



        private static void ReturnDetailsViewActionTypeEdit_ValidID()
        {
            Assert.Fail();  // TODO: setup
        }
        private static void ReturnDetailsViewActionTypeEdit_InValidID()
        {
            Assert.Fail();// TODO: setup
        }



        private static void ReturnErrorIfResultIsNotFound_PersonIDIsZeroMessageIsCorrect()
        {
            SetUpForTests(out repo, out controller, out item);
            ActionResult ar = controller.Details(0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            Assert.AreEqual("No " + typeof(T).Name + " was found with that id.", adr.Message);
        }

        private static void ReturnDetailsWhenIDIsFound()
        {
            SetUpForTests(out repo, out controller, out item);
          
            ActionResult ar = controller.Details(item.ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult vr = (ViewResult)adr.InnerResult;
            string viewName = vr.ViewName;
            int returnedID = ((T)(vr.Model)).ID;

            Assert.IsNotNull(ar);
            Assert.AreEqual("Here it is!", adr.Message);
            Assert.AreEqual(UIViewType.Details.ToString(), viewName);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(item.ID, returnedID);
        }  

    }
}

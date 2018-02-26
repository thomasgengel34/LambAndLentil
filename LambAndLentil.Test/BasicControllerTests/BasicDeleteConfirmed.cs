using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BaseControllerTests
{
    internal class BasicDeleteConfirmed<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    { 
        internal static void TestRunner()  
        {
            ReturnIndexWithActionMethodDeleteConfirmedWithBadID();
            ReturnIndexWithConfirmationWhenIDIsFound();
            DeleteTheCorrectItemAndNotOtherItemsWhenIDIsFound();
            DeleteConfirmed();
        }
        private static void ReturnIndexWithActionMethodDeleteConfirmedWithBadID()
        {
            T item = SetUpItemAndrepo(out controller);
            item.ID = -1;

            ActionResult ar = controller.DeleteConfirmed(item.ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;
         
            Assert.AreEqual(UIViewType.Index.ToString(), rdr.RouteValues.Values.ElementAt(0));
            Assert.AreEqual("No " + item.DisplayName + " was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }


        private static T SetUpItemAndrepo(out IGenericController<T> controller)
        {
            T item = new T() { ID = 5450 };
            repo = new TestRepository<T>();
            repo.Save(item);
             controller= BaseControllerTestFactory(typeof(T));
            return item;
        }


        private static void ReturnIndexWithConfirmationWhenIDIsFound()
        {
            T item = SetUpItemAndrepo(out controller);
         
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.DeleteConfirmed(item.ID);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            Assert.AreEqual(item.Name + " has been deleted", adr.Message);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(1, rtrr.RouteValues.Count, 1);
            Assert.AreEqual(UIViewType.Index.ToString(), rtrr.RouteValues.Values.ElementAt(0).ToString());
        }


        private static void DeleteTheCorrectItemAndNotOtherItemsWhenIDIsFound()
        {
            T item = SetUpItemAndrepo(out controller);
            int initialCount = repo.Count();

            controller.DeleteConfirmed(item.ID);
            int finalCount = repo.Count();
            object shouldBeNull = repo.GetById(item.ID);

            Assert.AreEqual(initialCount - 1, finalCount);
            Assert.IsNull(shouldBeNull);
        }

 
       private static void DeleteConfirmed()
        {

            int count = repo.Count();

            ActionResult result = controller.DeleteConfirmed(int.MaxValue) as ActionResult;
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { Controller = " ListEntityTVM", Action = "Index" } } );

            Assert.IsNotNull(result);
            Assert.AreEqual(count - 1, repo.Count());
        }
    }
}

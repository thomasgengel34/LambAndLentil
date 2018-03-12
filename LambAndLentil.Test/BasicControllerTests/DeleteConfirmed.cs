using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicTests
{
    internal class DeleteConfirmed<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    {
        internal static void TestRunner()
        {
            CountOfItemsInRepoIsReducedWhenDeleteConfirmIsRun();
            DeleteTheCorrectItemAndNotOtherItemsWhenIDIsFound();
            ItemIsDeleted();
            ReturnIndexWithActionMethodDeleteConfirmedWithBadID();
            ReturnIndexWithConfirmationWhenIDIsFound();
            ResultIsNotNull();
            ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithFoundResult();
        }
        private static void ReturnIndexWithActionMethodDeleteConfirmedWithBadID()
        {
            T item = SetUpItemAndRepo(out controller);
            item.ID = -1;

            ActionResult ar = controller.DeleteConfirmed(item.ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            Assert.AreEqual(UIViewType.Index.ToString(), rdr.RouteValues.Values.ElementAt(0));
            Assert.AreEqual("No " + item.DisplayName + " was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }


        private static T SetUpItemAndRepo(out IGenericController<T> controller)
        {
            T item = new T() { ID = 5450 };
            repo = new TestRepository<T>();
            repo.Save(item);
            controller = BaseControllerTestFactory();
            return item;
        }


        private static void ReturnIndexWithConfirmationWhenIDIsFound()
        {
            T item = SetUpItemAndRepo(out controller);

            AlertDecoratorResult adr = (AlertDecoratorResult)controller.DeleteConfirmed(item.ID);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            T returnedItem = repo.GetById(item.ID);

            Assert.IsNotNull(adr);
            Assert.IsNull(returnedItem);
            Assert.AreEqual(item.Name + " has been deleted", adr.Message);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(1, rtrr.RouteValues.Count, 1);
            Assert.AreEqual(UIViewType.Index.ToString(), rtrr.RouteValues.Values.ElementAt(0).ToString());
        }

        private static void CountOfItemsInRepoIsReducedWhenDeleteConfirmIsRun()
        {
            T item = SetUpItemAndRepo(out controller);
            int repoCount = repo.Count();
            controller.DeleteConfirmed(item.ID);
            int newrepoCount = repo.Count();
            Assert.AreEqual(repoCount - 1, newrepoCount);
        }

        private static void DeleteTheCorrectItemAndNotOtherItemsWhenIDIsFound()
        {
            T item = SetUpItemAndRepo(out controller);
            int initialCount = repo.Count();

            controller.DeleteConfirmed(item.ID);
            int finalCount = repo.Count();
            object shouldBeNull = repo.GetById(item.ID);

            Assert.AreEqual(initialCount - 1, finalCount);
            Assert.IsNull(shouldBeNull);
        }


        private static void ResultIsNotNull()
        {
            ClassCleanup();
            SetUpForTests(out repo, out controller, out item);
            int count = repo.Count();

            ActionResult result = controller.DeleteConfirmed(item.ID) as ActionResult;
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { Controller = " ListEntityTVM", Action = "Index" } } );

            Assert.IsNotNull(result);
        }

        private static void ItemIsDeleted()
        {
            ClassCleanup();
            SetUpForTests(out repo, out controller, out item);
            int count = repo.Count();

            ActionResult result = controller.DeleteConfirmed(item.ID) as ActionResult;
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { Controller = " ListEntityTVM", Action = "Index" } } );

            Assert.AreEqual(count - 1, repo.Count());
        }

        private static void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithFoundResult()
        {
            ClassCleanup();
            IRepository<T> repo = new TestRepository<T>();
            IGenericController<T> controller = BaseControllerTestFactory();

            T item = new T() { ID = 1000000, Name = "This is a test" };
            repo.Save(item);
            int beginningCount = repo.Count();

            ActionResult ar = controller.DeleteConfirmed(item.ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            T returnedItem = repo.GetById(item.ID);

            Assert.AreEqual(1, rdr.RouteValues.Count);
            Assert.IsNull(returnedItem);
            Assert.AreEqual(beginningCount - 1, repo.Count());
            Assert.AreEqual($"{item.Name} has been deleted", adr.Message);
            Assert.AreEqual(UIViewType.Index.ToString(), rdr.RouteValues.ElementAt(0).Value.ToString());
            Assert.AreEqual("alert-success", adr.AlertClass);
        }  // RedirectToAction(UIViewType.Index.ToString()).WithSuccess(string.Format($"{item.Name} has been deleted"));
    }
}

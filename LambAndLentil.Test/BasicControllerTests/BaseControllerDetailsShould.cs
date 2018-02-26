using System.Linq;
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
    internal class BaseControllerDetailsShould<T> : BaseControllerTest<T>
         where T : BaseEntity, IEntity, new()
    {

        internal static void TestRunner()  
        {
            ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithFoundResult();
            BeSuccessfulWithValidIngredientID();
        }

        private static void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithFoundResult()
        {
            ClassCleanup();
            IRepository<T> repo = new TestRepository<T>();
            IGenericController<T> controller = BaseControllerTestFactory(typeof(T));

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


        private static void BeSuccessfulWithValidIngredientID()
        {
            ClassCleanup();
            IRepository<T> repo = new TestRepository<T>();
            IGenericController<T> controller = BaseControllerTestFactory(typeof(T));
            T sut = new T { ID = 1000001 }; // sut = system.under.test 
            repo.Save(sut);


            ActionResult ar = controller.Details(sut.ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            ViewResult viewResult = (ViewResult)adr.InnerResult;

            Assert.IsNotNull(ar);
            Assert.AreEqual(UIViewType.Details.ToString(), viewResult.ViewName);
            Assert.AreEqual("Here it is!", adr.Message);
            Assert.AreEqual("alert-success", adr.AlertClass);
        }
    }
}

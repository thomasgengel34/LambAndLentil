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
        internal static void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithFoundResult()
        {
            ClassCleanup();
            IRepository<T> repo = new TestRepository<T>();
            IGenericController<T> controller = BaseControllerTestFactory(typeof(T));
           
            T item = new T() { ID = 1000000 };
            repo.Save(item);
            int count = repo.Count();

            ActionResult ar = controller.DeleteConfirmed(item.ID);
            T returnedItem = repo.GetById(item.ID);

            Assert.AreEqual(count - 1, repo.Count());
            Assert.IsNull(returnedItem);
            // TODO: flesh out rest of test

        }


        internal static void BeSuccessfulWithValidIngredientID()
        {
            ClassCleanup();
            IRepository<T> repo = new TestRepository<T>();
            IGenericController<T> controller = BaseControllerTestFactory(typeof(T));
            T sut = new T { ID = 60000 }; // sut = system.under.test 
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

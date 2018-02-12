using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    internal class BaseControllerDetailsShould<T> : BaseControllerTest<T>
         where T : BaseEntity, IEntity, new()
    {
       internal static void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithFoundResult()
        {
            int count = Repo.Count();

            T item = new T() { ID = 1000000 };
            Repo.Save(item);
             
            Controller.DeleteConfirmed(item.ID);
            T returnedItem = Repo.GetById(item.ID);

            Assert.AreEqual(count, Repo.Count());
            Assert.IsNull(returnedItem);
           
        }


        internal static void BeSuccessfulWithValidIngredientID()
        {
            // sut = system.under.test 
            T sut = new T { ID = 60000 };

            Repo.Save(sut);
            ActionResult ar = Controller.Details(sut.ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            ViewResult viewResult = (ViewResult)adr.InnerResult;

            Assert.IsNotNull(ar);
            Assert.AreEqual(UIViewType.Details.ToString(), viewResult.ViewName);
            Assert.AreEqual("Here it is!", adr.Message);
            Assert.AreEqual("alert-success", adr.AlertClass);
        }
    }
}


using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestCategory("PersonsController")]
    [TestCategory("Delete")]
    [TestClass]
    public class PersonsController_Delete_Should:PersonsController_Test_Should
    {
        public PersonsController_Delete_Should()
        {

        }
        [Ignore]
        [TestMethod]
        public void AllowUserToConfirmDeleteRequestAndCallConfirmDelete()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnIndexWithWarningWhenIDIsNotFound()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnIDetailsWhenIDIstFound()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }



        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteAFoundPerson()
        {
            // Arrange 

            // Act  
            ActionResult ar = Controller.Delete(int.MaxValue - 1);
            string viewName = ((ViewResult)ar).ViewName;

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual(UIViewType.Details.ToString(), viewName);
        }



        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteAnInvalidPerson()
        {
            // Arrange 
            var view = Controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Person was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual(UIViewType.Index.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteConfirmed()
        {
            // Arrange
            Person person = new Person { ID = 1 };
            Repo.Add(person);
            // Act
            ActionResult result = Controller.DeleteConfirmed(1) as ActionResult;
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { Controller = "Persons", Action = "Index" } } );
            // Assert 
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void CanDeleteValidPerson()
        {

            // Arrange  
            int initialCount = Repo.Count();
            Person person = new Person("John", "Doe") { ID = 100, Description = "test CanDeleteValidPerson" };
            Repo.Add(person);
            int newCount = Repo.Count();

            // Act - delete the person
            ActionResult result = Controller.DeleteConfirmed(person.ID);

            AlertDecoratorResult adr = (AlertDecoratorResult)result;

            // Assert
            Assert.AreEqual("John Doe has been deleted", adr.Message);
            Assert.AreEqual(initialCount, newCount - 1);
            Assert.AreEqual(initialCount, Repo.Count());
        }
    }
}

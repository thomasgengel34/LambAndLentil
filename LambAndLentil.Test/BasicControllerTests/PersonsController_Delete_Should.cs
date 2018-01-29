
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
    public class PersonsController_Delete_Should : PersonsController_Test_Should
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
        public void DeleteConfirmed()
        {
            // Arrange
            Person person = new Person { ID = 1 };
            Repo.Save(person);
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
            Repo.Save(person);
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

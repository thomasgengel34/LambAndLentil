
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;

namespace  LambAndLentil.Test.BaseControllerTests
{
    [TestCategory("PersonsController")]
    [TestCategory("Delete")]
    [TestClass]
    public class PersonsController_Delete_Should : PersonsController_Test_Should
    { 


         

       
      

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
         
    }
}

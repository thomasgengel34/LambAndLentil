
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
    internal class PersonsController_Delete_Should : PersonsController_Test_Should
    {  
        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteConfirmed()
        { 
            Person person = new Person { ID = 1 };
            repo.Save(person);
          
            ActionResult result = controller.DeleteConfirmed(1) as ActionResult;
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { Controller = "Persons", Action = "Index" } } );
           
            Assert.IsNotNull(result);
        } 
    }
}

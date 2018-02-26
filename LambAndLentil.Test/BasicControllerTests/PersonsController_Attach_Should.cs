using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace  LambAndLentil.Test.BaseControllerTests
{

    [TestClass]
   internal class PersonsController_Attach_Should : PersonsController_Test_Should
    {
        [Ignore]
        [TestMethod]
        public void ReturnsErrorWithUnknownrepository() =>
           
            Assert.Fail();
          

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void SuccessfullyDetachFirstIngredientChild()
        {
            //IGenericController<Person> DetachController = (IGenericController<Person>)(new PersonsController(repo));
            //BaseSuccessfullyDetachChild(repo, Controller, DetachController, UIControllerType.ShoppingLists, 0);
        }
         
    }
}

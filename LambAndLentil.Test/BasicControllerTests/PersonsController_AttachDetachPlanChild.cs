using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace  LambAndLentil.Test.BaseControllerTests
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    internal class PersonsController_AttachDetachPlanChild : PersonsController_Test_Should
    { 
        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachFirstPlanChild()
        { 
            Assert.Fail();
        }


        [TestMethod]
        public void SuccessfullyDetachASetOfMenuChildren()
        { 
            //Person.Menus.Add(new Menu { ID = 4005, Name = "Butter" });
            //Person.Menus.Add(new Menu { ID = 4006, Name = "Cayenne Pepper" });
            //Person.Menus.Add(new Menu { ID = 4007, Name = "Cheese" });
            //Person.Menus.Add(new Menu { ID = 4008, Name = "Chopped Green Pepper" });
            //repo.Save((Person)Person);
            //int initialMenuCount = Person.Menus.Count(); 
           
            //var setToSelect = new HashSet<int> { 4006, 4008 };
            //List<IEntity> selected = Person.Menus.Where(t => setToSelect.Contains(t.ID)).ToList();
            //controller.DetachASetOf(Person, selected);
            //Person returnedPerson = repo.GetById(Person.ID);
             
            //Assert.AreEqual(initialMenuCount - 2, returnedPerson.Menus.Count());
        }
         
    }
}

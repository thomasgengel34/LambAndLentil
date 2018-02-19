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
    public class PersonsController_AttachDetachMenuChild : PersonsController_Test_Should
    {
         

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachFirstMenuChild()
        {
            //IGenericController<Person> DetachController = (IGenericController<Person>)(new PersonsController(Repo));
            //BaseSuccessfullyDetachChild(Repo, Controller, DetachController, UIControllerType.Persons, 0);
        }

        [TestMethod]
        public void SuccessfullyDetachASetOfMenuChildren()
        { 
            //Person.Menus.Add(new Menu { ID = 4005, Name = "Butter" });
            //Person.Menus.Add(new Menu { ID = 4006, Name = "Cayenne Pepper" });
            //Person.Menus.Add(new Menu { ID = 4007, Name = "Cheese" });
            //Person.Menus.Add(new Menu { ID = 4008, Name = "Chopped Green Pepper" });
            //Repo.Save((Person)Person);
            //int initialMenuCount = Person.Menus.Count();
             
            //var setToSelect = new HashSet<int> { 4006, 4008 };
            //List<IEntity> selected = Person.Menus.Where(t => setToSelect.Contains(t.ID)).ToList();
            //Controller.DetachASetOf(Person, selected);
            //Person returnedPerson = Repo.GetById(Person.ID);
             
            //Assert.AreEqual(initialMenuCount - 2, returnedPerson.Menus.Count());
        }

     


        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachAllMenuChildren() =>
            Assert.Fail();

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWithWarningIfAttachingNullMenuChild() { }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownMenuChildID() { }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWhenDetachingShoppingListWithSuccessWithValidParentandValidIngredientChild() =>
                   Assert.Fail();
    }
}

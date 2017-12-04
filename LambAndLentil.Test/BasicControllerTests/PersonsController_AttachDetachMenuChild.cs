using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class PersonsController_AttachDetachMenuChild : PersonsController_Test_Should
    {
       
         
        [TestMethod]
        public void SuccessfullyAttachMenuChild()
        {
             IGenericController<Person> DetachController =(IGenericController<Person>)( new PersonsController(Repo));
             BaseSuccessfullyDetachMenuChild(Repo, Controller, DetachController, UIControllerType.Persons);
        }
          
        [TestMethod]
        public void SuccessfullyDetachFirstMenuChild()
        {
            IGenericController<Person> DetachController = (IGenericController<Person>)(new PersonsController(Repo));
            BaseSuccessfullyDetachIngredientChild(Repo, Controller, DetachController, UIControllerType.Persons, 0);
        }
         
        [TestMethod]
        public void SuccessfullyDetachASetOfMenuChildren()
        {
            // Arrange 
           Person.Menus.Add(new Menu { ID = 4005, Name = "Butter" });
            Person.Menus.Add(new Menu { ID = 4006, Name = "Cayenne Pepper" });
            Person.Menus.Add(new Menu { ID = 4007, Name = "Cheese" });
           Person.Menus.Add(new Menu { ID = 4008, Name = "Chopped Green Pepper" });
            Repo.Save((Person)Person);
            int initialMenuCount = Person.Menus.Count();

            // Act
            var setToSelect = new HashSet<int> { 4006, 4008 };
            List<Menu> selected = Person.Menus.Where(t => setToSelect.Contains(t.ID)).ToList();
            Controller.DetachAllMenus(Person.ID, selected);
            Person returnedPerson = Repo.GetById(Person.ID);

            // Assert
            Assert.AreEqual(initialMenuCount - 2, returnedPerson.Menus.Count());
        }
         
        [TestMethod]
        public void SuccessfullyDetachtheLastMenuChild()=> BaseDetachTheLastMenuChild(Repo, Controller, Person);


        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachAllMenuChildren() =>
            // Arrange

            // Act

            //Assert
            Assert.Fail();

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWithWarningIfAttachingNullMenuChild() { }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownMenuChildID() { }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild() { }
    }
}

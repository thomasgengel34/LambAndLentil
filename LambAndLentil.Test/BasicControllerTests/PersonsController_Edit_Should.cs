
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace  LambAndLentil.Test.BaseControllerTests
{
    [TestCategory("PersonsController")]
    [TestCategory("Edit")]
    [TestClass]
    internal class PersonsController_Edit_Should : PersonsController_Test_Should
    {
        public PersonsController_Edit_Should()
        {
            Person Person = new Person();
            repo.Save(Person);
        }
          

        [Ignore]   // brought in Ingredient edit methods instead of using this
        [TestMethod]
        [TestCategory("Edit")]
        public void EditPerson()
        {
            // Arrange
            IGenericController<Person> Controller2 = (IGenericController<Person>)(new PersonsController(repo));
            Person pVM = new Person("Kermit", "Frog") { ID = 1492, Description = "test CanEditPerson" };

            // Act  
            ViewResult view1 = (ViewResult)controller.Edit(1492);
            Person p1 = (Person)view1.Model;
            ViewResult view2 = (ViewResult)Controller2.Edit(2);
            Person p2 = (Person)view2.Model;

            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual(1, p1.ID);
            Assert.AreEqual(2, p2.ID);

            Assert.AreEqual("First edited", p1.Name);
            Assert.AreEqual("Old Name 2", p2.Name);
        }



        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedPerson()
        { 
            IGenericController<Person> Controller2 = new PersonsController(repo);
            IGenericController<Person> Controller3 = new PersonsController(repo);
             
            Person.FirstName = "SaveEditedPersonTest";
            Person.LastName = "";
            ActionResult ar2 = Controller2.PostEdit((Person)Person);
            ViewResult view2 = (ViewResult)Controller3.Index();
            ListEntity<Person> ListEntity2 = (ListEntity<Person>)view2.Model;
            Person person3 = (from m in ListEntity2.ListT
                              where m.ID == Person.ID
                              select m).AsQueryable().FirstOrDefault();
             
            Assert.AreEqual("SaveEditedPersonTest ", person3.Name);
        }
          
    }
}


using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestCategory("PersonsController")]
    [TestCategory("Edit")]
    [TestClass]
    public class PersonsController_Edit_Should : PersonsController_Test_Should
    {
        public PersonsController_Edit_Should()
        {
            Person Person = new Person();
            Repo.Save(Person);
        }

        [Ignore]
        [TestMethod]
        public void CorrectPersonsAreBoundInEdit() => Assert.Fail();



        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedPersonWithDescriptionChange()
        {
            // Arrange 
            IGenericController<Person> Controller1 = (IGenericController<Person>)(new PersonsController(Repo));
            IGenericController<Person> Controller2 = (IGenericController<Person>)(new PersonsController(Repo));
            IGenericController<Person> Controller3 = (IGenericController<Person>)(new PersonsController(Repo));
            IGenericController<Person> Controller4 = (IGenericController<Person>)(new PersonsController(Repo));
            IGenericController<Person> Controller5 = (IGenericController<Person>)(new PersonsController(Repo));
            IPerson person = new Person
            {
                FirstName = "0000 test",
                LastName = "",
                Description = "SaveEditedPersonWithDescriptionChange Pre-test",
                ID = 4000
            };


            // Act 
            ActionResult ar1 = Controller1.PostEdit((Person)person);
            ViewResult view1 = (ViewResult)Controller2.Index();
            List<Person> ListEntity = (List<Person>)(((ListEntity<Person>)view1.Model).ListT);
            Person recipeVM = (from m in ListEntity
                               where m.Name == "0000 test"
                               select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("SaveEditedPersonWithDescriptionChange Pre-test", recipeVM.Description);

            // now edit it
            person.ID = recipeVM.ID;
            person.FirstName = "0000 test Edited";
            person.LastName = "";
            person.Description = "SaveEditedPersonWithDescriptionChange Post-test";

            ActionResult ar2 = Controller3.PostEdit((Person)person);
            ViewResult view2 = (ViewResult)Controller4.Index();
            List<Person> ListEntity2 = (List<Person>)(((ListEntity<Person>)view2.Model).ListT);
            Person recipe2 = (from m in ListEntity2
                              where m.Name == "0000 test Edited"
                              select m).AsQueryable().FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited", recipe2.Name);
            Assert.AreEqual("SaveEditedPersonWithDescriptionChange Post-test", recipe2.Description);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnPersonCreationWithNoParameterCtor()
        {
            // Arrange
            DateTime CreationDate = DateTime.Now;

            // Act
            Person recipe = new Person();

            // Assert
            Assert.AreEqual(CreationDate.Date, recipe.CreationDate.Date);
        }


        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnPersonCreationWithDateTimeParameter()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);

            // Act
            Person recipe = new Person(CreationDate);

            // Assert
            Assert.AreEqual(CreationDate, recipe.CreationDate);
        }




        // [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);
            Person person = new Person(CreationDate)
            {
                FirstName = "test SaveTheCreationDateBetweenPostedEdits",
                LastName = "",
                ID = 5000
            };

            TestRepository<Person> repoPerson = new TestRepository<Person>();
            IGenericController<Person> ControllerEdit = (IGenericController<Person>)(new PersonsController(Repo));
            IGenericController<Person> ControllerView = (IGenericController<Person>)(new PersonsController(Repo));
            IGenericController<Person> ControllerDelete = (IGenericController<Person>)(new PersonsController(Repo));

            // Act
            ControllerEdit.PostEdit(person);
            ViewResult view = (ViewResult)ControllerView.Index();
            List<Person> ListEntity = (List<Person>)(((ListEntity<Person>)view.Model).ListT);
            person = (from m in ListEntity
                      where m.ID == 5000
                      select m).AsQueryable().FirstOrDefault();

            DateTime shouldBeSameDate = person.CreationDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);

        }





        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            Person person = new  Person()
            {
                ID = 6000,
                Name = "Test UpdateTheModificationDateBetweenPostedEdits"
            };
            Repo.Save(person);
            BaseUpdateTheModificationDateBetweenPostedEdits(person);
        }


        [Ignore]   // brought in Ingredient edit methods instead of using this
        [TestMethod]
        [TestCategory("Edit")]
        public void EditPerson()
        {
            // Arrange
            IGenericController<Person> Controller2 = (IGenericController<Person>)(new PersonsController(Repo));
            Person pVM = new Person("Kermit", "Frog") { ID = 1492, Description = "test CanEditPerson" };

            // Act  
            ViewResult view1 = (ViewResult)Controller.Edit(1492);
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
            // Arrange 
            IGenericController<Person> Controller2 = (IGenericController<Person>)(new PersonsController(Repo));
            IGenericController<Person> Controller3 = (IGenericController<Person>)(new PersonsController(Repo));


            // Act  
            Person.FirstName = "SaveEditedPersonTest";
            Person.LastName = "";
            ActionResult ar2 = Controller2.PostEdit((Person)Person);
            ViewResult view2 = (ViewResult)Controller3.Index();
            ListEntity<Person> ListEntity2 = (ListEntity<Person>)view2.Model;
            Person person3 = (from m in ListEntity2.ListT
                              where m.ID == Person.ID
                              select m).AsQueryable().FirstOrDefault();

            // Assert
            Assert.AreEqual("SaveEditedPersonTest ", person3.Name);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void CanPostEditPerson()
        {

            // Act 
            Person.FirstName = "Name has been changed";
            ViewResult view = (ViewResult)Controller.PostEdit((Person)Person);

            Person returnedPersonListEntity = Repo.GetById(Person.ID);

            // Assert 
            Assert.IsNotNull(view);
            Assert.AreEqual("Name has been changed", returnedPersonListEntity.FirstName);

        }



        [TestMethod]
        [TestCategory("Edit")]
        public void CannotEditNonexistentPerson()
        { 
            Person result = (Person)((ViewResult)Controller.Edit(8)).ViewData.Model;
          
            Assert.IsNull(result);
        }

    }
}

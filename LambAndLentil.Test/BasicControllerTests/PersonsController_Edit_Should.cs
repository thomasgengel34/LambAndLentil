﻿
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
    public class PersonsController_Edit_Should:PersonsController_Test_Should
    {
        
        [Ignore]
        [TestMethod]
        public void CorrectPersonsAreBoundInEdit()
        {
            Assert.Fail();
        }


        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedPersonWithDescriptionChange()
        {
            // Arrange 
            PersonsController Controller1 = new PersonsController(Repo);
            PersonsController Controller2 = new PersonsController(Repo);
            PersonsController Controller3 = new PersonsController(Repo);
            PersonsController Controller4 = new PersonsController(Repo);
            PersonsController Controller5 = new PersonsController(Repo);
            Person vm = new Person
            {
                FirstName = "0000 test",
                LastName="",
                Description = "SaveEditedPersonWithDescriptionChange Pre-test",
                ID = 4000
            };


            // Act 
            ActionResult ar1 = Controller1.PostEdit(vm);
            ViewResult view1 = Controller2.Index();
            List<Person> ListEntity= (List<Person>)((( ListEntity<Person>)view1.Model).ListT);
            Person recipeVM = (from m in ListEntity
                               where m.Name == "0000 test"
                               select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("SaveEditedPersonWithDescriptionChange Pre-test", recipeVM.Description);

            // now edit it
            vm.ID = recipeVM.ID;
            vm.FirstName = "0000 test Edited";
            vm.LastName = "";
            vm.Description = "SaveEditedPersonWithDescriptionChange Post-test";

            ActionResult ar2 = Controller3.PostEdit(vm);
            ViewResult view2 = Controller4.Index();
            List<Person> ListEntity2 = (List<Person>)((( ListEntity<Person>)view2.Model).ListT);
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

            TestRepository<Person> repoPerson = new TestRepository<Person>(); ;
            PersonsController ControllerEdit = new PersonsController(repoPerson);
            PersonsController ControllerView = new PersonsController(repoPerson);
            PersonsController ControllerDelete = new PersonsController(repoPerson);

            // Act
            ControllerEdit.PostEdit(person);
            ViewResult view = ControllerView.Index();
            List<Person> ListEntity= (List<Person>)((( ListEntity<Person>)view.Model).ListT);
           person = (from m in ListEntity
                        where m.ID == 5000
                     select m).AsQueryable().FirstOrDefault();

            DateTime shouldBeSameDate = person.CreationDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);

        }

        // [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            // Arrange 
            PersonsController ControllerPost = new PersonsController(Repo);
            PersonsController ControllerPost1 = new PersonsController(Repo);
            PersonsController ControllerView = new PersonsController(Repo);
            PersonsController ControllerView1 = new PersonsController(Repo);
            PersonsController ControllerDelete = new PersonsController(Repo);

            Person vm = new Person
            {
                FirstName = "Test UpdateTheModificationDateBetweenPostedEdits",
                LastName="",
                ID = 6000,
                Description = "Unchanged"
            };
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
            ControllerPost.PostEdit(vm);

            ViewResult view = ControllerView.Index();
            List<Person> ListEntity= (List<Person>)((( ListEntity<Person>)view.Model).ListT);
            Person vm2 = (from m in ListEntity
                          where m.ID == 6000
                          select m).AsQueryable().FirstOrDefault();


            vm.Description = "I've been edited to delay a bit";

            ControllerPost1.PostEdit(vm2);


            ViewResult view1 = ControllerView.Index();
            ListEntity= (List<Person>)((( ListEntity<Person>)view1.Model).ListT);
            Person vm3 = (from m in ListEntity
                          where m.ID == 6000
                          select m).AsQueryable().FirstOrDefault();

            DateTime shouldBeSameDate = vm3.CreationDate;
            DateTime shouldBeLaterDate = vm3.ModifiedDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);
            Assert.AreNotEqual(mod, shouldBeLaterDate);

        }


        [Ignore]   // brought in Ingredient edit methods instead of using this
        [TestMethod]
        [TestCategory("Edit")]
        public void EditPerson()
        {
            // Arrange
            var Controller2 = new PersonsController(Repo);
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
            PersonsController indexController = new PersonsController(Repo);
            PersonsController Controller2 = new PersonsController(Repo);
            PersonsController Controller3 = new PersonsController(Repo);


            Person person = new Person
            {
                FirstName = "0000 test",
                LastName = "",
                ID = int.MaxValue - 100,
                Description = "test PersonsControllerShould.SaveEditedPerson"
            };

            // Act 
            ActionResult ar1 = Controller.PostEdit(person);


            // now edit it
            person.FirstName = "0000 test Edited";
            person.LastName = "";
            person.ID = 7777;
            ActionResult ar2 = Controller2.PostEdit(person);
            ViewResult view2 = Controller3.Index();
            ListEntity<Person> ListEntity2 = (ListEntity<Person>)view2.Model;
            Person person3 = (from m in ListEntity2.ListT
                              where m.ID == 7777
                              select m).AsQueryable().FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited ", person3.Name);
            Assert.AreEqual(7777, person3.ID);

        }

        [Ignore]  // look into why this is not working
        [TestMethod]
        [TestCategory("Edit")]
        public void CanPostEditPerson()
        {
            // Arrange
            Person person = new Person
            {
                ID = 1,
                FirstName = "test PersonControllerTest.CanEditPerson",
                LastName = "",
                Description = "test PersonControllerTest.CanEditPerson"
            };
            Repo.Add(person);

            // Act 
            person.FirstName = "Name has been changed";
            Repo.Add(person);
            ViewResult view1 = (ViewResult)Controller.Edit(1);

            Person returnedPersonListEntity = Repo.GetById(1);

            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual("Name has been changed", returnedPersonListEntity.Name);
            Assert.AreEqual(person.Description, returnedPersonListEntity.Description);
            Assert.AreEqual(person.CreationDate, returnedPersonListEntity.CreationDate);
        }



        [TestMethod]
        [TestCategory("Edit")]
        public void CannotEditNonexistentPerson()
        {
            // Arrange

            // Act
            Person result = (Person)((ViewResult)Controller.Edit(8)).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }

    }
}

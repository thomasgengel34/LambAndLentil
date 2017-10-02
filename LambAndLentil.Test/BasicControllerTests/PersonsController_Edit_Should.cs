
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
            PersonsController controller1 = new PersonsController(Repo);
            PersonsController controller2 = new PersonsController(Repo);
            PersonsController controller3 = new PersonsController(Repo);
            PersonsController controller4 = new PersonsController(Repo);
            PersonsController controller5 = new PersonsController(Repo);
            Person vm = new Person
            {
                Name = "0000 test",
                Description = "SaveEditedPersonWithDescriptionChange Pre-test",
                ID = 4000
            };


            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            List<Person> list = (List<Person>)(((ListEntity<Person>)view1.Model).ListT);
            Person recipeVM = (from m in list
                               where m.Name == "0000 test"
                               select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("SaveEditedPersonWithDescriptionChange Pre-test", recipeVM.Description);

            // now edit it
            vm.ID = recipeVM.ID;
            vm.Name = "0000 test Edited";
            vm.Description = "SaveEditedPersonWithDescriptionChange Post-test";

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            List<Person> list2 = (List<Person>)(((ListEntity<Person>)view2.Model).ListT);
            Person recipe2 = (from m in list2
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
                Name = "test SaveTheCreationDateBetweenPostedEdits",
                ID = 5000
            };

            TestRepository<Person> repoPerson = new TestRepository<Person>(); ;
            PersonsController controllerEdit = new PersonsController(repoPerson);
            PersonsController controllerView = new PersonsController(repoPerson);
            PersonsController controllerDelete = new PersonsController(repoPerson);

            // Act
            controllerEdit.PostEdit(person);
            ViewResult view = controllerView.Index();
            List<Person> list = (List<Person>)(((ListEntity<Person>)view.Model).ListT);
           person = (from m in list
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
            PersonsController controllerPost = new PersonsController(Repo);
            PersonsController controllerPost1 = new PersonsController(Repo);
            PersonsController controllerView = new PersonsController(Repo);
            PersonsController controllerView1 = new PersonsController(Repo);
            PersonsController controllerDelete = new PersonsController(Repo);

            Person vm = new Person
            {
                Name = "Test UpdateTheModificationDateBetweenPostedEdits",
                ID = 6000,
                Description = "Unchanged"
            };
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
            controllerPost.PostEdit(vm);

            ViewResult view = controllerView.Index();
            List<Person> list = (List<Person>)(((ListEntity<Person>)view.Model).ListT);
            Person vm2 = (from m in list
                          where m.ID == 6000
                          select m).AsQueryable().FirstOrDefault();


            vm.Description = "I've been edited to delay a bit";

            controllerPost1.PostEdit(vm2);


            ViewResult view1 = controllerView.Index();
            list = (List<Person>)(((ListEntity<Person>)view1.Model).ListT);
            Person vm3 = (from m in list
                          where m.ID == 6000
                          select m).AsQueryable().FirstOrDefault();

            DateTime shouldBeSameDate = vm3.CreationDate;
            DateTime shouldBeLaterDate = vm3.ModifiedDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);
            Assert.AreNotEqual(mod, shouldBeLaterDate);

        }
    }
}

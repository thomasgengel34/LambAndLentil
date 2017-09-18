using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities; 
using LambAndLentil.Tests.Controllers;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("PersonController")]
    public class PersonsControllerShould
    {
        static IRepository<Person> Repo;
        static PersonsController controller;
        static Person personVM;

        public PersonsControllerShould()
        {
            Repo = new TestRepository<Person>();
            controller = new PersonsController(Repo);
            personVM = new Person
            {
                ID = 1000,
                Description = "test PersonControllerShould"
            };
        }


        [TestMethod]
        public void CreateAnPerson()
        {
            // Arrange


            // Act
            ViewResult vr = controller.Create(UIViewType.Create);
            Person vm = (Person)vr.Model;
            vm.Description = "Test.CreateAPerson";
            vm.ID = 33;
            string modelName = vm.Name;

            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(" ", modelName);
        }

       

        [Ignore]
        [TestMethod]
        public void SaveAValidPerson()
        {
            // Arrange 
            Person vm = new Person
            {
                Name = "test",
                Description = "Test.SaveAValidPerson",
                ID = 334
            };

            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit(vm);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;

            // Assert 
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(4, routeValues.Count);
            Assert.AreEqual(UIControllerType.Persons.ToString(), routeValues.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(1).ToString());
            Assert.AreEqual("Persons", routeValues.ElementAt(2).ToString());
            Assert.AreEqual(1.ToString(), routeValues.ElementAt(3).ToString());
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedPersonWithNameChange()
        {
            // Arrange 
            PersonsController controller1 = new PersonsController(Repo);
            PersonsController controller2 = new PersonsController(Repo);
            PersonsController controller3 = new PersonsController(Repo);
            PersonsController controller4 = new PersonsController(Repo);
            PersonsController controller5 = new PersonsController(Repo);
            Person vm = new Person
            {
                FirstName = "0000",
                LastName = "test",
                Description = "Test.SaveEditedPersonWithNameChange",
                ID = 335
            };

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            List<Person> list = (List<Person>)view1.Model;
            Person person = (from m in list
                               where m.Name == "0000 test"
                               select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("0000 test", person.Name);


            // now edit it
            vm.LastName = "test Edited";
            vm.ID = person.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            List<Person> list2 = (List<Person>)view2.Model;
            person = (from m in list2 
                      where m.Name == "0000 test Edited"
                      select m).AsQueryable().FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited", person.Name);
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
                Description = "SaveEditedPersonWithDescriptionChange Pre-test",
                ID = 336
            };

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            List<Person> list = (List<Person>)view1.Model;
            Person person = (from m in list
                               where m.Description == "SaveEditedPersonWithDescriptionChange Pre-test"
                               select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("SaveEditedPersonWithDescriptionChange Pre-test", person.Description);

            // now edit it
            vm.ID = person.ID;
            vm.Description = "SaveEditedPersonWithDescriptionChange Post-test";

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            List<Person> list2 = (List<Person>)view2.Model;
            person = (from m in list2 
                      where m.Description == "SaveEditedPersonWithDescriptionChange Post-test"
                      select m).AsQueryable().FirstOrDefault();


            // Assert
            Assert.AreEqual("0000 test Edited", person.Name);
            Assert.AreEqual("SaveEditedPersonWithDescriptionChange Post-test", person.Description);
        }


        [TestMethod]
        [TestCategory("DeleteConfirmed")]
        public void ActuallyDeleteAPersonFromTheDatabase()
        {
            // Arrange
            personVM.Name = "Test.ActuallyDeleteAPersonfromDB";
            Repo.Save(personVM);

            //Act
            controller.DeleteConfirmed(personVM.ID);
            var deletedItem = (from m in Repo.GetAll()
                               where m.Description == personVM.Name
                               select m).AsQueryable();

            //Assert
            Assert.AreEqual(0, deletedItem.Count());
        }
        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);
            Person personVM = new Person(CreationDate)
            {
                Description = "001 Test ",
                ID = 37
            };
            PersonsController controllerEdit = new PersonsController(Repo);
            PersonsController controllerView = new PersonsController(Repo);
            PersonsController controllerDelete = new PersonsController(Repo);

            // Act
            controllerEdit.PostEdit(personVM);
            ViewResult view = controllerView.Index();
            List<Person> list = (List<Person>)view.Model;
            var result = (from m in list
                          where m.Description == "001 Test "
                          select m).AsQueryable().FirstOrDefault();

            Person person = Mapper.Map<Person>(result);


            DateTime shouldBeSameDate = person.CreationDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);
        }

        [Ignore]
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
                Name = "002 Test Mod",
                Description = "Test.UpdateModDate"
            };
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
            controllerPost.PostEdit(vm);
            ViewResult view = controllerView.Index();
            List<Person> list = (List<Person>)view.Model;
            var result = (from m in list
                          where m.Description == "Test.UpdateModDate"
                          select m).AsQueryable().FirstOrDefault();

            Person person = Mapper.Map<Person>(result);


            person.Description = "I've been edited to delay a bit";

            controllerPost1.PostEdit(person);


            ViewResult view1 = controllerView.Index();
            list = (List<Person>)view1.Model;
            var result1 = (from m in list
                           where m.Description == "I've been edited to delay a bit"
                           select m).AsQueryable().FirstOrDefault();

            person = Mapper.Map<Person>(result1);

            DateTime shouldBeSameDate = person.CreationDate;
            DateTime shouldBeLaterDate = person.ModifiedDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);
            Assert.AreNotEqual(mod, shouldBeLaterDate);
        }

        internal Person GetPerson(IRepository<Person> Repo,  string description)
        {
            PersonsController controller = new PersonsController(Repo);
            Person vm = new Person
            {
                Description = description,
                ID = int.MaxValue
            };
            controller.PostEdit(vm);

            Person personVM = ((from m in Repo.GetAll()
                                  where m.Description == description
                                  select m).AsQueryable()).FirstOrDefault();
            return personVM;
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            PersonsController_Test_Should.ClassCleanup();
        }
    }
}

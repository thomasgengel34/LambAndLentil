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
    public class PersonsControllerShould : BaseControllerTest<Person>
    { 
        static PersonsController Controller;
        static Person Person;

        public PersonsControllerShould()
        {
            Repo = new TestRepository<Person>();
            Controller = new PersonsController(Repo);
            Person = new Person
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
            ViewResult vr = Controller.Create(UIViewType.Create);
            Person vm = (Person)vr.Model;
            vm.Description = "Test.CreateAPerson";
            vm.ID = 33;
            string modelName = vm.Name;

            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual("Newly Created", modelName);
        }

       

      //  [Ignore]
        [TestMethod]
        public void SaveAValidPerson()
        {
            // Arrange 
            Person vm = new Person
            {
                FirstName = "test",
                LastName="",
                Description = "Test.SaveAValidPerson",
                ID = 334
            };

            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.PostEdit(vm);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;

            // Assert 
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(1, routeValues.Count); 
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(0).ToString()); 
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedPersonWithNameChange()
        {
            // Arrange 
            PersonsController Controller1 = new PersonsController(Repo);
            PersonsController Controller2 = new PersonsController(Repo);
            PersonsController Controller3 = new PersonsController(Repo);
            PersonsController Controller4 = new PersonsController(Repo);
            PersonsController Controller5 = new PersonsController(Repo);
            Person vm = new Person
            {
                FirstName = "0000",
                LastName = "test",
                Description = "Test.SaveEditedPersonWithNameChange",
                ID = 335
            };

            // Act 
            ActionResult ar1 = Controller1.PostEdit(vm);
            ViewResult view1 = Controller2.Index();
            List<Person> ListEntity= (List<Person>)view1.Model;
            Person person = (from m in ListEntity
                               where m.Name == "0000 test"
                               select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("0000 test", person.Name);


            // now edit it
            vm.LastName = "test Edited";
            vm.ID = person.ID;
            ActionResult ar2 = Controller3.PostEdit(vm);
            ViewResult view2 = Controller4.Index();
            List<Person> ListEntity2 = (List<Person>)view2.Model;
            person = (from m in ListEntity2 
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
            PersonsController Controller1 = new PersonsController(Repo);
            PersonsController Controller2 = new PersonsController(Repo);
            PersonsController Controller3 = new PersonsController(Repo);
            PersonsController Controller4 = new PersonsController(Repo);
            PersonsController Controller5 = new PersonsController(Repo);
            Person vm = new Person
            {
                Description = "SaveEditedPersonWithDescriptionChange Pre-test",
                ID = 336
            };

            // Act 
            ActionResult ar1 = Controller1.PostEdit(vm);
            ViewResult view1 = Controller2.Index();
            List<Person> ListEntity= (List<Person>)view1.Model;
            Person person = (from m in ListEntity
                               where m.Description == "SaveEditedPersonWithDescriptionChange Pre-test"
                               select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("SaveEditedPersonWithDescriptionChange Pre-test", person.Description);

            // now edit it
            vm.ID = person.ID;
            vm.Description = "SaveEditedPersonWithDescriptionChange Post-test";

            ActionResult ar2 = Controller3.PostEdit(vm);
            ViewResult view2 = Controller4.Index();
            List<Person> ListEntity2 = (List<Person>)view2.Model;
            person = (from m in ListEntity2 
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
            Person person = new Person
            {
                FirstName = "Test.ActuallyDeleteAPersonfromDB",
                LastName = ""
            };
            Repo.Save(person );

            //Act
            Controller.DeleteConfirmed(person.ID);
            var deletedItem = (from m in Repo.GetAll()
                               where m.Description == Person.Name
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
            Person Person = new Person(CreationDate)
            {
                Description = "001 Test ",
                ID = 37
            };
            PersonsController ControllerEdit = new PersonsController(Repo);
            PersonsController ControllerView = new PersonsController(Repo);
            PersonsController ControllerDelete = new PersonsController(Repo);

            // Act
            ControllerEdit.PostEdit(Person);
            ViewResult view = ControllerView.Index();
            List<Person> ListEntity= (List<Person>)view.Model;
            var result = (from m in ListEntity
                          where m.Description == "001 Test "
                          select m).AsQueryable().FirstOrDefault();

            Person person = Mapper.Map<Person>(result);


            DateTime shouldBeSameDate = person.CreationDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);
        }

        
        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            Person.Name = "Test UpdateTheModificationDateBetweenPostedEdits";
            Person.ID = 6000;
            Repo.Save(Person);
            BaseUpdateTheModificationDateBetweenPostedEdits(Repo, Controller, Person);
        }

        internal Person GetPerson(IRepository<Person> Repo,  string description)
        {
            PersonsController Controller = new PersonsController(Repo);
            Person vm = new Person
            {
                Description = description,
                ID = int.MaxValue
            };
            Controller.PostEdit(vm);

            Person Person = ((from m in Repo.GetAll()
                                  where m.Description == description
                                  select m).AsQueryable()).FirstOrDefault();
            return Person;
        } 
    }
}

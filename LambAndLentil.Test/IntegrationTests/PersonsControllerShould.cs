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

namespace IntegrationTests
{

    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("PersonController")]
    public class PersonsControllerShould
    {
        static IRepository<PersonVM> Repo;
        static PersonsController controller;
        static PersonVM personVM;

        public PersonsControllerShould()
        {
            Repo = new TestRepository<PersonVM>();
            controller = new PersonsController(Repo);
            personVM = new PersonVM();
            personVM.ID = 1000;
            personVM.Description = "test PersonControllerShould";
        }


        [TestMethod]
        public void CreateAnPerson()
        {
            // Arrange


            // Act
            ViewResult vr = controller.Create(UIViewType.Create);
            PersonVM vm = (PersonVM)vr.Model;
            vm.Description = "Test.CreateAPerson";
            vm.ID = 33;
            string modelName = vm.Name;

            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual("First Name Last Name", modelName);
        }

        [Ignore]
        [TestMethod]
        public void SaveAValidPerson()
        {
            // Arrange 
            PersonVM vm = new PersonVM();
            vm.Name = "test";
            vm.Description = "Test.SaveAValidPerson";
            vm.ID = 334;

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
            PersonVM vm = new PersonVM();
            vm.FirstName = "0000";
            vm.LastName = "test";
            vm.Description = "Test.SaveEditedPersonWithNameChange";
            vm.ID = 335;

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM<PersonVM> listVM = (ListVM<PersonVM>)view1.Model;
            PersonVM person = (from m in listVM.ListT
                               where m.Name == "0000 test"
                               select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("0000 test", person.Name);


            // now edit it
            vm.LastName = "test Edited";
            vm.ID = person.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM<PersonVM> listVM2 = (ListVM<PersonVM>)view2.Model;
            person = (from m in listVM2.ListT
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
            PersonVM vm = new PersonVM();
            vm.Description = "SaveEditedPersonWithDescriptionChange Pre-test";
            vm.ID = 336;

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM<PersonVM> listVM = (ListVM<PersonVM>)view1.Model;
            PersonVM person = (from m in listVM.ListT
                               where m.Description == "SaveEditedPersonWithDescriptionChange Pre-test"
                               select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("SaveEditedPersonWithDescriptionChange Pre-test", person.Description);

            // now edit it
            vm.ID = person.ID;
            vm.Description = "SaveEditedPersonWithDescriptionChange Post-test";

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM<PersonVM> listVM2 = (ListVM<PersonVM>)view2.Model;
            person = (from m in listVM2.ListT
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
                               where m.Description == personVM.Description
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
            PersonVM personVM = new PersonVM(CreationDate);
            personVM.Description = "001 Test ";
            personVM.ID = 37;
            PersonsController controllerEdit = new PersonsController(Repo);
            PersonsController controllerView = new PersonsController(Repo);
            PersonsController controllerDelete = new PersonsController(Repo);

            // Act
            controllerEdit.PostEdit(personVM);
            ViewResult view = controllerView.Index();
            ListVM<PersonVM> listVM = (ListVM<PersonVM>)view.Model;
            var result = (from m in listVM.ListT
                          where m.Description == "001 Test "
                          select m).AsQueryable().FirstOrDefault();

            PersonVM person = Mapper.Map<PersonVM>(result);


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

            PersonVM vm = new PersonVM();
            vm.Name = "002 Test Mod";
            vm.Description = "Test.UpdateModDate";
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
            controllerPost.PostEdit(vm);
            ViewResult view = controllerView.Index();
            ListVM<PersonVM> listVM = (ListVM<PersonVM>)view.Model;
            var result = (from m in listVM.ListT
                          where m.Description == "Test.UpdateModDate"
                          select m).AsQueryable().FirstOrDefault();

            PersonVM person = Mapper.Map<PersonVM>(result);


            person.Description = "I've been edited to delay a bit";

            controllerPost1.PostEdit(person);


            ViewResult view1 = controllerView.Index();
            listVM = (ListVM<PersonVM>)view1.Model;
            var result1 = (from m in listVM.ListT
                           where m.Description == "I've been edited to delay a bit"
                           select m).AsQueryable().FirstOrDefault();

            person = Mapper.Map<PersonVM>(result1);

            DateTime shouldBeSameDate = person.CreationDate;
            DateTime shouldBeLaterDate = person.ModifiedDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);
            Assert.AreNotEqual(mod, shouldBeLaterDate);
        }

        internal PersonVM GetPerson(IRepository<PersonVM> Repo,  string description)
        {
            PersonsController controller = new PersonsController(Repo);
            PersonVM vm = new PersonVM();
            vm.Description = description;
            vm.ID = int.MaxValue;
            controller.PostEdit(vm);

            PersonVM personVM = ((from m in Repo.GetAll()
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

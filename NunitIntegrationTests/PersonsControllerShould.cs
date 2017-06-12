using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace NunitIntegrationTests
{

    [TestFixture]
    [TestCategory("Integration")]
    public class PersonsControllerShould
    {
        [Test]
        public void CreateAnPerson()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            PersonsController controller = new PersonsController(repo);
            // Act
            ViewResult vr = controller.Create(LambAndLentil.UI.UIViewType.Create);
            PersonVM vm = (PersonVM)vr.Model;
            string modelName = vm.Name;

            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual("First Name Last Name", modelName);

            // Cleanup not needed
        
        }

        [Test]
        public void SaveAValidPerson()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            PersonsController controller = new PersonsController(repo);
            PersonVM vm = new PersonVM();
            vm.Name = "test";
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

            // Clean Up - should run a  delete test to make sure this works after this
            List<Person> persons = repo.Persons.ToList<Person>();
            Person person = persons.Where(m => m.Name == "First Name Last Name").FirstOrDefault();

            // Delete it
            controller.DeleteConfirmed(person.ID);
        }

        [Test]
        [TestCategory("Edit")]
        public void SaveEditedPersonWithNameChange()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            PersonsController controller1 = new PersonsController(repo);
            PersonsController controller2 = new PersonsController(repo);
            PersonsController controller3 = new PersonsController(repo);
            PersonsController controller4 = new PersonsController(repo);
            PersonsController controller5 = new PersonsController(repo);
            PersonVM vm = new PersonVM();
            vm.FirstName = "0000";
            vm.LastName = "test";

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM listVM = (ListVM)view1.Model;
            var result = (from m in listVM.Persons
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            Person ingredient = result.FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("0000 test", ingredient.Name);

            // now edit it
            vm.LastName = "test Edited";
            vm.ID = ingredient.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM listVM2 = (ListVM)view2.Model;
            var result2 = (from m in listVM2.Persons
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable();

            ingredient = result2.FirstOrDefault();


            // Assert
            Assert.AreEqual("0000 test Edited", ingredient.Name);

            // clean up 
            controller5.DeleteConfirmed(vm.ID);
        }


         

        [Test]
        [TestCategory("Edit")]
        public void SaveEditedPersonWithDescriptionChange()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            PersonsController controller1 = new PersonsController(repo);
            PersonsController controller2 = new PersonsController(repo);
            PersonsController controller3 = new PersonsController(repo);
            PersonsController controller4 = new PersonsController(repo);
            PersonsController controller5 = new PersonsController(repo);
            PersonVM vm = new PersonVM(); 
            vm.Description = "SaveEditedPersonWithDescriptionChange Pre-test";


            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM listVM = (ListVM)view1.Model;
            var result = (from m in listVM.Persons
                          where m.Description == "SaveEditedPersonWithDescriptionChange Pre-test"
                          select m).AsQueryable();

            Person person = result.FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("SaveEditedPersonWithDescriptionChange Pre-test", person.Description);

            // now edit it
            vm.ID = person.ID; ;
            vm.Description = "SaveEditedPersonWithDescriptionChange Post-test";

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM listVM2 = (ListVM)view2.Model;
            var result2 = (from m in listVM2.Persons
                           where m.Description == "SaveEditedPersonWithDescriptionChange Post-test"
                           select m).AsQueryable();

            person = result2.FirstOrDefault();


            // Assert
            //Assert.AreEqual("0000 test Edited", person.Name);
            Assert.AreEqual("SaveEditedPersonWithDescriptionChange Post-test", person.Description);

            // clean up 
            controller5.DeleteConfirmed(vm.ID);
        }


        [Test]
        [TestCategory("DeleteConfirmed")]
        public void ActuallyDeleteAPersonFromTheDatabase()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            PersonsController editController = new PersonsController(repo);
            PersonsController indexController = new PersonsController(repo);
            PersonsController deleteController = new PersonsController(repo);
            PersonVM vm = new PersonVM(); 
            ActionResult ar = editController.PostEdit(vm);
            ViewResult view = indexController.Index();
            ListVM listVM = (ListVM)view.Model;
            var result = (from m in listVM.Persons
                          where m.Name == vm.Name
                          select m).AsQueryable();
            Person item = result.FirstOrDefault();

            //Act
            deleteController.DeleteConfirmed(item.ID);
            var deletedItem = (from m in repo.Persons
                               where m.Name == vm.Name
                               select m).AsQueryable();

            //Assert
            Assert.AreEqual(0, deletedItem.Count());
        }

        [Test]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);
            PersonVM personVM = new PersonVM(CreationDate);
            personVM.Description = "001 Test ";

            EFRepository repo = new EFRepository(); ;
            PersonsController controllerEdit = new PersonsController(repo);
            PersonsController controllerView = new PersonsController(repo);
            PersonsController controllerDelete = new PersonsController(repo);

            // Act
            controllerEdit.PostEdit(personVM);
            ViewResult view = controllerView.Index();
            ListVM listVM = (ListVM)view.Model;
            var result = (from m in listVM.Persons
                          where m.Description == "001 Test "
                          select m).AsQueryable();

            Person person = result.FirstOrDefault();

            DateTime shouldBeSameDate = person.CreationDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);

            // Cleanup
            controllerDelete.DeleteConfirmed(person.ID);
        }
    }
}

using AutoMapper;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MsTestIntegrationTests
{

    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("PersonController")]
    public class PersonsControllerShould
    {
        [TestMethod]
        public void CreateAnPerson()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            PersonsController controller = new PersonsController(repo);
            // Act
            ViewResult vr = controller.Create(LambAndLentil.UI.UIViewType.Create);
            PersonVM vm = (PersonVM)vr.Model;
            vm.Description = "Test.CreateAPerson";
            string modelName = vm.Name;

            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual("First Name Last Name", modelName);

            // Cleanup not needed

        }

        [TestMethod]
        public void SaveAValidPerson()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            PersonsController controller = new PersonsController(repo);
            PersonVM vm = new PersonVM();
            vm.Name = "test";
            vm.Description = "Test.SaveAValidPerson";
            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit(vm);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;

            try
            {
                // Assert 
                Assert.AreEqual("alert-success", adr.AlertClass);
                Assert.AreEqual(4, routeValues.Count);
                Assert.AreEqual(UIControllerType.Persons.ToString(), routeValues.ElementAt(0).ToString());
                Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(1).ToString());
                Assert.AreEqual("Persons", routeValues.ElementAt(2).ToString());
                Assert.AreEqual(1.ToString(), routeValues.ElementAt(3).ToString());
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                // Clean Up - should run a  delete test to make sure this works after this
                List<Person> persons = repo.Persons.ToList<Person>();
                Person person = persons.Where(m => m.Name == "First Name Last Name").FirstOrDefault();

                // Delete it
                controller.DeleteConfirmed(person.ID);
            }
        }

        [TestMethod]
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
            vm.Description = "Test.SaveEditedPersonWithNameChange";

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM listVM = (ListVM)view1.Model;
            var result = (from m in listVM.Persons
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            Person person = result.FirstOrDefault();

            try
            {
                // verify initial value:
                Assert.AreEqual("0000 test", person.Name);
            }
            catch (Exception)
            {
                controller5.DeleteConfirmed(vm.ID);
                throw;
            }


            // now edit it
            vm.LastName = "test Edited";
            vm.ID = person.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM listVM2 = (ListVM)view2.Model;
            var result2 = (from m in listVM2.Persons
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable();

            person = result2.FirstOrDefault();

            try
            {
                // Assert
                Assert.AreEqual("0000 test Edited", person.Name);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // clean up 
                controller5.DeleteConfirmed(vm.ID);
            }
        }

        [TestMethod]
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
            try
            {
                // verify initial value:
                Assert.AreEqual("SaveEditedPersonWithDescriptionChange Pre-test", person.Description);
            }
            catch (Exception)
            {
                controller5.DeleteConfirmed(vm.ID);
                throw;
            }


            // now edit it
            vm.ID = person.ID;
            vm.Description = "SaveEditedPersonWithDescriptionChange Post-test";

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM listVM2 = (ListVM)view2.Model;
            var result2 = (from m in listVM2.Persons
                           where m.Description == "SaveEditedPersonWithDescriptionChange Post-test"
                           select m).AsQueryable();

            person = result2.FirstOrDefault();

            try
            {
                // Assert
                //Assert.AreEqual("0000 test Edited", person.Name);
                Assert.AreEqual("SaveEditedPersonWithDescriptionChange Post-test", person.Description);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // clean up 
                controller5.DeleteConfirmed(vm.ID);
            }
        }


        [TestMethod]
        [TestCategory("DeleteConfirmed")]
        public void ActuallyDeleteAPersonFromTheDatabase()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            PersonsController editController = new PersonsController(repo);
            PersonsController indexController = new PersonsController(repo);
            PersonsController deleteController = new PersonsController(repo);
            PersonVM vm = new PersonVM();
            vm.Description = "Test.ActuallyDeleteAPersonfromDB";
            ActionResult ar = editController.PostEdit(vm);
            ViewResult view = indexController.Index();
            ListVM listVM = (ListVM)view.Model;
            var result = (from m in listVM.Persons
                          where m.Description == vm.Description
                          select m).AsQueryable();
            Person item = result.FirstOrDefault();

            //Act
            deleteController.DeleteConfirmed(item.ID);
            var deletedItem = (from m in repo.Persons
                               where m.Description == vm.Description
                               select m).AsQueryable();

            //Assert
            Assert.AreEqual(0, deletedItem.Count());

            // Clean up not needed

        }

        [TestMethod]
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
            try
            {
                // Assert
                Assert.AreEqual(CreationDate, shouldBeSameDate);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Cleanup
                controllerDelete.DeleteConfirmed(person.ID);
            }
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            // Arrange
            EFRepository repo = new EFRepository();
            PersonsController controllerPost = new PersonsController(repo);
            PersonsController controllerPost1 = new PersonsController(repo);
            PersonsController controllerView = new PersonsController(repo);
            PersonsController controllerView1 = new PersonsController(repo);
            PersonsController controllerDelete = new PersonsController(repo);

            PersonVM vm = new PersonVM();
            vm.Name = "002 Test Mod";
            vm.Description = "Test.UpdateModDate";
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
            controllerPost.PostEdit(vm);
            ViewResult view = controllerView.Index();
            ListVM listVM = (ListVM)view.Model;
            var result = (from m in listVM.Persons
                          where m.Description == "Test.UpdateModDate"
                          select m).AsQueryable();

            Person person = result.FirstOrDefault();
            vm = Mapper.Map<Person, PersonVM>(person);

            vm.Description = "I've been edited to delay a bit";

            controllerPost1.PostEdit(vm);


            ViewResult view1 = controllerView.Index();
            listVM = (ListVM)view1.Model;
            var result1 = (from m in listVM.Persons
                           where m.Description == "I've been edited to delay a bit"
                           select m).AsQueryable();

            person = result1.FirstOrDefault();

            DateTime shouldBeSameDate = person.CreationDate;
            DateTime shouldBeLaterDate = person.ModifiedDate;
            try
            {
                // Assert
                Assert.AreEqual(CreationDate, shouldBeSameDate);
                Assert.AreNotEqual(mod, shouldBeLaterDate);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                // Cleanup
                controllerDelete.DeleteConfirmed(person.ID);
            }
        }
    }
}

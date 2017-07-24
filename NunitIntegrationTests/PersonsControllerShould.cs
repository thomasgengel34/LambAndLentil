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
             EFRepository<Person,PersonVM>  repo = new  EFRepository<Person,PersonVM>(); ;
            PersonsController controller = new PersonsController();
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
             EFRepository<Person,PersonVM> repo = new  EFRepository<Person,PersonVM>(); ;
            PersonsController controller = new PersonsController();
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
                Person person = repo.GetAll().Where(m => m.Name == "First Name Last Name").FirstOrDefault(); 
                controller.DeleteConfirmed(person.ID);
            }
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedPersonWithNameChange()
        {
            // Arrange
             EFRepository<Person,PersonVM> repo = new  EFRepository<Person,PersonVM>(); ;
            PersonsController controller1 = new PersonsController();
            PersonsController controller2 = new PersonsController();
            PersonsController controller3 = new PersonsController();
            PersonsController controller4 = new PersonsController();
            PersonsController controller5 = new PersonsController();
            PersonVM vm = new PersonVM();
            vm.FirstName = "0000";
            vm.LastName = "test";
            vm.Description = "Test.SaveEditedPersonWithNameChange";

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
           ListVM<Person,PersonVM>  listVM = ( ListVM<Person, PersonVM> )view1.Model;
            var result = (from m in listVM.Entities
                          where m.Name == "0000 test"
                          select m).AsQueryable().FirstOrDefault();

            PersonVM person = Mapper.Map<Person,PersonVM>(result);

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
          ListVM<Person, PersonVM>  listVM2 =  (ListVM<Person, PersonVM> )view2.Model;
            var result2 = (from m in listVM2.Entities
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable().FirstOrDefault();

            person = Mapper.Map<Person,PersonVM>(result2);

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
             EFRepository<Person,PersonVM> repo = new  EFRepository<Person,PersonVM>(); ;
            PersonsController controller1 = new PersonsController();
            PersonsController controller2 = new PersonsController();
            PersonsController controller3 = new PersonsController();
            PersonsController controller4 = new PersonsController();
            PersonsController controller5 = new PersonsController();
            PersonVM vm = new PersonVM();
            vm.Description = "SaveEditedPersonWithDescriptionChange Pre-test";


            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM<Person, PersonVM>  listVM =  (ListVM<Person, PersonVM> )view1.Model;
            var result = (from m in listVM.Entities
                          where m.Description == "SaveEditedPersonWithDescriptionChange Pre-test"
                          select m).AsQueryable().FirstOrDefault();

           
            PersonVM person = Mapper.Map<Person, PersonVM>(result);
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
           ListVM<Person, PersonVM>  listVM2 =  (ListVM<Person, PersonVM> )view2.Model;
            var result2 = (from m in listVM2.Entities
                           where m.Description == "SaveEditedPersonWithDescriptionChange Post-test"
                           select m).AsQueryable().FirstOrDefault();
           person = Mapper.Map<Person, PersonVM>(result2); 

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
             EFRepository<Person,PersonVM> repo = new  EFRepository<Person,PersonVM>(); ;
            PersonsController editController = new PersonsController();
            PersonsController indexController = new PersonsController();
            PersonsController deleteController = new PersonsController();
            PersonVM vm = new PersonVM();
            vm.Description = "Test.ActuallyDeleteAPersonfromDB";
            ActionResult ar = editController.PostEdit(vm);
            ViewResult view = indexController.Index();
             ListVM<Person, PersonVM>  listVM =  (ListVM<Person, PersonVM> )view.Model;
            var result = (from m in listVM.Entities
                          where m.Description == vm.Description
                          select m).AsQueryable().FirstOrDefault();
            PersonVM item =   Mapper.Map<Person, PersonVM>(result); ;

            //Act
            deleteController.DeleteConfirmed(item.ID);
            var deletedItem = (from m in repo.GetAll() 
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

             EFRepository<Person,PersonVM> repo = new  EFRepository<Person,PersonVM>(); ;
            PersonsController controllerEdit = new PersonsController();
            PersonsController controllerView = new PersonsController();
            PersonsController controllerDelete = new PersonsController();

            // Act
            controllerEdit.PostEdit(personVM);
            ViewResult view = controllerView.Index();
           ListVM<Person, PersonVM>  listVM =  (ListVM<Person, PersonVM> )view.Model;
            var result = (from m in listVM.Entities
                          where m.Description == "001 Test "
                          select m).AsQueryable().FirstOrDefault();

           PersonVM person = Mapper.Map<Person, PersonVM>(result);
         

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
             EFRepository<Person,PersonVM> repo = new  EFRepository<Person,PersonVM>();
            PersonsController controllerPost = new PersonsController();
            PersonsController controllerPost1 = new PersonsController();
            PersonsController controllerView = new PersonsController();
            PersonsController controllerView1 = new PersonsController();
            PersonsController controllerDelete = new PersonsController();

            PersonVM vm = new PersonVM();
            vm.Name = "002 Test Mod";
            vm.Description = "Test.UpdateModDate";
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
            controllerPost.PostEdit(vm);
            ViewResult view = controllerView.Index();
            ListVM<Person, PersonVM>  listVM = (ListVM<Person, PersonVM> )view.Model;
            var result = (from m in listVM.Entities
                          where m.Description == "Test.UpdateModDate"
                          select m).AsQueryable().FirstOrDefault();

            PersonVM person = Mapper.Map<Person, PersonVM>(result);
            

           person.Description = "I've been edited to delay a bit";

            controllerPost1.PostEdit(person);


            ViewResult view1 = controllerView.Index();
            listVM =  (ListVM<Person,PersonVM> )view1.Model;
            var result1 = (from m in listVM.Entities
                           where m.Description == "I've been edited to delay a bit"
                           select m).AsQueryable().FirstOrDefault(); 
             
            person = Mapper.Map<Person, PersonVM>(result1);

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

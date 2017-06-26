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
    [TestCategory("MenusController")]
    public class MenusControllerShould
    {
        [TestMethod]
        public void CreateAnMenu()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            MenusController controller = new MenusController(repo);
            // Act
            ViewResult vr = controller.Create(LambAndLentil.UI.UIViewType.Create);
            MenuVM vm = (MenuVM)vr.Model;
            string modelName = vm.Name;

            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created");
            Assert.AreEqual(DayOfWeek.Sunday, vm.DayOfWeek);
        }

        [TestMethod]
        public void SaveAValidMenu()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            MenusController controller = new MenusController(repo);
            MenuVM vm = new MenuVM();
            vm.Name = "test";
            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit(vm);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;

            try
            {
                // Assert 
                Assert.AreEqual("alert-success", adr.AlertClass);
                Assert.AreEqual(4, routeValues.Count);
                Assert.AreEqual(UIControllerType.Menus.ToString(), routeValues.ElementAt(0).ToString());
                Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(1).ToString());
                Assert.AreEqual("Menus", routeValues.ElementAt(2).ToString());
                Assert.AreEqual(1.ToString(), routeValues.ElementAt(3).ToString());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Clean Up - should run a  delete test to make sure this works 
                List<Menu> menus = repo.Menus.ToList<Menu>();
                Menu menu = menus.Where(m => m.Name == "test").FirstOrDefault();

                // Delete it
                controller.DeleteConfirmed(menu.ID);
            }
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedMenuWithNameChange()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            MenusController controller1 = new MenusController(repo);
            MenusController controller2 = new MenusController(repo);
            MenusController controller3 = new MenusController(repo);
            MenusController controller4 = new MenusController(repo);
            MenusController controller5 = new MenusController(repo);
            MenuVM vm = new MenuVM();
            vm.Name = "0000 test";

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM listVM = (ListVM)view1.Model;
            var result = (from m in listVM.Menus
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            Menu menu = result.FirstOrDefault();
            try
            {
                // verify initial value:
                Assert.AreEqual("0000 test", menu.Name);
            }
            catch (Exception)
            {
                throw;
            }
            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = menu.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM listVM2 = (ListVM)view2.Model;
            var result2 = (from m in listVM2.Menus
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable();

            menu = result2.FirstOrDefault();

            try
            {
                // Assert
                Assert.AreEqual("0000 test Edited", menu.Name);
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
        public void SaveEditedMenuWithNameAndDayOfWeekChange()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            MenusController controller1 = new MenusController(repo);
            MenusController controller2 = new MenusController(repo);
            MenusController controller3 = new MenusController(repo);
            MenusController controller4 = new MenusController(repo);
            MenusController controller5 = new MenusController(repo);
            MenuVM vm = new MenuVM();
            vm.Name = "0000 test";


            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM listVM = (ListVM)view1.Model;
            var result = (from m in listVM.Menus
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            Menu menu = result.FirstOrDefault();
            try
            {
                // verify initial value:
                Assert.AreEqual("0000 test", menu.Name);
            }
            catch (Exception)
            {
                controller3.DeleteConfirmed(menu.ID);
                throw;
            }


            // now edit it
            vm.ID = menu.ID;
            vm.Name = "0000 test Edited";
            vm.DayOfWeek = DayOfWeek.Friday;

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM listVM2 = (ListVM)view2.Model;
            var result2 = (from m in listVM2.Menus
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable();

            menu = result2.FirstOrDefault();

            try
            {
                // Assert
                Assert.AreEqual("0000 test Edited", menu.Name);
                Assert.AreEqual(DayOfWeek.Friday, menu.DayOfWeek);
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
        public void ActuallyDeleteAMenuFromTheDatabase()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            MenusController editController = new MenusController(repo);
            MenusController indexController = new MenusController(repo);
            MenusController deleteController = new MenusController(repo);
            MenuVM vm = new MenuVM();
            vm.Name = "0000" + new Guid().ToString();
            ActionResult ar = editController.PostEdit(vm);
            ViewResult view = indexController.Index();
            ListVM listVM = (ListVM)view.Model;
            var result = (from m in listVM.Menus
                          where m.Name == vm.Name
                          select m).AsQueryable();
            Menu item = result.FirstOrDefault();

            //Act
            deleteController.DeleteConfirmed(item.ID);
            var deletedItem = (from m in repo.Menus
                               where m.Name == vm.Name
                               select m).AsQueryable();

            //Assert
            Assert.AreEqual(0, deletedItem.Count());
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedMenuWithDescriptionChange()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            MenusController controller1 = new MenusController(repo);
            MenusController controller2 = new MenusController(repo);
            MenusController controller3 = new MenusController(repo);
            MenusController controller4 = new MenusController(repo);
            MenusController controller5 = new MenusController(repo);
            MenuVM vm = new MenuVM();
            vm.Name = "0000 test";
            vm.Description = "SaveEditedMenuWithDescriptionChange Pre-test";


            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM listVM = (ListVM)view1.Model;
            var result = (from m in listVM.Menus
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            Menu menu = result.FirstOrDefault();
            try
            {
                // verify initial value:
                Assert.AreEqual("SaveEditedMenuWithDescriptionChange Pre-test", menu.Description);
            }
            catch (Exception)
            {
                controller4.DeleteConfirmed(menu.ID);
                throw;
            }

            // now edit it
            vm.ID = menu.ID;
            vm.Name = "0000 test Edited";
            vm.Description = "SaveEditedMenuWithDescriptionChange Post-test";

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM listVM2 = (ListVM)view2.Model;
            var result2 = (from m in listVM2.Menus
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable();

            menu = result2.FirstOrDefault();

            try
            {
                // Assert
                Assert.AreEqual("0000 test Edited", menu.Name);
                Assert.AreEqual("SaveEditedMenuWithDescriptionChange Post-test", menu.Description);
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
        public void SaveTheCreationDateOnMenuCreationWithNoParameterCtor()
        {
            // Arrange
            DateTime CreationDate = DateTime.Now;

            // Act
            Menu menu = new Menu();

            // Assert
            Assert.AreEqual(CreationDate.Date, menu.CreationDate.Date);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void ShouldSaveTheCreationDateOnMenuCreationWithDateTimeParameter()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);

            // Act
            Menu menu = new Menu(CreationDate);

            // Assert
            Assert.AreEqual(CreationDate, menu.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnMenuVMCreationWithNoParameterCtor()
        {
            // Arrange
            DateTime CreationDate = DateTime.Now;

            // Act
            MenuVM vm = new MenuVM();

            // Assert
            Assert.AreEqual(CreationDate.Date, vm.CreationDate.Date);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnMenuVMCreationWithDateTimeParameter()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);

            // Act
            MenuVM vm = new MenuVM(CreationDate);

            // Assert
            Assert.AreEqual(CreationDate, vm.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);
            MenuVM vm = new MenuVM(CreationDate);
            vm.Name = "001 Test ";

            EFRepository repo = new EFRepository();
            MenusController controllerEdit = new MenusController(repo);
            MenusController controllerView = new MenusController(repo);
            MenusController controllerDelete = new MenusController(repo);

            // Act
            controllerEdit.PostEdit(vm);
            ViewResult view = controllerView.Index();
            ListVM listVM = (ListVM)view.Model;
            var result = (from m in listVM.Menus
                          where m.Name == "001 Test "
                          select m).AsQueryable();

            Menu menu = result.FirstOrDefault();

            DateTime shouldBeSameDate = menu.CreationDate;
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
                controllerDelete.DeleteConfirmed(menu.ID);
            }
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            // Arrange
            EFRepository repo = new EFRepository();
            MenusController controllerPost = new MenusController(repo);
            MenusController controllerPost1 = new MenusController(repo);
            MenusController controllerView = new MenusController(repo);
            MenusController controllerDelete = new MenusController(repo);

            MenuVM vm = new MenuVM();
            vm.Name = "002 Test Mod";
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
            controllerPost.PostEdit(vm);
            ViewResult view = controllerView.Index();
            ListVM listVM = (ListVM)view.Model;
            var result = (from m in listVM.Menus
                          where m.Name == "002 Test Mod"
                          select m).AsQueryable();
            Menu menu = (Menu)result.FirstOrDefault();
            vm = Mapper.Map<Menu, MenuVM>(menu);
            vm.Description = "I've been edited to delay a bit";
            controllerPost1.PostEdit(vm);

            ViewResult view1 = controllerView.Index();
            listVM = (ListVM)view1.Model;
            var result1 = (from m in listVM.Menus
                           where m.Name == "002 Test Mod"
                           select m).AsQueryable();

            menu = result1.FirstOrDefault();

            DateTime shouldBeSameDate = menu.CreationDate;
            DateTime shouldBeLaterDate = menu.ModifiedDate;
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
                controllerDelete.DeleteConfirmed(menu.ID);
            }
        }
    }
}

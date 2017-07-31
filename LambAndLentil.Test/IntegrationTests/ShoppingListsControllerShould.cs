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

namespace IntegrationTests
{

    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("ShoppingListsController")]
    public class ShoppingListsControllerShould
    {
        [TestMethod]
        public void CreateAnShoppingList()
        {
            // Arrange
            EFRepository<ShoppingList, ShoppingListVM> repo = new EFRepository<ShoppingList, ShoppingListVM>(); ;
            ShoppingListsController controller = new ShoppingListsController(repo);
            // Act
            ViewResult vr = controller.Create(LambAndLentil.UI.UIViewType.Create);
            ListVM<ShoppingList, ShoppingListVM> vm = (ListVM<ShoppingList, ShoppingListVM>)vr.Model;
            string modelName = vm.Name;

            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created");
        }

        [TestMethod]
        public void SaveAValidShoppingList()
        {
            // Arrange
            EFRepository<ShoppingList, ShoppingListVM> repo = new EFRepository<ShoppingList, ShoppingListVM>(); ;
            ShoppingListsController controller = new ShoppingListsController(repo);
            ShoppingListVM vm = new ShoppingListVM();
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
                Assert.AreEqual(UIControllerType.ShoppingLists.ToString(), routeValues.ElementAt(0).ToString());
                Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(1).ToString());
                Assert.AreEqual("ShoppingLists", routeValues.ElementAt(2).ToString());
                Assert.AreEqual(1.ToString(), routeValues.ElementAt(3).ToString());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Clean Up - should run a  delete test to make sure this works  
                ShoppingList shoppingList = repo.GetAll().Where(m => m.Name == "test").FirstOrDefault();
                controller.DeleteConfirmed(shoppingList.ID);
            }
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedShoppingListWithNameChange()
        {
            // Arrange
            EFRepository<ShoppingList, ShoppingListVM> repo = new EFRepository<ShoppingList, ShoppingListVM>(); ;
            ShoppingListsController controller1 = new ShoppingListsController(repo);
            ShoppingListsController controller2 = new ShoppingListsController(repo);
            ShoppingListsController controller3 = new ShoppingListsController(repo);
            ShoppingListsController controller4 = new ShoppingListsController(repo);
            ShoppingListsController controller5 = new ShoppingListsController(repo);
            ShoppingListVM vm = new ShoppingListVM();
            vm.Name = "0000 test";

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            IEnumerable<ShoppingListVM> listVM = (IEnumerable<ShoppingListVM>)view1.Model;
            var result = (from m in listVM
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            ShoppingListVM item = result.FirstOrDefault();
            try
            {
                // verify initial value:
                Assert.AreEqual("0000 test", item.Name);
            }
            catch (Exception)
            {
                controller5.DeleteConfirmed(vm.ID);
                throw;
            }


            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = item.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM<ShoppingList, ShoppingListVM> listVM2 = (ListVM<ShoppingList, ShoppingListVM>)view2.Model;
            var result2 = (from m in listVM2.Entities
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable();

            ShoppingList sl = result2.FirstOrDefault();
            item = Mapper.Map<ShoppingList, ShoppingListVM>(sl);

            try
            {
                // Assert
                Assert.AreEqual("0000 test Edited", item.Name);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                // clean up
                // TO DO: write a test to make sure this happens.
                controller5.DeleteConfirmed(vm.ID);
            }
        }




        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedShoppingListWithDescriptionChange()
        {
            // Arrange
            EFRepository<ShoppingList, ShoppingListVM> repo = new EFRepository<ShoppingList, ShoppingListVM>(); ;
            ShoppingListsController controller1 = new ShoppingListsController(repo);
            ShoppingListsController controller2 = new ShoppingListsController(repo);
            ShoppingListsController controller3 = new ShoppingListsController(repo);
            ShoppingListsController controller4 = new ShoppingListsController(repo);
            ShoppingListsController controller5 = new ShoppingListsController(repo);
            ShoppingListVM vm = new ShoppingListVM();
            vm.Name = "0000 test";
            vm.Description = "SaveEditedShoppingListWithDescriptionChange Pre-test";


            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM<ShoppingList, ShoppingListVM> listVM = (ListVM<ShoppingList, ShoppingListVM>)view1.Model;
            var result = (from m in listVM.Entities
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            ShoppingList shoppingList = result.FirstOrDefault();

            try
            {
                // verify initial value:
                Assert.AreEqual("SaveEditedShoppingListWithDescriptionChange Pre-test", shoppingList.Description);
            }
            catch (Exception)
            {
                controller5.DeleteConfirmed(vm.ID);
                throw;
            }


            // now edit it
            vm.ID = shoppingList.ID;
            vm.Name = "0000 test Edited";
            vm.Description = "SaveEditedShoppingListWithDescriptionChange Post-test";

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM<ShoppingList, ShoppingListVM> listVM2 = (ListVM<ShoppingList, ShoppingListVM>)view2.Model;
            var result2 = (from m in listVM2.Entities
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable();

            shoppingList = result2.FirstOrDefault();

            try
            {
                // Assert
                Assert.AreEqual("0000 test Edited", shoppingList.Name);
                Assert.AreEqual("SaveEditedShoppingListWithDescriptionChange Post-test", shoppingList.Description);

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
        public void ActuallyDeleteAShoppingListFromTheDatabase()
        {
            // Arrange
            EFRepository<ShoppingList, ShoppingListVM> repo = new EFRepository<ShoppingList, ShoppingListVM>(); ;
            ShoppingListsController editController = new ShoppingListsController(repo);
            ShoppingListsController indexController = new ShoppingListsController(repo);
            ShoppingListsController deleteController = new ShoppingListsController(repo);
            ShoppingListVM vm = new ShoppingListVM();
            vm.Name = "0000" + new Guid().ToString();
            ActionResult ar = editController.PostEdit(vm);
            ViewResult view = indexController.Index();
            ListVM<ShoppingList, ShoppingListVM> listVM = (ListVM<ShoppingList, ShoppingListVM>)view.Model;
            var result = (from m in listVM.Entities
                          where m.Name == vm.Name
                          select m).AsQueryable();


            ShoppingList sl = result.FirstOrDefault();
            ShoppingListVM item = Mapper.Map<ShoppingList, ShoppingListVM>(sl);

            //Act
            deleteController.DeleteConfirmed(item.ID);
            var deletedItem = (from m in repo.GetAll()
                               where m.Name == vm.Name
                               select m).AsQueryable();

            //Assert
            Assert.AreEqual(0, deletedItem.Count());
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);
            ShoppingListVM shoppingListVM = new ShoppingListVM(CreationDate);
            shoppingListVM.Name = "001 Test ";

            EFRepository<ShoppingList, ShoppingListVM> repo = new EFRepository<ShoppingList, ShoppingListVM>(); ;
            ShoppingListsController controllerEdit = new ShoppingListsController(repo);
            ShoppingListsController controllerView = new ShoppingListsController(repo);
            ShoppingListsController controllerDelete = new ShoppingListsController(repo);

            // Act
            controllerEdit.PostEdit(shoppingListVM);
            ViewResult view = controllerView.Index();
            ShoppingListVM listVM = (ShoppingListVM)view.Model;
            var result = (from m in listVM.ShoppingLists
                          where m.Name == "001 Test "
                          select m).AsQueryable();

            ShoppingList shoppingList = result.FirstOrDefault();

            DateTime shouldBeSameDate = shoppingList.CreationDate;
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
                controllerDelete.DeleteConfirmed(shoppingList.ID);
            }
        }


        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            // Arrange
            EFRepository<ShoppingList, ShoppingListVM> repo = new EFRepository<ShoppingList, ShoppingListVM>();
            ShoppingListsController controllerPost = new ShoppingListsController(repo);
            ShoppingListsController controllerPost1 = new ShoppingListsController(repo);
            ShoppingListsController controllerView = new ShoppingListsController(repo);
            ShoppingListsController controllerView1 = new ShoppingListsController(repo);
            ShoppingListsController controllerDelete = new ShoppingListsController(repo);

            ShoppingListVM vm = new ShoppingListVM();
            vm.Name = "002 Test Mod";
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
            controllerPost.PostEdit(vm);

            ViewResult view = controllerView.Index();
            ShoppingListVM vm2 = (ShoppingListVM)view.Model;
            var result = (from m in vm2.ShoppingLists
                          where m.Name == "002 Test Mod"
                          select m).AsQueryable();

            vm2.Description = "I've been edited to delay a bit";

            controllerPost1.PostEdit(vm2);


            ViewResult view1 = controllerView.Index();
            ShoppingList listVM = (ShoppingList)view1.Model;
            var result1 = (from m in listVM.ShoppingLists
                           where m.Name == "002 Test Mod"
                           select m).AsQueryable();

            ShoppingList item = result1.FirstOrDefault();

            DateTime shouldBeSameDate = item.CreationDate;
            DateTime shouldBeLaterDate = item.ModifiedDate;

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
                controllerDelete.DeleteConfirmed(item.ID);
            }
        }
    }
}

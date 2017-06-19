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
    [TestCategory("ShoppingListController")]
    public class ShoppingListsControllerShould
    {
        [TestMethod]
        public void CreateAnShoppingList()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            ShoppingListsController controller = new ShoppingListsController(repo);
            // Act
            ViewResult vr = controller.Create(LambAndLentil.UI.UIViewType.Create);
            ShoppingListVM vm = (ShoppingListVM)vr.Model;
            string modelName = vm.Name;

            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created"); 
        }

        [TestMethod]
        public void SaveAValidShoppingList()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            ShoppingListsController controller = new ShoppingListsController(repo);
            ShoppingListVM vm = new ShoppingListVM();
            vm.Name = "test";
            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit(vm);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;


            // Assert 
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(4, routeValues.Count);
            Assert.AreEqual(UIControllerType.ShoppingLists.ToString(), routeValues.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(1).ToString());
            Assert.AreEqual("ShoppingLists", routeValues.ElementAt(2).ToString());
            Assert.AreEqual(1.ToString(), routeValues.ElementAt(3).ToString());

            // Clean Up - should run a  delete test to make sure this works 
            List<ShoppingList> shoppingLists = repo.ShoppingLists.ToList<ShoppingList>();
            ShoppingList shoppingList = shoppingLists.Where(m => m.Name == "test").FirstOrDefault();

            // Delete it
            controller.DeleteConfirmed(shoppingList.ID);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedShoppingListWithNameChange()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
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
            ListVM listVM = (ListVM)view1.Model;
            var result = (from m in listVM.ShoppingLists
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            ShoppingList ingredient = result.FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("0000 test", ingredient.Name);

            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = ingredient.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM listVM2 = (ListVM)view2.Model;
            var result2 = (from m in listVM2.ShoppingLists
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable();

            ingredient = result2.FirstOrDefault();


            // Assert
            Assert.AreEqual("0000 test Edited", ingredient.Name);

            // clean up
            // TO DO: write a test to make sure this happens.
            controller5.DeleteConfirmed(vm.ID);
        }


         

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedShoppingListWithDescriptionChange()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
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
            ListVM listVM = (ListVM)view1.Model;
            var result = (from m in listVM.ShoppingLists
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            ShoppingList shoppingList = result.FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("SaveEditedShoppingListWithDescriptionChange Pre-test", shoppingList.Description);

            // now edit it
            vm.ID = shoppingList.ID;
            vm.Name = "0000 test Edited";
            vm.Description = "SaveEditedShoppingListWithDescriptionChange Post-test";

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM listVM2 = (ListVM)view2.Model;
            var result2 = (from m in listVM2.ShoppingLists
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable();

            shoppingList = result2.FirstOrDefault();


            // Assert
            Assert.AreEqual("0000 test Edited", shoppingList.Name);
            Assert.AreEqual("SaveEditedShoppingListWithDescriptionChange Post-test", shoppingList.Description);

            // clean up 
            controller5.DeleteConfirmed(vm.ID);
        }

        [TestMethod]
        [TestCategory("DeleteConfirmed")]
        public void ActuallyDeleteAShoppingListFromTheDatabase()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            ShoppingListsController editController = new ShoppingListsController(repo);
            ShoppingListsController indexController = new ShoppingListsController(repo);
            ShoppingListsController deleteController = new ShoppingListsController(repo);
            ShoppingListVM vm = new ShoppingListVM();
            vm.Name = "0000" + new Guid().ToString();
            ActionResult ar = editController.PostEdit(vm);
            ViewResult view = indexController.Index();
            ListVM listVM = (ListVM)view.Model;
            var result = (from m in listVM.ShoppingLists
                          where m.Name == vm.Name
                          select m).AsQueryable();
            ShoppingList item = result.FirstOrDefault();

            //Act
            deleteController.DeleteConfirmed(item.ID);
            var deletedItem = (from m in repo.ShoppingLists
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

            EFRepository repo = new EFRepository(); ;
            ShoppingListsController controllerEdit = new ShoppingListsController(repo);
            ShoppingListsController controllerView = new ShoppingListsController(repo);
            ShoppingListsController controllerDelete = new ShoppingListsController(repo);

            // Act
            controllerEdit.PostEdit(shoppingListVM);
            ViewResult view = controllerView.Index();
            ListVM listVM = (ListVM)view.Model;
            var result = (from m in listVM.ShoppingLists
                          where m.Name == "001 Test "
                          select m).AsQueryable();

            ShoppingList shoppingList = result.FirstOrDefault();

            DateTime shouldBeSameDate = shoppingList.CreationDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);

            // Cleanup
            controllerDelete.DeleteConfirmed(shoppingList.ID);
        }


         [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            // Arrange
            EFRepository repo = new EFRepository();
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
            ListVM listVM = (ListVM)view.Model;
            var result = (from m in listVM.ShoppingLists
                          where m.Name == "002 Test Mod"
                          select m).AsQueryable();
            ShoppingList ingredient =  result.FirstOrDefault();
            vm = Mapper.Map<ShoppingList, ShoppingListVM>(ingredient);

            vm.Description = "I've been edited to delay a bit";

            controllerPost1.PostEdit(vm);


            ViewResult view1 = controllerView.Index();
            listVM = (ListVM)view1.Model;
            var result1 = (from m in listVM.ShoppingLists
                           where m.Name == "002 Test Mod"
                           select m).AsQueryable();

            ingredient = result1.FirstOrDefault();

            DateTime shouldBeSameDate = ingredient.CreationDate;
            DateTime shouldBeLaterDate = ingredient.ModifiedDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);
            Assert.AreNotEqual(mod, shouldBeLaterDate);

            // Cleanup
            controllerDelete.DeleteConfirmed(ingredient.ID);
        }
    }
}

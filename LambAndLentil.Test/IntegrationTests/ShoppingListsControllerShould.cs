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
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace IntegrationTests
{
    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("ShoppingListsController")]
    public class ShoppingListsControllerShould
    {

        static IRepository<ShoppingListVM> Repo;
        static ShoppingListsController controller;
        static ShoppingListVM vm;

        public ShoppingListsControllerShould()
        {
            Repo = new TestRepository<ShoppingListVM>();
            controller = new ShoppingListsController(Repo);
            vm = new ShoppingListVM();
            vm.ID = 400;
            vm.Name = "ShoppingListsControllerShould";
        }


        [TestMethod]
        public void CreateAShoppingList()
        {
            // Arrange

            // Act
            ViewResult vr = controller.Create(UIViewType.Create);
            ShoppingListVM vm = (ShoppingListVM)vr.Model;
            string modelName = vm.Name;

            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created");
        }

        [Ignore]
        [TestMethod]
        public void SaveAValidShoppingList()
        {
            // Arrange 
            ShoppingListsController controller = new ShoppingListsController(Repo);
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
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedShoppingListWithNameChange()
        {
            // Arrange 
            ShoppingListsController controller1 = new ShoppingListsController(Repo);
            ShoppingListsController controller2 = new ShoppingListsController(Repo);
            ShoppingListsController controller3 = new ShoppingListsController(Repo);
            ShoppingListsController controller4 = new ShoppingListsController(Repo);
            ShoppingListsController controller5 = new ShoppingListsController(Repo);
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

            // verify initial value:
            Assert.AreEqual("0000 test", item.Name);


            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = item.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM<ShoppingListVM> listVM2 = (ListVM<ShoppingListVM>)view2.Model;
            ShoppingListVM result2 = (from m in listVM2.ListT
                                      where m.Name == "0000 test Edited"
                                      select m).AsQueryable().FirstOrDefault();



            // Assert
            Assert.AreEqual("0000 test Edited", item.Name);

        }



        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedShoppingListWithDescriptionChange()
        {
            // Arrange 
            ShoppingListsController controller1 = new ShoppingListsController(Repo);
            ShoppingListsController controller2 = new ShoppingListsController(Repo);
            ShoppingListsController controller3 = new ShoppingListsController(Repo);
            ShoppingListsController controller4 = new ShoppingListsController(Repo);
            ShoppingListsController controller5 = new ShoppingListsController(Repo);
            ShoppingListVM vm = new ShoppingListVM();
            vm.Name = "0000 test";
            vm.Description = "SaveEditedShoppingListWithDescriptionChange Pre-test";


            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM<ShoppingListVM> listVM = (ListVM<ShoppingListVM>)view1.Model;
            ShoppingListVM shoppingListVM = (from m in listVM.ListT
                                             where m.Name == "0000 test"
                                             select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("SaveEditedShoppingListWithDescriptionChange Pre-test", shoppingListVM.Description);


            // now edit it
            vm.ID = shoppingListVM.ID;
            vm.Name = "0000 test Edited";
            vm.Description = "SaveEditedShoppingListWithDescriptionChange Post-test";

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM<ShoppingListVM> listVM2 = (ListVM<ShoppingListVM>)view2.Model;
            var result2 = (from m in listVM2.ListT
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable();

            shoppingListVM = result2.FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited", shoppingListVM.Name);
            Assert.AreEqual("SaveEditedShoppingListWithDescriptionChange Post-test", shoppingListVM.Description);
        }

        [TestMethod]
        [TestCategory("DeleteConfirmed")]
        public void ActuallyDeleteAShoppingListFromTheDatabase()
        {
            // Arrange  
            ShoppingListVM item = GetShoppingListVM(Repo,  "test ActuallyDeleteAShoppingListFromTheDatabase");
            //Act
            controller.DeleteConfirmed(item.ID);
            var deletedItem = (from m in Repo.GetAll()
                               where m.Description == item.Description
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
            ShoppingListVM shoppingListVM = new ShoppingListVM(CreationDate);
            shoppingListVM.Name = "001 Test ";

            JSONRepository<ShoppingList> Repo = new JSONRepository<ShoppingList>(); ;
            //ShoppingListsController controllerEdit = new ShoppingListsController(Repo);
            //ShoppingListsController controllerView = new ShoppingListsController(Repo);
            //ShoppingListsController controllerDelete = new ShoppingListsController(Repo);

            // Act
            //controllerEdit.PostEdit(shoppingListVM);
            //ViewResult view = controllerView.Index();
            //ShoppingListVM listVM = (ShoppingListVM)view.Model;
           // var result = (from m in listVM.ShoppingLists
            //              where m.Name == "001 Test "
           //               select m).AsQueryable();

           // ShoppingList shoppingList = result.FirstOrDefault();

           // DateTime shouldBeSameDate = shoppingList.CreationDate;
           
                // Assert
            //    Assert.AreEqual(CreationDate, shouldBeSameDate);
            
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            // Arrange
            JSONRepository<ShoppingList> Repo = new JSONRepository<ShoppingList>();
            //ShoppingListsController controllerPost = new ShoppingListsController(Repo);
            //ShoppingListsController controllerPost1 = new ShoppingListsController(Repo);
            //ShoppingListsController controllerView = new ShoppingListsController(Repo);
            //ShoppingListsController controllerView1 = new ShoppingListsController(Repo);
            //ShoppingListsController controllerDelete = new ShoppingListsController(Repo);

            ShoppingListVM vm = new ShoppingListVM();
            vm.Name = "002 Test Mod";
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
          //  controllerPost.PostEdit(vm);

         //   ViewResult view = controllerView.Index();
        //    ShoppingListVM vm2 = (ShoppingListVM)view.Model;
            //var result = (from m in vm2.ShoppingLists
            //              where m.Name == "002 Test Mod"
            //              select m).AsQueryable();

            //vm2.Description = "I've been edited to delay a bit";

            //controllerPost1.PostEdit(vm2);


            //ViewResult view1 = controllerView.Index();
            //ShoppingList listVM = (ShoppingList)view1.Model;
            //var result1 = (from m in listVM.ShoppingLists
            //               where m.Name == "002 Test Mod"
            //               select m).AsQueryable();

            //ShoppingList item = result1.FirstOrDefault();

            //DateTime shouldBeSameDate = item.CreationDate;
            //DateTime shouldBeLaterDate = item.ModifiedDate;

             
            //    // Assert
            //    Assert.AreEqual(CreationDate, shouldBeSameDate);
            //    Assert.AreNotEqual(mod, shouldBeLaterDate);
        
        }

        internal ShoppingListVM GetShoppingListVM(IRepository<ShoppingListVM> Repo,  string description)
        { 
            
            vm.ID = int.MaxValue;
            vm.Description = description;
            controller.PostEdit(vm);

            ShoppingListVM result =  (from m in Repo.GetAll()
                                  where m.Description == vm.Description
                                  select m).AsQueryable().FirstOrDefault();
            return result;
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void AttachAnExistingIngredientToAnExistingShoppingList()
        {
            // Arrange 
            JSONRepository<IngredientVM> repoIngredient = new JSONRepository< IngredientVM>();
            ShoppingListsController controller = new ShoppingListsController(Repo );

            ShoppingListVM  slVM = GetShoppingListVM(Repo,  "test AttachAnExistingIngredientToAnExistingShoppingList");
            IngredientVM ingredientVM = new RecipesControllerShould().GetIngredientVM(repoIngredient, "test AttachAnExistingIngredientToAnExistingShoppingList");

            // Act
            controller.AttachIngredient(slVM.ID, ingredientVM);
            ShoppingListVM returnedShoppingListVM = (from m in Repo.GetAll()
                                                 where m.Description == slVM.Description
                                                 select m).FirstOrDefault();



            // Assert 
            Assert.AreEqual(1, returnedShoppingListVM.Ingredients.Count());
            // how do I know the correct ingredient was added?
            Assert.AreEqual(ingredientVM.ID, returnedShoppingListVM.Ingredients.First().ID);
             
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void NotDeleteAnIngredientAfterIngredientIsDetachedFromShoppingList()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToANonExistingShoppingList()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingExistIngredientToNonExistingShoppingList()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingNonExistIngredientToNonExistingShoppingList()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNonExistingShoppingList()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnShoppingListEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingShoppingList()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnShoppingListEditViewWithWarningMessageWhenAttachingNonExistingIngredientToExistingShoppingList()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnShoppingListEditViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToExistingShoppingList()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnShoppingListIndexViewWithWarningWhenDetachingExistingingredientNotAttachedToAnExistingShoppingList()
        {
            Assert.Fail();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            ShoppingListsController_Test_Should.ClassCleanup();

        }
    }
}

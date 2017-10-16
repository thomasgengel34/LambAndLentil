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

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("ShoppingListsController")]
    public class ShoppingListsControllerShould
    {

        static IRepository<ShoppingList> Repo;
        static ShoppingListsController Controller;
        static ShoppingList vm;

        public ShoppingListsControllerShould()
        {
            Repo = new TestRepository<ShoppingList>();
            Controller = new ShoppingListsController(Repo);
            vm = new ShoppingList
            {
                ID = 400,
                Name = "ShoppingListsControllerShould"
            };
        }


        [TestMethod]
        public void CreateAShoppingList()
        {
            // Arrange

            // Act
            ViewResult vr = Controller.Create(UIViewType.Create);
            ShoppingList vm = (ShoppingList)vr.Model;
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
            ShoppingListsController Controller = new ShoppingListsController(Repo);
            ShoppingList vm = new ShoppingList
            {
                Name = "test"
            };
            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.PostEdit(vm);
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
            ShoppingListsController Controller1 = new ShoppingListsController(Repo);
            ShoppingListsController Controller2 = new ShoppingListsController(Repo);
            ShoppingListsController Controller3 = new ShoppingListsController(Repo);
            ShoppingListsController Controller4 = new ShoppingListsController(Repo);
            ShoppingListsController Controller5 = new ShoppingListsController(Repo);
            ShoppingList vm = new ShoppingList
            {
                Name = "0000 test"
            };

            // Act 
            ActionResult ar1 = Controller1.PostEdit(vm);
            ViewResult view1 = Controller2.Index();
            IEnumerable<ShoppingList> ListEntity= (IEnumerable<ShoppingList>)view1.Model;
            var result = (from m in ListEntity
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            ShoppingList item = result.FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("0000 test", item.Name);


            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = item.ID;
            ActionResult ar2 = Controller3.PostEdit(vm);
            ViewResult view2 = Controller4.Index();
            List<ShoppingList> ListEntity2 = (List<ShoppingList>)view2.Model;
            ShoppingList result2 = (from m in ListEntity2
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
            ShoppingListsController Controller1 = new ShoppingListsController(Repo);
            ShoppingListsController Controller2 = new ShoppingListsController(Repo);
            ShoppingListsController Controller3 = new ShoppingListsController(Repo);
            ShoppingListsController Controller4 = new ShoppingListsController(Repo);
            ShoppingListsController Controller5 = new ShoppingListsController(Repo);
            ShoppingList vm = new ShoppingList
            {
                Name = "0000 test",
                Description = "SaveEditedShoppingListWithDescriptionChange Pre-test"
            };


            // Act 
            ActionResult ar1 = Controller1.PostEdit(vm);
            ViewResult view1 = Controller2.Index();
            List<ShoppingList> ListEntity= (List<ShoppingList>)view1.Model;
            ShoppingList shoppingListVM = (from m in ListEntity
                                             where m.Name == "0000 test"
                                             select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("SaveEditedShoppingListWithDescriptionChange Pre-test", shoppingListVM.Description);


            // now edit it
            vm.ID = shoppingListVM.ID;
            vm.Name = "0000 test Edited";
            vm.Description = "SaveEditedShoppingListWithDescriptionChange Post-test";

            ActionResult ar2 = Controller3.PostEdit(vm);
            ViewResult view2 = Controller4.Index();
            List<ShoppingList> ListEntity2 = (List<ShoppingList>)view2.Model;
            var result2 = (from m in ListEntity2
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
               ShoppingList item = new ShoppingList { ID = 1, Description = "test ActuallyDeleteAShoppingListFromTheDatabase" };
        
            Repo.Add(item);

            //Act
            Controller.DeleteConfirmed(item.ID);
            var deletedItem = (from m in Repo.GetAll()
                               where m.ID == item.ID
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
            ShoppingList shoppingListVM = new ShoppingList(CreationDate)
            {
                Name = "001 Test "
            };

            JSONRepository<ShoppingList> Repo = new JSONRepository<ShoppingList>(); ;
            //ShoppingListsController ControllerEdit = new ShoppingListsController(Repo);
            //ShoppingListsController ControllerView = new ShoppingListsController(Repo);
            //ShoppingListsController ControllerDelete = new ShoppingListsController(Repo);

            // Act
            //ControllerEdit.PostEdit(shoppingListVM);
            //ViewResult view = ControllerView.Index();
            //ShoppingList ListEntity= (ShoppingList)view.Model;
           // var result = (from m in ListEntity.ShoppingLists
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
            //ShoppingListsController ControllerPost = new ShoppingListsController(Repo);
            //ShoppingListsController ControllerPost1 = new ShoppingListsController(Repo);
            //ShoppingListsController ControllerView = new ShoppingListsController(Repo);
            //ShoppingListsController ControllerView1 = new ShoppingListsController(Repo);
            //ShoppingListsController ControllerDelete = new ShoppingListsController(Repo);

            ShoppingList vm = new ShoppingList
            {
                Name = "002 Test Mod"
            };
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
          //  ControllerPost.PostEdit(vm);

         //   ViewResult view = ControllerView.Index();
        //    ShoppingList vm2 = (ShoppingList)view.Model;
            //var result = (from m in vm2.ShoppingLists
            //              where m.Name == "002 Test Mod"
            //              select m).AsQueryable();

            //vm2.Description = "I've been edited to delay a bit";

            //ControllerPost1.PostEdit(vm2);


            //ViewResult view1 = ControllerView.Index();
            //ShoppingList ListEntity= (ShoppingList)view1.Model;
            //var result1 = (from m in ListEntity.ShoppingLists
            //               where m.Name == "002 Test Mod"
            //               select m).AsQueryable();

            //ShoppingList item = result1.FirstOrDefault();

            //DateTime shouldBeSameDate = item.CreationDate;
            //DateTime shouldBeLaterDate = item.ModifiedDate;

             
            //    // Assert
            //    Assert.AreEqual(CreationDate, shouldBeSameDate);
            //    Assert.AreNotEqual(mod, shouldBeLaterDate);
        
        }

        internal ShoppingList GetShoppingList(IRepository<ShoppingList> Repo,  string description)
        { 
            
            vm.ID = int.MaxValue;
            vm.Description = description;
            Controller.PostEdit(vm);

            ShoppingList result =  (from m in Repo.GetAll()
                                  where m.Description == vm.Description
                                  select m).AsQueryable().FirstOrDefault();
            return result;
        }

       
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void AttachAnExistingIngredientToAnExistingShoppingList()
        {
            // Arrange 
            JSONRepository<Ingredient> repoIngredient = new TestRepository< Ingredient>();
            //ShoppingListsController Controller = new ShoppingListsController(Repo );

            ShoppingList  slVM = GetShoppingList(Repo,  "test AttachAnExistingIngredientToAnExistingShoppingList");
            Ingredient ingredient = new Ingredient { ID = 500, Description="test AttachAnExistingIngredientToAnExistingShoppingList" };
            repoIngredient.Add(ingredient);
            // Act
            Controller.AttachIngredient(slVM.ID, ingredient);
            ShoppingList returnedShoppingList = (from m in Repo.GetAll()
                                                 where m.Description == slVM.Description
                                                 select m).FirstOrDefault();



            // Assert 
            Assert.AreEqual(1, returnedShoppingList.Ingredients.Count());
            // how do I know the correct ingredient was added?
            Assert.AreEqual(ingredient.ID, returnedShoppingList.Ingredients.First().ID);
             
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

        [Ignore]
        [TestMethod]
        public void CreateAPrintableShoppingList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void EditAPrintableShoppingList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DisplayAPrintableShoppingList()
        {

            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }


        [Ignore]
        [TestMethod]
        public void DeleteAPrintableShoppingList()
        { 
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DownloadAPrintableShoppingList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
        [Ignore]
        [TestMethod]
        public void CreateACheckableShoppingList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void EditACheckableShoppingList()
        {             
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DisplayACheckableShoppingList()
        {
           
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }


        [Ignore]
        [TestMethod]
        public void DeleteACheckableShoppingList()
        {
             
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DownloadACheckableShoppingList()
        {  
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            ShoppingListsController_Test_Should.ClassCleanup();

        }
    }
}

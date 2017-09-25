using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
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
    [TestCategory("ShoppingListsController")]
    public class ShoppingListsController_Test_Should_Misc:ShoppingListsController_Test_Should
    {
        

        [TestMethod]
        public void IsPublic()
        {
            // Arrange


            // Act
            Type type = Controller.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        }




        [TestMethod]
        public void InheritsFromBaseControllerCorrectly()
        {

            // Arrange

            // Act 
            Controller.PageSize = 4;

            var type = typeof(ShoppingListsController);
            var DoesDisposeExist = type.GetMethod("Dispose");

            // Assert 
            Assert.AreEqual(4, Controller.PageSize);
            Assert.IsNotNull(DoesDisposeExist);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void Index()
        {
            // Arrange


            // Act
            ViewResult result = Controller.Index(1) as ViewResult;


            // Assert
            Assert.IsNotNull(result);

        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllShoppingLists()
        {
            // Arrange

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = ((ListEntity<ShoppingList>)(view1.Model)).ListT.Count();

            ViewResult view2 = Controller.Index(2);

            int count2 = ((ListEntity<ShoppingList>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
            Assert.AreEqual(5, count1);
            Assert.AreEqual(0, count2);
            Assert.AreEqual(5, count);
            Assert.AreEqual("Index", view1.ViewName);
            Assert.AreEqual("Index", view2.ViewName);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsCorrect()
        {
            // Arrange 
            ListEntity<ShoppingList> list = new ListEntity<ShoppingList>();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);
            int count1 = ((ListEntity<ShoppingList>)(view1.Model)).ListT.Count();

            // Assert
            Assert.IsNotNull(view1);
            Assert.AreEqual(5, count1);
            Assert.AreEqual("Index", view1.ViewName);

            Assert.AreEqual("ShoppingListsController_Index_Test P1", ((ListEntity<ShoppingList>)(view1.Model)).ListT.FirstOrDefault().Name);
            Assert.AreEqual("ShoppingListsController_Index_Test P2", ((ListEntity<ShoppingList>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual("ShoppingListsController_Index_Test P3", ((ListEntity<ShoppingList>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        // currently we only have one page here
        public void ShoppingListsCtr_Index_SecondPageIsCorrect()
        {

        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void CanSendPaginationViewModel()
        {

            // Arrange
            

            // Act 
            ListEntity<ShoppingList> resultT = (ListEntity<ShoppingList>)((ViewResult)Controller.Index(2)).Model;


            // Assert 
            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(5, pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void PagingInfoIsCorrect()
        {
            // Arrange 

            // Action
            int totalItems = ((ListEntity<ShoppingList>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = ((ListEntity<ShoppingList>)((ViewResult)Controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = ((ListEntity<ShoppingList>)((ViewResult)Controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = ((ListEntity<ShoppingList>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalPages;



            // Assert
            Assert.AreEqual(5, totalItems);
            Assert.AreEqual(1, currentPage);
            Assert.AreEqual(8, itemsPerPage);
            Assert.AreEqual(1, totalPages);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate()
        {
            // Arrange

            // Act
            var result = (ListEntity<ShoppingList>)(Controller.Index(1)).Model;

            // Assert 
            Assert.IsTrue(result.ListT.Count() == 5);
            Assert.AreEqual("ShoppingListsController_Index_Test P1", result.ListT.FirstOrDefault().Name);
            Assert.AreEqual("ShoppingListsController_Index_Test P4", result.ListT.Skip(3).FirstOrDefault().Name);
        }

        [TestMethod]
        [TestCategory("Create")]
        public void Create()
        {
            // Arrange 
            ViewResult view = Controller.Create(UIViewType.Edit);


            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Remove")]
        public void RemoveAFoundShoppingList()
        {   // does not actually remove, just sets up to remove it.
            // TODO: verify "Are you sure you want to delete this?" message shows up.
            // Arrange
            int count = Repo.Count();

            // Act 
            ActionResult ar = Controller.Delete(int.MaxValue);
            ViewResult view = (ViewResult)ar;
            int newCount = Repo.Count();

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual(UIViewType.Details.ToString(), view.ViewName);
            Assert.AreEqual(count, newCount);
        }



        [TestMethod]
        [TestCategory("Remove")]
        public void RemoveAnInvalidShoppingList()
        {
            // Arrange 

            // Act 
            var view = Controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Shopping List was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);

            Assert.AreEqual(UIViewType.Index.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());

        }

        [TestMethod]
        [TestCategory("Remove")]
        public void RemoveConfirmed()
        {
            // Arrange
            int count = Repo.Count();

            // Act
            ActionResult result = Controller.DeleteConfirmed(int.MaxValue) as ActionResult;
            int newCount = Repo.Count();
            // TODO: improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { controller = "ShoppingLists", Action = "Index" } } );
            //TODO: check message

            // Assert 
            Assert.IsNotNull(result);
            Assert.AreEqual(count - 1, newCount);
        }

        [TestMethod]
        [TestCategory("Remove")]
        public void CanRemoveValidShoppingList()
        {
            // Arrange - create an shoppingListEntity
            ShoppingList shoppingListEntityVM = new ShoppingList { ID = 2, Name = "Test2" };
            Repo.Add(shoppingListEntityVM);

            // Act - delete the shoppingListEntity
            ActionResult result = Controller.DeleteConfirmed(shoppingListEntityVM.ID);

            AlertDecoratorResult adr = (AlertDecoratorResult)result;

            // Assert - ensure that the repository delete method was called with a correct ShoppingList



            Assert.AreEqual("Test2 has been deleted", adr.Message);
        }


        [Ignore]   // not working, not done, not sure it's worth pursuing or abandoning
        [TestMethod]
        [TestCategory("Edit")]
        public void CanEditShoppingList()
        {
            // Arrange 
            ShoppingListsController controller2 = new ShoppingListsController(Repo);

            // Act  
            ViewResult view1 = (ViewResult)Controller.Edit(int.MaxValue);
            ShoppingList p1 = (ShoppingList)view1.Model;
            ViewResult view2 = (ViewResult)Controller.Edit(int.MaxValue - 1);
            ShoppingList p2 = (ShoppingList)view2.Model;
            ViewResult view3 = (ViewResult)Controller.Edit(int.MaxValue - 2);
            ShoppingList p3 = (ShoppingList)view3.Model;

            // Assert 
            Assert.IsNotNull(view1);

        }


        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void  CannotEditNonexistentShoppingList()
        {
            // Arrange
            
            // Act
            ShoppingList result = (ShoppingList)((ViewResult)Controller.Edit(8)).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }


        [Ignore]   // look into why this is not working
        [TestMethod]
        [TestCategory("Edit")]
        public void CanEditShoppingListXXX()
        {
            // Arrange
            ShoppingList menuVM = new ShoppingList
            {
                ID = 1,
                Name = "test ShoppingListControllerTest.CanEditShoppingList",
                Description = "test ShoppingListControllerTest.CanEditShoppingList"
            };
            Repo.Save(menuVM);

            // Act 
            menuVM.Name = "Name has been changed";

            ViewResult view1 = (ViewResult)Controller.Edit(1);

            var returnedShoppingList = (ShoppingList)(view1.Model);


            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual("Name has been changed", returnedShoppingList.Name);
            //Assert.AreEqual(menuVM.Description, returnedShoppingListlist.Description);
            //Assert.AreEqual(menuVM.CreationDate, returnedShoppingListlist.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedShoppingList()
        {
            // Arrange
            ShoppingListsController indexController = new ShoppingListsController(Repo);
            ShoppingListsController controller2 = new ShoppingListsController(Repo);
            ShoppingListsController controller3 = new ShoppingListsController(Repo);


            ShoppingList vm = new ShoppingList
            {
                Name = "0000 test",
                ID = int.MaxValue - 100,
                Description = "test ShoppingListsControllerShould.SaveEditedShoppingList"
            };

            // Act 
            ActionResult ar1 = Controller.PostEdit(vm);


            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = 7777;
            ActionResult ar2 = controller2.PostEdit(vm);
            ViewResult view2 = controller3.Index();
            ListEntity<ShoppingList> list2 = (ListEntity<ShoppingList>)view2.Model;
            ShoppingList vm3 = (from m in list2.ListT
                                where m.Name == "0000 test Edited"
                                  select m).AsQueryable().FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited", vm3.Name);
            Assert.AreEqual(7777, vm3.ID);

        }

        [Ignore]  // look into why this is not working
        [TestMethod]
        [TestCategory("Edit")]
        public void CanPostEditShoppingList()
        {
            // Arrange
            ShoppingList menuVM = new ShoppingList
            {
                ID = 1,
                Name = "test ShoppingListControllerTest.CanEditShoppingList",
                Description = "test ShoppingListControllerTest.CanEditShoppingList"
            };
            Repo.Add(menuVM);

            // Act 
            menuVM.Name = "Name has been changed";

            ViewResult view1 = (ViewResult)Controller.Edit(1);

            ShoppingList returnedShoppingListlist = Repo.GetById(1);

            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual("Name has been changed", returnedShoppingListlist.Name);
            Assert.AreEqual(menuVM.Description, returnedShoppingListlist.Description);
            Assert.AreEqual(menuVM.CreationDate, returnedShoppingListlist.CreationDate);
        }


        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void XxxCannotEditNonexistentShoppingList()
        {
            // Arrange

            // Act
            ShoppingList result = (ShoppingList)((ViewResult)Controller.Edit(8)).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void  CreateReturnsNonNull()
        {
            // Arrange 

            // Act
            ViewResult result = Controller.Create(UIViewType.Create) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Ignore]
        [TestMethod]
        public void FlagAnIngredientFlaggedInAPerson()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void FlagAnIngredientFlaggedInTwoPersons()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void WhenAFlagHasBeenRemovedFromOnePersonStillThereForSecondFlaggedPerson()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("Copy")]
        [TestMethod]
        public void CopyModifySaveWithANewName()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void CorrectPropertiesAreBoundInEdit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetTheClassNameCorrect()
        {
            // Arrange

            // Act


            // Assert
            //  Assert.Fail();
            Assert.AreEqual("LambAndLentil.UI.Controllers.ShoppingListsController", Controller.ToString());
        } 
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.Domain.Abstract;  
using System.Linq;

using LambAndLentil.UI.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using AutoMapper;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using System.IO;

namespace LambAndLentil.Tests.Controllers
{

    [TestClass]
    [TestCategory("ShoppingListsController")]
    public class ShoppingListsControllerTest
    {
        private static IRepository<ShoppingListVM> repo { get; set; }
        public static MapperConfiguration AutoMapperConfig { get; set; }
        private static ListVM<ShoppingListVM> listVM;
        private static ShoppingListsController controller { get; set; }

        public ShoppingListsControllerTest()
        {
            AutoMapperConfigForTests.InitializeMap();
            repo = new TestRepository<ShoppingListVM>();
            listVM = new ListVM<ShoppingListVM>();
            controller = SetUpController();
        }

        private ShoppingListsController SetUpController()
        {

            listVM.ListT = new List<ShoppingListVM> {
                new ShoppingListVM {ID = int.MaxValue, Name = "ShoppingListsController_Index_Test P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new ShoppingListVM {ID = int.MaxValue-1, Name = "ShoppingListsController_Index_Test P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new ShoppingListVM {ID = int.MaxValue-2, Name = "ShoppingListsController_Index_Test P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new ShoppingListVM {ID = int.MaxValue-3, Name = "ShoppingListsController_Index_Test P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new ShoppingListVM {ID = int.MaxValue-4, Name = "ShoppingListsController_Index_Test P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            }.AsQueryable();

            foreach (ShoppingListVM ingredient in listVM.ListT)
            {
                repo.Add(ingredient);
            }

            controller = new ShoppingListsController(repo);
            controller.PageSize = 3;

            return controller;
        }

        [TestMethod]
        public void IsPublic()
        {
            // Arrange


            // Act
            Type type = controller.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        }




        [TestMethod]
        public void InheritsFromBaseControllerCorrectly()
        {

            // Arrange

            // Act 
            controller.PageSize = 4;

            var type = typeof(ShoppingListsController);
            var DoesDisposeExist = type.GetMethod("Dispose");

            // Assert 
            Assert.AreEqual(4, controller.PageSize);
            Assert.IsNotNull(DoesDisposeExist);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void Index()
        {
            // Arrange


            // Act
            ViewResult result = controller.Index(1) as ViewResult;


            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllShoppingLists()
        {
            // Arrange

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<ShoppingListVM>)(view1.Model)).ListT.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM<ShoppingListVM>)(view2.Model)).ListT.Count();

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


        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsCorrect()
        {
            // Arrange
            ShoppingListsController controller = SetUpController();
            ListVM<ShoppingListVM> ilvm = new ListVM<ShoppingListVM>();
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);
            int count1 = ((ListVM<ShoppingListVM>)(view1.Model)).ListT.Count();

            // Assert
            Assert.IsNotNull(view1);
            Assert.AreEqual(5, count1);
            Assert.AreEqual("Index", view1.ViewName);

            Assert.AreEqual("ShoppingListsController_Index_Test P1", ((ListVM<ShoppingListVM>)(view1.Model)).ListT.FirstOrDefault().Name);
            Assert.AreEqual("ShoppingListsController_Index_Test P2", ((ListVM<ShoppingListVM>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual("ShoppingListsController_Index_Test P3", ((ListVM<ShoppingListVM>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        // currently we only have one page here
        public void ShoppingListsCtr_Index_SecondPageIsCorrect()
        {

        }

        [TestMethod]
        [TestCategory("Index")]
        public void CanSendPaginationViewModel()
        {

            // Arrange
            ShoppingListsController controller = SetUpController();

            // Act 
            ListVM<ShoppingListVM> resultT = (ListVM<ShoppingListVM>)((ViewResult)controller.Index(2)).Model;


            // Assert 
            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(5, pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void PagingInfoIsCorrect()
        {
            // Arrange 

            // Action
            int totalItems = ((ListVM<ShoppingListVM>)((ViewResult)controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = ((ListVM<ShoppingListVM>)((ViewResult)controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = ((ListVM<ShoppingListVM>)((ViewResult)controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = ((ListVM<ShoppingListVM>)((ViewResult)controller.Index()).Model).PagingInfo.TotalPages;



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
            var result = (ListVM<ShoppingListVM>)(controller.Index(1)).Model;

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
            ViewResult view = controller.Create(UIViewType.Edit);


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
            int count = repo.Count();

            // Act 
            ActionResult ar = controller.Delete(int.MaxValue);
            ViewResult view = (ViewResult)ar;
            int newCount = repo.Count();

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
            var view = controller.Delete(4000) as ViewResult;
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
            int count = repo.Count();

            // Act
            ActionResult result = controller.DeleteConfirmed(int.MaxValue) as ActionResult;
            int newCount = repo.Count();
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
            // Arrange - create an shoppingList
            ShoppingListVM shoppingListVM = new ShoppingListVM { ID = 2, Name = "Test2" };
            repo.Add(shoppingListVM);

            // Act - delete the shoppingList
            ActionResult result = controller.DeleteConfirmed(shoppingListVM.ID);

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
            ShoppingListsController controller2 = new ShoppingListsController(repo);

            // Act  
            ViewResult view1 = controller.Edit(int.MaxValue);
            ShoppingListVM p1 = (ShoppingListVM)view1.Model;
            ViewResult view2 = controller.Edit(int.MaxValue - 1);
            ShoppingListVM p2 = (ShoppingListVM)view2.Model;
            ViewResult view3 = controller.Edit(int.MaxValue - 2);
            ShoppingListVM p3 = (ShoppingListVM)view3.Model;

            // Assert 
            Assert.IsNotNull(view1);

        }



        [TestMethod]
        [TestCategory("Edit")]
        public void ShoppingListsCtr_CannotEditNonexistentShoppingList()
        {
            // Arrange
            ShoppingListsController controller = SetUpController();
            // Act
            ShoppingList result = (ShoppingList)controller.Edit(8).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }


        [Ignore]   // look into why this is not working
        [TestMethod]
        [TestCategory("Edit")]
        public void CanEditShoppingListXXX()
        {
            // Arrange
            ShoppingListVM menuVM = new ShoppingListVM
            {
                ID = 1,
                Name = "test ShoppingListControllerTest.CanEditShoppingList",
                Description = "test ShoppingListControllerTest.CanEditShoppingList"
            };
            repo.Save(menuVM);

            // Act 
            menuVM.Name = "Name has been changed";

            ViewResult view1 = controller.Edit(1);

            var returnedShoppingListVM = (ShoppingListVM)(view1.Model);


            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual("Name has been changed", returnedShoppingListVM.Name);
            //Assert.AreEqual(menuVM.Description, returnedShoppingListVm.Description);
            //Assert.AreEqual(menuVM.CreationDate, returnedShoppingListVm.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedShoppingList()
        {
            // Arrange
            ShoppingListsController indexController = new ShoppingListsController(repo);
            ShoppingListsController controller2 = new ShoppingListsController(repo);
            ShoppingListsController controller3 = new ShoppingListsController(repo);


            ShoppingListVM vm = new ShoppingListVM();
            vm.Name = "0000 test";
            vm.ID = int.MaxValue - 100;
            vm.Description = "test ShoppingListsControllerShould.SaveEditedShoppingList";

            // Act 
            ActionResult ar1 = controller.PostEdit(vm);


            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = 7777;
            ActionResult ar2 = controller2.PostEdit(vm);
            ViewResult view2 = controller3.Index();
            ListVM<ShoppingListVM> listVM2 = (ListVM<ShoppingListVM>)view2.Model;
            ShoppingListVM vm3 = (from m in listVM2.ListT
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
            ShoppingListVM menuVM = new ShoppingListVM
            {
                ID = 1,
                Name = "test ShoppingListControllerTest.CanEditShoppingList",
                Description = "test ShoppingListControllerTest.CanEditShoppingList"
            };
            repo.Add(menuVM);

            // Act 
            menuVM.Name = "Name has been changed";

            ViewResult view1 = controller.Edit(1);

            ShoppingListVM returnedShoppingListVm = repo.GetById(1);

            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual("Name has been changed", returnedShoppingListVm.Name);
            Assert.AreEqual(menuVM.Description, returnedShoppingListVm.Description);
            Assert.AreEqual(menuVM.CreationDate, returnedShoppingListVm.CreationDate);
        }



        [TestMethod]
        [TestCategory("Edit")]
        public void CannotEditNonexistentShoppingList()
        {
            // Arrange

            // Act
            ShoppingList result = (ShoppingList)controller.Edit(8).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ShoppingListsCtr_CreateReturnsNonNull()
        {
            // Arrange
            ShoppingListsController controller = SetUpController();


            // Act
            ViewResult result = controller.Create(UIViewType.Create) as ViewResult;

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



        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\ShoppingList\";
            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }

        }
    }
}

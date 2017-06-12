using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.Domain.Abstract;
using Moq;
using LambAndLentil.Domain.Entities;
using System.Linq;

using LambAndLentil.UI.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using AutoMapper;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;

namespace LambAndLentil.Tests.Controllers
{
    [TestClass]
    [TestCategory("ShoppingListsController")]
    public class ShoppingListsControllerTest
    {
        static Mock<IRepository> mock;
        public static MapperConfiguration AutoMapperConfig { get; set; }

        public ShoppingListsControllerTest()
        {
            //AutoMapperConfig = AutoMapperConfigForTests.AMConfigForTests();

        }

        [TestMethod]
        public void ShoppingListsCtr_IsPublic()
        {
            // Arrange
            ShoppingListsController testController = SetUpSimpleController();

            // Act
            Type type = testController.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        }




        [TestMethod]
        public void ShoppingListsCtr_InheritsFromBaseControllerCorrectly()
        {

            // Arrange
            ShoppingListsController controller = SetUpSimpleController();
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
        public void ShoppingListsCtr_Index()
        {
            // Arrange
            ShoppingListsController controller = SetUpController();

            // Act
            ViewResult result = controller.Index(1) as ViewResult;
            ViewResult result1 = controller.Index(2) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ShoppingListsCtr_Index_ContainsAllShoppingLists()
        {
            // Arrange
            ShoppingListsController controller = SetUpController();
            ListVM ilvm = new ListVM();
            ilvm.ShoppingLists = (IEnumerable<ShoppingList>)mock.Object.ShoppingLists;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).ShoppingLists.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM)(view2.Model)).ShoppingLists.Count();

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
        public void ShoppingListsCtr_Index_FirstPageIsCorrect()
        {
            // Arrange
            ShoppingListsController controller = SetUpController();
            ListVM ilvm = new ListVM();
            ilvm.ShoppingLists = (IEnumerable<ShoppingList>)mock.Object.ShoppingLists;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).ShoppingLists.Count();



            // Assert
            Assert.IsNotNull(view1);
            Assert.AreEqual(5, count1);
            Assert.AreEqual("Index", view1.ViewName);

            Assert.AreEqual("Old Name 1", ((ListVM)(view1.Model)).ShoppingLists.FirstOrDefault().Name);
            Assert.AreEqual("Old Name 2", ((ListVM)(view1.Model)).ShoppingLists.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual("Old Name 3", ((ListVM)(view1.Model)).ShoppingLists.Skip(2).FirstOrDefault().Name);


        }


        [TestMethod]
        [TestCategory("Index")]
        // currently we only have one page here
        public void ShoppingListsCtr_Index_SecondPageIsCorrect()
        {
            
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ShoppingListsCtr_Index_CanSendPaginationViewModel()
        {

            // Arrange
            ShoppingListsController controller = SetUpController();

            // Act

            ListVM resultT = (ListVM)((ViewResult)controller.Index(2)).Model;


            // Assert

            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(5, pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void ShoppingListsCtr_Index_PagingInfoIsCorrect()
        {
            // Arrange
            ShoppingListsController controller = SetUpController();


            // Action
            int totalItems = ((ListVM)((ViewResult)controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = ((ListVM)((ViewResult)controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = ((ListVM)((ViewResult)controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = ((ListVM)((ViewResult)controller.Index()).Model).PagingInfo.TotalPages;



            // Assert
            Assert.AreEqual(5, totalItems);
            Assert.AreEqual(1, currentPage);
            Assert.AreEqual(8, itemsPerPage);
            Assert.AreEqual(1, totalPages);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ShoppingListsCtr_IndexCanPaginate()
        {
            // Arrange
            ShoppingListsController controller = SetUpController();

            // Act
            var result = (ListVM)(controller.Index(1)).Model;



            // Assert
            ShoppingList[] ingrArray1 = result.ShoppingLists.ToArray();
            Assert.IsTrue(ingrArray1.Length == 5);
            Assert.AreEqual("Old Name 1", ingrArray1[0].Name);
            Assert.AreEqual("Old Name 4", ingrArray1[3].Name);
        }

        [TestMethod]
        [TestCategory("Create")]
        public void ShoppingListsCtr_Create()
        {
            // Arrange
            ShoppingListsController controller = SetUpController();
            ViewResult view = controller.Create(UIViewType.Edit);


            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void ShoppingListsCtr_DeleteAFoundShoppingList()
        {
            // Arrange
            ShoppingListsController controller = SetUpController();

            // Act 
            var view = controller.Delete(1) as ViewResult;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual(UIViewType.Details.ToString(), view.ViewName);
        }



        [TestMethod]
        [TestCategory("Delete")]
        public void ShoppingListsCtr_DeleteAnInvalidShoppingList()
        {
            // Arrange
            ShoppingListsController controller = SetUpController();

            // Act 
            var view = controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No shopping list was found with that id.", adr.Message);
             Assert.AreEqual("alert-danger", adr.AlertClass);
           Assert.AreEqual(UIControllerType.ShoppingLists.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void ShoppingListsCtr_DeleteConfirmed()
        {
            // Arrange
            ShoppingListsController controller = SetUpController();
            // Act
            ActionResult result = controller.DeleteConfirmed(1) as ActionResult;
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { controller = "ShoppingLists", Action = "Index" } } );
            // Assert 
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void ShoppingLists_Ctr_CanDeleteValidShoppingList()
        {
            // Arrange - create an shoppingList
            ShoppingList shoppingList = new ShoppingList { ID = 2, Name = "Test2" };

            // Arrange - create the mock repository
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.ShoppingLists).Returns(new ShoppingList[]
            {
                new ShoppingList {ID=1,Name="Test1"},

                shoppingList,

                new ShoppingList {ID=3,Name="Test3"},
            }.AsQueryable());
            mock.Setup(m => m.Delete<ShoppingList>(It.IsAny<int>())).Verifiable();
            // Arrange - create the controller
            ShoppingListsController controller = new ShoppingListsController(mock.Object);

            // Act - delete the shoppingList
            ActionResult result = controller.DeleteConfirmed(shoppingList.ID);

            AlertDecoratorResult adr = (AlertDecoratorResult)result;

            // Assert - ensure that the repository delete method was called with a correct ShoppingList
            mock.Verify(m => m.Delete<ShoppingList>(shoppingList.ID));


            Assert.AreEqual("Test2 has been deleted", adr.Message);
        }



        [TestMethod]
        [TestCategory("Edit")]
        public void ShoppingListsCtr_CanEditShoppingList()
        {
            // Arrange
            ShoppingListsController controller = SetUpController();

            ShoppingList shoppingList = mock.Object.ShoppingLists.First();
            mock.Setup(c => c.Save(shoppingList)).Verifiable();
            shoppingList.Name = "First edited";

            // Act 

            ViewResult view1 = controller.Edit(1);
            ShoppingListVM p1 = (ShoppingListVM)view1.Model;
            ViewResult view2 = controller.Edit(2);
            ShoppingListVM p2 = (ShoppingListVM)view2.Model;
            ViewResult view3 = controller.Edit(3);
            ShoppingListVM p3 = (ShoppingListVM)view3.Model;


            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual(1, p1.ID);
            Assert.AreEqual(2, p2.ID);
            Assert.AreEqual(3, p3.ID);
            Assert.AreEqual("First edited", p1.Name);
            Assert.AreEqual("Old Name 2", p2.Name);
        }

     

        [TestMethod]
        [TestCategory("Edit")]
        public void ShoppingListsCtr_CannotEditNonexistentShoppingList()
        {
            //    // Arrange
            //    ShoppingListsController controller = SetUpController();
            //    // Act
            //    ShoppingList result = (ShoppingList)controller.Edit(8).ViewData.Model;
            //    // Assert
            //    Assert.IsNull(result);
            //}

            //[TestMethod]
            //public void ShoppingListsCtr_CreateReturnsNonNull()
            //{
            //    // Arrange
            //    ShoppingListsController controller = SetUpController();


            //    // Act
            //    ViewResult result = controller.Create(null) as ViewResult;

            //    // Assert
            //    Assert.IsNotNull(result);
        }

        private ShoppingListsController SetUpController()
        {
            // - create the mock repository
            mock = new Mock<IRepository>();
            mock.Setup(m => m.ShoppingLists).Returns(new ShoppingList[] {
                new ShoppingList {ID = 1, Name = "Old Name 1" },
                new ShoppingList {ID = 2, Name = "Old Name 2" },
                new ShoppingList {ID = 3, Name = "Old Name 3" },
                new ShoppingList {ID = 4, Name = "Old Name 4", },
                new ShoppingList {ID = 5, Name = "Old Name 5" }
            }.AsQueryable());

            // Arrange - create a controller
            ShoppingListsController controller = new ShoppingListsController(mock.Object);
            controller.PageSize = 3;

            return controller;
        }



        private ShoppingListsController SetUpSimpleController()
        {
            // - create the mock repository
            Mock<IRepository> mock = new Mock<IRepository>();


            // Arrange - create a controller
            ShoppingListsController controller = new ShoppingListsController(mock.Object);
            // controller.PageSize = 3;

            return controller;
        }
    }
}

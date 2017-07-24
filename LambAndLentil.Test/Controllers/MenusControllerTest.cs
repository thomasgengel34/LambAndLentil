using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using Moq;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using System.Linq;
using LambAndLentil.UI.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using AutoMapper;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI;
using LambAndLentil.UI.HtmlHelpers;
using LambAndLentil.UI.Infrastructure.Alerts; 
namespace LambAndLentil.Tests.Controllers
{
    [TestClass]
    [TestCategory("MenusController")]
    public class MenusControllerTest
    {
        static Mock<IRepository<Menu,MenuVM>> mock;
        public static MapperConfiguration AutoMapperConfig { get; set; }

        public MenusControllerTest()
        {
              AutoMapperConfigForTests.InitializeMap();

        }

        [TestMethod]
        public void MenusCtr_InheritsFromBaseControllerCorrectly()
        {

            Assert.IsNotNull("this is a placeholder");
        }

        [TestMethod]
        public void MenusCtr_IsPublic()
        {
            // Arrange
            MenusController testController = SetUpController();

            // Act
            Type type = testController.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(true, isPublic);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void MenusCtr_Index()
        {
            // Arrange
            MenusController controller = SetUpController();

            // Act
            ViewResult result = controller.Index(1) as ViewResult;
            ViewResult result1 = controller.Index(2) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void MenusCtr_Index_ContainsAllMenus()
        {
            // Arrange
            MenusController controller = SetUpController();
            ListVM<Menu,MenuVM> ilvm = new ListVM<Menu,MenuVM>();
            ilvm.Entities = (IEnumerable<Menu>)mock.Object.Menu;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Menu,MenuVM>)(view1.Model)).Entities.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM<Menu,MenuVM>)(view2.Model)).Entities.Count();

            int count = count1 + count2;

            // Assert
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
            Assert.AreEqual(5, count1);
            Assert.AreEqual(0, count2);
            Assert.AreEqual(5, count);
            Assert.AreEqual("Index", view1.ViewName);
            Assert.AreEqual("Index", view2.ViewName);

            //Assert.AreEqual("P1", ((ListVM<Menu,MenuVM>)(view1.Model)).Menus.FirstOrDefault().Name);
            //Assert.AreEqual("P2", ((ListVM<Menu,MenuVM>)(view1.Model)).Menus.Skip(1).FirstOrDefault().Name);
            //Assert.AreEqual("P3", ((ListVM<Menu,MenuVM>)(view1.Model)).Menus.Skip(2).FirstOrDefault().Name);
            //Assert.AreEqual("P5", ((ListVM<Menu,MenuVM>)(view2.Model)).Menus.FirstOrDefault().Name);

        }


        [TestMethod]
        [TestCategory("Index")]
        public void MenusCtr_Index_FirstPageIsCorrect()
        {
            // Arrange
            MenusController controller = SetUpController();
            ListVM<Menu,MenuVM> ilvm = new ListVM<Menu,MenuVM>();
            ilvm.Entities = (IEnumerable<Menu>)mock.Object.Menu;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Menu,MenuVM>)(view1.Model)).Entities.Count();



            // Assert
            Assert.IsNotNull(view1);
            Assert.AreEqual(5, count1);
            Assert.AreEqual("Index", view1.ViewName);

            Assert.AreEqual("Old Name 1", ((ListVM<Menu,MenuVM>)(view1.Model)).Entities.FirstOrDefault().Name);
            Assert.AreEqual("Old Name 2", ((ListVM<Menu,MenuVM>)(view1.Model)).Entities.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual("Old Name 3", ((ListVM<Menu,MenuVM>)(view1.Model)).Entities.Skip(2).FirstOrDefault().Name);


        }


        [TestMethod]
        [TestCategory("Index")]
        // currently we only have one page here
        public void MenusCtr_Index_SecondPageIsCorrect()
        {
            //// Arrange
            //MenusController controller = SetUpController();
            //ListVM<Menu,MenuVM> ilvm = new ListVM<Menu,MenuVM>();
            //ilvm.Menus = (IEnumerable<Menu,MenuVM>)mock.Object.Menus;

            //// Act
            //ViewResult view  = controller.Index(null, null, null, 2);

            //int count  = ((ListVM<Menu,MenuVM>)(view.Model)).Menus.Count(); 

            //// Assert
            //Assert.IsNotNull(view);
            //Assert.AreEqual(0, count );
            //Assert.AreEqual("Index", view.ViewName); 
            // Assert.AreEqual("P5", ((ListVM<Menu,MenuVM>)(view.Model)).Menus.FirstOrDefault().Name);
            // Assert.AreEqual( 5, ((ListVM<Menu,MenuVM>)(view.Model)).Menus.FirstOrDefault().ID);
            // Assert.AreEqual("C", ((ListVM<Menu,MenuVM>)(view.Model)).Menus.FirstOrDefault().Maker);
            // Assert.AreEqual("CC", ((ListVM<Menu,MenuVM>)(view.Model)).Menus.FirstOrDefault().Brand);

        }

        [TestMethod]
        [TestCategory("Index")]
        public void MenusCtr_Index_CanSendPaginationViewModel()
        {

            // Arrange
            MenusController controller = SetUpController();

            // Act

            ListVM<Menu,MenuVM> resultT = (ListVM<Menu,MenuVM>)((ViewResult)controller.Index(2)).Model;


            // Assert

            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(5, pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void MenusCtr_Index_PagingInfoIsCorrect()
        {
            // Arrange
            MenusController controller = SetUpController();


            // Action
            int totalItems = ((ListVM<Menu,MenuVM>)((ViewResult)controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = ((ListVM<Menu,MenuVM>)((ViewResult)controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = ((ListVM<Menu,MenuVM>)((ViewResult)controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = ((ListVM<Menu,MenuVM>)((ViewResult)controller.Index()).Model).PagingInfo.TotalPages;



            // Assert
            Assert.AreEqual(5, totalItems);
            Assert.AreEqual(1, currentPage);
            Assert.AreEqual(8, itemsPerPage);
            Assert.AreEqual(1, totalPages);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void MenusCtr_IndexCanPaginate()
        {
            // Arrange
            MenusController controller = SetUpController();

            // Act
            var result = (ListVM<Menu,MenuVM>)(controller.Index(1)).Model;



            // Assert
            Menu[] ingrArray1 = result.Entities.ToArray();
            Assert.IsTrue(ingrArray1.Length == 5);
            Assert.AreEqual("Old Name 1", ingrArray1[0].Name);
            Assert.AreEqual("Old Name 4", ingrArray1[3].Name);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void MenusCtr_DetailsRecipeIDIsNegative()
        {
            // Arrange
           MenusController controller = SetUpController();
            //AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No menu was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIControllerType.Menus.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void MenusCtr_DetailsWorksWithValidRecipeID()
        {
            // Arrange
           MenusController controller = SetUpController();
            //AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(1) as ViewResult;
            // Assert
            Assert.IsNotNull(view);

            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(LambAndLentil.UI.Models.MenuVM));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void MenusCtr_DetailsRecipeIDTooHigh()
        {
            // Arrange
           MenusController controller = SetUpController();
         //   AutoMapperConfigForTests.AMConfigForTests();
            ActionResult view = controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No menu was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIControllerType.Menus.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void MenusCtr_DetailsRecipeIDPastIntLimit()
        {
            // Arrange
           MenusController controller = SetUpController();
         //   AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult result = controller.Details(Int16.MaxValue + 1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void MenusCtr_DetailsRecipeIDIsZero()
        {
            // Arrange
           MenusController controller = SetUpController();
       //     AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No menu was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIControllerType.Menus.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Create")]
        public void MenusCtr_Create()
        {
            // Arrange
            MenusController controller = SetUpController();
            ViewResult view = controller.Create(UIViewType.Edit);


            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void MenusCtr_DeleteAFoundMenu()
        {
            // Arrange
            MenusController controller = SetUpController();

            // Act 
            var view = controller.Delete(1) as ViewResult;
 
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual(UIViewType.Details.ToString(), view.ViewName);
        }

       

        [TestMethod]
        [TestCategory("Delete")]
        public void MenusCtr_DeleteAnInvalidMenu()
        {
            // Arrange
            MenusController controller = SetUpController();

            // Act 
            var view = controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No menu was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIControllerType.Menus.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void MenusCtr_DeleteConfirmed()
        {
            // Arrange
            MenusController controller = SetUpController();
            // Act
            ActionResult result = controller.DeleteConfirmed(1) as ActionResult; 
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { controller = "Menus", Action = "Index" } } );
            // Assert 
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void Menus_Ctr_CanDeleteValidMenu()
        {
            // Arrange - create an menuVM
          
            MenuVM menu = new MenuVM { ID = 2, Name = "Test2" };

            // Arrange - create the mock repository
          Mock<IRepository<Menu,MenuVM>> mock = new Mock<IRepository<Menu,MenuVM>>();
            mock.Setup(m => m.Menu ).Returns(new MenuVM[]
            {
                new MenuVM {ID=1,Name="Test1"},

                menu,

                new MenuVM {ID=3,Name="Test3"},
            }.AsQueryable());
            mock.Setup(m => m.Remove(It.IsAny<MenuVM>())).Verifiable();
            // Arrange - create the controller
            MenusController controller = new MenusController();

            // Act - delete the menu
            ActionResult result = controller.DeleteConfirmed(menu.ID);

            AlertDecoratorResult adr = (AlertDecoratorResult)result;

            // Assert - ensure that the repository delete method was called with a correct Menu
            mock.Verify(m => m.Remove (menu));


            Assert.AreEqual("Test2 has been deleted", adr.Message);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void MenusCtr_CanEditMenu()
        {
            // Arrange
            MenusController controller = SetUpController();

            MenuVM menuVM = (MenuVM)mock.Object.Menu ;
            mock.Setup(c => c.Save(menuVM)).Verifiable();
            menuVM.Name = "First edited";

            // Act 

            ViewResult view1 = controller.Edit(1);
            MenuVM p1 = (MenuVM)view1.Model;
            ViewResult view2 = controller.Edit(2);
            MenuVM p2 = (MenuVM)view2.Model;
            ViewResult view3 = controller.Edit(3);
            MenuVM p3 = (MenuVM)view3.Model;


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
        public void MenusCtr_CannotEditNonexistentMenu()
        {
            //    // Arrange
            //    MenusController controller = SetUpController();
            //    // Act
            //    Menu result = (Menu)controller.Edit(8).ViewData.Model;
            //    // Assert
            //    Assert.IsNull(result);
            //}

            //[TestMethod]
            //public void MenusCtr_CreateReturnsNonNull()
            //{
            //    // Arrange
            //    MenusController controller = SetUpController();


            //    // Act
            //    ViewResult result = controller.Create(null) as ViewResult;

            //    // Assert
            //    Assert.IsNotNull(result);
        }


        private MenusController SetUpController()
        {
            // - create the mock repository
            mock = new Mock<IRepository<Menu,MenuVM>>();
            mock.Setup(m => m.Menu).Returns(new Menu[] {
                new Menu {ID = 1, Name = "Old Name 1" },
                new Menu {ID = 2, Name = "Old Name 2" },
                new Menu {ID = 3, Name = "Old Name 3" },
                new Menu {ID = 4, Name = "Old Name 4", },
                new Menu {ID = 5, Name = "Old Name 5" }
            }.AsQueryable());

            // Arrange - create a controller
            MenusController controller = new MenusController();
            controller.PageSize = 3;

            return controller;
        }



        private MenusController SetUpSimpleController()
        {
            // - create the mock repository
            Mock<IRepository> mock = new Mock<IRepository>();


            // Arrange - create a controller
            MenusController controller = new MenusController();
            // controller.PageSize = 3;

            return controller;
        }
         
    }
}

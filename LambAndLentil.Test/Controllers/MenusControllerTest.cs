﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
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
using LambAndLentil.Domain.Concrete;
using System.IO;

namespace LambAndLentil.Tests.Controllers
{

    [TestClass]
    [TestCategory("MenusController")]
    public class MenusControllerTest
    {
        private static IRepository<Menu, MenuVM> repo { get; set; }
        public static MapperConfiguration AutoMapperConfig { get; set; }
        private static MenusController controller { get; set; }

        private ListVM<Menu, MenuVM> vm { get; set; }

        public MenusControllerTest()
        {
            AutoMapperConfigForTests.InitializeMap();
            repo = new TestRepository<Menu, MenuVM>();
            vm = new ListVM<Menu, MenuVM>();
            controller = SetUpController();

        }

        private MenusController SetUpController()
        {
            vm.ListT = new List<Menu> {
                new Menu {ID = int.MaxValue, Name = "MenusController_Index_Test P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Menu {ID = int.MaxValue-1, Name = "MenusController_Index_Test P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new Menu {ID = int.MaxValue-2, Name = "MenusController_Index_Test P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new Menu {ID = int.MaxValue-3, Name = "MenusController_Index_Test P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Menu {ID = int.MaxValue-4, Name = "MenusController_Index_Test P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            }.AsQueryable();

            foreach (Menu ingredient in vm.ListT)
            {
                repo.AddT(ingredient);
            }

            controller = new MenusController(repo);
            controller.PageSize = 3;

            return controller;
        }


        [TestMethod]
        public void InheritsFromBaseControllerCorrectly()
        {

            Assert.IsNotNull("this is a placeholder");
        }

        [TestMethod]
        public void  IsPublic()
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
        public void  Index()
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
        public void IndexContainsAllMenus()
        {
            // Arrange   
            int repoCount = repo.Count();
            MenusController controller2 = new MenusController(repo);

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Menu, MenuVM>)(view1.Model)).ListTVM.Count();
            var firstName = ((ListVM<Menu, MenuVM>)(view1.Model)).ListTVM.FirstOrDefault().Name;
            var secondName = ((ListVM<Menu, MenuVM>)(view1.Model)).ListTVM.Skip(1).FirstOrDefault().Name;
            var thirdName = ((ListVM<Menu, MenuVM>)(view1.Model)).ListTVM.Skip(2).FirstOrDefault().Name;
            var fifthName = ((ListVM<Menu, MenuVM>)(view1.Model)).ListTVM.Skip(4).FirstOrDefault().Name;

            ViewResult view2 = controller2.Index(2);
            int count2 = ((ListVM<Menu, MenuVM>)(view2.Model)).ListTVM.Count();

            int count = count1 + count2;

            // Assert
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
            Assert.AreEqual(5, count1);
            Assert.AreEqual(0, count2);
            Assert.AreEqual(repoCount, count);
            Assert.AreEqual("Index", view1.ViewName);
            Assert.AreEqual("Index", view2.ViewName);
            Assert.AreEqual("MenusController_Index_Test P1", firstName);
            Assert.AreEqual("MenusController_Index_Test P2", secondName);
            Assert.AreEqual("MenusController_Index_Test P3", thirdName); 
            Assert.AreEqual("MenusController_Index_Test P5", fifthName);
            
        }


        [TestMethod]
        [TestCategory("Index")]
        public void  Index_FirstPageIsCorrect()
        {
            // Arrange    
            int repoCount = repo.Count();

            // Act
            ViewResult view = controller.Index(1);

            int count  = ((ListVM<Menu, MenuVM>)(view.Model)).ListTVM.Count();
            var firstName = ((ListVM<Menu, MenuVM>)(view.Model)).ListTVM.FirstOrDefault().Name;
            var secondName = ((ListVM<Menu, MenuVM>)(view.Model)).ListTVM.Skip(1).FirstOrDefault().Name;
            var thirdName = ((ListVM<Menu, MenuVM>)(view.Model)).ListTVM.Skip(2).FirstOrDefault().Name;
            var fourthName = ((ListVM<Menu, MenuVM>)(view.Model)).ListTVM.Skip(3).FirstOrDefault().Name;
            var fifthName = ((ListVM<Menu, MenuVM>)(view.Model)).ListTVM.Skip(4).FirstOrDefault().Name;


            // Assert
            Assert.IsNotNull(view); 
            Assert.AreEqual(repoCount, count );  
            Assert.AreEqual("Index", view.ViewName); 
            Assert.AreEqual("MenusController_Index_Test P1", firstName);
            Assert.AreEqual("MenusController_Index_Test P2", secondName);
            Assert.AreEqual("MenusController_Index_Test P3", thirdName);
            Assert.AreEqual("MenusController_Index_Test P4", fourthName);
            Assert.AreEqual("MenusController_Index_Test P5", fifthName);

        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        // currently we only have one page here
        public void  Index_SecondPageIsCorrect()
        {
          

        }

        [TestMethod]
        [TestCategory("Index")]
        public void  Index_CanSendPaginationViewModel()
        {

            // Arrange
            int count = repo.Count();

            // Act 
            ListVM<Menu, MenuVM> resultT = (ListVM<Menu, MenuVM>)((ViewResult)controller.Index(1)).Model;


            // Assert 
            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(1, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(count, pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void  Index_PagingInfoIsCorrect()
        {
            // Arrange
            int count = repo.Count();


            // Action
            int totalItems = ((ListVM<Menu, MenuVM>)((ViewResult)controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = ((ListVM<Menu, MenuVM>)((ViewResult)controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = ((ListVM<Menu, MenuVM>)((ViewResult)controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = ((ListVM<Menu, MenuVM>)((ViewResult)controller.Index()).Model).PagingInfo.TotalPages;
              
            // Assert
            Assert.AreEqual(count, totalItems);
            Assert.AreEqual(1, currentPage);
            Assert.AreEqual(8, itemsPerPage);
            Assert.AreEqual(1, totalPages);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void  CanPaginate()
        {
            // Arrange 
            int repoCount = repo.Count();
            // Act
            var result = (ListVM<Menu, MenuVM>)(controller.Index(1)).Model;
             
            // Assert
            
            Assert.AreEqual(repoCount, result.ListTVM.Count());
            Assert.AreEqual("MenusController_Index_Test P1", result.ListTVM.FirstOrDefault().Name);
            Assert.AreEqual("MenusController_Index_Test P4",  result.ListTVM.Skip(3).FirstOrDefault().Name);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  DetailsRecipeIDIsNegative()
        {
            // Arrange
            
            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Menu was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
           
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsWorksWithValidRecipeID()
        {
            // Arrange 

            // Act 
           ActionResult ar =   controller.Details(vm.ListT.FirstOrDefault().ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
           ViewResult view = (ViewResult)adr.InnerResult ;

            // Assert
            Assert.IsNotNull(ar); 
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(MenuVM));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  DetailsRecipeIDTooHigh()
        {
            // Arrange
             
            ActionResult view = controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Menu was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
             
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  DetailsRecipeIDPastIntLimit()
        {
            // Arrange
         
            // Act
            ViewResult result = controller.Details(Int16.MaxValue + 1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  DetailsRecipeIDIsZero()
        {
            // Arrange
            
            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Menu was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString()); 
        }

        [TestMethod]
        [TestCategory("Create")]
        public void  Create()
        {
            // Arrange

            // Act
            ViewResult view=controller.Create(UIViewType.Create);

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void  DeleteAFoundMenu()
        {
            // Arrange


            // Act 
            ActionResult ar = controller.Details(vm.ListT.FirstOrDefault().ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;
          

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
            Assert.AreEqual("Menu was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass); 
            Assert.AreEqual(UIViewType.Index.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString()); 
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void  DeleteConfirmed()
        {
            // Arrange
            
            // Act
            ActionResult result = controller.DeleteConfirmed(vm.ListT.FirstOrDefault().ID) as ActionResult;
            // improve this test when I do some more route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { controller = "Menus", Action = "Index" } } );
            // Assert 
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void  CanDeleteValidMenu()
        {
            // Arrange 

            MenuVM menu = new MenuVM { ID = 2, Name = "Test2", Description="test MenusControllerTest.CanDeleteValidMenu"  };
            repo.AddTVM(menu);
            int  beginningCount = repo.Count();

            // Act - delete the menu
            ActionResult result = controller.DeleteConfirmed(menu.ID); 
            AlertDecoratorResult adr = (AlertDecoratorResult)result;

            // Assert
            Assert.AreEqual(beginningCount - 1, repo.Count());
            Assert.AreEqual("Test2 has been deleted", adr.Message);
        }

        [Ignore]   // look into why this is not working
        [TestMethod]
        [TestCategory("Edit")]
        public void  CanEditMenu()
        {
            // Arrange
            MenuVM menuVM = new MenuVM
            {
                ID = 1,
                Name = "test MenuControllerTest.CanEditMenu",
                Description = "test MenuControllerTest.CanEditMenu"
            };
            repo.SaveTVM(menuVM);

            // Act 
            menuVM.Name = "Name has been changed";

            ViewResult view1 = controller.Edit(1);

            var returnedMenuVM = (MenuVM)(view1.Model);
             

            // Assert 
            Assert.IsNotNull(view1); 
            Assert.AreEqual("Name has been changed", returnedMenuVM.Name);
            //Assert.AreEqual(menuVM.Description, returnedMenuVm.Description);
            //Assert.AreEqual(menuVM.CreationDate, returnedMenuVm.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedMenu()
        {
            // Arrange
            MenusController indexController = new MenusController(repo);
            MenusController controller2 = new MenusController(repo);
            MenusController controller3 = new MenusController(repo);


            MenuVM vm = new MenuVM();
            vm.Name = "0000 test";
            vm.ID = int.MaxValue - 100;
            vm.Description = "test MenusControllerShould.SaveEditedMenu";

            // Act 
            ActionResult ar1 = controller.PostEdit(vm);


            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = 7777;
            ActionResult ar2 = controller2.PostEdit(vm);
            ViewResult view2 = controller3.Index();
            ListVM<Menu, MenuVM> listVM2 = (ListVM<Menu, MenuVM>)view2.Model;
            MenuVM vm3 = (from m in listVM2.ListTVM
                                where m.Name == "0000 test Edited"
                                select m).AsQueryable().FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited", vm3.Name);
            Assert.AreEqual(7777, vm3.ID);

        }

        [Ignore]  // look into why this is not working
        [TestMethod]
        [TestCategory("Edit")]
        public void CanPostEditMenu()
        {
            // Arrange
            MenuVM menuVM = new MenuVM
            {
                ID = 1,
                Name = "test MenuControllerTest.CanEditMenu",
                Description = "test MenuControllerTest.CanEditMenu"
            };
            repo.AddTVM(menuVM);

            // Act 
            menuVM.Name = "Name has been changed";

            ViewResult view1 = controller.Edit(1);

            MenuVM returnedMenuVm = repo.GetTVMById(1);

            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual("Name has been changed", returnedMenuVm.Name);
            Assert.AreEqual(menuVM.Description, returnedMenuVm.Description);
            Assert.AreEqual(menuVM.CreationDate, returnedMenuVm.CreationDate);
        }



        [TestMethod]
        [TestCategory("Edit")]
        public void  CannotEditNonexistentMenu()
        {
            // Arrange
           
            // Act
            Menu result = (Menu)controller.Edit(8).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void  CreateReturnsNonNull()
        {
            // Arrange 

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
        public void CorrectMenuPropertiesAreBoundInEdit()
        {
            Assert.Fail();
        }

        [TestCleanup()]
        public   void TestCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Menu\";

            IEnumerable<string> files=Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            } 

        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            new MenusControllerTest().TestCleanup(); 
        }
    }
}

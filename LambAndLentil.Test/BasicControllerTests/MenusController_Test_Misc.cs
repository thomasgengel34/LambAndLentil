using System;
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

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("MenusController")]
    public class MenusController_Test_Misc: MenusController_Test_Should
    {
         

        [TestMethod]
        public void InheritsFromBaseControllerCorrectly()
        {

            Assert.IsNotNull("this is a placeholder");
        }

        [TestMethod]
        public void IsPublic()
        {
            // Arrange 

            // Act
            Type type = Controller.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(true, isPublic);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void Index()
        {
            // Arrange
           

            // Act
            ViewResult result = Controller.Index(1) as ViewResult;
            ViewResult result1 = Controller.Index(2) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void IndexContainsAllMenus()
        {
            // Arrange   
            int repoCount = Repo.Count();
            MenusController controller2 = new MenusController(Repo);

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = ((ListEntity<Menu>)(view1.Model)).ListT.Count();
            var firstName = ((ListEntity<Menu>)(view1.Model)).ListT.FirstOrDefault().Name;
            var secondName = ((ListEntity<Menu>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name;
            var thirdName = ((ListEntity<Menu>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name;
            var fifthName = ((ListEntity<Menu>)(view1.Model)).ListT.Skip(4).FirstOrDefault().Name;

            ViewResult view2 = controller2.Index(2);
            int count2 = ((ListEntity<Menu>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
            Assert.AreEqual(6, count1);
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
        public void Index_FirstPageIsCorrect()
        {
            // Arrange    
            int repoCount = Repo.Count();

            // Act
            ViewResult view = Controller.Index(1);

            int count = ((ListEntity<Menu>)(view.Model)).ListT.Count();
            var firstName = ((ListEntity<Menu>)(view.Model)).ListT.FirstOrDefault().Name;
            var secondName = ((ListEntity<Menu>)(view.Model)).ListT.Skip(1).FirstOrDefault().Name;
            var thirdName = ((ListEntity<Menu>)(view.Model)).ListT.Skip(2).FirstOrDefault().Name;
            var fourthName = ((ListEntity<Menu>)(view.Model)).ListT.Skip(3).FirstOrDefault().Name;
            var fifthName = ((ListEntity<Menu>)(view.Model)).ListT.Skip(4).FirstOrDefault().Name;


            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual(repoCount, count);
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
        public void Index_SecondPageIsCorrect()
        {


        }

        [TestMethod]
        [TestCategory("Index")]
        public void Index_CanSendPaginationViewModel()
        {

            // Arrange
            int count = Repo.Count();

            // Act 
            ListEntity<Menu> resultT = (ListEntity<Menu>)((ViewResult)Controller.Index(1)).Model;


            // Assert 
            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(1, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(count, pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void Index_PagingInfoIsCorrect()
        {
            // Arrange
            int count = Repo.Count();


            // Action
            int totalItems = ((ListEntity<Menu>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = ((ListEntity<Menu>)((ViewResult)Controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = ((ListEntity<Menu>)((ViewResult)Controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = ((ListEntity<Menu>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalPages;

            // Assert
            Assert.AreEqual(count, totalItems);
            Assert.AreEqual(1, currentPage);
            Assert.AreEqual(8, itemsPerPage);
            Assert.AreEqual(1, totalPages);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate()
        {
            // Arrange 
            int repoCount = Repo.Count();
            // Act
            var result = (ListEntity<Menu>)(Controller.Index(1)).Model;

            // Assert

            Assert.AreEqual(repoCount, result.ListT.Count());
            Assert.AreEqual("MenusController_Index_Test P1", result.ListT.FirstOrDefault().Name);
            Assert.AreEqual("MenusController_Index_Test P4", result.ListT.Skip(3).FirstOrDefault().Name);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsRecipeIDIsNegative()
        {
            // Arrange

            // Act
            ViewResult view = Controller.Details(0) as ViewResult;
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
            ActionResult ar = Controller.Details(ListEntity.ListT.FirstOrDefault().ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Menu));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsRecipeIDTooHigh()
        {
            // Arrange

            ActionResult view = Controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Menu was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());

        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsRecipeIDPastIntLimit()
        {
            // Arrange

            // Act
            ViewResult result = Controller.Details(Int16.MaxValue + 1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsRecipeIDIsZero()
        {
            // Arrange

            // Act
            ViewResult view = Controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Menu was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Create")]
        public void Create()
        {
            // Arrange

            // Act
            ViewResult view = Controller.Create(UIViewType.Create);

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteAFoundMenu()
        {
            // Arrange


            // Act 
            ActionResult ar = Controller.Details(ListEntity.ListT.FirstOrDefault().ID);
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

            // Act 
            var view = Controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Menu was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual(UIViewType.Index.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteConfirmed()
        {
            // Arrange

            // Act
            ActionResult result = Controller.DeleteConfirmed(ListEntity.ListT.FirstOrDefault().ID) as ActionResult;
            // improve this test when I do some more route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { controller = "Menus", Action = "Index" } } );
            // Assert 
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void CanDeleteValidMenu()
        {
            // Arrange 

            Menu menu = new Menu { ID = 2, Name = "Test2", Description = "test MenusControllerTest.CanDeleteValidMenu" };
            Repo.Add(menu);
            int beginningCount = Repo.Count();

            // Act - delete the menu
            ActionResult result = Controller.DeleteConfirmed(menu.ID);
            AlertDecoratorResult adr = (AlertDecoratorResult)result;

            // Assert
            Assert.AreEqual(beginningCount - 1, Repo.Count());
            Assert.AreEqual("Test2 has been deleted", adr.Message);
        }

        [Ignore]   // look into why this is not working
        [TestMethod]
        [TestCategory("Edit")]
        public void CanEditMenu()
        {
            // Arrange
            Menu menuVM = new Menu
            {
                ID = 1,
                Name = "test MenuControllerTest.CanEditMenu",
                Description = "test MenuControllerTest.CanEditMenu"
            };
            Repo.Save(menuVM);

            // Act 
            menuVM.Name = "Name has been changed";

            ViewResult view1 = (ViewResult)Controller.Edit(1);

            var returnedMenu = (Menu)(view1.Model);


            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual("Name has been changed", returnedMenu.Name);
            //Assert.AreEqual(menuVM.Description, returnedMenulist.Description);
            //Assert.AreEqual(menuVM.CreationDate, returnedMenulist.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedMenu()
        {
            // Arrange
            MenusController indexController = new MenusController(Repo);
            MenusController controller2 = new MenusController(Repo);
            MenusController controller3 = new MenusController(Repo);


            Menu vm = new Menu
            {
                Name = "0000 test",
                ID = int.MaxValue - 100,
                Description = "test MenusControllerShould.SaveEditedMenu"
            };

            // Act 
            ActionResult ar1 = Controller.PostEdit(vm);


            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = 7777;
            ActionResult ar2 = controller2.PostEdit(vm);
            ViewResult view2 = controller3.Index();
            ListEntity<Menu> list2 = (ListEntity<Menu>)view2.Model;
            Menu vm3 = (from m in list2.ListT
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
            Menu menuVM = new Menu
            {
                ID = 1,
                Name = "test MenuControllerTest.CanEditMenu",
                Description = "test MenuControllerTest.CanEditMenu"
            };
            Repo.Add(menuVM);

            // Act 
            menuVM.Name = "Name has been changed";

            ViewResult view1 = (ViewResult)Controller.Edit(1);

            Menu returnedMenulist = Repo.GetById(1);

            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual("Name has been changed", returnedMenulist.Name);
            Assert.AreEqual(menuVM.Description, returnedMenulist.Description);
            Assert.AreEqual(menuVM.CreationDate, returnedMenulist.CreationDate);
        }


        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void CannotEditNonexistentMenu()
        {
            // Arrange

            // Act
            Menu result = (Menu)((ViewResult)Controller.Edit(8)).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreateReturnsNonNull()
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
        public void CorrectMenuPropertiesAreBoundInEdit()
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
            Assert.AreEqual("LambAndLentil.UI.Controllers.MenusController", MenusController_Test_Should.Controller.ToString());
        }
         
    }
}

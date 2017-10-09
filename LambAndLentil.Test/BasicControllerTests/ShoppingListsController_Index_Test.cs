﻿using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("ShoppingListsController")]
    public class ShoppingListsController_Index_Test:ShoppingListsController_Test_Should
    {
          



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
        }

       

        [TestMethod]
        [TestCategory("Index")]
        public void ShoppingListsCtr_Index1()
        {
            // Arrange


            // Act
            ViewResult result = Controller.Index(1) as ViewResult;
            ViewResult result1 = Controller.Index(2) as ViewResult;

            // Assert 
            Assert.IsNotNull(result1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllShoppingListsView2NotNull()
        {
            // Arrange 

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<ShoppingList>)(view1.Model)).ListT.Count();

            ViewResult view2 = Controller.Index(2);

            int count2 = (( ListEntity<ShoppingList>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert 
            Assert.IsNotNull(view2);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllShoppingListsView1Count5()
        {
            // Arrange 

            // Act
             ListEntity = ( ListEntity<ShoppingList>)(Controller.Index(1)).Model;
            ShoppingList[] ingrArray1 =  ListEntity.ListT.ToArray();
            int count1 = ingrArray1.Count();

            // Assert 
            Assert.AreEqual(5, count1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllShoppingListsView2Count0()
        {
            // Arrange 
             ListEntity = ( ListEntity<ShoppingList>)(Controller.Index(1)).Model;
            ShoppingList[] ingrArray1 =  ListEntity.ListT.ToArray();

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<ShoppingList>)(view1.Model)).ListT.Count();

            ViewResult view2 = Controller.Index(2);

            int count2 = (( ListEntity<ShoppingList>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert  
            Assert.AreEqual(0, count2);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllShoppingListsView1NameIsIndex()
        {
            // Arrange  

            // Act
            ViewResult view = Controller.Index(1);


            // Assert    
            Assert.AreEqual("Index", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllShoppingListsView2NameIsIndex()
        {
            // Arrange 
             ListEntity = ( ListEntity<ShoppingList>)(Controller.Index(1)).Model;
            ShoppingList[] ingrArray1 =  ListEntity.ListT.ToArray();

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<ShoppingList>)(view1.Model)).ListT.Count();

            ViewResult view2 = Controller.Index(2);

            int count2 = (( ListEntity<ShoppingList>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert   
            Assert.AreEqual("Index", view2.ViewName);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsNotNull()
        {
            // Arrange 
             ListEntity = ( ListEntity<ShoppingList>)(Controller.Index(1)).Model;
            ShoppingList[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<ShoppingList>)(view1.Model)).ListT.Count();

            // Assert
            Assert.IsNotNull(view1);

        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsCorrectCountIsFive()
        {
            // Arrange 
             ListEntity = ( ListEntity<ShoppingList>)(Controller.Index(1)).Model;
            ShoppingList[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);
            int count1 = (( ListEntity<ShoppingList>)(view1.Model)).ListT.Count();

            // Assert 
            Assert.AreEqual(5, count1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageNameIsIndex()
        {
            // Arrange 
            ListEntity = ( ListEntity<ShoppingList>)(Controller.Index(1)).Model;
            ShoppingList[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<ShoppingList>)(view1.Model)).ListT.Count();

            // Assert  
            Assert.AreEqual("Index", view1.ViewName);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void FirstItemNameIsCorrect()
        {
            // Arrange 
             ListEntity = ( ListEntity<ShoppingList>)(Controller.Index(1)).Model;
            ShoppingList[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<ShoppingList>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("ShoppingListsController_Index_Test P1", (( ListEntity<ShoppingList>)(view1.Model)).ListT.FirstOrDefault().Name);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void FirstShoppingListAddedByUserIsCorrect()
        {
            // Arrange 
             ListEntity = ( ListEntity<ShoppingList>)(Controller.Index(1)).Model;
            ShoppingList[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);

            // Assert   
            Assert.AreEqual("John Doe", (( ListEntity<ShoppingList>)(view1.Model)).ListT.FirstOrDefault().AddedByUser);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstModifiedByUserIsCorrect()
        {
            // Arrange 
             ListEntity = ( ListEntity<ShoppingList>)(Controller.Index(1)).Model;
            ShoppingList[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);
            string userName = WindowsIdentity.GetCurrent().Name;
            // Assert   
            Assert.AreEqual(userName, (( ListEntity<ShoppingList>)(view1.Model)).ListT.FirstOrDefault().ModifiedByUser);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstCreationDateIsCorrect()
        {
            // Arrange 
             ListEntity = ( ListEntity<ShoppingList>)(Controller.Index(1)).Model;
            ShoppingList[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MinValue, (( ListEntity<ShoppingList>)(view1.Model)).ListT.FirstOrDefault().CreationDate);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void Index_FirstModifiedDateIsCorrect()
        {
            // Arrange 
             ListEntity = ( ListEntity<ShoppingList>)(Controller.Index(1)).Model;
            ShoppingList[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MaxValue.AddYears(-10), (( ListEntity<ShoppingList>)(view1.Model)).ListT.FirstOrDefault().ModifiedDate);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void SecondItemNameIsCorrect()
        {
            // Arrange 
             ListEntity = ( ListEntity<ShoppingList>)(Controller.Index(1)).Model;
            ShoppingList[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<ShoppingList>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("ShoppingListsController_Index_Test P2", (( ListEntity<ShoppingList>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void ThirdItemNameIsCorrect()
        {
            // Arrange 
             ListEntity = ( ListEntity<ShoppingList>)(Controller.Index(1)).Model;
            ShoppingList[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<ShoppingList>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("ShoppingListsController_Index_Test P3", (( ListEntity<ShoppingList>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);
        }



        [TestMethod]
        [TestCategory("Index")]
        // currently we only have one page here
        public void SecondPageIsCorrect()
        {
            //TODO: add enough test ingredients to test the second page
        }



        [TestMethod]
        [TestCategory("Index")]
        public void ShoppingListsCtr_IndexCanPaginate_ArrayLengthIsCorrect()
        {
            // Arrange


            // Act
            var result = ( ListEntity<ShoppingList>)(Controller.Index(1)).Model;

            // Assert 
            Assert.IsTrue(result.ListT.Count() == 5);
        }

        // [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate_ArrayFirstItemNameIsCorrect()
        {
            // Arrange


            // Act
            var result = ( ListEntity<ShoppingList>)(Controller.Index(1)).Model;
            ShoppingList[] ingrArray1 = result.ListT.ToArray();

            // Assert  
            Assert.AreEqual("ShoppingListsController_Index_Test P1", ingrArray1[0].Name);
        }

        // [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate_ArrayThirdItemNameIsCorrect()
        {
            // Arrange 

            // Act
            var result = ( ListEntity<ShoppingList>)(Controller.Index(1)).Model;
            ShoppingList[] ingrArray1 = result.ListT.ToArray();

            // Assert  
            Assert.AreEqual("ShoppingListsController_Index_Test P3", ingrArray1[2].Name);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void CanSendPaginationViewModel_CurrentPageCountCorrect()
        {
            // Arrange 

            // Act 
             ListEntity<ShoppingList> resultT = ( ListEntity<ShoppingList>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert  
            Assert.AreEqual(2, pageInfoT.CurrentPage);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ShoppingListsCtr_Index_CanSendPaginationViewModel_ItemsPerPageCorrect()
        {
            // Arrange


            // Act 
             ListEntity<ShoppingList> resultT = ( ListEntity<ShoppingList>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert   
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void CanSendPaginationViewModel_TotalItemsCorrect()
        {
            // Arrange 

            // Act 
             ListEntity<ShoppingList> resultT = ( ListEntity<ShoppingList>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;
            //   PagingInfo pageInfoT = ListEntity.PagingInfo;

            // Assert 
            Assert.AreEqual(5, pageInfoT.TotalItems);
        }

        // [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void ShoppingListsCtr_Index_CanSendPaginationViewModel_TotalPagesCorrect()
        {
            // Arrange


            // Act 
             ListEntity<ShoppingList> resultT = ( ListEntity<ShoppingList>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert 
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }


 


        [Ignore]
        [TestMethod]
        public void FlagAnShoppingListFlaggedInAPerson()
        {
            Assert.Fail();
        }

         [Ignore]
        [TestMethod]
        public void FlagAnShoppingListFlaggedInTwoPersons()
        {
            Assert.Fail();
        }

          [Ignore]
        [TestMethod]
        public void WhenAFlagHasBeenRemovedFromOnePersonStillThereForSecondFlaggedPerson()
        {
            Assert.Fail();
        }

     

 
        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllShoppingLists()
        {
            // Arrange

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<ShoppingList>)(view1.Model)).ListT.Count();

            ViewResult view2 = Controller.Index(2);

            int count2 = (( ListEntity<ShoppingList>)(view2.Model)).ListT.Count();

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
             ListEntity<ShoppingList>  ListEntity = new  ListEntity<ShoppingList>();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);
            int count1 = (( ListEntity<ShoppingList>)(view1.Model)).ListT.Count();

            // Assert
            Assert.IsNotNull(view1);
            Assert.AreEqual(5, count1);
            Assert.AreEqual("Index", view1.ViewName);

            Assert.AreEqual("ShoppingListsController_Index_Test P1", (( ListEntity<ShoppingList>)(view1.Model)).ListT.FirstOrDefault().Name);
            Assert.AreEqual("ShoppingListsController_Index_Test P2", (( ListEntity<ShoppingList>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual("ShoppingListsController_Index_Test P3", (( ListEntity<ShoppingList>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);
        }

        // [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        // currently we only have one page here
        public void ShoppingListsCtr_Index_SecondPageIsCorrect()
        {

        }

        // [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void CanSendPaginationViewModel()
        {

            // Arrange


            // Act 
             ListEntity<ShoppingList> resultT = ( ListEntity<ShoppingList>)((ViewResult)Controller.Index(2)).Model;


            // Assert 
            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(5, pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }

        // [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void PagingInfoIsCorrect()
        {
            // Arrange 

            // Action
            int totalItems = (( ListEntity<ShoppingList>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = (( ListEntity<ShoppingList>)((ViewResult)Controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = (( ListEntity<ShoppingList>)((ViewResult)Controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = (( ListEntity<ShoppingList>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalPages;



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
            var result = ( ListEntity<ShoppingList>)(Controller.Index(1)).Model;

            // Assert 
            Assert.IsTrue(result.ListT.Count() == 5);
            Assert.AreEqual("ShoppingListsController_Index_Test P1", result.ListT.FirstOrDefault().Name);
            Assert.AreEqual("ShoppingListsController_Index_Test P4", result.ListT.Skip(3).FirstOrDefault().Name);
        } 
    }
}

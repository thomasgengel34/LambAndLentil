﻿using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("PersonsController")]
    [TestCategory("Index")]
    public class PersonsController_Index_Test:PersonsController_Test_Should
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
        public void PersonsCtr_Index1()
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
        public void ContainsAllPersonsView2NotNull()
        {
            // Arrange 

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<Person>)(view1.Model)).ListT.Count();

            ViewResult view2 = Controller.Index(2);

            int count2 = (( ListEntity<Person>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert 
            Assert.IsNotNull(view2);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllPersonsView1Count6()
        {
            // Arrange 

            // Act
            ListEntity= ( ListEntity<Person>)(Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            int count1 = ingrArray1.Count();

            // Assert 
            Assert.AreEqual(6, count1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllPersonsView2Count0()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)(Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<Person>)(view1.Model)).ListT.Count();

            ViewResult view2 = Controller.Index(2);

            int count2 = (( ListEntity<Person>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert  
            Assert.AreEqual(0, count2);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllPersonsView1NameIsIndex()
        {
            // Arrange  

            // Act
            ViewResult view = Controller.Index(1);


            // Assert    
            Assert.AreEqual("Index", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllPersonsView2NameIsIndex()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)(Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<Person>)(view1.Model)).ListT.Count();

            ViewResult view2 = Controller.Index(2);

            int count2 = (( ListEntity<Person>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert   
            Assert.AreEqual("Index", view2.ViewName);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsNotNull()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)(Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<Person>)(view1.Model)).ListT.Count();

            // Assert
            Assert.IsNotNull(view1);

        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsCorrectCountIsFive()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)(Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);
            int count1 = (( ListEntity<Person>)(view1.Model)).ListT.Count();

            // Assert 
            Assert.AreEqual(6, count1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageNameIsIndex()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)(Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<Person>)(view1.Model)).ListT.Count();

            // Assert  
            Assert.AreEqual("Index", view1.ViewName);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void FirstItemNameIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)(Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<Person>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("LambAndLentil.Domain.Entities.Person ControllerTest1", (( ListEntity<Person>)(view1.Model)).ListT.FirstOrDefault().Name);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void FirstPersonAddedByUserIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)(Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);

            // Assert   
            Assert.AreEqual("John Doe", (( ListEntity<Person>)(view1.Model)).ListT.FirstOrDefault().AddedByUser);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstModifiedByUserIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)(Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);
            string userName = WindowsIdentity.GetCurrent().Name;
            // Assert   
            Assert.AreEqual(userName, (( ListEntity<Person>)(view1.Model)).ListT.FirstOrDefault().ModifiedByUser);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstCreationDateIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)(Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MinValue, (( ListEntity<Person>)(view1.Model)).ListT.FirstOrDefault().CreationDate);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void Index_FirstModifiedDateIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)(Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MaxValue.AddYears(-10), (( ListEntity<Person>)(view1.Model)).ListT.FirstOrDefault().ModifiedDate);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void SecondItemNameIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)(Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<Person>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("LambAndLentil.Domain.Entities.Person ControllerTest2", (( ListEntity<Person>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void ThirdItemNameIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)(Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<Person>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("LambAndLentil.Domain.Entities.Person ControllerTest3", (( ListEntity<Person>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);
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
        public void PersonsCtr_IndexCanPaginate_ArrayLengthIsCorrect()
        {
            // Arrange


            // Act
            var result = ( ListEntity<Person>)(Controller.Index(1)).Model;

            // Assert 
            Assert.AreEqual(6,result.ListT.Count());
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate_ArrayFirstItemNameIsCorrect()
        {
            // Arrange


            // Act
            var result = ( ListEntity<Person>)(Controller.Index(1)).Model;
            Person[] ingrArray1 = result.ListT.ToArray();

            // Assert  
            Assert.AreEqual("PersonsController_Index_Test P1", ingrArray1[0].Name);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate_ArrayThirdItemNameIsCorrect()
        {
            // Arrange 

            // Act
            var result = ( ListEntity<Person>)(Controller.Index(1)).Model;
            Person[] ingrArray1 = result.ListT.ToArray();

            // Assert  
            Assert.AreEqual("PersonsController_Index_Test P3", ingrArray1[2].Name);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void CanSendPaginationViewModel_CurrentPageCountCorrect()
        {
            // Arrange 

            // Act 
             ListEntity<Person> resultT = ( ListEntity<Person>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert  
            Assert.AreEqual(2, pageInfoT.CurrentPage);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void PersonsCtr_Index_CanSendPaginationViewModel_ItemsPerPageCorrect()
        {
            // Arrange


            // Act 
             ListEntity<Person> resultT = ( ListEntity<Person>)((ViewResult)Controller.Index(2)).Model;
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
             ListEntity<Person> resultT = ( ListEntity<Person>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;
            //   PagingInfo pageInfoT =ListEntity.PagingInfo;

            // Assert 
            Assert.AreEqual(6, pageInfoT.TotalItems);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void PersonsCtr_Index_CanSendPaginationViewModel_TotalPagesCorrect()
        {
            // Arrange


            // Act 
             ListEntity<Person> resultT = ( ListEntity<Person>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert 
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }


       


        [Ignore]
        [TestMethod]
        public void FlagAnPersonFlaggedInAPerson()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void FlagAnPersonFlaggedInTwoPersons()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void WhenAFlagHasBeenRemovedFromOnePersonStillThereForSecondFlaggedPerson()
        {
            Assert.Fail();
        } 
    }
}

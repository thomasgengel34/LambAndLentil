using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
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
        public void Index()
        {
            // Arrange
            IGenericController<Person> Controller2 = (IGenericController<Person>)(new PersonsController(Repo));

            // Act
            ViewResult view1 = Controller.Index(1) as ViewResult;
            ViewResult view2 = Controller2.Index(2) as ViewResult;

            // Assert
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
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
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Person>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

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
            ListEntity= ( ListEntity<Person>)((ViewResult)Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            int count1 = ingrArray1.Count();

            // Assert 
            Assert.AreEqual(7, count1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllPersonsView2Count0()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)((ViewResult)Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Person>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

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
            ViewResult view = (ViewResult)Controller.Index(1);


            // Assert    
            Assert.AreEqual("Index", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllPersonsView2NameIsIndex()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)((ViewResult)Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Person>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

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
            ListEntity= ( ListEntity<Person>)((ViewResult)Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Person>)(view1.Model)).ListT.Count();

            // Assert
            Assert.IsNotNull(view1);

        }
         

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageNameIsIndex()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)((ViewResult)Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Person>)(view1.Model)).ListT.Count();

            // Assert  
            Assert.AreEqual("Index", view1.ViewName);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void FirstItemNameIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)((ViewResult)Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Person>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("LambAndLentil.Domain.Entities.Person ControllerTest1", (( ListEntity<Person>)(view1.Model)).ListT.FirstOrDefault().Name);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void FirstPersonAddedByUserIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)((ViewResult)Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            // Assert   
            Assert.AreEqual("John Doe", (( ListEntity<Person>)(view1.Model)).ListT.FirstOrDefault().AddedByUser);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstModifiedByUserIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)((ViewResult)Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);
            string userName = WindowsIdentity.GetCurrent().Name;
            // Assert   
            Assert.AreEqual(userName, (( ListEntity<Person>)(view1.Model)).ListT.FirstOrDefault().ModifiedByUser);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstCreationDateIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)((ViewResult)Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MinValue, (( ListEntity<Person>)(view1.Model)).ListT.FirstOrDefault().CreationDate);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void Index_FirstModifiedDateIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)((ViewResult)Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MaxValue.AddYears(-10), (( ListEntity<Person>)(view1.Model)).ListT.FirstOrDefault().ModifiedDate);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void SecondItemNameIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)((ViewResult)Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Person>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("LambAndLentil.Domain.Entities.Person ControllerTest2", (( ListEntity<Person>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void ThirdItemNameIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Person>)((ViewResult)Controller.Index(1)).Model;
            Person[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

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
        public void CanPaginateArrayLengthIsCorrect() => BaseCanPaginateArrayLengthIsCorrect(Repo, Controller);


        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate_ArrayFirstItemNameIsCorrect()
        {
            // Arrange


            // Act
            var result = (ListEntity<Person>)((ViewResult)Controller.Index(1)).Model;
            Person[] ingrArray1 = result.ListT.ToArray();

            // Assert  
            Assert.AreEqual("LambAndLentil.Domain.Entities.Person ControllerTest1", ingrArray1[0].Name);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate_ArrayThirdItemNameIsCorrect()
        {
            // Arrange 

            // Act
            var result = ( ListEntity<Person>)((ViewResult)Controller.Index(1)).Model;
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
        public void  CanSendPaginationViewModel_ItemsPerPageCorrect()
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
        public void CanSendPaginationViewModel_TotalItemsCorrect() => BaseCanSendPaginationViewModel_TotalItemsCorrect(Repo, Controller, UIControllerType.Persons);

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

      

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllPersons()
        {
            // Arrange 
            IGenericController<Person> Controller2 = (IGenericController<Person>)(new PersonsController(Repo));

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<Person>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller2.Index(2);

            int count2 = ((ListEntity<Person>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
            Assert.AreEqual(7, count1);
            Assert.AreEqual(0, count2);
            Assert.AreEqual(7, count);
            Assert.AreEqual("Index", view1.ViewName);
            Assert.AreEqual("Index", view2.ViewName);

            Assert.AreEqual("LambAndLentil.Domain.Entities.Person ControllerTest1", ((ListEntity<Person>)(view1.Model)).ListT.FirstOrDefault().Name);
            Assert.AreEqual("LambAndLentil.Domain.Entities.Person ControllerTest2", ((ListEntity<Person>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual("LambAndLentil.Domain.Entities.Person ControllerTest3", ((ListEntity<Person>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);
            Assert.AreEqual("LambAndLentil.Domain.Entities.Person ControllerTest5", ((ListEntity<Person>)(view1.Model)).ListT.Skip(4).FirstOrDefault().Name);

        }


        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsCorrect()
        {
            // Arrange 
            Controller.PageSize = 8;


            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<Person>)(view1.Model)).ListT.Count();

            // Assert
            Assert.IsNotNull(view1);
            Assert.AreEqual(7, count1);
            Assert.AreEqual("Index", view1.ViewName);

            Assert.AreEqual("LambAndLentil.Domain.Entities.Person ControllerTest1", ((ListEntity<Person>)(view1.Model)).ListT.FirstOrDefault().Name);
            Assert.AreEqual("LambAndLentil.Domain.Entities.Person ControllerTest2", ((ListEntity<Person>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual("LambAndLentil.Domain.Entities.Person ControllerTest3", ((ListEntity<Person>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);


        }

       

        [TestMethod]
        [TestCategory("Index")]
        public void Index_CanSendPaginationViewModel()
        {
            // Arrange 

            // Act 
            ListEntity<Person> resultT = (ListEntity<Person>)((ViewResult)Controller.Index(2)).Model;

            // Assert
            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(7, pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void PagingInfoIsCorrect()
        {
            BasePagingInfoIsCorrect(Repo, Controller, UIControllerType.Recipes);
            //// Arrange 

            //// Action
            //int totalItems = ((ListEntity<Person>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalItems;
            //int currentPage = ((ListEntity<Person>)((ViewResult)Controller.Index()).Model).PagingInfo.CurrentPage;
            //int itemsPerPage = ((ListEntity<Person>)((ViewResult)Controller.Index()).Model).PagingInfo.ItemsPerPage;
            //int totalPages = ((ListEntity<Person>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalPages;

            //// Assert
            //Assert.AreEqual(7, totalItems);
            //Assert.AreEqual(1, currentPage);
            //Assert.AreEqual(8, itemsPerPage);
            //Assert.AreEqual(1, totalPages);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate()
        {
            // Arrange

            // Act
            var result = (ListEntity<Person>)((ViewResult)Controller.Index(1)).Model;
            var ListEntity = result.ListT;

            // Assert 
            Assert.AreEqual(7, ListEntity.Count());
            Assert.AreEqual("LambAndLentil.Domain.Entities.Person ControllerTest1", ListEntity.FirstOrDefault().Name);
            Assert.AreEqual("LambAndLentil.Domain.Entities.Person ControllerTest3", ListEntity.Skip(2).FirstOrDefault().Name);
        }
    }
}

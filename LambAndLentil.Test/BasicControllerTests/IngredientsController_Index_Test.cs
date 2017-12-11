using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI;
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
    [TestCategory("IngredientsController")]
    [TestCategory("Index")]
    public class IngredientsController_Index_Test:IngredientsController_Test_Should
    { 

        public IngredientsController_Index_Test()
        { 
        }



        [TestMethod] 
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
        public void  Index1()
        {
            // Arrange


            // Act
            ViewResult result = Controller.Index(1) as ViewResult;
            ViewResult result1 = Controller.Index(2) as ViewResult;

            // Assert 
            Assert.IsNotNull(result1);
        }

        [TestMethod] 
        public void ContainsAllIngredientsView2NotNull()
        {
            // Arrange 

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Ingredient>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

            int count2 = (( ListEntity<Ingredient>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert 
            Assert.IsNotNull(view2);
        }

        [TestMethod] 
        public void ContainsAllIngredientsView1Count5()
        {
            // Arrange 

            // Act
            ListEntity= ( ListEntity<Ingredient>)((ViewResult)(Controller.Index(1))).Model;
            Ingredient[] ingrArray1 = ListEntity.ListT.ToArray();
            int count1 = ingrArray1.Count();

            // Assert 
            Assert.AreEqual(6, count1);
        }

        [TestMethod] 
        public void ContainsAllIngredientsView2Count0()
        {
            // Arrange 
            ListEntity= ( ListEntity<Ingredient>)((ViewResult)Controller.Index(1)).Model;
            Ingredient[] ingrArray1 = ListEntity.ListT.ToArray();

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Ingredient>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

            int count2 = (( ListEntity<Ingredient>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert  
            Assert.AreEqual(0, count2);
        }


        [TestMethod] 
        public void ContainsAllIngredientsView1NameIsIndex()
        {
            // Arrange  

            // Act
            ViewResult view = (ViewResult)Controller.Index(1);


            // Assert    
            Assert.AreEqual("Index", view.ViewName);
        }

        [TestMethod] 
        public void ContainsAllIngredientsView2NameIsIndex()
        {
            // Arrange 
            ListEntity= ( ListEntity<Ingredient>)((ViewResult)Controller.Index(1)).Model;
            Ingredient[] ingrArray1 = ListEntity.ListT.ToArray();

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Ingredient>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

            int count2 = (( ListEntity<Ingredient>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert   
            Assert.AreEqual("Index", view2.ViewName);
        }

        [TestMethod] 
        public void FirstPageIsNotNull()
        {
            // Arrange 
            ListEntity= ( ListEntity<Ingredient>)((ViewResult)Controller.Index(1)).Model;
            Ingredient[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Ingredient>)(view1.Model)).ListT.Count();

            // Assert
            Assert.IsNotNull(view1);

        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsCorrect()=> BaseFirstPageIsCorrect(Repo, Controller, UIControllerType.Ingredients); 

        [TestMethod] 
        public void FirstPageNameIsIndex()
        {
            // Arrange 
            ListEntity= ( ListEntity<Ingredient>)((ViewResult)Controller.Index(1)).Model;
            Ingredient[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Ingredient>)(view1.Model)).ListT.Count();

            // Assert  
            Assert.AreEqual("Index", view1.ViewName);
        }



        [TestMethod] 
        public void FirstItemNameIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Ingredient>)((ViewResult)Controller.Index(1)).Model;
            Ingredient[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Ingredient>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("LambAndLentil.Domain.Entities.Ingredient ControllerTest1", (( ListEntity<Ingredient>)(view1.Model)).ListT.FirstOrDefault().Name);
        }



        [TestMethod] 
        public void FirstIngredientAddedByUserIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Ingredient>)((ViewResult)Controller.Index(1)).Model;
            Ingredient[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            // Assert   
            Assert.AreEqual("John Doe", (( ListEntity<Ingredient>)(view1.Model)).ListT.FirstOrDefault().AddedByUser);
        }

        [TestMethod] 
        public void FirstModifiedByUserIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Ingredient>)((ViewResult)Controller.Index(1)).Model;
            Ingredient[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);
            string userName = WindowsIdentity.GetCurrent().Name;
            // Assert   
            Assert.AreEqual(userName, (( ListEntity<Ingredient>)(view1.Model)).ListT.FirstOrDefault().ModifiedByUser);
        }

        [TestMethod] 
        public void FirstCreationDateIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Ingredient>)((ViewResult)Controller.Index(1)).Model;
            Ingredient[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MinValue, (( ListEntity<Ingredient>)(view1.Model)).ListT.FirstOrDefault().CreationDate);
        }

        [TestMethod] 
        public void  FirstModifiedDateIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Ingredient>)((ViewResult)Controller.Index(1)).Model;
            Ingredient[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MaxValue.AddYears(-10), (( ListEntity<Ingredient>)(view1.Model)).ListT.FirstOrDefault().ModifiedDate);
        }



        [TestMethod] 
        public void SecondItemNameIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Ingredient>)((ViewResult)Controller.Index(1)).Model;
            Ingredient[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Ingredient>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("LambAndLentil.Domain.Entities.Ingredient ControllerTest2", (( ListEntity<Ingredient>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
        }


        [TestMethod] 
        public void ThirdItemNameIsCorrect()
        {
            // Arrange 
            ListEntity= ( ListEntity<Ingredient>)((ViewResult)Controller.Index(1)).Model;
            Ingredient[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Ingredient>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("LambAndLentil.Domain.Entities.Ingredient ControllerTest3", (( ListEntity<Ingredient>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);
        }



        [TestMethod] 
        // currently we only have one page here
        public void SecondPageIsCorrect()
        {
            //TODO: add enough test ingredients to test the second page
        }



        [TestMethod]
        public void CanPaginateArrayLengthIsCorrect() => BaseCanPaginateArrayLengthIsCorrect(Repo, Controller);


        [TestMethod] 
        public void CanPaginate_ArrayFirstItemNameIsCorrect()
        {
            // Arrange 

            // Act
            var result = ( ListEntity<Ingredient>)((ViewResult)Controller.Index(1)).Model;
            Ingredient[] ingrArray1 = result.ListT.ToArray();

            // Assert  
            Assert.AreEqual("LambAndLentil.Domain.Entities.Ingredient ControllerTest1", ingrArray1[0].Name);
        }

       
        [TestMethod] 
        public void CanPaginate_ArrayThirdItemNameIsCorrect()
        {
            // Arrange 

            // Act
            var result = ( ListEntity<Ingredient>)((ViewResult)Controller.Index(1)).Model;
            Ingredient[] ingrArray1 = result.ListT.ToArray();

            // Assert  
            Assert.AreEqual("LambAndLentil.Domain.Entities.Ingredient ControllerTest3", ingrArray1[2].Name);
        }


        [TestMethod] 
        public void CanSendPaginationViewModel_CurrentPageCountCorrect()
        {
            // Arrange 

            // Act 
             ListEntity<Ingredient> resultT = ( ListEntity<Ingredient>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert  
            Assert.AreEqual(2, pageInfoT.CurrentPage);
        }

        [TestMethod] 
        public void  CanSendPaginationViewModel_ItemsPerPageCorrect()
        {
            // Arrange


            // Act 
             ListEntity<Ingredient> resultT = ( ListEntity<Ingredient>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert   
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
        }

        [TestMethod]
        public void CanSendPaginationViewModel_TotalItemsCorrect() => BaseCanSendPaginationViewModel_TotalItemsCorrect(Repo, Controller, UIControllerType.Ingredients);

        [TestMethod] 
        public void  CanSendPaginationViewModel_TotalPagesCorrect()
        {
            // Arrange


            // Act 
             ListEntity<Ingredient> resultT = ( ListEntity<Ingredient>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert 
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }




        [Ignore]
        [TestMethod]
        public void FlagAnIngredientFlaggedInAPerson() => Assert.Fail();

        [Ignore]
        [TestMethod]
        public void FlagAnIngredientFlaggedInTwoPersons() => Assert.Fail();

        [Ignore]
        [TestMethod]
        public void WhenAFlagHasBeenRemovedFromOnePersonStillThereForSecondFlaggedPerson() => Assert.Fail();
    }
}

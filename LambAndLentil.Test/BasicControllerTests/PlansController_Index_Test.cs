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
    [TestCategory("PlansController")]
    [TestCategory("Index")]
    public class PlansController_Index_Test : PlansController_Test_Should
    {


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
        public void ContainsAllPlansView2NotNull()
        { 
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<Plan>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

            int count2 = ((ListEntity<Plan>)(view2.Model)).ListT.Count();

            int count = count1 + count2;
             
            Assert.IsNotNull(view2);
        }

        [TestMethod]
        public void ContainsAllPlansView1Count6()
        {
            // Arrange 
            int count = Repo.Count();

            // Act
            ListEntity = (ListEntity<Plan>)((ViewResult)Controller.Index(1)).Model;
            Plan[] ingrArray1 = ListEntity.ListT.ToArray();
            int count1 = ingrArray1.Count();

            // Assert 
            Assert.AreEqual(count, count1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllPlansView2Count0()
        {
            // Arrange 
            ListEntity = (ListEntity<Plan>)((ViewResult)Controller.Index(1)).Model;
            Plan[] ingrArray1 = ListEntity.ListT.ToArray();

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<Plan>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

            int count2 = ((ListEntity<Plan>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert  
            Assert.AreEqual(0, count2);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllPlansView1NameIsIndex()
        { 
            ViewResult view = (ViewResult)Controller.Index(1); 
            Assert.AreEqual("Index", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllPlansView2NameIsIndex()
        { 
            ListEntity = (ListEntity<Plan>)((ViewResult)Controller.Index(1)).Model;
            Plan[] ingrArray1 = ListEntity.ListT.ToArray();
             
            ViewResult view1 = (ViewResult)Controller.Index(1); 
            int count1 = ((ListEntity<Plan>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

            int count2 = ((ListEntity<Plan>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert   
            Assert.AreEqual("Index", view2.ViewName);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsNotNull()
        {
            // Arrange 
            ListEntity = (ListEntity<Plan>)((ViewResult)Controller.Index(1)).Model;
            Plan[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<Plan>)(view1.Model)).ListT.Count();
             
            Assert.IsNotNull(view1); 
        }


        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageNameIsIndex()
        {
            // Arrange 
            ListEntity = (ListEntity<Plan>)((ViewResult)Controller.Index(1)).Model;
            Plan[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<Plan>)(view1.Model)).ListT.Count();

            // Assert  
            Assert.AreEqual("Index", view1.ViewName);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void FirstItemNameIsCorrect()
        {
            // Arrange 
            ListEntity = (ListEntity<Plan>)((ViewResult)Controller.Index(1)).Model;
            Plan[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<Plan>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("LambAndLentil.Domain.Entities.Plan ControllerTest1", ((ListEntity<Plan>)(view1.Model)).ListT.FirstOrDefault().Name);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void FirstPlanAddedByUserIsCorrect()
        {
            // Arrange 
            ListEntity = (ListEntity<Plan>)((ViewResult)Controller.Index(1)).Model;
            Plan[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            // Assert   
            Assert.AreEqual("John Doe", ((ListEntity<Plan>)(view1.Model)).ListT.FirstOrDefault().AddedByUser);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstModifiedByUserIsCorrect()
        {
            // Arrange 
            ListEntity = (ListEntity<Plan>)((ViewResult)Controller.Index(1)).Model;
            Plan[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);
            string userName = WindowsIdentity.GetCurrent().Name;
            // Assert   
            Assert.AreEqual(userName, ((ListEntity<Plan>)(view1.Model)).ListT.FirstOrDefault().ModifiedByUser.ToString());


        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstCreationDateIsCorrect()
        {
            // Arrange 
            ListEntity = (ListEntity<Plan>)((ViewResult)Controller.Index(1)).Model;
            Plan[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MinValue, ((ListEntity<Plan>)(view1.Model)).ListT.FirstOrDefault().CreationDate);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void Index_FirstModifiedDateIsCorrect()
        {
            // Arrange 
            ListEntity = (ListEntity<Plan>)((ViewResult)Controller.Index(1)).Model;
            Plan[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MaxValue.AddYears(-10), ((ListEntity<Plan>)(view1.Model)).ListT.FirstOrDefault().ModifiedDate);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void SecondItemNameIsCorrect()
        {
            // Arrange 
            ListEntity = (ListEntity<Plan>)((ViewResult)Controller.Index(1)).Model;
            Plan[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<Plan>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("LambAndLentil.Domain.Entities.Plan ControllerTest2", ((ListEntity<Plan>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void ThirdItemNameIsCorrect()
        {
            // Arrange 
            ListEntity = (ListEntity<Plan>)((ViewResult)Controller.Index(1)).Model;
            Plan[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<Plan>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("LambAndLentil.Domain.Entities.Plan ControllerTest3", ((ListEntity<Plan>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);
        }



        [TestMethod]
        [TestCategory("Index")]
        // currently we only have one page here
        public void SecondPageIsCorrect()
        {
            //TODO: add enough test ingredients to test the second page
        }



        [TestMethod]
        public void CanPaginateArrayLengthIsCorrect()
        {
            BaseCanPaginateArrayLengthIsCorrect(Repo,Controller);
        }

     



        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate_ArrayFirstItemNameIsCorrect()
        {
            // Arrange


            // Act
            var result = (ListEntity<Plan>)((ViewResult)Controller.Index(1)).Model;
            Plan[] ingrArray1 = result.ListT.ToArray();

            // Assert  
            Assert.AreEqual("LambAndLentil.Domain.Entities.Plan ControllerTest1", ingrArray1[0].Name);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate_ArrayThirdItemNameIsCorrect()
        {
            // Arrange 

            // Act
            var result = (ListEntity<Plan>)((ViewResult)Controller.Index(1)).Model;
            Plan[] ingrArray1 = result.ListT.ToArray();

            // Assert  
            Assert.AreEqual("PlansController_Index_Test P3", ingrArray1[2].Name);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void CanSendPaginationViewModel_CurrentPageCountCorrect()
        {
            // Arrange 

            // Act 
            ListEntity<Plan> resultT = (ListEntity<Plan>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert  
            Assert.AreEqual(2, pageInfoT.CurrentPage);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void PlansCtr_Index_CanSendPaginationViewModel_ItemsPerPageCorrect()
        {
            // Arrange


            // Act 
            ListEntity<Plan> resultT = (ListEntity<Plan>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert   
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
        }



        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void PlansCtr_Index_CanSendPaginationViewModel_TotalPagesCorrect()
        {
            // Arrange


            // Act 
            ListEntity<Plan> resultT = (ListEntity<Plan>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert 
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }

        [Ignore]
        [TestMethod]
        public void FlagAnPlanFlaggedInAPlan()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void FlagAnPlanFlaggedInTwoPlans()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void WhenAFlagHasBeenRemovedFromOnePlanStillThereForSecondFlaggedPlan()
        {
            Assert.Fail();
        }



        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllListEntityTVM()
        {
            // Arrange
            int repoCount = Repo.Count();

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<Plan>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

            int count2 = ((ListEntity<Plan>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
            Assert.AreEqual(repoCount, count1);
            Assert.AreEqual(0, count2);
            Assert.AreEqual(repoCount, count);
            Assert.AreEqual("Index", view1.ViewName);
            Assert.AreEqual("Index", view2.ViewName);

            //Assert.AreEqual("P1", (( ListEntity<Plan,Plan>)(view1.Model)). ListEntityTVM.FirstOrDefault().Name);
            //Assert.AreEqual("P2", (( ListEntity<Plan,Plan>)(view1.Model)). ListEntityTVM.Skip(1).FirstOrDefault().Name);
            //Assert.AreEqual("P3", (( ListEntity<Plan,Plan>)(view1.Model)). ListEntityTVM.Skip(2).FirstOrDefault().Name);
            //Assert.AreEqual("P5", (( ListEntity<Plan,Plan>)(view2.Model)). ListEntityTVM.FirstOrDefault().Name);

        }


        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsCorrect()
        {
            BaseFirstPageIsCorrect(Repo, Controller, UIControllerType.Plans);
        }


        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        // currently we only have one page here
        public void ListEntityTVMCtr_Index_SecondPageIsCorrect()
        {

        }

        [TestMethod]
        [TestCategory("Index")]
        public void CanSendPaginationViewModel()
        {
            BaseCanSendPaginationViewModel(Repo, Controller, UIControllerType.Plans);
        }

        [TestMethod]
        public void CanSendPaginationViewModel_TotalItemsCorrect()
        {
            BaseCanSendPaginationViewModel_TotalItemsCorrect(Repo, Controller, UIControllerType.Plans);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void PagingInfoIsCorrect()
        {
            BasePagingInfoIsCorrect(Repo, Controller, UIControllerType.Recipes);
            //// Arrange



            //// Action
            //int totalItems = ((ListEntity<Plan>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalItems;
            //int currentPage = ((ListEntity<Plan>)((ViewResult)Controller.Index()).Model).PagingInfo.CurrentPage;
            //int itemsPerPage = ((ListEntity<Plan>)((ViewResult)Controller.Index()).Model).PagingInfo.ItemsPerPage;
            //int totalPages = ((ListEntity<Plan>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalPages;



            //// Assert
            //Assert.AreEqual(6, totalItems);
            //Assert.AreEqual(1, currentPage);
            //Assert.AreEqual(8, itemsPerPage);
            //Assert.AreEqual(1, totalPages);
        }



        [TestMethod]
        public void CanPaginate()
        {
            // Arrange
            int count = Repo.Count();

            // Act
            var result = (ListEntity<Plan>)((ViewResult)Controller.Index(1)).Model;

            // Assert 
            Assert.AreEqual(count, result.ListT.Count());
            Assert.AreEqual("LambAndLentil.Domain.Entities.Plan ControllerTest1", result.ListT.First().Name);
            Assert.AreEqual("LambAndLentil.Domain.Entities.Plan ControllerTest3", result.ListT.Skip(2).First().Name);
        }
    }
}

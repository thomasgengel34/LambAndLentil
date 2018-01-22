using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("RecipesController")]
    [TestCategory("Index")]
    public class RecipesController_Index_Test:RecipesController_Test_Should
    {
       
        

        public RecipesController_Index_Test()
        { 
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
        }

        [TestMethod]
        [TestCategory("Index")]
        public void RecipesCtr_Index1()
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
        public void ContainsAllRecipesView2NotNull()
        {
            // Arrange 

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Recipe>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

            int count2 = (( ListEntity<Recipe>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert 
            Assert.IsNotNull(view2);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllRecipesView1Count5()
        {
            // Arrange 
            int count = Repo.Count();

            // Act
            ListEntity = ( ListEntity<Recipe>)((ViewResult)Controller.Index(1)).Model;
            Recipe[] ingrArray1 =  ListEntity.ListT.ToArray();
            int count1 = ingrArray1.Count();

            // Assert 
            Assert.AreEqual(count, count1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllRecipesView2Count0()
        {
            // Arrange 
             ListEntity = ( ListEntity<Recipe>)((ViewResult)Controller.Index(1)).Model;
            Recipe[] ingrArray1 =  ListEntity.ListT.ToArray();

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Recipe>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

            int count2 = (( ListEntity<Recipe>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert  
            Assert.AreEqual(0, count2);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllRecipesView1NameIsIndex()
        {
            // Arrange  

            // Act
            ViewResult view = (ViewResult)Controller.Index(1);


            // Assert    
            Assert.AreEqual("Index", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllRecipesView2NameIsIndex()
        {
            // Arrange 
             ListEntity = ( ListEntity<Recipe>)((ViewResult)Controller.Index(1)).Model;
            Recipe[] ingrArray1 =  ListEntity.ListT.ToArray();

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Recipe>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

            int count2 = (( ListEntity<Recipe>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert   
            Assert.AreEqual("Index", view2.ViewName);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsNotNull()
        {
            // Arrange 
             ListEntity = ( ListEntity<Recipe>)((ViewResult)Controller.Index(1)).Model;
            Recipe[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Recipe>)(view1.Model)).ListT.Count();

            // Assert
            Assert.IsNotNull(view1);

        }
         

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageNameIsIndex()
        {
            // Arrange 
             ListEntity = ( ListEntity<Recipe>)((ViewResult)Controller.Index(1)).Model;
            Recipe[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Recipe>)(view1.Model)).ListT.Count();

            // Assert  
            Assert.AreEqual("Index", view1.ViewName);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void FirstItemNameIsCorrect()
        {
            // Arrange 
             ListEntity = ( ListEntity<Recipe>)((ViewResult)Controller.Index(1)).Model;
            Recipe[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Recipe>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("LambAndLentil.Domain.Entities.Recipe ControllerTest1", (( ListEntity<Recipe>)(view1.Model)).ListT.FirstOrDefault().Name);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void FirstRecipeAddedByUserIsCorrect()
        {
            // Arrange 
             ListEntity = ( ListEntity<Recipe>)((ViewResult)Controller.Index(1)).Model;
            Recipe[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            // Assert   
            Assert.AreEqual("John Doe", (( ListEntity<Recipe>)(view1.Model)).ListT.FirstOrDefault().AddedByUser);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstModifiedByUserIsCorrect()
        {
            // Arrange 
             ListEntity = ( ListEntity<Recipe>)((ViewResult)Controller.Index(1)).Model;
            Recipe[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);
            string userName = WindowsIdentity.GetCurrent().Name;
            // Assert   
            Assert.AreEqual(userName, (( ListEntity<Recipe>)(view1.Model)).ListT.FirstOrDefault().ModifiedByUser);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstCreationDateIsCorrect()
        {
            // Arrange 
             ListEntity = ( ListEntity<Recipe>)((ViewResult)Controller.Index(1)).Model;
            Recipe[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MinValue, (( ListEntity<Recipe>)(view1.Model)).ListT.FirstOrDefault().CreationDate);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void Index_FirstModifiedDateIsCorrect()
        {
            // Arrange 
             ListEntity = ( ListEntity<Recipe>)((ViewResult)Controller.Index(1)).Model;
            Recipe[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MaxValue.AddYears(-10), (( ListEntity<Recipe>)(view1.Model)).ListT.FirstOrDefault().ModifiedDate);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void SecondItemNameIsCorrect()
        {
            // Arrange 
             ListEntity = ( ListEntity<Recipe>)((ViewResult)Controller.Index(1)).Model;
            Recipe[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Recipe>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("LambAndLentil.Domain.Entities.Recipe ControllerTest2", (( ListEntity<Recipe>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void ThirdItemNameIsCorrect()
        {
            // Arrange 
             ListEntity = ( ListEntity<Recipe>)((ViewResult)Controller.Index(1)).Model;
            Recipe[] ingrArray1 =  ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = (( ListEntity<Recipe>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("LambAndLentil.Domain.Entities.Recipe ControllerTest3", (( ListEntity<Recipe>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);
        }



        [TestMethod]
        [TestCategory("Index")]
        // currently we only have one page here
        public void SecondPageIsCorrect()
        {
            //TODO: add enough test ingredients to test the second page
        }



        [TestMethod] 
        public void  CanPaginateArrayLengthIsCorrect()
        {
            BaseCanPaginateArrayLengthIsCorrect(Controller);
        }

     
        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate_ArrayFirstItemNameIsCorrect()
        { 
            var result = ( ListEntity<Recipe>)((ViewResult)Controller.Index(1)).Model;
            Recipe[] ingrArray1 = result.ListT.ToArray();
             
            Assert.AreEqual("LambAndLentil.Domain.Entities.Recipe ControllerTest1", ingrArray1[0].Name); 
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate_ArrayThirdItemNameIsCorrect()
        { 
            var result = ( ListEntity<Recipe>)((ViewResult)Controller.Index(1)).Model;
            Recipe[] ingrArray1 = result.ListT.ToArray();
             
            Assert.AreEqual("RecipesController_Index_Test P3", ingrArray1[2].Name);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void CanSendPaginationViewModel_CurrentPageCountCorrect()
        { 
             ListEntity<Recipe> resultT = ( ListEntity<Recipe>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;
             
            Assert.AreEqual(2, pageInfoT.CurrentPage);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void RecipesCtr_Index_CanSendPaginationViewModel_ItemsPerPageCorrect()
        {  
             ListEntity<Recipe> resultT = ( ListEntity<Recipe>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;
             
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void CanSendPaginationViewModel_TotalItemsCorrect()
        {
            BaseCanSendPaginationViewModel_TotalItemsCorrect(Repo, Controller);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void RecipesCtr_Index_CanSendPaginationViewModel_TotalPagesCorrect()
        {
            BaseCanSendPaginationViewModel_TotalItemsCorrect(Repo, Controller);
        }


        
        [Ignore]
        [TestMethod]
        public void FlagAnRecipeFlaggedInAPerson()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void FlagAnRecipeFlaggedInTwoPersons()
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
        public void ShowAllRecipesonIndex()
        {
            // Arrange
            int repoCount = Repo.Count();

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);


            int count1 = ((ListEntity<Recipe>)view1.Model).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

            int count2 = ((ListEntity<Recipe>)view2.Model).ListT.Count();

            int count = count1 + count2;

            // Assert
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
            Assert.AreEqual(repoCount, count1);
            Assert.AreEqual(0, count2);
            Assert.AreEqual(repoCount, count);
            Assert.AreEqual("Index", view1.ViewName);
            Assert.AreEqual("Index", view2.ViewName);

        }

        [TestMethod]
        public void CanSendPaginationViewModelonIndex()
        {

            // Arrange

            // Act

            ListEntity<Recipe> resultT = (ListEntity<Recipe>)((ViewResult)Controller.Index(2)).Model;


            // Assert

            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(Repo.Count(), pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);

        }
         
        [TestMethod]
        public void  FirstPageIsCorrect()
        {
            BaseFirstPageIsCorrect(Repo, Controller); 
        }

      

        [TestMethod]
        public void  PagingInfoIsCorrect()
        {
            BasePagingInfoIsCorrect(Repo, Controller);
        }

        [Ignore]
        [TestMethod]
        // currently we only have one page here 
        public void Index_SecondPageIsCorrect()
        {

        }

    
        [TestMethod]
        public void  CanPaginate()
        {
            // Arrange
            int count = Repo.Count();

            // Act
            var result = (ListEntity<Recipe>)((ViewResult)Controller.Index(1)).Model;

            // Assert 
            Assert.AreEqual(count, result.ListT.Count());
            Assert.AreEqual("LambAndLentil.Domain.Entities.Recipe ControllerTest1", result.ListT.First().Name);
            Assert.AreEqual("LambAndLentil.Domain.Entities.Recipe ControllerTest3", result.ListT.Skip(2).First().Name);
        }
    }
}

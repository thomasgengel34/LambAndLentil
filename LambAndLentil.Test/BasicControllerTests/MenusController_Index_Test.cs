using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("MenusController")]
    [TestCategory("Index")]
    public class MenusController_Index_Test : MenusController_Test_Should
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
        public void MenusCtr_Index1()
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
        public void ContainsAllMenusView2NotNull()
        {
            // Arrange 

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<Menu>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

            int count2 = ((ListEntity<Menu>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert 
            Assert.IsNotNull(view2);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllMenusView1Count6()
        {
            // Arrange 
            int count = Repo.Count();

            // Act
            ListEntity = (ListEntity<Menu>)((ViewResult)Controller.Index(1)).Model;
            Menu[] ingrArray1 = ListEntity.ListT.ToArray();
            int count1 = ingrArray1.Count();

            // Assert 
            Assert.AreEqual(count, count1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllMenusView2Count0()
        {
            // Arrange 
            ListEntity = (ListEntity<Menu>)((ViewResult)Controller.Index(1)).Model;
            Menu[] ingrArray1 = ListEntity.ListT.ToArray();

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<Menu>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

            int count2 = ((ListEntity<Menu>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert  
            Assert.AreEqual(0, count2);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllMenusView1NameIsIndex()
        {
            // Arrange  

            // Act
            ViewResult view = (ViewResult)Controller.Index(1);


            // Assert    
            Assert.AreEqual("Index", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllMenusView2NameIsIndex()
        {
            // Arrange 
            ListEntity = (ListEntity<Menu>)((ViewResult)Controller.Index(1)).Model;
            Menu[] ingrArray1 = ListEntity.ListT.ToArray();

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<Menu>)(view1.Model)).ListT.Count();

            ViewResult view2 = (ViewResult)Controller.Index(2);

            int count2 = ((ListEntity<Menu>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert   
            Assert.AreEqual("Index", view2.ViewName);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsNotNull()
        {
            // Arrange 
            ListEntity = (ListEntity<Menu>)((ViewResult)Controller.Index(1)).Model;
            Menu[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<Menu>)(view1.Model)).ListT.Count();

            // Assert
            Assert.IsNotNull(view1);

        }

       

        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageNameIsIndex()
        {
            // Arrange 
            ListEntity = (ListEntity<Menu>)((ViewResult)Controller.Index(1)).Model;
            Menu[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<Menu>)(view1.Model)).ListT.Count();

            // Assert  
            Assert.AreEqual("Index", view1.ViewName);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void FirstItemNameIsCorrect()
        {
            // Arrange 
            ListEntity = (ListEntity<Menu>)((ViewResult)Controller.Index(1)).Model;
            Menu[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<Menu>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("LambAndLentil.Domain.Entities.Menu ControllerTest1", ((ListEntity<Menu>)(view1.Model)).ListT.FirstOrDefault().Name);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void FirstMenuAddedByUserIsCorrect()
        {
            // Arrange 
            ListEntity = (ListEntity<Menu>)((ViewResult)Controller.Index(1)).Model;
            Menu[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            // Assert   
            Assert.AreEqual("John Doe", ((ListEntity<Menu>)(view1.Model)).ListT.FirstOrDefault().AddedByUser);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstModifiedByUserIsCorrect()
        {
            // Arrange 
            ListEntity = (ListEntity<Menu>)((ViewResult)Controller.Index(1)).Model;
            Menu[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);
            string userName = WindowsIdentity.GetCurrent().Name;
            // Assert   
            Assert.AreEqual(userName, ((ListEntity<Menu>)(view1.Model)).ListT.FirstOrDefault().ModifiedByUser);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void FirstCreationDateIsCorrect()
        {
            // Arrange 
            ListEntity = (ListEntity<Menu>)((ViewResult)Controller.Index(1)).Model;
            Menu[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MinValue, ((ListEntity<Menu>)(view1.Model)).ListT.FirstOrDefault().CreationDate);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void Index_FirstModifiedDateIsCorrect()
        {
            // Arrange 
            ListEntity = (ListEntity<Menu>)((ViewResult)Controller.Index(1)).Model;
            Menu[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            // Assert   
            Assert.AreEqual(DateTime.MaxValue.AddYears(-10), ((ListEntity<Menu>)(view1.Model)).ListT.FirstOrDefault().ModifiedDate);
        }



        [TestMethod]
        [TestCategory("Index")]
        public void SecondItemNameIsCorrect()
        {
            // Arrange 
            ListEntity = (ListEntity<Menu>)((ViewResult)Controller.Index(1)).Model;
            Menu[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<Menu>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("LambAndLentil.Domain.Entities.Menu ControllerTest2", ((ListEntity<Menu>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void ThirdItemNameIsCorrect()
        {
            // Arrange 
            ListEntity = (ListEntity<Menu>)((ViewResult)Controller.Index(1)).Model;
            Menu[] ingrArray1 = ListEntity.ListT.ToArray();
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<Menu>)(view1.Model)).ListT.Count();

            // Assert   
            Assert.AreEqual("LambAndLentil.Domain.Entities.Menu ControllerTest3", ((ListEntity<Menu>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);
        }



        [TestMethod]
        [TestCategory("Index")]
        // currently we only have one page here
        public void SecondPageIsCorrect()
        {
            //TODO: add enough test ingredients to test the second page
        }



        [TestMethod]
        public void CanPaginateArrayLengthIsCorrect() => BaseCanPaginateArrayLengthIsCorrect(Controller);


        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate_ArrayFirstItemNameIsCorrect()
        {
            // Arrange


            // Act
            var result = (ListEntity<Menu>)((ViewResult)Controller.Index(1)).Model;
            Menu[] ingrArray1 = result.ListT.ToArray();

            // Assert  
            Assert.AreEqual("LambAndLentil.Domain.Entities.Menu ControllerTest1", ingrArray1[0].Name);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate_ArrayThirdItemNameIsCorrect()
        {
            // Arrange 

            // Act
            var result = (ListEntity<Menu>)((ViewResult)Controller.Index(1)).Model;
            Menu[] ingrArray1 = result.ListT.ToArray();

            // Assert  
            Assert.AreEqual("MenusController_Index_Test P3", ingrArray1[2].Name);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void CanSendPaginationViewModel_CurrentPageCountCorrect()
        {
            // Arrange 

            // Act 
            ListEntity<Menu> resultT = (ListEntity<Menu>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert  
            Assert.AreEqual(2, pageInfoT.CurrentPage);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void MenusCtr_Index_CanSendPaginationViewModel_ItemsPerPageCorrect()
        {
            // Arrange


            // Act 
            ListEntity<Menu> resultT = (ListEntity<Menu>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert   
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void CanSendPaginationViewModel_TotalItemsCorrect() => BaseCanSendPaginationViewModel_TotalItemsCorrect(Repo, Controller);

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void MenusCtr_Index_CanSendPaginationViewModel_TotalPagesCorrect()
        {
            // Arrange


            // Act 
            ListEntity<Menu> resultT = (ListEntity<Menu>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            // Assert 
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }

        [Ignore]
        [TestMethod]
        public void FlagAnMenuFlaggedInAPerson() => 
            Assert.Fail();

        [Ignore]
        [TestMethod]
        public void FlagAnMenuFlaggedInTwoPersons() =>
            // Arrange

            // Act

            // Assert
            Assert.Fail();

        [Ignore]
        [TestMethod]
        public void WhenAFlagHasBeenRemovedFromOnePersonStillThereForSecondFlaggedPerson() => 
            Assert.Fail();




        [TestMethod]
        [TestCategory("Index")]
        public void  ContainsAllMenus()
        {
            // Arrange   
            int repoCount = Repo.Count();
            IGenericController<Menu> Controller2 = new MenusController(Repo);

            // Act
            ViewResult view1 = (ViewResult)Controller.Index(1);

            int count1 = ((ListEntity<Menu>)(view1.Model)).ListT.Count();
            var firstName = ((ListEntity<Menu>)(view1.Model)).ListT.FirstOrDefault().Name;
            var secondName = ((ListEntity<Menu>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name;
            var thirdName = ((ListEntity<Menu>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name;
            var fifthName = ((ListEntity<Menu>)(view1.Model)).ListT.Skip(4).FirstOrDefault().Name;

            ViewResult view2 = (ViewResult)Controller2.Index(2);
            int count2 = ((ListEntity<Menu>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
            Assert.AreEqual(repoCount, count1);
            Assert.AreEqual(0, count2);
            Assert.AreEqual(repoCount, count);
            Assert.AreEqual("Index", view1.ViewName);
            Assert.AreEqual("Index", view2.ViewName);
            Assert.AreEqual("LambAndLentil.Domain.Entities.Menu ControllerTest1", firstName);
            Assert.AreEqual("LambAndLentil.Domain.Entities.Menu ControllerTest2", secondName);
            Assert.AreEqual("LambAndLentil.Domain.Entities.Menu ControllerTest3", thirdName);
            Assert.AreEqual("LambAndLentil.Domain.Entities.Menu ControllerTest5", fifthName);

        }


        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsCorrect() => BaseFirstPageIsCorrect(Repo, Controller);

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
        public void PagingInfoIsCorrect() => BasePagingInfoIsCorrect(Repo, Controller);//// Arrange//int count = Repo.Count();//// Action//int totalItems = ((ListEntity<Menu>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalItems;//int currentPage = ((ListEntity<Menu>)((ViewResult)Controller.Index()).Model).PagingInfo.CurrentPage;//int itemsPerPage = ((ListEntity<Menu>)((ViewResult)Controller.Index()).Model).PagingInfo.ItemsPerPage;//int totalPages = ((ListEntity<Menu>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalPages;//// Assert//Assert.AreEqual(count, totalItems);//Assert.AreEqual(1, currentPage);//Assert.AreEqual(8, itemsPerPage);//Assert.AreEqual(1, totalPages);

        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate()
        {
            // Arrange 
            int repoCount = Repo.Count();
            // Act
            var result = (ListEntity<Menu>)((ViewResult)Controller.Index(1)).Model;

            // Assert

            Assert.AreEqual(repoCount, result.ListT.Count());
            Assert.AreEqual("LambAndLentil.Domain.Entities.Menu ControllerTest1", result.ListT.FirstOrDefault().Name);
            Assert.AreEqual("LambAndLentil.Domain.Entities.Menu ControllerTest4", result.ListT.Skip(3).FirstOrDefault().Name);
        }

    }
}

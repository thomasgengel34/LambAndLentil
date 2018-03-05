using System;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace  LambAndLentil.Test.BaseControllerTests
{
    internal class  Pagination<T> : BaseControllerTest<T>
         where T : BaseEntity, IEntity, new()
    { 
        internal static void TestRunner()  
        {
            CanPaginateArrayLengthIsCorrect();
            CanPaginate();
            FirstItemNameIsCorrect();
            CurrentPageCountCorrect();
            ThirdItemIDIsCorrect();
            ItemsPerPageCorrect();
            PagingInfoIsCorrect();
            TotalItemsCorrect();
            TotalPagesCorrect();
            CanReturnCorrectPageInfo();
        }

        // currently we only have one page here
        private static void SecondPageIsCorrect()
        {
            //TODO: add enough test ingredients to test the second page
        }


        private static void CanPaginate()
        { 

            SetupRepoAndControllerForAStaticMethod(out repo, out controller);
            var result = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            int count = result.PagingInfo.ItemsPerPage;
            if (count>result.PagingInfo.TotalItems)
            {
                count = result.PagingInfo.TotalItems;
            }
            Assert.AreEqual(count, result.ListT.Count());
            int firstID =  (from r in repo.GetAll()
                                select r.ID).First();
            int secondID = (from r in repo.GetAll()
                           select r.ID).Skip(2).First();
            Assert.AreEqual(firstID, result.ListT.First().ID);
            Assert.AreEqual(secondID, result.ListT.Skip(2).First().ID);
        }

        private static void CanReturnCorrectPageInfo()
        {
            SetupRepoAndControllerForAStaticMethod(out repo, out controller);
            int totalItems = repo.Count();
            ListEntity<T> resultT = (ListEntity<T>)((ViewResult)controller.Index(2)).Model;
            decimal decimalNumberOfPages = totalItems / ((decimal) resultT.PagingInfo.ItemsPerPage);
            int integerNumberOfPages=(int)Decimal.Ceiling(decimalNumberOfPages);
            
            // TODO: use variables not "magic numbers"
            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(totalItems, pageInfoT.TotalItems);
            Assert.AreEqual(integerNumberOfPages, pageInfoT.TotalPages); 
        }

        
        private static void  CanPaginateArrayLengthIsCorrect()
        { 
            SetupRepoAndControllerForAStaticMethod(out repo, out controller);

            int repoCount = repo.Count();

            var result = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;

           // Assert.AreEqual(repoCount, result.ListT.Count());
        }

        private static void SetupRepoAndControllerForAStaticMethod(out IRepository<T> repo, out IGenericController<T> controller)
        {
            ClassCleanup();
            repo = new TestRepository<T>();
            T item1 = new T() { ID = 1000 };
            T item2 = new T() { ID = 1001 };
            T item3 = new T() { ID = 1002 };
            T item4 = new T() { ID = 1003 };
            T item5 = new T() { ID = 1004 };
            repo.Save(item1);
            repo.Save(item2);
            repo.Save(item3);
            repo.Save(item4);
            repo.Save(item5);
            controller = BaseControllerTestFactory();
        }

        private static void FirstItemNameIsCorrect()
        {
            SetupRepoAndControllerForAStaticMethod(out repo, out controller);
            var result = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            T[] ingrArray1 = result.ListT.ToArray();
            int firstID = (from r in repo.GetAll()
                           select r.ID).First();
            Assert.AreEqual(firstID, ingrArray1[0].ID);
        }

        private static void FirstPageIsNotNull()
        {
            SetupRepoAndControllerForAStaticMethod(out repo, out controller);
            ListEntity = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            T[] ingrArray1 = ListEntity.ListT.ToArray();
            controller.PageSize = 8;

            ViewResult view1 = (ViewResult)controller.Index(1);

            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            Assert.IsNotNull(view1);
        }


     

        private static void ThirdItemIDIsCorrect()
        {
            SetupRepoAndControllerForAStaticMethod(out repo, out controller);
            var result = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            T[] ingrArray1 = result.ListT.ToArray();
            int thirdID = (from r in repo.GetAll()
                           select r.ID).Skip(2).First();
            Assert.AreEqual(thirdID, ingrArray1[2].ID);
        }

        private static void CurrentPageCountCorrect()
        {
            SetupRepoAndControllerForAStaticMethod(out repo, out controller);
            ListEntity<T> resultT = (ListEntity<T>)((ViewResult)controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            Assert.AreEqual(2, pageInfoT.CurrentPage);
        }

        private static void ItemsPerPageCorrect()
        {
            SetupRepoAndControllerForAStaticMethod(out repo, out controller);
            ListEntity<T> resultT = (ListEntity<T>)((ViewResult)controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
        }


        private static void TotalItemsCorrect()
        {
            SetupRepoAndControllerForAStaticMethod(out repo, out controller);
            ListEntity<T> resultT = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;
            int count = repo.Count();
            Assert.AreEqual(count, pageInfoT.TotalItems);
        }

        private static void TotalPagesCorrect()
        {
            SetupRepoAndControllerForAStaticMethod(out repo, out controller);
            ListEntity<T> resultT = (ListEntity<T>)((ViewResult)controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;
            int totalItems = repo.Count();
            decimal decimalNumberOfPages = totalItems / ((decimal)resultT.PagingInfo.ItemsPerPage);
            int integerNumberOfPages = (int)Decimal.Ceiling(decimalNumberOfPages);
            Assert.AreEqual(integerNumberOfPages , pageInfoT.TotalPages);
        }



        // TODO: refactor and combine with other tests
        private static void CanSendPaginationViewModel()
        {
            SetupRepoAndControllerForAStaticMethod(out repo, out controller);
            ListEntity<T> resultT = (ListEntity<T>)((ViewResult)controller.Index(2)).Model;

            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(7, pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }

       private static void BaseCanSendPaginationViewModel()
        {
            int count = repo.Count();

            ListEntity<T> resultT1 = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            ListEntity<T> resultT2 = (ListEntity<T>)((ViewResult)controller.Index(2)).Model;
            int repoCount = repo.Count();
            int resultT1Count = resultT1.ListT.Count();
            int resultT2Count = resultT2.ListT.Count();
            PagingInfo pageInfoT = resultT1.PagingInfo;

            Assert.AreEqual(1, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(count, resultT1.ListT.Count());
            Assert.AreEqual(0, resultT2.ListT.Count());
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }
         

        private static void  PagingInfoIsCorrect()
        {
            SetupRepoAndControllerForAStaticMethod(out repo, out controller);
            int repoCount = repo.Count();

            int totalItems = ((ListEntity<T>)((ViewResult)controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = ((ListEntity<T>)((ViewResult)controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = ((ListEntity<T>)((ViewResult)controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = ((ListEntity<T>)((ViewResult)controller.Index()).Model).PagingInfo.TotalPages;

            Assert.AreEqual(repoCount, totalItems);
            Assert.AreEqual(1, currentPage);
            Assert.AreEqual(8, itemsPerPage);
            Assert.AreEqual(1, totalPages);
        }

        private static void BaseCanSendPaginationViewModel_TotalItemsCorrect()
        {
            int count = repo.Count();

            ListEntity<T> result = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            PagingInfo pageInfoT = result.PagingInfo;

            Assert.AreEqual(count, result.ListT.Count());
        }

        private void BaseShouldAddRecipeToList(IRepository<Ingredient> repo, Ingredient entity, Ingredient returnedEntity, IngredientsController controller) => throw new NotImplementedException();



       private static void BaseFirstPageIsCorrect()
        {
            int repoCount = repo.Count();
            ListEntity<T> ilListEntity = new ListEntity<T>();
            controller.PageSize = 8;

            ViewResult view1 = (ViewResult)controller.Index(1);
            int count1 = ((ListEntity<T>)(view1.Model)).ListT.Count();

            Assert.IsNotNull(view1);
            Assert.AreEqual(repoCount, count1);
            Assert.AreEqual("Index", view1.ViewName);

            Assert.AreEqual(typeof(T).ToString() + " ControllerTest1", ((ListEntity<T>)(view1.Model)).ListT.FirstOrDefault().Name);
            Assert.AreEqual(typeof(T).ToString() + " ControllerTest2", ((ListEntity<T>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual(typeof(T).ToString() + " ControllerTest3", ((ListEntity<T>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);
        }


      
    }
}

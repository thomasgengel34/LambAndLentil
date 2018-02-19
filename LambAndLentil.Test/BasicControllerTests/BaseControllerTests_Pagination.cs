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
    internal class BaseControllerTests_Pagination<T> : BaseControllerTest<T>
         where T : BaseEntity, IEntity, new()
    {
        static private IRepository<T> repo;
        static private IGenericController<T> controller;

        // currently we only have one page here
        internal static void SecondPageIsCorrect()
        {
            //TODO: add enough test ingredients to test the second page
        }


        internal static void CanPaginate()
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

        internal static void CanReturnCorrectPageInfo()
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

        
        internal static void BaseCanPaginateArrayLengthIsCorrect()
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
            controller = BaseControllerTestFactory(typeof(T));
        }

        internal static void FirstItemNameIsCorrect()
        {
            SetupRepoAndControllerForAStaticMethod(out repo, out controller);
            var result = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            T[] ingrArray1 = result.ListT.ToArray();
            int firstID = (from r in repo.GetAll()
                           select r.ID).First();
            Assert.AreEqual(firstID, ingrArray1[0].ID);
        }

        internal static void ThirdItemNameIsCorrect()
        {
            SetupRepoAndControllerForAStaticMethod(out repo, out controller);
            var result = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            T[] ingrArray1 = result.ListT.ToArray();
            int thirdID = (from r in repo.GetAll()
                           select r.ID).Skip(2).First();
            Assert.AreEqual(thirdID, ingrArray1[2].ID);
        }

        internal static void CurrentPageCountCorrect()
        {
            SetupRepoAndControllerForAStaticMethod(out repo, out controller);
            ListEntity<T> resultT = (ListEntity<T>)((ViewResult)controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            Assert.AreEqual(2, pageInfoT.CurrentPage);
        }

        internal static void ItemsPerPageCorrect()
        {
            SetupRepoAndControllerForAStaticMethod(out repo, out controller);
            ListEntity<T> resultT = (ListEntity<T>)((ViewResult)controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
        }


        internal static void TotalItemsCorrect()
        {
            SetupRepoAndControllerForAStaticMethod(out repo, out controller);
            ListEntity<T> resultT = (ListEntity<T>)((ViewResult)controller.Index(1)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;
            int count = repo.Count();
            Assert.AreEqual(count, pageInfoT.TotalItems);
        }

        internal static void TotalPagesCorrect()
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
        internal static void CanSendPaginationViewModel()
        {
            SetupRepoAndControllerForAStaticMethod(out repo, out controller);
            ListEntity<T> resultT = (ListEntity<T>)((ViewResult)controller.Index(2)).Model;

            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(7, pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }
    }
}

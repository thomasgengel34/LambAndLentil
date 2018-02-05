using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    internal class BaseControllerTests_Pagination<T>:BaseControllerTest<T>
         where T : BaseEntity, IEntity, new()
    { 
        // currently we only have one page here
        public void SecondPageIsCorrect()
        {
            //TODO: add enough test ingredients to test the second page
        }


        public void CanPaginate()
        { 
            var result = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;

            Assert.AreEqual(Repo.Count(), result.ListT.Count());
            Assert.AreEqual("ControllerTest1", result.ListT.First().Name);
            Assert.AreEqual("ControllerTest3", result.ListT.Skip(2).First().Name);
        }

        public void CanReturnCorrectPageInfo()
        { 
            int totalItems = Repo.Count();
            ListEntity<T> resultT = (ListEntity<T>)((ViewResult)Controller.Index(2)).Model;

            // TODO: use variables not "magic numbers"
            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(  8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(totalItems, pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);

        }

        public void  FirstItemNameIsCorrect()
        {
            var result = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = result.ListT.ToArray();
             
            Assert.AreEqual("ControllerTest1", ingrArray1[0].Name);
        }
         
        public void   ThirdItemNameIsCorrect()
        { 
            var result = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;
            T[] ingrArray1 = result.ListT.ToArray();
              
            Assert.AreEqual("ControllerTest3", ingrArray1[2].Name);
        }
         
        public void  CurrentPageCountCorrect()
        { 
            ListEntity<T> resultT = (ListEntity<T>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;
             
            Assert.AreEqual(2, pageInfoT.CurrentPage);
        }
         
        public void  ItemsPerPageCorrect()
        {
            ListEntity<T> resultT = (ListEntity<T>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
        }
         
        
        public void TotalItemsCorrect()
        {
            ListEntity<T> resultT  = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model; 
            PagingInfo pageInfoT  = resultT.PagingInfo; 
            int count = Repo.Count();
            Assert.AreEqual(count, pageInfoT.TotalItems);
        }
         
        public void  TotalPagesCorrect()
        {
            ListEntity<T> resultT = (ListEntity<T>)((ViewResult)Controller.Index(2)).Model;
            PagingInfo pageInfoT = resultT.PagingInfo;

            Assert.AreEqual(1, pageInfoT.TotalPages);
        }
         
      

        // TODO: refactor and combine with other tests
        public void CanSendPaginationViewModel()
        {

            ListEntity<T> resultT = (ListEntity<T>)((ViewResult)Controller.Index(2)).Model;
 
            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(7, pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }
    }
}

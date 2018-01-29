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
            int count = Controller.PageSize;

            var result = (ListEntity<T>)((ViewResult)Controller.Index(1)).Model;

            Assert.AreEqual(count, result.ListT.Count());
            Assert.AreEqual("LambAndLentil.Domain.Entities.Recipe ControllerTest1", result.ListT.First().Name);
            Assert.AreEqual("LambAndLentil.Domain.Entities.Recipe ControllerTest3", result.ListT.Skip(2).First().Name);
        }

        public void  FirstItemNameIsCorrect()
        {
            var result = (ListEntity<Person>)((ViewResult)Controller.Index(1)).Model;
            Person[] ingrArray1 = result.ListT.ToArray();
             
            Assert.AreEqual("LambAndLentil.Domain.Entities.Person ControllerTest1", ingrArray1[0].Name);
        }
         
        public void   ThirdItemNameIsCorrect()
        { 
            var result = (ListEntity<Person>)((ViewResult)Controller.Index(1)).Model;
            Person[] ingrArray1 = result.ListT.ToArray();
              
            Assert.AreEqual("PersonsController_Index_Test P3", ingrArray1[2].Name);
        }
         
        public void  CurrentPageCountCorrect()
        { 
            ListEntity<Person> resultT = (ListEntity<Person>)((ViewResult)Controller.Index(2)).Model;
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
         
        public void CanReturnCorrectPageInfo()
        {
            int pageSize = Controller.PageSize;

            int totalItems = Repo.Count();
            ListEntity<Recipe> resultT = (ListEntity<Recipe>)((ViewResult)Controller.Index(2)).Model;

            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(pageSize, pageInfoT.ItemsPerPage);
            Assert.AreEqual(totalItems, pageInfoT.TotalItems);
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

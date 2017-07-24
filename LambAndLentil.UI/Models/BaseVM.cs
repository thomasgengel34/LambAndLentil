using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LambAndLentil.UI.Models
{
    public class BaseVM : BaseEntity,IEntity
    {
        internal BaseVM() : base()
        {
        }

        public int ID { get; set; }
       

        internal static ListVM<T,TVM> GetIndexedModel<T,TVM>(int PageSize, int page = 1)
            where T :BaseEntity,IEntity
            where TVM:BaseVM, IEntity
        {
            IRepository<T,TVM> repository = new EFRepository<T,TVM>();

            var result = repository.GetAll()
                      .OrderBy(p => p.Name)
                      .Skip((page - 1) * PageSize)
                      .Take(PageSize);

            return  (ListVM<T,TVM>)result;
        }

         

        internal static PagingInfo PagingFunction<T,TVM>(int page, int PageSize)
            where T:class
            where TVM : BaseVM,IEntity
        {
            IRepository<T,TVM> repository = new EFRepository<T,TVM>();

            PagingInfo PagingInfo = new PagingInfo();
            PagingInfo.CurrentPage = page;
            PagingInfo.ItemsPerPage = PageSize;
            int totalItems = repository.Count();
            PagingInfo.TotalItems = totalItems;

            return PagingInfo;
        }


    }
}

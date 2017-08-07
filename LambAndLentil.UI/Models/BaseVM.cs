using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LambAndLentil.UI.Models
{
    public class BaseVM : BaseEntity, IEntity
    {
        public BaseVM() : base()
        {
        }

        public int ID { get; set; }


        internal static List<TVM> GetIndexedModel<T, TVM>(int PageSize, int page = 1)
            where T : BaseEntity, IEntity
            where TVM : BaseVM, IEntity
        {
            IRepository<T, TVM> repository = new JSONRepository<T, TVM>();

            var result = repository.GetAllT()
                      .OrderBy(p => p.Name)
                      .Skip((page - 1) * PageSize)
                      .Take(PageSize);
            List<TVM> listVM = new List<TVM>();
            foreach (var item in result)
            {
                var itemVM = Mapper.Map<T, TVM>(item);
                listVM.Add(itemVM);
            }

            return   listVM;
        }



        internal static PagingInfo PagingFunction<T, TVM>(int page, int PageSize)
            where T : BaseEntity,IEntity
            where TVM : BaseVM, IEntity
        {
            IRepository<T, TVM> repository = new JSONRepository<T, TVM>();

            PagingInfo PagingInfo = new PagingInfo();
            PagingInfo.CurrentPage = page;
            PagingInfo.ItemsPerPage = PageSize;
            int totalItems = repository.Count();
            PagingInfo.TotalItems = totalItems;

            return PagingInfo;
        }


    }
}

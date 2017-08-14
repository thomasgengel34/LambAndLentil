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


        internal   List<TVM> GetIndexedModel<T, TVM>(IRepository<T, TVM> repository, int PageSize, int page = 1)
            where T : BaseEntity, IEntity
            where TVM : BaseVM, IEntity
        {
           

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



        internal   PagingInfo PagingFunction<T, TVM>(IRepository<T, TVM> repository, int page, int PageSize)
            where T : BaseEntity,IEntity
            where TVM : BaseVM, IEntity
        {
            PagingInfo PagingInfo = new PagingInfo();
            PagingInfo.CurrentPage = page;
            PagingInfo.ItemsPerPage = PageSize;
            int totalItems = repository.Count();
            PagingInfo.TotalItems = totalItems;

            return PagingInfo;
        }


    }
}

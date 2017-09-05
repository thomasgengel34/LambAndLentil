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
       

        internal   List<T> GetIndexedModel<T>(IRepository<T > repository, int PageSize, int page = 1)
            where T : BaseVM, IEntity
        {
           

            var result = repository.GetAll()
                      .OrderBy(p => p.Name)
                      .Skip((page - 1) * PageSize)
                      .Take(PageSize);
            List<T> listVM = new List<T>();
            foreach (var item in result)
            {
                
                listVM.Add(item);
            }

            return   listVM;
        }



        internal   PagingInfo PagingFunction<T >(IRepository<T> repository, int page, int PageSize)
            where T :  BaseVM, IEntity
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

using LambAndLentil.Domain.Entities;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambAndLentil.Domain.Entities
{
    public class ListEntity<T>
         where T : BaseEntity
    {
        private DateTime creationDate;


        public ListEntity()
        {
            ListT = new List<T>();
        }

        public ListEntity(DateTime creationDate)
        {
            this.creationDate = creationDate;
        }

        public string Name { get; set; }
        public IEnumerable<T> ListT { get; set; }
        public PagingInfo PagingInfo { get; set; }


        public ListEntity<T> Add(ListEntity<T> list, BaseEntity item)
        {
            return list;
        }
    } 
}

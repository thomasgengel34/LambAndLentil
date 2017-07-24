using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambAndLentil.Domain.Abstract
{
    public interface IRepository
    { }
       

        public interface IRepository<T,TVM>
        where T:class
        where TVM : class
    {
        EFDbContext context { get; set; }
        IQueryable Ingredient { get; }
        IQueryable Recipe { get; }
        IQueryable Menu { get; }
        IQueryable Plan { get; }
        IQueryable Person { get; }
        IQueryable ShoppingList { get; }

        TVM GetById(int id);

        int Count();

        IEnumerable<T> GetAll();

        IEnumerable<T> Query(Expression<Func<TVM, bool>> filter);

        void Add(TVM entity);

        void Remove(TVM entity);

        void Update(TVM entity, int key);

        void Save(TVM entity);

        void AttachAnIndependentChild<TParent, TChild>(int parentID, int childID)
            where TParent : BaseEntity, IEntity
            where TChild : BaseEntity, IEntity;

        void DetachAnIndependentChild<TParent, TChild>(int parentID, int childID)
            where TParent : BaseEntity, IEntity
            where TChild : BaseEntity, IEntity;
    }
}

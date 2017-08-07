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


    public interface IRepository<T, TVM>
    where T : class
    where TVM : class
    {
        //  EFDbContext context { get; set; }
        IQueryable Ingredient { get; }
        IQueryable Recipe { get; }
        IQueryable Menu { get; }
        IQueryable Plan { get; }
        IQueryable Person { get; }
        IQueryable ShoppingList { get; }

        T  GetTById(int id);
        TVM GetTVMById(int id);

        int Count();

        IEnumerable<T> GetAllT();
        IEnumerable<TVM> GetAllTVM();

        IEnumerable<T> Query(Expression<Func<T , bool>> filter);
        IEnumerable<TVM> Query(Expression<Func<TVM , bool>> filter);

        void AddTVM(TVM entity);

        void AddT(T entity);

        void RemoveTVM(TVM entity); 
        void RemoveT(T t);

        void UpdateTVM(TVM entity, int key);
        void UpdateT(T  t, int key);

        void SaveTVM(TVM entity); 
        void SaveT(T t);

        void AttachAnIndependentChild<TParent, TChild>(int parentID, int childID)
            where TParent : BaseEntity, IEntity
            where TChild : BaseEntity, IEntity;

        void DetachAnIndependentChild<TParent, TChild>(int parentID, int childID)
            where TParent : BaseEntity, IEntity
            where TChild : BaseEntity, IEntity;
    }
}

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


    public interface IRepository<T >
    where T : class, IEntity
    {
        //  EFDbContext context { get; set; }
        IQueryable Ingredient { get; }
        IQueryable Recipe { get; }
        IQueryable Menu { get; }
        IQueryable Plan { get; }
        IQueryable Person { get; }
        IQueryable ShoppingList { get; }

        T  GetById(int id); 
        int Count(); 
        IEnumerable<T> GetAll(); 
        IEnumerable<T> Query(Expression<Func<T , bool>> filter); 
        void Add(T entity); 
        void Remove(T entity);  
        void Update(T entity, int? key); 
        void Save(T entity);  

        void AttachAnIndependentChild<TChild>(int parentID, TChild child, int orderNumber) 
            where TChild : BaseEntity, IEntity;

        void DetachAnIndependentChild<TChild>(int parentID, TChild child, int orderNumber) 
            where TChild : BaseEntity, IEntity,new();
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Domain.Abstract
{
    public interface IRepository
    { }


    public interface IRepository<T >
    where T : class, IEntity
    {   
        string FullPath { get; set; }

        T  GetById(int id); 
        T  GetById(int id, string fullPath);

        int Count(); 
        int Count(string fullPath);

        IEnumerable<T> GetAll(); 
        IEnumerable<T> GetAll(string fullPath);

        IEnumerable<T> Query(Expression<Func<T , bool>> filter); 
        IEnumerable<T> Query(Expression<Func<T , bool>> filter, string fullPath);

        void Remove(T entity);  
        void Remove(T entity, string fullPath);

        void Update(T entity, int? key);  
        void Update(T entity, int? key, string fullPath);

        void Save(T entity);   
        void Save(T entity, string fullPath);
    }
}

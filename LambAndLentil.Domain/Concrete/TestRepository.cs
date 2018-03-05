using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Domain.Concrete
{
    public class TestRepository<T> : IRepository<T>  
        where T : BaseEntity, IEntity 
    {
        public string FullPath { get; set; } 
        private static string folder { get; set; } 

        public TestRepository()  
        {
            char[] charsToTrim = { 'V', 'M' };
         folder = typeof(T).ToString().Split('.').Last().Split('+').Last().TrimEnd(charsToTrim);

            // TODO: get relative path to work.  The first line works in testing but not in running it.
            // fullPath = @"../../../\LambAndLentil.Domain\App_Data\JSON\" + folder + "\\";
           FullPath = @" C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\" + folder + "\\";
        }

        public TestRepository(string path)
        { 
            FullPath = path;
         }

        public T GetById(int id) => new JSONRepository<T>().GetById(id,FullPath);
        public T GetById(int id, string FullPath) => new JSONRepository<T>().GetById(id,FullPath);



        public int Count(string FullPath) => new JSONRepository<T>().Count(FullPath);
        public int Count() => new JSONRepository<T>().Count(FullPath);


        public IEnumerable<T> GetAll(string FullPath) => new JSONRepository<T>().GetAll(FullPath);
        public IEnumerable<T> GetAll() => new JSONRepository<T>().GetAll(FullPath);

   public IEnumerable<T> Query(Expression<Func<T, bool>> filter, string fullPath) => new JSONRepository<T>().Query(filter,FullPath);
        public IEnumerable<T> Query(Expression<Func<T, bool>> filter) => new JSONRepository<T>().Query(filter,FullPath);

        public void Remove(T entity,string FullPath) => new JSONRepository<T>().Remove(entity, FullPath);
        public void Remove(T entity) => new JSONRepository<T>().Remove(entity, FullPath);


        public void Update(T entity, int? key, string  FullPath) => new JSONRepository<T>().Update(entity, key, FullPath);
        public void Update(T entity, int? key) => new JSONRepository<T>().Update(entity, key, FullPath); 
         
        public void Save(T entity, string FullPath) => new JSONRepository<T>().Save(entity, FullPath);
        public void Save(T entity) => new JSONRepository<T>().Save(entity, FullPath);
     
    }
}
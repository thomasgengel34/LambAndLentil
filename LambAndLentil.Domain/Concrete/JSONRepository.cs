using LambAndLentil.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LambAndLentil.Domain.Entities;
using System.Linq.Expressions;
using AutoMapper;
using System.IO;
using Newtonsoft.Json;
using AutoMapper;

namespace LambAndLentil.Domain.Concrete
{
    public class JSONRepository<T, TVM> : IRepository<T, TVM>
        where T : BaseEntity
        where TVM : class, IEntity
    {

        public static MapperConfiguration AutoMapperConfig { get; set; }
        static string folder;
        static string fullPath;

        public JSONRepository()
        {
            folder = typeof(T).ToString().Split('.').Last().Split('+').Last();
             fullPath = @"../../../\LambAndLentil.Domain\AppData\JSON\" + folder + "\\";
           

        }



        // not intending to use
        public EFDbContext context { get; set; }

        public IQueryable Ingredient { get; set; }

        public IQueryable Recipe { get; set; }

        public IQueryable Menu { get; set; }

        public IQueryable Plan { get; set; }

        public IQueryable Person { get; set; }

        public IQueryable ShoppingList { get; set; }

        public void Add(TVM entity)
        { 
            T _data = Mapper.Map<TVM, T>(entity); 
            using (StreamWriter file = File.CreateText(fullPath + entity.Name + ".txt"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, entity);
            }
        }

        public void AttachAnIndependentChild<TParent, TChild>(int parentID, int childID)
            where TParent : BaseEntity, IEntity
            where TChild : BaseEntity, IEntity
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            int fileCount = (from file in Directory.EnumerateFiles(fullPath, "*.txt", SearchOption.AllDirectories)
                             select file).Count();

            return fileCount;
        }

        public void DetachAnIndependentChild<TParent, TChild>(int parentID, int childID)
            where TParent : BaseEntity, IEntity
            where TChild : BaseEntity, IEntity
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public TVM GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query(Expression<Func<TVM, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Remove(TVM entity)
        {
            throw new NotImplementedException();
        }

        public void Save(TVM entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TVM entity, int key)
        {
            throw new NotImplementedException();
        }
    }
}

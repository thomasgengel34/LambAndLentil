using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace LambAndLentil.Domain.Concrete
{
    public class TestRepository<T, TVM> : IRepository<T, TVM>
        where T : BaseEntity, IEntity
        where TVM : class, IEntity
    { 
        static string folder;
        static string fullPath;

        public TestRepository() : base()
        {
            char[] charsToTrim = { 'V', 'M' };
            folder = typeof(T).ToString().Split('.').Last().Split('+').Last().TrimEnd(charsToTrim);

            // TODO: get relative path to work.  The first line works in testing but not in running it.
            // fullPath = @"../../../\LambAndLentil.Domain\App_Data\JSON\" + folder + "\\";
            fullPath = @" C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\" + folder + "\\";
        }




        public IQueryable Ingredient { get; set; }

        public IQueryable Recipe { get; set; }

        public IQueryable Menu { get; set; }

        public IQueryable Plan { get; set; }

        public IQueryable Person { get; set; }

        public IQueryable ShoppingList { get; set; }

        public void Add(TVM entity)
        {
            T _data = Mapper.Map<TVM, T>(entity);
            // using the ID allows spaces in the name
            using (StreamWriter file = File.CreateText(fullPath + entity.ID + ".txt"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _data);
            }
        }

        public void AddT(T entity)
        {
            using (StreamWriter file = File.CreateText(fullPath + entity.ID + ".txt"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, entity);
            }
        }

        /// <summary>
        /// not yet ready for prime time
        /// </summary>
        /// <param name="oldValue">  </param>
        /// <param name="newValue"></param>
        private void ReplaceStringInJsonDir(string oldValue, string newValue)
        {

            IEnumerable<string> files = Directory.EnumerateDirectories(@" C:\Dev\TGE\LambAndLentil\LambAndLentil.Domain\App_Data\JSON\", null, SearchOption.AllDirectories);
            foreach (string file in files)
            {
                string text = File.ReadAllText(file);
                text = text.Replace(oldValue, newValue);
                File.WriteAllText(file, text);
            }
        }

        public void AttachAnIndependentChild<T, TChild>(int parentID, int childID)
            where T : BaseEntity, IEntity
            where TChild : BaseEntity, IEntity
        {
            T parent = JsonConvert.DeserializeObject<T>(File.ReadAllText(String.Concat(fullPath, parentID, ".txt")));
            TChild child = JsonConvert.DeserializeObject<TChild>(File.ReadAllText(String.Concat(fullPath, childID, ".txt")));

            if (parent != null && child != null)
            {
                if (typeof(TChild) == typeof(Ingredient))
                {
                    parent.Ingredients.Add(child as Ingredient);
                }
                if (typeof(TChild) == typeof(Recipe))
                {
                    parent.Recipes.Add(child as Recipe);
                }
                if (typeof(TChild) == typeof(Menu))
                {
                    parent.Menus.Add(child as Menu);
                }
                if (typeof(TChild) == typeof(Plan))
                {
                    parent.Plans.Add(child as Plan);
                }
                if (typeof(TChild) == typeof(Person))
                {
                    parent.Persons.Add(child as Person);
                }
                if (typeof(TChild) == typeof(ShoppingList))
                {
                    parent.ShoppingLists.Add(child as ShoppingList);
                }
                TVM entity = Mapper.Map<T, TVM>(parent);
                Add(entity);
            }
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

        //public IEnumerable<T> GetAll()
        //{
        //    var result = from file in Directory.EnumerateFiles(fullPath, "*.txt", SearchOption.AllDirectories)
        //                 select file;
        //    List<T> list = new List<T>();

        //    foreach (string file in result)
        //    {
        //        T entity = JsonConvert.DeserializeObject<T>(File.ReadAllText(String.Concat(file)));
        //        list.Add(entity);
        //    }

        //    return list;
        //}

        public TVM GetById(int id)
        {
            IEnumerable<string> availableFiles = Directory.EnumerateFiles(fullPath);

            var result = from f in availableFiles
                         where f == string.Concat(fullPath, id, ".txt")
                         select f;

            if (result != null)
            {
                T entity = JsonConvert.DeserializeObject<T>(File.ReadAllText(String.Concat(fullPath, id, ".txt")));
                return Mapper.Map<T, TVM>(entity);
            }
            throw (new Exception());
        }



        public IEnumerable<T> Query(Expression<Func<TVM, bool>> filter)
        {
            throw new NotImplementedException();
        }

        //public void Remove(TVM entity)
        //{
        //    File.Delete(String.Concat(fullPath, entity.ID, ".txt"));
        //}

        public void RemoveT(T t)
        {
            File.Delete(String.Concat(fullPath, t.ID, ".txt"));
        }

        public void Save(TVM entity)
        {
            Add(entity);
        }

        public void SaveT(T t)
        {
            AddT(t);
        }

        public void Update(TVM entity, int key)
        {
            entity.ModifiedDate = DateTime.Now;
            Add(entity);
        }

        public void UpdateT(T t, int key)
        {
            t.ModifiedDate = DateTime.Now;
            AddT(t);
        }

        public T GetTById(int id)
        {
            IEnumerable<string> availableFiles = Directory.EnumerateFiles(fullPath);

            var result = from f in availableFiles
                         where f == string.Concat(fullPath, id, ".txt")
                         select f;

            if (result.Count() > 0)
            {
                T entity = JsonConvert.DeserializeObject<T>(File.ReadAllText(String.Concat(fullPath, id, ".txt")));
                return entity;
            }
            else
            {
                return null;
            }

        }

        public TVM GetTVMById(int id)
        {
            IEnumerable<string> availableFiles = Directory.EnumerateFiles(fullPath);

            var result = from f in availableFiles
                         where f == string.Concat(fullPath, id, ".txt")
                         select f;

            if (result.Count()>0)
            {
                T entity = JsonConvert.DeserializeObject<T>(File.ReadAllText(String.Concat(fullPath, id, ".txt")));
                return Mapper.Map<T, TVM>(entity);
            }
            else
            {
              return null;
            }

        }

        public IEnumerable<T> GetAllT()
        {
            var result = from file in Directory.EnumerateFiles(fullPath, "*.txt", SearchOption.AllDirectories)
                         select file;
            List<T> list = new List<T>();

            foreach (string file in result)
            {
                T entity = JsonConvert.DeserializeObject<T>(File.ReadAllText(String.Concat(file)));
                list.Add(entity);
            }
            return list;
        }

        public IEnumerable<TVM> GetAllTVM()
        {
            var result = from file in Directory.EnumerateFiles(fullPath, "*.txt", SearchOption.AllDirectories)
                         select file;
            List<TVM> list = new List<TVM>();

            foreach (string file in result)
            {
                TVM entity = JsonConvert.DeserializeObject<TVM>(File.ReadAllText(String.Concat(file)));
                list.Add(entity);
            }
            return list;
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<TVM> IRepository<T, TVM>.Query(Expression<Func<TVM, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void AddTVM(TVM entity)
        {
            T t = Mapper.Map<TVM, T>(entity);
            AddT(t);
        }

        public void RemoveTVM(TVM entity)
        {
            File.Delete(String.Concat(fullPath, entity.ID, ".txt"));
        }

        public void UpdateTVM(TVM entity, int key)
        {
            entity.ModifiedDate = DateTime.Now;
            Add(entity);
        }

        public void SaveTVM(TVM entity)
        {
            Add(entity);
        }
    }
}
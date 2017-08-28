 
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
    public class JSONRepository<T> : IRepository<T>
        where T : class, IEntity
    {
        // T should be an incoming view model, TE is the entity in the db
        // but LambAndLentil.Domain only deals with Domain, does not have a dependency on UI and cannot,should not, will not.

 
        static string Folder { get; set; } 
        protected static string FullPath { get; set; } 

       public JSONRepository()
        {
            char[] charsToTrim = { 'V', 'M' };
            Folder = typeof(T).ToString().Split('.').Last().Split('+').Last().TrimEnd(charsToTrim);
            string className = typeof(T).ToString().TrimEnd(charsToTrim);
         //   E = Type.GetType(className, true);
            // TODO: get relative path to work.  The first line works in testing but not in running it.
            // fullPath = @"../../../\LambAndLentil.Domain\App_Data\JSON\" + folder + "\\";
            FullPath = @" C:\Dev\TGE\LambAndLentil\LambAndLentil.Domain\App_Data\JSON\" + Folder + "\\";
        }
       

        public IQueryable Ingredient { get; set; }

        public IQueryable Recipe { get; set; }

        public IQueryable Menu { get; set; }

        public IQueryable Plan { get; set; }

        public IQueryable Person { get; set; }

        public IQueryable ShoppingList { get; set; }

        public void Add(T entity)
        {
            // using the ID allows spaces in the name
            using (StreamWriter file = File.CreateText(FullPath + entity.ID + ".txt"))
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

        public void AttachAnIndependentChild<TChild>(int parentID, int childID) 
            where TChild : BaseEntity,  IEntity 
        {
            T parent = JsonConvert.DeserializeObject<T>(File.ReadAllText(String.Concat(FullPath, parentID, ".txt")));
            TChild child = JsonConvert.DeserializeObject<TChild>(File.ReadAllText(String.Concat(FullPath, childID, ".txt")));

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
            }
        }

        public int Count()
        {
            int fileCount = (from file in Directory.EnumerateFiles(FullPath, "*.txt", SearchOption.AllDirectories)
                             select file).Count();

            return fileCount;
        }

        public void DetachAnIndependentChild<TChild>(int parentID, int childID) 
            where TChild : BaseEntity, IEntity
        {
            throw new NotImplementedException();
        }
         
        public T GetById(int id)
        {
            IEnumerable<string> availableFiles = Directory.EnumerateFiles(FullPath);

            var result = from f in availableFiles
                         where f == string.Concat(FullPath, id, ".txt")
                         select f;

            if (result.Count() > 0)
            {
                T entity = JsonConvert.DeserializeObject<T>(File.ReadAllText(String.Concat(FullPath, id, ".txt")));
                return entity;
            }
            else
            {
                return null;
            } 
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        //public void Remove(T entity)
        //{
        //    File.Delete(String.Concat(fullPath, entity.ID, ".txt"));
        //}

        public void Remove(T t)
        {
            File.Delete(String.Concat(FullPath, t.ID, ".txt"));
        }
         

        public void Save(T entity)
        {
            Add(entity);
        }

      
        public void Update(T entity, int key)
        {
            entity.ModifiedDate = DateTime.Now;
            Add(entity);
        }
         

        public IEnumerable<T> GetAll()
        {
            var result = from file in Directory.EnumerateFiles(FullPath, "*.txt", SearchOption.AllDirectories)
                         select file;
            List<T> list = new List<T>();

            foreach (string file in result)
            {
                T entity = JsonConvert.DeserializeObject<T>(File.ReadAllText(String.Concat(file)));
                list.Add(entity);
            }
            return list;
        } 
    }
}

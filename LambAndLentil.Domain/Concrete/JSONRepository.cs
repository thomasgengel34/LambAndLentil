using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;

namespace LambAndLentil.Domain.Concrete
{
    public class JSONRepository<T> : IRepository<T>
         where T : BaseEntity, IEntity
    {
        private static string className = typeof(T).ToString().Split('.').Last().Split('+').Last();
        public string FullPath { get; set; } = @" C:\Dev\TGE\LambAndLentil\LambAndLentil.Domain\App_Data\JSON\" + className + "\\";
        //  internal static string FullPath { get; set; } = "foo";

        public JSONRepository()
        {
        }

        public JSONRepository(string path)
        {
            FullPath = path;
        }

        public IQueryable Ingredient { get; set; }
        public IQueryable Recipe { get; set; }
        public IQueryable Menu { get; set; }
        public IQueryable Plan { get; set; }
        public IQueryable Person { get; set; }
        public IQueryable ShoppingList { get; set; }

        public void Save(T entity, string FullPath)
        {
            entity.ModifiedByUser = WindowsIdentity.GetCurrent().Name;
            using (StreamWriter file = File.CreateText(FullPath + entity.ID + ".txt"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, entity);
            }
        }

        public void Save(T entity)
        {
            Save(entity, FullPath);
        }

        public int Count() => Count(FullPath);

        public int Count(string fullPath)
        {
            FullPath = fullPath;
            int fileCount = (from file in Directory.EnumerateFiles(FullPath, "*.txt", SearchOption.AllDirectories)
                             select file).Count();

            return fileCount;
        }




        public T GetById(int id)
        {
            return GetById(id, FullPath);
        }

        public T GetById(int id, string fullPath)
        {
            FullPath = fullPath;  
            IEnumerable<string> availableFiles = Directory.EnumerateFiles(FullPath);
            string path = string.Concat(FullPath, id, ".txt");
            var result = from f in availableFiles
                         where f == path
                         select f;

            if (result.Count() > 0)
            {
                // Menu entity = JsonConvert.DeserializeObject<Menu>(File.ReadAllText(@"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Menu\101.txt"));
                T entity = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                return entity as T;
            }
            else
            {
                return null;
            }
        }






        public IEnumerable<T> Query(Expression<Func<T, bool>> filter)
        {
            return Query(filter, FullPath);
        } 

        public IEnumerable<T> Query(Expression<Func<T, bool>> filter, string fullPath) => throw new NotImplementedException();



        public void Remove(T t) => Remove(t, FullPath);

        public void Remove(T entity, string fullPath)
        {
            FullPath = fullPath;
            File.Delete(String.Concat(FullPath, entity.ID, ".txt"));
        }
 


        public void Update(T entity, int? key) => Update(entity, key, FullPath);

        public void Update(T entity, int? key, string fullPath)
        {
            FullPath = fullPath;
            T oldEntity = GetById(entity.ID);
            if (oldEntity != null)
            {
                entity.CreationDate = oldEntity.CreationDate;
                entity.AddedByUser = oldEntity.AddedByUser;
                entity.ModifiedDate = DateTime.Now;
                entity.ModifiedByUser = WindowsIdentity.GetCurrent().Name;

                if (typeof(T) == typeof(Person))
                {
                    Person person = entity as Person;
                    Person oldPerson = oldEntity as Person;

                    /* first name changed, last name did not, then name changes, full name changes
                     * last name changed, first name did not, then name changes, full name changes
                     * name changes but full name did not, first name ="" and last name ="", fullname=name
                     * 
                     * full name changes but name did not, first name ="" and last name ="", name=fullname  */
                    if (person.Name != oldPerson.Name || person.FullName != oldPerson.FullName)
                    {
                        person.FirstName = "";
                        person.LastName = "";

                        if (person.Name != oldPerson.Name)
                        {
                            person.FullName = person.Name;
                        }
                        else if (person.FullName != oldPerson.FullName)
                        {
                            person.Name = person.FullName;
                        }
                    }
                    else
                    {
                        person.FullName = person.GetName(person.FirstName, person.LastName);
                    }
                    if (entity.Name != oldEntity.Name)
                    {
                        person.FullName = entity.Name;
                    }

                    entity.Name = person.FullName;
                }
            }
            Save(entity, FullPath);
        }

       
        public IEnumerable<T> GetAll() => GetAll(FullPath);

        public IEnumerable<T> GetAll(string fullPath)
        {
            FullPath = fullPath;

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

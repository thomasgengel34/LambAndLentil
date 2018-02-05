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
        protected static string FullPath { get; set; }
        private static string className;

        public JSONRepository()
        {
            char[] charsToTrim = { 'V', 'M' };
            className = typeof(T).ToString().Split('.').Last().Split('+').Last().TrimEnd(charsToTrim);
            // className = Folder;

            FullPath = @" C:\Dev\TGE\LambAndLentil\LambAndLentil.Domain\App_Data\JSON\" + className + "\\";
        }


        public IQueryable Ingredient { get; set; }
        public IQueryable Recipe { get; set; }
        public IQueryable Menu { get; set; }
        public IQueryable Plan { get; set; }
        public IQueryable Person { get; set; }
        public IQueryable ShoppingList { get; set; }

        public void Save(T entity)
        {
            entity.ModifiedByUser = WindowsIdentity.GetCurrent().Name;
            using (StreamWriter file = File.CreateText(FullPath + entity.ID + ".txt"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, entity);
            }
        }

         
        public int Count()
        {
            int fileCount = (from file in Directory.EnumerateFiles(FullPath, "*.txt", SearchOption.AllDirectories)
                             select file).Count();

            return fileCount;
        }


      

        public T GetById(int id)
        {
            IEnumerable<string> availableFiles = Directory.EnumerateFiles(FullPath);
            string path = string.Concat(FullPath, id, ".txt");
            var result = from f in availableFiles
                         where f == path
                         select f;

            if (result.Count() > 0)
            {
                   // Menu entity = JsonConvert.DeserializeObject<Menu>(File.ReadAllText(@"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Menu\101.txt"));
                 T entity = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                return  entity as T;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> filter) => throw new NotImplementedException();

        public void Remove(T t) => File.Delete(String.Concat(FullPath, t.ID, ".txt")); 

        public void Update(T entity, int? key)
        {
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
            Save(entity);
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

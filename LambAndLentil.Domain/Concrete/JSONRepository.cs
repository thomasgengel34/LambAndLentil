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

        public void Add(T entity)
        {
            entity.ModifiedByUser = WindowsIdentity.GetCurrent().Name;
            using (StreamWriter file = File.CreateText(FullPath + entity.ID + ".txt"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, entity);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TChild"></typeparam>
        /// <param name="parentID"></param>
        /// <param name="child"></param>
        /// <param name="orderNumber">zero-based index in list currently used only in Detach version</param>
        public void AttachAnIndependentChild<TChild>(int parentID, TChild child, int orderNumber = 0)
            where TChild : BaseEntity, IEntity
        {
            char[] charsToTrim = { 'V', 'M' };
            string childName = typeof(TChild).ToString().Split('.').Last().Split('+').Last().TrimEnd(charsToTrim);
            T parent = JsonConvert.DeserializeObject<T>(File.ReadAllText(String.Concat(FullPath, parentID, ".txt")));

            if (parent != null && child != null)
            {
                T entity = JsonConvert.DeserializeObject<T>(File.ReadAllText
                    (String.Concat(FullPath, parentID, ".txt")));
              //  child.AddChildrenToParent(entity);
                switch (childName)
                {
                    case "Ingredient":
                        IEntityChildClassIngredients item = (IEntityChildClassIngredients)entity;
                        item.Ingredients.Add(child as Ingredient);
                        entity = (T)item;
                        break;
                    case "Recipe":
                        IEntityChildClassRecipes itemR = (IEntityChildClassRecipes)entity;
                        itemR.Recipes.Add(child as Recipe);
                        entity = (T)itemR;
                        break;
                    case "Menu":
                        Type t = entity.GetType().GetInterface("IEntityChildClassMenus");
                        if (t != null)
                        {
                            IEntityChildClassMenus itemM = (IEntityChildClassMenus)entity;
                            itemM.Menus.Add(child as Menu);
                            entity = (T)itemM;
                        }

                        break;
                    case "Plan":
                        IEntityChildClassPlans itemP = (IEntityChildClassPlans)entity;
                        itemP.Plans.Add(child as Plan);
                        entity = (T)itemP;
                        break;
                    case "ShoppingList":
                        IEntityChildClassShoppingLists itemS = (IEntityChildClassShoppingLists)entity;
                        itemS.ShoppingLists.Add(child as ShoppingList);
                        entity = (T)itemS;
                        break;
                    default:
                        break;
                }
                Update(entity, entity.ID);
            }
        }

        public int Count()
        {
            int fileCount = (from file in Directory.EnumerateFiles(FullPath, "*.txt", SearchOption.AllDirectories)
                             select file).Count();

            return fileCount;
        }


        public void DetachAnIndependentChild<TChild>(int parentID, TChild child, int orderNumber = 1)
            where TChild : BaseEntity, IEntity
        {
            char[] charsToTrim = { 'V', 'M' };
            string childName = typeof(TChild).ToString().Split('.').Last().Split('+').Last().TrimEnd(charsToTrim);
            T parent = JsonConvert.DeserializeObject<T>(File.ReadAllText(String.Concat(FullPath, parentID, ".txt")));

            if (parent != null && child != null)
            {
                T entity = JsonConvert.DeserializeObject<T>(File.ReadAllText
                (String.Concat(FullPath, parentID, ".txt")));

                switch (childName)
                {
                    case "Ingredient":
                        IEntityChildClassIngredients item = (IEntityChildClassIngredients)entity;
                        if (item.Ingredients.Count >= orderNumber && item.Ingredients.Count > 0)
                        {
                            item.Ingredients.RemoveAt(orderNumber);
                            entity = (T)item;
                        }
                        break;
                    case "Recipe":
                        IEntityChildClassRecipes itemR = (IEntityChildClassRecipes)entity;
                        itemR.Recipes.RemoveAt(orderNumber);
                        entity = (T)itemR;
                        break;
                    case "Menu":
                        IEntityChildClassMenus itemM = (IEntityChildClassMenus)entity;
                        itemM.Menus.RemoveAt(orderNumber);
                        entity = (T)itemM;
                        break;
                    case "Plan":
                        IEntityChildClassPlans itemP = (IEntityChildClassPlans)entity;
                        itemP.Plans.RemoveAt(orderNumber);
                        entity = (T)itemP;
                        break;
                    case "ShoppingList":
                        IEntityChildClassShoppingLists itemS = (IEntityChildClassShoppingLists)entity;
                        itemS.ShoppingLists.RemoveAt(orderNumber);
                        entity = (T)itemS;
                        break;
                    default:
                        break;
                }
                Update(entity, entity.ID);
            }
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

        public IEnumerable<T> Query(Expression<Func<T, bool>> filter) => throw new NotImplementedException();

        public void Remove(T t) => File.Delete(String.Concat(FullPath, t.ID, ".txt"));


        public void Save(T entity) => Add(entity);


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

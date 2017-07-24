using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace LambAndLentil.Domain.Concrete
{
    public class EFRepository<T,TVM> : IRepository<T,TVM>
        where T :class
        where TVM : class, IEntity
        
    {
        EFDbContext context;

        public EFRepository()
        {
            context = new EFDbContext(); 
        }

        private dynamic ConvertVMtoClass()
        {
            string className = GetPlainClassName();
            dynamic type = Type.GetType(className, true); 
            return type;
        }

        public string GetPlainClassName()
        {
            char[] charsToTrim = { 'V', 'M' };
            string className = typeof(TVM).ToString().TrimEnd(charsToTrim);
            char[] splitterArray = { '.' };
            string[] classNameArray = className.Split(splitterArray);
            className = String.Concat("LambAndLentil.Domain.Entities.",classNameArray.Last());
            return className;
        }

        public string GetClassName()
        {
            string className = GetPlainClassName();
            return (className == "ShoppingList") ? "Shopping List" : className;
        }

        //EFDbContext IRepository<T>.context { get; set; } 

        public IQueryable Ingredient => context.Ingredient;
        public IQueryable Recipe => context.Recipe;
        public IQueryable Menu => context.Menu;
        public IQueryable Plan => context.Plan;
        public IQueryable Person => context.Person;
        public IQueryable ShoppingList => context.ShoppingList;

        EFDbContext IRepository<T,TVM>.context { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public void Add(TVM entity)
        {
            dynamic T = ConvertVMtoClass();
            // object T = new Ingredient();
            AddHelper((dynamic)T, entity);
        }

        public void AddHelper<T>(T obj, TVM entity)
            where T : BaseEntity, IEntity
        {
            AddWithT<T>(entity);
        }

        private void AddWithT<T>(TVM entity)
            where T : BaseEntity, IEntity
        {

            T item = Mapper.Map<TVM, T>(entity);
            context.Set<T>().Add(item);
            context.SaveChanges();
        }

        public void AttachAnIndependentChild<TParent, TChild>(int parentID, int childID)
            where TParent : BaseEntity, IEntity
            where TChild : BaseEntity, IEntity
        {
            using (var context = new EFDbContext())
            {
                TParent parent = context.Set<TParent>().Find(parentID);
                TChild child = context.Set<TChild>().Find(childID);

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
                    context.SaveChanges();
                }
            }
        }

        public void DetachAnIndependentChild<TParent, TChild>(int parentID, int childID)
            where TParent : BaseEntity, IEntity
            where TChild : BaseEntity, IEntity
        {
            using (var context = new EFDbContext())
            {
                TParent parent = context.Set<TParent>().Find(parentID);
                TChild child = context.Set<TChild>().Find(childID);

                if (parent != null && child != null)
                {
                    if (typeof(TChild) == typeof(Ingredient))
                    {
                        parent.Ingredients.Remove(child as Ingredient);
                    }
                    if (typeof(TChild) == typeof(Recipe))
                    {
                        parent.Recipes.Remove(child as Recipe);
                    }
                    if (typeof(TChild) == typeof(Menu))
                    {
                        parent.Menus.Remove(child as Menu);
                    }
                    if (typeof(TChild) == typeof(Plan))
                    {
                        parent.Plans.Remove(child as Plan);
                    }
                    if (typeof(TChild) == typeof(Person))
                    {
                        parent.Persons.Remove(child as Person);
                    }
                    if (typeof(TChild) == typeof(ShoppingList))
                    {
                        parent.ShoppingLists.Remove(child as ShoppingList);
                    }
                    context.SaveChanges();
                }
            }
        }

        public IEnumerable<T> GetAll()
        {
            //string T = GetPlainClassName();
            return (IEnumerable<T>)context.Set<T>().ToList();
            // return GetAllHelper(T);
        }

        //private IEnumerable<T> GetAllHelper<T>(T obj)
        //       where T : class
        //{

        //    return GetAllWithT<T>();
        //}

        //private IEnumerable<T> GetAllWithT<T>()
        //    where T : class
        //{
        //    return (IEnumerable<T>)context.Set<T>().ToList(); 
        //}


        public TVM GetById(int id)
        { 
          T item = context.Set<T>().Find(id); 
            return Mapper.Map<T, TVM>(item);
        }
       
        public IEnumerable<T> Query(Expression<Func<TVM, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Remove(TVM entity)
        {
            Type T = ConvertVMtoClass();
            RemoveHelper((dynamic)T, entity);
        }

        private void RemoveHelper<T>(T obj, TVM entity)
            where T : class
        {
            RemoveWithT<T>(entity);
        }

        private void RemoveWithT<T>(TVM entity)
             where T : class
        {
            T item = Mapper.Map<TVM, T>(entity);
            context.Set<T>().Remove(item);
            context.SaveChanges();
        }

        private void Remove<T>(TVM entity) where T : class
        {
            T item = Mapper.Map<TVM, T>(entity);
            context.Set<T>().Remove(item);
            context.SaveChanges();
        }

        public void Save(TVM entity)
        {
            Type T = ConvertVMtoClass();
            SaveHelper((dynamic)T, entity);
        }

        private void SaveHelper<T>(T ob, TVM entity)
   where T : class, IEntity
        {
            SaveWithT<T>(entity);
        }

        private void SaveWithT<T>(TVM entity)
         where T : class, IEntity
        {
            T item = Mapper.Map<TVM, T>(entity);

            if (item == null)
                return;

            T existing = context.Set<T>().Find(item.ID);
            if (existing != null)
            {
                context.Entry(existing).CurrentValues.SetValues(item);
                context.SaveChanges();
            }
            else
            {
                Add(entity);
            }
        }



        public void Update(TVM entity, int key)
        {
            dynamic T = ConvertVMtoClass();
            UpdateHelper( T, entity, key);
        }

        private void UpdateHelper<T>(T ob, TVM entity, int key)
        where T : BaseEntity
        {
            UpdateWithT<T>(entity, key);
        }

        private void UpdateWithT<T>(TVM entity, int key)
        where T : BaseEntity
        {
            T updated = Mapper.Map<TVM, T>(entity);
            T existing = context.Set<T>().Find(key);
            if (existing != null)
            {
                context.Entry(existing).CurrentValues.SetValues(entity);

                // later search for collections on entity using reflection
                // todo: add for all
                foreach (Ingredient ingredient in existing.Ingredients)
                {
                    var ingredientEntry = context.Entry(existing.Ingredients.Where(s => s.ID == ingredient.ID).FirstOrDefault());
                    ingredientEntry.State = EntityState.Deleted;
                }
                context.SaveChanges();
                {
                    // TODO: add other types that might be children
                    // later search for collections on entity using reflection
                    foreach (Ingredient ingredient in entity.Ingredients)
                    {
                        var ingredientEntry = context.Entry(entity.Ingredients.Where(s => s.ID == ingredient.ID).FirstOrDefault());
                        ingredientEntry.State = EntityState.Added;
                    }
                    context.SaveChanges();
                }
            }
        }

        public int Count()
        {
            Type T = ConvertVMtoClass();
            return CountHelper((dynamic)T);
        }

        private int CountHelper<T>(T ob)
        where T : BaseEntity, IEntity
        {
            return CountWithT<T>();
        }


        private int CountWithT<T>()
                where T : BaseEntity, IEntity
        {
            return context.Set<T>().ToList().Count;
        }
    }
}
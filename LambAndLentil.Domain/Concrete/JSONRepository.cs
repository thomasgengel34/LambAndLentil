﻿using LambAndLentil.Domain.Abstract;
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
        // T should be an incoming entity. It cannot be a view model.  LambAndLentil.Domain only deals with Domain, does not have a dependency on UI and cannot,should not, will not. I can get away with this as is because the ViewModels are identical to the Entities. If that changes, this will have to change.


        static string Folder { get; set; }
        protected static string FullPath { get; set; }
        private static string className;

        public JSONRepository()
        {
            char[] charsToTrim = { 'V', 'M' };
            Folder = typeof(T).ToString().Split('.').Last().Split('+').Last().TrimEnd(charsToTrim);
            //className = typeof(T).ToString().TrimEnd(charsToTrim);
            className = Folder;
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
            entity.ModifiedByUser = WindowsIdentity.GetCurrent().Name;
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
                if (className == "Ingredient")   // can only attach an Ingredient   
                {
                    parent.Ingredients.Add(child as Ingredient);
                    Update(parent, parent.ID);
                }
                else if (className == "Recipe")   // can only attach an Ingredient   
                {
                    Recipe recipe = JsonConvert.DeserializeObject<Recipe>(File.ReadAllText(String.Concat(FullPath, parentID, ".txt")));
                    if (childName == "Ingredient")
                    {
                        Ingredient ingredient = child as Ingredient;
                        recipe.Ingredients.Add(ingredient);
                        Add(recipe as T);
                    }
                    else
                    {
                        // see notes above.  How did we get here? Try to and figure out how to block it. 
                    }
                }
                else if (className == "Menu")   // can  attach an Ingredient or recipe
                {
                    Menu menu = JsonConvert.DeserializeObject<Menu>(File.ReadAllText
                        (String.Concat(FullPath, parentID, ".txt")));

                    if (childName == "Ingredient")
                    {
                        if (menu.Ingredients == null)
                        {
                            menu.Ingredients = new List<Ingredient>();
                        }
                        menu.Ingredients.Add(child as Ingredient);
                        IEntity menu1 = menu;
                        Add((T)menu1);
                    }
                    else if (childName == "Recipe")
                    {
                        menu.Recipes.Add(child as Recipe);
                    }
                }
                else if (className == "Plan")   // can  attach an Ingredient or recipe or menu
                {
                    Plan plan = JsonConvert.DeserializeObject<Plan>(File.ReadAllText
                        (String.Concat(FullPath, parentID, ".txt")));

                    if (childName == "Ingredient")
                    {
                        plan.Ingredients.Add(child as Ingredient);

                    }
                    else if (childName == "Recipe")
                    {
                        plan.Recipes.Add(child as Recipe);
                    }
                    else if (childName == "Menu")
                    {
                        if (plan.Menus == null)
                        {
                            plan.Menus = new List<Menu>();
                        }

                        plan.Menus.Add(child as Menu);
                    }
                    Update(plan as T, plan.ID);
                }
                else if (className == "ShoppingList")   // can  attach an Ingredient or recipe or menu or plan
                {
                    ShoppingList shoppingList = JsonConvert.DeserializeObject<ShoppingList>(File.ReadAllText
                        (String.Concat(FullPath, parentID, ".txt")));

                    if (childName == "Ingredient")
                    {
                        shoppingList.Ingredients.Add(child as Ingredient);
                    }
                    else if (childName == "Recipe")
                    {
                        shoppingList.Recipes.Add(child as Recipe);
                    }
                    else if (childName == "Menu")
                    {
                        shoppingList.Menus.Add(child as Menu);
                    }
                    else if (childName == "Plan")
                    {
                        shoppingList.Plans.Add(child as Plan);
                    }
                    Update(shoppingList as T, shoppingList.ID);
                }
                else if (className == "Person")
                {
                    Person person = JsonConvert.DeserializeObject<Person>(File.ReadAllText
                        (String.Concat(FullPath, parentID, ".txt")));

                    if (childName == "Ingredient")
                    {
                        if (person.Ingredients == null)
                        {
                            person.Ingredients = new List<Ingredient>();
                        }
                        person.Ingredients.Add(child as Ingredient);
                    }
                    else if (childName == "Recipe")
                    {
                        person.Recipes.Add(child as Recipe);
                    }
                    else if (childName == "Menu")
                    {
                        person.Menus.Add(child as Menu);
                    }
                    else if (childName == "Plan")
                    {
                        person.Plans.Add(child as Plan);
                    }
                    else if (childName == "ShoppingList")
                    {
                        person.ShoppingLists.Add(child as ShoppingList);
                    }
                    Update(person as T, person.ID);
                }
            }
        }




        public int Count()
        {
            int fileCount = (from file in Directory.EnumerateFiles(FullPath, "*.txt", SearchOption.AllDirectories)
                             select file).Count();

            return fileCount;
        }

        /// <summary>
        /// Detach a child that can exist on its own without the parent entity
        /// </summary>
        /// <typeparam name="TChild">the child that is attached</typeparam>
        /// <param name="parentID">ID of the parent</param>
        /// <param name="child">actual entity to be detached</param>
        /// <param name="orderNumber">zero-based index on the list of children</param>
        public void DetachAnIndependentChild<TChild>(int parentID, TChild child, int orderNumber = 0)
            where TChild : BaseEntity, IEntity
        {
            char[] charsToTrim = { 'V', 'M' };
            string childName = typeof(TChild).ToString().Split('.').Last().Split('+').Last().TrimEnd(charsToTrim);
            T parent = JsonConvert.DeserializeObject<T>(File.ReadAllText(String.Concat(FullPath, parentID, ".txt")));

            if (parent != null && child != null)
            {
                if (className == "Ingredient")
                {
                    Ingredient ingredientChild = child as Ingredient;
                    //    bool successfulRemoval = parent.Ingredients.Remove(ingredientChild); 
                    parent.Ingredients.RemoveAt(orderNumber);
                    Update(parent as T, parent.ID);
                }
                if (className == "Recipe")   // can only attach an Ingredient   
                {
                    Recipe recipe = JsonConvert.DeserializeObject<Recipe>(File.ReadAllText(String.Concat(FullPath, parentID, ".txt")));
                    if (childName == "Ingredient")
                    {
                        recipe.Ingredients.RemoveAt(orderNumber);
                    }
                    else
                    {
                        // see notes above.  How did we get here? Try to and figure out how to block it. 
                    }
                    Update(recipe as T, parent.ID);
                }
                else if (className == "Menu")   // can  attach an Ingredient or recipe
                {
                    Menu menu = JsonConvert.DeserializeObject<Menu>(File.ReadAllText
                        (String.Concat(FullPath, parentID, ".txt")));

                    if (childName == "Ingredient")
                    { 
                        menu.Ingredients.RemoveAt(orderNumber); 
                    }
                    else if (childName == "Recipe")
                    {
                        menu.Recipes.Remove(child as Recipe);
                    }
                    Update(menu as T, parent.ID);
                }
                else if (className == "Plan")   // can  attach an Ingredient or recipe or menu
                {
                    Plan plan = JsonConvert.DeserializeObject<Plan>(File.ReadAllText
                        (String.Concat(FullPath, parentID, ".txt")));

                    if (childName == "Ingredient")
                    {
                        plan.Ingredients.RemoveAt(orderNumber);
                    }
                    else if (childName == "Recipe")
                    {
                        plan.Recipes.Remove(child as Recipe);
                    }
                    else if (childName == "Menu")
                    {
                         parent.Ingredients.RemoveAt(orderNumber);
                        Update(parent as T, parent.ID);
                    }
                    Update(plan as T, parent.ID);
                }
                else if (className == "ShoppingList")   // can  attach an Ingredient or recipe or menu or plan
                {
                    ShoppingList shoppingList = JsonConvert.DeserializeObject<ShoppingList>(File.ReadAllText
                        (String.Concat(FullPath, parentID, ".txt")));

                    if (childName == "Ingredient")
                    { 
                        shoppingList.Ingredients.RemoveAt(orderNumber);
                    }
                    else if (childName == "Recipe")
                    {
                        shoppingList.Recipes.Remove(child as Recipe);
                    }
                    else if (childName == "Menu")
                    {
                        shoppingList.Menus.Remove(child as Menu);
                    }
                    else if (childName == "Plan")
                    {
                        shoppingList.Plans.Remove(child as Plan);
                    }
                    Update(shoppingList as T, parent.ID);
                }
                else if (className == "Person")
                {
                    Person person = JsonConvert.DeserializeObject<Person>(File.ReadAllText
                        (String.Concat(FullPath, parentID, ".txt")));

                    if (childName == "Ingredient")
                    {
                        parent.Ingredients.RemoveAt(orderNumber);
                        Update(parent as T, parent.ID);
                    }
                    else if (childName == "Recipe")
                    {
                        person.Recipes.Remove(child as Recipe);
                    }
                    else if (childName == "Menu")
                    {
                        person.Menus.Remove(child as Menu);
                    }
                    else if (childName == "Plan")
                    {
                        person.Plans.Remove(child as Plan);
                    }
                    else if (childName == "ShoppingList")
                    {
                        person.ShoppingLists.Remove(child as ShoppingList);
                    }
                    Update(parent as T, parent.ID);
                }
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

        public IEnumerable<T> Query(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Remove(T t)
        {
            File.Delete(String.Concat(FullPath, t.ID, ".txt"));
        }


        public void Save(T entity)
        {
            Add(entity);
        }


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

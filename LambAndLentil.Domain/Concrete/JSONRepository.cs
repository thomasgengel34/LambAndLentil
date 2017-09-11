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
        // T should be an incoming entity. It cannot be a view model.  LambAndLentil.Domain only deals with Domain, does not have a dependency on UI and cannot,should not, will not. I can get away with this as is because the ViewModels are identical to the Entities. If that changes, this will have to change.


        static string Folder { get; set; }
        protected static string FullPath { get; set; }
        private static string className;

        public JSONRepository()
        {
            char[] charsToTrim = { 'V', 'M' };
            Folder = typeof(T).ToString().Split('.').Last().Split('+').Last().TrimEnd(charsToTrim);
            //className = typeof(T).ToString().TrimEnd(charsToTrim);
            className =Folder;
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

        public void AttachAnIndependentChild<TChild>(int parentID, TChild child)
            where TChild : BaseEntity, IEntity
        {

            char[] charsToTrim = { 'V', 'M' };
            string childName = typeof(TChild).ToString().Split('.').Last().Split('+').Last().TrimEnd(charsToTrim);
            

            T parent = JsonConvert.DeserializeObject<T>(File.ReadAllText(String.Concat(FullPath, parentID, ".txt")));
           
            if (parent != null && child != null)
            {
                if (className== "Ingredient")
                {
                    // cannot attach a child
                    // do nothing
                    // TODO: think about returning an error message. But user should not be given the chance to do this, so, aside from messing with the query string, how is this possible?  Make sure this is a POST request so no one can mess with the query string and get here. 
                }
                if (className == "Recipe")   // can only attach an Ingredient   
                {
                    Recipe recipe = JsonConvert.DeserializeObject<Recipe>(File.ReadAllText(String.Concat(FullPath, parentID, ".txt")));
                    if (childName == "Ingredient")
                    {
                        Ingredient ingredient = child as Ingredient;
                        recipe.Ingredients.Add(ingredient);
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
                        menu.Ingredients.Add(child as Ingredient);
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
                        plan.Menus.Add(child as Menu);
                    }
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
                } 
            }
        }

        public int Count()
        {
            int fileCount = (from file in Directory.EnumerateFiles(FullPath, "*.txt", SearchOption.AllDirectories)
                             select file).Count();

            return fileCount;
        }

        public void DetachAnIndependentChild<TChild>(int parentID, TChild child)
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

using LambAndLentil.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LambAndLentil.Domain.Entities;
using System.Reflection;

namespace LambAndLentil.Domain.Concrete
{
    public class EFRepository : IRepository
    {
        private EFDbContext context = new EFDbContext();

        #region ingredient

        public IQueryable Ingredient => context.Ingredients;
        public IQueryable<Ingredient> Ingredients => context.Ingredients;
        Ingredient dbEntry;
        public int Save(Ingredient ingredient)
        {
            if (ingredient.ID == 0)
            {
                dbEntry = context.Ingredients.Create();

                dbEntry.CreationDate = DateTime.Now;
                dbEntry.ModifiedDate = DateTime.Now;
                context.Ingredients.Add(dbEntry);
            }
            else
            {
                dbEntry = context.Ingredients.Find(ingredient.ID);
            }
            if (dbEntry != null)
            {
                dbEntry.Name = ingredient.Name;
                dbEntry.Description = ingredient.Description;
                dbEntry.Brand = ingredient.Brand;
                dbEntry.CountryOfOrigin = ingredient.CountryOfOrigin;
                dbEntry.CalFromFat = ingredient.CalFromFat;
                dbEntry.ID = ingredient.ID;
                dbEntry.ContainerSizeUnit = ingredient.ContainerSizeUnit;
                dbEntry.Calories = ingredient.Calories;
                dbEntry.ContainerSize = ingredient.ContainerSize;
                dbEntry.ContainerSizeInGrams = ingredient.ContainerSizeInGrams;
                dbEntry.Kosher = ingredient.Kosher;
                dbEntry.Maker = ingredient.Maker;
                dbEntry.Recipes = ingredient.Recipes;
                dbEntry.ServingSize = ingredient.ServingSize;
                dbEntry.ServingSizeUnit = ingredient.ServingSizeUnit;
                dbEntry.ServingsPerContainer = ingredient.ServingsPerContainer;
                dbEntry.ModifiedDate = DateTime.Now;
                dbEntry.Corn = ingredient.Corn;
                dbEntry.DataSource = ingredient.DataSource;
            }

            int numberOfReturns = context.SaveChanges();
            return numberOfReturns;
        }

     

        public void Save<T>( BaseEntity item)
        {
            string entity = typeof(T).ToString().Split('.').Last();
            switch (entity)
            {
                case "Ingredient":
                    Save((Ingredient)item);
                    break;
                case "Recipe":
                    Save((Recipe)item);
                    break;
                case "Menu":
                    Save((Menu)item);
                    break;
                case "Plan":
                    Save((Plan)item);
                    break;
                case "Person":
                    Save((Person)item);
                    break;
                case "ShoppingList":
                    Save((ShoppingList)item);
                    break;
                default:
                    break;
            }
        }

        public void Delete<T>(int ID)
        {
            string entity = typeof(T).ToString().Split('.').Last();
            switch (entity)
            {
                case "Ingredient":
                    DeleteIngredient(ID);
                    break;
                case "Recipe":
                    DeleteRecipe(ID);
                    break;
                case "Menu":
                    DeleteMenu(ID);
                    break;
                case "Plan":
                    DeletePlan(ID);
                    break;
                case "Person":
                    DeletePerson(ID);
                    break;
                case "ShoppingList":
                    DeleteShoppingList(ID);
                    break;
                default:
                    break;
            }
        }

        private void DeleteIngredient(int ID)
        {
            Ingredient dbEntry = context.Ingredients.Find(ID);
            if (dbEntry != null)
            {
                context.Ingredients.Remove(dbEntry);
                context.SaveChanges();
            }
        }

        #endregion
        ///////////////////////////////
        #region Recipe


        public IQueryable<Recipe> Recipes => context.Recipes;


        public IQueryable Recipe => context.Recipes;


        public int Save(Recipe recipe)
        {
            if (recipe.ID == 0)
            {
                context.Recipes.Add(recipe);
            }
            else
            {
                Recipe dbEntry = context.Recipes.Find(recipe.ID);
                {
                    dbEntry.Calories = recipe.Calories == null ? null : recipe.Calories;
                    dbEntry.CalsFromFat = recipe.CalsFromFat == null ? null : recipe.CalsFromFat;
                    dbEntry.Description = recipe.Description;
                    dbEntry.MealType = recipe.MealType;
                    dbEntry.Name = recipe.Name;
                    dbEntry.ID = recipe.ID;
                    //   dbEntry.RecipeIngredients = recipe.RecipeIngredients;
                    dbEntry.Servings = recipe.Servings;
                }
            }
           int x =context.SaveChanges();
            return x;
        }


        private void DeleteRecipe(int ID)
        {
            Recipe dbEntry = context.Recipes.Find(ID);
            if (dbEntry != null)
            {
                context.Recipes.Remove(dbEntry);
                context.SaveChanges();
            }
        }

        #endregion
        /////////////////////////////
        #region Menu 


        public IQueryable<Menu> Menus => context.Menus;

        public IQueryable Menu => context.Menus;


        public int Save(Menu menu)
        {
            if (menu.ID == 0)
            {
                context.Menus.Add(menu);
            }
            else
            {
                Menu dbEntry = context.Menus.Find(menu.ID);
                {
                    dbEntry.Diners = menu.Diners;
                    dbEntry.MealType = menu.MealType;
                    dbEntry.ID = menu.ID;
                    dbEntry.Name = menu.Name;
                }
            }
           return context.SaveChanges();
        }


        private void DeleteMenu(int ID)
        {
            Menu dbEntry = context.Menus.Find(ID);
            if (dbEntry != null)
            {
                context.Menus.Remove(dbEntry);
                context.SaveChanges();
            }
        }
        #endregion
        /////////////////////////////
        #region Plan


        public IQueryable<Plan> Plans => context.Plans;

        public IQueryable Plan => context.Plans;


        public int Save(Plan plan)
        {
            if (plan.ID == 0)
            {
                context.Plans.Add(plan);
            }
            else
            {
                Plan dbEntry = context.Plans.Find(plan.ID);
                {
                    dbEntry.Menus = plan.Menus;
                    dbEntry.Name = plan.Name;
                    dbEntry.ID = plan.ID;
                }
            }
            return context.SaveChanges();
        }


        private void DeletePlan(int ID)
        {
            Plan dbEntry = context.Plans.Find(ID);
            if (dbEntry != null)
            {
                context.Plans.Remove(dbEntry);
                context.SaveChanges();
            }
        }

        #endregion
        /////////////////////////////
        #region ShoppingLists
        public IQueryable<ShoppingList> ShoppingLists => context.ShoppingLists;

        public IQueryable ShoppingList => context.ShoppingLists;

        public int Save(ShoppingList shoppingList)
        {
            if (shoppingList.ID == 0)
            {
                context.ShoppingLists.Add(shoppingList);
            }
            else
            {
                ShoppingList dbEntry = context.ShoppingLists.Find(shoppingList.ID);
                {
                    dbEntry.Date = shoppingList.Date;
                    dbEntry.Name = shoppingList.Name;
                    dbEntry.ID = shoppingList.ID;
                }
            }
            return context.SaveChanges();
        }


        private void DeleteShoppingList(int ID)
        {
            ShoppingList dbEntry = context.ShoppingLists.Find(ID);
            if (dbEntry != null)
            {
                context.ShoppingLists.Remove(dbEntry);
                context.SaveChanges();
            }

        }


        #endregion
        /////////////////////////////
        #region Person
        public IQueryable<Person> Persons => context.Persons;
        public IQueryable Person => context.Persons;

        public int Save(Person person)
        {
            if (person.ID == 0)
            {
                context.Persons.Add(person);
            }
            else
            {
                Person dbEntry = context.Persons.Find(person.ID);
                {
                    dbEntry.FirstName = person.FirstName;
                    dbEntry.LastName = person.LastName;
                    dbEntry.MaxCalories = person.MaxCalories;
                    dbEntry.MinCalories = person.MinCalories;
                    dbEntry.MustHaveGarlic = person.MustHaveGarlic;
                    dbEntry.NoGarlic = person.NoGarlic;
                    dbEntry.Weight = person.Weight;
                    dbEntry.ID = person.ID;
                }
            }
            return context.SaveChanges();
        }


        private void DeletePerson(int ID)
        {
            Person dbEntry = context.Persons.Find(ID);
            if (dbEntry != null)
            {
                context.Persons.Remove(dbEntry);
                context.SaveChanges();
            }
        }




        #endregion
    }

    //public class EFRepository<T> : IRepository<T>
    //{
    //    private EFDbContext context = new EFDbContext();

    //    //    public IQueryable<T> item => throw new NotImplementedException();

    //    public IQueryable<T> items => throw new NotImplementedException();

    //    IQueryable IRepository<T>.item => throw new NotImplementedException();



    //    public void Delete(int ID)
    //    {
    //        throw new NotImplementedException();
    //    }

    //  public string Save(T t)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}

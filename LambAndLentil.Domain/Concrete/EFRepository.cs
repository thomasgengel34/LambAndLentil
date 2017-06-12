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

        public int Save(Ingredient ingredient)
        {
            Ingredient dbEntry;
            if (ingredient.ID == 0)
            {
                dbEntry = context.Ingredients.Create();
                context.Ingredients.Add(dbEntry);
            }
            else
            {
                dbEntry = context.Ingredients.Find(ingredient.ID);
            }
            dbEntry.AddedByUser = ingredient.AddedByUser;
            dbEntry.Brand = ingredient.Brand;
            dbEntry.Caffeine = ingredient.Caffeine;
            dbEntry.Calcium = ingredient.Calcium;
            dbEntry.CalFromFat = ingredient.CalFromFat;
            dbEntry.Calories = ingredient.Calories;
            dbEntry.ContainerSize = ingredient.ContainerSize;
            dbEntry.ContainerSizeInGrams = ingredient.ContainerSizeInGrams;
            dbEntry.ContainerSizeUnit = ingredient.ContainerSizeUnit;
            dbEntry.Category = ingredient.Category;
            dbEntry.Cholesterol = ingredient.Cholesterol;
            dbEntry.Corn = ingredient.Corn;
            dbEntry.CountryOfOrigin = ingredient.CountryOfOrigin;
            dbEntry.CreationDate = ingredient.CreationDate;
            dbEntry.DataSource = ingredient.DataSource;
            dbEntry.Description = ingredient.Description;
            dbEntry.DietaryFiber = ingredient.DietaryFiber;
            dbEntry.Egg = ingredient.Egg;
            dbEntry.Fish = ingredient.Fish;
            dbEntry.FolicAcid = ingredient.FolicAcid;
            dbEntry.FoodGroup = ingredient.FoodGroup;
            dbEntry.Garlic = ingredient.Garlic;
            dbEntry.ID = ingredient.ID;
            dbEntry.IngredientsList = ingredient.IngredientsList;
            dbEntry.Iron = ingredient.Iron;
            dbEntry.IsGMO = ingredient.IsGMO;
            dbEntry.Kosher = ingredient.Kosher;
            dbEntry.Maker = ingredient.Maker;
            dbEntry.Milk = ingredient.Milk;
            dbEntry.ModifiedDate = DateTime.Now;
            dbEntry.ModifiedByUser = ingredient.ModifiedByUser;
            dbEntry.MonoUnSaturatedFat = ingredient.MonoUnSaturatedFat;
            dbEntry.Name = ingredient.Name;
            dbEntry.Nuts = ingredient.Nuts;
            dbEntry.Onion = ingredient.Onion;
            dbEntry.PolyUnSaturatedFat = ingredient.PolyUnSaturatedFat;
            dbEntry.Potassium = ingredient.Potassium;
            dbEntry.Protein = ingredient.Protein;
            dbEntry.Recipes = ingredient.Recipes;
            dbEntry.SaturatedFat = ingredient.SaturatedFat;
            dbEntry.ServingSize = ingredient.ServingSize;
            dbEntry.ServingSizeUnit = ingredient.ServingSizeUnit;
            dbEntry.ServingsPerContainer = ingredient.ServingsPerContainer;
            dbEntry.Sodium = ingredient.Sodium;
            dbEntry.SodiumNitrite = ingredient.SodiumNitrite;
            dbEntry.Soy = ingredient.Soy;
            dbEntry.StorageType = ingredient.StorageType;
            dbEntry.Sugars = ingredient.Sugars;
            dbEntry.TotalCarbohydrates = ingredient.TotalCarbohydrates;
            dbEntry.TotalFat = ingredient.TotalFat;
            dbEntry.TransFat = ingredient.TransFat;
            dbEntry.UPC = ingredient.UPC;
            dbEntry.VitaminA = ingredient.VitaminA;
            dbEntry.VitaminC = ingredient.VitaminC;
            dbEntry.Wheat = ingredient.Wheat;

            int numberOfReturns = context.SaveChanges();
            return numberOfReturns;
        }



        public void Save<T>(BaseEntity item)
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
            Recipe dbEntry;
            if (recipe.ID == 0)
            {
                dbEntry = context.Recipes.Create();
                context.Recipes.Add(dbEntry);
            }
            else
            {
                dbEntry = context.Recipes.Find(recipe.ID);
            }
            dbEntry.Calories = recipe.Calories == null ? null : recipe.Calories;
            dbEntry.CalsFromFat = recipe.CalsFromFat == null ? null : recipe.CalsFromFat;
            dbEntry.CreationDate = recipe.CreationDate;
            dbEntry.Description = recipe.Description;
            dbEntry.MealType = recipe.MealType;
            dbEntry.ModifiedDate = DateTime.Now;
            dbEntry.Name = recipe.Name;
            dbEntry.ID = recipe.ID;
            //   dbEntry.RecipeIngredients = recipe.RecipeIngredients;
            dbEntry.Servings = recipe.Servings;

            int x = context.SaveChanges();
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
            Menu dbEntry;
            if (menu.ID == 0)
            {
                dbEntry = context.Menus.Create();
                context.Menus.Add(menu);
            }
            else
            {
                dbEntry = context.Menus.Find(menu.ID);
            }
            dbEntry.AddedByUser = menu.AddedByUser;
            dbEntry.CreationDate = menu.CreationDate;
            dbEntry.Description = menu.Description;
            dbEntry.Diners = menu.Diners;
            dbEntry.DayOfWeek = menu.DayOfWeek;
            dbEntry.ID = menu.ID;
            dbEntry.MealType = menu.MealType;
            dbEntry.ModifiedDate = DateTime.Now;
            dbEntry.ModifiedByUser = menu.ModifiedByUser;
            dbEntry.Name = menu.Name;
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
            Plan dbEntry;
            if (plan.ID == 0)
            {
                dbEntry = context.Plans.Create();
                dbEntry = context.Plans.Add(plan);
            }
            else
            {
                dbEntry = context.Plans.Find(plan.ID);
            }
            dbEntry.CreationDate = plan.CreationDate;
            dbEntry.Menus = plan.Menus;
            dbEntry.Name = plan.Name;
            dbEntry.ID = plan.ID;
            dbEntry.Description = plan.Description;
            dbEntry.CreationDate = plan.CreationDate;
            plan.ModifiedDate = DateTime.Now;
            dbEntry.ModifiedByUser = plan.ModifiedByUser;
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
            ShoppingList dbEntry;
            if (shoppingList.ID == 0)
            {
                dbEntry = context.ShoppingLists.Add(shoppingList);
                dbEntry = context.ShoppingLists.Add(shoppingList);
            }
            else
            {
                dbEntry = context.ShoppingLists.Find(shoppingList.ID);
            }
            dbEntry.ID = shoppingList.ID;
            dbEntry.Name = shoppingList.Name;
            dbEntry.Description = shoppingList.Description;
            dbEntry.CreationDate = shoppingList.CreationDate;
            dbEntry.ModifiedByUser = shoppingList.ModifiedByUser;
            dbEntry.ModifiedDate = DateTime.Now;
            dbEntry.Date = shoppingList.Date;
            dbEntry.Author = shoppingList.Author;
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
            Person dbEntry;
            if (person.ID == 0)
            {
                dbEntry = context.Persons.Add(person);
                dbEntry.ModifiedDate = DateTime.Now;

                context.Persons.Add(person);
            }
            else
            {
                dbEntry = context.Persons.Find(person.ID);
            }
            dbEntry.ID = person.ID;
            dbEntry.Description = person.Description;
            dbEntry.CreationDate = person.CreationDate;
            dbEntry.ModifiedByUser = person.ModifiedByUser;
            dbEntry.ModifiedDate = DateTime.Now;
            dbEntry.FirstName = person.FirstName;
            dbEntry.LastName = person.LastName;
            dbEntry.MaxCalories = person.MaxCalories;
            dbEntry.MinCalories = person.MinCalories;
            dbEntry.Name = String.Concat(person.FirstName, " ", person.LastName);
            dbEntry.NoGarlic = person.NoGarlic;
            dbEntry.Weight = person.Weight;
            dbEntry.ID = person.ID; 
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

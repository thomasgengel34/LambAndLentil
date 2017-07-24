namespace LambAndLentil.Domain.Migrations
{
    using LambAndLentil.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LambAndLentil.Domain.Concrete.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled =true;
        }

        protected override void Seed(LambAndLentil.Domain.Concrete.EFDbContext context)
        { 
            context.Ingredient.AddOrUpdate(
              p => p.Name,
             new Ingredient(DateTime.Now){ Name = "Romulan Ale" , Description="Illegal in the Federation",IngredientsList="Earth, air, fire, water"} ,
             new Ingredient(DateTime.Now) { Name = "Klingon Fire Water", Description = "Do not use near open flame", IngredientsList = "gak,gasoline, coffee"  } ,
              new Ingredient(DateTime.Now) { Name = "Vulcan Veal", Description = "Volcano, Ltd.", IngredientsList = "sheep, potassium sorbate, salt" },
              new Ingredient(DateTime.Now) { Name = "Apple Pie", Description = "Newton Apples, Ltd.", IngredientsList = "enriched wheat flour, baking soda, apples, sugar" },
              new Ingredient(DateTime.Now) { Name = "Ground Beef", Description = "Blue Ox Corp.",IngredientsList ="ground beef" },
              new Ingredient(DateTime.Now) { Name = "Sea Salt", Description = "Popeye LLC", IngredientsList = "sea salt"},
              new Ingredient(DateTime.Now) { Name = "Cheddar cheese", Description = "Cheese Heads, Ltd."  },
              new Ingredient(DateTime.Now) { Name = "Red Cabbage", Description = "Rot & Kraut",IngredientsList = "milk, enzymes" },
                new Ingredient(DateTime.Now) { Name = "Chilton cheese", Description = "Cheese Heads, Ltd.", IngredientsList="milk, enzymes" },
              new Ingredient(DateTime.Now) { Name = "Blue Cabbage", Description = "Rot & Kraut",IngredientsList ="red cabbage"  },
              new Ingredient(DateTime.Now) { Name = "Great Value Chopped Green Chili Peppers - FRPC", Description = "Great Value Chopped Green Chili Peppers - FRPC",IngredientsList ="chili peppers"  }
            );

            context.Recipe.AddOrUpdate(
                r => r.Name,
                new Recipe { Name = "Vulcan Cutlets", Description = "Delicious and delicate dish from the Southern hemisphere, traditionally served boiled", Calories = 100,
                    Ingredients = new List<Ingredient>() {
                        context.Ingredient.Where(m=>m.Name=="Ground Beef").Single(),
                        context.Ingredient.Where(m=>m.Name=="Sea Salt").Single(),
                        context.Ingredient.Where(m=>m.Name=="Chilton Cheese").Single(),
                        context.Ingredient.Where(m=>m.Name=="Romulan Ale").Single(),
                    }
                },
                new Recipe { Name = "Flaming Romulan Punch", Description = "just what it says.", Calories = 100 },
                new Recipe { Name = "Cheese fondue", Description = "Requires fondue heater.", Calories = 100 },
                new Recipe { Name = "Rotkraut", Description = "Traditional German dish", Calories = 100 },
                new Recipe { Name = "Stuffed Rabbit", Description = "Delicious and delicate dish from the South", Calories = 100 },
                new Recipe { Name = "Macaroni and Cheese", Description = "Easy meal", Calories = 100 }
                );

            context.Menu.AddOrUpdate(
                m => m.Name,
                new Menu { Name = "Big Breakfast", Diners = 1, MealType = MealType.Breakfast },
                new Menu { Name = "Big Lunch", Diners = 3, MealType = MealType.Lunch },
                new Menu { Name = "Easy Dinner", Diners = 4, MealType = MealType.Dinner },
                new Menu { Name = "Thanksgiving 2017", Diners = 12, MealType = MealType.Feast },
                new Menu { Name = "Little Breakfast", Diners = 1, MealType = MealType.Breakfast },
                new Menu { Name = "Midmorning Snack", Diners = 1, MealType = MealType.Snack }
                );

            context.Plan.AddOrUpdate(
                new Plan { Name = "First Try", Description = "Failure is Not an Option" },
                new Plan { Name = "March 6", Description = "Well, maybe it is" },
                new Plan { Name = "March 13", Description = "Not for the faint of heart" },
                new Plan { Name = "March 20", Description = "Usually we get it right by the hundreth try" },
                new Plan { Name = "March 27", Description = "Or the thousandth" },
                new Plan { Name = "Vacation", Description = "Maybe" }
                );

            context.Person.AddOrUpdate(
             p => p.LastName,
             new Person
             {
                 Name = "Jonathan Seagull",
                 FirstName = "Jonathan",
                 LastName = "Seagull",
                 Weight = 10
             },
             new Person { Name = "Harold King", FirstName = "Harold", LastName = "King" },
             new Person { Name = "John Doe", FirstName = "John", LastName = "Doe" },
             new Person { Name = "Mary Roe", FirstName = "Mary", LastName = "Roe" },
             new Person { Name = "Fred Stone", FirstName = "Fred", LastName = "Stone" },
             new Person { Name = "Alice Restaurant", FirstName = "Alice", LastName = "Restaurant" },
             new Person { Name = "James Kirk", FirstName = "James", LastName = "Kirk" }
             );

            context.ShoppingList.AddOrUpdate(
                s => s.Name,
                new ShoppingList { Name = "3/4/17", Date=DateTime.Now },
                new ShoppingList { Name = "3/11/17", Date = DateTime.Now },
                new ShoppingList { Name = "3/18/17", Date = DateTime.Now },
                new ShoppingList { Name = "3/25/17", Date = DateTime.Now },
                new ShoppingList { Name = "Thanksgiving", Date = DateTime.Now }
                );
        }
    }
}

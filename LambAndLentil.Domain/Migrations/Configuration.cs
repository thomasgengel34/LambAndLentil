namespace LambAndLentil.Domain.Migrations
{
    using LambAndLentil.Domain.Entities;
    using System;
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
            context.Ingredients.AddOrUpdate(
              p => p.Name,
             new Ingredient(DateTime.Now){ Name = "Romulan Ale", Maker = "Romulan Ales, Ltd.", Brand = "Baggins Enterprises", ServingSizeUnit = Measurement.FluidOz, ServingSize = 8, Calories = 64, Kosher = Kosher.Unknown,ContainerSizeUnit=ContainerSizeUnit.Cup } ,
             new Ingredient(DateTime.Now) { Name = "Klingon Fire Water", Maker = "Spock, Ltd.", ServingSizeUnit = Measurement.FluidOz, ServingSize = 8, Calories = 64, Kosher = Kosher.OK, ContainerSizeUnit = ContainerSizeUnit.unknown, } ,
              new Ingredient(DateTime.Now) { Name = "Vulcan Veal", Maker = "Volcano, Ltd.", ServingSizeUnit = Measurement.Pound, ServingSize = 0.25m, Calories = 640, Kosher = Kosher.KofK, ContainerSizeUnit = ContainerSizeUnit.unknown },
              new Ingredient(DateTime.Now) { Name = "Apple Pie", Maker = "Newton Apples, Ltd.", ServingSizeUnit = Measurement.Each, ServingSize = 1, Calories = 100, Kosher = Kosher.Unknown, ContainerSizeUnit = ContainerSizeUnit.unknown },
              new Ingredient(DateTime.Now) { Name = "Ground Beef", Maker = "Blue Ox Corp.", ServingSizeUnit = Measurement.Pound, ServingSize = 1, Calories = 64, Kosher = Kosher.Unknown, ContainerSizeUnit = ContainerSizeUnit.unknown },
              new Ingredient(DateTime.Now) { Name = "Sea Salt", Maker = "Popeye LLC", ServingSizeUnit = Measurement.Tablespoon, ServingSize = 1, Calories = 64, Kosher = Kosher.Not, ContainerSizeUnit = ContainerSizeUnit.unknown },
              new Ingredient(DateTime.Now) { Name = "Cheddar cheese", Maker = "Cheese Heads, Ltd.", ServingSizeUnit = Measurement.Slice, ServingSize = 2, Calories = 64, Kosher = Kosher.Not, ContainerSizeUnit = ContainerSizeUnit.unknown },
              new Ingredient(DateTime.Now) { Name = "Red Cabbage", Maker = "Rot & Kraut", ServingSizeUnit = Measurement.Cup, ServingSize = 1, Calories = 64, Kosher = Kosher.Not, ContainerSizeUnit = ContainerSizeUnit.unknown },
                new Ingredient(DateTime.Now) { Name = "Chilton cheese", Maker = "Cheese Heads, Ltd.", ServingSizeUnit = Measurement.Slice, ServingSize = 1, Calories = 640, Kosher = Kosher.Not, ContainerSizeUnit = ContainerSizeUnit.unknown },
              new Ingredient(DateTime.Now) { Name = "Blue Cabbage", Maker = "Rot & Kraut", ServingSizeUnit = Measurement.Cup, ServingSize = 1, Calories = 128, Kosher = Kosher.Not, ContainerSizeUnit = ContainerSizeUnit.unknown },
              new Ingredient(DateTime.Now) { Name = "Great Value Chopped Green Chili Peppers - FRPC", Description = "Great Value Chopped Green Chili Peppers - FRPC", ServingSize = 2, ServingSizeUnit = Measurement.Tablespoon, ContainerSize = 4, ContainerSizeUnit = ContainerSizeUnit.OZ, ContainerSizeInGrams = 113, Calories = 5, CalFromFat = 5, TotalFat = 0, Maker = "Wal Mart Stores, Inc.", UPC = "078742433943", IngredientsList = "Green chile pappers, water, contains less than 2% of calcium chloride, citric acid, salt. Allergy warning: may contain traces of wheat and soy.", Brand = "Great Value", Kosher = Kosher.Not }
            );

            //context.Recipes.AddOrUpdate(
            //    r => r.Name,
            //    new Recipe { Name = "Vulcan Cutlets", Description = "Delicious and delicate dish from the Southern hemisphere, traditionally served boiled", Calories = 100 },
            //    new Recipe { Name = "Flaming Romulan Punch", Description = "just what it says.", Calories = 100 },
            //    new Recipe { Name = "Cheese fondue", Description = "Requires fondue heater.", Calories = 100 },
            //    new Recipe { Name = "Rotkraut", Description = "Traditional German dish", Calories = 100 },
            //    new Recipe { Name = "Stuffed Rabbit", Description = "Delicious and delicate dish from the South", Calories = 100 },
            //    new Recipe { Name = "Macaroni and Cheese", Description = "Easy meal", Calories = 100 }
            //    );

            //context.Menus.AddOrUpdate(
            //    m => m.Name,
            //    new Menu { Name = "Big Breakfast", Diners = 1, MealType = MealType.Breakfast },
            //    new Menu { Name = "Big Lunch", Diners = 3, MealType = MealType.Lunch },
            //    new Menu { Name = "Easy Dinner", Diners = 4, MealType = MealType.Dinner },
            //    new Menu { Name = "Thanksgiving 2017", Diners = 12, MealType = MealType.Feast },
            //    new Menu { Name = "Little Breakfast", Diners = 1, MealType = MealType.Breakfast },
            //    new Menu { Name = "Midmorning Snack", Diners = 1, MealType = MealType.Snack }
            //    );

            //context.Plans.AddOrUpdate(
            //    new Plan { Name = "First Try", Description="Failure is Not an Option" },
            //    new Plan { Name = "March 6", Description="Well, maybe it is" },
            //    new Plan { Name = "March 13",Description="Not for the faint of heart" },
            //    new Plan { Name = "March 20", Description = "Usually we get it right by the hundreth try" },
            //    new Plan { Name = "March 27",Description ="Or the thousandth" },
            //    new Plan { Name = "Vacation", Description ="Maybe" }
            //    );

            //context.Persons.AddOrUpdate(
            // p => p.LastName,
            // new Person {
            //     Name = "Jonathan Seagull", FirstName = "Jonathan", LastName = "Seagull", Weight = 10 },
            // new Person { Name = "Harold King",  FirstName = "Harold", LastName = "King" },
            // new Person { Name = "John Doe",  FirstName = "John", LastName = "Doe" },
            // new Person { Name = "Mary Roe",  FirstName = "Mary", LastName = "Roe" },
            // new Person { Name = "Fred Stone",  FirstName = "Fred", LastName = "Stone" },
            // new Person { Name = "Alice Restaurant",  FirstName = "Alice", LastName = "Restaurant" },
            // new Person { Name = "James Kirk",  FirstName = "James", LastName = "Kirk" }
            // );

            //context.ShoppingLists.AddOrUpdate(
            //    s => s.Name,
            //    new ShoppingList { Name = "3/4/17" },
            //    new ShoppingList { Name = "3/11/17" },
            //    new ShoppingList { Name = "3/18/17" },
            //    new ShoppingList { Name = "3/25/17" },
            //    new ShoppingList { Name = "Thanksgiving" }
            //    );
        }
    }
}

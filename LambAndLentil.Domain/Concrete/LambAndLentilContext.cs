using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using LambAndLentil.Domain.Entities; 

namespace LambAndLentil.Domain.Concrete
{
    public class LambAndLentilContext : DbContext,ILambAndLentilContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public LambAndLentilContext() : base("name=LambAndLentilContext")
        {
        }

        public DbSet<Ingredient> Ingredients { get; set; }  
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Menu> Menus { get; set; }

        int ILambAndLentilContext.SaveChanges()  
        {
            return  SaveChanges();
        }

        DbEntityEntry ILambAndLentilContext.Entry(Ingredient existingIngredient)
        {
            return  Entry(existingIngredient);
        }

        DbEntityEntry ILambAndLentilContext.Entry(Recipe existingRecipe)
        {
            return Entry(existingRecipe);
        }

        public System.Data.Entity.DbSet< Plan> Plans { get; set; }

        public System.Data.Entity.DbSet<LambAndLentil.Domain.Entities.ShoppingList> ShoppingLists { get; set; }

        public System.Data.Entity.DbSet<LambAndLentil.Domain.Entities.Person> People { get; set; }
    }
}

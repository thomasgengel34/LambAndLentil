using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace LambAndLentil.Domain.Concrete
{
    public class EFDbContext : DbContext //, ILambAndLentilContext
    {
        public EFDbContext() : base("name=EFDbContext")
        {

        }

       

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }

       

        public DbEntityEntry  Entry(Ingredient existingIngredient)
        {
            return Entry(existingIngredient);
        }

        public DbEntityEntry  Entry(Recipe existingRecipe)
        {
            return Entry(existingRecipe);
        }  
    } 
}


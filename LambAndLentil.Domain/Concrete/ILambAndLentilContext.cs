using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Domain.Concrete
{
    public interface ILambAndLentilContext
    {
        DbSet<Ingredient> Ingredients { get; set; }
        DbSet<Recipe> Recipes { get; set; }
        DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        DbEntityEntry  Entry(Ingredient existingIngredient);

        DbEntityEntry  Entry(Recipe existingRecipe);

        //DbEntityEntry<Ingredient> Entry(Ingredient existingIngredient);

        //DbEntityEntry<Recipe> Entry(Recipe existingRecipe);

        string SaveChanges();

        void Dispose();
    }
     

}
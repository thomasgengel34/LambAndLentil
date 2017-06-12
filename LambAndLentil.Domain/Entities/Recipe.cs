using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LambAndLentil.Domain.Entities
{

    [Table("RECIPE.Recipe")]
    public class Recipe:BaseEntity
    {
        public Recipe() : base()
        {
            //RecipeIngredients = new List<RecipeIngredient>();
            
        }

        public Recipe(DateTime creationDate) : this()
        {
            CreationDate = creationDate;
        }

        // need to test this, just written, never used. Not sure it will work. 
        //public Recipe(Recipe recipe)
        //{
        // //   RecipeIngredients = new List<RecipeIngredient>();
        //   // Calories = GetCalories(recipe);
        //}

        public decimal Servings { get; set; }  
        public MealType MealType { get; set; } 
        public int? Calories { get; set; }  
        public short? CalsFromFat { get; set; }

      //  public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }


        //public static short GetCalories(Recipe recipe)
        //{
        //    short? calories = 0;

        //    if (recipe.RecipeIngredients == null || recipe.RecipeIngredients.Count == 0)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        foreach (RecipeIngredient item in recipe.RecipeIngredients)
        //        {
        //            if (item.Ingredient != null)
        //            {
        //                calories += item.Ingredient.Calories;  // this really needs to be adjusted by quantity and unit of measure
        //            } 
        //        }

        //        if (calories == null)
        //        {
        //            calories = 0;
        //        }
        //        return (short)calories;
        //    }
        //}
    }
}
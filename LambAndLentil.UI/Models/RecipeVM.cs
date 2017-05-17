using LambAndLentil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LambAndLentil.UI.Models
{


    public class RecipeVM : BaseVM
    {
        public RecipeVM()
        {
            //  RecipeIngredients = new List<RecipeIngredient>();

        }
        // need to test this, just written, never used. Not sure it will work. 
        public RecipeVM(RecipeVM recipeVM)
        {
            //  RecipeIngredients = new List<RecipeIngredient>();
            Calories = GetCalories(recipeVM);
        }
        public string Description { get; set; }
        public decimal Servings { get; set; }

        [Display(Name = "Meal Type")]
        public MealType MealType { get; set; }
        public int? Calories { get; set; }
        [Display(Name = "Calories From Fat")]
        public short? CalsFromFat { get; set; }

        //public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }


        public static short GetCalories(RecipeVM recipeVM)
        {
            short? calories = 0;

            //if (recipeVM.RecipeIngredients == null || recipeVM.RecipeIngredients.Count == 0)
            //{
            //    return 0;
            //}
            //else
            //{
            //    foreach (RecipeIngredient item in recipeVM.RecipeIngredients)
            //    {
            //        if (item.Ingredient != null)
            //        {
            //            calories += item.Ingredient.Calories;  // this really needs to be adjusted by quantity and unit of measure
            //        } 
            //    }

            //    if (calories == null)
            //    {
            //        calories = 0;
            //    }
            return (short)calories;
        }
    }
}
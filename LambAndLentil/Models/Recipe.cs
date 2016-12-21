using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LambAndLentil.Models
{
    
        [Table("RECIPE.Recipe")]
        public class Recipe
        {
            public Recipe()
            {
                Ingredients = new List<Ingredient>();
            }
            public int RecipeId { get; set; }


            [DataType(DataType.Text)]
            public string Name { get; set; }

            public string Description { get; set; }

         
            public decimal  Servings { get; set; }
            public FoodGroup FoodGroup { get; set; }

            public int Calories { get; set; }
            public virtual List<Ingredient> Ingredients { get; set; }
        }
    } 
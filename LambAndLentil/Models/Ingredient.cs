using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LambAndLentil.Models
{
    public class Ingredient
    {
        public int IngredientID { get; set; }

        [StringLength(50)]
        public string ShortDescription { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ServingSize { get; set; }

        public int ServingSizeUnit { get; set; }

        public short? Calories { get; set; }

        public short? CalFromFat { get; set; }
    }
}
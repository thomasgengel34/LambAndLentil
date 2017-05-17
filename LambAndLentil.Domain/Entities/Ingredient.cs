using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LambAndLentil.Domain.Entities
{
    [Table("INGREDIENT.Ingredient")]
    public class Ingredient:BaseEntity
    {
        public Ingredient()
        {
            Maker = "Not Provided";
            Brand = "Not Provided";
            FoodGroup = "Default";
            Category = "Default"; 
        }
 
         
        public string Maker { get; set; }
        public string Brand { get; set; }  
        public string  Description { get; set; }
        public decimal? ServingSize { get; set; } 
        public Measurement ServingSizeUnit { get; set; } 
        public decimal? ServingsPerContainer { get; set; } 
        public decimal? ContainerSize { get; set; } 
        public ContainerSizeUnit? ContainerSizeUnit { get; set; } 
        public decimal? ContainerSizeInGrams { get; set; } 
        public short? Calories { get; set; } 
        public short? CalFromFat { get; set; } 
        public decimal TotalFat { get; set; }
        public decimal SaturatedFat { get; set; }

        public decimal TransFat { get; set; }
        public decimal PolyUnSaturatedFat { get; set; }
        public decimal MonoUnSaturatedFat { get; set; }
        public decimal Cholesterol { get; set; }
        public decimal Sodium{ get; set; }
        public decimal TotalCarbohydrates { get; set; }
        public decimal Protein { get; set; }
        public decimal Potassium { get; set; }
        public decimal DietaryFiber{ get; set; }
        public decimal Sugars { get; set; }
        public int VitaminA { get; set; }
        public int VitaminC { get; set; }
        public int Calcium { get; set; }
        public int Iron { get; set; }
        public int FolicAcid { get; set; }
        public string Egg  { get; set; }
        public string Nuts  { get; set; }
        public string Milk  { get; set; }
        public string Wheat { get; set; }
        public string Soy  { get; set; }
        public string Category  { get; set; }
        public string Corn  { get; set; }
        public string Onion  { get; set; }
        public string Garlic  { get; set; }
        public string SodiumNitrite  { get; set; }
        public string UPC  { get; set; }
        public string Caffeine { get; set; } 
        public string FoodGroup { get; set; }
        public string StorageType { get; set; }
        public string IngredientsList { get; set; }
        
        public string IsGMO { get; set; }
        public string CountryOfOrigin { get; set; } 
        public bool? Kosher { get; set; }
        public string DataSource { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}
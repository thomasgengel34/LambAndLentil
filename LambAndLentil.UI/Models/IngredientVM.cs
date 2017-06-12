using LambAndLentil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LambAndLentil.UI.Models
{
    public class IngredientVM: BaseVM,IBaseVM
    {
        public IngredientVM():base()
        {
            Brand = "not given";
            Category = "not given";
            FoodGroup = "not given";
            Maker = "not given"; 
            Kosher = Kosher.Unknown;
            ContainerSizeUnit = ContainerSizeUnit.Cup;
        }

        public IngredientVM(DateTime dateTime) : this()
        {
            CreationDate = dateTime;
        }

        [StringLength(50)]
            [Required] 
        public string Maker { get; set; }

        [StringLength(50)]
        [Required]
        public string Brand { get; set; }

        
        [Column(TypeName = "numeric")]
        [Range(0.01 , 100,ErrorMessage ="Please enter a postive value")] 
        public decimal? ServingSize { get; set; } 
  
        public Measurement ServingSizeUnit { get; set; }

        [Range(0.01, 100, ErrorMessage = "Please enter a postive value")]
        public decimal? ServingsPerContainer { get; set; }

        [Range(0.01, 100, ErrorMessage = "Please enter a postive value")]
        public decimal? ContainerSize { get; set; }

        [Range(0.01, 100, ErrorMessage = "Please enter a postive value")]
       
        public ContainerSizeUnit  ContainerSizeUnit { get; set; }

        [Range(0.01, 100, ErrorMessage = "Please enter a postive value")]
        public decimal? ContainerSizeInGrams { get; set; }

 
        [Range(1, short.MaxValue, ErrorMessage = "Please enter a postive value")]
        public short? Calories { get; set; }

        [Range(1, short.MaxValue, ErrorMessage = "Please enter a postive value")]
        [Display(Name = "Fat Cals")]
        public short? CalFromFat { get; set; }
         
        public Kosher  Kosher { get; set; } 
      
        
        public decimal? TotalFat { get; set; }
        public decimal? SaturatedFat { get; set; }
        public decimal? TransFat { get; set; }
        public decimal? PolyUnSaturatedFat { get; set; }
        public decimal? MonoUnSaturatedFat { get; set; }
        public decimal? Cholesterol { get; set; }
        public decimal? Sodium { get; set; }
        public decimal? TotalCarbohydrates { get; set; }
        public decimal? Protein { get; set; }
        public decimal? Potassium { get; set; }
        public decimal? DietaryFiber { get; set; }
        public decimal? Sugars { get; set; }
        public int?  VitaminA { get; set; }
        public int?  VitaminC { get; set; }
        public int?  Calcium { get; set; }
        public int?  Iron { get; set; }
        public int?  FolicAcid { get; set; }
        public string Egg { get; set; }
        public string Nuts { get; set; }
        public string Milk { get; set; }
        public string Wheat { get; set; }
        public string Soy { get; set; }

        [Required]
        public string Category { get; set; }

        public string Corn { get; set; }
        public string Onion { get; set; }
        public string Garlic { get; set; }
        public string SodiumNitrite { get; set; }
        public string UPC { get; set; }
        public string Caffeine { get; set; }

        [Required]
        public string FoodGroup { get; set; }

        public string StorageType { get; set; }

        [DataType(DataType.MultilineText)]
        public string IngredientsList { get; set; }
      
      
        public string IsGMO { get; set; }
        public string CountryOfOrigin { get; set; }
        public string DataSource { get; set; }
        public string Fish { get; set; }





        //  public IEnumerable<Ingredient> Ingredients { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
      //  public int?  RecipeIngredientID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
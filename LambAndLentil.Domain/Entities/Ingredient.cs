using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LambAndLentil.Domain.Entities
{
    [Table("INGREDIENT.Ingredient")]
    public class Ingredient : BaseEntity
    {
 
        public Ingredient() : base()
        {
            
        }

        public Ingredient(DateTime creationDate) : this()
        {
            CreationDate = creationDate;
        } 
        public string IngredientsList { get; set; } 
    }
}
using System.Collections.Generic;

namespace LambAndLentil.Domain.Entities
{
    public interface IRecipe:IEntity
    {
        int? Calories { get; set; }
        short? CalsFromFat { get; set; } 
        MealType MealType { get; set; }
        decimal Servings { get; set; }
    }
}
using System.Collections.Generic;

namespace LambAndLentil.Domain.Entities
{
    public interface IRecipe
    {
        int? Calories { get; set; }
        short? CalsFromFat { get; set; }
        int ID { get; set; }
        List<Ingredient> Ingredients { get; set; }
        MealType MealType { get; set; }
        decimal Servings { get; set; }
    }
}
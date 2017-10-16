using System;
using System.Collections.Generic;

namespace LambAndLentil.Domain.Entities
{
    public interface IMenu:IEntity
    {
        DayOfWeek DayOfWeek { get; set; }
        int Diners { get; set; } 
        List<Ingredient> Ingredients { get; set; }
        MealType MealType { get; set; }
        List<Recipe> Recipes { get; }
    }
}
using System;
using System.Collections.Generic;

namespace LambAndLentil.Domain.Entities
{
    public interface IMenu:IEntityChildClassIngredients, IEntityChildClassRecipes
    {
        DayOfWeek DayOfWeek { get; set; }
        int Diners { get; set; }  
        MealType MealType { get; set; } 
    }
}
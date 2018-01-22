using System;
using System.Collections.Generic;

namespace LambAndLentil.Domain.Entities
{
    public interface IMenu:IEntity 
    {
        DayOfWeek DayOfWeek { get; set; }
        int Diners { get; set; }  
        MealType MealType { get; set; } 
    }
}
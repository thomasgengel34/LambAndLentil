using LambAndLentil.Domain.Entities;
using System;
using System.Collections.Generic;

namespace LambAndLentil.UI.Models
{
    public class MenuVM:BaseVM,IBaseVM,IMenu
    {
        public MenuVM():base()
        {

        }

        public MenuVM(DateTime creationDate) : this()
        {
            CreationDate = creationDate;
        }

      
        public MealType MealType { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public  int Diners { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public List<Recipe> Recipes   { get; set; }
    }
}

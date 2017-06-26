using LambAndLentil.Domain.Entities;
using System;
using System.Collections.Generic;

namespace LambAndLentil.UI.Models
{
    public class MenuVM:BaseVM,IBaseVM
    {
        public MenuVM():base()
        {

        }

        public MenuVM(DateTime creationDate) : this()
        {
            CreationDate = creationDate;
        }

        public int ID { get; set; }
        public MealType MealType { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public  int Diners { get; set; } 

        ICollection<Recipe> Recipes { get; set; }
    }
}

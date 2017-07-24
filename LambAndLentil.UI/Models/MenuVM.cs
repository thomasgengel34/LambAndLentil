using LambAndLentil.Domain.Entities;
using System;
using System.Collections.Generic;

namespace LambAndLentil.UI.Models
{
    public class MenuVM:BaseVM,IBaseVM,IEntity
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

      
    }
}

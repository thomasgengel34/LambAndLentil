using LambAndLentil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.UI.Models
{
    public class MenuVM:BaseVM
    { 
        public MealType MealType { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public  int Diners { get; set; } 

        ICollection<Recipe> Recipes { get; set; }
    }
}

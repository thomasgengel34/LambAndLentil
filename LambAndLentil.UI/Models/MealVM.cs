using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LambAndLentil.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace LambAndLentil.UI.Models
{
    public class MealVM
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Meal> Meals   { get; set; }
    } 
}

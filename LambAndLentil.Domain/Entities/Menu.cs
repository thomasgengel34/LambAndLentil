using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.Domain.Entities
{
    [Table("MENU.Menu")]
    public class Menu : BaseEntity
    {
        
        public Menu():base() 
        { 
        }

        public Menu(DateTime creationDate) : this()
        {
            CreationDate = creationDate;
        }

        public MealType MealType { get; set; }
        public DayOfWeek DayOfWeek { get; set; }

        public int Diners { get; set; }

        ICollection<Recipe> Recipes { get; set; }

    }
}

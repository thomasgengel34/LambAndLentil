using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LambAndLentil.Domain.Entities
{
    [Table("MENU.Menu")]
    public class Menu : BaseEntity, IEntity
    {

        public Menu() : base()
        {
        }

        public Menu(DateTime creationDate) : base(creationDate)
        {
            CreationDate = creationDate;
        }

        public int ID { get; set; }
        public MealType MealType { get; set; }
        public DayOfWeek DayOfWeek { get; set; } 
        public int Diners { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LambAndLentil.Domain.Entities
{
    [Table("MENU.Menu")]
    public class Menu : BaseEntity, IEntity, IMenu
    {

        public Menu() : base()
        {
            Ingredients = new List<Ingredient>();
            Recipes = new List<Recipe>(); 
        }

        public Menu(DateTime creationDate) : base(creationDate)
        {
            CreationDate = creationDate;
        }

        public int ID { get; set; }
        public MealType MealType { get; set; }
        public DayOfWeek DayOfWeek { get; set; } 
        public int Diners { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Recipe> Recipes { get;   set; }
    }
}

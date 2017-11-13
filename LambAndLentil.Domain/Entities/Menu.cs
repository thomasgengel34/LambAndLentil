using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LambAndLentil.Domain.Entities
{
    [Table("MENU.Menu")]
    public class Menu : BaseEntity, IEntityChildClassIngredients, IEntityChildClassRecipes

    {

        public Menu() : base()
        {
            Ingredients = new List<Ingredient>();
            Recipes = new List<Recipe>(); 
        }

        public Menu(DateTime creationDate) : base(creationDate) => CreationDate = creationDate;

        public int ID { get; set; }
        public MealType MealType { get; set; }
        public DayOfWeek DayOfWeek { get; set; } 
        public int Diners { get; set; }

        private new List<Menu> Menus { get; set; }
        private new List<Plan> Plans { get; set; }
        private new List<ShoppingList> ShoppingLists { get; set; }
        private new List<Person> Persons { get; set; }
    }
}

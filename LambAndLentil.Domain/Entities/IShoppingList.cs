using System;
using System.Collections.Generic;

namespace LambAndLentil.Domain.Entities
{
    public interface IShoppingList:IEntity
    {
        string Author { get; set; }
        DateTime Date { get; set; } 
        ICollection<Ingredient> Ingredients { get; set; }
        ICollection<Menu> Menus { get; set; } 
        ICollection<Plan> Plans { get; set; }
        ICollection<Recipe> Recipes { get; set; }
    }
}
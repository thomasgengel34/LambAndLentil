using System;
using System.Collections.Generic;

namespace LambAndLentil.Domain.Entities
{
    public interface IShoppingList:IEntity
    {
        string Author { get; set; }
        DateTime Date { get; set; } 
        List<Ingredient> Ingredients { get; set; }
        List<Menu> Menus { get; set; } 
        List<Plan> Plans { get; set; }
        List<Recipe> Recipes { get; set; }
    }
}
using System;
using System.Collections.Generic;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.UI.Models
{
    public interface IShoppingListVM
    {
        string Author { get; set; }
        DateTime Date { get; set; }
        ICollection<Ingredient> Ingredients { get; set; }
        ICollection<Menu> Menus { get; set; } 
        ICollection<Plan> Plans { get; set; }
        ICollection<Recipe> Recipes { get; set; }
    }
}
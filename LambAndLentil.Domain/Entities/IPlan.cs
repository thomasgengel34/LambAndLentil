using System.Collections.Generic;

namespace LambAndLentil.Domain.Entities
{
    public interface IPlan
    {
        int ID { get; set; }
        List<Ingredient> Ingredients { get; set; }
        List<Menu> Menus { get; set; }
        List<Recipe> Recipes { get; set; }
    }
}
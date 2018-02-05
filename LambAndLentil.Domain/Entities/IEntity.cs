using System;
using System.Collections.Generic;

namespace LambAndLentil.Domain.Entities
{
    public interface IEntity 
    {
        string AddedByUser { get; set; }
        DateTime CreationDate { get; set; }
        int ID { get; set; }
        string ModifiedByUser { get; set; }
        DateTime ModifiedDate { get; set; }
        string Name { get; set; }
        string Description { get; set; } 
        string IngredientsList { get; set; }

        List<Ingredient> Ingredients { get; set; }
        List<Recipe> Recipes { get; set; }
        List<Menu> Menus { get; set; }
        List<Plan> Plans { get; set; }
        List<ShoppingList> ShoppingLists { get; set; }

        // TODO: think about reducing parameter complexity down to string or Type
        bool CanHaveChild(IEntity child);


        int GetCountOfChildrenOnParent(IEntity parent );
      
    }
}
 
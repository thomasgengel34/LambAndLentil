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

        ICollection<Recipe> Recipes { get; set; }
        ICollection<Ingredient> Ingredients { get; set; }
        ICollection<Menu> Menus { get; set; }
        ICollection<Plan> Plans { get; set; }
        ICollection<ShoppingList> ShoppingLists { get; set; }
        ICollection<Person> Persons { get; set; }
    }
}
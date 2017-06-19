using LambAndLentil.Domain.Entities;
using System.Collections.Generic;

namespace LambAndLentil.UI.Models
{
    public class  ListVM  
    { 
        public IEnumerable<Ingredient> Ingredients { get; set; } 
        public IEnumerable<Recipe> Recipes { get; set; }
        public IEnumerable<Menu> Menus { get; set; }
        public IEnumerable<Person>Persons { get; set; }
        public IEnumerable<Plan> Plans { get; set; }
        public IEnumerable<ShoppingList> ShoppingLists { get; set; }
        public PagingInfo PagingInfo { get; set; }      
    }
}
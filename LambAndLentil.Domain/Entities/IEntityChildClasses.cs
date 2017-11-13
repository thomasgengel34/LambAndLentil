using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.Domain.Entities
{
   public interface IEntityChildClasses:IEntity
    {
         List<Ingredient> Ingredients { get; set; }
         List<Recipe> Recipes { get; set; }
         List<Menu> Menus { get; set; }
         List<Plan> Plans { get; set; }
         List<ShoppingList> ShoppingLists { get; set; }
         List<Person> Persons { get; set; }
    }
}

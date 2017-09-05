using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
 

namespace LambAndLentil.Domain.Entities 
{
    [Table("SHOPPINGLIST.ShoppingList")]
    public class ShoppingList : BaseEntity, IEntity, IShoppingList
    {
        public ShoppingList():base()
        {
            Ingredients = new List<Ingredient>();
            Recipes = new List<Recipe>();
            Menus = new List<Menu>();
            Plans = new List<Plan>(); 
        }

        public ShoppingList(DateTime creationDate) : base(creationDate)
        {
            CreationDate = creationDate;
            Date = creationDate;
        }
         
   
        public   ICollection<Recipe> Recipes { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public  ICollection<Menu> Menus { get; set; }
        public   ICollection<Plan> Plans { get; set; } 

        public int ID { get; set; }
        public DateTime Date { get; set; } 
        public string Author { get; set; }

     
    }
}
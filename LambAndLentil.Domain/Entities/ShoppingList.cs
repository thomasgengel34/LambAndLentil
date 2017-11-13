using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
 

namespace LambAndLentil.Domain.Entities 
{
    [Table("SHOPPINGLIST.ShoppingList")]
    public class ShoppingList : BaseEntity, IEntityChildClasses, IShoppingList
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
         
   
        public new  List<Recipe> Recipes { get; set; } 
        public new List<Menu> Menus { get; set; }
        public  new  List<Plan> Plans { get; set; } 

        public int ID { get; set; }
        public DateTime Date { get; set; } 
        public string Author { get; set; }

        private new List<ShoppingList> ShoppingLists { get; set; }
        private new List<Person> Persons { get; set; }
    }
}
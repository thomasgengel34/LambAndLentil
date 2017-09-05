using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LambAndLentil.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace LambAndLentil.UI.Models
{
    public class ShoppingListVM : BaseVM, IBaseVM, IShoppingList, IShoppingListVM
    {
         

 
        

        public ShoppingListVM()
        {
            Date = DateTime.Now;
        }

        public ShoppingListVM(DateTime creationDate):this()
        {
            CreationDate = creationDate;
        }

        public string Author { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Menu> Menus { get; set; } 
        public ICollection<Plan> Plans { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
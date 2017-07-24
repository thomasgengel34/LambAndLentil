using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LambAndLentil.Domain.Entities
{
    public class ShoppingLists  
    {
        public int ID { get; set; }
        public List<ShoppingList> MyShoppingLists { get; set; }

        public ShoppingLists(List<ShoppingList> ShoppingList)
        {
            MyShoppingLists = ShoppingList;
        }

        public static SelectList GetListOfAllShoppingListNames(List<ShoppingList> MyShoppingLists)
        {
            var list = from n in MyShoppingLists.AsQueryable()
                       select n.Name;
            SelectList descriptions = new SelectList(list);
            return descriptions;
        }
    } 
}

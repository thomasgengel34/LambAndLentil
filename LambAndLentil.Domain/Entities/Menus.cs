using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Domain.Entities
{
    public  class Menus  
    {
        public int ID { get; set; }
        public List<Menu> MyMenus { get; set; }

        public Menus(List<Menu> Menu)
        {
            MyMenus = Menu;
        }

        public static SelectList GetListOfAllMenuNames(List<Menu> MyMenus)
        {
            var list = from n in MyMenus.AsQueryable()
                       select n.Name;
            SelectList descriptions = new SelectList(list);
            return descriptions;
        }
    }
}

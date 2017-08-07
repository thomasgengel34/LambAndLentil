using LambAndLentil.Domain.Entities;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambAndLentil.UI.Models
{
    //public class  ListVM 
    //{ 
    //    public IEnumerable<Ingredient> Ingredients { get; set; } 
    //    public IEnumerable<Recipe> Recipes { get; set; }
    //    public IEnumerable<Menu> Menus { get; set; }
    //    public IEnumerable<Person>Persons { get; set; }
    //    public IEnumerable<Plan> Plans { get; set; }
    //    public IEnumerable<ShoppingList> ShoppingLists { get; set; }
    //    public PagingInfo PagingInfo { get; set; } 
    //}

    public class ListVM<T, TVM>
        where T : class
        where TVM : BaseVM
    {
        private DateTime creationDate;


        public ListVM()
        {
            Entities = new List<T>();
            List = new List<TVM>();
        }

        public ListVM(DateTime creationDate)
        {
            this.creationDate = creationDate;
        }

        public string Name { get; set; }
        public IEnumerable<T> Entities { get; set; }
        public IEnumerable<TVM> List { get; set; }
        public PagingInfo PagingInfo { get; set; }


        public ListVM<T, TVM> Add(ListVM<T, TVM> list, BaseVM item)
        {
            return list;
        }
    }


}
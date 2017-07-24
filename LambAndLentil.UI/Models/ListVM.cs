using LambAndLentil.Domain.Entities;
using System.Collections;
using System.Collections.Generic;
using System;

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

    public class ListVM<T, TVM>:IEnumerable<T>
        where T : class
        where TVM : BaseVM
    {
        private DateTime creationDate;

        public ListVM()
        {

        }

        public ListVM(DateTime creationDate)
        {
            this.creationDate = creationDate;
        }

        public string Name { get; set; }
        public IEnumerable<T> Entities { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public ListVM<T, TVM> List {get;set;}

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)List).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)List).GetEnumerator();
        }
    }
}
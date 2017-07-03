using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LambAndLentil.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace LambAndLentil.UI.Models
{
    public class ShoppingListVM:BaseVM,IBaseVM
    {


        public string Author { get; set; }
        public DateTime Date { get; set; }

        public List<Ingredient> Ingredients { get; set; }
        public int ID { get; set ; }

        public ShoppingListVM()
        {
            Date = DateTime.Now;
        }

        public ShoppingListVM(DateTime creationDate):this()
        {
            CreationDate = creationDate;
        }
    }
}
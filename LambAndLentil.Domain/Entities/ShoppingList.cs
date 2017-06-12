﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
 

namespace LambAndLentil.Domain.Entities 
{
    [Table("SHOPPINGLIST.ShoppingList")]
    public class ShoppingList:BaseEntity
    {
        public ShoppingList() : base(new DateTime(2010, 1, 1))
        {

        }
        public DateTime Date { get; set; } 
        public string Author { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}
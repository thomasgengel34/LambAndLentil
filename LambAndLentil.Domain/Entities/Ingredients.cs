using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Domain.Entities
{
    public class Ingredients  
    {
        public int ID { get; set; }
        public List<Ingredient> MyIngredients { get; set; }

        public Ingredients(List<Ingredient> Ingredient)
        {
            MyIngredients = Ingredient;
        }

        
    }
}
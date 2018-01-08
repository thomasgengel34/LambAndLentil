using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Test.TestObjects
{
    public class TestIngredient
    {
        public Ingredient Ingredient { get; set; }
        public List<Ingredient> ListOfIngredients { get; set; }


        public TestIngredient()
        {
            Ingredient = new Ingredient();
            ListOfIngredients = new BaseTestObjects<Ingredient>().SetUpTList();
        }

    }
}

using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("RecipesController")]
    public class RecipesController_Test_Should : BaseControllerTest<Recipe>
    {

        protected static RecipesController Controller { get; set; }
        protected static Recipe Recipe { get; set; }

        public RecipesController_Test_Should()
        {
            ListEntity = new ListEntity<Recipe>();
            Controller = new RecipesController(Repo)
            {
                PageSize = 3
            };

            Recipe = new Recipe
            {
                ID = 300,
                Name = "RecipesController_Test_Should",
                Servings = 4.5m,
                MealType = MealType.Breakfast,
                Calories = 100,
                CalsFromFat = 10,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient{Name="Salt"},
                    new Ingredient{Name="Egg"},
                    new Ingredient{Name="Pepper"}
                }
            };
        }

    
    }
}

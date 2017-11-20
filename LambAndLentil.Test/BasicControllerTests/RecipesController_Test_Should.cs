using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
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
         
        protected static Recipe Recipe { get; set; }
        protected static Recipe ReturnedRecipe { get; set; }

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
                    new Ingredient{ID=4000, Name="Salt"},
                    new Ingredient{ID=4001, Name="Egg"},
                    new Ingredient{ID=4002, Name="Pepper"}
                }
            };
            Repo.Save(Recipe);
        } 
    }
}

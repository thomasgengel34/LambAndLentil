﻿using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory(" IngredientsController")]
    public class IngredientsController_Test_Should : BaseControllerTest<Ingredient>
    {
        protected static IngredientsController Controller; 
        protected static Ingredient Ingredient { get; set; }
        protected static Ingredient ReturnedIngredient { get; set; }


        protected IngredientsController_Test_Should()
        { 
            Controller = new IngredientsController(Repo)
            {
                PageSize = 3
            };
            Ingredient = new Ingredient
            {
                ID = 1,
                Recipe = new Recipe(),
                IngredientsList = "IngredientsController_Test_Should : BaseControllerTest<Ingredient> BasicIngredients IngredientsList",
                Ingredients = new List<Ingredient>()
            };
                
           
        Ingredient.Ingredients.Add(new Ingredient { ID = 545, Name = "Default" });
        }

       
    }
}

using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace  LambAndLentil.Test.BaseControllerTests
{

    [TestClass]
    [TestCategory(" IngredientsController")]
   public class IngredientsController_Test_Should : BaseControllerTest<Ingredient>
    {
       
        protected static  Ingredient Ingredient { get; set; }
        protected static  Ingredient ReturnedIngredient { get; set; }


         public IngredientsController_Test_Should()
        { 
             controller = new IngredientsController(repo)
            {
                PageSize = 3
            };
            Ingredient = new Ingredient
            {
                ID = 1,
                IngredientsList = "IngredientsController_Test_Should : BaseControllerTest<Ingredient> BasicIngredients IngredientsList",
                Ingredients = new List<Ingredient>()
            };
            Ingredient ingredient = new Ingredient() { ID = 545, Name = "Default" };
            Ingredient.Ingredients.Add(new Ingredient() { ID = 545, Name = "Default" });
        }


      
        private static void  InheritIngredientsControllerAsync()
        {
            Type type = Type.GetType("LambAndLentil.UI.Controllers.IngredientsController, LambAndLentil.UI", true).GetInterface("IIngredientsControllerAsync");

            Assert.IsNotNull(type);
        } 
    }
}

using LambAndLentil.Domain.Entities;
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
       
        protected static IIngredient Ingredient { get; set; }
        protected static IIngredient ReturnedIngredient { get; set; }


       public IngredientsController_Test_Should()
        { 
             Controller = new IngredientsController(Repo)
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
    }
}

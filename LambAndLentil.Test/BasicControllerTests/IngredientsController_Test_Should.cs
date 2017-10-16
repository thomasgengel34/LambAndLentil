using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory(" IngredientsController")]
    public class IngredientsController_Test_Should : BaseControllerTest<Ingredient>
    {
        protected static IngredientsController Controller;
        protected static Ingredient BasicIngredient { get; set; }

       protected IngredientsController_Test_Should()
        { 
            Controller = new IngredientsController(Repo)
            {
                PageSize = 3
            };
            BasicIngredient = new Ingredient
            {
                ID = 1,
                Recipe = new Recipe(),
                IngredientsList = "IngredientsController_Test_Should : BaseControllerTest<Ingredient> BasicIngredients IngredientsList" 
            };
        }

       
    }
}

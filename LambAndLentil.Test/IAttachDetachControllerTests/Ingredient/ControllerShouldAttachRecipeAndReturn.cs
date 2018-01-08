using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;
using RecipeType = LambAndLentil.Domain.Entities.Recipe;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Ingredient
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldAttachRecipeAndReturn : BaseControllerShouldAttachXAndReturn<IngredientType, RecipeType>
    {
        // ingredient cannot attach a plan
         

        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParent();


        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNotForAnExistingIngredient() =>
             BaseIndexWithErrorWhenParentIDIsNotForAnExistingIngredient();

         
    }
}

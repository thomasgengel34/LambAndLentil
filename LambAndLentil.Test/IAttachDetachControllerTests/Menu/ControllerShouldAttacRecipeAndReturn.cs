using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MenuType = LambAndLentil.Domain.Entities.Menu;
using RecipeType = LambAndLentil.Domain.Entities.Recipe;


namespace LambAndLentil.Test.IAttachDetachControllerTests.Menu
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldAttachRecipeAndReturn: BaseControllerShouldAttachXAndReturn<MenuType,RecipeType>
    { 
        [TestMethod]
        public void DetailWithSuccessWhenParentIDIsValidAndChildIsValid() => BaseDetailWithSuccessWhenParentIDIsValidAndChildIsValid();
         
        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParent(); 

        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNotForAnExistingMenu() => BaseIndexWithErrorWhenParentIDIsNotForAnExistingIngredient(); 
         
        [TestMethod]
        public void DetailWithErrorWhenParentIDIsValidAndChildIsNotValid() => BaseDetailWithErrorWhenParentIDIsValidAndChildIsNotValid(); 
         
    }
}

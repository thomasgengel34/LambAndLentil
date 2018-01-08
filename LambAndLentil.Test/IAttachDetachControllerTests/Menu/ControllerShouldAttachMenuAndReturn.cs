using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;
using MenuType = LambAndLentil.Domain.Entities.Menu;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Menu
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldAttachMenuAndReturn : BaseControllerShouldAttachXAndReturn<MenuType, MenuType>
    {
        //menu cannot attach a menu
         

        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParent();


         

        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNotForAnExistingMenu() =>
             BaseIndexWithErrorWhenParentIDIsNotForAnExistingMenu(); 
    } 
}

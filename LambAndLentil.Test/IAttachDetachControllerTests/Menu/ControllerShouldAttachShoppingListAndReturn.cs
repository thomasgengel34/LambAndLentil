using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting; 
using MenuType = LambAndLentil.Domain.Entities.Menu;
using  ShoppingListType = LambAndLentil.Domain.Entities.ShoppingList;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Menu
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldAttachShoppingListAndReturn : BaseControllerShouldAttachXAndReturn<MenuType, ShoppingListType>
    {
        //menu cannot attach a shopping list
         

        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParent();


 

        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNotForAnExistingMenu() =>
             BaseIndexWithErrorWhenParentIDIsNotForAnExistingMenu();  
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BaseControllerTests
{
    [TestClass]
    [TestCategory("Attach-Detach")]
   internal class ShoppingListController_AttachDetachChild : ShoppingListsController_Test_Should
    {
         
        [TestMethod]
        public void SuccessfullyDetachASetOfMenuChildren()
        {
            //ShoppingList.Menus.Add(new Menu { ID = 4005, Name = "Butter" });
            //ShoppingList.Menus.Add(new Menu { ID = 4006, Name = "Cayenne Pepper" });
            //ShoppingList.Menus.Add(new Menu { ID = 4007, Name = "Cheese" });
            //ShoppingList.Menus.Add(new Menu { ID = 4008, Name = "Chopped Green Pepper" });
            //repo.Save(ShoppingList);
            //int initialMenuCount = ShoppingList.Menus.Count();

            //var setToSelect = new HashSet<int> { 4006, 4008 };
            //List<IEntity> selected = ShoppingList.Menus.Where(t => setToSelect.Contains(t.ID)).ToList();
            //controller.DetachASetOf(ShoppingList, selected);
            //ShoppingList returnedShoppingList = repo.GetById(ShoppingList.ID);

            //Assert.AreEqual(initialMenuCount - 2, returnedShoppingList.Menus.Count());
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BaseControllerTests
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ShoppingListController_AttachDetachChild : ShoppingListsController_Test_Should
    {

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachFirstMenuChild()
        {
            //IGenericController<ShoppingList> DetachController = new ShoppingListsController(Repo);
            //BaseSuccessfullyDetachChild(Repo, Controller, DetachController, UIControllerType.ShoppingLists, 0);
        }


        [TestMethod]
        public void SuccessfullyDetachASetOfMenuChildren()
        {
            //ShoppingList.Menus.Add(new Menu { ID = 4005, Name = "Butter" });
            //ShoppingList.Menus.Add(new Menu { ID = 4006, Name = "Cayenne Pepper" });
            //ShoppingList.Menus.Add(new Menu { ID = 4007, Name = "Cheese" });
            //ShoppingList.Menus.Add(new Menu { ID = 4008, Name = "Chopped Green Pepper" });
            //Repo.Save(ShoppingList);
            //int initialMenuCount = ShoppingList.Menus.Count();

            //var setToSelect = new HashSet<int> { 4006, 4008 };
            //List<IEntity> selected = ShoppingList.Menus.Where(t => setToSelect.Contains(t.ID)).ToList();
            //Controller.DetachASetOf(ShoppingList, selected);
            //ShoppingList returnedShoppingList = Repo.GetById(ShoppingList.ID);

            //Assert.AreEqual(initialMenuCount - 2, returnedShoppingList.Menus.Count());
        }
    }
}

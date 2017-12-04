using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ShoppingListController_AttachDetachMenuChild : ShoppingListsController_Test_Should
    {
       

        [TestMethod]
        public void SuccessfullyAttachMenuChild()
        {
            IGenericController<ShoppingList> DetachController = new ShoppingListsController(Repo);
            BaseSuccessfullyDetachMenuChild(Repo, Controller, DetachController, UIControllerType.ShoppingLists);
        } 

        [TestMethod]
        public void SuccessfullyDetachFirstMenuChild()
        {
            IGenericController<ShoppingList> DetachController = new ShoppingListsController(Repo);
            BaseSuccessfullyDetachIngredientChild(Repo, Controller, DetachController, UIControllerType.ShoppingLists, 0);
        }

        
        [TestMethod]
        public void SuccessfullyDetachASetOfMenuChildren()
        {
            // Arrange 
            ShoppingList.Menus.Add(new Menu { ID = 4005, Name = "Butter" });
            ShoppingList.Menus.Add(new Menu { ID = 4006, Name = "Cayenne Pepper" });
            ShoppingList.Menus.Add(new Menu { ID = 4007, Name = "Cheese" });
            ShoppingList.Menus.Add(new Menu { ID = 4008, Name = "Chopped Green Pepper" });
            Repo.Save(ShoppingList);
            int initialMenuCount = ShoppingList.Menus.Count();

            // Act
            var setToSelect = new HashSet<int> { 4006, 4008 };
            List<Menu> selected = ShoppingList.Menus.Where(t => setToSelect.Contains(t.ID)).ToList();
            Controller.DetachAllMenus(ShoppingList.ID, selected);
            ShoppingList returnedShoppingList = Repo.GetById(ShoppingList.ID);

            // Assert
            Assert.AreEqual(initialMenuCount - 2, returnedShoppingList.Menus.Count());
        }

        [TestMethod]
        public void SuccessfullyDetachtheLastMenuChild() => BaseDetachTheLastMenuChild(Repo, Controller,ShoppingList);


        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachAllMenuChildren() =>
            // Arrange

            // Act

            //Assert
            Assert.Fail();

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWithWarningIfAttachingNullMenuChild() { }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownMenuChildID() { }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild() { }
    }
}

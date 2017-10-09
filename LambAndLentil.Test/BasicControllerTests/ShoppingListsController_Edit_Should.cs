
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("ShoppingListsController")]

    public class ShoppingListsController_Edit_Should:ShoppingListsController_Test_Should
    {
        public ShoppingListsController_Edit_Should():base()
        {

        }
        [Ignore]
        [TestMethod]
        public void CorrectShoppingListsAreBoundInEdit()
        {
            Assert.Fail();
        }


        [Ignore]   // not working, not done, not sure it's worth pursuing or abandoning
        [TestMethod]
        [TestCategory("Edit")]
        public void CanEditShoppingList()
        {
            // Arrange 
            ShoppingListsController Controller2 = new ShoppingListsController(Repo);

            // Act  
            ViewResult view1 = (ViewResult)Controller.Edit(int.MaxValue);
            ShoppingList p1 = (ShoppingList)view1.Model;
            ViewResult view2 = (ViewResult)Controller.Edit(int.MaxValue - 1);
            ShoppingList p2 = (ShoppingList)view2.Model;
            ViewResult view3 = (ViewResult)Controller.Edit(int.MaxValue - 2);
            ShoppingList p3 = (ShoppingList)view3.Model;

            // Assert 
            Assert.IsNotNull(view1);

        }



        [TestMethod]
        [TestCategory("Edit")]
        public void CannotEditNonexistentShoppingList()
        {
            // Arrange

            // Act
            ShoppingList result = (ShoppingList)((ViewResult)Controller.Edit(8)).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }


        [Ignore]   // look into why this is not working
        [TestMethod]
        [TestCategory("Edit")]
        public void CanEditShoppingListXXX()
        {
            // Arrange
            ShoppingList menu = new ShoppingList
            {
                ID = 1,
                Name = "test ShoppingListControllerTest.CanEditShoppingList",
                Description = "test ShoppingListControllerTest.CanEditShoppingList"
            };
            Repo.Save(menu);

            // Act 
            menu.Name = "Name has been changed";

            ViewResult view1 = (ViewResult)Controller.Edit(1);

            var returnedShoppingList = (ShoppingList)(view1.Model);


            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual("Name has been changed", returnedShoppingList.Name);
            //Assert.AreEqual(menu.Description, returnedShoppingListListEntity.Description);
            //Assert.AreEqual(menu.CreationDate, returnedShoppingListListEntity.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedShoppingList()
        {
            // Arrange
            ShoppingListsController indexController = new ShoppingListsController(Repo);
            ShoppingListsController Controller2 = new ShoppingListsController(Repo);
            ShoppingListsController Controller3 = new ShoppingListsController(Repo);


            ShoppingList vm = new ShoppingList
            {
                Name = "0000 test",
                ID = int.MaxValue - 100,
                Description = "test ShoppingListsControllerShould.SaveEditedShoppingList"
            };

            // Act 
            ActionResult ar1 = Controller.PostEdit(vm);


            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = 7777;
            ActionResult ar2 = Controller2.PostEdit(vm);
            ViewResult view2 = Controller3.Index();
             ListEntity<ShoppingList> ListEntity2 = ( ListEntity<ShoppingList>)view2.Model;
            ShoppingList vm3 = (from m in ListEntity2.ListT  
                                where m.Name == "0000 test Edited"
                                select m).AsQueryable().FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited", vm3.Name);
            Assert.AreEqual(7777, vm3.ID);

        }

        // [Ignore]  look into why this is not working
        [TestMethod]
        [TestCategory("Edit")]
        public void CanPostEditShoppingList()
        {
            // Arrange
            ShoppingList shoppingList = new ShoppingList
            {
                ID = 1,
                Name = "test ShoppingListControllerTest.CanEditShoppingList",
                Description = "test ShoppingListControllerTest.CanEditShoppingList"
            };
            Repo.Add(shoppingList);

            // Act 
            shoppingList.Name = "Name has been changed";
            Repo.Add(shoppingList);
            ViewResult view1 = (ViewResult)Controller.Edit(1);

            ShoppingList returnedShoppingListListEntity = Repo.GetById(1);

            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual("Name has been changed", returnedShoppingListListEntity.Name);
            Assert.AreEqual(shoppingList.Description, returnedShoppingListListEntity.Description);
            Assert.AreEqual(shoppingList.CreationDate, returnedShoppingListListEntity.CreationDate);
        }


        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void XxxCannotEditNonexistentShoppingList()
        {
            // Arrange

            // Act
            ShoppingList result = (ShoppingList)((ViewResult)Controller.Edit(8)).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }


    }
}

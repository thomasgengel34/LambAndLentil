
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BaseControllerTests
{
    [TestClass]
    [TestCategory("ShoppingListsController")]

    public class ShoppingListsController_Edit_Should:ShoppingListsController_Test_Should
    {
       private static IGenericController<ShoppingList>  Controller2, Controller3; 

        public ShoppingListsController_Edit_Should():base()
        {

        }

        [Ignore]
        [TestMethod]
        public void CorrectShoppingListsAreBoundInEdit() => Assert.Fail();


        // not working, not done, not sure it's worth pursuing or abandoning
        [TestMethod]
        [TestCategory("Edit")]
        public void CanEditShoppingList()
        {
            // Arrange 
            ShoppingListsController Controller2 = new ShoppingListsController(Repo);
              
            ViewResult view1 = (ViewResult)Controller.Edit(int.MaxValue);
            //ShoppingList p1 = (ShoppingList)view1.Model;
            //ViewResult view2 = (ViewResult)Controller.Edit(int.MaxValue - 1);
            //ShoppingList p2 = (ShoppingList)view2.Model;
            //ViewResult view3 = (ViewResult)Controller.Edit(int.MaxValue - 2);
            //ShoppingList p3 = (ShoppingList)view3.Model;
             
            Assert.IsNotNull(view1);

        }

         

         

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedShoppingList()
        { 
             Controller2 = new ShoppingListsController(Repo);
              Controller3 = new ShoppingListsController(Repo);


            ShoppingList.Name = "0000 test";
            ShoppingList.ID = int.MaxValue - 100;
            ShoppingList.Description = "test ShoppingListsControllerShould.SaveEditedShoppingList"; 

            // Act 
            ActionResult ar1 = Controller.PostEdit((ShoppingList)ShoppingList);


            // now edit it
            ShoppingList.Name = "0000 test Edited";
            ShoppingList.ID = 7777;
            ActionResult ar2 = Controller2.PostEdit((ShoppingList)ShoppingList);
            ViewResult view2 = (ViewResult)Controller3.Index();
             ListEntity<ShoppingList> ListEntity2 = ( ListEntity<ShoppingList>)view2.Model;
            ShoppingList ShoppingList3 = (from m in ListEntity2.ListT  
                                where m.Name == "0000 test Edited"
                                select m).AsQueryable().FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited", ShoppingList3.Name);
            Assert.AreEqual(7777, ShoppingList3.ID);

        }
         
        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void XxxCannotEditNonexistentShoppingList()
        { 
            ShoppingList result = (ShoppingList)((ViewResult)Controller.Edit(8)).ViewData.Model;
          
            Assert.IsNull(result);
        } 
    }
}

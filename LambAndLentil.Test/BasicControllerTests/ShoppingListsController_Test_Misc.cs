using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BaseControllerTests
{

    [TestClass]
    [TestCategory("ShoppingListsController")]
   internal class ShoppingListsController_Test_Misc : ShoppingListsController_Test_Should
    { 
       
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void SuccessfullyDeleteAFoundShoppingList()
        {   // does not actually detach, just sets up to remove it.
            // TODO: verify "Are you sure you want to delete this?" message shows up.
            // Arrange
            int count = repo.Count();
             
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.Delete(int.MaxValue);
            ViewResult view = (ViewResult)adr.InnerResult;

            string viewName = view.ViewName;
            int newCount = repo.Count();
            string message = adr.Message;

            // Assert
            Assert.IsNotNull(view); 
             Assert.AreEqual(UIViewType.Details.ToString(), viewName);
            Assert.AreEqual(count, newCount);
            Assert.AreEqual("Here it is!", message); 
        }

         

        [TestMethod]
        [TestCategory("Detach")]
        public void DetachConfirmed()
        { 
            int count = repo.Count();

            // Act
            ActionResult result = controller.DeleteConfirmed(int.MaxValue) as ActionResult;
            int newCount = repo.Count();
            // TODO: improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { Controller = "ShoppingLists", Action = "Index" } } );
            //TODO: check message

            // Assert 
            Assert.IsNotNull(result);
            Assert.AreEqual(count - 1, newCount);
        }

        [TestMethod]
        [TestCategory("Detach")]
        public void CanDetachValidShoppingList()
        {
            // Arrange - create an shoppingList
            ShoppingList shoppingListEntity = new ShoppingList { ID = 2, Name = "Test2" };
            repo.Save(shoppingListEntity);

            // Act - delete the shoppingList Entity
            ActionResult result = controller.DeleteConfirmed(shoppingListEntity.ID);

            AlertDecoratorResult adr = (AlertDecoratorResult)result;

            // Assert - ensure that the repository delete method was called with a correct ShoppingList



            Assert.AreEqual("Test2 has been deleted", adr.Message);
        }
 
         

        [Ignore]
        [TestMethod]
        public void WhenAFlagHasBeenRemovedFromOnePersonStillThereForSecondFlaggedPerson()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("Copy")]
        [TestMethod]
        public void CopyModifySaveWithANewName()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void CorrectPropertiesAreBoundInEdit()
        {
            Assert.Fail();
        }

    
    }
}

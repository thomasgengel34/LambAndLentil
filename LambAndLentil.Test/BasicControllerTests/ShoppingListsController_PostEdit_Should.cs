using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("ShoppingListsController")]

    public class ShoppingListsController_PostEdit_Should: ShoppingListsController_Test_Should
    {
        [TestMethod]
        public void ReturnIndexWithValidModelStateWithSuccessMessageWhenSaved()
        {
            // Arrange
            ShoppingList rvm = new ShoppingList
            {
                ID = -2
            };


            // Act
            ActionResult ar =Controller.PostEdit(rvm);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            // Assert
            Assert.AreEqual("Something is wrong with the data!",adr.Message);
            Assert.AreEqual("alert-warning",adr.AlertClass);
            Assert.AreEqual("Details", view.ViewName);


        }

        [TestMethod]
        public void ReturnIndexWithInValidModelStateWithWarningMessageWhenSaved()
        {

            // Arrange
            ShoppingList rvm = new ShoppingList
            {
                ID = -2
            };


            // Act
            ActionResult ar = Controller.PostEdit(rvm);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            // Assert
            Assert.AreEqual("Something is wrong with the data!", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Details", view.ViewName);
             
        }

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
        public void NotSaveLogicallyInvalidModel()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();

        }

        [Ignore]
        [TestMethod]
        public void NotSaveModelFlaggedInvalidByDataAnnotation()
        {  // see https://msdn.microsoft.com/en-us/library/cc668224(v=vs.98).aspx

            // Arrange

            // Act

            // Assert
            Assert.Fail();

        }
    }
} 

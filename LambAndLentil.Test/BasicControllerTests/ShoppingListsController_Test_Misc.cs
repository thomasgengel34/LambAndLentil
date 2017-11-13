using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("ShoppingListsController")]
    public class ShoppingListsController_Test_Misc : ShoppingListsController_Test_Should
    {
        [TestMethod]
        public void IsPublic()
        {
            // Arrange 

            // Act
            Type type = Controller.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        }

        [TestMethod]
        public void InheritsFromBaseControllerCorrectly()
        {
            // Arrange

            // Act 
            Controller.PageSize = 4;

            var type = typeof(ShoppingListsController);
            var DoesDisposeExist = type.GetMethod("Dispose");

            // Assert 
            Assert.AreEqual(4, Controller.PageSize);
            Assert.IsNotNull(DoesDisposeExist);
        }





        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void SuccessfullyDeleteAFoundShoppingList()
        {   // does not actually detach, just sets up to remove it.
            // TODO: verify "Are you sure you want to delete this?" message shows up.
            // Arrange
            int count = Repo.Count();

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Delete(int.MaxValue);
            ViewResult view = (ViewResult)adr.InnerResult;

            string viewName = view.ViewName;
            int newCount = Repo.Count();
            string message = adr.Message;

            // Assert
            Assert.IsNotNull(view); 
             Assert.AreEqual(UIViewType.Details.ToString(), viewName);
            Assert.AreEqual(count, newCount);
            Assert.AreEqual("Here it is!", message); 
        }



        [TestMethod]
        [TestCategory("Remove")]
        public void DetachAnInvalidShoppingList()
        {
            // Arrange 

            // Act 
            var view = Controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Shopping List was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);

            Assert.AreEqual(UIViewType.Index.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());

        }

        [TestMethod]
        [TestCategory("Detach")]
        public void DetachConfirmed()
        {
            // Arrange
            int count = Repo.Count();

            // Act
            ActionResult result = Controller.DeleteConfirmed(int.MaxValue) as ActionResult;
            int newCount = Repo.Count();
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
            Repo.Add(shoppingListEntity);

            // Act - delete the shoppingList Entity
            ActionResult result = Controller.DeleteConfirmed(shoppingListEntity.ID);

            AlertDecoratorResult adr = (AlertDecoratorResult)result;

            // Assert - ensure that the repository delete method was called with a correct ShoppingList



            Assert.AreEqual("Test2 has been deleted", adr.Message);
        }


        [TestMethod]
        public void CreateReturnsNonNull()
        {
            // Arrange 

            // Act
            ViewResult result = Controller.Create(UIViewType.Create) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Ignore]
        [TestMethod]
        public void FlagAnIngredientFlaggedInAPerson()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void FlagAnIngredientFlaggedInTwoPersons()
        {
            Assert.Fail();
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

        [TestMethod]
        public void GetTheClassNameCorrect()
        {
            // Arrange

            // Act


            // Assert
            //  Assert.Fail();
            Assert.AreEqual("LambAndLentil.UI.Controllers.ShoppingListsController", Controller.ToString());
        }
    }
}

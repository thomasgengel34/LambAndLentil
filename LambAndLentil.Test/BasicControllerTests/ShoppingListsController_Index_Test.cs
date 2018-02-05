using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("ShoppingListsController")]
    public class ShoppingListsController_Index_Test : ShoppingListsController_Test_Should
    {
     

        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate_ArrayThirdItemNameIsCorrect()
        {
            var result = (ListEntity<ShoppingList>)((ViewResult)Controller.Index(1)).Model;
            ShoppingList[] ingrArray1 = result.ListT.ToArray();

            Assert.AreEqual("ControllerTest3", ingrArray1[2].Name);
        }

        [Ignore]
        [TestMethod]
        public void FlagAnShoppingListFlaggedInAPerson()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void FlagAnShoppingListFlaggedInTwoPersons()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void WhenAFlagHasBeenRemovedFromOnePersonStillThereForSecondFlaggedPerson()
        {
            Assert.Fail();
        } 
    }
}

using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParentType = LambAndLentil.Domain.Entities.Recipe;
using ChildType = LambAndLentil.Domain.Entities.ShoppingList;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Recipe
{
    [TestClass]
    public class ControllerShouldDetachAllShoppingListsAndReturn : BaseControllerShouldDetachXAndReturn<ParentType, ChildType>
    {
        [Ignore]
        [TestMethod]
        public void DetailWithSuccessWhenIDisValidAndThereIsOneIngredientOnList()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DetailWithSuccessWhenIDisValidAndThereAreThreeIngredientsOnList()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DetailWithErrorWhenIDisNotForAFoundParent()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }
    }
}

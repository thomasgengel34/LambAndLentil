using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;
using ShoppingListType = LambAndLentil.Domain.Entities.ShoppingList;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Ingredient
{
    [TestClass]
    public class ControllerShouldDetachAllShoppingListsAndReturn : BaseControllerShouldDetachXAndReturn<IngredientType, ShoppingListType>
    {
        // since ShoppingList cannot be attached to an ingredient, these tests should return with an error


        [TestMethod]
        public void DetailWithSuccessWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndChildCannotBeAttachedWhenDetachingAll()
       => BaseDetailWithDangerWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndChildCannotBeAttachedWhenDetachingAll();

        [TestMethod]
        public void DetailWithErrorWhenIDisNotForAFoundParentWhenDetachingAndChildCannotBeAttachedWhenDetachingAll()
        => BaseDetailWithErrorWhenIDisNotForAFoundParentWhenDetachingAndChildCannotBeAttachedWhenDetachingAll();
    }
}

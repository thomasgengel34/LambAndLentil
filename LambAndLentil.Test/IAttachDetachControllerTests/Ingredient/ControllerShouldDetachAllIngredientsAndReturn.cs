using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Ingredient
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldDetachAllIngredientsAndReturn : BaseControllerShouldDetachXAndReturn<IngredientType, IngredientType>
    {
        [TestMethod]
        public void DetailWithSuccessWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndSelectionSetIsNotSupplied() => BaseDetailWithSuccessWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndSelectionSetIsNotSupplied();


        [TestMethod]
        public void DetailWithSuccessWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndSelectionSetIsSupplied() => BaseDetailWithSuccessWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndSelectionSetIsSupplied();


        [TestMethod]
        public void DetailWithSuccessWhenIDisValidAndThereAreThreeChildrenOnList() =>
           BaseDetailWithSuccessWhenIDisValidAndThereAreThreeChildrenOnListWhenDetachingAll();


        [TestMethod]
        public void DetailWithErrorWhenIDisNotForAFoundParentWhenDetachingAll() =>
BaseDetailWithErrorWhenIDisNotForAFoundParentWhenDetachingAll();

        [Ignore]
        [TestMethod]
        public void DetailWithErrorWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSuppliedAndThereIsNoChildAttached()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }
    }
}

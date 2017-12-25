using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Ingredient
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldAttachIngredientAndReturn : BaseControllerShouldAttachXAndReturn<IngredientType, IngredientType>
    {
        [TestMethod]
        public void DetailWithSuccessWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSupplied() => BaseDetailWithSuccessWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSupplied();


        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParent();



        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNotForAnExistingIngredient() =>
            BaseIndexWithErrorWhenParentIDIsNotForAnExistingIngredient();


        [TestMethod]
        public void DetailWithErrorWhenParentIDIsValidAndChildIsNotValid() => BaseDetailWithErrorWhenParentIDIsValidAndChildIsNotValid();

        [Ignore]
        [TestMethod]
        public void DetailWithWarningWhenParentIDIsValidAndChildIstValidAndOrderNumberIsNegativeWhenDetaching() => //BaseDetailWithWarningWhenParentIDIsValidAndChildIstValidAndOrderNumberIsNegativeWhenDetaching();
            Assert.Fail();


        [TestMethod]
        public void DetailWithSuccessWhenParentIDIsValidAndChildIstValidAndOrderNumberIsInUseWhenAttaching() => BaseDetailWithSuccessWhenParentIDIsValidAndChildIstValidAndOrderNumberIsInUseWhenAttaching();



        [TestMethod]
        public void DetailWithSuccessWhenParentIDIsValidAndChildIsValidAndOrderNumberIsGreaterThanTheNumberOfElementsWhenAttaching() =>  BaseDetailWithSuccessWhenParentIDIsValidAndChildIsValidAndOrderNumberIsGreaterThanTheNumberOfElementsWhenAttaching();

         
    }
}

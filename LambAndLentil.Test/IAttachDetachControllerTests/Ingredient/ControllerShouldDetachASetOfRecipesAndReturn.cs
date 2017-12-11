using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;
using RecipeType = LambAndLentil.Domain.Entities.Recipe;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Ingredient
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldDetachASetOfRecipesAndReturn : BaseControllerShouldDetachXAndReturn<IngredientType, RecipeType>
    {
        // ingredient cannot attach a menu

        [TestMethod]
        public void DetailWithErrorWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSuppliedWhenAttachingUnattachableChild() => BaseDetailWithErrorWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSuppliedWhenDetachingUnattachableChild();



        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParentWhenDetaching();


        [TestMethod]
        public void IndexWithWarningWhenParentIDIsNotForAnExistingIngredientWhenDetachingUnattachableChild() =>
            BaseIndexWithWarningWhenParentIDIsNotForAnExistingIngredientWhenDetachingUnattachableChild();

        [TestMethod]
        public void DetailWithErrorWhenParentIDIsValidAndChildIsNotValid() => BaseDetailWithErrorWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSuppliedWhenDetachingUnattachableChild();

        [TestMethod]
        public void IndexWithWarningWhenParentIDIsValidAndChildIstValidAndOrderNumberIsNegative() => BaseIndexWithWarningWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberIsNegativeWhenAttachingUnattachableChild();

        [TestMethod]
        public void DetailWithSuccessWhenParentIDIsValidAndChildIsValidAndOrderNumberIsGreaterThanTheNumberOfElementsWhenAttaching() => BaseDetailWithErrorWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSuppliedWhenAttachingUnattachableChild();
    }
}

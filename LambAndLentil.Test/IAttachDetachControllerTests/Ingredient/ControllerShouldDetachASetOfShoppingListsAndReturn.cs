using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;
using ShoppingListType = LambAndLentil.Domain.Entities.ShoppingList;
namespace LambAndLentil.Test.IAttachDetachControllerTests.Ingredient
{
    [TestClass]
     [TestCategory("Attach-Detach")]
    public class ControllerShouldDetachASetOfShoppingListsAndReturn : BaseControllerShouldDetachXAndReturn<IngredientType, ShoppingListType>
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

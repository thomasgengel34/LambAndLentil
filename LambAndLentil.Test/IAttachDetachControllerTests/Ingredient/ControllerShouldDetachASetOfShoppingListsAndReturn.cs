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
            public void DetailWithErrorWhenParentIDIsValidAndChildIsValidWhenAttachingUnattachableChild() => BaseDetailWithErrorWhenParentIDIsValidAndChildIsValidWhenDetachingUnattachableChild();



            [TestMethod]
            public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParentWhenDetaching();


            [TestMethod]
            public void IndexWithWarningWhenParentIDIsNotForAnExistingIngredientWhenDetachingUnattachableChild() =>
                BaseIndexWithWarningWhenParentIDIsNotForAnExistingIngredientWhenDetachingUnattachableChild();

            [TestMethod]
            public void DetailWithErrorWhenParentIDIsValidAndChildIsNotValid() => BaseDetailWithErrorWhenParentIDIsValidAndChildIsValidWhenDetachingUnattachableChild(); 
        }
    }

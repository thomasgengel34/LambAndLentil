using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;
using PlanType = LambAndLentil.Domain.Entities.Plan;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Ingredient
{
    [TestClass]
     [TestCategory("Attach-Detach")]
    public class ControllerShouldDetachASetOfPlansAndReturn:BaseControllerShouldDetachXAndReturn<IngredientType, PlanType>
    {
        // ingredient cannot attach a menu

        [TestMethod]
        public void DetailWithErrorWhenParentIDIsValidAndChildIsValidSuppliedWhenAttachingUnattachableChild() => BaseDetailWithErrorWhenParentIDIsValidAndChildIsValidWhenDetachingUnattachableChild();



        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParentWhenDetaching();


        [TestMethod]
        public void IndexWithWarningWhenParentIDIsNotForAnExistingIngredientWhenDetachingUnattachableChild() =>
            BaseIndexWithWarningWhenParentIDIsNotForAnExistingIngredientWhenDetachingUnattachableChild();

        [TestMethod]
        public void DetailWithErrorWhenParentIDIsValidAndChildIsNotValid() => BaseDetailWithErrorWhenParentIDIsValidAndChildIsValidWhenDetachingUnattachableChild(); 
    }
} 

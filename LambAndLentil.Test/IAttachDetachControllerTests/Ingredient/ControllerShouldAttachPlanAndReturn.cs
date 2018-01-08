using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using TParent = LambAndLentil.Domain.Entities.Ingredient;
using TChild = LambAndLentil.Domain.Entities.Plan;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Ingredient
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldAttachPlanAndReturn: BaseControllerShouldAttachXAndReturn<TParent, TChild>
    {
        // ingredient cannot attach a plan 

        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParent();


        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNotForAnExistingIngredient() =>
             BaseIndexWithErrorWhenParentIDIsNotForAnExistingIngredient();
         
    }
}

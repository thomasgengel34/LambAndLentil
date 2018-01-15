using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TParent = LambAndLentil.Domain.Entities.Menu;
using TChild = LambAndLentil.Domain.Entities.Plan;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Plan
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldDetachASetOfPlansAndReturn : BaseControllerShouldDetachXAndReturn<TParent, TChild>
    {
        // menu cannot attach a menu

        [TestMethod]
        public void DetailWithErrorWhenParentIDIsValidAndChildIsValidWhenAttachingUnattachableChild() => BaseDetailWithErrorWhenParentIDIsValidAndChildIsValidWhenDetachingUnattachableChild();



        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParentWhenDetaching();


        [TestMethod]
        public void IndexWithWarningWhenParentIDIsNotForAnExistingIngredientWhenDetachingUnattachableChild() =>
            BaseIndexWithWarningWhenParentIDIsNotForAnExistingIngredientWhenDetachingUnattachableChild(); 
    }
}


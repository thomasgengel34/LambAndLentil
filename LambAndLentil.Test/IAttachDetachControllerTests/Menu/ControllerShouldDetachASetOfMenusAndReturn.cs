using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TParent = LambAndLentil.Domain.Entities.Menu;
using TChild = LambAndLentil.Domain.Entities.Menu;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Menu
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldDetachASetOfMenusAndReturn : BaseControllerShouldDetachXAndReturn<TParent, TChild>
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

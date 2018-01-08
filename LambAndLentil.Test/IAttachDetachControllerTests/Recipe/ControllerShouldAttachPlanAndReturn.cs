using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TParent = LambAndLentil.Domain.Entities.Recipe;
using TChild= LambAndLentil.Domain.Entities.Plan;


namespace LambAndLentil.Test.IAttachDetachControllerTests.Recipe
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldAttachPlanAndReturn: BaseControllerShouldAttachXAndReturn<TParent,TChild>
    {
        // Recipe cannot attach a plan
         

        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParent();


        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNotForAnExistingIngredient() =>
             BaseIndexWithErrorWhenParentIDIsNotForAnExistingIngredient();
         
    }
}

using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting; 
using TParent = LambAndLentil.Domain.Entities.Menu;
using TChild = LambAndLentil.Domain.Entities.Plan;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Menu
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldAttachPlanAndReturn : BaseControllerShouldAttachXAndReturn<TParent, TChild>
    {
        // menu cannot attach a plan
         

        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParent();

         
        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNotForAnExistingMenu() =>
             BaseIndexWithErrorWhenParentIDIsNotForAnExistingMenu();  
    }
}

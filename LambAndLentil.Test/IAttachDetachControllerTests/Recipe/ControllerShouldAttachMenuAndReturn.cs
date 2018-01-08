using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TParent = LambAndLentil.Domain.Entities.Recipe;
using TChild = LambAndLentil.Domain.Entities.Menu;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Recipe
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldAttachMenuAndReturn : BaseControllerShouldAttachXAndReturn<TParent,TChild>
    {
        //recipe cannot attach a menu 

        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParent();

 

        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNotForAnExistingMenu() =>
             BaseIndexWithErrorWhenParentIDIsNotForAnExistingMenu(); 
    } 
}

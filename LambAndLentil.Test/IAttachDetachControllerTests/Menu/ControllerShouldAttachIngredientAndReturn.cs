using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParentType = LambAndLentil.Domain.Entities.Menu;
using ChildType = LambAndLentil.Domain.Entities.Menu;


namespace LambAndLentil.Test.IAttachDetachControllerTests.Menu
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldAttachIngredientAndReturn : BaseControllerShouldAttachXAndReturn<ParentType, ChildType>
    {
         // menu cannot have a menu child

        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParent();

        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNotForAnExistingMenu() => BaseIndexWithErrorWhenParentIDIsNotForAnExistingIngredient();


        [TestMethod]
        public void DetailWithErrorWhenParentIDIsValidAndChildIsNotValid() => BaseDetailWithErrorWhenParentIDIsValidAndChildIsNotValid();

      
    }       
}

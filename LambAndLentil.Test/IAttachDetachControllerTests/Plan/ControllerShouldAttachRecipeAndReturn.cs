using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TParent = LambAndLentil.Domain.Entities.Plan;
using TChild= LambAndLentil.Domain.Entities.Recipe;


namespace LambAndLentil.Test.IAttachDetachControllerTests.Plan
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldAttachRecipeAndReturn: BaseControllerShouldAttachXAndReturn<TParent,TChild>
    { 
        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParent(); 

        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNotForAnExistingMenu() => BaseIndexWithErrorWhenParentIDIsNotForAnExistingIngredient(); 
         
        [TestMethod]
        public void DetailWithErrorWhenParentIDIsValidAndChildIsNotValid() => BaseDetailWithErrorWhenParentIDIsValidAndChildIsNotValid(); 
         
    }
}

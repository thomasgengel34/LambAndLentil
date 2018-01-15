using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TParent = LambAndLentil.Domain.Entities.ShoppingList;
using TChild= LambAndLentil.Domain.Entities.Plan;


namespace LambAndLentil.Test.IAttachDetachControllerTests.ShoppingList
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldAttachPlanAndReturn: BaseControllerShouldAttachXAndReturn<TParent,TChild>
    {
        
       
        [TestMethod]
        public void DetailWithSuccessWhenParentIDIsValidAndChildIsValid() => BaseDetailWithSuccessWhenParentIDIsValidAndChildIsValid();
         
        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParent(); 

        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNotForAnExistingMenu() => BaseIndexWithErrorWhenParentIDIsNotForAnExistingIngredient();

         
        [TestMethod]
        public void DetailWithErrorWhenParentIDIsValidAndChildIsNotValid() => BaseDetailWithErrorWhenParentIDIsValidAndChildIsNotValid(); 

       
    }
}

using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TParent = LambAndLentil.Domain.Entities.Person;
using TChild= LambAndLentil.Domain.Entities.Recipe;


namespace LambAndLentil.Test.IAttachDetachControllerTests.Person
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldAttachRecipeAndReturn: BaseControllerShouldAttachXAndReturn<TParent,TChild>
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

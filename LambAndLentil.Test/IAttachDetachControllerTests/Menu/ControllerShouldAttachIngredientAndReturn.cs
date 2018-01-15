using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MenuType = LambAndLentil.Domain.Entities.Menu;


namespace LambAndLentil.Test.IAttachDetachControllerTests.Menu
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldAttachIngredientAndReturn: BaseControllerShouldAttachXAndReturn<MenuType,MenuType>
    {
        
        [Ignore]
        [TestMethod]
        public void DetailWithSuccessWhenParentIDIsValidAndChildIsValid() => BaseDetailWithSuccessWhenParentIDIsValidAndChildIsValid();
         
        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParent(); 

        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNotForAnExistingMenu() => BaseIndexWithErrorWhenParentIDIsNotForAnExistingIngredient();

         
        [TestMethod]
        public void DetailWithErrorWhenParentIDIsValidAndChildIsNotValid() => BaseDetailWithErrorWhenParentIDIsValidAndChildIsNotValid(); 

        [Ignore]
        [TestMethod]
        public void  DetailWithWarningWhenParentIDIsValidAndChildIstValidAndOrderNumberIsNegative()
        { // simply add such an child onto the end of the child-list and tell the user that is what happened
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void  DetailWithWarningWhenParentIDIsValidAndChildIstValidAndOrderNumberIsInUse()
        { // simply add such an child onto the end of the child-list and tell the user that is what happened
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void  DetailWithSuccessWhenParentIDIsValidAndChildIsValidAndOrderNumberIsGreaterThanTheNumberOfElementsWhenAttaching()
        { // simply add such an child onto the end of the child-list and tell the user that is what happened
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }
    }
}

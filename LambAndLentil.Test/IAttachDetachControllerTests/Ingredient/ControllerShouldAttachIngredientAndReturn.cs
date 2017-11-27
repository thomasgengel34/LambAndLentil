using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests; 
 using IngredientType =LambAndLentil.Domain.Entities.Ingredient;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Ingredient 
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldAttachIngredientAndReturn: BaseTest<IngredientType,IngredientType> 
    {
        [TestMethod]
        public void DetailWithSuccessWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSupplied() => BaseDetailWithSuccessWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSupplied();


        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNull() => BaseReturnsIndexWithWarningWithNullParent();



        [TestMethod]
        public void IndexWithErrorWhenParentIDIsNotForAnExistingIngredient() =>
            BaseIndexWithErrorWhenParentIDIsNotForAnExistingIngredient(); 

        
        [TestMethod]
        public void DetailWithErrorWhenParentIDIsValidAndChildIsNotValid()=> BaseDetailWithErrorWhenParentIDIsValidAndChildIsNotValid();

        
        [TestMethod]
        public void DetailWithWarningWhenParentIDIsValidAndChildIstValidAndOrderNumberIsNegativeWhenDetaching() => BaseDetailWithWarningWhenParentIDIsValidAndChildIstValidAndOrderNumberIsNegativeWhenDetaching(); 

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
        public void  DetailWithWarningWhenParentIDIsValidAndChildIsValidAndOrderNumberIsGreaterThanTheNumberOfElements()
        { // simply add such an child onto the end of the child-list and tell the user that is what happened
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }
    }
}

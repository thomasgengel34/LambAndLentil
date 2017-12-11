﻿using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MenuType = LambAndLentil.Domain.Entities.Menu;
using RecipeType = LambAndLentil.Domain.Entities.Recipe;


namespace LambAndLentil.Test.IAttachDetachControllerTests.Menu
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldAttachRecipeAndReturn: BaseControllerShouldAttachXAndReturn<MenuType,RecipeType>
    {
        
        [Ignore]
        [TestMethod]
        public void DetailWithSuccessWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSupplied() => BaseDetailWithSuccessWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSupplied();
         
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
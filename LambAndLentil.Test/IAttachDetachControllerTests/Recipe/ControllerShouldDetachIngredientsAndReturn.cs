using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Recipe
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldDetachsAndReturn
    {
        [Ignore]
        [TestMethod]
        public void  DetailWithWarningWhenParentIDIsValidAndChildIsValidAndThereIsOneChildAttached()
        {   // warning that the last child child was detached
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void  DetailWithErrorWhenParentIDIsValidAndChildIsValidAndThereIsNoChildAttached()
        {    
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void  IndexWithErrorWhenParentIDIsNull()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void  IndexWithErrorWhenParentIDIsNotForAnExistingRecipe()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void  DetailWithErrorWhenParentIDIsValidAndChildIsNotFound()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void  DetailWithWarningWhenParentIDIsValidAndChildIstValidAndOrderNumberIsNegativeAndAnChildIsAttached()
        { // simply add such an child onto the end of the child-list and tell the user that is what happened
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void  DetailWithErrorWhenParentIDIsValidAndChildIstValidAndOrderNumberIsNegativeAndAnChildIsNotAttached()
        { //error: nothing was attached, so nothing could be detached
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void  DetailWithSuccessWhenParentIDIsValidAndChildIstValidAndOrderNumberIsInUse()
        {  
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void  DetailWithErrorWhenParentIDIsValidAndChildIstValidAndOrderNumberIsGreaterThanTheNumberOfElements()
        { // return error that the child does not exist
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.IAttachDetachControllerTests.Plan
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class ControllerShouldDetachPlanAndReturn
    {
        [Ignore]
        [TestMethod]
        public void  DetailWithWarningWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSuppliedAndThereIsOneChildAttached()
        {   // warning that the last child child was detached
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void  DetailWithErrorWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSuppliedAndThereIsNoChildAttached()
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
        public void  IndexWithErrorWhenParentIDIsNotForAnExistingPlan()
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

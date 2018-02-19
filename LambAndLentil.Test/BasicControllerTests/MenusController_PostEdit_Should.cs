using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace  LambAndLentil.Test.BaseControllerTests
{
    [Ignore]
    [TestClass]
    public class MenusController_PostEdit_Should
    {
        [TestMethod]
        public void ReturnIndexWithValidModelStateWithSuccessMessageWhenSaved()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnIndexWithInValidModelStateWithWarningMessageWhenSaved()
        {
             
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void NotSaveLogicallyInvalidModel()
        { 
            Assert.Fail();

        }

        [Ignore]
        [TestMethod]
        public void NotSaveModelFlaggedInvalidByDataAnnotation()
        {  // see https://msdn.microsoft.com/en-us/library/cc668224(v=vs.98).aspx

            // Arrange

            // Act

            // Assert
            Assert.Fail();

        }
    }
} 

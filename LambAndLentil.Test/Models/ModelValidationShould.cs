using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.Models
{
    [Ignore]
    [TestClass]
    public class ModelValidationShould
    {
        [TestMethod]
        public void ReturnTrueForAValidModel()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnFalseForAnInvalidModel()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnTrueForAnValidModelContainingAnIntWhereIntRequired()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnTrueForAValidModelContainingADateWhereDateRequired()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnFalseForAnInvalidModelContainingANonIntWhereIntRequired()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnFalseForAnInvalidModelContainingANonDateWhereDateRequired()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}

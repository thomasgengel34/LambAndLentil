using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Test.BasicControllerTests
{
    [Ignore]
    [TestClass]
    public class BasicControllerTest_Misc:BaseControllerTest<Ingredient>
    {
        [TestMethod]
        public void ShouldEditName()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void DoesNotEditID()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ShouldEditDescription()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void DoesNotEditCreationDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void DoesNotEditAddedByUser()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void CannotAlterModifiedByUserByHand()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void CannotAlterModifiedDateByHand()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}

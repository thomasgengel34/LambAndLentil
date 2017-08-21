using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace LambAndLentil.Test.Controllers
{
    [Ignore]
    [TestClass]
    public class IngredientsControllerTest_FilterShould
    {

        [TestMethod]
        public void FindIngredientsContainingOnionInASet()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsNotContainingOnionInASet()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsAddedBeforeACertainDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsAddedOnACertainDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsAddedAfterACertainDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsAddedInADateRange()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsAddedOutsideADateRange()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsAddedByOneUser()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsNotAddedByOneUser()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsAddedByASetOfUsers()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsNotAddedByASetOfUsers()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsAddedByAUserContainingOnion()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsAddedByAUserContainingNotOnion()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsNotAddedByAUserContainingOnion()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsNotAddedByAUserNotContainingOnion()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        public void FindIngredientsNotAddedByAUserNotContainingOnionAddedByACertainDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Ingredient\";
            int count = int.MaxValue;
            try
            {

                for (int i = count; i > count - 6; i--)
                {
                    File.Delete(string.Concat(path, i, ".txt"));
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
} 

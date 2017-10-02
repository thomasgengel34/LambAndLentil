using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace LambAndLentil.Test.Controllers
{
    [Ignore]
    [TestCategory("RecipesController")] 
    [TestClass]
    public class RecipesControllerTest_FilterShould
    {

        [TestMethod]
        public void FindRecipesContainingOnionInASet()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindRecipesNotContainingOnionInASet()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindRecipesAddedBeforeACertainDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindRecipesAddedOnACertainDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindRecipesAddedAfterACertainDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindRecipesAddedInADateRange()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindRecipesAddedOutsideADateRange()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindRecipesAddedByOneUser()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindRecipesNotAddedByOneUser()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindRecipesAddedByASetOfUsers()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindRecipesNotAddedByASetOfUsers()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindRecipesAddedByAUserContainingOnion()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindRecipesAddedByAUserContainingNotOnion()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindRecipesNotAddedByAUserContainingOnion()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindRecipesNotAddedByAUserNotContainingOnion()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        public void FindRecipesNotAddedByAUserNotContainingOnionAddedByACertainDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Recipe\";

            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
} 

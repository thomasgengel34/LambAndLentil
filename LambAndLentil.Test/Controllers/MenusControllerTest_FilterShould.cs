using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace LambAndLentil.Test.Controllers
{
    [Ignore]
    [TestClass]
    public class MenusControllerTest_FilterShould
    {

        [TestMethod]
        public void FindMenusContainingOnionInASet()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindMenusNotContainingOnionInASet()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindMenusAddedBeforeACertainDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindMenusAddedOnACertainDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindMenusAddedAfterACertainDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindMenusAddedInADateRange()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindMenusAddedOutsideADateRange()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindMenusAddedByOneUser()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindMenusNotAddedByOneUser()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindMenusAddedByASetOfUsers()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindMenusNotAddedByASetOfUsers()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindMenusAddedByAUserContainingOnion()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindMenusAddedByAUserContainingNotOnion()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindMenusNotAddedByAUserContainingOnion()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindMenusNotAddedByAUserNotContainingOnion()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        public void FindMenusNotAddedByAUserNotContainingOnionAddedByACertainDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Menu\";
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

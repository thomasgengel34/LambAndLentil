﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace LambAndLentil.Test.BasicControllerTests
{
    [Ignore]
    [TestCategory("PlansController")] 
    [TestCategory("Filter")]
    [TestClass]
    public class PlansControllerTest_FilterShould:PlansController_Test_Should
    {

        [TestMethod]
        public void FindPlansContainingOnionInASet()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindPlansNotContainingOnionInASet()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindPlansAddedBeforeACertainDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindPlansAddedOnACertainDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindPlansAddedAfterACertainDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindPlansAddedInADateRange()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindPlansAddedOutsideADateRange()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindPlansAddedByOneUser()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindPlansNotAddedByOneUser()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindPlansAddedByASetOfUsers()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindPlansNotAddedByASetOfUsers()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindPlansAddedByAUserContainingOnion()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindPlansAddedByAUserContainingNotOnion()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindPlansNotAddedByAUserContainingOnion()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void FindPlansNotAddedByAUserNotContainingOnion()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        public void FindPlansNotAddedByAUserNotContainingOnionAddedByACertainDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Plan\";

            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
} 

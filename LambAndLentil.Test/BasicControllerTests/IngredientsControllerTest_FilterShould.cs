using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;

namespace LambAndLentil.Test.Controllers
{
    [Ignore]
    [TestClass]
    internal class IngredientsControllerTest_FilterShould
    {

        [TestMethod]
        private static void FindIngredientsContainingOnionInASet()
        { 
            Assert.Fail();
        }

        [TestMethod]
        private static void FindIngredientsNotContainingOnionInASet()
        { 
            Assert.Fail();
        }

        [TestMethod]
        private static void FindIngredientsAddedBeforeACertainDate()
        { 
            Assert.Fail();
        }

        [TestMethod]
        private static void FindIngredientsAddedOnACertainDate()
        { 
            Assert.Fail();
        }

        [TestMethod]
        private static void FindIngredientsAddedAfterACertainDate()
        { 
            Assert.Fail();
        }

        [TestMethod]
        private static void FindIngredientsAddedInADateRange()
        { 
            Assert.Fail();
        }

        [TestMethod]
        private static void FindIngredientsAddedOutsideADateRange()
        { 
            Assert.Fail();
        }

        [TestMethod]
        private static void FindIngredientsAddedByOneUser()
        { 
            Assert.Fail();
        }

        [TestMethod]
        private static void FindIngredientsNotAddedByOneUser()
        { 
            Assert.Fail();
        }

        [TestMethod]
        private static void FindIngredientsAddedByASetOfUsers()
        { 
            Assert.Fail();
        }

        [TestMethod]
        private static void FindIngredientsNotAddedByASetOfUsers()
        { 
            Assert.Fail();
        }

        [TestMethod]
        private static void FindIngredientsAddedByAUserContainingOnion()
        { 
            Assert.Fail();
        }

        [TestMethod]
        private static void FindIngredientsAddedByAUserContainingNotOnion()
        { 
            Assert.Fail();
        }

        [TestMethod]
        private static void FindIngredientsNotAddedByAUserContainingOnion()
        { 
            Assert.Fail();
        }

        [TestMethod]
        private static void FindIngredientsNotAddedByAUserNotContainingOnion()
        { 
            Assert.Fail();
        }

        private static  void FindIngredientsNotAddedByAUserNotContainingOnionAddedByACertainDate()
        { 
            Assert.Fail();
        }

        [ClassCleanup()]
        private static  void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Ingredient\";

            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
} 

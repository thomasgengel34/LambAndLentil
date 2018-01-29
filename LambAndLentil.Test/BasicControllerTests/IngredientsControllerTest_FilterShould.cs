using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;

namespace LambAndLentil.Test.Controllers
{
    [Ignore]
    [TestClass]
    public class IngredientsControllerTest_FilterShould
    {

        [TestMethod]
        public void FindIngredientsContainingOnionInASet()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsNotContainingOnionInASet()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsAddedBeforeACertainDate()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsAddedOnACertainDate()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsAddedAfterACertainDate()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsAddedInADateRange()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsAddedOutsideADateRange()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsAddedByOneUser()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsNotAddedByOneUser()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsAddedByASetOfUsers()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsNotAddedByASetOfUsers()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsAddedByAUserContainingOnion()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsAddedByAUserContainingNotOnion()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsNotAddedByAUserContainingOnion()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindIngredientsNotAddedByAUserNotContainingOnion()
        { 
            Assert.Fail();
        }

        public void FindIngredientsNotAddedByAUserNotContainingOnionAddedByACertainDate()
        { 
            Assert.Fail();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
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

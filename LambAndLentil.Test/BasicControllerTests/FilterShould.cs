using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;

namespace LambAndLentil.Test.Controllers
{
    [Ignore]
    [TestClass]
    public class  FilterShould
    { 
        [TestMethod]
        public void FindItemsContainingOnionInASet()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindItemsNotContainingOnionInASet()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindItemsAddedBeforeACertainDate()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindItemsAddedOnACertainDate()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindItemsAddedAfterACertainDate()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindItemsAddedInADateRange()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindItemsAddedOutsideADateRange()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindItemsAddedByOneUser()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindItemsNotAddedByOneUser()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindItemsAddedByASetOfUsers()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindItemsNotAddedByASetOfUsers()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindItemsAddedByAUserContainingOnion()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindItemsAddedByAUserContainingNotOnion()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindItemsNotAddedByAUserContainingOnion()
        { 
            Assert.Fail();
        }

        [TestMethod]
        public void FindItemsNotAddedByAUserNotContainingOnion()
        { 
            Assert.Fail();
        }

        public void FindItemsNotAddedByAUserNotContainingOnionAddedByACertainDate()
        { 
            Assert.Fail();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Menu\";

            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
} 

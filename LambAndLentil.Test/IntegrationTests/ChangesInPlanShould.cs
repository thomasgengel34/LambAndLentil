using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace LambAndLentil.Test.IntegrationTests
{
    /* Just do add, modify, remove for now for each condition */
    [Ignore]
    [TestClass]
    public class ChangesInPlanShould
    {
        /// <summary>
        ///   ShoppingList/Plan: plan added
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListWhenPlanAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        ///    ShoppingList/Plan: menu modified
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListWhenPlanModified()
        {
            Assert.Fail();
        }

        /// <summary>
        ///    ShoppingList/Menu: menu removed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListWhenPlanRemoved()
        {
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

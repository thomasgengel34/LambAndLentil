using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace LambAndLentil.Test.IntegrationTests
{
    [Ignore]
    [TestClass]
    public class ChangesInMenuShould
    {
        /* Just do add, modify, remove for now for each condition */

        /// <summary>
        ///   ShoppingList/Plan/Menu: menu added
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanWhenMenuAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        ///    ShoppingList/Plan/Menu: menu modified
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanWhenMenuModified()
        {
            Assert.Fail();
        }

        /// <summary>
        ///    ShoppingList/Plan/Menu: menu removed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanWhenMenuRemoved()
        {
            Assert.Fail();
        }

        /// <summary>
        ///   ShoppingList/Menu: menu added
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListMenuAdded()
        {
            Assert.Fail();
        }

        /// <summary>
        ///    ShoppingList/Menu: menu modified
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListWhenMenuModified()
        {
            Assert.Fail();
        }

        /// <summary>
        ///    ShoppingList/Menu: menu removed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListWhenMenuRemoved()
        {
            Assert.Fail();
        }

        /// <summary>
        ///   ShoppingList/Plan: menu added
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListPlanAdded()
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
        ///    ShoppingList/Plan: menu removed
        /// </summary>
        [TestMethod]
        public void ChangeShoppingListWhenPlanRemoved()
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
 

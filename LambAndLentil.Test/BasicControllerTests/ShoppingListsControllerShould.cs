using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicTests
{
    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("ShoppingListsController")]
    public class ShoppingListsControllerShould<T> : BaseControllerTest<T>
         where T : BaseEntity, IEntity, new()
    {
        internal static void TestRunner()
        { 
            if (typeof(T) == typeof(ShoppingList))
            {
                // TODO: write tests
            }
        }



        private static void CreateAPrintableShoppingList()
        {
            Assert.Fail();
        }

        private static void EditAPrintableShoppingList()
        {
            Assert.Fail();
        }

        private static void DisplayAPrintableShoppingList()
        {
            Assert.Fail();
        }

        private static void DeleteAPrintableShoppingList()
        {
            Assert.Fail();
        }

        private static void DownloadAPrintableShoppingList()
        {
            Assert.Fail();
        }
        private static void CreateACheckableShoppingList()
        {
            Assert.Fail();
        }

        private static void EditACheckableShoppingList()
        {
            Assert.Fail();
        }

        private static void DisplayACheckableShoppingList()
        {
            Assert.Fail();
        }

        private static void DeleteACheckableShoppingList()
        {
            Assert.Fail();
        }

        private static void DownloadACheckableShoppingList()
        {
            Assert.Fail();
        }
    }
}

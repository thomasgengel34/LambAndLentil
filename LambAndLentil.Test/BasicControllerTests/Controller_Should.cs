using System;
using System.Linq;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BaseControllerTests
{
    internal class  Controller_Should<T> : BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    {

        internal static void TestRunner()
        {
            HavePublicIntPageSizeProperty();
            ActuallyDeleteFromTheDatabase();
            CallRepositoryWithCorrectName();
        }

        private static void HavePublicIntPageSizeProperty()
        {
            // BaseController is abstract, so use property on inherited class

            var name = Type.GetType("LambAndLentil.UI.Controllers.IngredientsController, LambAndLentil.UI").GetProperty("PageSize").Name;
            var propertyType = Type.GetType("LambAndLentil.UI.Controllers.IngredientsController, LambAndLentil.UI").GetProperty("PageSize").PropertyType;

            Assert.AreEqual("PageSize", name);
            Assert.AreEqual("Int32", propertyType.Name);
        }

        private static void ActuallyDeleteFromTheDatabase()
        {
            SetUpForTests(out repo, out controller, out item);
            int initialCount = repo.Count();
            controller.DeleteConfirmed(item.ID);
            var deletedItem = repo.GetById(item.ID);

            Assert.AreEqual(initialCount - 1, repo.Count());
            Assert.IsNull(deletedItem);
        }


        private static void CallRepositoryWithCorrectName()
        {
            Type type = repo.GetType();
            string typeName = typeof(T).Name;
            string repoName = type.GenericTypeArguments[0].Name;


            Assert.AreEqual(typeName, repoName);
        }

    }
}

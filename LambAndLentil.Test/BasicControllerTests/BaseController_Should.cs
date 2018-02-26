using System;
using System.Linq;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BaseControllerTests
{
    internal class BaseController_Should<T> : BaseControllerTest<T>
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

            controller.DeleteConfirmed(item.ID);
            var deletedItem = (from m in repo.GetAll()
                               where m.ID == item.ID
                               select m).AsQueryable();

            Assert.AreEqual(0, deletedItem.Count());
            Assert.IsNull(deletedItem);
        }


       private static void CallRepositoryWithCorrectName()
        {
            Type type = repo.GetType();
            string name = typeof(T).Name;

            Assert.AreEqual(type.Name, name);
        }
         
    }
}

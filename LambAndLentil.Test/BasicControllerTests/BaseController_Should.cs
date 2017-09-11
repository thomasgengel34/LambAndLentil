using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    public class BaseController_Should
    {
        [TestMethod]
        public void ReturnNullWhenItemIsNotInRepoForGuardID()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnEmptyResultWhenItemIsInRepoForGuardID()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void HavePublicReadOnlyStringClassNameProperty()
        {
            // BaseController is abstract, so use property on inherited class
            // Arrange

            // Act
            var thing = Type.GetType("LambAndLentil.UI.Controllers.IngredientsController, LambAndLentil.UI").GetProperty("className") ;
            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void HavePublicIntPageSizeProperty()
        {
            // BaseController is abstract, so use property on inherited class
            // Arrange

            // Act        
            var name  = Type.GetType("LambAndLentil.UI.Controllers.IngredientsController, LambAndLentil.UI").GetProperty("PageSize").Name;
            var propertyType = Type.GetType("LambAndLentil.UI.Controllers.IngredientsController, LambAndLentil.UI").GetProperty("PageSize").PropertyType;

            // Assert  
            Assert.AreEqual("PageSize", name);
            Assert.AreEqual("Int32",propertyType.Name);
        }
    }
}

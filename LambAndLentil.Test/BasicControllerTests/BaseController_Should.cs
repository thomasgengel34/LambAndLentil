using System;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    public class BaseController_Should:BaseControllerTest<Ingredient>
    {
         
       

        public BaseController_Should() => Controller = new IngredientsController(Repo);



        [TestMethod]
        public void HavePublicReadOnlyStringClassNameProperty()
        {
            // BaseController is abstract, so use property on inherited class
            // Arrange
             
            // Act
            var property = Type.GetType("LambAndLentil.UI.Controllers.IngredientsController, LambAndLentil.UI").GetProperty("ClassName");
            var ListEntity= Type.GetType("LambAndLentil.UI.Controllers.IngredientsController, LambAndLentil.UI").GetProperties();
            var zz = property.DeclaringType.Attributes;
            // Assert 
            Assert.AreEqual("ClassName", property.Name);
            Assert.IsTrue(property.CanRead);
            Assert.IsFalse(property.CanWrite);
            Assert.AreEqual("String", property.PropertyType.Name);
            Assert.AreEqual( System.Reflection.TypeAttributes.Abstract|System.Reflection.TypeAttributes.Public|System.Reflection.TypeAttributes.BeforeFieldInit, zz );  
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

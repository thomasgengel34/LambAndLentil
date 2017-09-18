using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.UI.Models;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Tests.Controllers;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    public class BaseController_Should
    {
        static IRepository<Ingredient> Repo;
        static IngredientsController controller;

        public BaseController_Should()
        {
            Repo = new TestRepository<Ingredient>();
            controller = new IngredientsController_Index_Test().SetUpIngredientsController(Repo);
        }

        [TestMethod]
        public void ReturnNullWhenItemIsNotInRepoForGuardID()
        {
            // Arrange

            // Act
            ActionResult result = controller.GuardId(Repo, UI.UIControllerType.Ingredients,2);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ReturnEmptyResultWhenItemIsInRepoForGuardID()
        {
            // Arrange

            // Act
            ActionResult result = controller.GuardId(Repo, UI.UIControllerType.Ingredients, int.MaxValue);

            // Assert
            Assert.AreEqual(typeof(EmptyResult), result.GetType());
        }

        [TestMethod]
        public void HavePublicReadOnlyStringClassNameProperty()
        {
            // BaseController is abstract, so use property on inherited class
            // Arrange
             
            // Act
            var property = Type.GetType("LambAndLentil.UI.Controllers.IngredientsController, LambAndLentil.UI").GetProperty("ClassName");
            var list = Type.GetType("LambAndLentil.UI.Controllers.IngredientsController, LambAndLentil.UI").GetProperties();
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

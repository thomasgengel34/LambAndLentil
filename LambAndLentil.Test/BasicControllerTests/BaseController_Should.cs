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


using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.UI.Controllers;
using System;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestCategory("IngredientsController")]
    [TestCategory("")]
    [TestClass]
    public class IngredientsController__Should:IngredientsController_Test_Should
    {
        public IngredientsController__Should()
        {
          
        }

        [Ignore]
        [TestMethod]
        public void AllowUserToConfirmRequestAndCallConfirm()=> 
            Assert.Fail(); 

 
        [TestMethod]
        public void ReturnIndexWithWarningWhenIDIsNotFound()
        {
            // Arrange

            // Act
            AlertDecoratorResult adr= (AlertDecoratorResult)Controller.Index(50000);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            // Assert
        //    Assert.Fail();
            Assert.AreEqual("Ingredient was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Index", rtrr.RouteValues.Values.ElementAt(0).ToString());
           
        }


        [TestMethod]
        public void ReturnDetailsWhenIDIsFound() => BaseReturnDetailsWhenIDIsFound(Controller);

        [TestMethod] 
        public void AFoundIngredient()=> BaseAFoundEntity(Controller); 

        private void BaseAFoundEntity(IGenericController<Ingredient> controller) => throw new NotImplementedException();
    }
}

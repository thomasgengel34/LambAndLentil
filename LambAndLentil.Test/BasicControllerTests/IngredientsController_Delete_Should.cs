 
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestCategory("IngredientsController")]
    [TestCategory("Delete")]
    [TestClass]
    public class IngredientsController_Delete_Should:IngredientsController_Test_Should
    {
        public IngredientsController_Delete_Should()
        {
          
        }
        [Ignore]
        [TestMethod]
        public void AllowUserToConfirmDeleteRequestAndCallConfirmDelete()
        {
            Assert.Fail();
        }

 
        [TestMethod]
        public void ReturnIndexWithWarningWhenIDIsNotFound()
        {
            // Arrange

            // Act
            AlertDecoratorResult adr= (AlertDecoratorResult)Controller.Delete(50000);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            // Assert
        //    Assert.Fail();
            Assert.AreEqual("Ingredient was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Index", rtrr.RouteValues.Values.ElementAt(0).ToString());
           
        }


        [TestMethod]
        public void ReturnDetailsWhenIDIsFound()
        {
            BaseReturnDetailsWhenIDIsFound(Controller);
        }

        [TestMethod] 
        public void DeleteAFoundIngredient()
        {
            BaseDeleteAFoundEntity(Controller);
        } 
    }
}

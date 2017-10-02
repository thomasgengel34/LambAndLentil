 
using LambAndLentil.Domain.Entities;
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
            AlertDecoratorResult adr= (AlertDecoratorResult)controller.Delete(50000);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            // Assert
        //    Assert.Fail();
            Assert.AreEqual("Ingredient was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Index", rtrr.RouteValues.Values.ElementAt(0).ToString());
           
        }


        [TestMethod]
        public void ReturnDetailsWhenIDIstFound()
        {
            // Arrange

            // Act
         //   AlertDecoratorResult adr= (AlertDecoratorResult)controller.Delete(int.MaxValue);
             ViewResult vr  = (ViewResult)controller.Delete(int.MaxValue);
          // = (ViewResult)adr.InnerResult;
            // Assert
            //  Assert.Fail();
            Assert.AreEqual("Details", vr.ViewName);
            //Assert.AreEqual("Here it is!", adr.Message);
            Assert.AreEqual(int.MaxValue,  ((Ingredient)vr.Model).ID);
                //  return View(UIViewType.Details.ToString(), item).WithSuccess("Here it is!"); 
        }
    }
}

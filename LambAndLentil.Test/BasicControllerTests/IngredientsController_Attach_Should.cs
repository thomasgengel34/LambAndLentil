using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Models;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [Ignore]
    [TestClass]
    public class IngredientsController_Attach_Should:IngredientsController_Test_Should
    {
        [TestMethod]
        public void ReturnsErrorWithUnknownRepository()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnsIndexWithWarningWithUnknownParentID()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnsIndexWithWarningWithNullParent()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownChildID()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnsDetailWithWarningWithNullChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild()
        {    // cannot attach an Ingredient to an Ingredient
             
            // Arrange
            //Ingredient ivm = new Ingredient();
            //ivm.ID = int.MaxValue;
            //ivm.Description = "test PersonControllerTest AssignAnIngredientToAPerson";
            //IRepository<Ingredient> ing_repo = new TestRepository<Ingredient>();
            //ing_repo.Add(ivm);

            //// Act
            //ActionResult ar = controller..AttachIngredient(int.MaxValue, ivm);
            //AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            //RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;
            //Assert.Fail();
            ////Assert
            //Assert.AreEqual("alert-success", adr.AlertClass);
            //Assert.AreEqual("Ingredient was Successfully Attached!", adr.Message);
            //Assert.AreEqual(int.MaxValue, rdr.RouteValues.ElementAt(0).Value);
            //Assert.AreEqual("Edit", rdr.RouteValues.ElementAt(1).Value.ToString());
            //Assert.AreEqual("Details", rdr.RouteValues.ElementAt(2).Value.ToString());
        }

        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}

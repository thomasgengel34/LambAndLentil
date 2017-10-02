using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using System.Web.Mvc;
using LambAndLentil.UI.Infrastructure.Alerts;
using System.Linq;

namespace LambAndLentil.Test.BasicControllerTests
{
    
    [TestClass]
    public class MenusController_Attach_Should:MenusController_Test_Should
    {
        [Ignore]
        [TestMethod]
        public void ReturnsErrorWithUnknownRepository()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnsIndexWithWarningWithUnknownParentID()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnsIndexWithWarningWithNullParent()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownChildID()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWithWarningWithNullChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild()
        { 
            // Arrange
            Menu menu = new Menu
            {
                ID = int.MaxValue,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };
            IRepository<Menu> mRepo = new TestRepository<Menu>();
            mRepo.Add(menu);
            Ingredient ingredient = new Ingredient
            {
                ID = 1492,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };

            // Act
            ActionResult ar = Controller.AttachIngredient(int.MaxValue, ingredient);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;
     
            //Assert
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("Ingredient was Successfully Attached!", adr.Message);
            Assert.AreEqual(int.MaxValue, rdr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual("Edit", rdr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual("Details", rdr.RouteValues.ElementAt(2).Value.ToString()); 
        }

        [Ignore]   // note yet implemented
        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidChild()
        { 
            // Arrange
            Menu menu = new Menu
            {
                ID = int.MaxValue,
                Description = "test ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidChild"
            };
            IRepository<Menu> mRepo = new TestRepository<Menu>();
            mRepo.Add(menu);
            Ingredient ingredient = new Ingredient
            {
                ID = 1493,
                Description = "test ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidChild"
            };

            // Act
            ActionResult ar = Controller.DetachIngredient(int.MaxValue, ingredient);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            //Assert
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("Ingredient was Successfully Dettached!", adr.Message);
            Assert.AreEqual(int.MaxValue, rdr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual("Edit", rdr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual("Details", rdr.RouteValues.ElementAt(2).Value.ToString());
        }
    }
}

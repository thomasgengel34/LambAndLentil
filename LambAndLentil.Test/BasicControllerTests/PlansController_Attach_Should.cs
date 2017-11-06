﻿using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestCategory("PlansController")]
    [TestCategory("Attach")]  
    [TestClass]
    public class PlansController_Attach_Should:PlansController_Test_Should
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
            Plan menu = new Plan
            {
                ID = int.MaxValue,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };
            IRepository<Plan> mRepo = new TestRepository<Plan>();
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

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
 
        [TestMethod]
        public void SuccessfullyAttachIngredientChild()
        {
            // Arrange
            Ingredient child = new Ingredient() { ID = 3000, Name = "SuccessfullyAttachIngredientChild" };
            TestRepository<Ingredient> IngredientRepo = new TestRepository<Ingredient>();
            IngredientRepo.Save(child);

            // Act
            Controller.AttachIngredient(Plan.ID, child);
            ReturnedPlan = Repo.GetById(Plan.ID);
            // Assert
            //  Assert.AreEqual("Default", Ingredient.Ingredients.Last().Name);
            Assert.AreEqual("SuccessfullyAttachIngredientChild", ReturnedPlan.Ingredients.Last().Name);
        }
         
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void SuccessfullyDetachFirstIngredientChild()
        {
            IGenericController<Plan> DetachController = new PlansController(Repo);
            BaseSuccessfullyDetachIngredientChild(Repo, Controller, DetachController, UIControllerType.Plans, 0); 
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachARangeOfIngredientChildren()
        { // RemoveRange
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachTheLastIngredientChild()
        { // RemoveAt
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachAllIngredientChildren()
        { // RemoveAll
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void SuccessfullyAttachRecipeChild()
        {

        }

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachRecipeChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void SuccessfullyAttachMenuChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachMenuChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}

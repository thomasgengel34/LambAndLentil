using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{


    [TestClass]
    [TestCategory("PlansController")]
    public class PlansController_Test_ClassPropertyChanges : PlansController_Test_Should
    {

     
        public Plan ReturnedPlan { get; set; }

        public PlansController_Test_ClassPropertyChanges()
        {
            Plan = new Plan { ID = 1000, Name = "Original Name" };
            Repo.Save(Plan);
        }

        [TestMethod]
        public void ShouldEditName()
        {
            // Arrange

            // Act
            Plan.Name = "Name is changed";
            Controller.PostEdit(Plan);
            ReturnedPlan = Repo.GetById(1000);

            // Assert
            Assert.AreEqual("Name is changed", ReturnedPlan.Name);
        }

        [TestMethod]
        public void EditID()
        {  // this actually creates a copy.  
            // Arrange

            // Act
            Plan.ID = 42;
            Controller.PostEdit(Plan);
            Plan returnedPlan = Repo.GetById(42);
            Plan originalPlan = Repo.GetById(1000);

            // Assert
            Assert.AreEqual(42, returnedPlan.ID);
            Assert.IsNotNull(originalPlan);
        }

        [Ignore]
        [TestMethod]
        public void ShouldEditDescription()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DoesNotEditCreationDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DoesNotEditAddedByUser()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void CannotAlterModifiedByUserByHand()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void CannotAlterModifiedDateByHand()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldAddPlanToPlans()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldRemovePlanFromPlans()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldAddRecipeToRecipesList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldRemoveRecipeFromRecipesList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldAddMenuToMenusList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldRemoveMenuFromMenusList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldEditPlansList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}

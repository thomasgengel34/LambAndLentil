using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Models;
using LambAndLentil.Tests.Controllers;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("PlansController")]
    public class PlansController_PlansGenericController_Should : PlansController_Test_Should
    {
        [TestMethod]
        public void InheritBaseControllerInPlan()
        {
            // Arrange

            // Act 
            Type type = Type.GetType("LambAndLentil.UI.Controllers.PlansController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(BaseController<Plan>)));
        }

        [TestMethod]
        public void InheritPlansGenericControllerInPlan()
        {
            // Arrange

            // Act
            Type type = Type.GetType("LambAndLentil.UI.Controllers.PlansController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(PlansGenericController<Plan>)));
        }

        [TestMethod]
        public void CallRepositoryInPlan()
        {
            // Arrange

            // Act
            Type type = Repo.GetType();
           string name=  type.GenericTypeArguments[0].Name;
            // Assert
            Assert.AreEqual("Plan", name);
        }
    }
}

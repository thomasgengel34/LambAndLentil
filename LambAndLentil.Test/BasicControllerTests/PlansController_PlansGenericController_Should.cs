using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("PlansController")]
    public class PlansController_PlansGenericController_Should : PlansController_Test_Should
    {
        [TestMethod]
        public void InheritBaseControllerInPlan()
        { 
            Type type = Type.GetType("LambAndLentil.UI.Controllers.PlansController, LambAndLentil.UI", true);
          
            Assert.IsTrue(type.IsSubclassOf(typeof(BaseController<Plan>)));
        }

        [TestMethod]
        public void InheritBaseAttachDetachControllerInPlan()
        {
             
            Type type = Type.GetType("LambAndLentil.UI.Controllers.PlansController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(BaseAttachDetachController<Plan>)));
        }

        [TestMethod]
        public void CallRepositoryInPlan()
        { 
            Type type = Repo.GetType();
           string name=  type.GenericTypeArguments[0].Name;
          
            Assert.AreEqual("Plan", name);
        }
    }
}

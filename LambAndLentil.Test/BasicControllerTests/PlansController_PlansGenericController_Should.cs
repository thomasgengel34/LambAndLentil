using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LambAndLentil.Test.BaseControllerTests
{

    [TestClass]
    [TestCategory("PlansController")]
    public class PlansController_PlansGenericController_Should : PlansController_Test_Should
    { 

        [TestMethod]
        public void CallRepositoryInPlan()
        { 
            Type type = Repo.GetType();
           string name=  type.GenericTypeArguments[0].Name;
          
            Assert.AreEqual("Plan", name);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Models; 
using LambAndLentil.Tests.Controllers;
using LambAndLentil.Domain.Entities;

namespace  LambAndLentil.Test.BaseControllerTests
{

    [TestClass]
    [TestCategory("MenusController")]
    public class MenusController_MenusGenericController_Should : MenusController_Test_Should
    { 

        [TestMethod]
        public void CallRepositoryInMenu()
        { 
            Type type = Repo.GetType();
           string name=  type.GenericTypeArguments[0].Name;
            // Assert
            Assert.AreEqual("Menu", name);
        }
    }
}

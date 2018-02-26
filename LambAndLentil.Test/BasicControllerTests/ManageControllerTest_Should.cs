using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;

namespace LambAndLentil.Tests.Controllers
{
    [TestClass]
    [TestCategory("ManageController")]
    internal class ManageControllerTest_Should
    {

        [TestMethod]
        private static void  InheritsFromController()
        { 
            var testController = new ManageController();
             
            Type baseType = typeof(IController);
            bool isBase = baseType.IsInstanceOfType(testController);
             
            Assert.AreEqual(isBase, true);
        }

        [TestMethod]
        private static void ManageCtr_IsPublic()
        { 
            var controller = new ManageController();
             
            Type type =  controller.GetType();
            bool isPublic = type.IsPublic;
             
            Assert.AreEqual(isPublic, true);
        } 
    }
}

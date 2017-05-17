using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil;
using LambAndLentil.UI.Controllers;

namespace LambAndLentil.Tests.Controllers
{ 
    [TestClass]
    [TestCategory("ManageController")]
    public class ManageControllerTest
    {

        [TestMethod]
        public void ManageCtr_InheritsFromBaseController()
        {
            // Arrange
            ManageController testController = new ManageController();

            // Act 
            Type baseType = typeof(IController);
            bool isBase = baseType.IsInstanceOfType(testController);

            // Assert 
            Assert.AreEqual(isBase, true);
        }

        [TestMethod]
        public void ManageCtr_IsPublic()
        {
            // Arrange
            ManageController testController = new ManageController();

            // Act
            Type type = testController.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        } 
    }
}

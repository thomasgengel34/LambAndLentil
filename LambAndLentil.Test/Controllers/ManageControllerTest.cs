﻿using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;

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

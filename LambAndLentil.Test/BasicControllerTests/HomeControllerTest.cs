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
    [TestCategory("HomeController")]
    public class HomeControllerTest
    {

        [TestMethod]
        public void InheritsFromBaseController()
        {
            // Arrange
            HomeController testHomeController = new HomeController();

            // Act 
            Type baseType = typeof(IController);
            bool isBase = baseType.IsInstanceOfType(testHomeController);

            // Assert 
            Assert.AreEqual(isBase, true);
        }

        [TestMethod]
        public void  IsPublic()
        {
            // Arrange
            HomeController testHomeController = new HomeController();

            // Act
            Type type = testHomeController.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        }

        [TestMethod]
        public void  Index()
        {
            // Arrange
            HomeController Controller = new HomeController();

            // Act
            ViewResult result = Controller.Index as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void  About()
        {
            // Arrange
            HomeController Controller = new HomeController();

            // Act
            ViewResult result = Controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result); 
        }
    }
}

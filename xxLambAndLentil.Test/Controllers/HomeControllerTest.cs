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
    public class HomeControllerTest
    {

        [TestMethod]
        public void HomeControllerInheritsFromBaseController()
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
        public void HomeControllerIsPublic()
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
        public void HomeIndex()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void HomeAbout()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result); 
        }
    }
}

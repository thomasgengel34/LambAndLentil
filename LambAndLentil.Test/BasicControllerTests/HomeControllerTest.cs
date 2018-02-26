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
   internal class HomeControllerTest
    {

        [TestMethod]
        private static void InheritsFromBaseController()
        { 
            HomeController testHomeController = new HomeController();
             
            Type baseType = typeof(IController);
            bool isBase = baseType.IsInstanceOfType(testHomeController);
             
            Assert.AreEqual(isBase, true);
        }

        [TestMethod]
        private static void  IsPublic()
        { 
            HomeController controller = new HomeController();
             
            Type type =  controller.GetType();
            bool isPublic = type.IsPublic;
 
            Assert.AreEqual(isPublic, true);
        }

        [TestMethod]
        private static void  Index()
        { 
            HomeController controller = new HomeController();
 
            ViewResult result = (ViewResult)controller.Index();
             
            Assert.IsNotNull(result);
        }

        [TestMethod]
        private static void  About()
        { 
            HomeController controller = new HomeController();
             
            ViewResult result = controller.About() as ViewResult;
 
            Assert.IsNotNull(result); 
        }
    }
}

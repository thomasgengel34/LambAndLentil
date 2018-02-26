using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil;
using LambAndLentil.UI.Controllers; 
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using System.Collections;
using LambAndLentil.UI.Models;

namespace LambAndLentil.Tests.Controllers
{ 
    [TestClass]
    [TestCategory("NavController")]
    public class NavControllerTest
    { 

        [TestMethod]
        public void NavControllerInheritsFromBaseController()
        { 
            NavController testController = new NavController();
             
            Type baseType = typeof(IController);
            bool isBase = baseType.IsInstanceOfType(testController);
             
            Assert.AreEqual(isBase, true);
        }

        [TestMethod]
        public void NavCtr_IsPublic()
        { 
            NavController testController = new NavController();
             
            Type type = testController.GetType();
            bool isPublic = type.IsPublic;
             
            Assert.AreEqual(isPublic, true);
        } 
    }
}

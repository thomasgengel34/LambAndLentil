using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory(" IngredientsController")]
    public class IngredientsController_Test_Misc:IngredientsController_Test_Should
    {
        

        [TestMethod]
        public void InheritFromBaseControllerCorrectlyPageSizeRight()
        { 
            Controller.PageSize = 4;

            var type = typeof(IngredientsController);
            var DoesDisposeExist = type.GetMethod("Dispose");

            // Assert  
            Assert.AreEqual(4, Controller.PageSize);
        }



        [TestMethod]
        public void InheritFromBaseControllerCorrectlyDisposeExists()
        { 
            Controller.PageSize = 4;

            var type = typeof(IngredientsController);
            var DoesDisposeExist = type.GetMethod("Dispose");

            // Assert  
            Assert.IsNotNull(DoesDisposeExist);
        }

        [TestMethod]
        public void BePublic()
        { 
            Type type = Controller.GetType();
            bool isPublic = type.IsPublic;
             
            Assert.AreEqual(isPublic, true); 
        }





        [TestMethod]
        public void GetTheClassNameCorrect() => Assert.AreEqual("LambAndLentil.UI.Controllers.IngredientsController", Controller.ToString());
    }
}

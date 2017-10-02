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

            // Arrange
        
            // Act 
            controller.PageSize = 4;

            var type = typeof(IngredientsController);
            var DoesDisposeExist = type.GetMethod("Dispose");

            // Assert  
            Assert.AreEqual(4, controller.PageSize);
        }



        [TestMethod]
        public void InheritFromBaseControllerCorrectlyDisposeExists()
        {

            // Arrange
           
            // Act 
            controller.PageSize = 4;

            var type = typeof(IngredientsController);
            var DoesDisposeExist = type.GetMethod("Dispose");

            // Assert  
            Assert.IsNotNull(DoesDisposeExist);
        }

        [TestMethod]
        public void BePublic()
        {
            // Arrange
         

            // Act
            Type type = controller.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
           // Assert.Fail();
        }

     
 

        
        [TestMethod]
        public void GetTheClassNameCorrect()
        {
            // Arrange

            // Act


            // Assert 
           Assert.AreEqual("LambAndLentil.UI.Controllers.IngredientsController", IngredientsController_Test_Should.controller.ToString()); 
        }

        private class FakeRepository : TestRepository<Ingredient> { }


        [TestMethod]
        [ExpectedException(typeof(Exception), "Fake Repostory")]
        public void ReturnsErrorWithUnknownRepository()
        {
            // Arrange
            FakeRepository fakeRepo = new FakeRepository();
            IngredientsController fcontroller = new IngredientsController(fakeRepo);
            // Act
            ActionResult ar = fcontroller.BaseAttach(fakeRepo, int.MaxValue, new Ingredient());
            // Assert

        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using Moq;
using LambAndLentil.Domain.Abstract;

namespace LambAndLentil.Tests.Controllers
{
    [TestClass]
    public class PersonsControllerTest

    {
        [TestMethod]
        public void PersonsControllerInheritsFromBaseControllerCorrectly()
        {

            // Arrange
            PersonsController controller = SetUpSimpleController();
            // Act 
            controller.PageSize = 4;
    
            var type = typeof(PersonsController);
            var DoesDisposeExist = type.GetMethod("Dispose");

            // Assert 
           Assert.AreEqual(4, controller.PageSize);
            Assert.IsNotNull(DoesDisposeExist); 
        }

        [TestMethod]
        public void PersonsControllerIsPublic()
        {
            // Arrange
            PersonsController testController = SetUpSimpleController();

            // Act
            Type type = testController.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        }


        private PersonsController SetUpSimpleController()
        {
            // - create the mock repository
            Mock<IPersonRepository> mock = new Mock<IPersonRepository>();


            // Arrange - create a controller
            PersonsController controller = new PersonsController(mock.Object );
            // controller.PageSize = 3;

            return controller;
        }
    }
}

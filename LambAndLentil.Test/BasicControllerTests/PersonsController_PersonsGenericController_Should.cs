using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Models; 
using LambAndLentil.Tests.Controllers;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("PersonsController")]
    public class PersonsController_PersonsGenericController_Should : PersonsController_Test_Should
    {
        [TestMethod]
        public void InheritBaseControllerInPersonVM()
        {
            // Arrange

            // Act 
            Type type = Type.GetType("LambAndLentil.UI.Controllers.PersonsController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(BaseController<PersonVM>)));
        }

        [TestMethod]
        public void InheritPersonsGenericControllerInPersonVM()
        {
            // Arrange

            // Act
            Type type = Type.GetType("LambAndLentil.UI.Controllers.PersonsController, LambAndLentil.UI", true);
            // Assert
            Assert.IsTrue(type.IsSubclassOf(typeof(PersonsGenericController<PersonVM>)));
        }

        [TestMethod]
        public void CallRepositoryInPersonVM()
        {
            // Arrange

            // Act
            Type type = Repo.GetType();
           string name=  type.GenericTypeArguments[0].Name;
            // Assert
            Assert.AreEqual("PersonVM", name);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Models; 
using LambAndLentil.Tests.Controllers;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("PersonsController")]
    public class PersonsController_PersonsGenericController_Should : PersonsController_Test_Should
    {
        [TestMethod]
        public void InheritBaseControllerInPerson()
        { 
            Type type = Type.GetType("LambAndLentil.UI.Controllers.PersonsController, LambAndLentil.UI", true);
           
            Assert.IsTrue(type.IsSubclassOf(typeof(BaseController<Person>)));
        }

        [TestMethod]
        public void InheritBaseAttachDetachControllerInPerson()
        { 
            Type type = Type.GetType("LambAndLentil.UI.Controllers.PersonsController, LambAndLentil.UI", true);
            
            Assert.IsTrue(type.IsSubclassOf(typeof(BaseAttachDetachController<Person>)));
        }

        [TestMethod]
        public void CallRepositoryInPerson()
        { 
            Type type = Repo.GetType();
           string name=  type.GenericTypeArguments[0].Name;
          
            Assert.AreEqual("Person", name);
        }
    }
}

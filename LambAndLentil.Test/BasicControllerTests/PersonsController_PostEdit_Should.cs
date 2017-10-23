using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestCategory("PersonsController")]
    [TestCategory("PostEdit")] 
    [TestClass]
    public class PersonsController_PostEdit_Should:PersonsController_Test_Should
    {
        [Ignore]
        [TestMethod]
        public void ReturnIndexWithValidModelStateWithSuccessMessageWhenSaved()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
        [Ignore]
        [TestMethod]
        public void ReturnIndexWithInValidModelStateWithWarningMessageWhenSaved()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

      
        [TestMethod] 
        public void CanEditPerson2()
        {
            // Arrange
            Person person = new Person
            {
                ID = 1,
                FirstName = "test PersonControllerTest.CanEditPerson",
                LastName = "",
                Description = "test PersonControllerTest.CanEditPerson"
            };
            Repo.Save(person);

            // Act 
            person.FirstName = "Name has been changed";
            person.LastName = ""; 
           ActionResult view1 = Controller.PostEdit(person);

            Person returnedPerson = Repo.GetById(person.ID);


            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual("Name has been changed ", returnedPerson.Name);
            //Assert.AreEqual(person.Description, returnedPersonListEntity.Description);
            //Assert.AreEqual(person.CreationDate, returnedPersonListEntity.CreationDate);
        }

        [Ignore]
        [TestMethod]
        public void NotSaveLogicallyInvalidModel()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();

        }

        [Ignore]
        [TestMethod]
        public void NotSaveModelFlaggedInvalidByDataAnnotation()
        {  // see https://msdn.microsoft.com/en-us/library/cc668224(v=vs.98).aspx

            // Arrange

            // Act

            // Assert
            Assert.Fail();

        }
    }
} 

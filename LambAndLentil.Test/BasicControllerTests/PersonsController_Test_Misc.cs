using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("PersonsController")]
    [TestCategory("Misc")]
    public class PersonsController_Test_Misc : PersonsController_Test_Should
    {
        [TestMethod]
        public void InheritsFromBaseControllerCorrectly()
        {
            // Arrange

            // Act 
            Controller.PageSize = 4;

            var type = typeof(PersonsController);
            var DoesDisposeExist = type.GetMethod("Dispose");

            // Assert 
            Assert.AreEqual(4, Controller.PageSize);
            Assert.IsNotNull(DoesDisposeExist);
        }

        [TestMethod]
        public void IsPublic()
        {
            // Arrange 

            // Act
            Type type = Controller.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        }
           

        [TestMethod]
        public void PostEditPersonsFullName()
        {
            // Arrange
            Person person = new Person { ID = 1000, FirstName = "Jon", LastName = "Johns", Description = "PostEditPersonsFullName" };
            Repo.Add(person);

            // Act
            person.FirstName = "Reynard";
            person.LastName = "Finkelstein";
            Controller.PostEdit(person);
            Repo.Add(person);
            Person newPerson = Repo.GetById(1000);
            //Assert
            Assert.AreEqual("Reynard Finkelstein", person.Name);
        }

        [TestMethod]
        public void PostEditFirstName()
        {
            // Arrange
            Person person = new Person { ID = 1001, FirstName = "Jon", LastName = "Johns", Description = "PostEditPersonsFirstName" };
            Repo.Add(person);

            // Act
            person.FirstName = "Reynard"; 
            Controller.PostEdit(person);
            Repo.Add(person);
            Person newPerson = Repo.GetById(1000);
            //Assert
            Assert.AreEqual("Reynard Johns", person.Name);
        }
  

        [TestMethod]
        public void PostEditLastName()
        {
            // Arrange
            Person person = new Person { ID = 1001, FirstName = "Jon", LastName = "Johns", Description = "PostEditPersonsLastName" };
            Repo.Add(person);

            // Act
            person.LastName = "Luc";
            Controller.PostEdit(person);
            Repo.Add(person);
            Person newPerson = Repo.GetById(1000);
            //Assert
            Assert.AreEqual("Jon Luc", person.Name);
        }

   


        [TestMethod]
        public void CreateReturnsNonNull()
        {
            // Arrange 

            // Act
            ViewResult result = Controller.Create(UIViewType.Create) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        } 

        [Ignore]
        [TestMethod]
        public void UnAssignAnIngredientFromAPerson()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("Copy")]
        [TestMethod]
        public void CopyModifySaveWithANewName()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void CorrectPersonsAreBoundInEdit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetTheClassNameCorrect()
        {
            // Arrange

            // Act


            // Assert
            //  Assert.Fail();
            Assert.AreEqual("LambAndLentil.UI.Controllers.PersonsController", PersonsController_Test_Should.Controller.ToString());
        } 
    }
}

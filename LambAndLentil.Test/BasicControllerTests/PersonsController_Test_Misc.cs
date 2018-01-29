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
        public void PostEditPersonsFullName()
        {
            // Arrange
            Person person = new Person { ID = 1000, FirstName = "Jon", LastName = "Johns", Description = "PostEditPersonsFullName" };
            Repo.Save(person);

            // Act
            person.FirstName = "Reynard";
            person.LastName = "Finkelstein";
            Controller.PostEdit(person);
            Repo.Save(person);
            Person newPerson = Repo.GetById(1000);
            //Assert
            Assert.AreEqual("Reynard Finkelstein", person.Name);
        }

        [TestMethod]
        public void PostEditFirstName()
        {
            // Arrange
            Person person = new Person { ID = 1001, FirstName = "Jon", LastName = "Johns", Description = "PostEditPersonsFirstName" };
            Repo.Save(person);

            // Act
            person.FirstName = "Reynard"; 
            Controller.PostEdit(person);
            Repo.Save(person);
            Person newPerson = Repo.GetById(1000);
            //Assert
            Assert.AreEqual("Reynard Johns", person.Name);
        }
  

        [TestMethod]
        public void PostEditLastName()
        {
            // Arrange
            Person person = new Person { ID = 1001, FirstName = "Jon", LastName = "Johns", Description = "PostEditPersonsLastName" };
            Repo.Save(person);
             
            person.LastName = "Luc";
            Controller.PostEdit(person);
            Repo.Save(person);
            Person newPerson = Repo.GetById(1000);
           
            Assert.AreEqual("Jon Luc", person.Name);
        }

   


        [TestMethod]
        public void CreateReturnsNonNull()
        { 
            ViewResult result = Controller.Create() as ViewResult;
             
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

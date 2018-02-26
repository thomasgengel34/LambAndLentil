using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Web.Mvc;

namespace  LambAndLentil.Test.BaseControllerTests
{
    [TestClass]
    [TestCategory("PersonsController")]
    [TestCategory("Misc")]
   internal class PersonsController_Test_Misc : PersonsController_Test_Should
    {
        
        public void PostEditPersonsFullName()
        { 
            Person person = new Person { ID = 1000, FirstName = "Jon", LastName = "Johns", Description = "PostEditPersonsFullName" };
            repo.Save(person);
             
            person.FirstName = "Reynard";
            person.LastName = "Finkelstein";
            controller.PostEdit(person);
            repo.Save(person);
            Person newPerson = repo.GetById(1000);
            //Assert
            Assert.AreEqual("Reynard Finkelstein", person.Name);
        }

        [TestMethod]
        public void PostEditFirstName()
        {
            // Arrange
            Person person = new Person { ID = 1001, FirstName = "Jon", LastName = "Johns", Description = "PostEditPersonsFirstName" };
            repo.Save(person);

            // Act
            person.FirstName = "Reynard"; 
            controller.PostEdit(person);
            repo.Save(person);
            Person newPerson = repo.GetById(1000);
            //Assert
            Assert.AreEqual("Reynard Johns", person.Name);
        }
  

        [TestMethod]
        public void PostEditLastName()
        {
            // Arrange
            Person person = new Person { ID = 1001, FirstName = "Jon", LastName = "Johns", Description = "PostEditPersonsLastName" };
            repo.Save(person);
             
            person.LastName = "Luc";
            controller.PostEdit(person);
            repo.Save(person);
            Person newPerson = repo.GetById(1000);
           
            Assert.AreEqual("Jon Luc", person.Name);
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

       
    }
}

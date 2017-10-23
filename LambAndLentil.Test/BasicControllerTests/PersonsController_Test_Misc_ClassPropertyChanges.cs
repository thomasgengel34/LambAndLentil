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
    public class PersonsController_ClassPropertyChanges : PersonsController_Test_Should
    {
        public Person ReturnedPerson { get; set; }

        public PersonsController_ClassPropertyChanges()
        {
            Person = new Person { ID = 1000, Name = "Original Name" };
            Repo.Save(Person);
        }

        [TestMethod]
        public void ShouldEditFirstNameOnlyAndFullNameIsChangedButLastNameIsNot()
        {
            // Arrange
            Person.FirstName = "Changed";

            // Act 
            Controller.PostEdit(Person);
            ReturnedPerson = Repo.GetById(1000);

            // Assert
            Assert.AreEqual("Changed Created", ReturnedPerson.Name);
            // Name should always be fed from and equal to FullName
            Assert.AreEqual("Changed Created", ReturnedPerson.FullName);
            Assert.AreEqual("Changed", ReturnedPerson.FirstName);
            Assert.AreEqual("Created", ReturnedPerson.LastName);

        }

        [TestMethod]
        public void ShouldEditLastNameOnlyAndFullNameIsChangedBuFirstNameIsNot()
        {
            // Arrange
            Person.LastName = "Altered";

            // Act
            Controller.PostEdit(Person);
            ReturnedPerson = Repo.GetById(1000);

            // Assert
            Assert.AreEqual("Newly Altered", ReturnedPerson.Name);
            // Name should always be fed from and equal to FullName
            Assert.AreEqual("Newly Altered", ReturnedPerson.FullName);
            Assert.AreEqual("Newly", ReturnedPerson.FirstName);
            Assert.AreEqual("Altered", ReturnedPerson.LastName);

        }

        [TestMethod]
        public void ShouldEditFirstAndLastNamesAndFullNameIsChanged()
        {
            // Arrange
            Person.FirstName = "Changed";
            Person.LastName = "Altered";

            // Act
            Controller.PostEdit(Person);
            ReturnedPerson = Repo.GetById(1000);

            // Assert
            Assert.AreEqual("Changed Altered", ReturnedPerson.Name);
            // Name should always be fed from and equal to FullName
            Assert.AreEqual("Changed Altered", ReturnedPerson.FullName);
            Assert.AreEqual("Changed", ReturnedPerson.FirstName);
            Assert.AreEqual("Altered", ReturnedPerson.LastName);

        }

        [TestMethod]
        public void ShouldEditNameAndFirstNameAndLastNameChangeToEmptyStrings()
        {
            // Arrange
            Person.Name = "Changed";

            // Act
            Controller.PostEdit(Person);
            ReturnedPerson = Repo.GetById(1000);

            // Assert
            Assert.AreEqual("Changed", ReturnedPerson.Name);
            // Name should always be fed from and equal to FullName
            Assert.AreEqual("Changed", ReturnedPerson.FullName);
            Assert.AreEqual("", ReturnedPerson.FirstName);
            Assert.AreEqual("", ReturnedPerson.LastName);
        }

        [TestMethod]
        public void ShouldEditFullNameAndFirstNameAndLastNameChangeToEmptyStrings()
        {
            // Arrange
            Person.FullName = "Changed";

            // Act
            Controller.PostEdit(Person);
            ReturnedPerson = Repo.GetById(1000);

            // Assert
            Assert.AreEqual("Changed", ReturnedPerson.Name);
            // Name should always be fed from and equal to FullName
            Assert.AreEqual("Changed", ReturnedPerson.FullName);
            Assert.AreEqual("", ReturnedPerson.FirstName);
            Assert.AreEqual("", ReturnedPerson.LastName);
        }

        [TestMethod]
        public void ShouldNotEditNameDirectly()
        { 
            // Arrange

            // Act
            Person.Name = "Name is changed";
            Controller.PostEdit(Person);
            ReturnedPerson = Repo.GetById(1000);

            // Assert
            Assert.AreEqual("Newly Created", ReturnedPerson.Name);
        }

        [TestMethod]
        public void EditID()
        {  // this actually creates a copy.  
            // Arrange

            // Act
            Person.ID = 42;
            Controller.PostEdit(Person);
            Person returnedPerson = Repo.GetById(42);
            Person originalPerson = Repo.GetById(1000);

            // Assert
            Assert.AreEqual(42, returnedPerson.ID);
            Assert.IsNotNull(originalPerson);
        }

        [Ignore]
        [TestMethod]
        public void ShouldEditDescription()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DoesNotEditCreationDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DoesNotEditAddedByUser()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void CannotAlterModifiedByUserByHand()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void CannotAlterModifiedDateByHand()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldAddPersonToPersons()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldRemovePersonFromPersons()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldAddRecipeToRecipesList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldRemoveRecipeFromRecipesList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldAddMenuToMenusList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldRemoveMenuFromMenusList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldAddPlanToPlansList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldRemovePlanFromPlansList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldEditPersonsList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        public void ShouldEditMaxCalories()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        public void ShouldEditMinCalories()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        public void ShouldEditWeight()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        public void WeightCannotBeLessThanZero()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        public void WeightCannotBeMoreThanOneThousand()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        public void CanEditNoGarlic()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}

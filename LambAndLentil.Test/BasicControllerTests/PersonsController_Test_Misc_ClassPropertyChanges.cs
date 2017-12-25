using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("PersonsController")]
    [TestCategory("Misc")]
    public class PersonsController_ClassPropertyChanges : PersonsController_Test_Should
    {
        public Person Entity { get; set; }
        public Person ReturnedEntity { get; set; }

        public PersonsController_ClassPropertyChanges()
        {
           Entity = new Person {
               ID = 1000,
               Name = "Original Name",
               Description = "Description has changed" 
        };

            Repo.Save(Entity);
        }

        [TestMethod]
        public void ShouldEditFirstNameOnlyAndFullNameIsChangedButLastNameIsNot()
        {
            // Arrange
            Entity.FirstName = "Changed";

            // Act 
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(1000);

            // Assert
            Assert.AreEqual("Changed Created", ReturnedEntity.Name);
            // Name should always be fed from and equal to FullName
            Assert.AreEqual("Changed Created", ReturnedEntity.FullName);
            Assert.AreEqual("Changed", ReturnedEntity.FirstName);
            Assert.AreEqual("Created", ReturnedEntity.LastName);

        }

        [TestMethod]
        public void ShouldEditLastNameOnlyAndFullNameIsChangedBuFirstNameIsNot()
        {
            // Arrange
            Entity.LastName = "Altered";

            // Act
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(1000);

            // Assert
            Assert.AreEqual("Newly Altered", ReturnedEntity.Name);
            // Name should always be fed from and equal to FullName
            Assert.AreEqual("Newly Altered", ReturnedEntity.FullName);
            Assert.AreEqual("Newly", ReturnedEntity.FirstName);
            Assert.AreEqual("Altered", ReturnedEntity.LastName);

        }

        [TestMethod]
        public void ShouldEditFirstAndLastNamesAndFullNameIsChanged()
        {
            // Arrange
            Entity.FirstName = "Changed";
            Entity.LastName = "Altered";

            // Act
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(1000);

            // Assert
            Assert.AreEqual("Changed Altered", ReturnedEntity.Name);
            // Name should always be fed from and equal to FullName
            Assert.AreEqual("Changed Altered", ReturnedEntity.FullName);
            Assert.AreEqual("Changed", ReturnedEntity.FirstName);
            Assert.AreEqual("Altered", ReturnedEntity.LastName);

        }

        [TestMethod]
        public void ShouldEditNameAndFirstNameAndLastNameChangeToEmptyStrings()
        {
            // Arrange
            Entity.Name = "Changed";

            // Act
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(1000);

            // Assert
            Assert.AreEqual("Changed", ReturnedEntity.Name);
            // Name should always be fed from and equal to FullName
            Assert.AreEqual("Changed", ReturnedEntity.FullName);
            Assert.AreEqual("", ReturnedEntity.FirstName);
            Assert.AreEqual("", ReturnedEntity.LastName);
        }

        [TestMethod]
        public void ShouldEditFullNameAndFirstNameAndLastNameChangeToEmptyStrings()
        {
            // Arrange
            Entity.FullName = "Changed";

            // Act
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(1000);

            // Assert
            Assert.AreEqual("Changed", ReturnedEntity.Name);
            // Name should always be fed from and equal to FullName
            Assert.AreEqual("Changed", ReturnedEntity.FullName);
            Assert.AreEqual("", ReturnedEntity.FirstName);
            Assert.AreEqual("", ReturnedEntity.LastName);
        }
         
        [TestMethod]
        public void EditID()
        {  // this actually creates a copy.  
            // Arrange

            // Act
            Entity.ID = 42;
            Controller.PostEdit(Entity);
            Person returnedEntity = Repo.GetById(42);
            Person originalEntity = Repo.GetById(1000);

            // Assert
            Assert.AreEqual(42, returnedEntity.ID);
            Assert.IsNotNull(originalEntity);
        }

        [TestMethod]
        public void ShouldEditDescription()
        {
            // Arrange
            string changedDescription = "Description has changed";

            // Act
            Entity.Description = changedDescription;
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(Entity.ID);

            // Assert
            Assert.AreNotEqual(changedDescription, ReturnedEntity.AddedByUser);
        }

        [TestMethod]
        public void DoesNotEditCreationDate()
        {
            // Arrange
            DateTime dateTime = new DateTime(1776, 7, 4);
            Controller.PostEdit(Entity);

            // Act 
            Entity.CreationDate = dateTime;
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(Entity.ID);

            // Assert
            Assert.AreNotEqual(dateTime.Year, ReturnedEntity.CreationDate.Year);
        }

        [TestMethod]
        public void DoesNotEditAddedByUser()
        {
            // Arrange
            string user = "Abraham Lincoln";

            // Act
            Entity.AddedByUser = user;

            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(Entity.ID);

            // Assert
            Assert.AreNotEqual(user, ReturnedEntity.AddedByUser);
        }


        [TestMethod]
        public void CannotAlterModifiedByUserByHand()
        {
            // Arrange
            string user = "Abraham Lincoln";
            // Act
           Entity.ModifiedByUser = user;
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(Entity.ID);

            // Assert
            Assert.AreNotEqual(user, ReturnedEntity.ModifiedByUser);
        }

        [TestMethod]
        public void CannotAlterModifiedDateByHand()
        {
            // Arrange
            DateTime dateTime = new DateTime(1776, 7, 4);
            Entity.ModifiedDate = dateTime;

            // Act 
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(Entity.ID);

            // Assert
            Assert.AreNotEqual(dateTime.Year, ReturnedEntity.ModifiedDate.Year);
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
        public void ShouldDetachPersonFromPersons()
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
        public void ShouldDetachRecipeFromRecipesList()
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
        public void ShouldDetachFromMenusList()
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
        public void ShouldDetachPlanFromPlansList()
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

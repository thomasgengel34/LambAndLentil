using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("Class Property Changes")]
    public class BasicControllerTest_ClassPropertyChanges : IngredientsController_Test_Should
    {
        public Ingredient Entity { get; set; }
        public Ingredient ReturnedEntity { get; set; }

        public BasicControllerTest_ClassPropertyChanges()
        {
            Entity = new Ingredient { ID = 1000, Name = "Original Name" };
            Repo.Save(Entity);
        }

        [TestMethod]
        public void ShouldEditName()
        {
            // Arrange 


            // Act
            Entity.Name = "Changed";
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(1000);

            // Assert
            Assert.AreEqual("Changed", ReturnedEntity.Name);
        }

       
        [TestMethod]
        public void EditID()
        {  // this actually creates a copy.  
            // Arrange

            // Act
            Entity.ID = 42;
            Controller.PostEdit(Entity);
            Ingredient ReturnedEntity = Repo.GetById(42);
            Ingredient originalIngredient = Repo.GetById(1000);

            // Assert
            Assert.AreEqual(42, ReturnedEntity.ID);
            Assert.IsNotNull(originalIngredient);
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
            Ingredient ReturnedIngredient = Repo.GetById(Entity.ID);

            // Assert
            Assert.AreNotEqual(user, ReturnedIngredient.AddedByUser);
        }

        
        [TestMethod]
        public void CannotAlterModifiedByUserByHand()
        {
            // Arrange
            string user = "Abraham Lincoln";
           
            // Act
            Entity.ModifiedByUser = user;
         
            Controller.PostEdit(Entity);
            Ingredient ReturnedIngredient= Repo.GetById(Entity.ID);

            // Assert
            Assert.AreNotEqual(user, ReturnedIngredient.ModifiedByUser);
        }

        [TestMethod]
        public void CannotAlterModifiedDateByHand()
        {
            // Arrange
            DateTime dateTime = new DateTime(1776, 7, 4);
            Entity.ModifiedDate = dateTime;

            // Act 
            Controller.PostEdit(Entity);
            Ingredient ReturnedIngredient = Repo.GetById(Entity.ID);

            // Assert
            Assert.AreNotEqual(dateTime.Year, ReturnedIngredient.ModifiedDate.Year);
        }
    }
}

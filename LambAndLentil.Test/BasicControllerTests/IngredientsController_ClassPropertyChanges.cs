using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.UI.Controllers;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("IngredientsController")]
    [TestCategory("Edit")]
    public class IngredientsController_ClassPropertyChanges: IngredientsController_Test_Should
    {
        public Ingredient  Entity{ get; set; }
        public Ingredient ReturnedEntity{ get; set; }

        public IngredientsController_ClassPropertyChanges()
        {
            Entity = new Ingredient { ID = 1000, Name = "Original Name",Ingredients=new List<Ingredient>() };
            Repo.Save(Entity); 
        }

        [TestMethod]
        public void ShouldEditName()
        {
            // Arrange

            // Act
            Entity.Name = "Name is changed";
             Controller.PostEdit(Entity);
            ReturnedEntity= Repo.GetById(1000);

            // Assert
            Assert.AreEqual("Name is changed", ReturnedEntity.Name);
        }

        [TestMethod]
        public void EditID()
        {  // this actually creates a copy.  
            // Arrange

            // Act
            Entity.ID = 42;
            Controller.PostEdit(Entity);
             ReturnedEntity = Repo.GetById(42);
              Entity = Repo.GetById(1000);

            // Assert
            Assert.AreEqual(42, ReturnedEntity.ID);
            Assert.IsNotNull( Entity);
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
            Entity.ModifiedByUser = user ;
             Controller.PostEdit(Entity);
            ReturnedEntity= Repo.GetById(Entity.ID);

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
            ReturnedEntity= Repo.GetById(Entity.ID);

            // Assert
            Assert.AreNotEqual(dateTime.Year, ReturnedEntity.ModifiedDate.Year);
        }
         
        [TestMethod]
        public void ShouldEditUserGeneratedIngredientsList()
        {
            // Arrange
            Entity.IngredientsList = "Edited";

            // Act
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(Entity.ID);

            // Assert
            Assert.AreEqual("Edited", ReturnedEntity.IngredientsList);
        }

        [Ignore]
        [TestMethod]
        public void ShouldNotEditWebAPIGeneratedIngredientsList() =>
            // Arrange

            // Act 

            // Assert
            Assert.Fail();

        [TestMethod]
        public void ShouldAddIngredientToIngredients()
        {
            // Arrange
            int initialCount = Entity.Ingredients.Count;

            // Act
            Entity.Ingredients.Add(new Ingredient() { ID = 134, Name = "ShouldAddIngredientToIngredients" });
             Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(Entity.ID);

            // Assert
            Assert.AreEqual(initialCount + 1, Entity.Ingredients.Count);
            Assert.AreEqual("ShouldAddIngredientToIngredients", Entity.Ingredients[initialCount].Name);
        }

        [TestMethod]
        public void ShouldAddIngredientToIngredientsList() => BaseShouldAddIngredientToIngredientsList();
    }
}

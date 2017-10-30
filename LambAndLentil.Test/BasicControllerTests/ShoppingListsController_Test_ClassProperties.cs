using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    public class ShoppingListsController_Test_ClassProperties : ShoppingListsController_Test_Should
    {
        public ShoppingList Entity { get; set; }
        public ShoppingList ReturnedEntity { get; set; }

        public ShoppingListsController_Test_ClassProperties()
        {
            Entity = new ShoppingList { ID = 1000, Name = "Original Name" };
            Repo.Save(Entity);
        }


        [TestMethod]
        public void ShouldEditName()
        {
            // Arrange

            // Act
           Entity.Name = "Name is changed";
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(1000);

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
            ShoppingList returnedEntity = Repo.GetById(42);
            ShoppingList originalEntity = Repo.GetById(1000);

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

        [TestMethod]
        public void ShouldEditIngredientsList()
        {
            // Arrange
            Entity.IngredientsList = "Edited";

            // Act
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(Entity.ID);

            // Assert
            Assert.AreEqual("Edited", ReturnedEntity.IngredientsList);
        }

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
        public void ShouldAddIngredientToIngredientsList()
        {
            BaseShouldAddIngredientToIngredientsList(Repo, Entity, ReturnedEntity, Controller, UIControllerType.ShoppingLists);
        }

        [Ignore]
        [TestMethod]
        public void ShouldDetachIngredientFromIngredients()
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
        [TestCategory("Class Property")]
        public void ShouldDetachMenuFromMenusList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Class Property")]
        public void ShouldAddPlanToPlansList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Class Property")]
        public void ShouldDetachPlanFromPlansList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

       

        [Ignore]
        [TestMethod]
        [TestCategory("Class Property")]
        public void ShouldEditDate()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Class Property")]
        public void ShouldEditAuthor()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}

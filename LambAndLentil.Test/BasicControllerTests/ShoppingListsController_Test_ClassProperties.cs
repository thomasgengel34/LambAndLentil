using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    public class ShoppingListsController_Test_ClassProperties: ShoppingListsController_Test_Should
    {
        public ShoppingList ReturnedShoppingList { get; set; }

        public ShoppingListsController_Test_ClassProperties()
        {
            ShoppingList = new ShoppingList { ID = 1000, Name = "Original Name" };
            Repo.Save(ShoppingList);
        }
      
       
        [TestMethod]
        public void ShouldEditName()
        {
            // Arrange

            // Act
            ShoppingList.Name = "Name is changed";
            Controller.PostEdit(ShoppingList); 
            ReturnedShoppingList = Repo.GetById(1000);

            // Assert
            Assert.AreEqual("Name is changed", ReturnedShoppingList.Name); 
        }

        [TestMethod]
        public void EditID()
        {  // this actually creates a copy.  
            // Arrange

            // Act
            ShoppingList.ID = 42;
            Controller.PostEdit(ShoppingList);
            ShoppingList returnedShoppingList = Repo.GetById(42);
            ShoppingList originalShoppingList = Repo.GetById(1000);

            // Assert
            Assert.AreEqual(42, returnedShoppingList.ID);
            Assert.IsNotNull(originalShoppingList);
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
        public void ShouldAddIngredientToIngredients()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldRemoveIngredientFromIngredients()
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
        [TestCategory("Class Property")]
        public void ShouldRemoveMenuFromMenusList()
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
        public void ShouldRemovePlanFromPlansList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Class Property")]
        public void ShouldEditIngredientsList()
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

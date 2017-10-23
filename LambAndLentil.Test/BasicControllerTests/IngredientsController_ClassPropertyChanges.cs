using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("IngredientsController")]
    [TestCategory("Edit")]
    public class IngredientsController_ClassPropertyChanges: IngredientsController_Test_Should
    {
        public Ingredient ReturnedIngredient { get; set; }

        public IngredientsController_ClassPropertyChanges()
        {
            Ingredient = new Ingredient { ID = 1000, Name = "Original Name" };
            Repo.Save(Ingredient); 
        }

        [TestMethod]
        public void ShouldEditName()
        {
            // Arrange

            // Act
            Ingredient.Name = "Name is changed";
            Controller.PostEdit(Ingredient);
            ReturnedIngredient = Repo.GetById(1000);

            // Assert
            Assert.AreEqual("Name is changed", ReturnedIngredient.Name);
        }

        [TestMethod]
        public void EditID()
        {  // this actually creates a copy.  
            // Arrange

            // Act
            Ingredient.ID = 42;
            Controller.PostEdit(Ingredient);
            Ingredient returnedIngredient = Repo.GetById(42);
            Ingredient originalIngredient = Repo.GetById(1000);

            // Assert
            Assert.AreEqual(42, returnedIngredient.ID);
            Assert.IsNotNull(originalIngredient);
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
        public void ShouldAddRecipeToList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldRemoveRecipeFromList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldEditIngredientsList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}

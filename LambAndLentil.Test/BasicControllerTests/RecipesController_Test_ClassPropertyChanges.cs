using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("RecipesController")]
    public class RecipesController_Test_ClassPropertyChanges:RecipesController_Test_Should
    {
        public Recipe ReturnedRecipe { get; set; }

        public RecipesController_Test_ClassPropertyChanges()
        {
            Recipe = new Recipe { ID = 1000, Name = "Original Name" };
            Repo.Save(Recipe);
        }

        [TestMethod]
        public void ShouldEditName()
        {
            // Arrange

            // Act
            Recipe.Name = "Name is changed";
            Controller.PostEdit(Recipe);
            ReturnedRecipe = Repo.GetById(1000);

            // Assert
            Assert.AreEqual("Name is changed", ReturnedRecipe.Name);
        }

        [TestMethod]
        public void EditID()
        {  // this actually creates a copy.  
            // Arrange

            // Act
            Recipe.ID = 42;
            Controller.PostEdit(Recipe);
            Recipe returnedRecipe = Repo.GetById(42);
            Recipe originalRecipe = Repo.GetById(1000);

            // Assert
            Assert.AreEqual(42, returnedRecipe.ID);
            Assert.IsNotNull(originalRecipe);
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
        public void ShouldAddIngredientToIngredientsList()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldRemoveIngredientFromIngredientsList()
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


        [Ignore]
        [TestMethod]
        public void ShouldEditServings()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldEditMealType()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldEditCalories()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShouldCalsFromFat()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}

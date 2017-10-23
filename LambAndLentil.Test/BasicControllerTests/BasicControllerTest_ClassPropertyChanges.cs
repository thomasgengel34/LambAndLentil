using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("Class Property Changes")]
    public class BasicControllerTest_ClassPropertyChanges : IngredientsController_Test_Should
    {
        public Ingredient ingredient { get; set; }
        public Ingredient returnedIngredient { get; set; }

        public BasicControllerTest_ClassPropertyChanges()
        {
            ingredient = new Ingredient { ID = 1000, Name = "Original Name" };
            Repo.Save(ingredient);
        }

        [TestMethod]
        public void ShouldEditName()
        {
            // Arrange 


            // Act
            ingredient.Name = "Changed";
            Controller.PostEdit(ingredient);
            returnedIngredient = Repo.GetById(1000);

            // Assert
            Assert.AreEqual("Changed", returnedIngredient.Name);
        }

       
        [TestMethod]
        public void EditID()
        {  // this actually creates a copy.  
            // Arrange

            // Act
            ingredient.ID = 42;
            Controller.PostEdit(ingredient);
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
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Domain.Test.Entities
{
    [TestClass]
    [TestCategory("Ingredient Class")]
    public class IngredientClassShould
    {
        [TestMethod]
        public void HaveCorrectDefaultsInConstructor()
        {
            // Arrange
            Ingredient ingredient = new Ingredient();

            // Act
            // nothing to see here, just move along

            // Assert 
            Assert.IsNotNull(ingredient.CreationDate);
            Assert.IsNotNull(ingredient.ModifiedDate); 
            Assert.IsNotNull(ingredient.AddedByUser); 
            Assert.IsNotNull(ingredient.ModifiedByUser);
            Assert.AreEqual(ingredient.AddedByUser, ingredient.ModifiedByUser);
        }

        [TestMethod]
        public void  InheritFromBaseEntity()
        {
            // Arrange
            Ingredient ingredient = new Ingredient();

            // Act 
            Type baseType = typeof(BaseEntity);
            bool isBase = baseType.IsInstanceOfType(ingredient);

            // Assert  
            Assert.AreEqual(true, isBase);
        }

        [TestMethod]
        public void HaveBaseEntityPropertiesOnCreation()
        {
            // Arrange
            Ingredient ingredient = new Ingredient(new DateTime(2017, 06, 26));

            // Act - nothing

            // Assert
            Assert.AreEqual("Newly Created", ingredient.Name);
            Assert.AreEqual("not yet described", ingredient.Description);
            Assert.AreEqual("6/26/2017", ingredient.CreationDate.ToShortDateString());
        }

        [TestMethod]
        public void HaveOlderThanFortyYearCreationDateOKOnCreation()
        {
            // Arrange
            Ingredient ingredient = new Ingredient(new DateTime(1977, 06, 26));

            // Act - nothing

            // Assert 
            Assert.AreEqual("6/26/1977", ingredient.CreationDate.ToShortDateString());
        }

        [Ignore]
        [TestMethod]
        public void NotBeAbleToHaveIngredientChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void NotBeAbleToHaveRecipeChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void NotBeAbleToHaveMenuChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void NotBeAbleToHavePlanChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void NotBeAbleToHaveShoppingListChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void NotBeAbleToHavePersonChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}

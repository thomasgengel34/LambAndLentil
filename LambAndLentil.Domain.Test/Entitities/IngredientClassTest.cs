using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Domain.Test.Entities
{
    [TestClass]
    public class IngredientClass
    {
        [TestMethod]
        public void IngredientClass_HasCorrectDefaultsInConstructor()
        {
            // Arrange
            Ingredient ingredient = new Ingredient();
            // Act
            // nothing to see here, just move along
            // Assert
            Assert.AreEqual(ingredient.Maker, "Not Provided");
            Assert.AreEqual(ingredient.Brand, "Not Provided");
            Assert.AreEqual(ingredient.FoodGroup, "Default");
            Assert.AreEqual(ingredient.Category, "Default");
            Assert.IsNotNull(ingredient.CreationDate);
            Assert.IsNotNull(ingredient.ModifiedDate);
            Assert.AreEqual(ingredient.CreationDate.Day, ingredient.ModifiedDate.Day);
        }

        [TestMethod]
        public void IngredientClass_InheritsFromBaseEntity()
        {
            // Arrange
            Ingredient ingredient = new Ingredient();

            // Act 
            Type baseType = typeof(BaseEntity);
            bool isBase = baseType.IsInstanceOfType(ingredient);

            // Assert  
            Assert.AreEqual(true, isBase);
        }
    }
}

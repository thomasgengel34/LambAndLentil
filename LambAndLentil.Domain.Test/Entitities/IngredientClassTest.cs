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
            Assert.AreEqual("Not Provided",ingredient.Maker  );
            Assert.AreEqual("Not Provided",ingredient.Brand );
            Assert.AreEqual("Default", ingredient.FoodGroup );
            Assert.AreEqual("Default", ingredient.Category );
            Assert.IsNotNull(ingredient.CreationDate);
            Assert.IsNotNull(ingredient.ModifiedDate); 
            Assert.IsNotNull(ingredient.AddedByUser); 
            Assert.IsNotNull(ingredient.ModifiedByUser);
            Assert.AreEqual(ingredient.AddedByUser, ingredient.ModifiedByUser);
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

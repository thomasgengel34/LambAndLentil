using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq; 

namespace LambAndLentil.Domain.Test.Entities

{
    [TestClass]
    [TestCategory("Recipe")]
    public class RecipetClassShould {

        [TestMethod]
        public void HaveBaseEntityPropertiesOnCreation()
        {
            // Arrange
            Recipe recipe = new Recipe(new DateTime( 2017,06,26));

            // Act - nothing

            // Assert
            Assert.AreEqual("Newly Created", recipe.Name);
            Assert.AreEqual("not yet described", recipe.Description);
            Assert.AreEqual("6/26/2017", recipe.CreationDate.ToShortDateString()); 
        }

        [TestMethod]
        public void HaveClassPropertiesOnCreation()
        {
            // Arrange
            Recipe recipe = new Recipe(new DateTime(2017, 06, 26));

            // Act - nothing

            // Assert 
            Assert.AreEqual(0, recipe.Servings);
            Assert.AreEqual(recipe.MealType, MealType.Breakfast);
            Assert.IsNull(recipe.Calories);
            Assert.IsNull(recipe.CalsFromFat);

        }

        [TestMethod]
        public void HaveOlderThanFortyYearCreationDateOKOnCreation()
        {
            // Arrange
            Recipe recipe = new Recipe(new DateTime(1977, 06, 26));

            // Act - nothing

            // Assert 
            Assert.AreEqual("6/26/1977", recipe.CreationDate.ToShortDateString());
        }

 
    }
}

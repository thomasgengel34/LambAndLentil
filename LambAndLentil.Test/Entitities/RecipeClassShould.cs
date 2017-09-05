using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;

namespace LambAndLentil.Domain.Test.Entities

{
    [TestClass]
    [TestCategory("Recipe")]
    public class RecipetClassShould {

        static Recipe recipe;

        public RecipetClassShould()
        {
            recipe = new Recipe();
        }


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

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void BeAbleToHaveIngredientsChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = recipe.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Ingredients");

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void NotBeAbleToHaveRecipesChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = recipe.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Recipes");

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void NotBeAbleToHaveMenusChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = recipe.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Menus");

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void NotBeAbleToHavePlansChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = recipe.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Plans");

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void NotBeAbleToHaveShoppingListsChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = recipe.GetType().GetProperties();
            var result = props.Where(p => p.Name == "ShoppingLists");

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void NotBeAbleToHavePersonsChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = recipe.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Persons");

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }
}

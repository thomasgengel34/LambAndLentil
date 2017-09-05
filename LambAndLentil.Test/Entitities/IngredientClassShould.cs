using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace LambAndLentil.Domain.Test.Entities
{
    [TestClass]
    [TestCategory("Ingredient Class")]
    public class IngredientClassShould
    {
        static Ingredient ingredient { get; set; }
        static List<string> list;

        public IngredientClassShould()
        {
            ingredient = new Ingredient();
            list = new List<string>();
        }


        [TestMethod]
        public void HaveCorrectDefaultsInConstructor()
        {
            // Arrange


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
        public void InheritFromBaseEntity()
        {
            // Arrange
            ingredient = new Ingredient();

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

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void NotBeAbleToHaveIngredientsChild()
        {
            // Arrange


            // Act
            PropertyInfo[] props = ingredient.GetType().GetProperties();
            var result =  props.Where(p=>p.Name == "Ingredients");

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void NotBeAbleToHaveRecipesChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = ingredient.GetType().GetProperties();
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
            PropertyInfo[] props = ingredient.GetType().GetProperties();
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
            PropertyInfo[] props = ingredient.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Plans");

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void NotBeAbleToHaveShoppingListChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = ingredient.GetType().GetProperties();
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
            PropertyInfo[] props = ingredient.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Persons");

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }
}

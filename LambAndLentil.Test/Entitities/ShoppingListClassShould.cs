using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;
using System.Reflection;
using System.Linq;

namespace LambAndLentil.Domain.Test.Entities
{
    [TestClass]
    [TestCategory("ShoppingList Class")]
    public class ShoppingListClassShould
    {
        static ShoppingList shoppingList;

        public ShoppingListClassShould()
        {
            shoppingList = new ShoppingList();
        }


        [TestMethod]
        public void HaveCorrectDefaultsInConstructor()
        {
            // Arrange
            ShoppingList shoppingList = new ShoppingList();

            // Act
            // nothing to see here, just move along

            // Assert 
            Assert.IsNotNull(shoppingList.CreationDate);
            Assert.IsNotNull(shoppingList.ModifiedDate);
            Assert.IsNotNull(shoppingList.AddedByUser);
            Assert.IsNotNull(shoppingList.ModifiedByUser);
            Assert.AreEqual(shoppingList.AddedByUser, shoppingList.ModifiedByUser);
        }

        [TestMethod]
        public void InheritFromBaseEntity()
        {
            // Arrange
            ShoppingList shoppingList = new ShoppingList();

            // Act 
            Type baseType = typeof(BaseEntity);
            bool isBase = baseType.IsInstanceOfType(shoppingList);

            // Assert  
            Assert.AreEqual(true, isBase);
        }

        [TestMethod]
        public void HaveBaseEntityPropertiesOnCreation()
        {
            // Arrange
            ShoppingList shoppingList = new ShoppingList(new DateTime(2017, 06, 26));

            // Act - nothing

            // Assert
            Assert.AreEqual("Newly Created", shoppingList.Name);
            Assert.AreEqual("not yet described", shoppingList.Description);
            Assert.AreEqual("6/26/2017", shoppingList.CreationDate.ToShortDateString());
        }


        [TestMethod]
        public void HaveClassPropertiesOnCreation()
        {
            // Arrange
            ShoppingList shoppingList = new ShoppingList(new DateTime(2017, 06, 26));

            // Act - nothing

            // Assert 
            Assert.AreEqual(new DateTime(2017, 06, 26), shoppingList.Date);
            Assert.IsNull(shoppingList.Author);
        }

        [TestMethod]
        public void HaveOlderThanFortyYearCreationDateOKOnCreation()
        {
            // Arrange
            ShoppingList shoppingList = new ShoppingList(new DateTime(1977, 06, 26));

            // Act - nothing

            // Assert 
            Assert.AreEqual("6/26/1977", shoppingList.CreationDate.ToShortDateString());
        }


     [TestCategory("Class Child Test")]
        [TestMethod]
        public void BeAbleToHaveIngredientsChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = shoppingList.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Ingredients");

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void BeAbleToHaveRecipesChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = shoppingList.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Recipes");

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void BeAbleToHaveMenusChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = shoppingList.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Menus");

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void BeAbleToHavePlansChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = shoppingList.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Plans");

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void NotBeAbleToHaveShoppingListsChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = shoppingList.GetType().GetProperties();
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
            PropertyInfo[] props = shoppingList.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Persons");

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }
}

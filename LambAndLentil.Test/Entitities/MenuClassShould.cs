using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace LambAndLentil.Domain.Test.Entities
{
    [TestClass]
    [TestCategory("Menu Class")]
    public class MenuClassShould
    {
        static Menu menu { get; set; }
        static List<string> list;

        public MenuClassShould()
        {
           menu = new Menu();
            list = new List<string>();
        }

        [TestMethod]
        public void HaveCorrectDefaultsInConstructor()
        {
            // Arrange
            Menu menu = new Menu();

            // Act
            // nothing to see here, just move along

            // Assert 
            Assert.IsNotNull(menu.CreationDate);
            Assert.IsNotNull(menu.ModifiedDate); 
            Assert.IsNotNull(menu.AddedByUser); 
            Assert.IsNotNull(menu.ModifiedByUser);
            Assert.AreEqual(menu.AddedByUser, menu.ModifiedByUser);
        }

        [TestMethod]
        public void  InheritFromBaseEntity()
        {
            // Arrange
            Menu menu = new Menu();

            // Act 
            Type baseType = typeof(BaseEntity);
            bool isBase = baseType.IsInstanceOfType(menu);

            // Assert  
            Assert.AreEqual(true, isBase);
        }

        [TestMethod]
        public void HaveBaseEntityPropertiesOnCreation()
        {
            // Arrange
            Menu menu = new Menu(new DateTime(2017, 06, 26));

            // Act - nothing

            // Assert
            Assert.AreEqual("Newly Created", menu.Name);
            Assert.AreEqual("not yet described", menu.Description);
            Assert.AreEqual("6/26/2017", menu.CreationDate.ToShortDateString());
        }

        [TestMethod]
        public void HaveOlderThanFortyYearCreationDateOKOnCreation()
        {
            // Arrange
            Menu menu = new Menu(new DateTime(1977, 06, 26));

            // Act - nothing

            // Assert 
            Assert.AreEqual("6/26/1977", menu.CreationDate.ToShortDateString());
        }

        [TestMethod]
        public void HaveClassPropertiesOnCreation()
        {
            // Arrange
            Menu menu = new Menu(new DateTime(2017, 06, 26));

            // Act - nothing

            // Assert 
            
            Assert.AreEqual(menu.MealType, MealType.Breakfast);
            Assert.AreEqual(DayOfWeek.Sunday, menu.DayOfWeek); 
            Assert.AreEqual(0,menu.Diners);

        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void BeAbleToHaveIngredientsChild()
        {
            // Arrange
 
            // Act
            PropertyInfo[] props = menu.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Ingredients");

            // Assert
            Assert.AreEqual(1, result.Count());
             
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void  BeAbleToHaveRecipesChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props =menu.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Recipes");

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void NotBeAbleToHaveMenusChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = menu.GetType().GetProperties();
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
            PropertyInfo[] props = menu.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Plans");

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void NotBeAbleToHaveShoppingListsChild()
        {
            // Arrange
            PropertyInfo[] props = menu.GetType().GetProperties();
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
            PropertyInfo[] props = menu.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Persons");

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }
}

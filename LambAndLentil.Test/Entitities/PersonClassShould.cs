using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;
using System.Reflection;
using System.Linq;

namespace LambAndLentil.Domain.Test.Entities
{
    [TestClass]
    [TestCategory("Person Class")]
    public class PersonClassShould
    {
        static Person person; 

        public PersonClassShould()
        {
            person = new Person();
        }

        [TestMethod]
        public void HaveCorrectDefaultsInConstructor()
        {
            // Arrange
            Person person = new Person();

            // Act
            // nothing to see here, just move along

            // Assert 
            Assert.IsNotNull(person.CreationDate);
            Assert.IsNotNull(person.ModifiedDate); 
            Assert.IsNotNull(person.AddedByUser); 
            Assert.IsNotNull(person.ModifiedByUser);
            Assert.AreEqual(person.AddedByUser, person.ModifiedByUser);
        }

        [TestMethod]
        public void  InheritFromBaseEntity()
        {
            // Arrange
            Person person = new Person();

            // Act 
            Type baseType = typeof(BaseEntity);
            bool isBase = baseType.IsInstanceOfType(person);

            // Assert  
            Assert.AreEqual(true, isBase);
        }

        [TestMethod]
        public void HaveBaseEntityPropertiesOnCreation()
        {
            // Arrange
            Person person = new Person(new DateTime(2017, 06, 26));

            // Act - nothing

            // Assert
            Assert.AreEqual("Newly Created", person.Name);
            Assert.AreEqual("not yet described", person.Description);
            Assert.AreEqual("6/26/2017", person.CreationDate.ToShortDateString());
        }

        [TestMethod]
        public void HaveClassPropertiesOnCreation()
        {
            // Arrange
           Person person = new Person(new DateTime(2017, 06, 26));

            // Act - nothing

            // Assert 
            Assert.AreEqual(null, person.FirstName);
            Assert.AreEqual(null, person.LastName);
            Assert.AreEqual(0,person.Weight);
            Assert.AreEqual(0, person.MinCalories);
            Assert.AreEqual(0, person.MaxCalories);
            Assert.AreEqual(false, person.NoGarlic); 

        }

        [TestMethod]
        public void HaveOlderThanFortyYearCreationDateOKOnCreation()
        {
            // Arrange
            Person person = new Person(new DateTime(1977, 06, 26));

            // Act - nothing

            // Assert 
            Assert.AreEqual("6/26/1977", person.CreationDate.ToShortDateString());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void BeAbleToHaveIngredientsChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = person.GetType().GetProperties();
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
            PropertyInfo[] props = person.GetType().GetProperties();
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
            PropertyInfo[] props = person.GetType().GetProperties();
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
            PropertyInfo[] props = person.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Plans");

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void  BeAbleToHaveShoppingListsChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = person.GetType().GetProperties();
            var result = props.Where(p => p.Name == "ShoppingLists");

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void NotBeAbleToHavePersonsChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = person.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Persons");

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [Ignore]
        [TestMethod]
        public void RequireIngredientChildrenToHaveUniqueIDs()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void RequireRecipeChildrenToHaveUniqueIDs()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void RequireMenuChildrenToHaveUniqueIDs()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void RequirePlanChildrenToHaveUniqueIDs()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void RequireShoppingListChildrenToHaveUniqueIDs()
        {
            Assert.Fail();
        }
    }
}

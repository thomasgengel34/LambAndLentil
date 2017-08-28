using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Domain.Test.Entities
{
    [TestClass]
    [TestCategory("Person Class")]
    public class PersonClassShould
    {
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


        [Ignore]
        [TestMethod]
        public void BeAbleToHaveIngredientChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void BeAbleToHaveRecipeChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void BeAbleToHaveMenuChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void BeAbleToHavePlanChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void  BeAbleToHaveShoppingListChild()
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

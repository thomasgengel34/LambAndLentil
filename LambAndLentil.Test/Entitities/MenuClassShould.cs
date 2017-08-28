using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Domain.Test.Entities
{
    [TestClass]
    [TestCategory("Menu Class")]
    public class MenuClassShould
    {
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
        public void  BeAbleToHaveRecipeChild()
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

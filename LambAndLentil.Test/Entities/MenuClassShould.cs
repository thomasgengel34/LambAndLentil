using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using LambAndLentil.Test.Entities;

namespace LambAndLentil.Domain.Test.Entities
{
    [TestClass]
    [TestCategory("Menu Class")]
    public class MenuClassShould:BaseTest<Menu>
    {
        static Menu Menu { get; set; }
        static List<string> ListEntity;

        public MenuClassShould()
        {
           Menu = new Menu();
            ListEntity= new List <string>();
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
 
            
            Assert.AreEqual(menu.MealType, MealType.Breakfast);
            Assert.AreEqual(DayOfWeek.Sunday, menu.DayOfWeek); 
            Assert.AreEqual(0,menu.Diners);

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
    }
}

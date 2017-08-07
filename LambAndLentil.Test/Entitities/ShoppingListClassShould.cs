using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Domain.Test.Entities
{
    [TestClass]
    [TestCategory("ShoppingList Class")]
    public class ShoppingListClassShould
    {
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
        public void  InheritFromBaseEntity()
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
            Assert.IsNull( shoppingList.Author); 
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


    }
}

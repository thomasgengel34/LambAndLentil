using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using LambAndLentil.Test.Entities;

namespace LambAndLentil.Domain.Test.Entities
{
    [TestClass]
    [TestCategory("ShoppingList Class")]
    public class ShoppingListClassShould:BaseTest<ShoppingList>
    {
        static ShoppingList shoppingList;

        public ShoppingListClassShould()
        {
            shoppingList = new ShoppingList();
        }


        [TestMethod]
        public void HaveCorrectDefaultsInConstructor()
        { 
            ShoppingList shoppingList = new ShoppingList();
             
            Assert.IsNotNull(shoppingList.CreationDate);
            Assert.IsNotNull(shoppingList.ModifiedDate);
            Assert.IsNotNull(shoppingList.AddedByUser);
            Assert.IsNotNull(shoppingList.ModifiedByUser);
            Assert.AreEqual(shoppingList.AddedByUser, shoppingList.ModifiedByUser);
        }

        [TestMethod]
        public void InheritFromBaseEntity()
        { 
            ShoppingList shoppingList = new ShoppingList();
             
            Type baseType = typeof(BaseEntity);
            bool isBase = baseType.IsInstanceOfType(shoppingList);
             
            Assert.AreEqual(true, isBase);
        }

        [TestMethod]
        public void HaveBaseEntityPropertiesOnCreation()
        { 
            ShoppingList shoppingList = new ShoppingList(new DateTime(2017, 06, 26));
             
            Assert.AreEqual("Newly Created", shoppingList.Name);
            Assert.AreEqual("not yet described", shoppingList.Description);
            Assert.AreEqual("6/26/2017", shoppingList.CreationDate.ToShortDateString());
        }


        [TestMethod]
        public void HaveClassPropertiesOnCreation()
        { 
            ShoppingList shoppingList = new ShoppingList(new DateTime(2017, 06, 26));
             
            Assert.AreEqual(new DateTime(2017, 06, 26), shoppingList.Date);
            Assert.IsNull(shoppingList.Author);
        }

        [TestMethod]
        public void HaveOlderThanFortyYearCreationDateOKOnCreation()
        { 
            ShoppingList shoppingList = new ShoppingList(new DateTime(1977, 06, 26));
 
            Assert.AreEqual("6/26/1977", shoppingList.CreationDate.ToShortDateString());
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
    }
}

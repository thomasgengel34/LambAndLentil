using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  LambAndLentil.Test.BasicTests
{
    public class NonFoodItemsCan
    {

        [Ignore]
        [TestMethod]
        [TestCategory("NonFood")]
        public void NotAddANonFoodItemToAnIngredient()
        {
             
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("NonFood")]
        public void  AddANonFoodItemToARecipe()
        { 
            Assert.Fail();
        }
        [Ignore]
        [TestMethod]
        [TestCategory("NonFood")]
        public void  AddANonFoodItemToAMenu()
        { 
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("NonFood")]
        public void   ANonFoodItemToAPlan()
        {
             
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("NonFood")]
        public void  AddANonFoodItemToAShoppingList()
        {
             
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("NonFood")]
        public void AddANonFoodItemToAPerson()
        {   // might be an ADA spoon, or a tippy cup
            // Arrange 

            // Act  

            // Assert  
            Assert.Fail();
        }
    }
}

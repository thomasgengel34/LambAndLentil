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
    [TestCategory("Ingredient Class")]
    public class IngredientClassShould:BaseTest<Ingredient>
    {
        static Ingredient Ingredient { get; set; }
        static List<string> ListEntity;

        public IngredientClassShould()
        {
            Ingredient = new Ingredient();
            ListEntity= new List<string>();
        }


        [TestMethod]
        public void HaveCorrectDefaultsInConstructor()
        {
            // Arrange


            // Act
            // nothing to see here, just move along

            // Assert 
            Assert.IsNotNull(Ingredient.CreationDate);
            Assert.IsNotNull(Ingredient.ModifiedDate);
            Assert.IsNotNull(Ingredient.AddedByUser);
            Assert.IsNotNull(Ingredient.ModifiedByUser);
            Assert.AreEqual(Ingredient.AddedByUser, Ingredient.ModifiedByUser);
        }

        [TestMethod]
        public void InheritFromBaseEntity()
        {
            // Arrange
            Ingredient = new Ingredient();

            // Act 
            Type baseType = typeof(BaseEntity);
            bool isBase = baseType.IsInstanceOfType(Ingredient);

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
            Ingredient ingredient = new Ingredient(new DateTime(1977, 06, 26));
             
            Assert.AreEqual("6/26/1977", ingredient.CreationDate.ToShortDateString());
        }

     

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void  BeAbleToHaveRecipesChild()
        {
            BaseBeAbleToHaveRecipesChild(); 
        }

        

        [Ignore]
        [TestMethod]
        public void RequireIngredientChildrenToHaveUniqueIDs()
        {
            Assert.Fail();
        }
        
    }
}

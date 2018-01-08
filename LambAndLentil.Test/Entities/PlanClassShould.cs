using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;
using System.Reflection;
using System.Linq;

namespace LambAndLentil.Domain.Test.Entities
{ 
    [TestClass]
    [TestCategory("Plan Class")]
    public class PlanClassShould
    {
        static Plan plan;

        public PlanClassShould() => plan = new Plan();

        [TestMethod]
        public void HaveCorrectDefaultsInConstructor()
        {
            // Arrange
            Plan plan = new Plan();

            // Act
            // nothing to see here, just move along

            // Assert 
            Assert.IsNotNull(plan.CreationDate);
            Assert.IsNotNull(plan.ModifiedDate); 
            Assert.IsNotNull(plan.AddedByUser); 
            Assert.IsNotNull(plan.ModifiedByUser);
            Assert.AreEqual(plan.AddedByUser, plan.ModifiedByUser);
        }

        [TestMethod]
        public void  InheritFromBaseEntity()
        {
            // Arrange
            Plan plan = new Plan();

            // Act 
            Type baseType = typeof(BaseEntity);
            bool isBase = baseType.IsInstanceOfType(plan);

            // Assert  
            Assert.AreEqual(true, isBase);
        }

        [TestMethod]
        public void HaveBaseEntityPropertiesOnCreation()
        {
            // Arrange
            Plan plan = new Plan(new DateTime(2017, 06, 26));

            // Act - nothing

            // Assert
            Assert.AreEqual("Newly Created", plan.Name);
            Assert.AreEqual("not yet described", plan.Description);
            Assert.AreEqual("6/26/2017", plan.CreationDate.ToShortDateString());
        }

        [TestMethod]
        public void HaveOlderThanFortyYearCreationDateOKOnCreation()
        {
            // Arrange
            Plan plan = new Plan(new DateTime(1977, 06, 26));

            // Act - nothing

            // Assert 
            Assert.AreEqual("6/26/1977", plan.CreationDate.ToShortDateString());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void BeAbleToHaveIngredientsChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = plan.GetType().GetProperties();
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
            PropertyInfo[] props = plan.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Recipes");

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void  BeAbleToHaveMenusChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = plan.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Menus");

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void NotBeAbleToHavePlansChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = plan.GetType().GetProperties();
            var result = props.Where(p => p.Name == "Plans");

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestCategory("Class Child Test")]
        [TestMethod]
        public void NotBeAbleToHaveShoppingListsChild()
        {
            // Arrange

            // Act
            PropertyInfo[] props = plan.GetType().GetProperties();
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
            PropertyInfo[] props = plan.GetType().GetProperties();
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
    }
}

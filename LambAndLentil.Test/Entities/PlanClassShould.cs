using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;
using System.Reflection;
using System.Linq;
using LambAndLentil.Test.Entities;

namespace LambAndLentil.Domain.Test.Entities
{ 
    [TestClass]
    [TestCategory("Plan Class")]
    public class PlanClassShould:BaseTest<Plan>
    {
        static Plan plan;

        public PlanClassShould() => plan = new Plan();

        [TestMethod]
        public void HaveCorrectDefaultsInConstructor()
        { 
            Plan plan = new Plan();
             
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

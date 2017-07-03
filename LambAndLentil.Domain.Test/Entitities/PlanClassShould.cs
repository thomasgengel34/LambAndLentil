using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;

namespace LambAndLentil.Domain.Test.Entities
{
    [TestClass]
    [TestCategory("Plan Class")]
    public class PlanClassShould
    {
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

    }
}

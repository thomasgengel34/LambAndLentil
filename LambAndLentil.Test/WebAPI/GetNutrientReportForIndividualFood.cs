using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.WebAPI
{
    [TestClass]
    public class GetNutrientreportForIndividualFood
    {
        /*  Nutrient reports -- obtain reports for foods by one or more nutrients
  
For chedder cheese (ndbno 01009) only:
Browser: https://api.nal.usda.gov/ndb/nutrients/?format=json&api_key=DEMO_KEY&nutrients=205&nutrients=204&nutrients=208&nutrients=269&ndbno=01009
  */
        [Ignore]
        [TestMethod]
        public void GetABasicFoodreport()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void GetAFullFoodreport()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void GetAStatsFoodreport()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void GetCountOfNumberOfFoodsRequestedAndProcessed()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void GetCountOfNumberOfFoodsRequestedAndNotFound()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void GetFoodsRequestedApiVersion()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void GetFoodsreportType()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void GetFoodsReleaseVersion()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}

/* {
    "report": {
        "sr": "28",
        "groups": "All groups",
        "subset": "All foods",
        "end": 1,
        "start": 0,
        "total": 1,
        "foods": [
            {
                "ndbno": "01009",
                "name": "Cheese, cheddar",
                "weight": 132.0,
                "measure": "1.0 cup, diced",
                "nutrients": [
                    {
                        "nutrient_id": "208",
                        "nutrient": "Energy",
                        "unit": "kcal",
                        "value": "533",
                        "gm": 404.0
                    },
                    {
                        "nutrient_id": "269",
                        "nutrient": "Sugars, total",
                        "unit": "g",
                        "value": "0.63",
                        "gm": 0.48
                    }, */

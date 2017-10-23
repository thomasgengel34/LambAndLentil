using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.WebAPI
{
    [TestClass]
    public class GetListsOfFoodsNutrientsOrFoodGroupsShould
    {
        /*
         Lists -- retrieve lists of foods, nutrients, or food groups 
https://api.nal.usda.gov/ndb/list?format=json&lt=f&sort=n&api_key=DEMO_KEY

            https://api.nal.usda.gov/ndb/list?format=json&lt=f&sort=n&api_key=sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm
*/
        [Ignore]
        [TestMethod]
        public void GetAListOfFoods()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void GetAListOfFoodsByFoodGroup()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void GetTheSecondPageForAListOfFoods()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void GetAListOfFoodsSortedByNdbno()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void GetAListOfNutrients()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void GetAListOFoodGroups()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}

/* {
    "list": {
        "lt": "f",
        "start": 0,
        "end": 50,
        "total": 50,
        "sr": "28",
        "sort": "n",
        "item": [
            {
                "offset": 0,
                "id": "45137745",
                "name": "!AJUA!, CAFFEINE FREE SODA, MANDARIN ORANGE, UPC: 061500127178"
            },
            {
                "offset": 1,
                "id": "45137747",
                "name": "!AJUA!, CAFFEINE FREE SODA, PINEAPPLE, UPC: 061500127161"
            },
            {
                "offset": 2,
                "id": "45137744",
                "name": "!AJUA!, CAFFEINE FREE SODA, TUTTI FRUITTI FRUIT PUNCH, UPC: 061500127154"
            },
            ...     */

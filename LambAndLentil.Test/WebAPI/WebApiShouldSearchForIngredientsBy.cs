﻿using System.Threading.Tasks;
using LambAndLentil.Test.BasicControllerTests;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.WebAPI
{

    [TestClass]
    public class WebApiShouldSearchForIngredientsBy: IngredientsController_Test_Should
    {
        protected IIngredientsControllerAsync AsyncController = new IngredientsController(Repo);

        [TestMethod]
        public async Task BrandedFoodProductsAndFindAtLeast215557Ingredients()
        {
            // Arrange
            string searchString = "";   

            // Act 
            long count = await AsyncController.GetIngredientCountAsync(searchString);

            //Assert
            Assert.IsTrue(215557<=count);
        }

        //[Ignore]    // can't do this using the API.  Keep for reference
        //[TestMethod]
        //public async Task ManufacturerConagraFoodsIncAndFindAtLeast83Foods()
        //{  // https://ndb.nal.usda.gov/ndb/search/list?SYNCHRONIZER_TOKEN=2118be34-71bf-4d01-bcdb-381a21ad9049&SYNCHRONIZER_URI=%2Fndb%2Fsearch%2Flist&qt=&ds=Branded+Food+Products&qlookup=&manu=ConAgra+Foods+Inc.

        //    //https://api.nal.usda.gov/ndb/search/?format=json&q=&+max=9223372036854775807offset=0&ds=Branded Food Products&manu=ConAgra Foods inc.&api_key=sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm
        //    //https://api.nal.usda.gov/ndb/search/?format=json&q=&+max=9223372036854775807offset=0&ds=Branded%20Food%20Products&manu=ConAgra%20Foods%20inc.&api_key=sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm
        //    // Arrange
        //    //try
        //    //https://api.nal.usda.gov/ndb/search/?format=json&+max=9223372036854775807offset=0&ds=Branded%20Food%20Products&manu=ConAgra%20Foods%20Inc.&q=&api_key=sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm    no

        //    //  https://api.nal.usda.gov/ndb/search/?format=json&qt=&ds=Branded+Food+Products&qlookup=&manu=ConAgra+Foods+Inc.&api_key=sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm  215557 no

        //    // https://ndb.nal.usda.gov/ndb/search/list?SYNCHRONIZER_TOKEN=2118be34-71bf-4d01-bcdb-381a21ad9049&SYNCHRONIZER_URI=%2Fndb%2Fsearch%2Flist&ds=Branded%20Food%20Products&manu=ConAgra%20Foods%20Inc.&q=&api_key=sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm     83 works

        //    //  https://api.nal.usda.gov/ndb/search/?format=json&qt=ds=Branded%20Food%20Products&manu=ConAgra%20Foods%20Inc.&q=&api_key=sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm    224346 no

        //    //  https://api.nal.usda.gov/ndb/search/?format=json&qt=&manu=ConAgra%20Foods%20Inc.&q=&api_key=sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm    224346 no

        //    string searchString = "";
        //    string manufacturer = "ConAgra Foods inc.";
           
        //    // Act 
        //    long count = await Controller.GetIngredientCountAsync(searchString, "Branded Food Products",long.MaxValue,0, manufacturer);

        //    //Assert
        //    Assert.IsTrue(83<= count);
        //}

        [TestMethod]
        public async Task FoodGroupIfDataSourceIsStandardReferenceAndFoodGroupIsBabyFoodShouldReturnAtLeast368Foods()
        {
            // https://ndb.nal.usda.gov/ndb/search/list?SYNCHRONIZER_TOKEN=b0bc3961-c92f-462f-84b9-0da1ffadd5ad&SYNCHRONIZER_URI=%2Fndb%2Fsearch%2Flist&qt=&ds=Standard+Reference&qlookup=&fgcd=Baby+Foods&manu=      368 found

           // https://api.nal.usda.gov/ndb/search/?format=json&q=&+max=9223372036854775807offset=0&ds=Standard+Reference&fg=Baby+Foods&api_key=sFtfcrVdSOKA4ip3Z1MlylQmdj5Uw3JoIIWlbeQm    368 found.

            // Arrange
            string searchString = "";
            string foodGroup = "Baby Foods";
            // Act
            long count = await AsyncController.GetIngredientCountAsync(searchString, "", long.MaxValue, 0, foodGroup);
            //Assert
            Assert.IsTrue(368 <= count);

        }

        //[TestMethod]
        //public void NotByFoodGroupIfDataSourceIsBrandedFoodProduct()
        //{
        //    // Arrange

        //    // Act

        //    //Assert
        //    Assert.Fail();

        //}

        //[TestMethod]
        //public void BrandedFoodProductsDataSourceAndManufacturer()
        //{
        //    // Arrange

        //    // Act

        //    //Assert
        //    Assert.Fail();

        //}

        //[TestMethod]
        //public void StandardReferenceAndManufacturer()
        //{
        //    // Arrange

        //    // Act

        //    //Assert
        //    Assert.Fail();

        //}

            [Ignore]
        [TestMethod]
        public void UnfilteredAutocomplete()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();

        }

        [Ignore]
        [TestMethod]
        public void FilteredAutocompleteForBrandedFoodProducts()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();

        }

        [Ignore]
        [TestMethod]
        public void FilteredAutocompleteForManufacturer()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();

        }

        [Ignore]
        [TestMethod]
        public void FilteredAutocompleteForFoodGroupIfDataSourceIsStandardReference()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();

        }

        [Ignore]
        [TestMethod]
        public void FilteredAutocompleteForNotByFoodGroupIfDataSourceIsBrandedFoodProduct()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();

        }

        [Ignore]
        [TestMethod]
        public void FilteredAutocompleteForBrandedFoodProductsDataSourceAndManufacturer()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();

        }

        [Ignore]
        [TestMethod]
        public void FilteredAutocompleteForStandardReferenceAndManufacturer()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();

        }
    }
}

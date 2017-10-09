﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Test.BasicControllerTests;

namespace LambAndLentil.Test.WebAPI
{
    //  [Ignore]
    [TestClass]
    public class GetNdbnoShould: IngredientsController_Test_Should
    {

        [TestMethod]
        public void ReturnTheCorrectErrorMessageWhenItemIsNotFound()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnCorrectIngredientsForAardvarkHabeneroOnSingleItemSearch()
        {
            //// Arrange
            //string searchString = "AARDVARK HABENERO HOT SAUCE, UPC: 853393000030";

            //string correctIngredients = "TOMATOES(TOMATOES AND FIRE ROASTED TOMATOES, TOMATO JUICE, CITRIC ACID, CALCIUM CHLORIDE), WHITE WINE VINEGAR, CARROTS, WATER, YELLOW ONION, HABANERO CHILI PEPPER(HABANERO CHILI PEPPERS, WATER, SALT, CITRIC ACID), MUSTARD(DISTILLED VINEGAR, WATER, MUSTARD SEED, SALT, TURMERIC, SPICES), ORGANIC CANE SUGAR, SALT, MODIFIED FOOD STARCH, GARLIC, SUNFLOWER OIL, HERBS AND SPICES. Date Available: 09 / 23 / 2016";

            //// Act
            //// string returnedIngredients = Controller.GetIngredients(searchString);
            //string returnedIngredients = "";
            //                 // Assert
            //                 //   Assert.Fail();  // not actually working 
            //                 Assert.AreEqual(correctIngredients, returnedIngredients);
        }
    }
        public class GetNdbnoShouldxxxxx : IngredientsController_Test_Should
        {
            [TestMethod]
        public void ReturnFourItemsInTheDDLWhenHabeneroIsTheInput()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }
        [TestMethod]
        public void ReturnOnlyFifteenItemsInTheDDLWhenThereAreMoreThanFifteenInTheUSDADb()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }
        [TestMethod]
        public void ReturnCorrectIngredientsForAardvarkHabeneroOnSingleItemSearch()
        {
            // Arrange
            string searchString =   "AARDVARK HABENERO HOT SAUCE, UPC: 853393000030";
           
            string correctIngredients = "TOMATOES(TOMATOES AND FIRE ROASTED TOMATOES, TOMATO JUICE, CITRIC ACID, CALCIUM CHLORIDE), WHITE WINE VINEGAR, CARROTS, WATER, YELLOW ONION, HABANERO CHILI PEPPER(HABANERO CHILI PEPPERS, WATER, SALT, CITRIC ACID), MUSTARD(DISTILLED VINEGAR, WATER, MUSTARD SEED, SALT, TURMERIC, SPICES), ORGANIC CANE SUGAR, SALT, MODIFIED FOOD STARCH, GARLIC, SUNFLOWER OIL, HERBS AND SPICES. Date Available: 09 / 23 / 2016";

            // Act
         //   string returnedIngredients = Controller.GetIngredients(searchString);

            // Assert
           Assert.Fail();  // not actually working 
         //   Assert.AreEqual(correctIngredients, returnedIngredients);
        }

        [TestMethod]
        public void ReturnCorrectNdbnoForAardvarkHabeneroOnSingleItemSearch()
        {
            // Arrange
            string searchString = "AARDVARK HABENERO HOT SAUCE, UPC: 853393000030";
            int corectNdbno = 45078606; 

            // Act
            int returnedNdbno = Controller.GetNdbno(searchString);

            // Assert
             Assert.Fail();  // not actually working 
              Assert.AreEqual(corectNdbno, returnedNdbno);
        }



        /***********************************************************************
          * Beer tests
         * by ndbno directly:  AJ-STEPHANS, BIRCH BEER NEW ENGLAND'S BEST TONIC, UPC: 0899000068311 ndbno 45157747
          ⦁	seach Birch Beer: :  AJ-STEPHANS, BIRCH BEER NEW ENGLAND'S BEST TONIC, UPC: 0899000068311 ndbno 45157747   
          *********************************************************************/
        /**********************************************************************
     * Chef Boyardee Tests
     * Search for Chef Boyardee S =>CHEF BOYARDEE ABC123 With Meatball Mini Bites Pasta, UNPREPARED, GTIN: 000641440416401   
⦁	search for CHEF BOYARDEE : CHEF BOYARDEE ABC123 With Meatball Mini Bites Pasta, UNPREPARED, GTIN: 00064144041640 
⦁	 fails on     
⦁	 one ingredient input: CHEF BOYARDEE ABC123 With Meatball Mini Bites Pasta, UNPREPARED, GTIN: 00064144041640  : : Ingredient not found. Please try again.  
⦁	this worked when I just put in the GTIN.
⦁	worked for CHEF BOYARDEE ABC123 and then select on DDL
⦁	single select CHEF BOYARDEE ABC123 With Meatball Mini Bites Pasta, UNPREPARED does not work
⦁	single select CHEF BOYARDEE ABC123 With Meatball Mini Bites Pasta does not work	
⦁	select 'CHEF BOYARDEE ABC123 With Meatball Mini Bites' not found
⦁	single select 'CHEF BOYARDEE ABC123 With'  works  
⦁	single select 'CHEF BOYARDEE ABC123 With Meatball ' works
⦁	 select 'CHEF BOYARDEE ABC123 With Meatball Mini ' found 8 results, then could select 
⦁	this is not a problem in my software.  There are tricks in finding stuff on USDA as well     * 
     *********************************************************************/

        /**********************************************************************
         * Habenero Tests
         *********************************************************************/
        [TestMethod]
        public void ReturnCorrectIngredientsFor076606619663OnSingleItemSearch()
        {
            //	single item search: 076606619663 will return PICKLED ASPARAGUS , SPICY!, UPC: 076606619663
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnCorrectIngredientsForAcornStewApacheOnSingleItemSearch()
        {
            //	single item search:  Acorn Stew(Apache)
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }
        /* Butter
         *  search for butter:'ABC, PEANUT BUTTER, UPC: 837991219186'.    
⦁	search for butter: ABBOTT, EAS, MYOPLEX 30 BUILD MUSCLE BAR, CHOCOLATE PEANUT BUTTER, UPC: 791083622813
         * search on butter: ACAI BERRY NUT BUTTER, UPC: 8700010060170 
         */

        [TestMethod]
        public void ReturnCorrectIngredientsForAardvarkHabeneroOnSeachOnHabenero()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnCorrectIngredientsForRaspberryHabeneroPreservesOnSeachOnHabenero()
        { // search: Habenero: PRIMO, RASPBERRY HABENERO PRESERVES, MILD HEAT, UPC: 1864350001571    
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnCorrectIngredientsForSackOhNutsGarlicHabeneroSmokedPistachiosOnSeachOnHabenero()
        { //  search: Habenero: SACK-OH-NUTS GARLIC HABENERO SMOKED PISTACHIOS, UPC: 8656400002122   
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnCorrectIngredientsForPickledAsparagusSpicyOnSeachOnHabenero()
        { //   search for Habenero: selecting PICKLED ASPARAGUS , SPICY!, UPC: 076606619663 
            //  	search for Habenero: selecting SPICY HABENERO PICKLED ASPARAGUS, UPC: 868171000034  
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }



        [TestMethod]
        public void ReturnCorrectIngredientsForSpicyHabeneroPickledAsparagusOnSeachOnHabenero()
        {
            //  	search for Habenero: selecting SPICY HABENERO PICKLED ASPARAGUS, UPC: 868171000034  
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }
        /* infant formula tests
         *  Infant formula, ABBOTT NUTRITION, SIMILAC NEOSURE, ready-to-feed, with ARA and DHA ndbno 03944  
         */

        /* orange tests */
        [TestMethod]
        public void ReturnAnOrangeOnSearchForOrange()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        /* tuna
         * search for tuna: ADOBO TUNA IN SOY SAUCE AND VINEGAR, UPC: 7484851029621   
         * 	search tuna: ACE OF DIAMONDS, CHUNK LIGHT TUNA IN WATER, UPC: 077600525851  
⦁	search for TERIYAKI AHI TUNA BURGERS, UPC: 0778903837422: TERIYAKI AHI TUNA BURGERS, UPC: 0778903837422    4 results   but selectable
⦁	077600525851  
         *  search tuna: 555, AFRITATA, TUNA IN TOMATO SAUCE WITH VEGETABLE, UPC: 748485102979  
⦁	AHI TUNA BURGER WITH GARDEN VEGETABLES, UPC: 6538490949231  
         */
    }
}
/*   
⦁	⦁	
⦁	
⦁	

    
 https://ndb.nal.usda.gov/ndb/search/ListEntity  
⦁	https://api.nal.usda.gov/ndb/V2/reports?ndbno=01009&ndbno=45202763&ndbno=35193&type=f&format=json&api_key=DEMO_KEY */

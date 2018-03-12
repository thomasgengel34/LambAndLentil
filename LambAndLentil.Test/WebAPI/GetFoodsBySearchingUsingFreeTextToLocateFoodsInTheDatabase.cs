using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Test.BasicTests;
using System.Threading.Tasks;
using LambAndLentil.Domain.Entities;
using System.Collections.Generic;
using LambAndLentil.UI.Controllers;

namespace LambAndLentil.Test.WebAPI
{  [Ignore]  // TODO: build an interface; need a test fake as the actual database changes. 
    [TestClass]
   internal class GetFoodsBySearchingUsingFreeTextToLocateFoodsInTheDatabase : IngredientsControllerShould
    {
         IIngredientsControllerAsync  AsyncController = (IIngredientsControllerAsync)(new IngredientsController(repo));

        /* Search -- use free text to locate foods in the database
 https://api.nal.usda.gov/ndb/search/?format=json&q=butter&sort=n&max=25&offset=0&api_key=DEMO_KEY 
 */

        [TestMethod]
        public async Task ReturnCorrectIngredientsFor076606619663OnSingleItemSearchAsync()
        {
            //	single item search: 076606619663 will return ingredients for PICKLED ASPARAGUS , SPICY!, UPC: 076606619663 
     
            string searchString = " 076606619663";
            string correctIngredients = "INGREDIENTS: GREEN ASPARAGUS, WATER, VINEGAR, SUGAR, SALT, MUSTARD SEEDS, BLACK PEPPER, RED HOT PEPPER, GARLIC";
            // Act 
            string returnedIngredients = await  AsyncController.GetIngredientsFromDescription(searchString);
             
            Assert.AreEqual(correctIngredients, returnedIngredients);

        }
        [TestMethod]
        [TestCategory("Websearch: Habenero")]
        public async Task ReturnCorrectNameForAardvarkHabeneroFromNdbnoAsync()
        { 
            int ndbno = 45078606;
             
            string returnedName = await  AsyncController.GetIngredientNameFromNdbno(ndbno);
             
            Assert.AreEqual("AARDVARK HABENERO HOT SAUCE, UPC: 853393000030", returnedName);
        }

        [TestMethod]
        [TestCategory("Websearch: Habenero")]
        public async Task ReturnCorrectIngredientsForAardvarkHabeneroFromNdbnoAsync()
        { 
            int ndbno = 45078606;
            string correctIngredients = "TOMATOES (TOMATOES AND FIRE ROASTED TOMATOES, TOMATO JUICE, CITRIC ACID, CALCIUM CHLORIDE), WHITE WINE VINEGAR, CARROTS, WATER, YELLOW ONION, HABANERO CHILI PEPPER (HABANERO CHILI PEPPERS, WATER, SALT, CITRIC ACID), MUSTARD (DISTILLED VINEGAR, WATER, MUSTARD SEED, SALT, TURMERIC, SPICES), ORGANIC CANE SUGAR, SALT, MODIFIED FOOD STARCH, GARLIC, SUNFLOWER OIL, HERBS AND SPICES.";
             
            string returnedIngredients = await  AsyncController.GetIngredientsByNdbno(ndbno);
             
            Assert.AreEqual(correctIngredients, returnedIngredients);

        }


        [TestMethod]
        [TestCategory("Websearch: Habenero")]
        public async Task ReturnCorrectIngredientsForAardvarkHabeneroOnSingleItemSearchAsync()
        { 
            string searchString = "AARDVARK HABENERO HOT SAUCE, UPC: 853393000030";

            string correctIngredients = "TOMATOES (TOMATOES AND FIRE ROASTED TOMATOES, TOMATO JUICE, CITRIC ACID, CALCIUM CHLORIDE), WHITE WINE VINEGAR, CARROTS, WATER, YELLOW ONION, HABANERO CHILI PEPPER (HABANERO CHILI PEPPERS, WATER, SALT, CITRIC ACID), MUSTARD (DISTILLED VINEGAR, WATER, MUSTARD SEED, SALT, TURMERIC, SPICES), ORGANIC CANE SUGAR, SALT, MODIFIED FOOD STARCH, GARLIC, SUNFLOWER OIL, HERBS AND SPICES.";
             
            //   int?  ndbno = await controller.GetNdbnoFromSearchStringAsync(searchString);
            string returnedIngredients = await  AsyncController.GetIngredientsFromDescription(searchString);
             
            Assert.AreEqual(correctIngredients, returnedIngredients);
        }


        [TestMethod]
        [TestCategory("Websearch: Habenero")]
        public async Task ReturnCorrectNdbnoForAardvarkHabeneroOnSingleItemSearchAsync()
        { 
            string searchString = "AARDVARK HABENERO HOT SAUCE, UPC: 853393000030";
            int corectNdbno = 45078606;
 
            int? ndbno = await  AsyncController.GetNdbnoFromSearchStringAsync(searchString);
 
            Assert.AreEqual(corectNdbno, ndbno);
        }
        [TestMethod]
        public async Task ReturnCorrectIngredientsForAcornStewApacheOnSingleItemSearch()
        { 
            string searchString = "Acorn Stew Apache";

            string correctIngredients = "Boiled stew made with water, beef or deer, prepared acorns, dumpling strips, salt, and pepper.";
 
            string returnedIngredients = await AsyncController.GetIngredientsFromDescription(searchString);
             
            Assert.AreEqual(correctIngredients, returnedIngredients);
        }

        [TestMethod]
        public async Task ReturnCorrectNdbnoForAJStephansBirchBeerOnSingleItemSearchAsync()
        {
            // Arrange                                                                            AJ - STEPHANS, BIRCH BEER NEW
            string searchString = "AJ - STEPHANS, BIRCH BEER NEW ENGLAND'S BEST TONIC, UPC: 0899000068311";   
            int correctNdbno = 45157747;
             
            int? ndbno = await  AsyncController.GetNdbnoFromSearchStringAsync(searchString);
             
            Assert.AreEqual(correctNdbno, ndbno);
        }

         
        [TestMethod]
        [TestCategory("Websearch: Orange")]
        public async Task ReturnOrangeSherbetOnSearchForOrange()
        { 
            string searchString = "Orange";
            string expected = "Sherbet, orange";
             
            UsdaWebApiDataSource uwads = UsdaWebApiDataSource.StandardReference;
            string name = await  AsyncController.GetIngredientsFromDescription(searchString, "", uwads);
             
            Assert.AreEqual(expected, name);
        }

          [TestMethod]
        [TestCategory("Websearch: Habenero")]
        public async Task ReturnFourItemsInTheDDLWhenHabeneroIsTheInput()
        { 
            string searchString = "Habenero";
          
            List<string> list = (List<string>)(await AsyncController.GetIngredientNamesAsync(searchString));
             
            Assert.AreEqual(3, list.Count);
        }

        [Ignore]
        [TestMethod]
        public void ReturnOnlyFifteenItemsInTheDDLWhenThereAreMoreThanFifteenInTheUSDADb() =>
             
            Assert.Fail();

        [TestMethod]
        public async Task ReturnCorrectIngredientsForRaspberryHabeneroPreservesOnSingleItemSearchAsync()
        {  //search: Habenero: PRIMO, RASPBERRY HABENERO PRESERVES, MILD HEAT, UPC: 1864350001571
           
            string searchString = " PRIMO, RASPBERRY HABENERO PRESERVES, MILD HEAT, UPC: 1864350001571";

            string correctIngredients = "RED RASPBERRIES, PURE CANE SUGAR, VINEGAR, FRUIT PECTIN, HABANERO CHILE.";

           
            //   int?  ndbno = await controller.GetNdbnoFromSearchStringAsync(searchString);
            string returnedIngredients = await  AsyncController.GetIngredientsFromDescription(searchString); 
           
            Assert.AreEqual(correctIngredients, returnedIngredients);
        }

        [TestMethod]
        [TestCategory("Websearch: Habenero")]
        public async Task ReturnCorrectIngredientsForSackOhNutsGarlicHabeneroSmokedPistachiosOnSeachOnHabenero()
        { //  search: Habenero: SACK-OH-NUTS GARLIC HABENERO SMOKED PISTACHIOS, UPC: 8656400002122    
            string searchString = "SACK - OH - NUTS GARLIC HABENERO SMOKED PISTACHIOS, UPC: 865640000212";
            string correctIngredients = "PISTACHIOS, GARLIC, HABENERO, SALT & NATURAL SMOKE FLAVOR.";
             
            string returnedIngredients = await AsyncController.GetIngredientsFromDescription(searchString);
             
            Assert.AreEqual(correctIngredients, returnedIngredients);
        }

        [TestMethod]
        [TestCategory("Websearch: Habenero")]
        public async Task ReturnCorrectIngredientsForPickledAsparagusSpicyOnSeachOnHabenero()
        { //   search for Habenero: selecting PICKLED ASPARAGUS , SPICY!, UPC: 076606619663 
         
            string searchString = "PICKLED ASPARAGUS , SPICY!, UPC: 076606619663";
            string correctIngredients = "INGREDIENTS: GREEN ASPARAGUS, WATER, VINEGAR, SUGAR, SALT, MUSTARD SEEDS, BLACK PEPPER, RED HOT PEPPER, GARLIC";
 
            string returnedIngredients = await AsyncController.GetIngredientsFromDescription(searchString);
             
            Assert.AreEqual(correctIngredients, returnedIngredients);
        }



        [TestMethod]
        [TestCategory("Websearch: Habenero")]
        public async Task ReturnCorrectIngredientsForSpicyHabeneroPickledAsparagusOnSeachOnHabenero()
        {
            //  	search for Habenero: selecting SPICY HABENERO PICKLED ASPARAGUS, UPC: 868171000034  
           
            string searchString = "SPICY HABENERO PICKLED ASPARAGUS, UPC: 868171000034";
            string correctIngredients = "ASPARAGUS, HABANERO PEPPERS, WATER, WHITE VINEGAR, WHOLE GARLIC CLOVES, SUNDRIED TOMATOES AND SPICES";
 
            string returnedIngredients = await AsyncController.GetIngredientsFromDescription(searchString);
 
            Assert.AreEqual(correctIngredients, returnedIngredients);
        }


        [TestMethod]
        [TestCategory("Websearch: Peanut Butter")]
        public async Task ReturnCorrectIngredientsForAbcPeanutButter()
        {
            //  	search for ABC, PEANUT BUTTER, UPC: 837991219186  
         
            string searchString = "ABC, PEANUT BUTTER, UPC: 837991219186";
            string correctIngredients = "FRESHLY ROASTED PEANUTS, SUGAR, CONTAINS 2% OR LESS OF: MOLASSES, HYDROGENATED VEGETABLES OILS (RAPESSED, COTTONSEED AND SOYBEAN), DEXTROSE, CORN SYRUP AND SALT.";
 
            string returnedIngredients = await AsyncController.GetIngredientsFromDescription(searchString);
             
            Assert.AreEqual(correctIngredients, returnedIngredients);
        }

        [TestMethod]
        [TestCategory("Websearch: Butter")]
        public async Task ReturnCorrectIngredientsForAbbottMusclebarPeanutButter()
        {
            //  	search for ABBOTT, EAS, MYOPLEX 30 BUILD MUSCLE BAR, CHOCOLATE PEANUT BUTTER, UPC: 791083622813
           
            string searchString = "ABBOTT, EAS, MYOPLEX 30 BUILD MUSCLE BAR, CHOCOLATE PEANUT BUTTER, UPC: 791083622813";
            string correctIngredients = "CORN SYRUP, SOY PROTEIN ISOLATE,. CHOCOLATE FLAVORED COATING (WHEY PROTEIN CONCENTRATE, SUGAR, PALM KERNEL POWDER, PALM OIL, NATURAL FLAVOR, SOY LECITHIN, MILKFAT), SOY PROTEIN NUGGETS (SOY PROTEIN ISOLATE, TAPIOCA STARCH, SALT), PEANUT BUTTER FUDGE (CORN SYRUP, INVERT SUGAR, PEANUT BUTTER [PEANUTS, SUGAR, SALT], SUGAR, PALM KERNEL OIL, PEANUT FLOUR, MILK PROTEIN ISOLATE, SOY LECITHIN, SALT, VANILLA EXTRACT, XANTHAN GUM, CARBO SEED GUM, BETA-CAROTENE), FRUCTOSE SYRUP PEANUT FLOUR, PEANUT BUTTER, PEANUTS, CRYSTALLINE FRUCTOSE; LESS THAN 2% OF THE FOLLOWING: GLYCERIN, NATURAL FLAVOR, SALT. VITAMIN AND MINERAL BLEND (CALCIUM PHOSPHATE, ASCORBIC ACID, MAGNESIUM OXIDE, DL-ALPHA-TOCOPHERYL ACETATE, VITAMIN A PALMITATE, NIACINAMIDE, ZINC OXIDE, PYRIDOXINE HYDROCHLORIDE, CALCIUM PANTOTHENATE, RIBOFLAVIN, FERROUS FUMARATE, THIAMINE MONONITRATE, FOLIC ACID, CHROMIUM CHLORIDE, BIOTIN, SODIUM SELENITE MOLYBDATE, CYANOCOBALAMIN).";
             
            string returnedIngredients = await AsyncController.GetIngredientsFromDescription(searchString);
 
            Assert.AreEqual(correctIngredients, returnedIngredients);
        }


        [TestMethod]
        [TestCategory("Websearch: Butter")]
        public async Task ReturnCorrectIngredientsForAcaiBerryNutButter()
        {
            //  	search for ACAI BERRY NUT BUTTER, UPC: 8700010060170 
        
            string searchString = "ACAI BERRY NUT BUTTER, UPC: 8700010060170";
            string correctIngredients = "RAW ORGANIC CASHEW BUTTER, RAW ORGANIC ALMOND OIL, RAW ORGANIC POWDER, RAW ORGANIC ELDERBERRY POWDER, RAW ORGANIC AGAVE, RAW ORGANIC VANILLA POWDER, RAW ORGANIC STEVIA AND SEASALT.";
             
            string returnedIngredients = await AsyncController.GetIngredientsFromDescription(searchString);
             
            Assert.AreEqual(correctIngredients, returnedIngredients);
        }

        [Ignore]   // not working
        [TestMethod]
        [TestCategory("Websearch:Similac")]
        public async Task ReturnCorrectIngredientsForSimilac()
        {
            //  	search for  Infant formula, ABBOTT NUTRITION, SIMILAC NEOSURE, ready-to-feed, with ARA and DHA ndbno 03944  
            
            string searchString = "ABBOTT NUTRITION, SIMILAC NEOSURE, ready-to-feed, with ARA and DHA";
            string correctIngredients = "RAW ";
             
            string returnedIngredients = await AsyncController.GetIngredientsFromDescription(searchString);
             
            Assert.AreEqual(correctIngredients, returnedIngredients);
        }

        [TestMethod]
        [TestCategory("Websearch:Tuna")]
        public async Task ReturnCorrectIngredientsForAdoboTuna()
        {
            //  	search for  tuna: ADOBO TUNA IN SOY SAUCE AND VINEGAR, UPC: 748485102962    
            string searchString = "ADOBO TUNA IN SOY SAUCE AND VINEGAR, UPC: 748485102962";
            string correctIngredients = "TUNA FLAKES, WATER, SOY PROTEIN CONCENTRATE, SOY SAUCE (FERMENTED SOYBEAN, WHEAT, WATER, SALT), SOYBEAN OIL, VINEGAR, BROWN SUGAR, IODIZED SALT, THICKENERS (CORNSTARCH, TAPIOCA), SEASONINGS (GARLIC, BLACK PEPPER,GINGER), MONOSODIUM GLUTAMATE, BAY LEAF AND VITAMIN A.";
             
            string returnedIngredients = await AsyncController.GetIngredientsFromDescription(searchString);
 
            Assert.AreEqual(correctIngredients, returnedIngredients);
        }

        [TestMethod]
        [TestCategory("Websearch:Tuna")]
        public async Task ReturnCorrectIngredientsForAceOfDiamondsTuna()
        {
            //  	search for  tuna: ACE OF DIAMONDS, CHUNK LIGHT TUNA IN WATER, UPC: 077600525851  
           
            //   string searchString = "ACE OF DIAMONDS, CHUNK LIGHT TUNA I";  //    does not work 
            string searchString = "ACE OF DIAMONDS, CHUNK LIGHT TUNA  ";  //  works  
            string correctIngredients = "CHUNK LIGHT TUNA, WATER, VEGETABLE BROTH (CONTAINS SOY), SALT.";
             
            string returnedIngredients = await AsyncController.GetIngredientsFromDescription(searchString);
             
            Assert.AreEqual(correctIngredients, returnedIngredients);
        }

        [TestMethod]
        [TestCategory("Websearch:Tuna")]
        public async Task ReturnCorrectIngredientsForTeriyakiAhiTuna()
        {
            //  	search for TERIYAKI AHI TUNA BURGERS, UPC: 0778903837422: TERIYAKI AHI TUNA BURGERS, UPC: 0778903837422    4 results   but selectable
          
            string searchString = "TERIYAKI AHI TUNA BURGERS, UPC: 0778903837422";  //   
            string correctIngredients = "WILD CAUGHT YELLOWFIN TUNA, ONION, CELERY, CARROT, RED BELL PEPPER, CANOLA OIL, PARSLEY, SALT, PEPPER, FILTERED HARDWOOD SMOKE TO PROMOTE COLOR RETENTION.";
             
            string returnedIngredients = await AsyncController.GetIngredientsFromDescription(searchString);
             
            Assert.AreEqual(correctIngredients, returnedIngredients);
        }

        [TestMethod]
        [TestCategory("Websearch:Tuna")]
        public async Task ReturnCorrectIngredientsForAfritataTuna()
        {
            //  	search for 555, AFRITATA, TUNA IN TOMATO SAUCE WITH VEGETABLE, UPC: 748485102979  
          
            string searchString = "AFRITATA, TUNA IN TOMATO SAUCE WITH VEGETABLE, UPC: 748485102979";  //   
            string correctIngredients = "TUNA FLAKES, TOMATO SAUCE (WATER, TOMATO PASTE), POTATOES, SOY PROTEIN CONCENTRATE, SOYBEAN OIL, GREEN PEAS, BROWN SUGAR, BELL PEPPER, SEASONING (ONION, GARLIC, PAPRIKA), IODIZED SALT, SOY SAUCE (FERMENTED SOYBEANS, WHEAT, WATER, SALT), MONOSODIUM GLUTAMATE AND VITAMIN A.";
 
            string returnedIngredients = await AsyncController.GetIngredientsFromDescription(searchString);
             
            Assert.AreEqual(correctIngredients, returnedIngredients);
        }

        [TestMethod]
        [TestCategory("Websearch:Tuna")]
        public async Task ReturnCorrectIngredientsForAhiTunaBurger()
        {
            //  	search for AHI TUNA BURGER WITH GARDEN VEGETABLES, UPC: 6538490949231  
             
            // string searchString = "AHI TUNA BURGER WITH GARDEN VEGETABLES, UPC: 6538490949231";  //   doesn't work
              string searchString = "AHI TUNA BURGER WITH GARDEN VEGETABLES ";  //   
            string correctIngredients = "WILD CAUGHT YELLOWFIN TUNA, ONION, CELERY, CARROT, RED BELL PEPPER, CANOLA OIL, PARSLEY, SALT, PEPPER, FILTERED HARDWOOD SMOKE TO PROMOTE COLOR RETENTION.";
             
            string returnedIngredients = await AsyncController.GetIngredientsFromDescription(searchString);
 
            Assert.AreEqual(correctIngredients, returnedIngredients);
        }

        
        [TestMethod]
        [TestCategory("Websearch:Manufacturer Conagra")]
        [TestCategory("Websearch:Chef Boyardee")]
        public async Task ReturnCorrectIngredientsForCBPizzaTwistFromConagraOnSearch()
        {
            // search for 45134255, CHEF BOYARDEE Pizza Twist, UNPREPARED, GTIN: 00064144020805
            // manufacturer Conagra

          
            string searchString = "CHEF BOYARDEE Pizza Twist, UNPREPARED, GTIN: 00064144020805";  //   
            string correctIngredients = "Tomatoes (Tomato Puree, Water), Water, Enriched Pasta (Durum Wheat Semolina, Niacin, Ferrous Sulfate, Thiamine Mononitrate [Vitamin B1], Riboflavin [Vitamin B2], Folic Acid), Pepperoni (Pork And Beef, Salt, Contains 2% or Less of: Dextrose, Flavorings, Lactic Acid Starter Culture, Oleoresin Paprika, Sodium Nitrite and BHA, BHT, Citric Acid [to protect flavor]), LESS THAN 2% OF: High Fructose Corn Syrup, Garlic Powder, Onion Powder, Modified Corn Starch, Salt, Spices, Maltodextrin, Citric Acid, Yeast Extract, Sugar, Sea Salt, Potassium Chloride, Flavorings, Ammonium Chloride, Paprika, Lactic Acid.  CONTAINS:  WHEAT";
            string manufacturer = "Conagra";
            
            string returnedIngredients = await AsyncController.GetIngredientsFromDescription(searchString, manufacturer);
 
            Assert.AreEqual(correctIngredients, returnedIngredients);
        }

       [Ignore]  // this test is dependent on the data over on the actual site.  The method needs to be replaced by an interface so it can be tested without going out to the actual data, which they keep changing.
        [TestMethod]
        [TestCategory("Websearch:Diet Coke")]
        public async Task ReturnCorrectIngredientsForDietCokeOnSearch()
        {  
            string searchString = "Diet Coke";  //   
            string correctIngredients = "Carbonated Water, Caramel Color, Aspartame, Phosphoric Acid, Natural Flavors, Citric Acid, Caffeine, Potassium Citrate, Phenylketonurics: Contains Phenylalanine, Phenylalanine"; 
            
            string returnedIngredients = await AsyncController.GetIngredientsFromDescription(searchString);
 
            Assert.AreEqual(correctIngredients.ToUpper(), returnedIngredients.ToUpper());
        }



        [Ignore]
        [TestMethod]
        public void ReturnTheCorrectErrorMessageWhenItemIsNotFoundOnSearch() =>
            
            Assert.Fail();
    }
}

/*
*   This is a query USDA used to successfully find something that my app won't, because I used the method they say to use.
* https://ndb.nal.usda.gov/ndb/search/list?SYNCHRONIZER_TOKEN=94b4dff5-91ba-4e06-b4ae-952ba594e518&SYNCHRONIZER_URI=%2Fndb%2Fsearch%2Flist&qt=&ds=&qlookup=0899000068311&manu=
* 
* and another
* https://ndb.nal.usda.gov/ndb/search/list?SYNCHRONIZER_TOKEN=101ced90-e40e-46ef-97ce-fb2268a83d3c&SYNCHRONIZER_URI=%2Fndb%2Fsearch%2Flist&qt=&ds=&qlookup=acorn+stew&manu=
*/

/*    
https://ndb.nal.usda.gov/ndb/search/ListEntity  
⦁	https://api.nal.usda.gov/ndb/V2/reports?ndbno=01009&ndbno=45202763&ndbno=35193&type=f&format=json&api_key=DEMO_KEY */


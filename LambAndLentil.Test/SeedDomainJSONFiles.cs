using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System.Collections.Generic;

namespace LambAndLentil.Test
{


    /// <summary>
    /// This is not a test.  This is to seed the Domain database with JSON files whenever necessary.
    /// </summary>
    [TestClass]
    public class SeedDomainJSONFiles
    {

        [TestMethod]
        public void SeedIngredient()
        {
            try
            {
                IRepository<Ingredient> Repo = new JSONRepository< Ingredient>();
                List<Ingredient> ListEntity= new List<Ingredient> {
            new Ingredient{ ID = 1, Name = "Egg", Description = "Fresh" },
            new Ingredient{ ID =2, Name = "AARDVARK HABENERO HOT SAUCE, UPC: 853393000030", Description = "AARDVARK HABENERO HOT SAUCE, UPC: 853393000030", IngredientsList= "TOMATOES (TOMATOES AND FIRE ROASTED TOMATOES, TOMATO JUICE, CITRIC ACID, CALCIUM CHLORIDE), WHITE WINE VINEGAR, CARROTS, WATER, YELLOW ONION, HABANERO CHILI PEPPER (HABANERO CHILI PEPPERS, WATER, SALT, CITRIC ACID), MUSTARD (DISTILLED VINEGAR, WATER, MUSTARD SEED, SALT, TURMERIC, SPICES), ORGANIC CANE SUGAR, SALT, MODIFIED FOOD STARCH, GARLIC, SUNFLOWER OIL, HERBS AND SPICES. Date Available: 09/23/2016" },
             new Ingredient{ ID = 3, Name = "Asian Stir-Fry Vegetables, 32 oz, UNPREPARED, GTIN: 00027000539835", IngredientsList= "Broccoli, Carrots, Red Bell Peppers, Water Chestnuts, Sugar Snap Peas, Mushrooms" },
           new Ingredient{ ID = 4, Name = " BANQUET Ham and Cheese Breadstick, UNPREPARED, GTIN: 00031000102104", Description = "Fresh" ,IngredientsList= "Ham and Cheese Sticks: Bleached Enriched Wheat Flour (Bleached Wheat Flour, Malted Barley Flour, Niacin, Reduced Iron, Thiamine Mononitrate, Riboflavin, Folic Acid), Water, Cooked Ham (Ham, Water, Sodium Lactate, Salt, Dextrose, Corn Syrup Solids, Sodium Phosphates, Smoke Flavoring, Sodium Diacetate, Sodium Erythorbate, Sodium Nitrite), Soybean Oil Blend (Soybean Oil, Partially Hydrogenated Soybean Oil, Citric Acid), Casein, Cheddar Flavored Bread Crumbs (Bleached Wheat Flour, Cheddar Cheese [Milk, Cheese Cultures, Salt, Enzymes], Salt, Whey, Sugar, Buttermilk Solids, Yellow Corn Flour, Yeast, Soybean Oil, Lactic Acid, Yellow 5, Yellow 6, Extractives Of Paprika), Soybean Oil, Milk Protein Concentrate, Cheddar Cheese Blend (Whey, Cheddar Cheese [{Milk, Cheese Cultures, Salt, Enzymes}, Maltodextrin, Buttermilk, Salt, Canola Oil, Sodium Phosphate, Lactic Acid], Modified Food Starch, Corn Syrup Solids, Whey, Coconut Oil, Salt, Phosphate [Calcium, Sodium, Potassium], Natural Flavor, Color [Annatto, Turmeric], Maltodextrin, Sodium Caseinate, Yeast Extract, Whey Protein Concentrate, Sugar, Citric Acid, Xanthan Gum, Mono & Diglycerides, Disodium Inosinate, Buttermilk, Nonfat Dry Milk, Lactic Acid, Calcium Lactate, Soy Lecithin, Onion), Dextrose, Modified Corn Starch, Modified Potato Starch, Sodium Aluminum Phosphate, Salt, Cheddar Cheese Flavor (Cheddar, Blue And Semi-Soft Cheese [Pasteurized Milk, Cheese Cultures, Salt, Enzymes], Water, Whey, Salt, Citric Acid), Baking Powder (Sodium Bicarbonate, Sodium Aluminum Sulfate, Cornstarch, Monocalcium Phosphate, Calcium Sulfate), Dough Conditioner (Wheat Flour, Salt, Soy Oil, L. Cysteine, Ascorbic Acid, Enzyme), Yeast (Yeast, Starch, Sorbitan Monostearate, May Contain Ascorbic Acid), Disodium Phosphate, Mozzarella Cheese Type Flavor (Cheese [Milk, Culture, Rennet, Salt], Milk Solids, Emulsifier [Disodium Phosphate]), Lactic Acid, Methylcellulose, Sorbic Acid (Preservative), Soy Flour, Paprika Annatto Blend (Natural Extractives Of Annatto Seeds And Paprika With Mono-, Di-, And Triglycerides, Soybean and/or Canola Oil, Other Natural Flavors, Tocopherol And Potassium Hydroxide), Vitamin Blend (Magnesium Oxide, Zinc Oxide, Calcium Pantothenate, Riboflavin and Vitamin B-12), Vitamin A Palmitate. Cheesy Bacon Dipping Sauce: Water, Cheddar Club Cheese (Pasteurized Cultured Milk, Salt, Enzymes, Annatto Coloring), Whey, Bacon Bits (Bacon, Water, Salt, Sodium Phosphate, Sodium Nitrate, Smoke Flavoring, May Contain: Sugar, Sodium Erythorbate, Brown Sugar, Sodium Ascorbate, Potassium Chloride, Dextrose), Modified Corn Starch, Whey Protein Concentrate, Salt, Butter (Cream, Annatto), Wheat Flour, Disodium Phosphate, Mono- and Diglycerides, Cheese Powder (Granular and Blue Cheese [Milk, Cultures, Salt, Enzymes]), Carob Bean Gum, Sunflower Oil, Maltodextrin, Soybean Oil, Citric Acid, Sodium Phosphate, Lactic Acid, Color (Corn Oil, Beta Carotene, D-alpha-Tocopherol). CONTAINS: MILK, SOY, WHEAT."},
             new Ingredient{ ID = 5, Name = "McDONALD'S, Cheeseburger" },
             new Ingredient{ ID = 6, Name = "A. BAUER'S, PREPARED MUSTARD, UPC: 723738002022",IngredientsList= "MUSTARD SEED, VINEGAR, SALT AND SPICES" },
           new Ingredient{ ID = 7, Name = "A.1., BBQ- SAUCE, UPC: 054400001785", IngredientsList= "TOMATO PUREE (WATER, TOMATO PASTE), HIGH FRUCTOSE CORN SYRUP, VINEGAR, RAISIN PASTE, SALT, CONTAINS LESS THAN 2% OF SPICE, DRIED GARLIC, NATURAL FLAVOR, DRIED ONIONS, CELERY, POTASSIUM SORBATE (TO PRESERVE FRESHNESS), CARAMEL COLOR, XANTHAN GUM " },
            new Ingredient{ ID = 8, Name = "BAKED ZITI, UPC: 782796023691", IngredientsList = "ZITI BASE [SPAGHETTI WITH MEAT SAUCE (SAUCE BASE (VINE-RIPENED FRESH TOMATOES, A BLEND OF EXTRA VIRGIN OLIVE OIL AND SUNFLOWER OIL, SALT, OREGANO, GRANULATED GARLIC, BLACK PEPPER AND NATURALLY DERIVED CITRIC ACID)), GROUND BEEF, YELLOW ONION, ITALIAN SAUSAGE (PORK, WATER, CONTAINS 2% OR LESS OF: SALT, CORN SYRUP SOLIDS, SPICES, FLAVORING, BHA, BHT, AND CITRIC ACID), SUGAR, POMACE OLIVE OIL, GARLIC (GARLIC, WATER, SOYBEAN OIL, PHOSPHORIC ACID, SODIUM BENZOATE AND POTASSIUM SORBATE (PRESERVATIVES)), SALT, BLACK PEPPER, BASIL, PARSLEY), PENNE RIGATE PASTE (SEMOLINA, NIACIN, FERROUS SULFATE (IRON), THIAMINE MONONITRATE, RIBOFLAVIN, FOLIC ACID), MOZZARELLA CHEESE (PASTEURIZED MILK, CHEESE CULTURE, SALT, ENZYMES, POWDERED CELLULOSE AND DEXTROSE (TO PREVENT CAKING))], PARMESAN CHEESE (IMPORTED PARMESAN CHEESE (PASTEURIZED MILK, CHEESE CULTURES, SALT, ENZYMES), RICE FLOUR, POWDERED CELLULOSE ADDED TO PREVENT CAKING). " }
            };

                foreach (Ingredient ingredient in ListEntity)
                {
                    Repo.Save(ingredient);
                }

            }
            catch (Exception)
            {
                Assert.Fail();
                throw; // belt and suspenders.  Probably not needed. 
            }
        }
    }
}

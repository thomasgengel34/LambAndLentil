using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LambAndLentil.Domain.Entities
{
    public class RecipeIngredient:BaseEntity
    {
        public RecipeIngredient() : base(new DateTime(2010, 1, 1))
        {

        }
        public decimal Quantity { get; set; }

        public Measurement Measurement { get; set; }

        public Ingredient Ingredient { get; set; }

        /// <summary>
        /// should return the correct quantity of whatever item is in an ingredient in the units and quantity of the recipe ingredient
        /// </summary>
        /// <param name="Quantity"></param>
        /// <param name="Ingredient"></param>
        /// <param name="RecipeIngredient"></param>
        /// <returns></returns>
        public static decimal QuantityConverter(decimal Quantity, Ingredient Ingredient, RecipeIngredient RecipeIngredient)
        {
            // this should be based on the number of grams in the particular ingredient, so you take it back to the ingredient's grams in the container and the number of servings and the number of ingredientmeasurement per serving.  Then you convert it to the new unit from the grams.  If grams is unknown, use a default conversion.
            // Here is the procedure using tablespoons, teaspoons, cups:
            // for example:          we have, in ingredients, Q with 2 tablespoons for 1 serving and 50 cals
            //                             we have a recipe with 2 cups of Q and 4 servings.  How many cals are in the recipe?
            //    ingredients has 50 cal / 2 tablespoons
            //    recipe has 2 cups.  The '4 servings' is irrelevant.  Ingredients are not necessarily directly proportionate.
            //    So we have (2 cups [Recipe]) * (16 tablespoons[Ingredient]/1 cup[Recipe])*(50 cal [Ingredient] / 2 tablespoons [Ingredient]) = 800cal in a recipe
            //       cal in a recipe =  Quantity* conversion(ingredientmeasurement, recipeingredientmeasurement)* ingredient.cal/ingredient.servingsize       2 cups*conversion*50cal/tablespoon
            //     OZ,
            //LB, 
            //FLOZ,
            //Cup,
            //Quart,
            //Gallon,
            //Gram,
            //Liter, 
            //NA,
            //unknown  


            decimal factor = UnitConvert(Ingredient, RecipeIngredient);

            // need to fence off if Ingredient.Calories or Ingredient.ServingSize are null
            return Quantity * factor   ;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ingredient"></param>
        /// <param name="RecipeIngredient"></param>
        /// <returns> (Ingredient unit of measure) / (Recipe unit of measure) </returns>
        private static decimal UnitConvert(Ingredient Ingredient, RecipeIngredient RecipeIngredient)
        {
            if (Ingredient.ServingSizeUnit.Equals(RecipeIngredient.Ingredient.ServingSizeUnit))
            {
                return 1;
            }

            if (Ingredient.ContainerSize == null || Ingredient.ContainerSizeUnit == null)
            {

            }

            // not sure there isn't a more elegant and painless way to do this, but some conversions are complex. I don't want a nested  switch statement.

            // For the ingredients table, I need to add columns servings per container and container size, container size unit, containersizeingrams to do this.  The database has null, fl oz, na, 0, OZ and unknown in containerunitsize where measurement is each. Allow that it may not be convertible (null, na, 0, unknown).    
            // Maybe the Ingredients class should do a conversion to a base factor to eliminate this mess
            decimal factor = -1;
            switch (RecipeIngredient.Measurement)
            {
                case Measurement.Each:
                    factor = CalculateEach(Ingredient);
                    break;

                case Measurement.Tablespoon:
                    // Recipe shows tablespoon, ingredient is in ??
                    factor = CalculateTablespoon(Ingredient);
                    break;
                case Measurement.Teaspoon:
                    factor=3* CalculateTablespoon(Ingredient);
                    break;
                case Measurement.Cup:
                    factor = 16 * CalculateTablespoon(Ingredient);
                    break;

                case Measurement.QuarterCup:
                    factor = 4 * CalculateTablespoon(Ingredient);
                    break;
                case Measurement.Ounce:

                    break;
                case Measurement.Pound:

                    break;
                case Measurement.teaspn14:

                    break;
                case Measurement.link:

                    break;
                case Measurement.can12oz:

                    break;
                case Measurement.FluidOz:

                    break;
                case Measurement._150g:

                    break;
                case Measurement.Meal:

                    break;
                case Measurement.Package:

                    break;
                case Measurement.NLEAServing:

                    break;
                case Measurement.Head:

                    break;
                case Measurement.Leaf:

                    break;
                case Measurement.Slice:

                    break;
                case Measurement.Cube:

                    break;
                case Measurement._6ozCcontainer:

                    break;
                case Measurement.Sheet50g:

                    break;
                case Measurement.Box:

                    break;
                case Measurement.Gram:

                    break;
                default:
                    break;
            }

            return factor;

        }

        /// <summary>
        /// Caulculate the conversion factor when RecipeIngredient.Measurement is Each from varying Ingredient.Serving Size units
        /// </summary>
        /// <param name="ingredient"></param>
        /// <returns></returns>
        private static decimal CalculateEach(Ingredient ingredient)
        {
            decimal eachFactor = 0;
            switch (ingredient.ServingSizeUnit)
            {
                case Measurement.Each:
                    eachFactor = 1;
                    break;


                // for most measurements, Each cannot be directly converted. We have whole units (each, box, meal, link, package, etc.)
                // If we have oz or grams, servings per container, we have oz/serving and we can assume 1 each = 1 serving
                // and we have volume (tablespoon, teaspoon, cup, Liter, Fl Oz, etc.
                // and we have weight (once, pound, gram) 
                // There is some ambiguity.  This should be noted as I go through this.
                // If I cannot convert something, that should be carried back and the user alerted. How? return -1??

                /*  
                    * This should be given to the user - it's on the about.cshtml
                    *    https://www.verywell.com/what-is-serving-size-3496390
                       a serving size of fruit is usually 1 medium whole fruit or ½ cup cooked, canned or dried fruit
   a serving size of vegetables is usually one cup of raw or cooked leafy greens or ½ cup of higher calorie vegetables like carrots.
   a serving size of potatoes, pasta or grains is ½ cup or one slice of bread
   a serving of dairy is one cup of skim milk or yogurt or a 1.5 ounces of cheese
   a serving size of meat, fish or poultry is about 3 ounces
   a serving size of oil or salad dressing is 2 teaspoons
   A serving size of alcohol depends on the drink that you choose.A serving of wine is 4 ounces, a serving of beer is 12 ounces and a serving of liquor is 1.5 ounces 

        use ingredient.FoodGroup for this       
        
                some things, like a diet meal, are absurd to put in a recipe for something else. How to handle?     
                    */
                    /* For now, for functionality, I am just going to do the cup/tablespoon/teaspoon routine and expand with demand. */

                case Measurement.Tablespoon:
                // serving size unit - tablespoon.  Why would a recipe call out 1 each of something like butter? That would be meaningless
                case Measurement.Teaspoon:
                case Measurement.Cup:
                case Measurement.QuarterCup:
                case Measurement.Ounce:
                case Measurement.Pound:
                case Measurement.teaspn14:
                    eachFactor = -1;
                    break;
                case Measurement.link:
                case Measurement.can12oz:
                    eachFactor = 1;
                    break;
                case Measurement.FluidOz:
                case Measurement._150g:

                    break;
                case Measurement.Meal:
                case Measurement.Package:
                case Measurement.NLEAServing:
                case Measurement.Head:
                case Measurement.Leaf:
                case Measurement.Slice:
                case Measurement.Cube:
                case Measurement._6ozCcontainer:
                case Measurement.Sheet50g:
                case Measurement.Box:
                    eachFactor = 1;
                    break;
                case Measurement.Gram:
                    eachFactor = -1;
                    break;
            }
            return eachFactor;
        }
        // Recipe shows tablespoon, ingredient is in ??
        private static decimal CalculateTablespoon(Ingredient ingredient)
        {
            decimal eachFactor = -1;
            switch (ingredient.ServingSizeUnit)
            {
                case Measurement.Each:
                    eachFactor = -1;
                    break;
                case Measurement.Tablespoon:
                    eachFactor = 1;
                    break;
                case Measurement.Teaspoon:
                    eachFactor = 3;
                    break;
                case Measurement.Cup:
                    eachFactor = 0.0616115m;
                    break;
                case Measurement.QuarterCup:
                    eachFactor = 0.25m;
                    break;
                case Measurement.Ounce:
                    break;
                case Measurement.Pound:
                    eachFactor = -1;
                    break;
                case Measurement.teaspn14: 
                    break;
                case Measurement.link:
                    break;
                case Measurement.can12oz:
                    break;
                case Measurement.FluidOz:
                    break;
                case Measurement._150g:
                    break;
                case Measurement.Meal:
                    break;
                case Measurement.Package:
                    break;
                case Measurement.NLEAServing:
                    break;
                case Measurement.Head:
                    break;
                case Measurement.Leaf:
                    break;
                case Measurement.Slice:
                    break;
                case Measurement.Cube:
                    break;
                case Measurement._6ozCcontainer:
                    break;
                case Measurement.Sheet50g:
                    break;
                case Measurement.Box:
                    break;
                case Measurement.Gram:
                    break;
                default:
                    break;
            }

            return eachFactor;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Domain.Entities
{
    public class Ingredients
    {
        public List<Ingredient> MyIngredients { get; set; }

        public Ingredients(List<Ingredient> Ingredient)
        {
            MyIngredients = Ingredient;
        }

        public  static SelectList GetListOfAllIngredientNames(List<Ingredient> MyIngredients)
        {
            var list = from n in  MyIngredients.AsQueryable()
                       select n.Name;
            SelectList descriptions = new SelectList(list);
            return descriptions;
        }


        /// <summary>
        /// Gets list of all measurements.  This rightfully belongs in a 'Measurements' class, but I don't have one and putting it here gets it out of the controllers.
        /// </summary>
        /// <returns>a list of all measurements</returns>
        public SelectList GetListOfAllMeasurements()
        {
            List<string> list = new List<string>();
            foreach (var value in Enum.GetValues(typeof(Measurement)))
            {
                list.Add(value.ToString());
            }
            SelectList measurements = new SelectList(list);
            return measurements;
        }

    }
}
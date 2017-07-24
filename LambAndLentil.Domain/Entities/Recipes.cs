using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Domain.Entities
{
    public class Recipes  
    {
        public int ID { get; set; }
        public List<Recipe> MyRecipes { get; set; }

        public Recipes(List<Recipe>Recipe)
        {
            MyRecipes = Recipe;
        }

        public static SelectList GetListOfAllRecipeNames(List<Recipe> MyRecipes)
        {
            var list = from n in MyRecipes.AsQueryable()
                       select n.Name;
            SelectList descriptions = new SelectList(list);
            return descriptions;
        }
    }
}
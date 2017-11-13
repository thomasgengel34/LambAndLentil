using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LambAndLentil.Domain.Entities
{

    [Table("RECIPE.Recipe")]
    public class Recipe : BaseEntity, IEntityChildClasses, IRecipe
    {
        public Recipe() : base() => Ingredients = new List<Ingredient>();

        public Recipe(DateTime creationDate) : base(creationDate) => CreationDate = creationDate;



        public int ID { get; set; }
        public decimal Servings { get; set; }
        public MealType MealType { get; set; }
        public int? Calories { get; set; }
        public short? CalsFromFat { get; set; }

        private new List<Recipe> Recipes { get; set; }
        private new List<Menu> Menus { get; set; }
        private new List<Plan> Plans { get; set; }
        private new List<ShoppingList> ShoppingLists { get; set; }
        private new List<Person> Persons { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LambAndLentil.Domain.Entities
{

    [Table("RECIPE.Recipe")]
    public class Recipe : BaseEntity, IRecipe
    {
        public Recipe() : base()
        {
            Ingredients = new List<Ingredient>();

            CanHaveMenuChild = false;
            CanHavePlanChild = false;
            CanHaveRecipeChild = false;
            CanHaveShoppingListChild = false;
        }

        public Recipe(DateTime creationDate) : base(creationDate) => CreationDate = creationDate;

        public int ID { get; set; }
        public decimal Servings { get; set; }
        public MealType MealType { get; set; }
        public int? Calories { get; set; }
        public short? CalsFromFat { get; set; }
        public List<Ingredient> Ingredients { get; set; }    

        void IEntity.AddChildrenToParent(IEntity entity) => throw new NotImplementedException();

        bool IEntity.ParentCanHaveChild(IPossibleChildren parent)
        {
            return parent.CanHaveRecipeChild;
        }
    }
}
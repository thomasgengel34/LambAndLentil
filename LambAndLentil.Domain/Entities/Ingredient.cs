using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LambAndLentil.Domain.Entities
{
    [Table("INGREDIENT.Ingredient")]
    public class Ingredient : BaseEntity, IEntityChildClassIngredients, IIngredient
    {
        public Ingredient() : base()
        {
            Ingredients = new List<Ingredient>();  
            CanHaveMenuChild = false; 
            CanHavePlanChild = false;
            CanHaveRecipeChild = false;
            CanHaveShoppingListChild = false;
        }

        public Ingredient(DateTime creationDate) : this() => CreationDate = creationDate;


        public int  ID { get; set; }
        public List<Ingredient> Ingredients { get; set; }  
        

        bool  IEntity.ParentCanHaveChild(IPossibleChildren parent)
        {
            return parent.CanHaveIngredientChild;
        }

        public bool  ParentCanHaveChild(IPossibleChildren parent)
        {
            return parent.CanHaveIngredientChild;
        }

        void IEntity.AddChildrenToParent(IEntity entity) => throw new NotImplementedException();
    }
}
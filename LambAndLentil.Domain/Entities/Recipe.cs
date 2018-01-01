using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

        void IEntity.AddChildToParent(IEntity parent, IEntity child)
        {
            ((IEntityChildClassRecipes)parent).Recipes.Add((Recipe)child);
        }

        bool IEntity.ParentCanHaveChild(IPossibleChildren parent)
        {
            return parent.CanHaveRecipeChild;
        }

        public void ParentRemoveAllChildrenOfAType( IEntity parent, IEntity child) => ((IEntityChildClassRecipes)parent).Recipes.Clear();


        public IEntity  RemoveSelectionFromChildren<TChild>(IEntity  parent, List<TChild> selected)
            where TChild : BaseEntity, IEntity, IPossibleChildren, new()
        {
            var setToRemove = new HashSet<TChild>(selected);
             
           ((IEntityChildClassRecipes)parent).Recipes.RemoveAll(ContainsSelected);
            return parent;

            bool ContainsSelected(IEntity item)
            {
                int itemID = item.ID;
                var numbers = from f in selected select f.ID;
                bool trueOrFalse = numbers.Contains(itemID);
                return trueOrFalse;
            }

        }

        int IEntity.GetCountOfChildrenOnParent(IEntity parent)
        {
            try
            {
                return ((IEntityChildClassRecipes)parent).Recipes.Count();
            }
            catch (InvalidCastException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
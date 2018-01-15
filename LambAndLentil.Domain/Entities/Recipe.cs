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


        public decimal Servings { get; set; }
        public MealType MealType { get; set; }
        public int? Calories { get; set; }
        public short? CalsFromFat { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public int ID { get; set; }

        bool ParentCanHaveChild(IPossibleChildren parent)
        {
            return parent.CanHaveRecipeChild;
        }

        public void ParentRemoveAllChildrenOfAType(IEntity parent, IEntity child) => ((IEntityChildClassRecipes)parent).Recipes.Clear();


        public IEntity RemoveSelectionFromChildren<TChild>(IEntityChildClassIngredients parent, List<TChild> selected)
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

        bool IEntity.ParentCanHaveChild(IEntity parent)   // TODO: replace parameter with type or make generic
        {  // TODO: eliminate repetition in other modules
            Type type = parent.GetType();   // this line should be eliminated when parameter is changed
            if ( type ==typeof(Ingredient))
            {
                // return parent.CanHaveIngredientChild;    // TODO: is this property really needed??
                return true;
            } 
            else return false;
        }

        void IEntity.ParentRemoveAllChildrenOfAType(IEntity parent, IEntity child) => throw new NotImplementedException();
        IEntity IEntity.RemoveSelectionFromChildren<TChild>(IEntityChildClassRecipes parent, List<TChild> selected)
        {

            var allRecipeIDs = parent.Recipes.Select(a => a.ID);

            var setToRemove = new HashSet<TChild>(selected).Where(p => p != null).Select(r => r.ID).AsQueryable();

            var remainingIDs = allRecipeIDs.Except(setToRemove);
            var remainingChildren = parent.Recipes.Where(b => remainingIDs.Contains(b.ID));
            parent.Recipes = remainingChildren.ToList();
            return parent;
        }
    }
}
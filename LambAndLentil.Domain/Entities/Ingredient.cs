using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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



        public List<Ingredient> Ingredients { get; set; }


        public int ID { get; set; } 

        bool IEntity.ParentCanHaveChild(IEntity parent)
        {
            return true;
        } 

        public void ParentRemoveAllChildrenOfAType(IEntity parent, IEntity child)
        {
            ((IEntityChildClassIngredients)parent).Ingredients.Clear();
        }

        int IEntity.GetCountOfChildrenOnParent(IEntity parent)
        {
            try
            {
                return ((IEntityChildClassIngredients)parent).Ingredients.Count();
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
        void IEntity.ParentRemoveAllChildrenOfAType(IEntity parent, IEntity child) => ((IEntityChildClassIngredients)parent).Ingredients.Clear();



        IEntity IEntity.RemoveSelectionFromChildren<TChild>(IEntityChildClassIngredients parent, List<TChild> selected)
        {
            var allIngredientIDs = parent.Ingredients.Select(a => a.ID);

            var setToRemove = new HashSet<TChild>(selected).Where(p => p != null).Select(r => r.ID).AsQueryable();

            var remainingIDs = allIngredientIDs.Except(setToRemove);
            var remainingChildren = parent.Ingredients.Where(b => remainingIDs.Contains(b.ID));
            parent.Ingredients = remainingChildren.ToList();
            return parent;
        }

        IEntity IEntity.RemoveSelectionFromChildren<TChild>(IEntityChildClassRecipes parent, List<TChild> selected) => throw new NotImplementedException();
    }
}
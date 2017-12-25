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


        public int ID { get; set; }
        public List<Ingredient> Ingredients { get; set; }


        bool IEntity.ParentCanHaveChild(IPossibleChildren parent)
        {
            return parent.CanHaveIngredientChild;
        }

        public bool ParentCanHaveChild(IPossibleChildren parent)
        {
            return parent.CanHaveIngredientChild;
        }

        void IEntity.AddChildToParent(IEntity parent, IEntity child)
        {
            ((IEntityChildClassIngredients)parent).Ingredients.Add((Ingredient)child);
        }
       

        public void ParentRemoveAllChildrenOfAType(IEntity parent, IEntity child)
        {
            ((IEntityChildClassIngredients)parent).Ingredients.Clear();
        }

        public IEntity RemoveSelectionFromChildren<TChild>(IEntity parent, List<TChild> selected)
            where TChild : BaseEntity, IEntity, IPossibleChildren, new()
        {
            var setToRemove = new HashSet<TChild>(selected);
            ((IEntityChildClassIngredients)parent).Ingredients.RemoveAll(ContainsSelected);
            return parent;

            bool ContainsSelected(IEntity item)
            {
                if (item == null)
                {
                    return true;
                }
                else
                {
                    int itemID = item.ID;
                    var newSet = from f in selected where f!= null select f;

                    var numbers = from f in newSet select f.ID;
                    bool trueOrFalse = numbers.Contains(itemID);
                    return trueOrFalse;
                } 
            } 
        }
    }
}
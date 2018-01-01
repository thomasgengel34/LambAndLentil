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
        List<Ingredient> IEntityChildClassIngredients.Ingredients { get; set; }
        string IEntity.AddedByUser { get; set; }
        DateTime IEntity.CreationDate { get; set; }
        int IEntity.ID { get; set; }
        string IEntity.ModifiedByUser { get; set; }
        DateTime IEntity.ModifiedDate { get; set; }
        string IEntity.Name { get; set; }
        string IEntity.Description { get; set; }
        string IEntity.IngredientsList { get; set; }

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
                    var newSet = from f in selected where f != null select f;

                    var numbers = from f in newSet select f.ID;
                    bool trueOrFalse = numbers.Contains(itemID);
                    return trueOrFalse;
                }
            }
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

        IEntity IEntity.RemoveSelectionFromChildren<TChild>(IEntity parent, List<TChild> selected)
        {
            var setToRemove = new HashSet<TChild>(selected).Where(p => p != null);
            ((IEntityChildClassIngredients)parent).Ingredients.RemoveAll(ContainsSelected);
            return parent;

            bool ContainsSelected(IEntity item)
            { 
                int itemID = ((Ingredient)item).ID;
                //      var numbers = from f in selected select  f.ID;
                //      bool trueOrFalse = numbers.Contains(itemID);

                bool trueOrFalse = setToRemove.Contains(item);

                return trueOrFalse; 
            }
        }
    }
}
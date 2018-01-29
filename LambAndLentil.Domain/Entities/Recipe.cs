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
        }

        public Recipe(DateTime creationDate) : base(creationDate) => CreationDate = creationDate;


        public decimal Servings { get; set; }
        public MealType MealType { get; set; }
        public int? Calories { get; set; }
        public short? CalsFromFat { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public int ID { get; set; } 
       
        List<Recipe> IEntity.Recipes { get; set; } = null;
        List<Menu> IEntity.Menus { get; set; } = null;
        List<Plan> IEntity.Plans { get; set; } = null;
        List<ShoppingList> IEntity.ShoppingLists { get; set; } = null;

        bool IEntity.CanHaveChild(IEntity child)
        {
            Type type = child.GetType();

            List<Type> possibleChildren = new List<Type>()
            {
                typeof(Ingredient)
            };

            if (possibleChildren.Contains(type))
            {
                return true;
            }
            return false;
        }

        public override bool CanHaveChild(IEntity child)
        {
            Type type = child.GetType();

            List<Type> possibleChildren = new List<Type>()
            {
                typeof(Ingredient)
            };

            if (possibleChildren.Contains(type))
            {
                return true;
            }
            return false;
        }

        public void ParentRemoveAllChildrenOfAType(IEntity parent, IEntity child) => ((IEntity)parent).Recipes.Clear();


        public IEntity RemoveSelectionFromChildren<TChild>(IEntity parent, List<TChild> selected)
            where TChild : BaseEntity, IEntity, new()
        {
            var setToRemove = new HashSet<TChild>(selected);

            ((IEntity)parent).Recipes.RemoveAll(ContainsSelected);
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
                return ((IEntity)parent).Recipes.Count();
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
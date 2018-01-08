using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace LambAndLentil.Domain.Entities
{
    [Table("MENU.Menu")]
    public class Menu : BaseEntity,IMenu

    {

        public Menu() : base()
        {
            Ingredients = new List<Ingredient>();
            Recipes = new List<Recipe>();
            CanHaveMenuChild = false;
            CanHavePlanChild = false;
            CanHaveRecipeChild = true;
            CanHaveShoppingListChild = false;
        }

        public Menu(DateTime creationDate) : base(creationDate) => CreationDate = creationDate;

     
        public MealType MealType { get; set; }
        public DayOfWeek DayOfWeek { get; set; } 
        public int Diners { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Recipe> Recipes { get; set; }  
        
        string IEntity.AddedByUser { get; set; } 

         void IEntity.AddChildToParent(IEntity parent, IEntity child)
        {
            ((IEntityChildClassMenus)parent).Menus.Add((Menu)child);
        }

        bool IEntity.ParentCanHaveChild(IPossibleChildren parent)
        {
            return parent.CanHaveMenuChild;
        }

        public void ParentRemoveAllChildrenOfAType(IEntity  parent, IEntity child)
        {
            ((IEntityChildClassMenus)parent).Menus.Clear();
        }


        public IEntity  RemoveSelectionFromChildren<TChild>(IEntity  parent, List<TChild> selected)
            where TChild : BaseEntity, IEntity, IPossibleChildren, new()
        {
            var setToRemove = new HashSet<TChild>(selected);
            ((IEntityChildClassMenus)parent).Menus.RemoveAll(ContainsSelected);
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
                return ((IEntityChildClassMenus)parent).Menus.Count();
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

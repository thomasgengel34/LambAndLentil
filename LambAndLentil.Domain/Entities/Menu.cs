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
        public int ID { get; set; }
        DayOfWeek IMenu.DayOfWeek { get; set; }
        int IMenu.Diners { get; set; }
        MealType IMenu.MealType { get; set; }
        List<Recipe> IEntityChildClassRecipes.Recipes { get; set; }
        string IEntity.AddedByUser { get; set; }
        DateTime IEntity.CreationDate { get; set; }
        int IEntity.ID { get; set; }
        string IEntity.ModifiedByUser { get; set; }
        DateTime IEntity.ModifiedDate { get; set; }
        string IEntity.Name { get; set; }
        string IEntity.Description { get; set; }
        string IEntity.IngredientsList { get; set; }
        List<Ingredient> IEntity.Ingredients { get; set; }

        void  AddChildToParent(IEntity parent, IEntity child)
        {
            ((IEntityChildClassMenus)parent).Menus.Add((Menu)child);
        }

        bool  ParentCanHaveChild(IPossibleChildren parent)
        {
            return parent.CanHaveMenuChild;
        }

        public void ParentRemoveAllChildrenOfAType(IEntity  parent, IEntity child)
        {
            ((IEntityChildClassMenus)parent).Menus.Clear();
        }
       

        public IEntity  RemoveSelectionFromChildren<TChild>(IEntityChildClassIngredients  parent, List<TChild> selected)
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

        

        bool IEntity.ParentCanHaveChild(IEntity parent) => parent.CanHaveMenuChild;




        IEntity IEntity.RemoveSelectionFromChildren<TChild>(IEntityChildClassIngredients parent, List<TChild> selected) => throw new NotImplementedException();
        void IEntity.ParentRemoveAllChildrenOfAType(IEntity parent, IEntity child) => throw new NotImplementedException();
        IEntity IEntity.RemoveSelectionFromChildren<TChild>(IEntityChildClassRecipes parent, List<TChild> selected) => throw new NotImplementedException();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace LambAndLentil.Domain.Entities
{
    [Table("MENU.Menu")]
    public class Menu : BaseEntity, IMenu

    {

        public Menu() : base()
        {
            Ingredients = new List<Ingredient>();
            Recipes = new List<Recipe>();
            ClassName = "Menu";
            DisplayName = "Menu";
        }

        public Menu(DateTime creationDate) : base(creationDate) => CreationDate = creationDate;


        public MealType MealType { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public int Diners { get; set; }

        public int ID { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        new List<Recipe> Recipes { get; set; }
        new List<Menu> Menus { get; set; } = null;
        new List<Plan> Plans { get; set; } = null;
       public List<ShoppingList> ShoppingLists { get; set; } = null;

        void AddChildToParent(IEntity parent, IEntity child)
        {
            parent.Menus.Add((Menu)child);
        }

        bool IEntity.CanHaveChild(IEntity child)
        {
            Type type = child.GetType();
            if (type == typeof(Ingredient) || type == typeof(Recipe))
            {
                return true;
            }
            return false;
        }

        public override bool CanHaveChild(IEntity child)
        {
            Type type = child.GetType();
            if (type == typeof(Ingredient) || type == typeof(Recipe))
            {
                return true;
            }
            return false;
        }
        public void ParentRemoveAllChildrenOfAType(IEntity parent, IEntity child)
        {
            ((IEntity)parent).Menus.Clear();
        }


        public IEntity RemoveSelectionFromChildren<TChild>(IEntity parent, List<TChild> selected)
            where TChild : BaseEntity, IEntity, new()
        {
            var setToRemove = new HashSet<TChild>(selected);
            ((IEntity)parent).Menus.RemoveAll(ContainsSelected);
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
                return ((IEntity)parent).Menus.Count();
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

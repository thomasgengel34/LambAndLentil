using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq; 


namespace LambAndLentil.Domain.Entities
{
    [Table("SHOPPINGLIST.ShoppingList")]
    public class ShoppingList : BaseEntity, IShoppingList
    {
        public ShoppingList() : base()
        {
            Ingredients = new List<Ingredient>();
            Recipes = new List<Recipe>();
            Menus = new List<Menu>();
            Plans = new List<Plan>();

            CanHaveMenuChild =true;
            CanHavePlanChild = true;
            CanHaveRecipeChild = true;
            CanHaveShoppingListChild = false;
        }

        public ShoppingList(DateTime creationDate) : base(creationDate)
        {
            CreationDate = creationDate;
            Date = creationDate;
        }

        public int ID { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<Menu> Menus { get; set; }
        public List<Plan> Plans { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        string IShoppingList.Author { get; set; }
        DateTime IShoppingList.Date { get; set; }


        void IEntity.AddChildToParent(IEntity parent, IEntity child)
        {
            ((IEntityChildClassShoppingLists)parent).ShoppingLists.Add((ShoppingList)child);
        }

        bool IEntity.ParentCanHaveChild(IPossibleChildren parent)
        {
            return parent.CanHaveShoppingListChild;
        }

        public void ParentRemoveAllChildrenOfAType(IEntity  parent, IEntity child)
        {
            ((IEntityAllChildren)parent).ShoppingLists.Clear();
        }


        public IEntity  RemoveSelectionFromChildren<TChild>(IEntity parent, List<TChild> selected)
            where TChild : BaseEntity, IEntity, IPossibleChildren, new()
        {
            var setToRemove = new HashSet<TChild>(selected);
            ((IEntityChildClassShoppingLists)parent).ShoppingLists.RemoveAll(ContainsSelected);
            return parent;

            bool ContainsSelected(IEntity item)
            {
                int itemID = item.ID;
                var numbers = from f in selected select f.ID;
                bool trueOrFalse = numbers.Contains(itemID);
                return trueOrFalse;
            } 
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace LambAndLentil.Domain.Entities
{
    [Table("SHOPPINGLIST.ShoppingList")]
    public class ShoppingList : BaseEntity, IEntity
    {
        public ShoppingList() : base()
        {
            List<Ingredient> Ingredients = new List<Ingredient>(); 
                List<Recipe> Recipes = new List<Recipe>(); 
                List<Menu> Menus = new List<Menu>();
                List<Plan> Plans = new List<Plan>();
                List<ShoppingList> ShoppingLists = new List<ShoppingList>(); 
            ClassName =  "ShoppingList";
            DisplayName =  "Shopping List";
        }

        public ShoppingList(DateTime creationDate) : base(creationDate)
        {
            CreationDate = creationDate;
            Date = creationDate;
        }



        public DateTime Date { get; set; }
        public string Author { get; set; }
        public int ID { get; set; }

        public List<Ingredient> Ingredients { get; set; }
        new List<Recipe> Recipes { get; set; }
        new List<Menu> Menus { get; set; }
        new List<Plan> Plans { get; set; }
        public List<ShoppingList> ShoppingLists { get; set; } = null;

        bool IEntity.CanHaveChild(IEntity child)
        {
            Type type = child.GetType();

            List<Type> possibleChildren = new List<Type>()
            {
                typeof(Ingredient),
                typeof(Recipe),
                typeof(Menu),
                typeof(Plan)
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
                typeof(Ingredient),
                typeof(Recipe),
                typeof(Menu),
                typeof(Plan)
            };

            if (possibleChildren.Contains(type))
            {
                return true;
            }
            return false;
        }

        public void ParentRemoveAllChildrenOfAType(IEntity parent, IEntity child)
        {
            ((IEntity)parent).ShoppingLists.Clear();
        }


        public IEntity RemoveSelectionFromChildren<TChild>(IEntity parent, List<TChild> selected)
            where TChild : BaseEntity, IEntity, new()
        {
            var setToRemove = new HashSet<TChild>(selected);
            ((IEntity)parent).ShoppingLists.RemoveAll(ContainsSelected);
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
                return ((IEntity)parent).ShoppingLists.Count();
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
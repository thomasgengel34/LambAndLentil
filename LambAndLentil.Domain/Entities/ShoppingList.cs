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

      
        public List<Ingredient> Ingredients { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<Menu> Menus { get; set; }
        public List<Plan> Plans { get; set; }
        public List<ShoppingList> ShoppingLists { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }  
        public int  ID { get; set; }
        

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

        public void ParentRemoveAllChildrenOfAType(IEntity  parent, IEntity child)
        {
            ((IEntity)parent).ShoppingLists.Clear();
        }


        public IEntity  RemoveSelectionFromChildren<TChild>(IEntity parent, List<TChild> selected)
            where TChild : BaseEntity, IEntity,  new()
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
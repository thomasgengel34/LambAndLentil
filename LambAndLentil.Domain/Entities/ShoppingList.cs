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
        public DateTime Date { get; set; }
        public string Author { get; set; }  
        public int  ID { get; set; }
        string IShoppingList.Author { get; set; }
        DateTime IShoppingList.Date { get; set; }
        List<Menu> IEntityChildClassMenus.Menus { get; set; }
        List<Plan> IEntityChildClassPlans.Plans { get; set; }
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

        bool  ParentCanHaveChild(IPossibleChildren parent)
        {
            return parent.CanHaveShoppingListChild;
        }

        public void ParentRemoveAllChildrenOfAType(IEntity  parent, IEntity child)
        {
            ((IEntityChildClassShoppingLists)parent).ShoppingLists.Clear();
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

        int IEntity.GetCountOfChildrenOnParent(IEntity parent)
        {
            try
            {
                return ((IEntityChildClassShoppingLists)parent).ShoppingLists.Count();
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

        void IEntity.ParentRemoveAllChildrenOfAType(IEntity parent, IEntity child)  => ((IEntityChildClassShoppingLists)parent).ShoppingLists.Clear(); 
        
        IEntity IEntity.RemoveSelectionFromChildren<TChild>(IEntityChildClassIngredients parent, List<TChild> selected) 
        {
            var allIngredientIDs = parent.Ingredients.Select(a => a.ID);

            var setToRemove = new HashSet<TChild>(selected).Where(p => p != null).Select(r => r.ID).AsQueryable();

            var remainingIDs = allIngredientIDs.Except(setToRemove);
            var remainingChildren = parent.Ingredients.Where(b => remainingIDs.Contains(b.ID));
            parent.Ingredients = remainingChildren.ToList();
            return parent;
        }
        bool IEntity.ParentCanHaveChild(IEntity parent) => parent.CanHaveShoppingListChild;
        IEntity IEntity.RemoveSelectionFromChildren<TChild>(IEntityChildClassRecipes parent, List<TChild> selected) => throw new NotImplementedException();
    }
}
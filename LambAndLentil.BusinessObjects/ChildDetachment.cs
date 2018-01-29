using System;
using System.Collections.Generic;
using System.Linq;
using LambAndLentil.Domain.Entities;
using EntityType = LambAndLentil.BusinessObjects.IEntityTypes.IEntityType;

namespace LambAndLentil.BusinessObjects
{
    public class ChildDetachment
    {
        private IEntity _parent; 
       
        public IEntity DetachAllChildrenOfAType(IEntity parent, Type type)
        {
            _parent = parent; 

            string typeAsString = type.ToString().Split('.').Last();
            Enum.TryParse(typeAsString, out EntityType typeAsEnumMember);

            switch (typeAsEnumMember)
            {
                case EntityType.Ingredient:
                    _parent.Ingredients.Clear();
                    break;
                case EntityType.Recipe:
                    _parent.Recipes.Clear();
                    break;
                case EntityType.Menu:
                    _parent.Menus.Clear();
                    break;
                case EntityType.Plan:
                    _parent.Plans.Clear();
                    break;
                case EntityType.ShoppingList:
                    _parent.ShoppingLists.Clear();
                    break;
                default:
                    break;
            }

            parent = _parent;
            return parent;
        }



        public IEntity DetachSelectionFromChildren(IEntity parent, List<IEntity> selected) 
        {
            _parent = parent;
           Type  childType = selected.First().GetType(); 
             

            if ( childType == typeof(Ingredient))
            {
                var allIngredientIDs = _parent.Ingredients.Select(a => a.ID);

                var setToDetach = new HashSet<IEntity>(selected).Where(p => p != null).Select(r => r.ID).AsQueryable();

                var remainingIDs = allIngredientIDs.Except(setToDetach);
                var remainingChildren = _parent.Ingredients.Where(b => remainingIDs.Contains(b.ID));
                _parent.Ingredients = remainingChildren.ToList();
                parent = _parent;
                return parent;
            }
            else if (childType == typeof(Recipe))
            {
                var allRecipeIDs = _parent.Recipes.Select(a => a.ID);

                var setToDetach = new HashSet<IEntity>(selected).Where(p => p != null).Select(r => r.ID).AsQueryable();

                var remainingIDs = allRecipeIDs.Except(setToDetach);
                var remainingChildren = _parent.Recipes.Where(b => remainingIDs.Contains(b.ID));
                _parent.Recipes = remainingChildren.ToList();
                parent = _parent;
                return parent;

            }
            else if (childType == typeof(Menu))
            {
                var allMenuIDs = _parent.Menus.Select(a => a.ID);

                var setToDetach = new HashSet<IEntity>(selected).Where(p => p != null).Select(r => r.ID).AsQueryable();

                var remainingIDs = allMenuIDs.Except(setToDetach);
                var remainingChildren = _parent.Menus.Where(b => remainingIDs.Contains(b.ID));
                _parent.Menus = remainingChildren.ToList();
                parent = _parent;
                return parent;

            }
            else if (childType == typeof(Plan))
            {
                var allPlanIDs = _parent.Plans.Select(a => a.ID);

                var setToDetach = new HashSet<IEntity>(selected).Where(p => p != null).Select(r => r.ID).AsQueryable();

                var remainingIDs = allPlanIDs.Except(setToDetach);
                var remainingChildren = _parent.Plans.Where(b => remainingIDs.Contains(b.ID));
                _parent.Plans = remainingChildren.ToList();
                parent = _parent;
                return parent;

            }
            else if (childType == typeof(ShoppingList))
            {
                var allShoppingListIDs = _parent.ShoppingLists.Select(a => a.ID);

                var setToDetach = new HashSet<IEntity>(selected).Where(p => p != null).Select(r => r.ID).AsQueryable();

                var remainingIDs = allShoppingListIDs.Except(setToDetach);
                var remainingChildren = _parent.ShoppingLists.Where(b => remainingIDs.Contains(b.ID));
                _parent.ShoppingLists = remainingChildren.ToList();
                parent = _parent;
                return parent;

            }
            else if (childType == typeof(Person))
            {
                throw new Exception("Person cannot be a child");
            }
            else throw new NotImplementedException();
        }

        public IEntity DetachAnIndependentChild(IEntity parent, IEntity child) 
        {  
            _parent = parent;
            List<IEntity> selected = new List<IEntity>() {  child };
            _parent = DetachSelectionFromChildren(_parent, selected);
            parent = _parent;
            return parent;
        }
    }
}  
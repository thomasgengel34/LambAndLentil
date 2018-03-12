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

        public IEntity DetachAllChildrenOfAType(IEntity parent, IEntity child)
        {
            _parent = parent;

            bool parentCanHaveChild = _parent.CanHaveChild(child);

            if (parentCanHaveChild)
            {

                string typeAsString = child.GetType().ToString().Split('.').Last();
                Enum.TryParse(typeAsString, out EntityType typeAsEnumMember);

                switch (typeAsEnumMember)
                {
                    case EntityType.Ingredient:
                        ClearIngredientChildren();
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
            else throw new Exception("You can't attach that!");
        }

        private void ClearIngredientChildren()
        {
            if (_parent.Ingredients == null)
            {
                _parent.Ingredients = new List<Ingredient>();
            }
            _parent.Ingredients.Clear();
        }

        public IEntity DetachSelectionFromChildren(IEntity parent, List<IEntity> selected)
        {
            _parent = parent;
            Type childType = selected.First().GetType();


            if (childType == typeof(Ingredient))
            {
                if (_parent.Ingredients.Count > 0)
                { 
                    var allIngredientIDs = from c in _parent.Ingredients
                                           where c != null
                                           select c.ID;

                    var setToDetach = new HashSet<IEntity>(selected).Where(p => p != null).Select(r => r.ID).AsQueryable();

                    var remainingIDs = allIngredientIDs.Except(setToDetach);

                    var remainingChildren = _parent.Ingredients.Where(a => a != null).Where(b => remainingIDs.Contains(b.ID));
                    _parent.Ingredients = remainingChildren.ToList();

                }
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
                if (parent.ShoppingLists == null)
                {
                    parent.ShoppingLists = new List<ShoppingList>();
                }

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
            List<IEntity> selected = new List<IEntity>() { child };
            _parent = DetachSelectionFromChildren(_parent, selected);
            parent = _parent;
            return parent;
        }
    }
}
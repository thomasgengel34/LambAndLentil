using LambAndLentil.Domain.Entities;
using System;

namespace LambAndLentil.BusinessObjects
{
    public class ChildAttachment
    {
        public IEntity AddChildToParent(IEntity parent, IEntity child) 
        {
            Type type = child.GetType();
            {   // TODO: avoid mundane repetition  
                if ( type  == typeof(Ingredient))
                { 
                    parent.Ingredients.Add(child as Ingredient);
                    return parent;
                }
                if (type  == typeof(Menu))
                {
                    return AddMenu(parent, child);
                }
                if (type  == typeof(Recipe))
                {
                    return  AddRecipe(parent, child);
                }
                if (type  == typeof(Plan))
                {
                    return  AddPlan(parent, child);
                }
                if (type  == typeof(ShoppingList))
                {
                    return  AddShoppingList(parent, child);
                }
                return parent;
            }
        }

        private static IEntity AddShoppingList(IEntity parent, IEntity child) 
        {
            parent.ShoppingLists.Add(child as ShoppingList);
            return parent;
        }

        private static IEntity AddPlan(IEntity parent, IEntity child) 
        {
            parent.Plans.Add( child );
            return parent;
        }

        private static IEntity AddRecipe(IEntity parent, IEntity child) 
        {
            parent.Recipes.Add(child as Recipe);
            return parent;
        }

        private static IEntity AddMenu(IEntity parent, IEntity child) 
        {
             parent.Menus.Add(child as Menu);
            return parent;
        }
    }
}

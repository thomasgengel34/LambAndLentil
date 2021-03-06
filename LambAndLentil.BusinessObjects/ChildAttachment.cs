﻿using LambAndLentil.Domain.Entities;
using System;
using System.Collections.Generic;

namespace LambAndLentil.BusinessObjects
{
    public class ChildAttachment
    {
        public IEntity AddChildToParent(IEntity parent, IEntity child)
        {
            Type type = child.GetType();
            {   // TODO: avoid mundane repetition  
                if (type == typeof(Ingredient))
                {
                    if (parent.Ingredients == null)
                    {
                        parent.Ingredients = new List<Ingredient>();
                    }
                    parent.Ingredients.Add(child as Ingredient);
                    return parent;
                }
                if (type == typeof(Menu))
                {
                    return AddMenu(parent, child);
                }
                if (type == typeof(Recipe))
                {
                    return AddRecipe(parent, child);
                }
                if (type == typeof(Plan))
                {
                    return AddPlan(parent, child);
                }
                if (type == typeof(ShoppingList))
                {
                    return AddShoppingList(parent, child);
                }
                return parent;
            }
        }

        private static IEntity AddShoppingList(IEntity parent, IEntity child)
        {
            if (parent.CanHaveChild(child))
            {
                parent.ShoppingLists.Add(child as ShoppingList);
            }
            return parent;
        }

        private static IEntity AddPlan(IEntity parent, IEntity child)
        {
            if (parent.CanHaveChild(child))
            {
                parent.Plans.Add((Plan)child);
            }
            return parent;
        }

        private static IEntity AddRecipe(IEntity parent, IEntity child)
        {
            if (parent.CanHaveChild(child))
            {
                parent.Recipes.Add((Recipe)child);
            } 
            return parent;
        }

        private static IEntity AddMenu(IEntity parent, IEntity child)
        {
            if (parent.CanHaveChild(child))
            {
                parent.Menus.Add((Menu)child);
            }
            return parent;
        }
    }
}

using LambAndLentil.Domain.Entities;

namespace LambAndLentil.BusinessObjects
{
    public class ChildAttachment
    {
        public TParent AddChildToParent<TParent, TChild>(TParent parent, TChild child)
            where TParent : BaseEntity, IEntity
            where TChild : BaseEntity, IEntity
        {
            {   // TODO: avoid mundane repetition  
                if (typeof(TChild) == typeof(Ingredient))
                {
                    parent.Ingredients.Add(child as Ingredient);
                    return parent;
                }
                if (typeof(TChild) == typeof(Menu))
                {
                    return AddMenu(parent, child);
                }
                if (typeof(TChild) == typeof(Recipe))
                {
                    return AddRecipe(parent, child);
                }
                if (typeof(TChild) == typeof(Plan))
                {
                    return AddPlan(parent, child);
                }
                if (typeof(TChild) == typeof(ShoppingList))
                {
                    return AddShoppingList(parent, child);
                }
                return parent;
            }
        }

        private static TParent AddShoppingList<TParent, TChild>(TParent parent, TChild child)
            where TParent : BaseEntity, IEntity
            where TChild : BaseEntity, IEntity
        {
            parent.ShoppingLists.Add(child as ShoppingList);
            return parent;
        }

        private static TParent AddPlan<TParent, TChild>(TParent parent, TChild child)
            where TParent : BaseEntity, IEntity
            where TChild : BaseEntity, IEntity
        {
            parent.Plans.Add(child as Plan);
            return parent;
        }

        private static TParent AddRecipe<TParent, TChild>(TParent parent, TChild child)
            where TParent : BaseEntity, IEntity
            where TChild : BaseEntity, IEntity
        {
            parent.Recipes.Add(child as Recipe);
            return parent;
        }

        private static TParent AddMenu<TParent, TChild>(TParent parent, TChild child)
            where TParent : BaseEntity, IEntity
            where TChild : BaseEntity, IEntity
        {
             parent.Menus.Add(child as Menu);
            return parent;
        }
    }
}

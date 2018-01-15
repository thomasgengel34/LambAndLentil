using System;
using System.Collections.Generic;

namespace LambAndLentil.Domain.Entities
{
    public interface IEntity:IPossibleChildren
    {
        string AddedByUser { get; set; }
        DateTime CreationDate { get; set; }
        int ID { get; set; }
        string ModifiedByUser { get; set; }
        DateTime ModifiedDate { get; set; }
        string Name { get; set; }
        string Description { get; set; } 
        string IngredientsList { get; set; }
        List<Ingredient> Ingredients { get; set; }

        bool ParentCanHaveChild(IEntity parent );
   
        int GetCountOfChildrenOnParent(IEntity parent );
        void ParentRemoveAllChildrenOfAType(IEntity  parent, IEntity child);
        IEntity  RemoveSelectionFromChildren<TChild>(IEntityChildClassIngredients  parent, List<TChild> selected)
            where TChild : BaseEntity, IEntity, IPossibleChildren, new();

        IEntity  RemoveSelectionFromChildren<TChild>(IEntityChildClassRecipes parent, List<TChild> selected) where TChild : BaseEntity, IEntity, IPossibleChildren, new();
    }
}
 
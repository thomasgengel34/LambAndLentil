using System;
using System.Collections.Generic;

namespace LambAndLentil.Domain.Entities
{
    public interface IEntity
    {
        string AddedByUser { get; set; }
        DateTime CreationDate { get; set; }
        int ID { get; set; }
        string ModifiedByUser { get; set; }
        DateTime ModifiedDate { get; set; }
        string Name { get; set; }
        string Description { get; set; } 
        string IngredientsList { get; set; }

        bool ParentCanHaveChild(IPossibleChildren parent );
        void AddChildToParent(IEntity parent,IEntity child);
        int GetCountOfChildrenOnParent(IEntity parent );
        void ParentRemoveAllChildrenOfAType(IEntity  parent, IEntity child);
        IEntity  RemoveSelectionFromChildren<TChild>(IEntity  parent, List<TChild> selected)
            where TChild : BaseEntity, IEntity, IPossibleChildren, new();
    }
}
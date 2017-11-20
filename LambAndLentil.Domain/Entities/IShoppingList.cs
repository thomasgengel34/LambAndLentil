using System;
using System.Collections.Generic;

namespace LambAndLentil.Domain.Entities
{
    public interface IShoppingList: IEntityChildClassIngredients,
        IEntityChildClassMenus,
        IEntityChildClassPlans,
        IEntityChildClassRecipes
    {
        string Author { get; set; }
        DateTime Date { get; set; }  
    }
}
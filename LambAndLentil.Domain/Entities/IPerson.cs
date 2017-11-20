using System;
using System.Collections.Generic;

namespace LambAndLentil.Domain.Entities
{
    public interface IPerson:IEntityChildClassIngredients, IEntityChildClassRecipes, IEntityChildClassMenus, IEntityChildClassPlans, IEntityChildClassShoppingLists
    { 
          string FirstName { get; set; }
          string LastName { get; set; }
          string FullName { get; set; }
          decimal Weight { get; set; }


          int MinCalories { get; set; }
          int MaxCalories { get; set; }
          bool NoGarlic { get; set; }
    }
}
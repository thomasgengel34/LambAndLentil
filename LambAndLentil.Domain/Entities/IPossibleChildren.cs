using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.Domain.Entities
{
    public  interface IPossibleChildren
    {
        bool CanHaveIngredentChild { get; set; }
        bool CanHaveMenuChild { get; set; }
        bool CanHavePersonChild { get; set; }
        bool CanHavePlanChild { get; set; }
        bool CanHaveRecipeChild { get; set; }
        bool CanHaveShoppingListChild { get; set; }
    }
}

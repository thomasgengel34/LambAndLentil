using LambAndLentil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LambAndLentil.UI.Models
{
    /// <summary>
    /// use this to filter ingredient in Index and List
    /// </summary>
    public class IngredientFilter:Ingredient
    {
        public enum AscOrDesc
        {
            ascend,
            descend
        }

        public AscOrDesc Direction { get; set; }
    }
}
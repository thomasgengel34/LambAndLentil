using System.Collections.Generic;

namespace LambAndLentil.Domain.Entities
{
    public interface IIngredient:IEntity
    { 
        List<Ingredient> Ingredients { get; set; }
    }
}
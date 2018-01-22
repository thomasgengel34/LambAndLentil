using System;
using System.Collections.Generic;

namespace LambAndLentil.Domain.Entities
{
    public interface IShoppingList: IEntity  
    {
        string Author { get; set; }
        DateTime Date { get; set; }  
    }
}
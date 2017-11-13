﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.Domain.Entities
{
   public interface IEntityChildClassShoppingLists:IEntity
    { 
         List<ShoppingList> ShoppingLists { get; set; } 
    }
}
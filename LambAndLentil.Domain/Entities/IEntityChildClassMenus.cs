using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.Domain.Entities
{
   public interface IEntityChildClassMenus:IEntity
    { 
         List<Menu> Menus { get; set; } 
    }
}

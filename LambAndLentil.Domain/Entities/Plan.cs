using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.Domain.Entities
{
    [Table("PLAN.Plan")]
    public class Plan:BaseEntity
    {
        public Plan() : base(new DateTime(2010, 1, 1))
        {
        }

        public ICollection<Menu> Menus   { get; set; }
    } 
}

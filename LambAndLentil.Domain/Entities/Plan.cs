using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.Domain.Entities
{
    [Table("PLAN.Plan")]
    public class Plan:BaseEntity,IEntity
    {
        public Plan():base()
        {

        }


        public Plan(DateTime creationDate) : base(creationDate)
        {
            CreationDate = creationDate;
        }

        public int ID { get; set; }
        public ICollection<Menu> Menus   { get; set; }
    } 
}

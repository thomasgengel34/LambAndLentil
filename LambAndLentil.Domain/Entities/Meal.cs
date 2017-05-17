using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.Domain.Entities
{
    [Table("PLAN.Meal")]
    public class Meal : BaseEntity
    {
        public MealType MealType { get; set; }
        public Menu Menu { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }
}

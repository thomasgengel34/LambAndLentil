using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LambAndLentil.Domain.Entities
{
   public  class Plans
    {
        public List<Plan> MyPlans { get; set; } 

        public Plans(List<Plan> Plans)
        {
            MyPlans = Plans;
        }

        public static SelectList GetListOfAllPlannNames(List<Plan> MyPlans)
        {
            var list = from n in MyPlans.AsQueryable()
                       select n.Name;
            SelectList descriptions = new SelectList(list);
            return descriptions;
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.UI.Models
{
    
    public class PersonVM:BaseVM
    {
        [Key] 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Weight { get; set; }


        public int MinCalories { get; set; }
        public int MaxCalories { get; set; }
        public bool NoGarlic { get; set; }
        public bool MustHaveGarlic { get; set; }
        //TODO: add all ingredients after I figure out how to economically
    }
}

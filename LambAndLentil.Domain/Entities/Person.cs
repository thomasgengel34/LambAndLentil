using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.Domain.Entities
{
    [Table("PERSON.Person")]
    public class Person:BaseEntity,IEntity
    {
        public Person():base( new DateTime(2010, 1, 1))  
        {
            Name = FirstName + " " + LastName;
        }
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 

        public decimal Weight { get; set; }


        public int MinCalories { get; set; }
        public int MaxCalories { get; set; }
        public bool NoGarlic { get; set; } 
        //TODO: add all ingredients after I figure out how to economically
    }
}

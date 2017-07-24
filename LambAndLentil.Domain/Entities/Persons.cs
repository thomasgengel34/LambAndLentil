using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LambAndLentil.Domain.Entities
{
    public class Persons  
    {
        public int ID { get; set; }
        public List<Person> MyPersons { get; set; }

        public Persons(List<Person> Person)
        {
            MyPersons = Person;
        }

        public static SelectList GetListOfAllPersonNames(List<Person> MyPersons)
        {
            var list = from n in MyPersons.AsQueryable()
                       select n.Name;
            SelectList descriptions = new SelectList(list);
            return descriptions;
        }
    }
}

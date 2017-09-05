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
        public Person():base()
        {
            Ingredients = new List<Ingredient>();
            Recipes = new List<Recipe>();
            Menus = new List<Menu>();
            Plans = new List<Plan>();
            ShoppingLists = new List<ShoppingList>();
        }
        public Person(DateTime creationDate) : base(creationDate)
        {
            CreationDate = creationDate;
        }

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 

        public decimal Weight { get; set; }


        public int MinCalories { get; set; }
        public int MaxCalories { get; set; }
        public bool NoGarlic { get; set; }
        //TODO: add all ingredients after I figure out how to economically
         
        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Menu> Menus { get; set; }
        public ICollection<Plan> Plans { get; set; }
        public ICollection<ShoppingList> ShoppingLists { get; set; }
    }
}

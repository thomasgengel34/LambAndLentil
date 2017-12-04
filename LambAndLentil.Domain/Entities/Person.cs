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
    public class Person : BaseEntity, IPerson 
    {
         
        public Person() : base()
        {
            Ingredients = new List<Ingredient>();
            Recipes = new List<Recipe>();
            Menus = new List<Menu>();
            Plans = new List<Plan>();
            ShoppingLists = new List<ShoppingList>();
            FirstName = "Newly";
            LastName = "Created";
            FullName = GetName(FirstName, LastName);
            Name = FullName;

            CanHaveMenuChild = true;
            CanHavePlanChild = true;
            CanHaveRecipeChild =true;
            CanHaveShoppingListChild = true;
        }

        public Person(string firstName, string lastName) : base()
        {
            FirstName = firstName;
            LastName = lastName;
            FullName = GetName(FirstName, LastName);
            Name = FullName;
        }

        public Person(DateTime creationDate) : base(creationDate)
        {
            CreationDate = creationDate;
            FullName = GetName(FirstName, LastName);
        }

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public decimal Weight { get; set; }


        public int MinCalories { get; set; }
        public int MaxCalories { get; set; }
        public bool NoGarlic { get; set; }
        //TODO: add all ingredients after I figure out how to economically


       
        public   List<Plan> Plans { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<Menu> Menus { get; set; }
        public List<ShoppingList> ShoppingLists { get; set; }

        public string GetName(string FirstName, string LastName) => FullName = String.Concat(FirstName, " ", LastName);
    }
}

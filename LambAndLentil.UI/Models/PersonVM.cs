using LambAndLentil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.UI.Models
{
    
    public class PersonVM:BaseVM,IBaseVM,IEntity
    {
        public PersonVM()
        {
            FirstName = "First Name";
            LastName = "Last Name";
            Name = String.Concat(FirstName ," ", LastName);
        }

        public PersonVM(string firstName,string lastName) : this()
        {
            FirstName = firstName;
            LastName = lastName;
            Name = String.Concat(FirstName, " ", LastName);
        }

        public PersonVM(DateTime creationDate):this()
        {
            CreationDate = creationDate;
        }

         
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public decimal Weight { get; set; } 
        public int MinCalories { get; set; }
        public int MaxCalories { get; set; }
        public bool NoGarlic { get; set; }
        //TODO: add all ingredients after I figure out how to economically
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Menu> Menus { get; set; }
        public ICollection<Plan> Plans { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<ShoppingList> ShoppingLists { get; set; } 
    }
}

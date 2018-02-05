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

        new List<Ingredient> Ingredients { get; set; }
        new List<Recipe> Recipes { get; set; }
        new List<Menu> Menus { get; set; }  
        new List<Plan> Plans { get; set; }  
        new List<ShoppingList> ShoppingLists { get; set; }  

        public string LastName { get; set; }
        public string FullName { get; set; }
        public decimal Weight { get; set; }


        public int MinCalories { get; set; }
        public int MaxCalories { get; set; }
        public bool NoGarlic { get; set; }
        //TODO: add all ingredients after I figure out how to economically



        public string FirstName { get; set; }
        
        public int ID { get; set; }
         

        public string GetName(string FirstName, string LastName) => FullName = String.Concat(FirstName, " ", LastName);



        bool IEntity.CanHaveChild(IEntity child)
        {
            Type type = child.GetType();

            List<Type> possibleChildren = new List<Type>()
            {
                typeof(Ingredient),
                typeof(Menu),
                typeof(Plan),
                typeof(Recipe),
                typeof(ShoppingList)
            };

            if (possibleChildren.Contains(type))
            {
                return true;
            }
            return false;
        }

        public override bool CanHaveChild(IEntity child)
        {
            Type type = child.GetType();

            List<Type> possibleChildren = new List<Type>()
            {
                typeof(Ingredient),
                typeof(Menu),
                typeof(Plan),
                typeof(Recipe),
                typeof(ShoppingList)
            };

            if (possibleChildren.Contains(type))
            {
                return true;
            }
            return false;
        }
         
         

        int IEntity.GetCountOfChildrenOnParent(IEntity parent)
        {
            try
            {
                throw new Exception("Person cannot be a child");
            }
            catch (InvalidCastException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

        }

         
    }
}

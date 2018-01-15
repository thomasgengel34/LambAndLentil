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
            CanHaveRecipeChild = true;
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



        public string LastName { get; set; }
        public string FullName { get; set; }
        public decimal Weight { get; set; }


        public int MinCalories { get; set; }
        public int MaxCalories { get; set; }
        public bool NoGarlic { get; set; }
        //TODO: add all ingredients after I figure out how to economically


      
        public string FirstName { get; set; }
        public List<Plan> Plans { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<Menu> Menus { get; set; }
        public List<ShoppingList> ShoppingLists { get; set; }
        public int ID { get; set; }
        string IPerson.FirstName { get; set; }
        string IPerson.LastName { get; set; }
        string IPerson.FullName { get; set; }
        decimal IPerson.Weight { get; set; }
        int IPerson.MinCalories { get; set; }
        int IPerson.MaxCalories { get; set; }
        bool IPerson.NoGarlic { get; set; }
        List<Recipe> IEntityChildClassRecipes.Recipes { get; set; }
        List<Menu> IEntityChildClassMenus.Menus { get; set; }
        List<Plan> IEntityChildClassPlans.Plans { get; set; }
        List<ShoppingList> IEntityChildClassShoppingLists.ShoppingLists { get; set; }
        string IEntity.AddedByUser { get; set; }
        DateTime IEntity.CreationDate { get; set; }
        int IEntity.ID { get; set; }
        string IEntity.ModifiedByUser { get; set; }
        DateTime IEntity.ModifiedDate { get; set; }
        string IEntity.Name { get; set; }
        string IEntity.Description { get; set; }
        string IEntity.IngredientsList { get; set; }
        List<Ingredient> IEntity.Ingredients { get; set; }

        public string GetName(string FirstName, string LastName) => FullName = String.Concat(FirstName, " ", LastName);

        bool  ParentCanHaveChild(IPossibleChildren parent)
        {
            return parent.CanHavePersonChild;
        }

      

        public void ParentRemoveAllChildrenOfAType(IEntity  parent, IEntity child)
        {
            throw new Exception("You cannot have Persons as children");
        }

        public IEntity  RemoveSelectionFromChildren<TChild>(IEntityChildClassIngredients  parent, List<TChild> selected)
            where TChild : BaseEntity, IEntity, IPossibleChildren, new()
        {
            throw new Exception("You cannot have Persons as children");
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

        bool IEntity.ParentCanHaveChild(IEntity parent) => parent.CanHaveIngredientChild;
        void IEntity.ParentRemoveAllChildrenOfAType(IEntity parent, IEntity child) => throw new NotImplementedException();
        IEntity IEntity.RemoveSelectionFromChildren<TChild>(IEntityChildClassIngredients parent, List<TChild> selected) => throw new NotImplementedException();
        IEntity IEntity.RemoveSelectionFromChildren<TChild>(IEntityChildClassRecipes parent, List<TChild> selected) => throw new NotImplementedException();
    }
}

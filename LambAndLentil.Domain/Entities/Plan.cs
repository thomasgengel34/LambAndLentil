using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.Domain.Entities
{
    [Table("PLAN.Plan")]
    public class Plan : BaseEntity,  IEntityChildClasses, IPlan
    {
        public Plan():base()
        {
            Ingredients = new List<Ingredient>();
            Recipes = new List<Recipe>();
            Menus = new List<Menu>();
        }


        public Plan(DateTime creationDate) : base(creationDate)
        {
            CreationDate = creationDate;
        }

        public int ID { get; set; }

        private new List<Person> Persons { get; set; }
        private new List<Plan> Plans { get; set; }
        private new List<ShoppingList> ShoppingLists { get; set; }
    } 
}

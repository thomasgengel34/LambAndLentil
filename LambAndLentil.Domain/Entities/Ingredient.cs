using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LambAndLentil.Domain.Entities
{
    [Table("INGREDIENT.Ingredient")]
    public class Ingredient : BaseEntity, IEntityChildClasses
    { 
        public Ingredient() : base()
        { 
        }

        public Ingredient(DateTime creationDate) : this() => CreationDate = creationDate;


        public int ID { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        private   List<Recipe> Recipes { get; set; }
          List<Recipe> IEntityChildClasses.Recipes { get; set; }
        private   List<Menu> Menus { get; set; }
        List<Menu> IEntityChildClasses.Menus { get; set; }
        private   List<Plan> Plans { get; set; }
        List<Plan> IEntityChildClasses.Plans { get; set; }
        private  List<ShoppingList> ShoppingLists { get; set; }
        List<ShoppingList> IEntityChildClasses.ShoppingLists { get; set; }
        private   List<Person> Persons { get; set; }
        List<Person> IEntityChildClasses.Persons { get; set; }
    }
}
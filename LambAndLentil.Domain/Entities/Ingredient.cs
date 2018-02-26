using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace LambAndLentil.Domain.Entities
{
    [Table("INGREDIENT.Ingredient")]
    public class Ingredient :  BaseEntity, IEntity 
    {
        public Ingredient() : base()
        {
            Ingredients = new List<Ingredient>(); 
            ClassName  = "Ingredient";
            DisplayName  = "Ingredient";
        }

        public Ingredient(DateTime creationDate) : this() => CreationDate = creationDate; 
        
        public int ID { get; set; }
         
        public  List<Ingredient> Ingredients { get; set; }
        public   List<Recipe>  Recipes { get; set; } = null;
        public  List<Menu>  Menus { get; set; } = null;
        public  List<Plan>  Plans { get; set; } = null;
        public  List<ShoppingList>  ShoppingLists { get; set; } = null;
      
        bool IEntity.CanHaveChild(IEntity child)
        {
            Type type = child.GetType();

            List<Type> possibleChildren = new List<Type>()
            {
                typeof(Ingredient)
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
                typeof(Ingredient)
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
                return ((IEntity)parent).Ingredients.Count();
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
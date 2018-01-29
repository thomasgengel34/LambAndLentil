using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace LambAndLentil.Domain.Entities
{
    [Table("INGREDIENT.Ingredient")]
    public class Ingredient : BaseEntity, IEntity, IIngredient
    {
        public Ingredient() : base()
        {
            Ingredients = new List<Ingredient>(); 
        }

        public Ingredient(DateTime creationDate) : this() => CreationDate = creationDate;



        public List<Ingredient> Ingredients { get; set; } 
        public int ID { get; set; }
         
        List<IEntity> IEntity.Recipes { get; set; } = null;
        List<IEntity> IEntity.Menus { get; set; } = null;
        List<IEntity> IEntity.Plans { get; set; } = null;
        List<IEntity> IEntity.ShoppingLists { get; set; } = null;

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
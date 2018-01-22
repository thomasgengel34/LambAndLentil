using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.Domain.Entities
{
    [Table("PLAN.Plan")]
    public class Plan : BaseEntity, IPlan
    {
        public Plan() : base()
        {
            Ingredients = new List<Ingredient>();
            Recipes = new List<Recipe>();
            Menus = new List<Menu>();
        }


        public Plan(DateTime creationDate) : base(creationDate) => CreationDate = creationDate;


        public List<Ingredient> Ingredients { get; set; }
        public List<Menu> Menus { get; set; }
        public List<Recipe> Recipes { get; set; }
        public int ID { get; set; } 
        List<Plan> IEntity.Plans { get; set; } = null;
        List<ShoppingList> IEntity.ShoppingLists { get; set; } = null;

        void AddChildToParent(IEntity parent, IEntity child)
        {
            ((IEntity)parent).Plans.Add((Plan)child);
        }

        bool IEntity.CanHaveChild(IEntity child)
        {
            Type type = child.GetType();

            List<Type> possibleChildren = new List<Type>()
            {
                typeof(Ingredient),
                typeof(Recipe),
                typeof(Menu) 
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
                typeof(Recipe),
                typeof(Menu)
            };

            if (possibleChildren.Contains(type))
            {
                return true;
            }
            return false;
        }

        public void ParentRemoveAllChildrenOfAType(IEntity parent, IEntity child)
        {
            ((IEntity)parent).Plans.Clear();
        }


        public IEntity RemoveSelectionFromChildren<TChild>(IEntity parent, List<TChild> selected)
            where TChild : BaseEntity, IEntity,   new()
        {
            var setToRemove = new HashSet<TChild>(selected);
            ((IEntity)parent).Plans.RemoveAll(ContainsSelected);
            return parent;

            bool ContainsSelected(IEntity item)
            {
                int itemID = item.ID;
                var numbers = from f in selected select f.ID;
                bool trueOrFalse = numbers.Contains(itemID);
                return trueOrFalse;
            }

        }

        int IEntity.GetCountOfChildrenOnParent(IEntity parent)
        {
            try
            {
                return ((IEntity)parent).Plans.Count();
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

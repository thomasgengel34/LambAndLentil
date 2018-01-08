using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.Domain.Entities
{
    [Table("PLAN.Plan")]
    public class Plan : BaseEntity,  IPlan
    {
        public Plan():base()
        {
            Ingredients = new List<Ingredient>();
            Recipes = new List<Recipe>();
            Menus = new List<Menu>();

            CanHaveMenuChild = true;
            CanHavePlanChild = false;
            CanHaveRecipeChild = true;
            CanHaveShoppingListChild = false;
        }


        public Plan(DateTime creationDate) : base(creationDate) => CreationDate = creationDate;

     
        public List<Ingredient> Ingredients { get; set; }
        public List<Menu> Menus { get; set; }
        public List<Recipe> Recipes { get; set; }



        void IEntity.AddChildToParent(IEntity parent, IEntity child)
        {
            ((IEntityChildClassPlans)parent).Plans.Add((Plan)child);
        }

        bool IEntity.ParentCanHaveChild(IPossibleChildren parent)
        {
            return parent.CanHavePlanChild;
        }

        public void ParentRemoveAllChildrenOfAType(IEntity  parent, IEntity child)
        {
            ((IEntityChildClassPlans)parent).Plans.Clear();
        }


        public IEntity  RemoveSelectionFromChildren<TChild>(IEntity  parent, List<TChild> selected)
            where TChild : BaseEntity, IEntity, IPossibleChildren, new()
        {
            var setToRemove = new HashSet<TChild>(selected);
            ((IEntityChildClassPlans)parent).Plans.RemoveAll(ContainsSelected);
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
                return ((IEntityChildClassPlans)parent).Plans.Count();
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

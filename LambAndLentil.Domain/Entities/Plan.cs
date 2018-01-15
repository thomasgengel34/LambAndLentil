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
        public int ID { get; set; }
        List<Recipe> IEntityChildClassRecipes.Recipes { get; set; }
        List<Menu> IEntityChildClassMenus.Menus { get; set; }
        string IEntity.AddedByUser { get; set; }
        DateTime IEntity.CreationDate { get; set; }
        int IEntity.ID { get; set; }
        string IEntity.ModifiedByUser { get; set; }
        DateTime IEntity.ModifiedDate { get; set; }
        string IEntity.Name { get; set; }
        string IEntity.Description { get; set; }
        string IEntity.IngredientsList { get; set; }
        List<Ingredient> IEntity.Ingredients { get; set; }

        void  AddChildToParent(IEntity parent, IEntity child)
        {
            ((IEntityChildClassPlans)parent).Plans.Add((Plan)child);
        }

        bool ParentCanHaveChild(IPossibleChildren parent)
        {
            return parent.CanHavePlanChild;
        }

        public void ParentRemoveAllChildrenOfAType(IEntity  parent, IEntity child)
        {
            ((IEntityChildClassPlans)parent).Plans.Clear();
        }


        public IEntity  RemoveSelectionFromChildren<TChild>(IEntityChildClassIngredients  parent, List<TChild> selected)
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

        bool IEntity.ParentCanHaveChild(IEntity parent) =>   parent.CanHavePlanChild;
        void IEntity.ParentRemoveAllChildrenOfAType(IEntity parent, IEntity child) => throw new NotImplementedException();
        IEntity IEntity.RemoveSelectionFromChildren<TChild>(IEntityChildClassIngredients parent, List<TChild> selected) => throw new NotImplementedException();
        IEntity IEntity.RemoveSelectionFromChildren<TChild>(IEntityChildClassRecipes parent, List<TChild> selected) => throw new NotImplementedException();
    } 
}

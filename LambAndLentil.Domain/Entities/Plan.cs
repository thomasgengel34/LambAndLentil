using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.Domain.Entities
{
    [Table("PLAN.Plan")]
    public class Plan : BaseEntity, IEntity 
    {
        public Plan() : base()
        {
            Plans   = null;
            ShoppingLists = null;
            Ingredients = new List<Ingredient>(); 
            ClassName = "Plan";
            DisplayName = "Plan";
        }


        public Plan(DateTime creationDate) : base(creationDate) => CreationDate = creationDate;
         public List<Ingredient> Ingredients { get; set; }
        new List<Recipe> Recipes { get; set; }
        new List<Menu> Menus { get; set; }  
        new List<Plan> Plans { get; set; } = null;
        public List<ShoppingList> ShoppingLists { get; set; } = null;


        public int ID { get; set; } 
       
        void AddChildToParent(IEntity parent, IEntity child)
        {
            parent.Plans.Add( (Plan)child);
        }

        public override bool  CanHaveChild(IEntity child)
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
            parent.Plans.Clear();
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

       public int  GetCountOfChildrenOnParent(IEntity parent)
        {
            try
            {
                return parent.Plans.Count();
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

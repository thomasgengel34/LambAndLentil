using System.Linq;
using System.Reflection;
using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.Entities
{
    public class BaseTest<T>
        where T: BaseEntity, IEntity, new()
    {
        internal static void BaseBeAbleToHaveIngredientsChild()
        {
            T entity = new T();
            MemberInfo[] memberInfo = entity.GetType().GetMembers();
            var result = memberInfo.Where(m => m.Name == "set_Ingredients");
            Assert.IsNotNull(result);
        }

        internal static void BaseBeAbleToHaveRecipesChild()
        {
            T entity = new T();
            MemberInfo[] memberInfo = entity.GetType().GetMembers();
            var result = memberInfo.Where(m => m.Name == "set_Recipes");
            Assert.IsNotNull(result);
        }

        internal static void BaseBeAbleToHaveMenusChild()
        {
            T entity = new T();
            MemberInfo[] memberInfo = entity.GetType().GetMembers();
            var result = memberInfo.Where(m => m.Name == "set_Menus");
            Assert.IsNotNull(result);
        }

        internal static void BaseBeAbleToHavePlansChild()
        {
            T entity = new T();
            MemberInfo[] memberInfo = entity.GetType().GetMembers();
            var result = memberInfo.Where(m => m.Name == "set_Plans");
            Assert.IsNotNull(result);
        }


        internal static void BaseBeAbleToHaveShoppingListsChild()
        {
            T entity = new T();
            MemberInfo[] memberInfo = entity.GetType().GetMembers();
            var result = memberInfo.Where(m => m.Name == "set_ShoppingList");
            Assert.IsNotNull(result);
        }
        internal static void BaseRecipePropertyIsNull()
        {
            // TODO: develop for each entity where a property must be null.
            Assert.Fail();
        }
    }
}

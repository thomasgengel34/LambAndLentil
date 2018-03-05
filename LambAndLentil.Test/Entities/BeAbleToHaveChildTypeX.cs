using System;
using System.Linq;
using System.Reflection;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.BaseControllerTests;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.Entities
{
    internal class BeAbleToHaveChildTypeX<T> : BaseControllerTest<T>
         where T : BaseEntity, IEntity, new()
    {

        internal static void TestRunner()
        {
            BeAbleToHaveIngredientsChild();
            BeAbleToHaveMenusChild();
            BeAbleToHavePlansChild();
            BeAbleToHaveRecipesChild();
            BeAbleToHaveShoppingListsChild(); 
        }


        internal static T SetUpItemAndRepo(out IGenericController<T> controller)
        {
            T item = new T() { ID = 5450 };
            repo = new TestRepository<T>();
            repo.Save(item);
            controller = BaseControllerTestFactory();
            return item;
        }


        private static void BeAbleToHaveIngredientsChild()
        {
            T entity = new T();
            MemberInfo[] memberInfo = entity.GetType().GetMembers();
            var result = memberInfo.Where(m => m.Name == "set_Ingredients");
            Assert.IsNotNull(result);
        }

        private static void BeAbleToHaveRecipesChild()
        {
            T entity = new T();
            MemberInfo[] memberInfo = entity.GetType().GetMembers();
            var result = memberInfo.Where(m => m.Name == "set_Recipes");
            Assert.IsNotNull(result);
        }

        private static void BeAbleToHaveMenusChild()
        {
            T entity = new T();
            MemberInfo[] memberInfo = entity.GetType().GetMembers();
            var result = memberInfo.Where(m => m.Name == "set_Menus");
            Assert.IsNotNull(result);
        }

        private static void BeAbleToHavePlansChild()
        {
            T entity = new T();
            MemberInfo[] memberInfo = entity.GetType().GetMembers();
            var result = memberInfo.Where(m => m.Name == "set_Plans");
            Assert.IsNotNull(result);
        }


        private static void BeAbleToHaveShoppingListsChild()
        {
            T entity = new T();
            MemberInfo[] memberInfo = entity.GetType().GetMembers();
            var result = memberInfo.Where(m => m.Name == "set_ShoppingList");
            Assert.IsNotNull(result);
        }
        private static void BaseRecipePropertyIsNull()
        {
            // TODO: develop for each entity where a property must be null.
            Assert.Fail();
        }
         
    }
}

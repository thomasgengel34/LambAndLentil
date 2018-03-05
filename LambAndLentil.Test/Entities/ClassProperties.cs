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
    internal class ClassProperties<T> : BaseControllerTest<T>
         where T : BaseEntity, IEntity, new()
    {

        internal static void TestRunner()
        {
            HaveBaseEntityPropertiesOnCreation();
            HaveClassPropertiesOnCreation();
            HaveCorrectDefaultsInConstructor(); 
            InheritFromBaseEntity();
            // TODO: write more tests - some are specified below
        }


        internal static T SetUpItemAndRepo(out IGenericController<T> controller)
        {
            T item = new T() { ID = 5450 };
            repo = new TestRepository<T>();
            repo.Save(item);
            controller = BaseControllerTestFactory();
            return item;
        }



        private static void InheritFromBaseEntity()
        {
            T item = new T();

            Type baseType = typeof(BaseEntity);
            bool isBase = baseType.IsInstanceOfType(item);

            Assert.AreEqual(true, isBase);
        }


        private static void HaveCorrectDefaultsInConstructor()
        {
            T item = new T();

            Assert.IsNotNull(item.CreationDate);
            Assert.IsNotNull(item.ModifiedDate);
            Assert.IsNotNull(item.AddedByUser);
            Assert.IsNotNull(item.ModifiedByUser);
            Assert.AreEqual(item.AddedByUser, item.ModifiedByUser);
        }



        private static void HaveBaseEntityPropertiesOnCreation()
        {
            T item = new T();

            Assert.AreEqual("Newly Created", item.Name);
            Assert.AreEqual("not yet described", item.Description);
        }

        private static void HaveClassPropertiesOnCreation()
        {
            if (typeof(T) == typeof(Menu))
            {
                Menu item = new Menu();

                Assert.AreEqual(item.MealType, MealType.Breakfast);
                Assert.AreEqual(DayOfWeek.Sunday, item.DayOfWeek);
                Assert.AreEqual(0, item.Diners);
            }
            if (typeof(T) == typeof(Recipe))
            {
                Recipe recipe = new Recipe(new DateTime(2017, 06, 26));

                Assert.AreEqual(0, recipe.Servings);
                Assert.AreEqual(recipe.MealType, MealType.Breakfast);
                Assert.IsNull(recipe.Calories);
                Assert.IsNull(recipe.CalsFromFat);
            }
            if (typeof(T) == typeof(Person))
            {
                Person person = new Person(new DateTime(2017, 06, 26));

                Assert.AreEqual(null, person.FirstName);
                Assert.AreEqual(null, person.LastName);
                Assert.AreEqual(0, person.Weight);
                Assert.AreEqual(0, person.MinCalories);
                Assert.AreEqual(0, person.MaxCalories);
                Assert.AreEqual(false, person.NoGarlic);
            }
            if (typeof(T) == typeof(ShoppingList))
            {
                ShoppingList shoppingList = new ShoppingList(new DateTime(2017, 06, 26));

                Assert.AreEqual(new DateTime(2017, 06, 26), shoppingList.Date);
                Assert.IsNull(shoppingList.Author);
            }
        }

        private static void RequireIngredientChildrenToHaveUniqueIDs()
        {
            Assert.Fail();
        }

        private static void RequireRecipeChildrenToHaveUniqueIDs()
        {
            Assert.Fail();
        }


        private static void RequireMenuChildrenToHaveUniqueIDs()
        {
            Assert.Fail();
        }

        private static void RequirePlanChildrenToHaveUniqueIDs()
        {
            Assert.Fail();
        }

        private static void RequireShoppingListChildrenToHaveUniqueIDs()
        {
            Assert.Fail();
        }
    }
}

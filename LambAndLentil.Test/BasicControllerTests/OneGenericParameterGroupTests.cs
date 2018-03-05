using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.BaseControllerTests;
using LambAndLentil.Test.BasicControllerTests;
using LambAndLentil.Test.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;
using MenuType = LambAndLentil.Domain.Entities.Menu;
using PersonType = LambAndLentil.Domain.Entities.Person;
using PlanType = LambAndLentil.Domain.Entities.Plan;
using RecipeType = LambAndLentil.Domain.Entities.Recipe;
using ShoppingListType = LambAndLentil.Domain.Entities.ShoppingList;
using System.Linq;

namespace LambAndLentil.Tests.Controllers
{
    [TestClass]
    public class OneGenericParameterGroupTests
    {
      

        [TestMethod]
        public void RunOneGenericParameterGroupTests()
        {
            TestsRunner<IngredientType>();

            //TestsRunner<RecipeType>();

            //TestsRunner<MenuType>();

            //TestsRunner<PlanType>();

            //TestsRunner<ShoppingListType>();

            //TestsRunner<PersonType>();
        }



        public static void TestsRunner<T>()
             where T : BaseEntity, IEntity, new()
        {
            IRepository<T> repo = new TestRepository<T>();


            // TODO: refactor
            if (typeof(T) == typeof(IngredientType))
            {
                IGenericController<IngredientType> controller = new IngredientsController((IRepository<IngredientType>)repo);
            }
            if (typeof(T) == typeof(RecipeType))
            {
                IGenericController<RecipeType> controller = new RecipesController((IRepository<RecipeType>)(repo));
            }
            if (typeof(T) == typeof(MenuType))
            {
                IGenericController<MenuType> controller = new MenusController((IRepository<MenuType>)(repo));
            }
            if (typeof(T) == typeof(PersonType))
            {
                IGenericController<PersonType> controller = new PersonsController((IRepository<PersonType>)(repo));
            }
            if (typeof(T) == typeof(PlanType))
            {
                IGenericController<PlanType> controller = new PlansController((IRepository<PlanType>)(repo));
            }
            if (typeof(T) == typeof(ShoppingListType))
            {
                IGenericController<ShoppingListType> controller = new ShoppingListsController((IRepository<ShoppingListType>)(repo));
            }

            ClassProperties<T>.TestRunner();
            ControllerDetailsShould<T>.TestRunner();
            Controller_Should<T>.TestRunner();
            CreateActionMethod<T>.TestRunner();
            DeleteConfirmed<T>.TestRunner();
            Details<T>.TestRunner();
            DetachTheFirstChild<T>.TestRunner();
            DetachTheLastChild<T>.TestRunner();
            Index<T>.TestRunner();
            PostEdit<T>.TestRunner();
            BeAbleToHaveChildTypeX<T>.TestRunner();
            Pagination<T>.TestRunner();
             BasicTests<T>.TestRunner();
            ClassPropertyChanges<T>.TestRunner();
            ClassPropertyChanges_Menu<T>.TestRunner();
            ClassPropertyChanges_Person<T>.TestRunner();
            DeleteReturnsIndexWithWarningWhen<T>.TestRunner();
            Flags<T>.TestRunner();

            // TODO: add more


        } 
    }
}

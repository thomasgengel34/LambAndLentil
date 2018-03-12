using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.IAttachDetachControllerTests.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;
using MenuType = LambAndLentil.Domain.Entities.Menu;
using PersonType = LambAndLentil.Domain.Entities.Person;
using PlanType = LambAndLentil.Domain.Entities.Plan;
using RecipeType = LambAndLentil.Domain.Entities.Recipe;
using ShoppingListType = LambAndLentil.Domain.Entities.ShoppingList;

namespace LambAndLentil.Test.IAttachDetachControllerTests
{
    [TestClass]
    public class TwoGenericParametersGroupTests
    {
        [TestMethod]
        public void RunAttachDetachControllerTests()
        {
            IngredientTestsDealer();
            RecipeTestsDealer();
            MenuTestsDealer();
            PersonTestsDealer();
            PlanTestsDealer();
            ShoppingListTestsDealer();
        }



        private void IngredientTestsDealer()
        {
            TestsRunner<IngredientType, IngredientType>();

            TestsRunner<IngredientType, RecipeType>();

            TestsRunner<IngredientType, MenuType>();

            TestsRunner<IngredientType, PlanType>();

            TestsRunner<IngredientType, ShoppingListType>();

            TestsRunner<IngredientType, PersonType>();

        }

        private void MenuTestsDealer()
        {
            TestsRunner<MenuType, IngredientType>();

            TestsRunner<MenuType, RecipeType>();

            TestsRunner<MenuType, MenuType>();

            TestsRunner<MenuType, PlanType>();

            TestsRunner<MenuType, ShoppingListType>();

            TestsRunner<MenuType, PersonType>();

        }

        private void PersonTestsDealer()
        {
            TestsRunner<PersonType, IngredientType>();

            TestsRunner<PersonType, RecipeType>();

            TestsRunner<PersonType, MenuType>();

            TestsRunner<PersonType, PlanType>();

            TestsRunner<PersonType, ShoppingListType>();

            TestsRunner<PersonType, PersonType>();
        }

        private void PlanTestsDealer()
        {
            TestsRunner<PlanType, IngredientType>();

            TestsRunner<PlanType, RecipeType>();

            TestsRunner<PlanType, MenuType>();

            TestsRunner<PlanType, PlanType>();

            TestsRunner<PlanType, ShoppingListType>();

            TestsRunner<PlanType, PersonType>();
        }

        private void RecipeTestsDealer()
        {
            TestsRunner<RecipeType, IngredientType>();

            TestsRunner<RecipeType, RecipeType>();

            TestsRunner<RecipeType, MenuType>();

            TestsRunner<RecipeType, PlanType>();

            TestsRunner<RecipeType, ShoppingListType>();

            TestsRunner<RecipeType, PersonType>();
        }

        private void ShoppingListTestsDealer()
        {
            TestsRunner<ShoppingListType, IngredientType>();

            TestsRunner<ShoppingListType, RecipeType>();

            TestsRunner<ShoppingListType, MenuType>();

            TestsRunner<ShoppingListType, PlanType>();

            TestsRunner<ShoppingListType, ShoppingListType>();

            TestsRunner<ShoppingListType, PersonType>();
        }

        public static void TestsRunner<TParent, TChild>()
             where TParent : BaseEntity, IEntity, new()
        where TChild : BaseEntity, IEntity, new()
        { 
            AttachAnXToAYEntity<TParent, TChild>.TestRunner(); 
            BaseControllerShouldDetachXAndReturn<TParent, TChild>.TestRunner();
             

            // TODO: add more

        } 
    }
}

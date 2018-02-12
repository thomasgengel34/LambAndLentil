using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
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

namespace LambAndLentil.Tests.Controllers
{
    [TestClass]
    public class GroupTests
    { 
        [TestMethod]
        public void RunBasicControllerTests()
        {
            TestsRunner<IngredientType>();

            TestsRunner<RecipeType>();

            TestsRunner<MenuType>();

            TestsRunner<PlanType>();

            TestsRunner<ShoppingListType>();

            TestsRunner<PersonType>();
        }



        public static void TestsRunner<T>()
             where T : BaseEntity, IEntity, new()
        {
            IRepository<T> Repo = new TestRepository<T>(); 
             

            // TODO: refactor
            if (typeof(T) == typeof(IngredientType))
            {
                IGenericController<IngredientType> controller = new IngredientsController((IRepository<IngredientType>)Repo);

            }
            if (typeof(T) == typeof(RecipeType))
            {
                IGenericController<RecipeType> controller = new RecipesController((IRepository<RecipeType>)(Repo));
            }
            if (typeof(T) == typeof(MenuType))
            {
                IGenericController<MenuType> controller = new MenusController((IRepository<MenuType>)(Repo));
            }
            if (typeof(T) == typeof(PersonType))
            {
                IGenericController<PersonType> controller = new PersonsController((IRepository<PersonType>)(Repo));
            }
            if (typeof(T) == typeof(PlanType))
            {
                IGenericController<PlanType> controller = new PlansController((IRepository<PlanType>)(Repo));
            }
            if (typeof(T) == typeof(ShoppingListType))
            {
                IGenericController<ShoppingListType> controller = new ShoppingListsController((IRepository<ShoppingListType>)(Repo));
            }
            BaseController_Should.HavePublicIntPageSizeProperty();
            BaseControllerTest<T>.BaseDetachAllIngredientChildren();
            BaseControllerTest<T>.BaseShouldCreate();
            BaseControllerTest<T>.BaseReturnDetailsWhenIDIsFound();
            BaseControllerTest<T>.IsPublic();
            BaseControllerTest<T>.InheritsFromBaseControllerCorrectly();
            BaseControllerTests_Pagination<T>.BaseCanPaginateArrayLengthIsCorrect(); 
            BaseControllerTests_Pagination<T>.CanPaginate();
            BaseControllerTests_Pagination<T>.FirstItemNameIsCorrect();
            BaseControllerTests_Pagination<T>.CurrentPageCountCorrect();
            BaseControllerTests_Pagination<T>.ThirdItemNameIsCorrect();
            BaseControllerTests_Pagination<T>.ItemsPerPageCorrect();
            BaseControllerTests_Pagination<T>.TotalItemsCorrect();
            BaseControllerTests_Pagination<T>.TotalPagesCorrect();
            BaseControllerTests_Pagination<T>.CanReturnCorrectPageInfo();

            BaseControllerTests_Pagination<T>.BaseCanPaginateArrayLengthIsCorrect();
            ClassPropertyChanges<T>.BaseDetachAllIngredientChildren();
            ClassPropertyChanges<T>.BaseShouldCreate();
            ClassPropertyChanges<T>.BaseDetachAllMenuChildren();

            DeleteReturnsIndexWithWarningWhen<T>.DeletingInvalidEntity();

            BasicIndex<T>.ContainsAllView1Count5();
            BasicIndex<T>.ContainsAllView1NameIsIndex();
            BasicIndex<T>.ContainsAllView2Count0();
            BasicIndex<T>.ContainsAllView2NameIsIndex();
            BasicIndex<T>.ContainsAllView2NotNull();
            BasicIndex<T>.FirstCreationDateIsCorrect();
            BasicIndex<T>.FirstAddedByUserIsCorrect();
            BasicIndex<T>.FirstItemNameIsCorrect();
            BasicIndex<T>.FirstModifiedByUserIsCorrect();
            BasicIndex<T>.FirstModifiedDateIsCorrect();
            BasicIndex<T>.FirstPageIsNotNull();
            BasicIndex<T>.FirstPageNameIsIndex();
            BasicIndex<T>.Index();
            BasicIndex<T>.SecondItemNameIsCorrect();
            BasicIndex<T>.SecondPageIsCorrect();
             
            BasicDetails<T>.ReturnIndexWithErrorWhenIDIsNegative(); 
            BasicDetails<T>.BaseDetachAllIngredientChildren();

            BasicDeleteConfirmed<T>.ReturnIndexWithActionMethodDeleteConfirmedWithBadID(); 
            BasicDeleteConfirmed<T>.ReturnIndexWithConfirmationWhenIDIsFound(); 
            BaseTest<T>.BaseBeAbleToHaveIngredientsChild();
            BaseTest<T>.BaseBeAbleToHaveMenusChild();
            BaseTest<T>.BaseBeAbleToHavePlansChild();
            BaseTest<T>.BaseBeAbleToHaveRecipesChild();
            BaseTest<T>.BaseBeAbleToHaveShoppingListsChild();

            BaseControllerDetailsShould<T>.ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithFoundResult();
            BaseControllerDetailsShould<T>.BeSuccessfulWithValidIngredientID();

            // TODO: add more


        }
    }
}

using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.BasicControllerTests;
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
            BaseController_Should baseController = new BaseController_Should();
            BaseControllerTests_Pagination<T> baseControllerPagination = new BaseControllerTests_Pagination<T>();
            ClassPropertyChanges<T> classPropertyChanges = new ClassPropertyChanges<T>();
            DeleteReturnsIndexWithWarningWhen<T> deleteReturnsIndexWithWarningWhen = new DeleteReturnsIndexWithWarningWhen<T>();

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
            baseController.BaseDetachAllIngredientChildren();
            baseController.BaseShouldCreate(); 
            baseController.BaseReturnDetailsWhenIDIsFound();
            baseController.IsPublic();
            baseController.InheritsFromBaseControllerCorrectly();

            baseControllerPagination.CanPaginate();
            baseControllerPagination.FirstItemNameIsCorrect();
            baseControllerPagination.CurrentPageCountCorrect();
            baseControllerPagination.ThirdItemNameIsCorrect();
            baseControllerPagination.ItemsPerPageCorrect();
            baseControllerPagination.TotalItemsCorrect();
            baseControllerPagination.TotalPagesCorrect();
            baseControllerPagination.CanReturnCorrectPageInfo();

            classPropertyChanges.BaseDetachAllIngredientChildren();
            classPropertyChanges.BaseShouldCreate();

            deleteReturnsIndexWithWarningWhen.DeletingInvalidEntity();



            BasicIndex<T> basicIndexContain = new BasicIndex<T>();
            basicIndexContain.ContainsAllView1Count5();
            basicIndexContain.ContainsAllView1NameIsIndex();
            basicIndexContain.ContainsAllView2Count0();
            basicIndexContain.ContainsAllView2NameIsIndex();
            basicIndexContain.ContainsAllView2NotNull();
            basicIndexContain.FirstCreationDateIsCorrect();
            basicIndexContain.FirstAddedByUserIsCorrect();
            basicIndexContain.FirstItemNameIsCorrect();
            basicIndexContain.FirstModifiedByUserIsCorrect();
            basicIndexContain.FirstModifiedDateIsCorrect();
            basicIndexContain.FirstPageIsNotNull();
            basicIndexContain.FirstPageNameIsIndex();
            basicIndexContain.Index(); 
            basicIndexContain.SecondItemNameIsCorrect();
            basicIndexContain.SecondPageIsCorrect();



            BasicDetails<T> basicDetails = new BasicDetails<T>();
            basicDetails.ReturnIndexWithErrorWhenIDIsNegative();

            BasicDeleteConfirmed<T> basicDeleteConfirmed = new BasicDeleteConfirmed<T>();
            basicDeleteConfirmed.ReturnIndexWithActionMethodDeleteConfirmedWithBadID();

            // TODO: add more


        }
    }
}

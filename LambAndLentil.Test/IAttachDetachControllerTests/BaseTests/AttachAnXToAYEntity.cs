using System.Linq;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;
using MenuType = LambAndLentil.Domain.Entities.Menu;
using PersonType = LambAndLentil.Domain.Entities.Person;
using PlanType = LambAndLentil.Domain.Entities.Plan;
using RecipeType = LambAndLentil.Domain.Entities.Recipe;
using ShoppingListType = LambAndLentil.Domain.Entities.ShoppingList;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;

namespace LambAndLentil.Test.IAttachDetachControllerTests.BaseTests
{
    public class AttachAnXToAYEntity<TParent, TChild> : BaseTest<TParent, TChild>
         where TParent : BaseEntity, IEntity, new()
         where TChild : BaseEntity, IEntity, new()
    {
        private static IRepository<TParent> repo;
        private static TParent item;

        internal static void TestRunner()
        {
            AttachAnIngredientToAParent();
            AttachAChildToAParent();
            ReturnRecipeEditViewWithWarningMessageWhenAttachingNonExistingIngredientToExistingRecipe();
        }

        private static void AttachAnIngredientToAParent()
        {
            SetUpForTests(out repo, out controller, out item);

            IngredientType ingredient = new IngredientType { ID = 100 };

            ActionResult ar = controller.Attach(item, ingredient);
            TParent returnedItem = repo.GetById(item.ID);

            Assert.AreEqual(1, returnedItem.Ingredients.Count());
            Assert.AreEqual(ingredient.ID, returnedItem.Ingredients.First().ID);

        }


        private static void AttachAChildToAParent()
        {
            SetUpForTests(out repo, out controller, out item);
            TParent parent = new TParent() { ID = 3000, Description = "test Attach " };
            IRepository<TParent> localRepo = new TestRepository<TParent>();
            localRepo.Save(parent);
            TChild child = new TChild { ID = 3300 };
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.Attach(parent, child);
            TParent returnedParent = localRepo.GetById(parent.ID) as TParent;
            if (parent.CanHaveChild(child))
            {
                if (typeof(TChild) == typeof(IngredientType))
                {
                    Assert.AreEqual(1, returnedParent.Ingredients.Count());
                }
                if (typeof(TChild) == typeof(MenuType))
                {
                    Assert.AreEqual(1, returnedParent.Menus.Count());
                }
                if (typeof(TChild) == typeof(PlanType))
                {
                    Assert.AreEqual(1, returnedParent.Plans.Count());
                }
                if (typeof(TChild) == typeof(RecipeType))
                {
                    Assert.AreEqual(1, returnedParent.Recipes.Count());
                }
                Assert.AreEqual("test Attach ", parent.Description);
                // TODO: alert class etc
            }
            else
            {
                RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
                var routeValues = rtrr.RouteValues.Values;
                if (parent.CanHaveChild(child))
                {
                    if (typeof(TChild) == typeof(IngredientType))
                    {
                        Assert.AreEqual(0, returnedParent.Ingredients.Count());
                    }
                    if (typeof(TChild) == typeof(MenuType))
                    {
                        Assert.AreEqual(0, returnedParent.Menus.Count());
                    }
                    if (typeof(TChild) == typeof(PlanType))
                    {
                        Assert.AreEqual(0, returnedParent.Plans.Count());
                    }
                    if (typeof(TChild) == typeof(RecipeType))
                    {
                        Assert.AreEqual(0, returnedParent.Recipes.Count());
                    }
                }

                Assert.AreEqual("alert-warning", adr.AlertClass);
                Assert.AreEqual("Element Could not Be Attached - So It Could Not Be Detached!", adr.Message);
                Assert.AreEqual(3, routeValues.Count);
                Assert.AreEqual(parent.ID, routeValues.ElementAt(0));
                Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(2).ToString());
                Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(1).ToString());
            }
        }

        private static void ReturnRecipeEditViewWithWarningMessageWhenAttachingNonExistingIngredientToExistingRecipe()
        {
            SetUpForTests(out repo, out controller, out item);
            IngredientType ingredient = null;

            AlertDecoratorResult adr = (AlertDecoratorResult)controller.Attach(item, ingredient);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Child was not found", adr.Message);
            Assert.AreEqual(3, routeValues.Count);
            Assert.AreEqual(item.ID, routeValues.ElementAt(0));
            Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(2).ToString());
            Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(1).ToString());
        }
    }
}

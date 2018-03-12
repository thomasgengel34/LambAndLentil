using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;
using RecipeType = LambAndLentil.Domain.Entities.Recipe;
using MenuType = LambAndLentil.Domain.Entities.Menu;
using PlanType = LambAndLentil.Domain.Entities.Plan;
using ShoppingListType = LambAndLentil.Domain.Entities.ShoppingList;
using LambAndLentil.UI.Models;
using System;
using LambAndLentil.UI.Controllers;
using LambAndLentil.Test.BasicTests;

namespace LambAndLentil.Test.IAttachDetachControllerTests.BaseTests
{
    public class BaseControllerShouldAttachXAndReturn<TParent, TChild> : BaseTest<TParent, TChild>
        where TParent : BaseEntity, IEntity, new()
        where TChild : BaseEntity, IEntity, new()
    {
        private static IRepository<TParent> repo;
        private TChild child;
        private string childName;
        private static TParent item;

        public BaseControllerShouldAttachXAndReturn()
        {
            repo = new TestRepository<TParent>();
            child = new TChild();
            childName = typeof(TChild).ToString().Split('.').Last();
        }

        internal static void TestRunner()
        {
            DetailWithErrorWhenParentIDIsValidAndChildIsNotValid();
            DetailWithErrorWhenParentIDIsValidAndChildIsValidWhenAttachingUnattachableChild();
            DetailWithSuccessWhenParentIDIsValidAndChildIsValidWhenAttaching();
            IndexWithErrorWhenParentIDIsNotForAnExistingIngredient();
            IndexWithWarningWithNullParent();

        }



        internal static void IndexWithWarningWithNullParent()
        { // TODO: expand IngredientTypeTo Other Types
            TParent tparent = new TParent() { ID = 9001 };
            TChild tchild = new TChild() { ID = 9000 };
            ActionResult ar = controller.Attach(null, tchild);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;
            string message = adr.Message;

            Assert.AreEqual(tparent.DisplayName + " was not found", message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        }





        internal static void IndexWithErrorWhenParentIDIsNotForAnExistingIngredient()
        {  // Todo: add logging 
            ActionResult ar = controller.Attach(null, new IngredientType());
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            string message = adr.Message;
            TParent tp = new TParent();

            Assert.AreEqual(tp.DisplayName + " was not found", message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }


        internal static void DetailWithErrorWhenParentIDIsValidAndChildIsNotValid()
        {
            TParent parent = new TParent() { ID = 314 };
            TChild child = new TChild();
            IRepository<TParent> repo = new TestRepository<TParent>();
            repo.Save(parent);
            child = null;
            ActionResult ar = controller.Attach(parent, child);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            string message = adr.Message;

            Assert.IsNotNull(ar);
            Assert.AreEqual(parent.ID, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual("Child was not found", message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }

        internal static void DetailWithSuccessWhenParentIDIsValidAndChildIsValidWhenAttaching()
        {
            TChild child = new TChild();
            if (Parent.CanHaveChild(child))
            {
                SetUpForTests(out repo, out controller, out item);

                ActionResult ar = controller.Attach(Parent, (TChild)Child);
                AlertDecoratorResult adr = (AlertDecoratorResult)ar;
                RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
                TParent returnedItem = repo.GetById(item.ID);

                Assert.IsNotNull(ar);
                Assert.AreEqual(1, rtrr.RouteValues.ElementAt(0).Value);
                Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
                Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
                Assert.AreEqual(3, rtrr.RouteValues.Count);
                Assert.AreEqual(child.DisplayName + " was Successfully Attached!", adr.Message);
                Assert.AreEqual("alert-success", adr.AlertClass);

                if (child.GetType() == typeof(RecipeType))
                {
                    Assert.AreEqual(1, returnedItem.Recipes.Count());
                    Assert.AreEqual(child.ID, returnedItem.Recipes.First().ID);
                }
            }
        }


            internal static void DetailWithErrorWhenParentIDIsValidAndChildIsValidWhenAttachingUnattachableChild()
            {
                TChild child = new TChild() { ID = 8000 };
                if (!Parent.CanHaveChild(child))
                {
                    ActionResult ar = controller.Attach(Parent, child);
                    AlertDecoratorResult adr = (AlertDecoratorResult)ar;
                    RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
                    Assert.IsNotNull(ar);
                    Assert.AreEqual(1, rtrr.RouteValues.ElementAt(0).Value);
                    Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
                    Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
                    Assert.AreEqual(3, rtrr.RouteValues.Count);
                    Assert.AreEqual("Element Could not Be Attached - So It Could Not Be Detached!", adr.Message);
                    Assert.AreEqual("alert-warning", adr.AlertClass);
                }
            }


            internal static void EditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingRecipe()
            {
                SetUpForTests(out repo, out controller, out item);
                IngredientType ingredient = new IngredientType { ID = 700 };


                AlertDecoratorResult adr = (AlertDecoratorResult)controller.Detach(item, ingredient);
                RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
                var routeValues = rtrr.RouteValues.Values;

                Assert.AreEqual("alert-success", adr.AlertClass);
                Assert.AreEqual("Ingredient was Successfully Detached!", adr.Message);
                Assert.AreEqual(3, routeValues.Count);
                Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(1).ToString());
                Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(2).ToString());
            }


            internal static IGenericController<TParent> BaseControllerTestFactory(Type T)
            {
                if (typeof(TParent) == typeof(Ingredient))
                {
                    return (IGenericController<TParent>)(new IngredientsController(new TestRepository<Ingredient>()));
                }
                else if (typeof(TParent) == typeof(Recipe))
                {
                    return (IGenericController<TParent>)(new RecipesController(new TestRepository<Recipe>()));
                }
                else if (typeof(TParent) == typeof(Menu))
                {
                    return (IGenericController<TParent>)(new MenusController(new TestRepository<Menu>()));
                }
                else if (typeof(TParent) == typeof(Plan))
                {
                    return (IGenericController<TParent>)(new PlansController(new TestRepository<Plan>()));
                }
                else if (typeof(TParent) == typeof(Person))
                {
                    return (IGenericController<TParent>)(new PersonsController(new TestRepository<Person>()));
                }
                else if (typeof(TParent) == typeof(ShoppingList))
                {
                    return (IGenericController<TParent>)(new ShoppingListsController(new TestRepository<ShoppingList>()));
                }
                else throw new Exception();
            }

        }
    }

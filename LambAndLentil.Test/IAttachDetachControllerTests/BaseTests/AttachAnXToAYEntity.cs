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

namespace LambAndLentil.Test.IAttachDetachControllerTests.BaseTests
{
    public class AttachAnXToAYEntity<TParent, TChild> : BaseTest<TParent, TChild>
         where TParent : BaseEntity, IEntity, new()
         where TChild : BaseEntity, IEntity, new()
    {
        internal static void AttachAChildToAParent<Parent, Child>()
            where Parent : BaseEntity, IEntity, new()
        where Child : BaseEntity, IEntity, new()
        {
            Parent parent = new Parent() { ID = 3000, Description = "test Attach " };
            IRepository<Parent> localRepo = new TestRepository<Parent>();
            localRepo.Save(parent);
            Child child = new Child { ID = 3300 };
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Attach(parent, child);
            Parent returnedParent = localRepo.GetById(parent.ID) as Parent;
            if (parent.CanHaveChild(child))
            {
                if (typeof(Child) == typeof(IngredientType)) 
                {
                    Assert.AreEqual(1, returnedParent.Ingredients.Count());
                }
                if (typeof(Child) == typeof(MenuType)) 
                {
                    Assert.AreEqual(1, returnedParent.Menus.Count());
                }
                if (typeof(Child) == typeof(PlanType)) 
                {
                    Assert.AreEqual(1, returnedParent.Plans.Count());
                }
                if (typeof(Child) == typeof(RecipeType)) 
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
                if (typeof(Child) == typeof(IngredientType))
                {
                    Assert.AreEqual(0, returnedParent.Ingredients.Count());
                }
                if (typeof(Child) == typeof(MenuType))
                {
                    Assert.AreEqual(0, returnedParent.Menus.Count());
                }
                if (typeof(Child) == typeof(PlanType))
                {
                    Assert.AreEqual(0, returnedParent.Plans.Count());
                }
                if (typeof(Child) == typeof(RecipeType))
                {
                    Assert.AreEqual(0, returnedParent.Recipes.Count());
                }

                Assert.AreEqual("alert-warning", adr.AlertClass);
                Assert.AreEqual("Element Could not Be Attached - So It Could Not Be Detached!", adr.Message);
                Assert.AreEqual(3, routeValues.Count);
                Assert.AreEqual(parent.ID, routeValues.ElementAt(0));
                Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(2).ToString());
                Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(1).ToString());
            }
        }
    }
}

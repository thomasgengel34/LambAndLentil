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
using System.Collections.Generic;

namespace LambAndLentil.Test.IAttachDetachControllerTests.BaseTests
{
    public class BaseControllerShouldDetachXAndReturn<TParent, TChild> : BaseTest<TParent, TChild>
        where TParent : BaseEntity, IEntity, new()
        where TChild : BaseEntity, IEntity, new()
    {
        protected IEntityChildClassIngredients parent;
        protected IIngredient child;

        public BaseControllerShouldDetachXAndReturn() => parent = (IEntityChildClassIngredients)Parent;

        protected void BaseDetailWithSuccessWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndSelectionSetIsNotSupplied()
        {
            child = (IIngredient)Child;
            parent.Ingredients.Add((IngredientType)child);
            ParentRepo.Update((TParent)parent, parent.ID);

            ActionResult ar = Controller.DetachAllIngredients(parent.ID, null);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual(1, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual("All Ingredients Were Successfully Detached!", adr.Message);
        }

        protected void BaseDetailWithSuccessWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndSelectionSetIsSupplied()
        {
            child = (IIngredient)Child;
            parent.Ingredients.Add((IngredientType)child);
            ParentRepo.Update((TParent)parent, parent.ID);
            List<IngredientType> list = new List<IngredientType>() { (IngredientType)child };


            ActionResult ar = Controller.DetachAllIngredients(parent.ID, list);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual(1, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual("All Ingredients Were Successfully Detached!", adr.Message);
        }

        protected void BaseDetailWithSuccessWhenIDisValidAndThereAreThreeChildrenOnListWhenDetachingAll()
        {
            child = (IIngredient)Child;

            IEntity ingredient1 = new IngredientType() { ID = 700, Name = "First" };
            IEntity ingredient2 = new IngredientType() { ID = 701, Name = "Second" };
            parent.Ingredients.Add((IngredientType)child);
            parent.Ingredients.Add((IngredientType)ingredient1);
            parent.Ingredients.Add((IngredientType)ingredient2);

            List<IngredientType> list = new List<IngredientType>()
            { (IngredientType)child,
               (IngredientType)ingredient1,
               (IngredientType)ingredient2};
            ParentRepo.Update((TParent)parent, parent.ID);

            ActionResult ar = Controller.DetachAllIngredients(parent.ID, list);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            IEntityChildClassIngredients returnedParent = (IEntityChildClassIngredients)ParentRepo.GetById(parent.ID);
            int returnedCount = returnedParent.Ingredients.Count;

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual(1, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual("All Ingredients Were Successfully Detached!", adr.Message);
            Assert.AreEqual(0, returnedCount);
        }

        protected void BaseDetailWithErrorWhenIDisNotForAFoundParentWhenDetachingAll()
        {
            ActionResult ar = Controller.DetachAllIngredients(9000, new List<IngredientType>());
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            IEntityChildClassIngredients returnedParent = (IEntityChildClassIngredients)ParentRepo.GetById(parent.ID);
            int returnedCount = returnedParent.Ingredients.Count;

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual(UIViewType.Index.ToString(), rtrr.RouteValues.ElementAt(0).Value);

            Assert.AreEqual(1, rtrr.RouteValues.Count);
            Assert.AreEqual("Ingredient was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }

        protected void BaseDetailWithDangerWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndChildCannotBeAttachedWhenDetachingAll()
        {
            ActionResult ar = Controller.DetachAllMenus(Parent.ID, new List<MenuType>());
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            Assert.IsNotNull(ar);
            Assert.AreEqual(1, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit, rtrr.RouteValues.ElementAt(1).Value);
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value);

            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual("Element Could not Be Attached!", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }

        protected void BaseDetailWithErrorWhenIDisNotForAFoundParentWhenDetachingAndChildCannotBeAttachedWhenDetachingAll()
        {
            ActionResult ar= new EmptyResult();
            if (typeof(TChild) == typeof(MenuType))
            {
                  ar = Controller.DetachAllMenus(800000, new List<MenuType>());
            }
            else if (typeof(TChild) == typeof(PlanType))
            {
                  ar = Controller.DetachAllPlans(800000, new List<PlanType>());
            }
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            Assert.IsNotNull(ar);
            Assert.AreEqual(UIViewType.Index.ToString(), rtrr.RouteValues.ElementAt(0).Value);


            Assert.AreEqual(1, rtrr.RouteValues.Count);
            Assert.AreEqual("Ingredient was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass); 
        } 
    }
}

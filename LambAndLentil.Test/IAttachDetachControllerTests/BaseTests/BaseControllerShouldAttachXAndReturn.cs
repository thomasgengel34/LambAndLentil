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

namespace LambAndLentil.Test.IAttachDetachControllerTests.BaseTests
{
    public class BaseControllerShouldAttachXAndReturn<TParent, TChild> : BaseTest<TParent, TChild>
        where TParent : BaseEntity, IEntity, new()
        where TChild : BaseEntity, IEntity, new()
    {
        public IRepository<TParent> Repo { get; set; }
        private TChild child { get; set; }

        public BaseControllerShouldAttachXAndReturn()
        {
            Repo = new TestRepository<TParent>();
            child = new TChild();
        }

       internal  void BaseDetailWithSuccessWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSupplied()
        {
            if (typeof(TChild) == typeof(IngredientType))
            {
                ActionResult ar = Controller.Attach(Repo,Parent.ID, (IngredientType)Child,0);
                AlertDecoratorResult adr = (AlertDecoratorResult)ar;
                RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

                // Assert
                Assert.IsNotNull(ar);
                Assert.AreEqual(1, rtrr.RouteValues.ElementAt(0).Value);
                Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
                Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
                Assert.AreEqual(3, rtrr.RouteValues.Count);
                Assert.AreEqual("Ingredient was Successfully Attached!", adr.Message);
            }
            if (typeof(TChild) == typeof(MenuType))
            {
                ActionResult ar = Controller.Attach(Repo,Parent.ID, (MenuType)Child,0);
                AlertDecoratorResult adr = (AlertDecoratorResult)ar;
                RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

                // Assert
                Assert.IsNotNull(ar);
                Assert.AreEqual(1, rtrr.RouteValues.ElementAt(0).Value);
                Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
                Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
                Assert.AreEqual(3, rtrr.RouteValues.Count);
                Assert.AreEqual("Menu was Successfully Attached!", adr.Message);
            }
        }

        protected void BaseReturnsIndexWithWarningWithNullParent()
        {

            ActionResult ar = Controller.Attach(Repo, null, new Domain.Entities.Ingredient(), 0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            string message = adr.Message;

            // Assert
            Assert.AreEqual(ParentClassName + " was not found", message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }

        protected void BaseIndexWithErrorWhenParentIDIsNotForAnExistingIngredient()
        {  // Todo: add logging
            // Act 
            ActionResult ar = Controller.Attach(Repo,null, new IngredientType(), 0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            string message = adr.Message;

            // Assert
            Assert.AreEqual(ParentClassName + " was not found", message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }

        protected void BaseIndexWithErrorWhenParentIDIsNotForAnExistingMenu()
        {  // Todo: add logging
            // Act 
            ActionResult ar = Controller.Attach(Repo,900000, new MenuType(), 0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            string message = adr.Message;

            // Assert
            Assert.AreEqual(ParentClassName + " was not found", message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }

        protected void BaseDetailWithErrorWhenParentIDIsValidAndChildIsNotValid() { }


      

        protected void BaseDetailWithSuccessWhenParentIDIsValidAndChildIstValidAndOrderNumberIsInUseWhenAttaching()
        {
            Parent.ID = 55;
            Child.ID = 500;
            ChildRepo.Save((TChild)Child);
            if (typeof(TChild) == typeof(IngredientType))
            {
                IEntityChildClassIngredients parent = (IEntityChildClassIngredients)Parent;
                IIngredient child = (IIngredient)Child;
                parent.Ingredients.Add((IngredientType)Child);
                ParentRepo.Save((TParent)Parent);

                IIngredient secondChild = new IngredientType() { ID = 60, Name = "second child" };

                // Act
                ActionResult ar = Controller.Attach(Repo,55, (IngredientType)secondChild, 0);
                AlertDecoratorResult adr = (AlertDecoratorResult)ar;
                string message = adr.Message;

                // Assert
                Assert.AreEqual("alert-success", adr.AlertClass);
                Assert.AreEqual("Ingredient was Successfully Attached!", message);
            }
            else
            {
                Assert.IsFalse(1 == 1, "class not supported");
            }
        }

        protected void BaseDetailWithSuccessWhenParentIDIsValidAndChildIsValidAndOrderNumberIsGreaterThanTheNumberOfElementsWhenAttaching()
        {
            ActionResult ar = Controller.Attach(Repo,Parent.ID, (IngredientType)Child,  AttachOrDetach.Attach, 70000);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual(1, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual("Ingredient was Successfully Attached!", adr.Message);
            Assert.AreEqual("alert-success", adr.AlertClass);
        }

        protected void BaseDetailWithErrorWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSuppliedWhenAttachingUnattachableChild()
        {  // Parent Ingredient for initial test, child Menu.  TODO: expand
            ActionResult ar= Controller.Attach(Repo,Parent.ID,  child, AttachOrDetach.Attach, 70000);
            

            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual(1, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual("Element Could not Be Attached!", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }
    }
}

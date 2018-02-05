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

namespace LambAndLentil.Test.IAttachDetachControllerTests.BaseTests
{
    public class BaseControllerShouldAttachXAndReturn<TParent, TChild> : BaseTest<TParent, TChild>
        where TParent : BaseEntity, IEntity, new()
        where TChild : BaseEntity, IEntity, new()
    {
        public IRepository<TParent> Repo { get; set; } // TODO: look at changing this to a private field
        private TChild child { get; set; }   // TODO: look at changing this to a field
        private string childName;

        public BaseControllerShouldAttachXAndReturn()
        {
            Repo = new TestRepository<TParent>();
            child = new TChild();
            childName = typeof(TChild).ToString().Split('.').Last();
        }

       internal  void BaseDetailWithSuccessWhenParentIDIsValidAndChildIsValid()
        {
            if (typeof(TChild) == typeof(IngredientType))
            {
                ActionResult ar = Controller.Attach(Parent, (IngredientType)Child );
                AlertDecoratorResult adr = (AlertDecoratorResult)ar;
                RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
                 
                Assert.IsNotNull(ar);
                Assert.AreEqual(1, rtrr.RouteValues.ElementAt(0).Value);
                Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
                Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
                Assert.AreEqual(3, rtrr.RouteValues.Count);
                Assert.AreEqual("Ingredient was Successfully Attached!", adr.Message);
                Assert.AreEqual("alert-success", adr.AlertClass);

            }
            if (typeof(TChild) == typeof(MenuType))
            {
                ActionResult ar = Controller.Attach(Parent, (MenuType)Child );
                AlertDecoratorResult adr = (AlertDecoratorResult)ar;
                RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
                 
                Assert.IsNotNull(ar);
                Assert.AreEqual(1, rtrr.RouteValues.ElementAt(0).Value);
                Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
                Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
                Assert.AreEqual(3, rtrr.RouteValues.Count);
                Assert.AreEqual("Menu was Successfully Attached!", adr.Message);
                Assert.AreEqual("alert-success", adr.AlertClass);
            }  
        }

        internal void BaseReturnsIndexWithWarningWithNullParent()
        {

            ActionResult ar = Controller.Attach( null, new Domain.Entities.Ingredient() );
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            string message = adr.Message;

            // Assert
            Assert.AreEqual(ParentClassName + " was not found", message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }

        internal void BaseIndexWithErrorWhenParentIDIsNotForAnExistingIngredient()
        {  // Todo: add logging 
            ActionResult ar = Controller.Attach(null, new IngredientType() );
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            string message = adr.Message;
             
            Assert.AreEqual(ParentClassName + " was not found", message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }
         

        internal void BaseDetailWithErrorWhenParentIDIsValidAndChildIsNotValid() {
            TParent parent = new TParent() { ID = 314 };
            TChild child = new TChild();
            Repo.Save(parent);
            child = null;
            ActionResult ar = Controller.Attach(parent, child);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            string message = adr.Message;

            Assert.IsNotNull(ar);
            Assert.AreEqual(parent.ID, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
            Assert.AreEqual(3, rtrr.RouteValues.Count); 
            Assert.AreEqual("Element Could not Be Attached - So It Could Not Be Detached!", message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }//   RedirectToAction(UIViewType.Details.ToString(), new { id = parentID, actionMethod = UIViewType.Edit }).WithWarning(EntityName + " was not found");
        // private ActionResult HandleParentCannotAttachChild(IEntity parent) => RedirectToAction(UIViewType.Details.ToString(), new { id = parent.ID, actionMethod = UIViewType.Edit }).WithError("Element Could not Be Attached - So It Could Not Be Detached!");   The test reflects what is happening. TODO: recode to get the first, not the second, message and alert class. 


        internal void BaseDetailWithSuccessWhenParentIDIsValidAndChildIsValidWhenAttaching()
        {
            ActionResult ar = Controller.Attach(Parent, (TChild)Child );
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
 
            Assert.IsNotNull(ar);
            Assert.AreEqual(1, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual(childName+" was Successfully Attached!", adr.Message);
            Assert.AreEqual("alert-success", adr.AlertClass);
        }

        internal void BaseDetailWithErrorWhenParentIDIsValidAndChildIsValidWhenAttachingUnattachableChild()
        {   
            ActionResult ar= Controller.Attach(Parent,  child ); 
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
}

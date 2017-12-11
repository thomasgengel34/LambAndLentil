using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;
using MenuType = LambAndLentil.Domain.Entities.Menu;
using PlanType = LambAndLentil.Domain.Entities.Plan;
using RecipeType = LambAndLentil.Domain.Entities.Recipe;
using ShoppingListType = LambAndLentil.Domain.Entities.ShoppingList;

namespace LambAndLentil.Test.IAttachDetachControllerTests.BaseTests
{
    public class BaseControllerShouldDetachXAndReturn<TParent, TChild> : BaseTest<TParent, TChild>
        where TParent : BaseEntity, IEntity, new()
        where TChild : BaseEntity, IEntity, new()
    {
        protected IEntityChildClassIngredients parent;
        protected IEntityChildClassRecipes parentRecipe;
        protected IIngredient child;
        protected IRecipe recipeChild;

        public List<IngredientType> MyIngredientsList { get; set; }
        public List<RecipeType> MyRecipesList { get; set; }
        public IngredientType Ingredient1 { get; set; }
        public IngredientType Ingredient2 { get; set; }
        public IngredientType Ingredient3 { get; set; }
        public IngredientType Ingredient4 { get; set; }
        public IngredientType Ingredient5 { get; set; }

        public BaseControllerShouldDetachXAndReturn()
        {
            parent.ID = 1;
            if (typeof(TChild) == typeof(IngredientType))
            {
                parent = (IEntityChildClassIngredients)Parent;

                IngredientType Ingredient1 = new IngredientType() { ID = 101, Name = "First" };
                IngredientType Ingredient2 = new IngredientType() { ID = 102, Name = "Second" };
                IngredientType Ingredient3 = new IngredientType() { ID = 103, Name = "Third" };
                IngredientType Ingredient4 = new IngredientType() { ID = 104, Name = "Fourth" };
                IngredientType Ingredient5 = new IngredientType() { ID = 105, Name = "Fifth" };


                MyIngredientsList = new List<IngredientType>()
            {
                  Ingredient1,
                  Ingredient2,
                  Ingredient3,
                  Ingredient4,
                  Ingredient5,
            };
                parent.Ingredients.AddRange(MyIngredientsList);
                ParentRepo.Save((TParent)parent);
            }

            ParentRepo.Add((TParent)parent);
            if (typeof(TChild) == typeof(RecipeType))
            {
                parentRecipe = (IEntityChildClassRecipes)Parent;

                RecipeType Recipe1 = new RecipeType() { ID = 101, Name = "First" };
                RecipeType Recipe2 = new RecipeType() { ID = 102, Name = "Second" };
                RecipeType Recipe3 = new RecipeType() { ID = 103, Name = "Third" };
                RecipeType Recipe4 = new RecipeType() { ID = 104, Name = "Fourth" };
                RecipeType Recipe5 = new RecipeType() { ID = 105, Name = "Fifth" };


               MyRecipesList = new List<RecipeType>() 
            {
                  Recipe1,
                  Recipe2,
                  Recipe3,
                  Recipe4,
                  Recipe5,
            };
                parentRecipe.Recipes.AddRange(MyRecipesList);
                ParentRepo.Save((TParent)parentRecipe);
            }  
        }




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
            Assert.AreEqual(parent.ID, rtrr.RouteValues.ElementAt(0).Value);
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
            Assert.AreEqual(parent.ID, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual("All Ingredients Were Successfully Detached!", adr.Message);
        }

        protected void BaseDetailWithSuccessWhenIDisValidAndThereAreThreeChildrenOnListWhenDetachingAll()
        {
            ActionResult ar;
            AlertDecoratorResult adr;
            RedirectToRouteResult rtrr;
            int returnedCount;
            if (typeof(TChild) == typeof(IngredientType))
            {
                child = (IIngredient)Child;
                IEntity ingredient1 = new IngredientType() { ID = 700, Name = "First" };
                IEntity ingredient2 = new IngredientType() { ID = 701, Name = "Second" };
                parent.Ingredients.Add((IngredientType)child);
                parent.Ingredients.Add((IngredientType)ingredient1);
                parent.Ingredients.Add((IngredientType)ingredient2);
                ar = Controller.DetachAllIngredients(parent.ID, MyIngredientsList);
                adr = (AlertDecoratorResult)ar;
                rtrr = (RedirectToRouteResult)adr.InnerResult;
                IEntityChildClassIngredients returnedParent = (IEntityChildClassIngredients)ParentRepo.GetById(parent.ID);
                returnedCount = returnedParent.Ingredients.Count;
            }
            else if (typeof(TChild) == typeof(RecipeType))
            {
                recipeChild = (IRecipe)Child;
                IEntity ingredient1 = new RecipeType() { ID = 700, Name = "First" };
                IEntity ingredient2 = new RecipeType() { ID = 701, Name = "Second" };
                parentRecipe.Recipes.Add((RecipeType)child);
                parentRecipe.Recipes.Add((RecipeType)ingredient1);
                parentRecipe.Recipes.Add((RecipeType)ingredient2);
                ar = Controller.DetachAllRecipes(parent.ID, MyRecipesList);
                adr = (AlertDecoratorResult)ar;
                rtrr = (RedirectToRouteResult)adr.InnerResult;
                IEntityChildClassRecipes returnedParent = (IEntityChildClassRecipes)ParentRepo.GetById(parent.ID);
                returnedCount = returnedParent.Recipes.Count;
            }
            else
            {
                // TODO: work this out for all entity types
                throw new System.Exception();
            }

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual(parent.ID, rtrr.RouteValues.ElementAt(0).Value);
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

            Assert.AreEqual(parent.ID, rtrr.RouteValues.Count);
            Assert.AreEqual(ParentClassName + " was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }

        protected void BaseDetailWithDangerWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndChildCannotBeAttachedWhenDetachingAll()
        {
            ActionResult ar = Controller.DetachAllMenus(Parent.ID, new List<MenuType>());
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            Assert.IsNotNull(ar);
            Assert.AreEqual(parent.ID, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit, rtrr.RouteValues.ElementAt(1).Value);
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value);

            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual("Element Could not Be Attached!", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }

        protected void BaseDetailWithErrorWhenIDisNotForAFoundParentWhenDetachingAndChildCannotBeAttachedWhenDetachingAll()
        {
            ActionResult ar = new EmptyResult();
            if (typeof(TChild) == typeof(IngredientType))
            {
                ar = Controller.DetachAllIngredients(800000, new List<IngredientType>());
            }
            if (typeof(TChild) == typeof(MenuType))
            {
                ar = Controller.DetachAllMenus(800000, new List<MenuType>());
            }
            else if (typeof(TChild) == typeof(PlanType))
            {
                ar = Controller.DetachAllPlans(800000, new List<PlanType>());
            }
            else if (typeof(TChild) == typeof(RecipeType))
            {
                ar = Controller.DetachAllRecipes(800000, new List<RecipeType>());
            }
            else if (typeof(TChild) == typeof(ShoppingListType))
            {
                ar = Controller.DetachAllShoppingLists(800000, new List<ShoppingListType>());
            }
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            Assert.IsNotNull(ar);
            Assert.AreEqual(UIViewType.Index.ToString(), rtrr.RouteValues.ElementAt(0).Value);


            Assert.AreEqual(1, rtrr.RouteValues.Count);
            Assert.AreEqual("Ingredient was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }



        protected void BaseDetailWithSuccessWhenIDisValidAndAlChildrenOnListExistWhendDetachASetOfIngredients()
        {
            List<IngredientType> itemsToBeRemoved = MyIngredientsList;
            itemsToBeRemoved.Remove(Ingredient2);
            itemsToBeRemoved.Remove(Ingredient4);

            ActionResult ar = Controller.DetachASetOfIngredients(parent.ID, itemsToBeRemoved);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            IIngredient returnedIngredient = (IIngredient)ParentRepo.GetById(parent.ID);

            Assert.AreEqual(2, returnedIngredient.Ingredients.Count);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("All Ingredients Were Successfully Detached!", adr.Message);

        }

        protected void BaseDetailWithSuccessWhenIDisValidAndNotAllChildrenOnListExistWhenDetachASetOfIngredients()
        {
            IngredientType nullIngredient = null;

            MyIngredientsList.Add(nullIngredient);
            MyIngredientsList.Remove(Ingredient4);

            ActionResult ar = Controller.DetachASetOfIngredients(parent.ID, MyIngredientsList);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            IIngredient returnedIngredient = (IIngredient)ParentRepo.GetById(parent.ID);

            Assert.AreEqual(1, returnedIngredient.Ingredients.Count);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("All Ingredients Were Successfully Detached!", adr.Message);
        }

        protected void BaseDetailWithErrorWhenIDisValidAndNoChildrenOnListExistWhenDetachASetOfIngredients()
        {
            IngredientType nullIngredient = null;


            MyIngredientsList.Clear();
            MyIngredientsList.Add(nullIngredient);
            ParentRepo.Update((TParent)parent, parent.ID);

            ActionResult ar = Controller.DetachASetOfIngredients(parent.ID, MyIngredientsList);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            IIngredient returnedIngredient = (IIngredient)ParentRepo.GetById(parent.ID);

            Assert.AreEqual(5, returnedIngredient.Ingredients.Count);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("All Ingredients Were Successfully Detached!", adr.Message);
        }

        protected void BaseDetailWithSuccessWhenIDisValidAndThereAreThreeChildrenOnListWhenDetachASetOfIngredients()
        {
            MyIngredientsList.Remove(Ingredient1);
            MyIngredientsList.Remove(Ingredient2);
            ActionResult ar = Controller.DetachASetOfIngredients(parent.ID, MyIngredientsList);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            IIngredient returnedIngredient = (IIngredient)ParentRepo.GetById(parent.ID);

            Assert.AreEqual(2, returnedIngredient.Ingredients.Count);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("All Ingredients Were Successfully Detached!", adr.Message);
        }

        protected void BaseDetailWithWarningWhenIDisNotForAFoundParentWhenDetachASetOfIngredients()
        {
            ActionResult ar = Controller.DetachASetOfIngredients(9001, MyIngredientsList);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            IIngredient returnedIngredient = (IIngredient)ParentRepo.GetById(parent.ID);

            Assert.AreEqual(5, returnedIngredient.Ingredients.Count);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Ingredient was not found", adr.Message);
        }

        protected void BaseReturnsIndexWithWarningWithNullParentWhenDetaching()
        {

            ActionResult ar = Controller.DetachIngredient(null, new IngredientType(), 0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            string message = adr.Message;

            // Assert
            Assert.AreEqual(ParentClassName + " was not found", message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }

        protected void BaseDetailWithErrorWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSuppliedWhenDetachingUnattachableChild()
        {
            if (typeof(TChild) == typeof(IngredientType))
            {
                ActionResult ar = Controller.DetachIngredient(Parent.ID, (IngredientType)Child);
                AlertDecoratorResult adr = (AlertDecoratorResult)ar;
                RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

                // Assert
                Assert.IsNotNull(ar);
                Assert.AreEqual(Parent.ID, rtrr.RouteValues.ElementAt(0).Value);
                Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
                Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
                Assert.AreEqual(3, rtrr.RouteValues.Count);
                Assert.AreEqual("Element Could not Be Attached - so it could not be detached", adr.Message);
            }
            if (typeof(TChild) == typeof(MenuType))
            {
                ActionResult ar = Controller.DetachMenu(Parent.ID, (MenuType)Child);
                AlertDecoratorResult adr = (AlertDecoratorResult)ar;
                RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

                // Assert
                Assert.IsNotNull(ar);
                Assert.AreEqual(1, rtrr.RouteValues.ElementAt(0).Value);
                Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
                Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
                Assert.AreEqual(3, rtrr.RouteValues.Count);
                Assert.AreEqual("Element Could not Be Attached - so it could not be detached", adr.Message);
            }
        }

        protected void BaseIndexWithWarningWhenParentIDIsNotForAnExistingIngredientWhenDetachingUnattachableChild()
        {
            // Parent Ingredient for initial test, child Menu.  TODO: expand
            Parent.ID = 90000;

            ActionResult ar = new ViewResult();
            if (typeof(TChild) == typeof(IngredientType))
            {
                ar = Controller.DetachIngredient(Parent.ID, (IngredientType)Child, 70000);
            }
            else if (typeof(TChild) == typeof(MenuType))
            {
                ar = Controller.DetachMenu(Parent.ID, (MenuType)Child, 70000);
            }
            else if (typeof(TChild) == typeof(PlanType))
            {
                ar = Controller.DetachPlan(Parent.ID, (PlanType)Child, 70000);
            }
            else if (typeof(TChild) == typeof(RecipeType))
            {
                ar = Controller.DetachRecipe(Parent.ID, (RecipeType)Child, 70000);
            }
            else if (typeof(TChild) == typeof(ShoppingListType))
            {
                ar = Controller.DetachShoppingList(Parent.ID, (ShoppingListType)Child, 70000);
            }

            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual(UIViewType.Index.ToString(), rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(1, rtrr.RouteValues.Count);
            Assert.AreEqual("Ingredient was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }


        protected void BaseIndexWithWarningWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberIsNegativeWhenAttachingUnattachableChild()
        {
            int orderNumber = -1;
            // Parent Ingredient for initial test, child Menu.  TODO: expand
            ActionResult ar = new ViewResult();
            if (typeof(TChild) == typeof(IngredientType))
            {
                ar = Controller.DetachIngredient(Parent.ID, (IngredientType)Child, orderNumber);
            }
            else if (typeof(TChild) == typeof(MenuType))
            {
                ar = Controller.DetachMenu(Parent.ID, (MenuType)Child, orderNumber);
            }
            else if (typeof(TChild) == typeof(PlanType))
            {
                ar = Controller.DetachPlan(Parent.ID, (PlanType)Child, orderNumber);
            }
            else if (typeof(TChild) == typeof(RecipeType))
            {
                ar = Controller.DetachRecipe(Parent.ID, (RecipeType)Child, orderNumber);
            }
            else if (typeof(TChild) == typeof(ShoppingListType))
            {
                ar = Controller.DetachShoppingList(Parent.ID, (ShoppingListType)Child, orderNumber);
            }

            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual(UIViewType.Index.ToString(), rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(1, rtrr.RouteValues.Count);
            Assert.AreEqual("Order Number Was Negative! Nothing was detached", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }

        protected void BaseDetailWithErrorWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSuppliedWhenAttachingUnattachableChild()
        {

            // Parent Ingredient for initial test, child Menu.  TODO: expand
            ActionResult ar = new ViewResult();
            if (typeof(TChild) == typeof(IngredientType))
            {
                ar = Controller.DetachIngredient(Parent.ID, (IngredientType)Child);
            }
            else if (typeof(TChild) == typeof(MenuType))
            {
                ar = Controller.DetachMenu(Parent.ID, (MenuType)Child);
            }
            else if (typeof(TChild) == typeof(PlanType))
            {
                ar = Controller.DetachPlan(Parent.ID, (PlanType)Child);
            }
            else if (typeof(TChild) == typeof(RecipeType))
            {
                ar = Controller.DetachRecipe(Parent.ID, (RecipeType)Child);
            }
            else if (typeof(TChild) == typeof(ShoppingListType))
            {
                ar = Controller.DetachShoppingList(Parent.ID, (ShoppingListType)Child);
            }

            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            Assert.IsNotNull(ar);
            Assert.AreEqual(parent.ID, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual("Element Could not Be Attached - so it could not be detached", adr.Message);
        }

        //protected void BaseIndexWithErrorWhenParentIDIsNotForAnExistingIngredient()
        //{
        //    ActionResult ar = Controller.Index(9000);
        //    AlertDecoratorResult adr = (AlertDecoratorResult)ar;
        //    RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

        //    // Assert
        //    Assert.IsNotNull(ar);
        //    Assert.AreEqual(UIViewType.Index.ToString(), rtrr.RouteValues.ElementAt(0).Value);
        //    Assert.AreEqual(1, rtrr.RouteValues.Count);
        //    Assert.AreEqual("Ingredient was not found", adr.Message);
        //    Assert.AreEqual("alert-warning", adr.AlertClass);
        //}

        protected void BaseDetailWithErrorWhenParentIDIsValidAndChildIsValidAndThereIsNoOrderNumberSuppliedAndThereIsNoChildAttachedWhenDetachingAllIngredients()
        {

            parent.Ingredients.Clear();
            ParentRepo.Add((TParent)parent);
            string childClassName = typeof(TChild).ToString().Split('.').Last();

            ActionResult ar = Controller.DetachAllIngredients(Parent.ID, null);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            Assert.IsNotNull(ar);
            Assert.AreEqual(parent.ID, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual("No " + childClassName + "s were attached!", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }
    }
}
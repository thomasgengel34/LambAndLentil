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

        protected TParent parent;
        protected TChild child;
        protected TChild child1 { get; set; }

        protected TChild child2 = new TChild();
        protected TChild child3 = new TChild();
        protected TChild child4 = new TChild();


        private string parentName = typeof(TParent).ToString().Split('.').Last();
        private string childName = typeof(TChild).ToString().Split('.').Last();

        public List<IngredientType> MyIngredientsList { get; set; }
        public List<RecipeType> MyRecipesList { get; set; }

        public IngredientType Ingredient1 { get; set; }
        public IngredientType Ingredient2 { get; set; }
        public IngredientType Ingredient3 { get; set; }
        public IngredientType Ingredient4 { get; set; }
        public IngredientType Ingredient5 { get; set; }
        public IEntityChildClassIngredients parentWithIngredients { get; set; }
        public IEntityChildClassRecipes parentWithRecipes { get; set; }

        public RecipeType Recipe1 { get; set; }
        public RecipeType Recipe2 { get; set; }
        public RecipeType Recipe3 { get; set; }
        public RecipeType Recipe4 { get; set; }
        public RecipeType Recipe5 { get; set; }
        private List<TChild> MyChildrenList { get; set; }

        public BaseControllerShouldDetachXAndReturn()
        {
            parent = new TParent() { ID = 1 };
            ParentRepo.Save(parent);
            child = new TChild();
            child1 = new TChild();
            child2 = new TChild();
            child3 = new TChild();
            child4 = new TChild();
            List<TChild> MyChildrenList = new List<TChild>() { child1, child2, child3, child4 };

            bool IsChildAttachable = BaseEntity.ParentCanAttachChild(new TParent(), new TChild());
            if (!IsChildAttachable)
            {
                BaseDetailWithDangerWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndChildCannotBeAttachedWhenDetachingAll();
            }
            else
            {
                //if (typeof(TChild) == typeof(IngredientType))
                //{
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

                parentWithIngredients = (IEntityChildClassIngredients)parent;
                parentWithIngredients.Ingredients.AddRange(MyIngredientsList);
                ParentRepo.Save((TParent)parentWithIngredients);
                //}


                if (typeof(TChild) == typeof(RecipeType))
                {

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
                    parentWithRecipes = (IEntityChildClassRecipes)parent;
                    parentWithRecipes.Recipes.AddRange(MyRecipesList);
                    ParentRepo.Save((TParent)parentWithRecipes);
                }
            }
        }




        protected void BaseDetailWithSuccessWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndSelectionSetIsNotSupplied()
        {
            parentWithIngredients.Ingredients.Add(Ingredient1);
            ParentRepo.Update((TParent)parentWithIngredients, parent.ID);

            ActionResult ar = Controller.DetachASetOf<TChild>(parent.ID, null);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            TParent returnedParent = ParentRepo.GetById(parentWithIngredients.ID);


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
            parentWithIngredients.Ingredients.Add(Ingredient1);
            ParentRepo.Update((TParent)parentWithIngredients, parent.ID);
            ParentRepo.Update((TParent)parent, parent.ID);
            List<TChild> list = new List<TChild>() { child };


            ActionResult ar = Controller.DetachASetOf(parent.ID, list);
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
                parentWithIngredients.Ingredients.Clear();
                parentWithIngredients.Ingredients.Add(Ingredient1);
                parentWithIngredients.Ingredients.Add(Ingredient2);
                parentWithIngredients.Ingredients.Add(Ingredient3);
                ParentRepo.Save((TParent)parentWithIngredients);
                ar = Controller.DetachASetOf<TChild>(parent.ID, MyChildrenList);
                adr = (AlertDecoratorResult)ar;
                rtrr = (RedirectToRouteResult)adr.InnerResult;
                IEntityChildClassIngredients returnedParent = (IEntityChildClassIngredients)ParentRepo.GetById(parent.ID);
                returnedCount = returnedParent.Ingredients.Count;
            }
            else if (typeof(TChild) == typeof(RecipeType))
            {
                parentWithRecipes.Recipes.Clear();
                parentWithRecipes.Recipes.Add(Recipe1);
                parentWithRecipes.Recipes.Add(Recipe2);
                parentWithRecipes.Recipes.Add(Recipe3);
                ParentRepo.Save((TParent)parentWithRecipes);
                ar = Controller.DetachASetOf(parent.ID, MyChildrenList);
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
            Assert.AreEqual("All " + childName + "s Were Successfully Detached!", adr.Message);
            Assert.AreEqual(0, returnedCount);
        }

        protected void BaseDetailWithErrorWhenIDisNotForAFoundParentWhenDetachingAll()
        {
            ActionResult ar = Controller.DetachASetOf(9000, MyChildrenList);
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
            ActionResult ar = Controller.DetachASetOf(parent.ID, MyChildrenList);
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
            List<TChild> list = new List<TChild>();
            ActionResult ar = new EmptyResult();
            ar = Controller.DetachASetOf<TChild>(80000000, list);

            ar = Controller.DetachASetOf(800000, list);

            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            Assert.IsNotNull(ar);
            Assert.AreEqual(UIViewType.Index.ToString(), rtrr.RouteValues.ElementAt(0).Value);


            Assert.AreEqual(1, rtrr.RouteValues.Count);
            Assert.AreEqual(parentName + " was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }



        protected void BaseDetailWithSuccessWhenIDisValidAndAlChildrenOnListExistWhendDetachASetOfIngredients()
        {
            List<IngredientType> itemsToBeRemoved = MyIngredientsList;
            itemsToBeRemoved.RemoveAt(1);
            itemsToBeRemoved.RemoveAt(3);

            ActionResult ar = Controller.DetachASetOf(parent.ID, itemsToBeRemoved);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;


            CheckIngredientsCount(2);


            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("All Ingredients Were Successfully Detached!", adr.Message);

        }

        private void CheckIngredientsCount(int correctNumber)
        {
            TParent item = ParentRepo.GetById(parent.ID);
            Assert.AreEqual(correctNumber, ((IEntityChildClassIngredients)item).Ingredients.Count);
        }

        protected void BaseDetailWithSuccessWhenIDisValidAndNotAllChildrenOnListExistWhenDetachASetOfIngredients()
        {
            IngredientType nullIngredient = null;

            MyIngredientsList.Add(nullIngredient);
            MyIngredientsList.RemoveAt(1);

            ActionResult ar = Controller.DetachASetOf(parent.ID, MyIngredientsList);
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

            ActionResult ar = Controller.DetachASetOf(parent.ID, MyIngredientsList);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            CheckIngredientsCount(5);

            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("All Ingredients Were Successfully Detached!", adr.Message);
        }

        protected void BaseDetailWithSuccessWhenIDisValidAndThereAreThreeChildrenOnListWhenDetachASetOfIngredients()
        {
            MyIngredientsList.Clear();
            MyIngredientsList.Add(new IngredientType() { ID = 1000 });
            MyIngredientsList.Add(new IngredientType() { ID = 1001 });
            MyIngredientsList.Add(new IngredientType() { ID = 1002 });
            IEntityChildClassIngredients parent = (IEntityChildClassIngredients)Parent;
            parent.Ingredients.Clear();
            parent.Ingredients.AddRange(MyIngredientsList);
            ParentRepo.Save((TParent)parent);
            List<IngredientType> MyIngredientsToRemoveList = new List<IngredientType>();
            MyIngredientsToRemoveList.Add(new IngredientType() { ID = 1000 });
            ActionResult ar = Controller.DetachASetOf(parent.ID, MyIngredientsToRemoveList);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            IIngredient returnedIngredient = (IIngredient)ParentRepo.GetById(parent.ID);

            Assert.AreEqual(2, returnedIngredient.Ingredients.Count);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("All Ingredients Were Successfully Detached!", adr.Message);
        }

        protected void BaseDetailWithWarningWhenIDisNotForAFoundParentWhenDetachASetOfIngredients()
        {
            ActionResult ar = Controller.DetachASetOf(9001, MyIngredientsList);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            IIngredient returnedIngredient = (IIngredient)ParentRepo.GetById(parent.ID);

            Assert.AreEqual(5, returnedIngredient.Ingredients.Count);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Ingredient was not found", adr.Message);
        }

        protected void BaseReturnsIndexWithWarningWithNullParentWhenDetaching()
        {

            ActionResult ar = Controller.Detach(null, new IngredientType(), 0);
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
                ActionResult ar = Controller.Detach(Parent.ID, (IngredientType)Child);
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
                ActionResult ar = Controller.Detach(Parent.ID, (MenuType)Child);
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



            ActionResult ar = Controller.Detach(Parent.ID, Child, 70000);


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
            ActionResult ar =   Controller.Detach(Parent.ID, Child, orderNumber); 
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
            ActionResult ar =  Controller.Detach(Parent.ID,  Child); 
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

            parentWithIngredients.Ingredients.Clear();
            ParentRepo.Add(parent);
            string childClassName = typeof(TChild).ToString().Split('.').Last();
            List<TChild> emptyList = new List<TChild>();
            ActionResult ar = Controller.DetachASetOf(Parent.ID, emptyList);
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
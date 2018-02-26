using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.BusinessObjects;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.BaseControllerTests;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IngredientType = LambAndLentil.Domain.Entities.Ingredient;
using MenuType = LambAndLentil.Domain.Entities.Menu;
using PersonType = LambAndLentil.Domain.Entities.Person;
using PlanType = LambAndLentil.Domain.Entities.Plan;
using RecipeType = LambAndLentil.Domain.Entities.Recipe;
using ShoppingListType = LambAndLentil.Domain.Entities.ShoppingList; 

namespace LambAndLentil.Test.IAttachDetachControllerTests.BaseTests
{
   public class BaseControllerShouldDetachXAndReturn<TParent, TChild> : BaseTest<TParent, TChild>
        where TParent : BaseEntity, IEntity, new()
        where TChild : BaseEntity, IEntity, new()
    { 
       internal TParent parent;
       internal TChild child;
       internal TChild child1 { get; set; }

       internal TChild child2 = new TChild();
       internal TChild child3 = new TChild();
       internal TChild child4 = new TChild();


        private string parentName = typeof(TParent).ToString().Split('.').Last();
        private string childName = typeof(TChild).ToString().Split('.').Last();

        private static List<Ingredient> MyIngredientsList { get; set; }
        private static List<RecipeType> MyRecipesList { get; set; }

        private static IngredientType Ingredient1 { get; set; }
        private static IngredientType Ingredient2 { get; set; }
        private static IngredientType Ingredient3 { get; set; }
        private static IngredientType Ingredient4 { get; set; }
        private static IngredientType Ingredient5 { get; set; }
        private static IEntity parentWithIngredients { get; set; }
        private static IEntity parentWithRecipes { get; set; }

        private static RecipeType Recipe1 { get; set; }
        private static RecipeType Recipe2 { get; set; }
        private static RecipeType Recipe3 { get; set; }
        private static RecipeType Recipe4 { get; set; }
        private static RecipeType Recipe5 { get; set; }
        private List<IEntity> MyChildrenList { get; set; }

        public   BaseControllerShouldDetachXAndReturn()
        {
            parent = new TParent() { ID = 1 };
            ParentRepo.Save(parent);
            child = new TChild();

            bool IsChildAttachable = parent.CanHaveChild(child);
            if (IsChildAttachable)
            {

                child1 = new TChild();
                child2 = new TChild();
                child3 = new TChild();
                child4 = new TChild();
                List<IEntity> MyChildrenList = new List<IEntity>() { child1, child2, child3, child4 };

                SetUpIngredientsAndMyIngredientsList();

                parentWithIngredients = (IEntity)parent;
                if (parentWithIngredients.Ingredients == null)
                {
                    parentWithIngredients.Ingredients = new List<Ingredient>();
                }
                parentWithIngredients.Ingredients.AddRange(MyIngredientsList);
                ParentRepo.Save((TParent)parentWithIngredients);

                SetUpRecipesAndMyRecipesList();

            }

        }

        private void SetUpIngredientsAndMyIngredientsList()
        {
            Ingredient1 = new IngredientType() { ID = 101, Name = "First" };
            Ingredient2 = new IngredientType() { ID = 102, Name = "Second" };
            Ingredient3 = new IngredientType() { ID = 103, Name = "Third" };
            Ingredient4 = new IngredientType() { ID = 104, Name = "Fourth" };
            Ingredient5 = new IngredientType() { ID = 105, Name = "Fifth" };


            MyIngredientsList = new List<Ingredient>()
            {
                  Ingredient1,
                  Ingredient2,
                  Ingredient3,
                  Ingredient4,
                  Ingredient5,
            };
        }

        private void SetUpRecipesAndMyRecipesList()
        {
            Recipe1 = new RecipeType() { ID = 101, Name = "First" };
            Recipe2 = new RecipeType() { ID = 102, Name = "Second" };
            Recipe3 = new RecipeType() { ID = 103, Name = "Third" };
            Recipe4 = new RecipeType() { ID = 104, Name = "Fourth" };
            Recipe5 = new RecipeType() { ID = 105, Name = "Fifth" };


            MyRecipesList = new List<RecipeType>()
            {
                  Recipe1,
                  Recipe2,
                  Recipe3,
                  Recipe4,
                  Recipe5,
            };
        }

       internal void BaseDetailWithSuccessWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndSelectionSetIsNotSupplied()
        {
            parentWithIngredients.Ingredients.Add(Ingredient1);
            ParentRepo.Update((TParent)parentWithIngredients, parent.ID);

            ActionResult ar = controller.DetachASetOf(parent, null);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            TParent returnedParent = ParentRepo.GetById(parentWithIngredients.ID);
             
            Assert.IsNotNull(ar);
            Assert.AreEqual(parent.ID, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual("All " + childName + "s Were Successfully Detached!", adr.Message);
        }

       internal void BaseDetailWithSuccessWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndSelectionSetIsSupplied()
        {
            parentWithIngredients.Ingredients.Add(Ingredient1);
            ParentRepo.Update((TParent)parentWithIngredients, parent.ID);
            ParentRepo.Update((TParent)parent, parent.ID);
            List<IEntity> list = new List<IEntity>() { child };


            ActionResult ar = controller.DetachASetOf(parent, list);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
             
            Assert.IsNotNull(ar);
            Assert.AreEqual(parent.ID, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual("All " + childName + "s Were Successfully Detached!", adr.Message);
        }

       internal void BaseDetailWithSuccessWhenIDisValidAndThereAreThreeChildrenOnListWhenDetachingAll()
        {
            TParent parent = new TParent() { ID = 7000 };
            ActionResult ar;
            AlertDecoratorResult adr;
            RedirectToRouteResult rtrr;
            int returnedCount = 10000000;   // obviously wrong, to initialize 
            parent = (TParent)(new ChildAttachment().AddChildToParent(parent, new TChild()));
            parent = (TParent)(new ChildAttachment().AddChildToParent(parent, new TChild()));
            parent = (TParent)(new ChildAttachment().AddChildToParent(parent, new TChild()));
            ParentRepo.Save(parent); 
            ar = controller.DetachAll(parent, new TChild());

            TParent returnedParent = ParentRepo.GetById(parent.ID);
            returnedCount = child.GetCountOfChildrenOnParent(returnedParent);


            adr = (AlertDecoratorResult)ar;
            rtrr = (RedirectToRouteResult)adr.InnerResult;



            Assert.IsNotNull(ar);
            Assert.AreEqual(parent.ID, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual("All " + childName + "s Were Successfully Detached!", adr.Message);
            Assert.AreEqual(0, returnedCount);
        }
       
         
        internal void BaseDetailWithWarningWhenIDisValidAndTheSelectionSetIsNullOrEmpty() { }

       internal static  void BaseDetailWithSuccessWhenIDisValidAndAlChildrenOnListExistWhendDetachASetOfIngredients()
        {
            TParent tParent = new TParent() { ID = 1492 };

            IngredientType ingredient1 = new IngredientType() { ID = 100 };
            IngredientType ingredient2 = new IngredientType() { ID = 101 };
            IngredientType ingredient3 = new IngredientType() { ID = 102 };
            IngredientType ingredient4 = new IngredientType() { ID = 103 };
            IngredientType ingredient5 = new IngredientType() { ID = 104 };

            List<IngredientType> listToBeAdded = new List<IngredientType>()
            {
                ingredient1,
                ingredient2,
                ingredient3,
                ingredient4,
                ingredient5
               };

            tParent.Ingredients.AddRange(listToBeAdded);
            ParentRepo.Save(tParent);
            int initialIngredientCount = tParent.Ingredients.Count();
            List<IEntity> itemsToBeRemoved = new List<IEntity>()
            {
               ingredient1,
               ingredient3
            }; 
            ActionResult ar = controller.DetachASetOf(tParent , itemsToBeRemoved);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            TParent returnedt = ParentRepo.GetById(tParent.ID);
            int returnedIngredientCount = returnedt.Ingredients.Count();

            Assert.AreEqual(initialIngredientCount-2, returnedIngredientCount);

            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("All Ingredients Were Successfully Detached!", adr.Message);
        } 

        private void CheckIngredientsCount(int correctNumber)
        {
            TParent item = ParentRepo.GetById(parent.ID);
            Assert.AreEqual(correctNumber, ((IEntity)item).Ingredients.Count);
        }

       internal void BaseDetailWithSuccessWhenIDisValidAndNotAllChildrenOnListExistWhenDetachASetOfIngredients()
        {
            IngredientType nullIngredient = null;

            MyIngredientsList.Add(nullIngredient);
            MyIngredientsList.RemoveAt(1);
            List<IEntity> selection = new List<IEntity>();  // TEMPORARY
            ActionResult ar = controller.DetachASetOf(parent, selection);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            // TODO: expand 
            IEntity returnedIngredient = (IEntity)ParentRepo.GetById(parent.ID);

            Assert.AreEqual(1, returnedIngredient.Ingredients.Count);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("All Ingredients Were Successfully Detached!", adr.Message);
        }
        
       internal void BaseDetailWithErrorWhenIDisValidAndNoChildrenOnListExistWhenDetachASetOfIngredients()
        {
            //IngredientType nullIngredient = null; 

            //MyIngredientsList.Clear();
            //MyIngredientsList.Add(nullIngredient);
            //ParentRepo.Update(parent, parent.ID);

            //ActionResult ar = controller.DetachASetOf(parent, parent.GetIngredientsSelection(MyIngredientsList));
            //AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            //CheckIngredientsCount(5);

            //Assert.AreEqual("alert-success", adr.AlertClass);
            //Assert.AreEqual(childName+" was Successfully Detached!", adr.Message);
        }
        

       internal void BaseDetailWithSuccessWhenIDisValidAndThereAreThreeChildrenOnListWhenDetachASetOfIngredients()
        {
            MyIngredientsList.Clear();
            MyIngredientsList.Add(new IngredientType() { ID = 1000 });
            MyIngredientsList.Add(new IngredientType() { ID = 1001 });
            MyIngredientsList.Add(new IngredientType() { ID = 1002 });
            parent.Ingredients.Clear();
            parent.Ingredients.AddRange(MyIngredientsList);
            ParentRepo.Save(parent);
            List<IEntity> MyIngredientsToRemoveList = new List<IEntity>();
            MyIngredientsToRemoveList.Add(new IngredientType() { ID = 1000 });
            ActionResult ar = controller.DetachASetOf(parent, MyIngredientsToRemoveList);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            IEntity returnedIngredient = (IEntity)ParentRepo.GetById(parent.ID);

            Assert.AreEqual(2, returnedIngredient.Ingredients.Count);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("All Ingredients Were Successfully Detached!", adr.Message);
        }
          

       internal void BaseDetailWithErrorWhenParentIDIsValidAndChildIsValidWhenDetachingUnattachableChild()
        {
            ActionResult ar = controller.Detach(Parent , (TChild)Child);

            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            Assert.IsNotNull(ar);
            Assert.AreEqual(Parent.ID, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual("Element Could not Be Attached - So It Could Not Be Detached!", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }


         

        internal void BaseEditWithWarningWhenParentIDIsValidAndChildIsNotValidWhenAttaching()
        {
            TParent parent = new TParent() { ID = 7001 };
            ParentRepo.Save(parent);
            IngredientType ingredient = null;
            ActionResult ar = controller.Detach(parent, ingredient);

            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            Assert.IsNotNull(ar);
            Assert.AreEqual(parent.ID, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit, rtrr.RouteValues.ElementAt(1).Value);
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value);
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual(parentName + " was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }
        internal void BaseEditWithWarningWhenParentIDIsValidAndChildIsNotValidWhenDetaching()
        {

        }


        
        private static void SuccessfullyDetachASetOfMenuChildren()
        {
            //Plan.Menus.Add(new Menu { ID = 4005, Name = "Butter" });
            //Plan.Menus.Add(new Menu { ID = 4006, Name = "Cayenne Pepper" });
            //Plan.Menus.Add(new Menu { ID = 4007, Name = "Cheese" });
            //Plan.Menus.Add(new Menu { ID = 4008, Name = "Chopped Green Pepper" });
            //repo.Save((Plan)Plan);
            //int initialMenuCount = Plan.Menus.Count();

            //var setToSelect = new HashSet<int> { 4006, 4008 };
            //List<Plan> selected = Plan.Menus.Where(t => setToSelect.Contains(t.ID)).ToList();
            //controller.DetachASetOf(Plan, selected);
            //Plan returnedPlan = repo.GetById(Plan.ID);

            //  Assert.AreEqual(initialMenuCount - 2, returnedPlan.Menus.Count());
        }

         
        private static void ReturnsDetailWithWarningIfAttachingNullMenuChild() { }
         
        private static void ReturnsDetailWithWarningWithUnknownMenuChildID() { }
 
        private static void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild() { }
    } 
}
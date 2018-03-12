using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.BusinessObjects;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.BasicTests;
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

        private static TParent item;
        private string parentName = typeof(TParent).ToString().Split('.').Last();
        private static string childName = typeof(TChild).ToString().Split('.').Last();

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

        internal BaseControllerShouldDetachXAndReturn()
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
        internal static void TestRunner()
        {
            DetailWithErrorWhenParentIDIsValidAndChildIsValidWhenDetachingUnattachableChild();
            DetailWithSuccessWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndSelectionSetIsSupplied(); 
            DetailWithSuccessWhenIDisValidAndThereAreThreeChildrenOnListWhenDetachingAll();
            DetailWithSuccessWhenIDisValidAndAlChildrenOnListExistWhendDetachASetOfIngredients();
            DetailWithWarningWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndSelectionSetIsNotSupplied();
            EditWithWarningWhenParentIDIsValidAndChildIsNotValidWhenAttaching();
            SuccessfullyDetachASetOfMenuChildren();
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

        private static void DetailWithWarningWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndSelectionSetIsNotSupplied()
        {
            SetUpForTests(out ParentRepo, out controller, out item);
            item.Ingredients.Add(Ingredient1);
            ParentRepo.Update(item, item.ID);

            ActionResult ar = controller.DetachASetOf(item, null);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            TParent returnedParent = ParentRepo.GetById(item.ID);

            Assert.IsNotNull(ar);
            Assert.AreEqual(item.ID, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            childName = typeof(TChild).ToString().Split('.').Last();
            Assert.AreEqual("Nothing was Selected!", adr.Message);
        }//n(UIViewType.Details.ToString(), new { id, actionMethod = UIViewType.Edit }).WithWarning("Nothing was Selected!");

        private static void DetailWithSuccessWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndSelectionSetIsSupplied()
        {
            SetUpForTests(out ParentRepo, out controller, out item);
            IngredientType child = new IngredientType() { ID = 7654321 };
            item.Ingredients.Add(child);
            ParentRepo.Update(item, item.ID);

            List<IEntity> list = new List<IEntity>() { child };


            ActionResult ar = controller.DetachASetOf(item, list);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            Assert.IsNotNull(ar);
            Assert.AreEqual(item.ID, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual("Ingredient was Successfully Detached!", adr.Message);
        }

        private static void DetailWithSuccessWhenIDisValidAndThereAreThreeChildrenOnListWhenDetachingAll()
        {
            TParent parent = new TParent() { ID = 7000 };
            TChild child = new TChild();
            if (parent.CanHaveChild(child))
            {
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
        }
         

        private static void DetailWithSuccessWhenIDisValidAndAlChildrenOnListExistWhendDetachASetOfIngredients()
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
            ActionResult ar = controller.DetachASetOf(tParent, itemsToBeRemoved);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            TParent returnedt = ParentRepo.GetById(tParent.ID);
            int returnedIngredientCount = returnedt.Ingredients.Count();

            Assert.AreEqual(initialIngredientCount - 2, returnedIngredientCount);

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
            Assert.Fail();  // TODO: write test
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


        private static void DetailWithErrorWhenParentIDIsValidAndChildIsValidWhenDetachingUnattachableChild()
        {
            SetUpForTests(out ParentRepo, out controller, out item);

            ActionResult ar = controller.Detach(item, null);

            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            Assert.IsNotNull(ar);
            Assert.AreEqual(item.ID, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual("Child was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }



        private static void EditWithWarningWhenParentIDIsValidAndChildIsNotValidWhenAttaching()
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
            Assert.AreEqual("Child was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }

        private static void BaseEditWithWarningWhenParentIDIsValidAndChildIsNotValidWhenDetaching()
        {

        }



        private static void SuccessfullyDetachASetOfMenuChildren()
        {  // TODO: move the if upwards
            TParent parent = new TParent();
            MenuType menu = new MenuType();
            if (parent.CanHaveChild(menu))
            { 
            SetUpForTests(out ParentRepo, out controller, out item);
            item.Menus.Add(new MenuType { ID = 4005, Name = "Butter" });
            item.Menus.Add(new MenuType { ID = 4006, Name = "Cayenne Pepper" });
            item.Menus.Add(new MenuType { ID = 4007, Name = "Cheese" });
            item.Menus.Add(new MenuType { ID = 4008, Name = "Chopped Green Pepper" });
            ParentRepo.Save(item);
            int initialMenuCount = item.Menus.Count();

            var setToSelect = new HashSet<int> { 4006, 4008 };
            List<MenuType> list = item.Menus.Where(t => setToSelect.Contains(t.ID)).ToList();
            List<IEntity> selected = new List<IEntity>();
            selected.AddRange(list);
            controller.DetachASetOf(item, selected);
            TParent returnedItem = ParentRepo.GetById(item.ID);

            Assert.AreEqual(initialMenuCount - 2, returnedItem.Menus.Count());
            }
        }


        private static void ReturnsDetailWithWarningIfAttachingNullMenuChild() { }

        private static void ReturnsDetailWithWarningWithUnknownMenuChildID() { }

        private static void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild() { }
    }
}
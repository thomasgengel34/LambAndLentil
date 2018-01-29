using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.BusinessObjects;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
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

        public List<IEntity> MyIngredientsList { get; set; }
        public List<RecipeType> MyRecipesList { get; set; }

        public IngredientType Ingredient1 { get; set; }
        public IngredientType Ingredient2 { get; set; }
        public IngredientType Ingredient3 { get; set; }
        public IngredientType Ingredient4 { get; set; }
        public IngredientType Ingredient5 { get; set; }
        public IEntity parentWithIngredients { get; set; }
        public IEntity parentWithRecipes { get; set; }

        public RecipeType Recipe1 { get; set; }
        public RecipeType Recipe2 { get; set; }
        public RecipeType Recipe3 { get; set; }
        public RecipeType Recipe4 { get; set; }
        public RecipeType Recipe5 { get; set; }
        private List<IEntity> MyChildrenList { get; set; }

        public BaseControllerShouldDetachXAndReturn()
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
                    parentWithIngredients.Ingredients = new List<IEntity>();
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


            MyIngredientsList = new List<IEntity>()
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

            ActionResult ar = Controller.DetachASetOf(parent, null);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            TParent returnedParent = ParentRepo.GetById(parentWithIngredients.ID);


            // Assert
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


            ActionResult ar = Controller.DetachASetOf(parent, list);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
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
            Type type = typeof(TChild);
            ar = Controller.DetachAll(parent, type);

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
 

       internal void BaseDetailWithDangerWhenIDisValidAndThereIsOneChildOnListWhenDetachingAndChildCannotBeAttachedWhenDetachingAll()
        {
            ActionResult ar = Controller.DetachASetOf(parent, MyChildrenList);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            Assert.IsNotNull(ar);
            Assert.AreEqual(parent.ID, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit, rtrr.RouteValues.ElementAt(1).Value);
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value);
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual("Element Could not Be Attached - So It Could Not Be Detached", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }
         


       internal void BaseDetailWithSuccessWhenIDisValidAndAlChildrenOnListExistWhendDetachASetOfIngredients()
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

            List<IEntity> itemsToBeRemoved = new List<IEntity>()
            {
               ingredient1,
               ingredient3
            };


            ActionResult ar = Controller.DetachASetOf(tParent , itemsToBeRemoved);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            TParent returnedt = ParentRepo.GetById(tParent.ID);

            Assert.AreEqual(itemsToBeRemoved.Count()+1, returnedt.Ingredients.Count());

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

            ActionResult ar = Controller.DetachASetOf(parent, MyIngredientsList);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            // TODO: expand 
            IEntity returnedIngredient = (IEntity)ParentRepo.GetById(parent.ID);

            Assert.AreEqual(1, returnedIngredient.Ingredients.Count);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("All Ingredients Were Successfully Detached!", adr.Message);
        }

       internal void BaseDetailWithErrorWhenIDisValidAndNoChildrenOnListExistWhenDetachASetOfIngredients()
        {
            IngredientType nullIngredient = null; 

            MyIngredientsList.Clear();
            MyIngredientsList.Add(nullIngredient);
            ParentRepo.Update(parent, parent.ID);

            ActionResult ar = Controller.DetachASetOf(parent, MyIngredientsList);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            CheckIngredientsCount(5);

            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(childName+" was Successfully Detached!", adr.Message);
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
            ActionResult ar = Controller.DetachASetOf(parent, MyIngredientsToRemoveList);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            IEntity returnedIngredient = (IEntity)ParentRepo.GetById(parent.ID);

            Assert.AreEqual(2, returnedIngredient.Ingredients.Count);
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("All Ingredients Were Successfully Detached!", adr.Message);
        }
          

       internal void BaseDetailWithErrorWhenParentIDIsValidAndChildIsValidWhenDetachingUnattachableChild()
        {
            ActionResult ar = Controller.Detach(Parent , (TChild)Child);

            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            Assert.IsNotNull(ar);
            Assert.AreEqual(Parent.ID, rtrr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual(UIViewType.Edit.ToString(), rtrr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), rtrr.RouteValues.ElementAt(2).Value.ToString());
            Assert.AreEqual(3, rtrr.RouteValues.Count);
            Assert.AreEqual("Element Could not Be Attached - So It Could Not Be Detached", adr.Message);
        }

         

        internal void BaseEditWithWarningWhenParentIDIsValidAndChildIsNotValidWhenAttaching()
        {
            TParent parent = new TParent() { ID = 7001 };
            ParentRepo.Save(parent);
            IngredientType ingredient = null;
            ActionResult ar = Controller.Detach(parent, ingredient);

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


    }
}
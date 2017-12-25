﻿using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.UI.Controllers;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using LambAndLentil.UI.Models;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestCategory("RecipesController")]
    [TestClass]
    public class RecipesController_Attach_Should : RecipesController_Test_Should
    {

        [TestMethod]
        public void ReturnsIndexWithWarningWithUnknownParentID() => BaseReturnsIndexWithWarningWithUnknownParentID(Repo, Controller);

        [TestMethod]
        public void ReturnsIndexWithWarningWithNullParent() => BaseReturnsIndexWithWarningWithNullParent(Repo, Controller);

        
        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownChildID() => BaseReturnsDetailWithWarningWithUnknownChildID(Recipe, Repo, Controller);

        [TestMethod]
        public void ReturnsDetailWithWarningIfAttachingNullChild() => BaseReturnsDetailWithWarningIfAttachingNullChild(Recipe, Repo, Controller);

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild()
        {
            // Arrange
            Recipe menu = new Recipe
            {
                ID = int.MaxValue,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };
            IRepository<Recipe> mRepo = new TestRepository<Recipe>();
            mRepo.Add(menu);
            Ingredient ingredient = new Ingredient
            {
                ID = 1492,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };

            // Act
            ActionResult ar = Controller.Attach(Repo,int.MaxValue, ingredient, AttachOrDetach.Attach);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            //Assert
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("Ingredient was Successfully Attached!", adr.Message);
            Assert.AreEqual(int.MaxValue, rdr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual("Edit", rdr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual("Details", rdr.RouteValues.ElementAt(2).Value.ToString());
        }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidChild() =>
            // Arrange

            // Act

            // Assert
            Assert.Fail();


        [TestMethod]
        public void SuccessfullyAttachChild()
        {
            // Arrange
            Ingredient child = new Ingredient() { ID = 3000, Name = "SuccessfullyAttachChild" };
            TestRepository<Ingredient> IngredientRepo = new TestRepository<Ingredient>();
            IngredientRepo.Save(child);

            // Act
            Controller.Attach(Repo,Recipe.ID, child,AttachOrDetach.Attach);
            ReturnedRecipe = Repo.GetById(Recipe.ID);
            // Assert
            //  Assert.AreEqual("Default", Ingredient.Ingredients.Last().Name);
            Assert.AreEqual("SuccessfullyAttachChild", ReturnedRecipe.Ingredients.Last().Name);
        }

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void SuccessfullyDetachFirstIngredientChild()
        //{
        //    IGenericController<Recipe> DetachController = new RecipesController(Repo);
        //    BaseSuccessfullyDetachChild(Repo, Controller, DetachController, UIControllerType.ShoppingLists, 0);
        //}

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachASetOfIngredientChildren()
        { // RemoveAll
          // Arrange

            Recipe.Ingredients.Add(new Ingredient { ID = 4005, Name = "Butter" });
            Recipe.Ingredients.Add(new Ingredient { ID = 4006, Name = "Cayenne Pepper" });
            Recipe.Ingredients.Add(new Ingredient { ID = 4007, Name = "Cheese" });
            Recipe.Ingredients.Add(new Ingredient { ID = 4008, Name = "Chopped Green Pepper" });
            Repo.Save(Recipe);
            int initialIngredientCount = Recipe.Ingredients.Count();

            // Act
            var setToSelect = new HashSet<int> {  4006, 4008 };

            List<Ingredient> selected = Recipe.Ingredients.Where(t => setToSelect.Contains(t.ID)).ToList();

            Controller.DetachASetOf(Recipe.ID, selected);
            Recipe returnedRecipe = Repo.GetById(Recipe.ID);
            // Assert
            Assert.AreEqual(initialIngredientCount - 2, returnedRecipe.Ingredients.Count());
        }


        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachASetOfIngredientChildrenSimplyIgnoresANonExistentIngredientIfItIsInTheSet() => BaseDetachASetOfIngredientChildrenSimplyIgnoresANonExistentIngredientIfItIsInTheSet<Recipe>(Repo, Controller);


        [TestMethod]
        public void DetachTheLastIngredientChild() => BaseDetachTheLastIngredientChild(Repo, Controller, Recipe);

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachAllIngredientChildren() => BaseDetachAllIngredientChildren(Repo, Controller, Recipe);

        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild() => BaseReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild(Repo, Controller, Recipe.ID);
    }
}

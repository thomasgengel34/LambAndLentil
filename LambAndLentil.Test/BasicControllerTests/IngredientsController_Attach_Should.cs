using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Models;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using System.Linq;
using LambAndLentil.UI;
using System.Collections.Generic;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    public class IngredientsController_Attach_Should : IngredientsController_Test_Should
    {
        [TestMethod] 
        public void SuccessfullyAttachIngredientChild()
        {
            // Arrange
            Ingredient child = new Ingredient() { ID = 3000, Name = "SuccessfullyAttachIngredientChild" };
            Repo.Save(child);

            // Act
            Controller.AttachIngredient(Ingredient.ID, child);
            ReturnedIngredient = Repo.GetById(Ingredient.ID);
            // Assert
            //  Assert.AreEqual("Default", Ingredient.Ingredients.Last().Name);
            Assert.AreEqual("SuccessfullyAttachIngredientChild", ReturnedIngredient.Ingredients.Last().Name);
        }

        [TestMethod] 
        public void SuccessfullyDetachFirstIngredientChild()
        {
            IGenericController<Ingredient> DetachController = new IngredientsController(Repo);
            BaseSuccessfullyDetachIngredientChild(Repo, Controller, DetachController, UIControllerType.ShoppingLists, 0);
        }


        [TestMethod] 
        public void DetachASetOfIngredientChildren()
        {
            // Arrange 
            Ingredient.Ingredients.Add(new Ingredient { ID = 4005, Name = "Butter" });
            Ingredient.Ingredients.Add(new Ingredient { ID = 4006, Name = "Cayenne Pepper" });
            Ingredient.Ingredients.Add(new Ingredient { ID = 4007, Name = "Cheese" });
            Ingredient.Ingredients.Add(new Ingredient { ID = 4008, Name = "Chopped Green Pepper" });
            Repo.Save((Ingredient)Ingredient);
            int initialIngredientCount = Ingredient.Ingredients.Count();

            // Act
            var setToSelect = new HashSet<int> { 4006, 4008 };
            List<Ingredient> selected = Ingredient.Ingredients.Where(t => setToSelect.Contains(t.ID)).ToList();
            Controller.DetachAllIngredients(Ingredient.ID, selected);
            Ingredient returnedIngredient = Repo.GetById(Ingredient.ID);

            // Assert
            Assert.AreEqual(initialIngredientCount - 2, returnedIngredient.Ingredients.Count());
        }





        [TestMethod]
        public void DetachTheLastIngredientChild() => BaseDetachTheLastIngredientChild(Repo, Controller, (Ingredient)Ingredient);

        [TestMethod] 
        public void DetachAllIngredientChildren() => BaseDetachAllIngredientChildren(Repo, Controller, (Ingredient)Ingredient);

        [TestMethod]
        public void ReturnsIndexWithWarningWithUnknownParentID() =>
            BaseReturnsIndexWithWarningWithUnknownParentID(Repo, Controller);

        [TestMethod]
        public void ReturnsIndexWithWarningWithNullParent() => BaseReturnsIndexWithWarningWithNullParent(Repo, Controller);

        [TestMethod]
        public void ReturnsDetailWithWarningIfAttachingNullChild() => BaseReturnsDetailWithWarningIfAttachingNullChild(Ingredient, Repo, Controller);

        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownChildID() => BaseReturnsDetailWithWarningWithUnknownChildID(Ingredient, Repo, Controller);

        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild() => BaseReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild(Repo, Controller,Ingredient.ID); 
    }
}

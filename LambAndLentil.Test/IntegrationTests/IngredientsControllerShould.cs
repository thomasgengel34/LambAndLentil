using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.BaseControllerTests;
using LambAndLentil.UI;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.Infrastructure
{
    [TestClass]
    // also using WebApi methods - TODO: something will be needed for additional ingredients, such as user entered
    [TestCategory("Integration")]
    [TestCategory("IngredientsController")]
   public class IngredientsControllerShould : IngredientsController_Test_Should
    { 
         
        [TestMethod]
        [TestCategory("Save")]
        public void SaveAValidIngredientAndReturnIndexView()
        { 
            Ingredient vm = new Ingredient
            {
                ID = 3000,
                Name = "SaveAValidIngredientAndReturnIndexView"
            };
             
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit(vm);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;
             
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());

        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedIngredient()
        {
            Ingredient ingredient = new Ingredient
            {
                Name = "0000 test",
                ID = 5000,
                Description = "test IngredientsControllerShould.SaveEditedIngredient"
            };
            repo.Save(ingredient);

            ingredient.Name = "0000 test Edited";
            ActionResult ar = controller.PostEdit(ingredient);
            Ingredient returnedIngredient = repo.GetById(ingredient.ID);

            Assert.AreEqual("0000 test Edited", returnedIngredient.Name);
        }


     

         
         

        [TestMethod]
        public void NotCreateASecondElementOnEditingOneElement()
        { 
            int initialCount = repo.Count();
             
            Ingredient.Name = "Changed";
            controller.Edit(Ingredient.ID);
             
            Assert.AreEqual(initialCount, repo.Count());
        }


        [TestMethod]
        public void NotCreateASecondElementOnPostEditingOneElement()
        { 
            int initialCount = repo.Count();
             
            Ingredient.Name = "Changed";
            controller.PostEdit((Ingredient)Ingredient);
             
            Assert.AreEqual(initialCount, repo.Count());
        }

      
       


        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveNameBoundInPostEditActionMethod()
        {
            Ingredient ingredient = new Ingredient() { ID = 4000, Name = "HaveNameBoundInPostEditActionMethod", IngredientsList = "" };
            ingredient.Name = "Changed";

            controller.PostEdit(ingredient);
            Ingredient returnedIngredient = repo.GetById(ingredient.ID);

            Assert.AreEqual("Changed", returnedIngredient.Name);
        }


        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveDescriptionBoundInPostEditActionMethod()
        {
             Ingredient ingredient = new Ingredient { ID = 123456789, Description = "Changed" };

            controller.PostEdit((Ingredient)ingredient);
            Ingredient returnedIngredient = repo.GetById(ingredient.ID);

            Assert.AreEqual(ingredient.Description, returnedIngredient.Description);
        }



        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveIngredientsBoundInPostEditActionMethod()
        { 
             Ingredient ingredient = new Ingredient() { ID = 2000 };
            ingredient.Ingredients = new List<Ingredient> {
                new Ingredient { Name = "Changed" },
                new Ingredient { Name = "Changed 2" },
                new Ingredient { Name = "Changed Up" }
            };
             
            ActionResult ar = controller.PostEdit( ingredient);
            Ingredient returnedIngredient = repo.GetById(ingredient.ID); 
            
            Assert.AreEqual("Changed", returnedIngredient.Ingredients.First().Name);
            Assert.AreEqual("Changed Up", returnedIngredient.Ingredients.Last().Name);
        }


        [TestMethod]
        public void HaveIngredientsListBoundInPostEditActionMethod()
        {
            Ingredient ingredient = new Ingredient() { ID = 4000, Name = "HaveIngredientsListBoundInPostEditActionMethod", IngredientsList = "" };
            ingredient.IngredientsList = "Changed";

            controller.PostEdit(ingredient);
            Ingredient returnedIngredient = repo.GetById(ingredient.ID);

            Assert.AreEqual("Changed", returnedIngredient.IngredientsList);
        }
    }
}

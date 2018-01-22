using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.BasicControllerTests;
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
        public IngredientsControllerShould()
        {
        }

        [TestMethod]
        [TestCategory("Create")]
        public void CreateAnIngredient()
        { 
            ViewResult vr = (ViewResult)Controller.Create(UIViewType.Create);
            string modelName = ((Ingredient)vr.Model).Name;
             
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created");
        }

        [TestMethod]
        [TestCategory("Save")]
        public void SaveAValidIngredientAndReturnIndexView()
        { 
            Ingredient vm = new Ingredient
            {
                ID = 3000,
                Name = "SaveAValidIngredientAndReturnIndexView"
            };
             
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.PostEdit(vm);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;
             
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(0).ToString());

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
            Repo.Save(ingredient);

            ingredient.Name = "0000 test Edited";
            ActionResult ar = Controller.PostEdit(ingredient);
            Ingredient returnedIngredient = Repo.GetById(ingredient.ID);

            Assert.AreEqual("0000 test Edited", returnedIngredient.Name);
        }


        [TestMethod]
        [TestCategory("Edit")]
        public void ShouldSaveTheCreationDateOnIngredientCreationWithDateTimeParameter()
        { 
            DateTime CreationDate = new DateTime(2010, 1, 1);
             
            Ingredient ingredient = new Ingredient(CreationDate);
             
            Assert.AreEqual(CreationDate, ingredient.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnIngredientCreationWithNoParameterCtor()
        { 
            DateTime CreationDate = DateTime.Now;
             
            Ingredient ingredient = new Ingredient();
             
            Assert.AreEqual(CreationDate.Date, ingredient.CreationDate.Date);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnIngredientCreationWithDateTimeParameter()
        { 
            DateTime CreationDate = new DateTime(2010, 1, 1);
 
            Ingredient ingredient = new Ingredient(CreationDate);
             
            Assert.AreEqual(CreationDate, ingredient.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            DateTime CreationDate = new DateTime(2010, 1, 1);
            Ingredient ingredient = new Ingredient(CreationDate)
            {
                ID = int.MaxValue - 200,
                Name = "test IngredientsControllerShould.SaveTheCreationDateBetweenPostedEdits"
            };
            Repo.Save(ingredient);
            Controller.PostEdit(ingredient);
            Ingredient returnedIngredient = Repo.GetById(ingredient.ID);
            DateTime shouldBeSameDate = returnedIngredient.CreationDate;

            Assert.AreEqual(CreationDate, shouldBeSameDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            Ingredient ingredient = new Domain.Entities.Ingredient()
            {
                ID = 6000,
                Name = "Test UpdateTheModificationDateBetweenPostedEdits"
            };
            Repo.Save(ingredient);
            BaseUpdateTheModificationDateBetweenPostedEdits(ingredient);
        }


        [TestMethod]
        public void NotCreateASecondElementOnEditingOneElement()
        {
            // Arrange
            int initialCount = Repo.Count();
             
            Ingredient.Name = "Changed";
            Controller.Edit(Ingredient.ID);
             
            Assert.AreEqual(initialCount, Repo.Count());
        }


        [TestMethod]
        public void NotCreateASecondElementOnPostEditingOneElement()
        { 
            int initialCount = Repo.Count();
             
            Ingredient.Name = "Changed";
            Controller.PostEdit((Ingredient)Ingredient);
             
            Assert.AreEqual(initialCount, Repo.Count());
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void ReturnIndexWithWarningForNonexistentIngredient()
        { 
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Edit(1000);
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;
             
            Assert.AreEqual(UIViewType.Index.ToString(), rdr.RouteValues.ElementAt(0).Value.ToString());
            Assert.AreEqual("Ingredient was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }


        [TestMethod]
        public void NotChangeIDInPostEditActionMethod()
        { 
            int originalID = Ingredient.ID;
            Ingredient.ID = 7000; 
            
            Controller.PostEdit((Ingredient)Ingredient);
            ReturnedIngredient = Repo.GetById(7000);
            Ingredient OriginalIngredient = Repo.GetById(originalID); 

            Assert.AreEqual(originalID, OriginalIngredient.ID);
            Assert.AreEqual(7000, ReturnedIngredient.ID);
        }


        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveNameBoundInPostEditActionMethod()
        {
            Ingredient ingredient = new Ingredient() { ID = 4000, Name = "HaveNameBoundInPostEditActionMethod", IngredientsList = "" };
            ingredient.Name = "Changed";

            Controller.PostEdit(ingredient);
            Ingredient returnedIngredient = Repo.GetById(ingredient.ID);

            Assert.AreEqual("Changed", returnedIngredient.Name);
        }


        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveDescriptionBoundInPostEditActionMethod()
        {
            IIngredient ingredient = new Ingredient { ID = 123456789, Description = "Changed" };

            Controller.PostEdit((Ingredient)ingredient);
            Ingredient returnedIngredient = Repo.GetById(ingredient.ID);

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
             
            ActionResult ar = Controller.PostEdit( ingredient);
            Ingredient returnedIngredient = Repo.GetById(ingredient.ID); 
            
            Assert.AreEqual("Changed", returnedIngredient.Ingredients.First().Name);
            Assert.AreEqual("Changed Up", returnedIngredient.Ingredients.Last().Name);
        }


        [TestMethod]
        public void HaveIngredientsListBoundInPostEditActionMethod()
        {
            Ingredient ingredient = new Ingredient() { ID = 4000, Name = "HaveIngredientsListBoundInPostEditActionMethod", IngredientsList = "" };
            ingredient.IngredientsList = "Changed";

            Controller.PostEdit(ingredient);
            Ingredient returnedIngredient = Repo.GetById(ingredient.ID);

            Assert.AreEqual("Changed", returnedIngredient.IngredientsList);
        }
    }
}

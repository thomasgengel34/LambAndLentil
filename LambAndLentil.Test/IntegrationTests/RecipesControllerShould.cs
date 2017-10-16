using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Tests.Controllers;
using LambAndLentil.Test.BasicControllerTests;

namespace IntegrationTests
{

    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("RecipesController")]
    public class RecipesControllerShould: RecipesController_Test_Should
    {  

        public RecipesControllerShould()
        { 
            Recipe = new Recipe();
        }


        [TestMethod]
        public void CreateARecipe()
        {
            // Arrange 

            // Act
            ViewResult vr = Controller.Create(UIViewType.Create);
            Recipe vm = (Recipe)vr.Model;
            string modelName = vm.Name;

            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created");
        }

        // [Ignore]
        [TestMethod]
        public void SaveAValidRecipe()
        {
            // Arrange

            Recipe vm = new Recipe
            {
                Name = "test SaveAValidRecipe",
                ID = int.MaxValue / 2
            };
            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.PostEdit(vm);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;


            // Assert 

            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("test SaveAValidRecipe has been saved or modified", adr.Message);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(0).ToString());
        }

        // [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedRecipeWithNameChange()
        {
            // Arrange 
            RecipesController Controller1 = new RecipesController(Repo);
            RecipesController Controller2 = new RecipesController(Repo);
            RecipesController Controller3 = new RecipesController(Repo);
            RecipesController Controller4 = new RecipesController(Repo);
            RecipesController Controller5 = new RecipesController(Repo);
            Recipe vm = new Recipe
            {
                Name = "0000 test",
                ID = 1000
            };

            // Act 
            ActionResult ar1 = Controller1.PostEdit(vm);
            ViewResult view1 = Controller2.Index();
            List<Recipe> ListEntity= (List<Recipe>)((( ListEntity<Recipe>)view1.Model).ListT);
            Recipe Recipe = (from m in ListEntity
                               where m.Name == "0000 test"
                               select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("0000 test", Recipe.Name);

            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = Recipe.ID;
            ActionResult ar2 = Controller3.PostEdit(vm);
            ViewResult view2 = Controller4.Index();
            List<Recipe> ListEntity2 = (List<Recipe>)((( ListEntity<Recipe>)view2.Model).ListT);
            Recipe Recipe2 = (from m in ListEntity2
                              where m.Name == "0000 test Edited"
                              select m).AsQueryable().First();

            // Assert
            Assert.AreEqual("0000 test Edited", Recipe2.Name);
        }



        [TestMethod]
        [TestCategory("DeleteConfirmed")]
        public void ActuallyDeleteARecipeFromTheDatabase()
        {
            // Arrange

           Recipe.Description = "test ActuallyDeleteARecipeFromTheDatabase";
            //Act
            Controller.DeleteConfirmed(Recipe.ID);
            var deletedItem = (from m in Repo.GetAll()
                               where m.Description == Recipe.Description
                               select m).AsQueryable();

            //Assert
            Assert.AreEqual(0, deletedItem.Count());
        }


        // [Ignore]
        [TestMethod]
        public void SaveAllPropertiesInBaseEntity()
        {
            // Arrange 
            RecipesController ControllerPost = new RecipesController(Repo);
            RecipesController ControllerView = new RecipesController(Repo);
            RecipesController ControllerDelete = new RecipesController(Repo);
            Recipe vm = new Recipe
            {
                Name = "___test387",
                Description = "test387 description",
                ID = 774
            };

            // Act
            ControllerPost.PostEdit(vm);
            ViewResult view1 = ControllerView.Index();
            List<Recipe> ListEntity= (List<Recipe>)((( ListEntity<Recipe>)view1.Model).ListT);
            Recipe Recipe = (from m in ListEntity
                               where m.Name == "___test387"
                               select m).AsQueryable().FirstOrDefault();

            Assert.AreEqual(vm.Name, Recipe.Name);
            Assert.AreEqual(vm.Description, Recipe.Description);
            Assert.AreEqual(vm.CreationDate.Day, Recipe.CreationDate.Day);
            Assert.AreEqual(vm.ModifiedDate.Day, Recipe.ModifiedDate.Day);
            Assert.AreEqual(vm.AddedByUser, Recipe.AddedByUser);
            Assert.AreEqual(vm.ModifiedByUser, Recipe.ModifiedByUser);
            // return View(UIViewType.Details.ToString(), vm).WithWarning("Something is wrong with the data!");
        }

        // [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void AttachAnExistingIngredientToAnExistingRecipe()
        {
            // Arrange 
            TestRepository<Recipe> repoRecipe = new TestRepository<Recipe>();
            TestRepository<Ingredient> repoIngredient = new TestRepository<Ingredient>();
            RecipesController Controller = new RecipesController(repoRecipe);

            Recipe Recipe = new Recipe { ID = 3000, Description = "test AttachAnExistingIngredientToAnExistingRecipe" };
            Repo.Add(Recipe);
            Ingredient ingredient = new Ingredient { ID = 3300, Description = "test AttachAnExistingIngredientToAnExistingRecipe" };
            repoIngredient.Add(ingredient);


            // Act
            Controller.AttachIngredient(Recipe.ID, ingredient);
            Recipe returnedRecipe = (from m in repoRecipe.GetAll()
                                     where m.Description == Recipe.Description
                                     select m).FirstOrDefault();
            // Assert 
            Assert.AreEqual(1, returnedRecipe.Ingredients.Count());
            // how do I know the correct ingredient was added?
            Assert.AreEqual(ingredient.ID, returnedRecipe.Ingredients.First().ID);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void NotDeleteAnIngredientAfterIngredientIsDetachedFromRecipe()
        {
            // Arrange
            TestRepository<Recipe> repoRecipe = new TestRepository<Recipe>();
            TestRepository<Ingredient> repoIngredient = new TestRepository<Ingredient>();
            RecipesController Controller = new RecipesController(repoRecipe);
            RecipesController ControllerSubtract = new RecipesController(repoRecipe);

            Recipe Recipe1 = GetRecipe(repoRecipe, "test NotDeleteAnIngredientAfterIngredientIsDetached");


            Ingredient ingredient2 = GetIngredient(repoIngredient, "test NotDeleteAnIngredientAfterIngredientIsDetached");
            Controller.AttachIngredient(Recipe1.ID, ingredient2);
            // Act
            ControllerSubtract.RemoveIngredient(Recipe1.ID, ingredient2);


            // Assert
            Assert.IsNotNull(ingredient2);
        }

        // [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingExistIngredientToNonExistingRecipe()
        {
            // Arrange
            TestRepository<Recipe> repoRecipe = new TestRepository<Recipe>();
            TestRepository<Ingredient> repoIngredient = new TestRepository<Ingredient>();
            RecipesController ControllerAttach = new RecipesController(repoRecipe);
            RecipesController ControllerSubtract = new RecipesController(repoRecipe);

            string description = "test ReturnIndexViewWhenAttachingExistIngredientToNonExistingRecipe";

            Ingredient ingredient = GetIngredient(repoIngredient, description);
            repoIngredient.Add(ingredient);

            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)ControllerAttach.AttachIngredient(-1, ingredient);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert

            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Recipe was not found", adr.Message);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        }

        // [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnRecipeEditViewWithWarningMessageWhenAttachingNonExistingIngredientToExistingRRecipe()
        {
            // Arrange

            Recipe Recipe = GetRecipe(Repo, "test ReturnRecipeEditViewWithErrorMessageWhenAttachingNonExistingIngredientToExistingRRecipe");
            Ingredient ingredient = null;
            // Act  
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.AttachIngredient(Recipe.ID, ingredient);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert 
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Recipe was not found", adr.Message);
            Assert.AreEqual(3, routeValues.Count);
            Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(2).ToString());
            Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(1).ToString());
        }

        // [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingNonExistIngredientToNonExistingRecipe()
        {
            // Arrange
            TestRepository<Recipe> repoRecipe = new TestRepository<Recipe>();
            RecipesController Controller = new RecipesController(repoRecipe);
            Ingredient vm = null;

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.AttachIngredient(-1, vm);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Recipe was not found", adr.Message);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnRecipeEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingRecipe()
        {
            // Arrange

            TestRepository<Ingredient> repoIngredient = new TestRepository<Ingredient>();

            RecipesController ControllerAttachIngredient = new RecipesController(Repo);
            RecipesController ControllerRemoveIngredient = new RecipesController(Repo);

            Recipe Recipe = GetRecipe(Repo, "test ReturnRecipeEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingRecipe");

            Ingredient ingredient = GetIngredient(repoIngredient, "test ReturnRecipeEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingRecipe");
            ControllerAttachIngredient.AttachIngredient(Recipe.ID, ingredient);

            // Act          
            AlertDecoratorResult adr = (AlertDecoratorResult)ControllerRemoveIngredient.RemoveIngredient(Recipe.ID, ingredient);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert 
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("Ingredient was Successfully Detached!", adr.Message);
            Assert.AreEqual(3, routeValues.Count);
            Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(1).ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(2).ToString());
        }

        // [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNonExistingRecipe()
        {
            // Arrange
            TestRepository<Recipe> repoRecipe = new TestRepository<Recipe>();
            TestRepository<Ingredient> repoIngredient = new TestRepository<Ingredient>();
            RecipesController ControllerDetachIngredient = new RecipesController(repoRecipe);
            string description = "test ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNoExistingRecipe";

            Ingredient ingredient = GetIngredient(repoIngredient, description);
            // Act  
            AlertDecoratorResult adr = (AlertDecoratorResult)ControllerDetachIngredient.RemoveIngredient(-1, ingredient);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert 
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Recipe was not found", adr.Message);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        }

        // [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnRecipeIndexViewWithWarningWhenDetachingExistingingredientNotAttachedToAnExistingRecipe()
        {
            // Arrange

            TestRepository<Ingredient> repoIngredient = new TestRepository<Ingredient>();
            IngredientsController ControllerIngredient = new IngredientsController(repoIngredient);
            RecipesController ControllerDetachIngredient = new RecipesController(Repo);
            string description = "test ReturnRecipeIndexViewWithWarningWhenDetachingExistingingredientNotAttachedToAnExistingRecipe";
            Ingredient ingredient = GetIngredient(repoIngredient, description);
            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)ControllerDetachIngredient.RemoveIngredient(-1, ingredient);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert 
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Recipe was not found", adr.Message);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        }

        // [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnRecipeEditViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToExistingRecipe()
        {
            // Arrange
            TestRepository<Recipe> repoRecipe = new TestRepository<Recipe>();
            RecipesController ControllerRecipe = new RecipesController(repoRecipe);
            RecipesController ControllerDetachIngredient = new RecipesController(repoRecipe);
            Recipe Recipe = GetRecipe(repoRecipe, "test ReturnRecipeEditViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToExistingRecipe");
            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)ControllerDetachIngredient.RemoveIngredient(Recipe.ID, null);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert 
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Recipe was not found", adr.Message);
            Assert.AreEqual(3, routeValues.Count);
            Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(2).ToString());
            Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(1).ToString());
        }

        // [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToANonExistingRecipe()
        {
            // Arrange 

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.RemoveIngredient(-1, null);

            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert

            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Recipe was not found", adr.Message);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());

        }

        internal Ingredient GetIngredient(IRepository<Ingredient> Repo, string description)
        {
            IngredientsController Controller = new IngredientsController(Repo);
            Ingredient ivm = new Ingredient
            {
                Description = description,
                ID = int.MaxValue
            };
            Controller.PostEdit(ivm);

            Ingredient ingredient = ((from m in Repo.GetAll()
                                      where m.Description == description
                                      select m).AsQueryable()).FirstOrDefault();
            return ingredient;
        }

        // phase out
        internal Recipe GetRecipe(IRepository<Recipe> Repo, string description)
        {
            RecipesController Controller = new RecipesController(Repo);
            Recipe vm = new Recipe
            {
                Description = description,
                ID = int.MaxValue
            };
            Controller.PostEdit(vm);

            Recipe Recipe = ((from m in Repo.GetAll()
                                where m.Description == description
                                select m).AsQueryable()).FirstOrDefault();
            return Recipe;
        }
         
    }
}

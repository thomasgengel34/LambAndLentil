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

        public RecipesControllerShould() =>   Recipe = new Recipe(); 

        [TestMethod]
        public void CreateARecipe()
        {
            // Arrange 

            // Act
            ViewResult vr = (ViewResult)Controller.Create(UIViewType.Create);
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

            IGenericController<Recipe> Controller2 = new RecipesController(Repo);
            IGenericController<Recipe> Controller3 = new RecipesController(Repo);
            IGenericController<Recipe> Controller4 = new RecipesController(Repo);
            
            Recipe vm = new Recipe
            {
                Name = "0000 test",
                ID = 1000
            };

            // Act 
            ActionResult ar1 = Controller.PostEdit(vm);
            ViewResult view1 = (ViewResult)Controller2.Index();
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
            ViewResult view2 = (ViewResult)Controller4.Index();
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
            IGenericController<Recipe> ControllerView = new RecipesController(Repo);
            IGenericController<Recipe> ControllerDelete = new RecipesController(Repo);
            IRecipe Recipe = new Recipe
            {
                Name = "___test387",
                Description = "test387 description",
                ID = 774
            };

            // Act
            Controller.PostEdit((Recipe)Recipe);
            ViewResult view1 = (ViewResult)ControllerView.Index();
            List<Recipe> ListEntity= (List<Recipe>)((( ListEntity<Recipe>)view1.Model).ListT);
            Recipe = (from m in ListEntity
                               where m.Name == "___test387"
                               select m).AsQueryable().FirstOrDefault();

            Assert.AreEqual(Recipe.Name, Recipe.Name);
            Assert.AreEqual(Recipe.Description, Recipe.Description);
            Assert.AreEqual(Recipe.CreationDate.Day, Recipe.CreationDate.Day);
            Assert.AreEqual(Recipe.ModifiedDate.Day, Recipe.ModifiedDate.Day);
            Assert.AreEqual(Recipe.AddedByUser, Recipe.AddedByUser);
            Assert.AreEqual(Recipe.ModifiedByUser, Recipe.ModifiedByUser);
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
          

            Recipe Recipe = new Recipe { ID = 3000, Description = "test AttachAnExistingIngredientToAnExistingRecipe" };
            Repo.Add(Recipe);
            Ingredient ingredient = new Ingredient { ID = 3300, Description = "test AttachAnExistingIngredientToAnExistingRecipe" };
            repoIngredient.Add(ingredient);


            // Act
            Controller.Attach(Repo,Recipe.ID, ingredient );
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
            
            IGenericController<Recipe> ControllerSubtract = new RecipesController(repoRecipe);

            Recipe Recipe1 = GetRecipe(repoRecipe, "test NotDeleteAnIngredientAfterIngredientIsDetached");


            Ingredient ingredient2 = GetIngredient(repoIngredient, "test NotDeleteAnIngredientAfterIngredientIsDetached");
            Controller.Attach(Repo,Recipe1.ID, ingredient2 );
            // Act
            ControllerSubtract.Detach(Repo,Recipe1.ID, ingredient2);


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

            string description = "test ReturnIndexViewWhenAttachingExistIngredientToNonExistingRecipe";

            Ingredient ingredient = GetIngredient(repoIngredient, description);
            repoIngredient.Add(ingredient);

            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Attach(Repo,-1, ingredient );
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
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Attach(Repo,Recipe.ID, ingredient );
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
            Ingredient ingredient = null;

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Attach(Repo,-1, ingredient );
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
            IGenericController<Recipe> ControllerRemoveIngredient = new RecipesController(Repo);

            Recipe Recipe = GetRecipe(Repo, "test ReturnRecipeEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingRecipe");

            Ingredient ingredient = GetIngredient(repoIngredient, "test ReturnRecipeEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingRecipe");
            Controller.Attach(Repo,Recipe.ID, ingredient );

            // Act          
            AlertDecoratorResult adr = (AlertDecoratorResult)ControllerRemoveIngredient.Detach(Repo,Recipe.ID, ingredient);
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
            IGenericController<Recipe> ControllerDetach = new RecipesController(repoRecipe);
            string description = "test ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNoExistingRecipe";

            Ingredient ingredient = GetIngredient(repoIngredient, description);
            // Act  
            AlertDecoratorResult adr = (AlertDecoratorResult)ControllerDetach.Detach(Repo,-1, ingredient);
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
            IGenericController<Ingredient> ControllerIngredient = new IngredientsController(repoIngredient);
            IGenericController<Recipe> ControllerDetach = new RecipesController(Repo);
            string description = "test ReturnRecipeIndexViewWithWarningWhenDetachingExistingingredientNotAttachedToAnExistingRecipe";
            Ingredient ingredient = GetIngredient(repoIngredient, description);
            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)ControllerDetach.Detach(Repo,-1, ingredient);
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
            IRepository<Recipe> repoRecipe = new TestRepository<Recipe>();
            IGenericController<Recipe> ControllerDetach = new RecipesController(repoRecipe);
            Recipe Recipe = GetRecipe(repoRecipe, "test ReturnRecipeEditViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToExistingRecipe");
            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Detach(Repo,Recipe.ID, (Ingredient)null);
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
        public void ReturnIndexViewWithWarningWhenDetachingNonExistingIngredientAttachedToANonExistingRecipe()
        {
            // Arrange 

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Detach(Repo,-1, (Ingredient)null);

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
            IGenericController<Ingredient> Controller = new IngredientsController(Repo);
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

        // phase out - use action result methods instead
        internal Recipe GetRecipe(IRepository<Recipe> Repo, string description)
        { 
            Recipe recipe = new Recipe
            {
                Description = description,
                ID = int.MaxValue
            };
            Controller.PostEdit(recipe);

            Recipe Recipe = ((from m in Repo.GetAll()
                                where m.Description == description
                                select m).AsQueryable()).FirstOrDefault();
            return Recipe;
        }
         
    }
}

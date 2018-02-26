using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.BaseControllerTests;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{

    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("RecipesController")]
    internal class RecipesControllerShould : RecipesController_Test_Should
    {

        public RecipesControllerShould() => Recipe = new Recipe();


        [TestMethod]
        public void SaveAValidRecipe()
        {
            Recipe vm = new Recipe
            {
                Name = "test SaveAValidRecipe",
                ID = int.MaxValue / 2
            };

            AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit(vm);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;


            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("test SaveAValidRecipe has been saved or modified", adr.Message);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedRecipeWithNameChange()
        {

            IGenericController<Recipe> Controller2 = new RecipesController(repo);
            IGenericController<Recipe> Controller3 = new RecipesController(repo);
            IGenericController<Recipe> Controller4 = new RecipesController(repo);

            Recipe vm = new Recipe
            {
                Name = "0000 test",
                ID = 1000
            };

            // Act 
            ActionResult ar1 = controller.PostEdit(vm);
            ViewResult view1 = (ViewResult)Controller2.Index();
            List<Recipe> ListEntity = (List<Recipe>)(((ListEntity<Recipe>)view1.Model).ListT);
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
            List<Recipe> ListEntity2 = (List<Recipe>)(((ListEntity<Recipe>)view2.Model).ListT);
            Recipe Recipe2 = (from m in ListEntity2
                              where m.Name == "0000 test Edited"
                              select m).AsQueryable().First();

            // Assert
            Assert.AreEqual("0000 test Edited", Recipe2.Name);
        }

         


        [TestMethod]
        public void SaveAllPropertiesInBaseEntity()
        {
            IGenericController<Recipe> ControllerView = new RecipesController(repo);
            IGenericController<Recipe> ControllerDelete = new RecipesController(repo);
            Recipe Recipe = new Recipe
            {
                Name = "___test387",
                Description = "test387 description",
                ID = 774
            };

            controller.PostEdit(Recipe);
            Recipe returnedRecipe = repo.GetById(Recipe.ID);

            Assert.AreEqual(returnedRecipe.Name, Recipe.Name);
            Assert.AreEqual(returnedRecipe.Description, Recipe.Description);
            Assert.AreEqual(returnedRecipe.CreationDate.Day, Recipe.CreationDate.Day);
            Assert.AreEqual(returnedRecipe.ModifiedDate.Day, Recipe.ModifiedDate.Day);
            Assert.AreEqual(returnedRecipe.AddedByUser, Recipe.AddedByUser);
            Assert.AreEqual(returnedRecipe.ModifiedByUser, Recipe.ModifiedByUser);
        }



        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void NotDeleteAnIngredientAfterIngredientIsDetachedFromRecipe()
        {
            IRepository<Recipe> repoRecipe = new TestRepository<Recipe>();
            IRepository<Ingredient> repoIngredient = new TestRepository<Ingredient>();

            IGenericController<Recipe> ControllerSubtract = new RecipesController(repoRecipe);

            Recipe Recipe1 = GetRecipe(repoRecipe, "test NotDeleteAnIngredientAfterIngredientIsDetached");


            Ingredient ingredient2 = GetIngredient(repoIngredient, "test NotDeleteAnIngredientAfterIngredientIsDetached");
            controller.Attach(Recipe1, ingredient2);
         
            ControllerSubtract.Detach(Recipe1, ingredient2);
 
            Assert.IsNotNull(ingredient2);
        }
         

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnRecipeEditViewWithWarningMessageWhenAttachingNonExistingIngredientToExistingRecipe()
        {
            Recipe Recipe = GetRecipe(repo, "test ReturnRecipeEditViewWithErrorMessageWhenAttachingNonExistingIngredientToExistingRRecipe");
            Ingredient ingredient = null;

            AlertDecoratorResult adr = (AlertDecoratorResult)controller.Attach(Recipe, ingredient);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Child was not found", adr.Message);
            Assert.AreEqual(3, routeValues.Count);
            Assert.AreEqual(Recipe.ID, routeValues.ElementAt(0));
            Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(2).ToString());
            Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(1).ToString()); 
        }  
         

         

          

        internal Ingredient GetIngredient(IRepository<Ingredient> repo, string description)
        {
            IGenericController<Ingredient> controller = new IngredientsController(repo);
            Ingredient ivm = new Ingredient
            {
                Description = description,
                ID = int.MaxValue
            };
            controller.PostEdit(ivm);

            Ingredient ingredient = ((from m in repo.GetAll()
                                      where m.Description == description
                                      select m).AsQueryable()).FirstOrDefault();
            return ingredient;
        }

        // phase out - use action result methods instead
        internal Recipe GetRecipe(IRepository<Recipe> repo, string description)
        {
            Recipe recipe = new Recipe
            {
                Description = description,
                ID = int.MaxValue
            };
            controller.PostEdit(recipe);

            Recipe Recipe = ((from m in repo.GetAll()
                              where m.Description == description
                              select m).AsQueryable()).FirstOrDefault();
            return Recipe;
        }

    }
}

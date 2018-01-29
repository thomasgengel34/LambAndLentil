using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.BasicControllerTests;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{

    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("RecipesController")]
    public class RecipesControllerShould: RecipesController_Test_Should
    {  

        public RecipesControllerShould() =>   Recipe = new Recipe(); 
         
         
        [TestMethod]
        public void SaveAValidRecipe()
        { 
            Recipe vm = new Recipe
            {
                Name = "test SaveAValidRecipe",
                ID = int.MaxValue / 2
            }; 

            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.PostEdit(vm);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult; 
            var routeValues = rtrr.RouteValues.Values;
             

            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("test SaveAValidRecipe has been saved or modified", adr.Message);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(0).ToString());
        }
         
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedRecipeWithNameChange()
        {
            
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

           Recipe.Description = "test ActuallyDeleteARecipeFromTheDatabase";
            
            Controller.DeleteConfirmed(Recipe.ID);
            var deletedItem = (from m in Repo.GetAll()
                               where m.Description == Recipe.Description
                               select m).AsQueryable();

            
            Assert.AreEqual(0, deletedItem.Count());
        }


        
        [TestMethod]
        public void SaveAllPropertiesInBaseEntity()
        { 
            IGenericController<Recipe> ControllerView = new RecipesController(Repo);
            IGenericController<Recipe> ControllerDelete = new RecipesController(Repo);
             Recipe Recipe = new Recipe
            {
                Name = "___test387",
                Description = "test387 description",
                ID = 774
            };
             
            Controller.PostEdit(Recipe); 
            Recipe returnedRecipe = Repo.GetById(Recipe.ID);

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
            TestRepository<Recipe> repoRecipe = new TestRepository<Recipe>();
            TestRepository<Ingredient> repoIngredient = new TestRepository<Ingredient>();
            
            IGenericController<Recipe> ControllerSubtract = new RecipesController(repoRecipe);

            Recipe Recipe1 = GetRecipe(repoRecipe, "test NotDeleteAnIngredientAfterIngredientIsDetached");


            Ingredient ingredient2 = GetIngredient(repoIngredient, "test NotDeleteAnIngredientAfterIngredientIsDetached");
            Controller.Attach(Recipe1.ID, ingredient2 );
            // Act
            ControllerSubtract.Detach(Recipe1.ID, ingredient2);


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
            repoIngredient.Save(ingredient);

            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Attach(-1, ingredient );
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
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Attach(Recipe.ID, ingredient );
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
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Attach(-1, ingredient );
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
            TestRepository<Ingredient> repoIngredient = new TestRepository<Ingredient>(); 
            IGenericController<Recipe> ControllerRemoveIngredient = new RecipesController(Repo);

            Recipe Recipe = GetRecipe(Repo, "test ReturnRecipeEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingRecipe");

            Ingredient ingredient = GetIngredient(repoIngredient, "test ReturnRecipeEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingRecipe");
            Controller.Attach(Recipe.ID, ingredient );

            // Act          
            AlertDecoratorResult adr = (AlertDecoratorResult)ControllerRemoveIngredient.Detach(Recipe.ID, ingredient);
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
            AlertDecoratorResult adr = (AlertDecoratorResult)ControllerDetach.Detach(-1, ingredient);
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
            TestRepository<Ingredient> repoIngredient = new TestRepository<Ingredient>();
            IGenericController<Ingredient> ControllerIngredient = new IngredientsController(repoIngredient);
            IGenericController<Recipe> ControllerDetach = new RecipesController(Repo);
            string description = "test ReturnRecipeIndexViewWithWarningWhenDetachingExistingingredientNotAttachedToAnExistingRecipe";
            Ingredient ingredient = GetIngredient(repoIngredient, description);
            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)ControllerDetach.Detach(-1, ingredient);
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
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Detach(Recipe.ID, (Ingredient)null);
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
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Detach(-1, (Ingredient)null);

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

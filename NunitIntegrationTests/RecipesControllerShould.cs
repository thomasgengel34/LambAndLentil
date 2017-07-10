using AutoMapper;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MsTestIntegrationTests
{
    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("RecipesController")]
    public class RecipesControllerShould
    {
        [TestMethod]
        public void CreateAnRecipe()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            RecipesController controller = new RecipesController(repo);
            // Act
            ViewResult vr = controller.Create(LambAndLentil.UI.UIViewType.Create);
            RecipeVM vm = (RecipeVM)vr.Model;
            string modelName = vm.Name;

            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created");
        }

        [TestMethod]
        public void SaveAValidRecipe()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            RecipesController controller = new RecipesController(repo);
            RecipeVM vm = new RecipeVM();
            vm.Name = "test";
            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit(vm);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;


            // Assert 
            try
            {
                Assert.AreEqual("alert-success", adr.AlertClass);
                Assert.AreEqual(4, routeValues.Count);
                Assert.AreEqual(UIControllerType.Recipes.ToString(), routeValues.ElementAt(0).ToString());
                Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(1).ToString());
                Assert.AreEqual("Recipes", routeValues.ElementAt(2).ToString());
                Assert.AreEqual(1.ToString(), routeValues.ElementAt(3).ToString());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Clean Up - should run a  delete test to make sure this works 
                List<Recipe> menus = repo.Recipes.ToList<Recipe>();
                Recipe menu = menus.Where(m => m.Name == "test").FirstOrDefault();

                // Delete it
                controller.DeleteConfirmed(menu.ID);
            }
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedRecipeWithNameChange()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            RecipesController controller1 = new RecipesController(repo);
            RecipesController controller2 = new RecipesController(repo);
            RecipesController controller3 = new RecipesController(repo);
            RecipesController controller4 = new RecipesController(repo);
            RecipesController controller5 = new RecipesController(repo);
            RecipeVM vm = new RecipeVM();
            vm.Name = "0000 test";

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM listVM = (ListVM)view1.Model;
            var result = (from m in listVM.Recipes
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            Recipe ingredient = result.FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("0000 test", ingredient.Name);

            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = ingredient.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM listVM2 = (ListVM)view2.Model;
            var result2 = (from m in listVM2.Recipes
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable();

            ingredient = result2.FirstOrDefault();

            try
            {
                // Assert
                Assert.AreEqual("0000 test Edited", ingredient.Name);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // clean up 
                controller5.DeleteConfirmed(vm.ID);
            }
        }



        [TestMethod]
        [TestCategory("DeleteConfirmed")]
        public void ActuallyDeleteARecipeFromTheDatabase()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            RecipesController editController = new RecipesController(repo);
            RecipesController indexController = new RecipesController(repo);
            RecipesController deleteController = new RecipesController(repo);
            RecipeVM vm = new RecipeVM();
            vm.Name = "0000" + new Guid().ToString();
            ActionResult ar = editController.PostEdit(vm);
            ViewResult view = indexController.Index();
            ListVM listVM = (ListVM)view.Model;
            var result = (from m in listVM.Recipes
                          where m.Name == vm.Name
                          select m).AsQueryable();
            Recipe item = result.FirstOrDefault();

            //Act
            deleteController.DeleteConfirmed(item.ID);
            var deletedItem = (from m in repo.Recipes
                               where m.Name == vm.Name
                               select m).AsQueryable();
            //Assert
            Assert.AreEqual(0, deletedItem.Count());
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedRecipeWithDescriptionChange()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            RecipesController controller1 = new RecipesController(repo);
            RecipesController controller2 = new RecipesController(repo);
            RecipesController controller3 = new RecipesController(repo);
            RecipesController controller4 = new RecipesController(repo);
            RecipesController controller5 = new RecipesController(repo);
            RecipeVM vm = new RecipeVM();
            vm.Name = "0000 test";
            vm.Description = "SaveEditedRecipeWithDescriptionChange Pre-test";


            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM listVM = (ListVM)view1.Model;
            var result = (from m in listVM.Recipes
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            Recipe menu = result.FirstOrDefault();
            try
            {
                // verify initial value:
                Assert.AreEqual("SaveEditedRecipeWithDescriptionChange Pre-test", menu.Description);
            }
            catch (Exception)
            {
                // clean up 
                controller5.DeleteConfirmed(vm.ID);
                throw;
            }
            // now edit it
            vm.ID = menu.ID;
            vm.Name = "0000 test Edited";
            vm.Description = "SaveEditedRecipeWithDescriptionChange Post-test";

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM listVM2 = (ListVM)view2.Model;
            var result2 = (from m in listVM2.Recipes
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable();

            menu = result2.FirstOrDefault();

            try
            {
                // Assert
                Assert.AreEqual("0000 test Edited", menu.Name);
                Assert.AreEqual("SaveEditedRecipeWithDescriptionChange Post-test", menu.Description);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // clean up 
                controller5.DeleteConfirmed(vm.ID);
            }
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnRecipeCreationWithNoParameterCtor()
        {
            // Arrange
            DateTime CreationDate = DateTime.Now;

            // Act
            Recipe ingredient = new Recipe();

            // Assert
            Assert.AreEqual(CreationDate.Date, ingredient.CreationDate.Date);
        }


        [TestMethod]
        [TestCategory("Edit")]
        public void ShouldSaveTheCreationDateOnRecipeCreationWithDateTimeParameter()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);

            // Act
            Recipe ingredient = new Recipe(CreationDate);

            // Assert
            Assert.AreEqual(CreationDate, ingredient.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnRecipeVMCreationWithNoParameterCtor()
        {
            // Arrange
            DateTime CreationDate = DateTime.Now;

            // Act
            RecipeVM ingredientVM = new RecipeVM();

            // Assert
            Assert.AreEqual(CreationDate.Date, ingredientVM.CreationDate.Date);
        }
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnRecipeVMCreationWithDateTimeParameter()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);

            // Act
            RecipeVM ingredientVM = new RecipeVM(CreationDate);

            // Assert
            Assert.AreEqual(CreationDate, ingredientVM.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);
            RecipeVM recipeVM = new RecipeVM(CreationDate);
            recipeVM.Name = "001 Test ";

            EFRepository repo = new EFRepository(); ;
            RecipesController controllerEdit = new RecipesController(repo);
            RecipesController controllerView = new RecipesController(repo);
            RecipesController controllerDelete = new RecipesController(repo);

            // Act
            controllerEdit.PostEdit(recipeVM);
            ViewResult view = controllerView.Index();
            ListVM listVM = (ListVM)view.Model;
            var result = (from m in listVM.Recipes
                          where m.Name == "001 Test "
                          select m).AsQueryable();

            Recipe recipe = result.FirstOrDefault();

            DateTime shouldBeSameDate = recipe.CreationDate;
            try
            {
                // Assert
                Assert.AreEqual(CreationDate, shouldBeSameDate);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Cleanup
                controllerDelete.DeleteConfirmed(recipe.ID);
            }
        }


        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            // Arrange
            EFRepository repo = new EFRepository();
            RecipesController controllerPost = new RecipesController(repo);
            RecipesController controllerPost1 = new RecipesController(repo);
            RecipesController controllerView = new RecipesController(repo);
            RecipesController controllerView1 = new RecipesController(repo);
            RecipesController controllerDelete = new RecipesController(repo);

            RecipeVM vm = new RecipeVM();
            vm.Name = "002 Test Mod";
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
            controllerPost.PostEdit(vm);

            ViewResult view = controllerView.Index();
            ListVM listVM = (ListVM)view.Model;
            var result = (from m in listVM.Recipes
                          where m.Name == "002 Test Mod"
                          select m).AsQueryable();
            Recipe ingredient = result.FirstOrDefault();
            vm = Mapper.Map<Recipe, RecipeVM>(ingredient);

            vm.Description = "I've been edited to delay a bit";

            controllerPost1.PostEdit(vm);


            ViewResult view1 = controllerView.Index();
            listVM = (ListVM)view1.Model;
            var result1 = (from m in listVM.Recipes
                           where m.Name == "002 Test Mod"
                           select m).AsQueryable();

            ingredient = result1.FirstOrDefault();

            DateTime shouldBeSameDate = ingredient.CreationDate;
            DateTime shouldBeLaterDate = ingredient.ModifiedDate;
            try
            {
                // Assert
                Assert.AreEqual(CreationDate, shouldBeSameDate);
                Assert.AreNotEqual(mod, shouldBeLaterDate);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Cleanup
                controllerDelete.DeleteConfirmed(ingredient.ID);
            }
        }
        [TestMethod]
        public void SaveAllPropertiesInBaseEntity()
        {
            // Arrange
            EFRepository repo = new EFRepository();
            RecipesController controllerPost = new RecipesController(repo);
            RecipesController controllerView = new RecipesController(repo);
            RecipesController controllerDelete = new RecipesController(repo);
            RecipeVM vm = new RecipeVM();
            vm.Name = "___test387";
            vm.Description = "test387 description";

            // Act
            controllerPost.PostEdit(vm);
            ViewResult view1 = controllerView.Index();
            ListVM listVM = (ListVM)view1.Model;
            var result1 = (from m in listVM.Recipes
                           where m.Name == "___test387"
                           select m).AsQueryable();

            Recipe recipe = result1.FirstOrDefault();

            try
            {
                Assert.AreEqual(vm.Name, recipe.Name);
                Assert.AreEqual(vm.Description, recipe.Description);
                Assert.AreEqual(vm.CreationDate.Day, recipe.CreationDate.Day);
                Assert.AreEqual(vm.ModifiedDate.Day, recipe.ModifiedDate.Day);
                Assert.AreEqual(vm.AddedByUser, recipe.AddedByUser);
                Assert.AreEqual(vm.ModifiedByUser, recipe.ModifiedByUser);

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                controllerDelete.DeleteConfirmed(recipe.ID);
            }
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void AddAnExistingIngredientToAnExistingRecipe()
        {
            // Arrange

            EFRepository repo = new EFRepository();
            RecipesController controllerAdd = new RecipesController(repo);
            IngredientsController controllerAddI = new IngredientsController(repo);
            RecipesController controllerCleanup = new RecipesController(repo);
            RecipeVM recipeVM = new RecipeVM();
            recipeVM.Description = "test AddAnExistingIngredientToAnExistingRecipe";
            IngredientVM ingredientVM = new IngredientVM();
            ingredientVM.Description = "test AddAnExistingIngredientToAnExistingRecipe";
            controllerAdd.PostEdit(recipeVM);
            controllerAddI.PostEdit(ingredientVM);

            var result1 = (from m in repo.Recipes
                           where m.Description == "test AddAnExistingIngredientToAnExistingRecipe"
                           select m).AsQueryable();

            Recipe recipe1 = result1.FirstOrDefault();

            var result2 = (from m in repo.Ingredients
                           where m.Description == "test AddAnExistingIngredientToAnExistingRecipe"
                           select m).AsQueryable();

            Ingredient ingredient2 = result2.FirstOrDefault();

            // Act
            controllerAdd.AttachIngredient(recipe1.ID, ingredient2.ID);

            // Assert 
            Assert.AreEqual(1, recipe1.Ingredients.Count());
            // how do I know the correct ingredient was added?
            Assert.AreEqual(ingredient2.ID, recipe1.Ingredients.First().ID);

            // Cleanup
            IngredientsController icontroller = new IngredientsController(repo);

            RecipeVM recipe1VM = Mapper.Map<Recipe, RecipeVM>(recipe1);
            IngredientVM ingredient2VM = Mapper.Map<Ingredient, IngredientVM>(ingredient2);
            controllerCleanup.DeleteConfirmed(recipe1VM.ID);
            icontroller.DeleteConfirmed(ingredient2VM.ID);
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void NotDeleteAnIngredientAfterIngredientIsDetached()
        {
            // Arrange
            EFRepository repo = new EFRepository();
            RecipesController controllerAdd = new RecipesController(repo);
            RecipesController controllerSubtract = new RecipesController(repo);
            IngredientsController controllerAddI = new IngredientsController(repo);
            RecipeVM recipeVM = new RecipeVM();
            recipeVM.Description = "test NotDeleteAnIngredientAfterIngredientIsDetached";
            IngredientVM ingredientVM = new IngredientVM();
            ingredientVM.Description = "test NotDeleteAnIngredientAfterIngredientIsDetached";
            controllerAdd.PostEdit(recipeVM);
            controllerAddI.PostEdit(ingredientVM);

            var result1 = (from m in repo.Recipes
                           where m.Description == "test NotDeleteAnIngredientAfterIngredientIsDetached"
                           select m).AsQueryable();

            Recipe recipe1 = result1.FirstOrDefault();

            var result2 = (from m in repo.Ingredients
                           where m.Description == "test NotDeleteAnIngredientAfterIngredientIsDetached"
                           select m).AsQueryable();

            Ingredient ingredient2 = result2.FirstOrDefault();
            controllerAdd.AttachIngredient(recipe1.ID, ingredient2.ID);
            // Act
            controllerSubtract.RemoveIngredient(recipe1.ID, ingredient2.ID);


            // Assert
            Assert.IsNotNull(ingredient2);

            // Cleanup
            RecipesController controllerCleanupRecipe = new RecipesController(repo);
            IngredientsController controllerCleanupIngredient = new IngredientsController(repo);
            controllerCleanupRecipe.DeleteConfirmed(recipe1.ID);
            controllerCleanupIngredient.DeleteConfirmed(ingredient2.ID);
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingExistIngredientToNonExistingRecipe()
        {
            // Arrange
            EFRepository repo = new EFRepository();
            RecipesController controllerAdd = new RecipesController(repo);
            RecipesController controllerSubtract = new RecipesController(repo);
            IngredientsController controllerAddI = new IngredientsController(repo);
            IngredientVM ingredientVM = new IngredientVM();
            ingredientVM.Description = "test ReturnIndexViewWhenAttachingExistIngredientToNonExistingRecipe";
            controllerAddI.PostEdit(ingredientVM);

            var result2 = (from m in repo.Ingredients
                           where m.Description == "test ReturnIndexViewWhenAttachingExistIngredientToNonExistingRecipe"
                           select m).AsQueryable();

            Ingredient ingredient2 = result2.FirstOrDefault();

            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)controllerAdd.AttachIngredient(-1, ingredient2.ID);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert
            try
            {
                Assert.AreEqual("alert-warning", adr.AlertClass);
                Assert.AreEqual("Recipe was not found", adr.Message);
                Assert.AreEqual(1, routeValues.Count);
                Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Cleanup 
                IngredientsController controllerCleanupIngredient = new IngredientsController(repo);
                controllerCleanupIngredient.DeleteConfirmed(ingredient2.ID);
            }
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnRecipeEditViewWithWarningMessageWhenAddingNonExistingIngredientToExistingRrecipe()
        {
            // Arrange
            EFRepository repo = new EFRepository();
            RecipesController controllerAddRecipe = new RecipesController(repo);
            RecipeVM rvm = new RecipeVM();
            rvm.Description = "test ReturnRecipeEditViewWithErrorMessageWhenAddingNonExistingIngredientToExistingRrecipe";
            controllerAddRecipe.PostEdit(rvm);
            RecipesController controllerAttachIngredient = new RecipesController(repo);
            var result1 = (from m in repo.Recipes
                           where m.Description == "test ReturnRecipeEditViewWithErrorMessageWhenAddingNonExistingIngredientToExistingRrecipe"
                           select m).AsQueryable();

            Recipe recipe1 = result1.FirstOrDefault();

            // Act  
            AlertDecoratorResult adr = (AlertDecoratorResult)controllerAttachIngredient.AttachIngredient(recipe1.ID, -1);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert
            try
            {
                Assert.AreEqual("alert-warning", adr.AlertClass);
                Assert.AreEqual("Please choose an ingredient", adr.Message);
                 Assert.AreEqual(3, routeValues.Count);
                 Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(1).ToString());
                 Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(2).ToString());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Cleanup 
                RecipesController controllerCleanupIngredient = new RecipesController(repo);
                controllerCleanupIngredient.DeleteConfirmed(recipe1.ID);
            }
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingNonExistIngredientToNonExistingRecipe()
        {
            // Arrange
            EFRepository repo = new EFRepository();
            RecipesController controller  = new RecipesController(repo);

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.AttachIngredient(-1, -1);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Recipe was not found", adr.Message);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString()); 

            // Clean up not needed

        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnRecipeEditViewWithSuccessMessageWhenRemovingExistingIngredientFromExistingRecipe()
        {
            // Arrange
            EFRepository repo = new EFRepository();
            RecipesController controllerAddRecipe = new RecipesController(repo);
            RecipesController controllerAttachIngredient = new RecipesController(repo);
            RecipesController controllerRemoveIngredient = new RecipesController(repo);
            IngredientsController controllerIngredient = new IngredientsController(repo);

            RecipeVM rvm = new RecipeVM();
            rvm.Description = "test ReturnRecipeEditViewWithSuccessMessageWhenRemovingExistingIngredientFromExistingRecipe";
            controllerAddRecipe.PostEdit(rvm);
          
            var result1 = (from m in repo.Recipes
                           where m.Description == "test ReturnRecipeEditViewWithSuccessMessageWhenRemovingExistingIngredientFromExistingRecipe"
                           select m).AsQueryable();

            Recipe recipe1 = result1.FirstOrDefault();

            IngredientVM ivm = new IngredientVM();
           ivm.Description = "test ReturnRecipeEditViewWithSuccessMessageWhenRemovingExistingIngredientFromExistingRecipe";
            controllerIngredient.PostEdit(ivm);

            var result2 = (from m in repo.Ingredients
                           where m.Description == "test ReturnRecipeEditViewWithSuccessMessageWhenRemovingExistingIngredientFromExistingRecipe"
                           select m).AsQueryable();

            Ingredient ingredient2 = result2.FirstOrDefault();
            controllerAttachIngredient.AttachIngredient(recipe1.ID, ingredient2.ID);

            // Act          
            AlertDecoratorResult adr = (AlertDecoratorResult)   controllerRemoveIngredient.RemoveIngredient(recipe1.ID, ingredient2.ID); 
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert
            try
            {
                Assert.AreEqual("alert-success", adr.AlertClass);
                Assert.AreEqual("Ingredient was Successfully Removed", adr.Message);
                Assert.AreEqual(3, routeValues.Count);
                Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(1).ToString());
                Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(2).ToString());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Cleanup 
                RecipesController controllerCleanupRecipe = new RecipesController(repo);
                controllerCleanupRecipe.DeleteConfirmed(recipe1.ID);
                IngredientsController controllerCleanupIngredient = new IngredientsController(repo);
                controllerCleanupIngredient.DeleteConfirmed(ingredient2.ID);
            }
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNoExistingRecipe()
        {
            // Arrange
            EFRepository repo = new EFRepository();
            IngredientsController controllerIngredient = new IngredientsController(repo);
            RecipesController controllerRemoveIngredient = new RecipesController(repo);

            IngredientVM ivm = new IngredientVM();
            ivm.Description = "test ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNoExistingRecipe";
            controllerIngredient.PostEdit(ivm);

            Ingredient ingredient = ((from m in repo.Ingredients
                                      where m.Description == "test ReturnRecipeEditViewWithSuccessMessageWhenRemovingExistingIngredientFromExistingRecipe"
                                      select m).AsQueryable()).FirstOrDefault();
            // Act  
            AlertDecoratorResult adr = (AlertDecoratorResult)controllerRemoveIngredient.RemoveIngredient(-1, ingredient.ID);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert
            try
            {
                Assert.AreEqual("alert-warning", adr.AlertClass);
                Assert.AreEqual("Recipe was not found", adr.Message);
                Assert.AreEqual(1, routeValues.Count);
                Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString()); 
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Cleanup 
          
                IngredientsController controllerCleanupIngredient = new IngredientsController(repo);
                controllerCleanupIngredient.DeleteConfirmed(ingredient.ID);
            }
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnRecipeEditViewWithWarningWhenRemovingExistingingredientNotAttachedToAnExistingRrecipe()
        {
            // Arrange

            // Act 

            // Assert
            Assert.Fail();

            // Clean up
        }
    }
}

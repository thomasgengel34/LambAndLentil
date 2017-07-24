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
           EFRepository<Recipe, RecipeVM> repo = new EFRepository<Recipe, RecipeVM>(); ;
            RecipesController controller = new RecipesController();
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
           EFRepository<Recipe, RecipeVM> repo = new EFRepository<Recipe, RecipeVM>(); ;
            RecipesController controller = new RecipesController();
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
                Recipe menu = repo.GetAll().Where(m => m.Name == "test").FirstOrDefault(); 
                controller.DeleteConfirmed(menu.ID);
            }
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedRecipeWithNameChange()
        {
            // Arrange
           EFRepository<Recipe, RecipeVM> repo = new EFRepository<Recipe, RecipeVM>(); ;
            RecipesController controller1 = new RecipesController();
            RecipesController controller2 = new RecipesController();
            RecipesController controller3 = new RecipesController();
            RecipesController controller4 = new RecipesController();
            RecipesController controller5 = new RecipesController();
            RecipeVM vm = new RecipeVM();
            vm.Name = "0000 test";

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
           ListVM<Recipe,RecipeVM> listVM = (ListVM<Recipe,RecipeVM>)view1.Model;
            var result = (from m in listVM.Entities
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            Recipe  item= result.FirstOrDefault();
            RecipeVM recipe = Mapper.Map<Recipe, RecipeVM>(item);

            // verify initial value:
            Assert.AreEqual("0000 test",recipe.Name);

            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = recipe.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
           ListVM<Recipe,RecipeVM> listVM2 = (ListVM<Recipe,RecipeVM>)view2.Model;
            var result2 = (from m in listVM2.Entities
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable();

             
            Recipe item2 = result2.FirstOrDefault();
            RecipeVM recipe2 = Mapper.Map<Recipe, RecipeVM>(item);
            try
            {
                // Assert
                Assert.AreEqual("0000 test Edited", recipe2.Name);
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
           EFRepository<Recipe, RecipeVM> repo = new EFRepository<Recipe, RecipeVM>(); ;
            RecipesController editController = new RecipesController();
            RecipesController indexController = new RecipesController();
            RecipesController deleteController = new RecipesController();
            RecipeVM vm = new RecipeVM();
            vm.Name = "0000" + new Guid().ToString();
            ActionResult ar = editController.PostEdit(vm);
            ViewResult view = indexController.Index();
           ListVM<Recipe,RecipeVM> listVM = (ListVM<Recipe,RecipeVM>)view.Model;
            var result = (from m in listVM.Entities
                          where m.Name == vm.Name
                          select m).AsQueryable();
            Recipe  item = result.FirstOrDefault(); 
            RecipeVM recipe2 = Mapper.Map<Recipe, RecipeVM>(item);

            //Act
            deleteController.DeleteConfirmed(item.ID);
            var deletedItem = (from m in repo.GetAll()
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
           EFRepository<Recipe, RecipeVM> repo = new EFRepository<Recipe, RecipeVM>(); ;
            RecipesController controller1 = new RecipesController();
            RecipesController controller2 = new RecipesController();
            RecipesController controller3 = new RecipesController();
            RecipesController controller4 = new RecipesController();
            RecipesController controller5 = new RecipesController();
            RecipeVM vm = new RecipeVM();
            vm.Name = "0000 test";
            vm.Description = "SaveEditedRecipeWithDescriptionChange Pre-test";


            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
           ListVM<Recipe,RecipeVM> listVM = (ListVM<Recipe,RecipeVM>)view1.Model;
            var result = (from m in listVM.Entities
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            Recipe  item = result.FirstOrDefault();             
            RecipeVM recipe = Mapper.Map<Recipe, RecipeVM>(item);

            try
            {
                // verify initial value:
                Assert.AreEqual("SaveEditedRecipeWithDescriptionChange Pre-test", recipe.Description);
            }
            catch (Exception)
            {
                // clean up 
                controller5.DeleteConfirmed(vm.ID);
                throw;
            }
            // now edit it
            vm.ID = recipe.ID;
            vm.Name = "0000 test Edited";
            vm.Description = "SaveEditedRecipeWithDescriptionChange Post-test";

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
           ListVM<Recipe,RecipeVM> listVM2 = (ListVM<Recipe,RecipeVM>)view2.Model;
            var result2 = (from m in listVM2.Entities
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable(); 
            Recipe item2 = result2.FirstOrDefault();
            RecipeVM recipe2 = Mapper.Map<Recipe, RecipeVM>(item);
            try
            {
                // Assert
                Assert.AreEqual("0000 test Edited", recipe2.Name);
                Assert.AreEqual("SaveEditedRecipeWithDescriptionChange Post-test", recipe2.Description);
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
        public void  SaveTheCreationDateOnRecipeCreationWithDateTimeParameter()
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

           EFRepository<Recipe, RecipeVM> repo = new EFRepository<Recipe, RecipeVM>(); ;
            RecipesController controllerEdit = new RecipesController();
            RecipesController controllerView = new RecipesController();
            RecipesController controllerDelete = new RecipesController();

            // Act
            controllerEdit.PostEdit(recipeVM);
            ViewResult view = controllerView.Index();
           ListVM<Recipe,RecipeVM> listVM = (ListVM<Recipe,RecipeVM>)view.Model;
            var result = (from m in listVM.Entities
                          where m.Name == "001 Test "
                          select m).AsQueryable();
            Recipe item  = result.FirstOrDefault();
            RecipeVM recipe  = Mapper.Map<Recipe, RecipeVM>(item);
           

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
           EFRepository<Recipe, RecipeVM> repo = new EFRepository<Recipe, RecipeVM>();
            RecipesController controllerPost = new RecipesController();
            RecipesController controllerPost1 = new RecipesController();
            RecipesController controllerView = new RecipesController();
            RecipesController controllerView1 = new RecipesController();
            RecipesController controllerDelete = new RecipesController();

            RecipeVM vm = new RecipeVM();
            vm.Name = "002 Test Mod";
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
            controllerPost.PostEdit(vm);

            ViewResult view = controllerView.Index();
           ListVM<Recipe,RecipeVM> listVM = (ListVM<Recipe,RecipeVM>)view.Model;
            var result = (from m in listVM.Entities
                          where m.Name == "002 Test Mod"
                          select m).AsQueryable();
             
            Recipe item = result.FirstOrDefault();
            RecipeVM vm2 = Mapper.Map<Recipe, RecipeVM>(item);

            vm.Description = "I've been edited to delay a bit";

            controllerPost1.PostEdit(vm2);


            ViewResult view1 = controllerView.Index();
            listVM = (ListVM<Recipe,RecipeVM>)view1.Model;
            var result1 = (from m in listVM.Entities
                           where m.Name == "002 Test Mod"
                           select m).AsQueryable();

            
            Recipe item3  = result1.FirstOrDefault();
            RecipeVM vm3 = Mapper.Map<Recipe, RecipeVM>(item3);
            DateTime shouldBeSameDate = vm3.CreationDate;
            DateTime shouldBeLaterDate = vm3.ModifiedDate;
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
                controllerDelete.DeleteConfirmed(vm3.ID);
            }
        }
        [TestMethod]
        public void SaveAllPropertiesInBaseEntity()
        {
            // Arrange
           EFRepository<Recipe, RecipeVM> repo = new EFRepository<Recipe, RecipeVM>();
            RecipesController controllerPost = new RecipesController();
            RecipesController controllerView = new RecipesController();
            RecipesController controllerDelete = new RecipesController();
            RecipeVM vm = new RecipeVM();
            vm.Name = "___test387";
            vm.Description = "test387 description";

            // Act
            controllerPost.PostEdit(vm);
            ViewResult view1 = controllerView.Index();
           ListVM<Recipe,RecipeVM> listVM = (ListVM<Recipe,RecipeVM>)view1.Model;
            var result1 = (from m in listVM.Entities
                           where m.Name == "___test387"
                           select m).AsQueryable(); 

            Recipe item = result1.FirstOrDefault();
            RecipeVM recipe  = Mapper.Map<Recipe, RecipeVM>(item);

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
        public void AttachAnExistingIngredientToAnExistingRecipe()
        {
            // Arrange

           EFRepository<Recipe, RecipeVM> repo = new EFRepository<Recipe, RecipeVM>();
           EFRepository<Ingredient, IngredientVM> repoIngredient= new EFRepository<Ingredient, IngredientVM>();
            RecipesController controller = new RecipesController(); 

            Recipe recipe = GetRecipe(repo, "test AttachAnExistingIngredientToAnExistingRecipe");
            Ingredient ingredient = GetIngredient(repoIngredient, "test AttachAnExistingIngredientToAnExistingRecipe"); 

            // Act
            controller.AttachIngredient(recipe.ID, ingredient.ID);

            // Assert 
            Assert.AreEqual(1, recipe.Ingredients.Count());
            // how do I know the correct ingredient was added?
            Assert.AreEqual(ingredient.ID, recipe.Ingredients.First().ID);

            // Cleanup
            IngredientsController  controllerCleanupIngredient = new IngredientsController();;
            RecipesController controllerCleanupRecipe = new RecipesController();

            RecipeVM recipeVM = Mapper.Map<Recipe, RecipeVM>(recipe);
            IngredientVM ingredientVM = Mapper.Map<Ingredient, IngredientVM>(ingredient);

            controllerCleanupRecipe.DeleteConfirmed(recipeVM.ID);
            controllerCleanupIngredient.DeleteConfirmed(ingredientVM.ID);
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void NotDeleteAnIngredientAfterIngredientIsDetached()
        {
            // Arrange
           EFRepository<Recipe, RecipeVM> repo = new EFRepository<Recipe, RecipeVM>(); 
           EFRepository<Ingredient, IngredientVM> repoIngredient = new EFRepository<Ingredient, IngredientVM>();
            RecipesController controller  = new RecipesController();  
            RecipesController controllerSubtract  = new RecipesController();

            Recipe recipe1 = GetRecipe(repo, "test NotDeleteAnIngredientAfterIngredientIsDetached");


            Ingredient ingredient2 = GetIngredient(repoIngredient, "test NotDeleteAnIngredientAfterIngredientIsDetached");
            controller.AttachIngredient(recipe1.ID, ingredient2.ID);
            // Act
            controllerSubtract.RemoveIngredient(recipe1.ID, ingredient2.ID);


            // Assert
            Assert.IsNotNull(ingredient2);

            // Cleanup
            RecipesController controllerCleanupRecipe = new RecipesController();
            IngredientsController controllerCleanupIngredient = new IngredientsController();;
            controllerCleanupRecipe.DeleteConfirmed(recipe1.ID);
            controllerCleanupIngredient.DeleteConfirmed(ingredient2.ID);
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingExistIngredientToNonExistingRecipe()
        {
            // Arrange
           EFRepository<Recipe, RecipeVM> repo = new EFRepository<Recipe, RecipeVM>();
           EFRepository<Ingredient, IngredientVM> repoIngredient = new EFRepository<Ingredient, IngredientVM>();
            RecipesController controllerAttach = new RecipesController();
            RecipesController controllerSubtract = new RecipesController();

            string description = "test ReturnIndexViewWhenAttachingExistIngredientToNonExistingRecipe";

            Ingredient ingredient = GetIngredient(repoIngredient   , description);

            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)controllerAttach.AttachIngredient(-1, ingredient.ID);
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
                IngredientsController controllerCleanupIngredient = new IngredientsController();;
                controllerCleanupIngredient.DeleteConfirmed(ingredient.ID);
            }
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnRecipeEditViewWithWarningMessageWhenAttachingNonExistingIngredientToExistingRrecipe()
        {
            // Arrange
           EFRepository<Recipe, RecipeVM> repo = new EFRepository<Recipe, RecipeVM>();  
            RecipesController controller = new RecipesController(); 
            Recipe recipe  = GetRecipe(repo, "test ReturnRecipeEditViewWithErrorMessageWhenAttachingNonExistingIngredientToExistingRrecipe");

            // Act  
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.AttachIngredient(recipe.ID, -1);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert
            try
            {
                Assert.AreEqual("alert-warning", adr.AlertClass);
                Assert.AreEqual("Please choose a(n) Ingredient", adr.Message);
                Assert.AreEqual(3, routeValues.Count);
                Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(2).ToString());
                Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(1).ToString());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Cleanup 
                RecipesController controllerCleanupRecipe = new RecipesController();
                controllerCleanupRecipe.DeleteConfirmed(recipe.ID);
            }
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingNonExistIngredientToNonExistingRecipe()
        {
            // Arrange
           EFRepository<Recipe, RecipeVM> repo = new EFRepository<Recipe, RecipeVM>();
            RecipesController controller = new RecipesController();

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
        public void ReturnRecipeEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingRecipe()
        {
            // Arrange
           EFRepository<Recipe, RecipeVM> repo = new EFRepository<Recipe, RecipeVM>();
           EFRepository<Ingredient, IngredientVM> repoIngredient = new EFRepository<Ingredient, IngredientVM>();

            RecipesController controllerAttachIngredient = new RecipesController();
            RecipesController controllerRemoveIngredient = new RecipesController(); 
  
            Recipe recipe  = GetRecipe(repo, "test ReturnRecipeEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingRecipe");





            Ingredient ingredient  = GetIngredient(repoIngredient, "test ReturnRecipeEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingRecipe");
            controllerAttachIngredient.AttachIngredient(recipe.ID, ingredient.ID);

            // Act          
            AlertDecoratorResult adr = (AlertDecoratorResult)controllerRemoveIngredient.RemoveIngredient(recipe.ID, ingredient.ID);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert
            try
            {
                Assert.AreEqual("alert-success", adr.AlertClass);
                Assert.AreEqual("Ingredient was Successfully Detached!", adr.Message);
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
                RecipesController controllerCleanupRecipe = new RecipesController();
                controllerCleanupRecipe.DeleteConfirmed(recipe.ID);
                IngredientsController controllerCleanupIngredient = new IngredientsController();;
                controllerCleanupIngredient.DeleteConfirmed(ingredient.ID);
            }
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNonExistingRecipe()
        {
            // Arrange
           EFRepository<Recipe, RecipeVM> repo = new EFRepository<Recipe, RecipeVM>();
           EFRepository<Ingredient, IngredientVM> repoIngredient = new EFRepository<Ingredient, IngredientVM>();
            RecipesController controllerDetachIngredient = new RecipesController();
            string description = "test ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNoExistingRecipe";

            Ingredient ingredient = GetIngredient(repoIngredient, description);
            // Act  
            AlertDecoratorResult adr = (AlertDecoratorResult)controllerDetachIngredient.RemoveIngredient(-1, ingredient.ID);
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

                IngredientsController controllerCleanupIngredient = new IngredientsController();;
                controllerCleanupIngredient.DeleteConfirmed(ingredient.ID);
            }
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnRecipeIndexViewWithWarningWhenDetachingExistingingredientNotAttachedToAnExistingRecipe()
        {
            // Arrange
           EFRepository<Recipe, RecipeVM> repo = new EFRepository<Recipe, RecipeVM>();
            EFRepository<Ingredient, IngredientVM> repoIngredient = new EFRepository<Ingredient, IngredientVM>();
            IngredientsController controllerIngredient = new IngredientsController();;
            RecipesController controllerDetachIngredient = new RecipesController();
            string description = "test ReturnRecipeIndexViewWithWarningWhenDetachingExistingingredientNotAttachedToAnExistingRecipe";
            Ingredient ingredient = GetIngredient(repoIngredient, description);
            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)controllerDetachIngredient.RemoveIngredient(-1, ingredient.ID);
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

                IngredientsController controllerCleanupIngredient = new IngredientsController();
                controllerCleanupIngredient.DeleteConfirmed(ingredient.ID);
            }
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnRecipeEditViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToExistingRecipe()
        {
            // Arrange
           EFRepository<Recipe, RecipeVM> repo = new EFRepository<Recipe, RecipeVM>();
            RecipesController controllerRecipe = new RecipesController();
            RecipesController controllerDetachIngredient = new RecipesController();
            Recipe recipe = GetRecipe(repo, "test ReturnRecipeEditViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToExistingRecipe");
            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)controllerDetachIngredient.RemoveIngredient(recipe.ID, -1);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert
            try
            {
                Assert.AreEqual("alert-warning", adr.AlertClass);
                Assert.AreEqual("Please choose a(n) Ingredient", adr.Message);
                Assert.AreEqual(3, routeValues.Count);
                Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(2).ToString());
                Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(1).ToString());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Cleanup 

                RecipesController controllerCleanup = new RecipesController();
                controllerCleanup.DeleteConfirmed(recipe.ID);
            }
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToANonExistingRecipe()
        {
            // Arrange
           EFRepository<Recipe, RecipeVM> repo = new EFRepository<Recipe, RecipeVM>();
            RecipesController controller = new RecipesController();

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.RemoveIngredient(-1, -1);

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
                // Cleanup not needed - this is kept for the sake of completeness
            }
        }

      internal Ingredient GetIngredient(EFRepository<Ingredient, IngredientVM> repo, string description)
        {
            IngredientsController controller = new IngredientsController();
            IngredientVM ivm = new IngredientVM();
            ivm.Description = description;
            controller.PostEdit(ivm);

            Ingredient ingredient = ((from m in repo.GetAll()
                                      where m.Description == description
                                      select m).AsQueryable()).FirstOrDefault();
            return ingredient;
        }


        internal Recipe GetRecipe(EFRepository<Recipe, RecipeVM> repo, string description)
        {
            RecipesController controller = new RecipesController();
            RecipeVM vm = new RecipeVM();
            vm.Description = description;
            controller.PostEdit(vm);

            Recipe recipe = ((from m in repo.GetAll()
                              where m.Description == description
                              select m).AsQueryable()).FirstOrDefault();
            return recipe;
        }
    }
}

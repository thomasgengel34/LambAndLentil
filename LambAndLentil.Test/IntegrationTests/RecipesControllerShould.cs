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

namespace IntegrationTests
{
    [Ignore]
    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("RecipesController")]
    public class RecipesControllerShould
    {
        //[TestMethod]
        //public void CreateARecipe()
        //{
        //    // Arrange
        //    JSONRepository<Recipe, RecipeVM> repoRecipe = new JSONRepository<Recipe, RecipeVM>();
        //    RecipesController controller = new RecipesController(repoRecipe);
        //    // Act
        //    ViewResult vr = controller.Create(LambAndLentil.UI.UIViewType.Create);
        //    RecipeVM vm = (RecipeVM)vr.Model;
        //    string modelName = vm.Name;

        //    // Assert 
        //    Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
        //    Assert.AreEqual(modelName, "Newly Created");
        //}

        //[TestMethod]
        //public void SaveAValidRecipe()
        //{
        //    // Arrange
        //    IRepository<Recipe, RecipeVM> repoRecipe = new JSONRepository<Recipe, RecipeVM>(); ;
        //    RecipesController controller = new RecipesController(repoRecipe);
        //    RecipeVM vm = new RecipeVM();
        //    vm.Name = "test";
        //    // Act
        //    AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit(vm);
        //    RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

        //    var routeValues = rtrr.RouteValues.Values;


        //    // Assert 
        //    try
        //    {
        //        Assert.AreEqual("alert-success", adr.AlertClass);
        //        Assert.AreEqual(4, routeValues.Count);
        //        Assert.AreEqual(UIControllerType.Recipes.ToString(), routeValues.ElementAt(0).ToString());
        //        Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(1).ToString());
        //        Assert.AreEqual("Recipes", routeValues.ElementAt(2).ToString());
        //        Assert.AreEqual(1.ToString(), routeValues.ElementAt(3).ToString());
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        // Clean Up - should run a  delete test to make sure this works  
        //        Recipe menu = repoRecipe.GetAllT().Where(m => m.Name == "test").FirstOrDefault();
        //        controller.DeleteConfirmed(menu.ID);
        //    }
        //}

        //[TestMethod]
        //[TestCategory("Edit")]
        //public void SaveEditedRecipeWithNameChange()
        //{
        //    // Arrange
        //    JSONRepository<Recipe, RecipeVM> repoRecipe = new JSONRepository<Recipe, RecipeVM>(); ;
        //    RecipesController controller1 = new RecipesController(repoRecipe);
        //    RecipesController controller2 = new RecipesController(repoRecipe);
        //    RecipesController controller3 = new RecipesController(repoRecipe);
        //    RecipesController controller4 = new RecipesController(repoRecipe);
        //    RecipesController controller5 = new RecipesController(repoRecipe);
        //    RecipeVM vm = new RecipeVM();
        //    vm.Name = "0000 test";

        //    // Act 
        //    ActionResult ar1 = controller1.PostEdit(vm);
        //    ViewResult view1 = controller2.Index();
        //    ListVM<Recipe, RecipeVM> listVM = (ListVM<Recipe, RecipeVM>)view1.Model;
        //    var result = (from m in listVM.Entities
        //                  where m.Name == "0000 test"
        //                  select m).AsQueryable();

        //    Recipe item = result.FirstOrDefault();
        //    RecipeVM recipe = Mapper.Map<Recipe, RecipeVM>(item);

        //    // verify initial value:
        //    Assert.AreEqual("0000 test", recipe.Name);

        //    // now edit it
        //    vm.Name = "0000 test Edited";
        //    vm.ID = recipe.ID;
        //    ActionResult ar2 = controller3.PostEdit(vm);
        //    ViewResult view2 = controller4.Index();
        //    ListVM<Recipe, RecipeVM> listVM2 = (ListVM<Recipe, RecipeVM>)view2.Model;
        //    var result2 = (from m in listVM2.Entities
        //                   where m.Name == "0000 test Edited"
        //                   select m).AsQueryable();


        //    Recipe item2 = result2.FirstOrDefault();
        //    RecipeVM recipe2 = Mapper.Map<Recipe, RecipeVM>(item);
        //    try
        //    {
        //        // Assert
        //        Assert.AreEqual("0000 test Edited", recipe2.Name);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        // clean up 
        //        controller5.DeleteConfirmed(vm.ID);
        //    }
        //}



        //[TestMethod]
        //[TestCategory("DeleteConfirmed")]
        //public void ActuallyDeleteARecipeFromTheDatabase()
        //{
        //    // Arrange
        //    JSONRepository<Recipe, RecipeVM> repoRecipe = new JSONRepository<Recipe, RecipeVM>();
        //    RecipesController controller = new RecipesController(repoRecipe);
        //    Recipe item = GetRecipe(repoRecipe, "test ActuallyDeleteARecipeFromTheDatabase");
        //    //Act
        //    controller.DeleteConfirmed(item.ID);
        //    var deletedItem = (from m in repoRecipe.GetAllT()
        //                       where m.Description == item.Description
        //                       select m).AsQueryable();

        //    //Assert
        //    Assert.AreEqual(0, deletedItem.Count());
        //}

        //[TestMethod]
        //[TestCategory("Edit")]
        //public void SaveEditedRecipeWithDescriptionChange()
        //{
        //    // Arrange
        //    JSONRepository<Recipe, RecipeVM> repoRecipe = new JSONRepository<Recipe, RecipeVM>(); ;
        //    RecipesController controller1 = new RecipesController(repoRecipe);
        //    RecipesController controller2 = new RecipesController(repoRecipe);
        //    RecipesController controller3 = new RecipesController(repoRecipe);
        //    RecipesController controller4 = new RecipesController(repoRecipe);
        //    RecipesController controller5 = new RecipesController(repoRecipe);
        //    RecipeVM vm = new RecipeVM();
        //    vm.Name = "0000 test";
        //    vm.Description = "SaveEditedRecipeWithDescriptionChange Pre-test";


        //    // Act 
        //    ActionResult ar1 = controller1.PostEdit(vm);
        //    ViewResult view1 = controller2.Index();
        //    ListVM<Recipe, RecipeVM> listVM = (ListVM<Recipe, RecipeVM>)view1.Model;
        //    var result = (from m in listVM.Entities
        //                  where m.Name == "0000 test"
        //                  select m).AsQueryable();

        //    Recipe item = result.FirstOrDefault();
        //    RecipeVM recipe = Mapper.Map<Recipe, RecipeVM>(item);

        //    try
        //    {
        //        // verify initial value:
        //        Assert.AreEqual("SaveEditedRecipeWithDescriptionChange Pre-test", recipe.Description);
        //    }
        //    catch (Exception)
        //    {
        //        // clean up 
        //        controller5.DeleteConfirmed(vm.ID);
        //        throw;
        //    }
        //    // now edit it
        //    vm.ID = recipe.ID;
        //    vm.Name = "0000 test Edited";
        //    vm.Description = "SaveEditedRecipeWithDescriptionChange Post-test";

        //    ActionResult ar2 = controller3.PostEdit(vm);
        //    ViewResult view2 = controller4.Index();
        //    ListVM<Recipe, RecipeVM> listVM2 = (ListVM<Recipe, RecipeVM>)view2.Model;
        //    var result2 = (from m in listVM2.Entities
        //                   where m.Name == "0000 test Edited"
        //                   select m).AsQueryable();
        //    Recipe item2 = result2.FirstOrDefault();
        //    RecipeVM recipe2 = Mapper.Map<Recipe, RecipeVM>(item);
        //    try
        //    {
        //        // Assert
        //        Assert.AreEqual("0000 test Edited", recipe2.Name);
        //        Assert.AreEqual("SaveEditedRecipeWithDescriptionChange Post-test", recipe2.Description);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        // clean up 
        //        controller5.DeleteConfirmed(vm.ID);
        //    }
        //}

        //[TestMethod]
        //[TestCategory("Edit")]
        //public void SaveTheCreationDateOnRecipeCreationWithNoParameterCtor()
        //{
        //    // Arrange
        //    DateTime CreationDate = DateTime.Now;

        //    // Act
        //    Recipe ingredient = new Recipe();

        //    // Assert
        //    Assert.AreEqual(CreationDate.Date, ingredient.CreationDate.Date);
        //}


        //[TestMethod]
        //[TestCategory("Edit")]
        //public void SaveTheCreationDateOnRecipeCreationWithDateTimeParameter()
        //{
        //    // Arrange
        //    DateTime CreationDate = new DateTime(2010, 1, 1);

        //    // Act
        //    Recipe ingredient = new Recipe(CreationDate);

        //    // Assert
        //    Assert.AreEqual(CreationDate, ingredient.CreationDate);
        //}

        //[TestMethod]
        //[TestCategory("Edit")]
        //public void SaveTheCreationDateOnRecipeVMCreationWithNoParameterCtor()
        //{
        //    // Arrange
        //    DateTime CreationDate = DateTime.Now;

        //    // Act
        //    RecipeVM ingredientVM = new RecipeVM();

        //    // Assert
        //    Assert.AreEqual(CreationDate.Date, ingredientVM.CreationDate.Date);
        //}
        //[TestMethod]
        //[TestCategory("Edit")]
        //public void SaveTheCreationDateOnRecipeVMCreationWithDateTimeParameter()
        //{
        //    // Arrange
        //    DateTime CreationDate = new DateTime(2010, 1, 1);

        //    // Act
        //    RecipeVM ingredientVM = new RecipeVM(CreationDate);

        //    // Assert
        //    Assert.AreEqual(CreationDate, ingredientVM.CreationDate);
        //}

        //[TestMethod]
        //[TestCategory("Edit")]
        //public void SaveTheCreationDateBetweenPostedEdits()
        //{
        //    // Arrange
        //    DateTime CreationDate = new DateTime(2010, 1, 1);
        //    RecipeVM recipeVM = new RecipeVM(CreationDate);
        //    recipeVM.Name = "001 Test ";

        //    JSONRepository<Recipe, RecipeVM> repoRecipe = new JSONRepository<Recipe, RecipeVM>(); ;
        //    RecipesController controllerEdit = new RecipesController(repoRecipe);
        //    RecipesController controllerView = new RecipesController(repoRecipe);
        //    RecipesController controllerDelete = new RecipesController(repoRecipe);

        //    // Act
        //    controllerEdit.PostEdit(recipeVM);
        //    ViewResult view = controllerView.Index();
        //    ListVM<Recipe, RecipeVM> listVM = (ListVM<Recipe, RecipeVM>)view.Model;
        //    var result = (from m in listVM.Entities
        //                  where m.Name == "001 Test "
        //                  select m).AsQueryable();
        //    Recipe item = result.FirstOrDefault();
        //    RecipeVM recipe = Mapper.Map<Recipe, RecipeVM>(item);


        //    DateTime shouldBeSameDate = recipe.CreationDate;
        //    try
        //    {
        //        // Assert
        //        Assert.AreEqual(CreationDate, shouldBeSameDate);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        // Cleanup
        //        controllerDelete.DeleteConfirmed(recipe.ID);
        //    }
        //}


        //[TestMethod]
        //[TestCategory("Edit")]
        //public void UpdateTheModificationDateBetweenPostedEdits()
        //{
        //    // Arrange
        //    JSONRepository<Recipe, RecipeVM> repoRecipe = new JSONRepository<Recipe, RecipeVM>();
        //    RecipesController controllerPost = new RecipesController(repoRecipe);
        //    RecipesController controllerPost1 = new RecipesController(repoRecipe);
        //    RecipesController controllerView = new RecipesController(repoRecipe);
        //    RecipesController controllerView1 = new RecipesController(repoRecipe);
        //    RecipesController controllerDelete = new RecipesController(repoRecipe);

        //    RecipeVM vm = new RecipeVM();
        //    vm.Name = "002 Test Mod";
        //    DateTime CreationDate = vm.CreationDate;
        //    DateTime mod = vm.ModifiedDate;

        //    // Act
        //    controllerPost.PostEdit(vm);

        //    ViewResult view = controllerView.Index();
        //    ListVM<Recipe, RecipeVM> listVM = (ListVM<Recipe, RecipeVM>)view.Model;
        //    var result = (from m in listVM.Entities
        //                  where m.Name == "002 Test Mod"
        //                  select m).AsQueryable();

        //    Recipe item = result.FirstOrDefault();
        //    RecipeVM vm2 = Mapper.Map<Recipe, RecipeVM>(item);

        //    vm.Description = "I've been edited to delay a bit";

        //    controllerPost1.PostEdit(vm2);


        //    ViewResult view1 = controllerView.Index();
        //    listVM = (ListVM<Recipe, RecipeVM>)view1.Model;
        //    var result1 = (from m in listVM.Entities
        //                   where m.Name == "002 Test Mod"
        //                   select m).AsQueryable();


        //    Recipe item3 = result1.FirstOrDefault();
        //    RecipeVM vm3 = Mapper.Map<Recipe, RecipeVM>(item3);
        //    DateTime shouldBeSameDate = vm3.CreationDate;
        //    DateTime shouldBeLaterDate = vm3.ModifiedDate;
        //    try
        //    {
        //        // Assert
        //        Assert.AreEqual(CreationDate, shouldBeSameDate);
        //        Assert.AreNotEqual(mod, shouldBeLaterDate);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        // Cleanup
        //        controllerDelete.DeleteConfirmed(vm3.ID);
        //    }
        //}
        //[TestMethod]
        //public void SaveAllPropertiesInBaseEntity()
        //{
        //    // Arrange
        //    JSONRepository<Recipe, RecipeVM> repoRecipe = new JSONRepository<Recipe, RecipeVM>();
        //    RecipesController controllerPost = new RecipesController(repoRecipe);
        //    RecipesController controllerView = new RecipesController(repoRecipe);
        //    RecipesController controllerDelete = new RecipesController(repoRecipe);
        //    RecipeVM vm = new RecipeVM();
        //    vm.Name = "___test387";
        //    vm.Description = "test387 description";

        //    // Act
        //    controllerPost.PostEdit(vm);
        //    ViewResult view1 = controllerView.Index();
        //    ListVM<Recipe, RecipeVM> listVM = (ListVM<Recipe, RecipeVM>)view1.Model;
        //    var result1 = (from m in listVM.Entities
        //                   where m.Name == "___test387"
        //                   select m).AsQueryable();

        //    Recipe item = result1.FirstOrDefault();
        //    RecipeVM recipe = Mapper.Map<Recipe, RecipeVM>(item);

        //    try
        //    {
        //        Assert.AreEqual(vm.Name, recipe.Name);
        //        Assert.AreEqual(vm.Description, recipe.Description);
        //        Assert.AreEqual(vm.CreationDate.Day, recipe.CreationDate.Day);
        //        Assert.AreEqual(vm.ModifiedDate.Day, recipe.ModifiedDate.Day);
        //        Assert.AreEqual(vm.AddedByUser, recipe.AddedByUser);
        //        Assert.AreEqual(vm.ModifiedByUser, recipe.ModifiedByUser);

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    finally
        //    {
        //        controllerDelete.DeleteConfirmed(recipe.ID);
        //    }
        //}

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void AttachAnExistingIngredientToAnExistingRecipe()
        //{
        //    // Arrange

        //    JSONRepository<Recipe, RecipeVM> repoRecipe = new JSONRepository<Recipe, RecipeVM>();
        //    JSONRepository<Ingredient, IngredientVM> repoIngredient = new JSONRepository<Ingredient, IngredientVM>();
        //    RecipesController controller = new RecipesController(repoRecipe);

        //    Recipe recipe = GetRecipe(repoRecipe, "test AttachAnExistingIngredientToAnExistingRecipe");
        //    Ingredient ingredient = GetIngredient(repoIngredient, "test AttachAnExistingIngredientToAnExistingRecipe");

        //    // Act
        //    controller.AttachIngredient(recipe.ID, ingredient.ID);
        //    Recipe returnedRecipe = (from m in repoRecipe.GetAllT()
        //                             where m.Description == recipe.Description
        //                             select m).FirstOrDefault();
        //    // Assert 
        //    Assert.AreEqual(1, returnedRecipe.Ingredients.Count());
        //    // how do I know the correct ingredient was added?
        //    Assert.AreEqual(ingredient.ID, returnedRecipe.Ingredients.First().ID);


        //    // Cleanup
        //     IngredientsController<Ingredient, IngredientVM> controllerCleanupIngredient = new  IngredientsController<Ingredient, IngredientVM>(repoIngredient);
        //    RecipesController controllerCleanupRecipe = new RecipesController(repoRecipe);

        //    RecipeVM recipeVM = Mapper.Map<Recipe, RecipeVM>(recipe);
        //    IngredientVM ingredientVM = Mapper.Map<Ingredient, IngredientVM>(ingredient);

        //    controllerCleanupRecipe.DeleteConfirmed(recipeVM.ID);
        //    controllerCleanupIngredient.DeleteConfirmed(ingredientVM.ID);
        //}

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void NotDeleteAnIngredientAfterIngredientIsDetachedFromRecipe()
        //{
        //    // Arrange
        //    JSONRepository<Recipe, RecipeVM> repoRecipe = new JSONRepository<Recipe, RecipeVM>();
        //    JSONRepository<Ingredient, IngredientVM> repoIngredient = new JSONRepository<Ingredient, IngredientVM>();
        //    RecipesController controller = new RecipesController(repoRecipe);
        //    RecipesController controllerSubtract = new RecipesController(repoRecipe);

        //    Recipe recipe1 = GetRecipe(repoRecipe, "test NotDeleteAnIngredientAfterIngredientIsDetached");


        //    Ingredient ingredient2 = GetIngredient(repoIngredient, "test NotDeleteAnIngredientAfterIngredientIsDetached");
        //    controller.AttachIngredient(recipe1.ID, ingredient2.ID);
        //    // Act
        //    controllerSubtract.RemoveIngredient(recipe1.ID, ingredient2.ID);


        //    // Assert
        //    Assert.IsNotNull(ingredient2);

        //    // Cleanup
        //    RecipesController controllerCleanupRecipe = new RecipesController(repoRecipe);
        //     IngredientsController<Ingredient, IngredientVM> controllerCleanupIngredient = new  IngredientsController<Ingredient, IngredientVM>(repoIngredient);
        //    controllerCleanupRecipe.DeleteConfirmed(recipe1.ID);
        //    controllerCleanupIngredient.DeleteConfirmed(ingredient2.ID);
        //}

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void ReturnIndexViewWithWarningWhenAttachingExistIngredientToNonExistingRecipe()
        //{
        //    // Arrange
        //    JSONRepository<Recipe, RecipeVM> repoRecipe = new JSONRepository<Recipe, RecipeVM>();
        //    JSONRepository<Ingredient, IngredientVM> repoIngredient = new JSONRepository<Ingredient, IngredientVM>();
        //    RecipesController controllerAttach = new RecipesController(repoRecipe);
        //    RecipesController controllerSubtract = new RecipesController(repoRecipe);

        //    string description = "test ReturnIndexViewWhenAttachingExistIngredientToNonExistingRecipe";

        //    Ingredient ingredient = GetIngredient(repoIngredient, description);

        //    // Act
        //    AlertDecoratorResult adr = (AlertDecoratorResult)controllerAttach.AttachIngredient(-1, ingredient.ID);
        //    RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
        //    var routeValues = rtrr.RouteValues.Values;

        //    // Assert
        //    try
        //    {
        //        Assert.AreEqual("alert-warning", adr.AlertClass);
        //        Assert.AreEqual("Recipe was not found", adr.Message);
        //        Assert.AreEqual(1, routeValues.Count);
        //        Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        // Cleanup 
        //         IngredientsController<Ingredient, IngredientVM> controllerCleanupIngredient = new  IngredientsController<Ingredient, IngredientVM>(repoIngredient);
        //        controllerCleanupIngredient.DeleteConfirmed(ingredient.ID);
        //    }
        //}

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void ReturnRecipeEditViewWithWarningMessageWhenAttachingNonExistingIngredientToExistingRrecipe()
        //{
        //    // Arrange
        //    JSONRepository<Recipe, RecipeVM> repoRecipe = new JSONRepository<Recipe, RecipeVM>();
        //    RecipesController controller = new RecipesController(repoRecipe);
        //    Recipe recipe = GetRecipe(repoRecipe, "test ReturnRecipeEditViewWithErrorMessageWhenAttachingNonExistingIngredientToExistingRrecipe");

        //    // Act  
        //    AlertDecoratorResult adr = (AlertDecoratorResult)controller.AttachIngredient(recipe.ID, -1);
        //    RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
        //    var routeValues = rtrr.RouteValues.Values;

        //    // Assert
        //    try
        //    {
        //        Assert.AreEqual("alert-warning", adr.AlertClass);
        //        Assert.AreEqual("Please choose a(n) Ingredient", adr.Message);
        //        Assert.AreEqual(3, routeValues.Count);
        //        Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(2).ToString());
        //        Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(1).ToString());
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        // Cleanup 
        //        RecipesController controllerCleanupRecipe = new RecipesController(repoRecipe);
        //        controllerCleanupRecipe.DeleteConfirmed(recipe.ID);
        //    }
        //}

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void ReturnIndexViewWithWarningWhenAttachingNonExistIngredientToNonExistingRecipe()
        //{
        //    // Arrange
        //    JSONRepository<Recipe, RecipeVM> repoRecipe = new JSONRepository<Recipe, RecipeVM>();
        //    RecipesController controller = new RecipesController(repoRecipe);

        //    // Act 
        //    AlertDecoratorResult adr = (AlertDecoratorResult)controller.AttachIngredient(-1, -1);
        //    RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
        //    var routeValues = rtrr.RouteValues.Values;

        //    // Assert
        //    Assert.AreEqual("alert-warning", adr.AlertClass);
        //    Assert.AreEqual("Recipe was not found", adr.Message);
        //    Assert.AreEqual(1, routeValues.Count);
        //    Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());

        //    // Clean up not needed

        //}

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void ReturnRecipeEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingRecipe()
        //{
        //    // Arrange
        //    JSONRepository<Recipe, RecipeVM> repoRecipe = new JSONRepository<Recipe, RecipeVM>();
        //    JSONRepository<Ingredient, IngredientVM> repoIngredient = new JSONRepository<Ingredient, IngredientVM>();

        //    RecipesController controllerAttachIngredient = new RecipesController(repoRecipe);
        //    RecipesController controllerRemoveIngredient = new RecipesController(repoRecipe);

        //    Recipe recipe = GetRecipe(repoRecipe, "test ReturnRecipeEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingRecipe");

        //    Ingredient ingredient = GetIngredient(repoIngredient, "test ReturnRecipeEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingRecipe");
        //    controllerAttachIngredient.AttachIngredient(recipe.ID, ingredient.ID);

        //    // Act          
        //    AlertDecoratorResult adr = (AlertDecoratorResult)controllerRemoveIngredient.RemoveIngredient(recipe.ID, ingredient.ID);
        //    RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
        //    var routeValues = rtrr.RouteValues.Values;

        //    // Assert
        //    try
        //    {
        //        Assert.AreEqual("alert-success", adr.AlertClass);
        //        Assert.AreEqual("Ingredient was Successfully Detached!", adr.Message);
        //        Assert.AreEqual(3, routeValues.Count);
        //        Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(1).ToString());
        //        Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(2).ToString());
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        // Cleanup 
        //        RecipesController controllerCleanupRecipe = new RecipesController(repoRecipe);
        //        controllerCleanupRecipe.DeleteConfirmed(recipe.ID);
        //         IngredientsController<Ingredient, IngredientVM> controllerCleanupIngredient = new  IngredientsController<Ingredient, IngredientVM>(repoIngredient);
        //        controllerCleanupIngredient.DeleteConfirmed(ingredient.ID);
        //    }
        //}

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNonExistingRecipe()
        //{
        //    // Arrange
        //    JSONRepository<Recipe, RecipeVM> repoRecipe = new JSONRepository<Recipe, RecipeVM>();
        //    JSONRepository<Ingredient, IngredientVM> repoIngredient = new JSONRepository<Ingredient, IngredientVM>();
        //    RecipesController controllerDetachIngredient = new RecipesController(repoRecipe);
        //    string description = "test ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNoExistingRecipe";

        //    Ingredient ingredient = GetIngredient(repoIngredient, description);
        //    // Act  
        //    AlertDecoratorResult adr = (AlertDecoratorResult)controllerDetachIngredient.RemoveIngredient(-1, ingredient.ID);
        //    RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
        //    var routeValues = rtrr.RouteValues.Values;

        //    // Assert
        //    try
        //    {
        //        Assert.AreEqual("alert-warning", adr.AlertClass);
        //        Assert.AreEqual("Recipe was not found", adr.Message);
        //        Assert.AreEqual(1, routeValues.Count);
        //        Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        // Cleanup 

        //         IngredientsController<Ingredient, IngredientVM> controllerCleanupIngredient = new  IngredientsController<Ingredient, IngredientVM>(repoIngredient);
        //        controllerCleanupIngredient.DeleteConfirmed(ingredient.ID);
        //    }
        //}

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void ReturnRecipeIndexViewWithWarningWhenDetachingExistingingredientNotAttachedToAnExistingRecipe()
        //{
        //    // Arrange
        //    JSONRepository<Recipe, RecipeVM> repoRecipe = new JSONRepository<Recipe, RecipeVM>();
        //    JSONRepository<Ingredient, IngredientVM> repoIngredient = new JSONRepository<Ingredient, IngredientVM>();
        //     IngredientsController<Ingredient, IngredientVM> controllerIngredient = new  IngredientsController<Ingredient, IngredientVM>(repoIngredient);
        //    RecipesController controllerDetachIngredient = new RecipesController(repoRecipe);
        //    string description = "test ReturnRecipeIndexViewWithWarningWhenDetachingExistingingredientNotAttachedToAnExistingRecipe";
        //    Ingredient ingredient = GetIngredient(repoIngredient, description);
        //    // Act 
        //    AlertDecoratorResult adr = (AlertDecoratorResult)controllerDetachIngredient.RemoveIngredient(-1, ingredient.ID);
        //    RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
        //    var routeValues = rtrr.RouteValues.Values;

        //    // Assert
        //    try
        //    {
        //        Assert.AreEqual("alert-warning", adr.AlertClass);
        //        Assert.AreEqual("Recipe was not found", adr.Message);
        //        Assert.AreEqual(1, routeValues.Count);
        //        Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        // Cleanup 

        //         IngredientsController<Ingredient, IngredientVM> controllerCleanupIngredient = new  IngredientsController<Ingredient, IngredientVM>(repoIngredient);
        //        controllerCleanupIngredient.DeleteConfirmed(ingredient.ID);
        //    }
        //}

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void ReturnRecipeEditViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToExistingRecipe()
        //{
        //    // Arrange
        //    JSONRepository<Recipe, RecipeVM> repoRecipe = new JSONRepository<Recipe, RecipeVM>();
        //    RecipesController controllerRecipe = new RecipesController(repoRecipe);
        //    RecipesController controllerDetachIngredient = new RecipesController(repoRecipe);
        //    Recipe recipe = GetRecipe(repoRecipe, "test ReturnRecipeEditViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToExistingRecipe");
        //    // Act 
        //    AlertDecoratorResult adr = (AlertDecoratorResult)controllerDetachIngredient.RemoveIngredient(recipe.ID, -1);
        //    RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
        //    var routeValues = rtrr.RouteValues.Values;

        //    // Assert
        //    try
        //    {
        //        Assert.AreEqual("alert-warning", adr.AlertClass);
        //        Assert.AreEqual("Please choose a(n) Ingredient", adr.Message);
        //        Assert.AreEqual(3, routeValues.Count);
        //        Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(2).ToString());
        //        Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(1).ToString());
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        // Cleanup 

        //        RecipesController controllerCleanup = new RecipesController(repoRecipe);
        //        controllerCleanup.DeleteConfirmed(recipe.ID);
        //    }
        //}

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void ReturnIndexViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToANonExistingRecipe()
        //{
        //    // Arrange
        //    JSONRepository<Recipe, RecipeVM> repoRecipe = new JSONRepository<Recipe, RecipeVM>();
        //    RecipesController controller = new RecipesController(repoRecipe);

        //    // Act 
        //    AlertDecoratorResult adr = (AlertDecoratorResult)controller.RemoveIngredient(-1, -1);

        //    RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
        //    var routeValues = rtrr.RouteValues.Values;

        //    // Assert
        //    try
        //    {
        //        Assert.AreEqual("alert-warning", adr.AlertClass);
        //        Assert.AreEqual("Recipe was not found", adr.Message);
        //        Assert.AreEqual(1, routeValues.Count);
        //        Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        // Cleanup not needed - this is kept for the sake of completeness
        //    }
        //}

        //internal Ingredient GetIngredient(IRepository<Ingredient, IngredientVM> repo, string description)
        //{
        //     IngredientsController<Ingredient, IngredientVM> controller = new  IngredientsController<Ingredient, IngredientVM>(repo);
        //    IngredientVM ivm = new IngredientVM();
        //    ivm.Description = description;
        //    ivm.ID = int.MaxValue;
        //    controller.PostEdit(ivm);

        //    Ingredient ingredient = ((from m in repo.GetAllT()
        //                              where m.Description == description
        //                              select m).AsQueryable()).FirstOrDefault();
        //    return ingredient;
        //}


        //internal Recipe GetRecipe(IRepository<Recipe, RecipeVM> repoRecipe, string description)
        //{
        //    RecipesController controller = new RecipesController(repoRecipe);
        //    RecipeVM vm = new RecipeVM();
        //    vm.Description = description;
        //    vm.ID = int.MaxValue;
        //    controller.PostEdit(vm);

        //    Recipe recipe = ((from m in repoRecipe.GetAllT()
        //                      where m.Description == description
        //                      select m).AsQueryable()).FirstOrDefault();
        //    return recipe;
        //}



    }
}

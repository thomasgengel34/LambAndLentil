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

    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("RecipesController")]
    public class RecipesControllerShould
    {
        static IRepository<RecipeVM> repo;
        static RecipesController controller;
        static RecipeVM recipeVM;

        public RecipesControllerShould()
        {
            repo = new TestRepository<RecipeVM>();
            controller = new RecipesController(repo);
            recipeVM = new RecipeVM();
        }


        [TestMethod]
        public void CreateARecipe()
        {
            // Arrange 

            // Act
            ViewResult vr = controller.Create(LambAndLentil.UI.UIViewType.Create);
            RecipeVM vm = (RecipeVM)vr.Model;
            string modelName = vm.Name;

            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created");
        }

        [Ignore]
        [TestMethod]
        public void SaveAValidRecipe()
        {
            // Arrange

            RecipeVM vm = new RecipeVM();
            vm.Name = "test";
            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit(vm);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;


            // Assert 

            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(4, routeValues.Count);
            Assert.AreEqual(UIControllerType.Recipes.ToString(), routeValues.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(1).ToString());
            Assert.AreEqual("Recipes", routeValues.ElementAt(2).ToString());
            Assert.AreEqual(1.ToString(), routeValues.ElementAt(3).ToString());

        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedRecipeWithNameChange()
        {
            // Arrange 
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
            ListVM<RecipeVM> listVM = (ListVM<RecipeVM>)view1.Model;
            RecipeVM recipeVM = (from m in listVM.ListT
                                 where m.Name == "0000 test"
                                 select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("0000 test", recipeVM.Name);

            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = recipeVM.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM<RecipeVM> listVM2 = (ListVM<RecipeVM>)view2.Model;
            RecipeVM recipe2 = (from m in listVM2.ListT
                                where m.Name == "0000 test Edited"
                                select m).AsQueryable().First();

            // Assert
            Assert.AreEqual("0000 test Edited", recipe2.Name);
        }


        [Ignore]
        [TestMethod]
        [TestCategory("DeleteConfirmed")]
        public void ActuallyDeleteARecipeFromTheDatabase()
        {
            // Arrange

            recipeVM.Description = "test ActuallyDeleteARecipeFromTheDatabase";
            //Act
            controller.DeleteConfirmed(recipeVM.ID);
            var deletedItem = (from m in repo.GetAll()
                               where m.Description == recipeVM.Description
                               select m).AsQueryable();

            //Assert
            Assert.AreEqual(0, deletedItem.Count());
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedRecipeWithDescriptionChange()
        {
            // Arrange 
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
            ListVM<RecipeVM> listVM = (ListVM<RecipeVM>)view1.Model;
            RecipeVM recipeVM = (from m in listVM.ListT
                                 where m.Name == "0000 test"
                                 select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("SaveEditedRecipeWithDescriptionChange Pre-test", recipeVM.Description);

            // now edit it
            vm.ID = recipeVM.ID;
            vm.Name = "0000 test Edited";
            vm.Description = "SaveEditedRecipeWithDescriptionChange Post-test";

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM<RecipeVM> listVM2 = (ListVM<RecipeVM>)view2.Model;
            RecipeVM recipe2 = (from m in listVM2.ListT
                                where m.Name == "0000 test Edited"
                                select m).AsQueryable().FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited", recipe2.Name);
            Assert.AreEqual("SaveEditedRecipeWithDescriptionChange Post-test", recipe2.Description);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnRecipeCreationWithNoParameterCtor()
        {
            // Arrange
            DateTime CreationDate = DateTime.Now;

            // Act
            Recipe recipe = new Recipe();

            // Assert
            Assert.AreEqual(CreationDate.Date, recipe.CreationDate.Date);
        }


        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnRecipeCreationWithDateTimeParameter()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);

            // Act
            Recipe recipe = new Recipe(CreationDate);

            // Assert
            Assert.AreEqual(CreationDate, recipe.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnRecipeVMCreationWithNoParameterCtor()
        {
            // Arrange
            DateTime CreationDate = DateTime.Now;

            // Act
            RecipeVM recipeVM = new RecipeVM();

            // Assert
            Assert.AreEqual(CreationDate.Date, recipeVM.CreationDate.Date);
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

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);
            RecipeVM recipeVM = new RecipeVM(CreationDate);
            recipeVM.Name = "001 Test ";

            TestRepository<RecipeVM> repoRecipe = new TestRepository<RecipeVM>(); ;
            RecipesController controllerEdit = new RecipesController(repoRecipe);
            RecipesController controllerView = new RecipesController(repoRecipe);
            RecipesController controllerDelete = new RecipesController(repoRecipe);

            // Act
            controllerEdit.PostEdit(recipeVM);
            ViewResult view = controllerView.Index();
            ListVM<RecipeVM> listVM = (ListVM<RecipeVM>)view.Model;
            recipeVM = (from m in listVM.ListT
                        where m.Name == "001 Test "
                        select m).AsQueryable().FirstOrDefault();

            DateTime shouldBeSameDate = recipeVM.CreationDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);

        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            // Arrange 
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
            ListVM<RecipeVM> listVM = (ListVM<RecipeVM>)view.Model;
            RecipeVM vm2 = (from m in listVM.ListT
                            where m.Name == "002 Test Mod"
                            select m).AsQueryable().FirstOrDefault();


            vm.Description = "I've been edited to delay a bit";

            controllerPost1.PostEdit(vm2);


            ViewResult view1 = controllerView.Index();
            listVM = (ListVM<RecipeVM>)view1.Model;
            RecipeVM vm3 = (from m in listVM.ListT
                            where m.Name == "002 Test Mod"
                            select m).AsQueryable().FirstOrDefault();

            DateTime shouldBeSameDate = vm3.CreationDate;
            DateTime shouldBeLaterDate = vm3.ModifiedDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);
            Assert.AreNotEqual(mod, shouldBeLaterDate);

        }

        [Ignore]
        [TestMethod]
        public void SaveAllPropertiesInBaseEntity()
        {
            // Arrange 
            RecipesController controllerPost = new RecipesController(repo);
            RecipesController controllerView = new RecipesController(repo);
            RecipesController controllerDelete = new RecipesController(repo);
            RecipeVM vm = new RecipeVM();
            vm.Name = "___test387";
            vm.Description = "test387 description";

            // Act
            controllerPost.PostEdit(vm);
            ViewResult view1 = controllerView.Index();
            ListVM<RecipeVM> listVM = (ListVM<RecipeVM>)view1.Model;
            RecipeVM recipeVM = (from m in listVM.ListT
                                 where m.Name == "___test387"
                                 select m).AsQueryable().FirstOrDefault();

            Assert.AreEqual(vm.Name, recipeVM.Name);
            Assert.AreEqual(vm.Description, recipeVM.Description);
            Assert.AreEqual(vm.CreationDate.Day, recipeVM.CreationDate.Day);
            Assert.AreEqual(vm.ModifiedDate.Day, recipeVM.ModifiedDate.Day);
            Assert.AreEqual(vm.AddedByUser, recipeVM.AddedByUser);
            Assert.AreEqual(vm.ModifiedByUser, recipeVM.ModifiedByUser);

        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void AttachAnExistingIngredientToAnExistingRecipe()
        {
            // Arrange

            TestRepository<RecipeVM> repoRecipe = new TestRepository<RecipeVM>();
            TestRepository<IngredientVM> repoIngredient = new TestRepository<IngredientVM>();
            RecipesController controller = new RecipesController(repoRecipe);

            RecipeVM recipeVM = GetRecipeVM(repoRecipe, "test AttachAnExistingIngredientToAnExistingRecipe");
            IngredientVM ingredientVM = GetIngredientVM(repoIngredient, "test AttachAnExistingIngredientToAnExistingRecipe");

            // Act
            controller.AttachIngredient(recipeVM.ID, ingredientVM.ID);
            RecipeVM returnedRecipeVM = (from m in repoRecipe.GetAll()
                                         where m.Description == recipeVM.Description
                                         select m).FirstOrDefault();
            // Assert 
            Assert.AreEqual(1, returnedRecipeVM.Ingredients.Count());
            // how do I know the correct ingredient was added?
            Assert.AreEqual(ingredientVM.ID, returnedRecipeVM.Ingredients.First().ID);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void NotDeleteAnIngredientAfterIngredientIsDetachedFromRecipe()
        {
            // Arrange
            TestRepository<RecipeVM> repoRecipe = new TestRepository<RecipeVM>();
            TestRepository<IngredientVM> repoIngredient = new TestRepository<IngredientVM>();
            RecipesController controller = new RecipesController(repoRecipe);
            RecipesController controllerSubtract = new RecipesController(repoRecipe);

            RecipeVM recipeVM1 = GetRecipeVM(repoRecipe, "test NotDeleteAnIngredientAfterIngredientIsDetached");


            IngredientVM ingredientVM2 = GetIngredientVM(repoIngredient, "test NotDeleteAnIngredientAfterIngredientIsDetached");
            controller.AttachIngredient(recipeVM1.ID, ingredientVM2.ID);
            // Act
            controllerSubtract.RemoveIngredient(recipeVM1.ID, ingredientVM2.ID);


            // Assert
            Assert.IsNotNull(ingredientVM2);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingExistIngredientToNonExistingRecipe()
        {
            // Arrange
            TestRepository<RecipeVM> repoRecipe = new TestRepository<RecipeVM>();
            TestRepository<IngredientVM> repoIngredient = new TestRepository<IngredientVM>();
            RecipesController controllerAttach = new RecipesController(repoRecipe);
            RecipesController controllerSubtract = new RecipesController(repoRecipe);

            string description = "test ReturnIndexViewWhenAttachingExistIngredientToNonExistingRecipe";

            IngredientVM ingredient = GetIngredientVM(repoIngredient, description);

            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)controllerAttach.AttachIngredient(-1, ingredient.ID);
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
        public void ReturnRecipeEditViewWithWarningMessageWhenAttachingNonExistingIngredientToExistingRrecipe()
        {
            // Arrange

            RecipeVM recipeVM = GetRecipeVM(repo, "test ReturnRecipeEditViewWithErrorMessageWhenAttachingNonExistingIngredientToExistingRrecipe");

            // Act  
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.AttachIngredient(recipeVM.ID, -1);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert 
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Please choose a(n) Ingredient", adr.Message);
            Assert.AreEqual(3, routeValues.Count);
            Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(2).ToString());
            Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(1).ToString());
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingNonExistIngredientToNonExistingRecipe()
        {
            // Arrange
            TestRepository<RecipeVM> repoRecipe = new TestRepository<RecipeVM>();
            RecipesController controller = new RecipesController(repoRecipe);

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.AttachIngredient(-1, -1);
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

            TestRepository<IngredientVM> repoIngredient = new TestRepository<IngredientVM>();

            RecipesController controllerAttachIngredient = new RecipesController(repo);
            RecipesController controllerRemoveIngredient = new RecipesController(repo);

            RecipeVM recipeVM = GetRecipeVM(repo, "test ReturnRecipeEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingRecipe");

            IngredientVM ingredientVM = GetIngredientVM(repoIngredient, "test ReturnRecipeEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingRecipe");
            controllerAttachIngredient.AttachIngredient(recipeVM.ID, ingredientVM.ID);

            // Act          
            AlertDecoratorResult adr = (AlertDecoratorResult)controllerRemoveIngredient.RemoveIngredient(recipeVM.ID, ingredientVM.ID);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert 
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("Ingredient was Successfully Detached!", adr.Message);
            Assert.AreEqual(3, routeValues.Count);
            Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(1).ToString());
            Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(2).ToString());
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNonExistingRecipe()
        {
            // Arrange
            TestRepository<RecipeVM> repoRecipe = new TestRepository<RecipeVM>();
            TestRepository<IngredientVM> repoIngredient = new TestRepository<IngredientVM>();
            RecipesController controllerDetachIngredient = new RecipesController(repoRecipe);
            string description = "test ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNoExistingRecipe";

            IngredientVM ingredientVM = GetIngredientVM(repoIngredient, description);
            // Act  
            AlertDecoratorResult adr = (AlertDecoratorResult)controllerDetachIngredient.RemoveIngredient(-1, ingredientVM.ID);
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
        public void ReturnRecipeIndexViewWithWarningWhenDetachingExistingingredientNotAttachedToAnExistingRecipe()
        {
            // Arrange
            
            TestRepository<IngredientVM> repoIngredient = new TestRepository<IngredientVM>();
            IngredientsController  controllerIngredient = new IngredientsController(repoIngredient);
            RecipesController controllerDetachIngredient = new RecipesController(repo);
            string description = "test ReturnRecipeIndexViewWithWarningWhenDetachingExistingingredientNotAttachedToAnExistingRecipe";
            IngredientVM ingredientVM = GetIngredientVM(repoIngredient, description);
            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)controllerDetachIngredient.RemoveIngredient(-1, ingredientVM.ID);
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
        public void ReturnRecipeEditViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToExistingRecipe()
        {
            // Arrange
            TestRepository<RecipeVM> repoRecipe = new TestRepository<RecipeVM>();
            RecipesController controllerRecipe = new RecipesController(repoRecipe);
            RecipesController controllerDetachIngredient = new RecipesController(repoRecipe);
            RecipeVM recipeVM = GetRecipeVM(repoRecipe, "test ReturnRecipeEditViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToExistingRecipe");
            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)controllerDetachIngredient.RemoveIngredient(recipeVM.ID, -1);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert 
                Assert.AreEqual("alert-warning", adr.AlertClass);
                Assert.AreEqual("Please choose a(n) Ingredient", adr.Message);
                Assert.AreEqual(3, routeValues.Count);
                Assert.AreEqual(UIViewType.Details.ToString(), routeValues.ElementAt(2).ToString());
                Assert.AreEqual(UIViewType.Edit.ToString(), routeValues.ElementAt(1).ToString()); 
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToANonExistingRecipe()
        {
            // Arrange 

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.RemoveIngredient(-1, -1);

            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert
            
                Assert.AreEqual("alert-warning", adr.AlertClass);
                Assert.AreEqual("Recipe was not found", adr.Message);
                Assert.AreEqual(1, routeValues.Count);
                Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
            
        }

        internal IngredientVM GetIngredientVM(IRepository<IngredientVM> repo, string description)
        {
            IngredientsController controller = new IngredientsController(repo);
            IngredientVM ivm = new IngredientVM();
            ivm.Description = description;
            ivm.ID = int.MaxValue;
            controller.PostEdit(ivm);

            IngredientVM ingredientVM = ((from m in repo.GetAll()
                                          where m.Description == description
                                          select m).AsQueryable()).FirstOrDefault();
            return ingredientVM;
        }


        internal RecipeVM GetRecipeVM(IRepository<RecipeVM> repo, string description)
        {
            RecipesController controller = new RecipesController(repo);
            RecipeVM vm = new RecipeVM();
            vm.Description = description;
            vm.ID = int.MaxValue;
            controller.PostEdit(vm);

            RecipeVM recipeVM = ((from m in repo.GetAll()
                                  where m.Description == description
                                  select m).AsQueryable()).FirstOrDefault();
            return recipeVM;
        }



    }
}

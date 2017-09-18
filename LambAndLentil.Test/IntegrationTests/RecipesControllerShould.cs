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

namespace IntegrationTests
{

    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("RecipesController")]
    public class RecipesControllerShould
    {
        static IRepository<Recipe> Repo;
        static RecipesController controller;
        static Recipe recipeVM;

        public RecipesControllerShould()
        {
            Repo = new TestRepository<Recipe>();
            controller = new RecipesController(Repo);
            recipeVM = new Recipe();
        }


        [TestMethod]
        public void CreateARecipe()
        {
            // Arrange 

            // Act
            ViewResult vr = controller.Create(LambAndLentil.UI.UIViewType.Create);
            Recipe vm = (Recipe)vr.Model;
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

            Recipe vm = new Recipe
            {
                Name = "test"
            };
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
            RecipesController controller1 = new RecipesController(Repo);
            RecipesController controller2 = new RecipesController(Repo);
            RecipesController controller3 = new RecipesController(Repo);
            RecipesController controller4 = new RecipesController(Repo);
            RecipesController controller5 = new RecipesController(Repo);
            Recipe vm = new Recipe
            {
                Name = "0000 test"
            };

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            List<Recipe> list = (List<Recipe>)view1.Model;
            Recipe recipeVM = (from m in list
                                 where m.Name == "0000 test"
                                 select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("0000 test", recipeVM.Name);

            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = recipeVM.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            List<Recipe> list2 = (List<Recipe>)view2.Model;
            Recipe recipe2 = (from m in list2 
                                where m.Name == "0000 test Edited"
                                select m).AsQueryable().First();

            // Assert
            Assert.AreEqual("0000 test Edited", recipe2.Name);
        }


       
        [TestMethod]
        [TestCategory("DeleteConfirmed")]
        public void ActuallyDeleteARecipeFromTheDatabase()
        {
            // Arrange

            recipeVM.Description = "test ActuallyDeleteARecipeFromTheDatabase";
            //Act
            controller.DeleteConfirmed(recipeVM.ID);
            var deletedItem = (from m in Repo.GetAll()
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
            RecipesController controller1 = new RecipesController(Repo);
            RecipesController controller2 = new RecipesController(Repo);
            RecipesController controller3 = new RecipesController(Repo);
            RecipesController controller4 = new RecipesController(Repo);
            RecipesController controller5 = new RecipesController(Repo);
            Recipe vm = new Recipe
            {
                Name = "0000 test",
                Description = "SaveEditedRecipeWithDescriptionChange Pre-test"
            };


            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            List<Recipe> list = (List<Recipe>)view1.Model;
            Recipe recipeVM = (from m in list
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
            List<Recipe> list2 = (List<Recipe>)view2.Model;
            Recipe recipe2 = (from m in list2
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

        
         

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);
            Recipe recipeVM = new Recipe(CreationDate)
            {
                Name = "001 Test "
            };

            TestRepository<Recipe> repoRecipe = new TestRepository<Recipe>(); ;
            RecipesController controllerEdit = new RecipesController(repoRecipe);
            RecipesController controllerView = new RecipesController(repoRecipe);
            RecipesController controllerDelete = new RecipesController(repoRecipe);

            // Act
            controllerEdit.PostEdit(recipeVM);
            ViewResult view = controllerView.Index();
            List<Recipe> list = (List<Recipe>)view.Model;
            recipeVM = (from m in list
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
            RecipesController controllerPost = new RecipesController(Repo);
            RecipesController controllerPost1 = new RecipesController(Repo);
            RecipesController controllerView = new RecipesController(Repo);
            RecipesController controllerView1 = new RecipesController(Repo);
            RecipesController controllerDelete = new RecipesController(Repo);

            Recipe vm = new Recipe
            {
                Name = "002 Test Mod"
            };
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
            controllerPost.PostEdit(vm);

            ViewResult view = controllerView.Index();
            List<Recipe> list = (List<Recipe>)view.Model;
            Recipe vm2 = (from m in list
                            where m.Name == "002 Test Mod"
                            select m).AsQueryable().FirstOrDefault();


            vm.Description = "I've been edited to delay a bit";

            controllerPost1.PostEdit(vm2);


            ViewResult view1 = controllerView.Index();
            list = (List<Recipe>)view1.Model;
            Recipe vm3 = (from m in list
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
            RecipesController controllerPost = new RecipesController(Repo);
            RecipesController controllerView = new RecipesController(Repo);
            RecipesController controllerDelete = new RecipesController(Repo);
            Recipe vm = new Recipe
            {
                Name = "___test387",
                Description = "test387 description"
            };

            // Act
            controllerPost.PostEdit(vm);
            ViewResult view1 = controllerView.Index();
            List<Recipe> list = (List<Recipe>)view1.Model;
            Recipe recipeVM = (from m in list
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

            TestRepository<Recipe> repoRecipe = new TestRepository<Recipe>();
            TestRepository<Ingredient> repoIngredient = new TestRepository<Ingredient>();
            RecipesController controller = new RecipesController(repoRecipe);

            Recipe recipeVM = GetRecipe(repoRecipe, "test AttachAnExistingIngredientToAnExistingRecipe");
            Ingredient ingredient = GetIngredient(repoIngredient, "test AttachAnExistingIngredientToAnExistingRecipe");

            // Act
            controller.AttachIngredient(recipeVM.ID, ingredient);
            Recipe returnedRecipe = (from m in repoRecipe.GetAll()
                                         where m.Description == recipeVM.Description
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
            RecipesController controller = new RecipesController(repoRecipe);
            RecipesController controllerSubtract = new RecipesController(repoRecipe);

            Recipe recipeVM1 = GetRecipe(repoRecipe, "test NotDeleteAnIngredientAfterIngredientIsDetached");


            Ingredient ingredient2 = GetIngredient(repoIngredient, "test NotDeleteAnIngredientAfterIngredientIsDetached");
            controller.AttachIngredient(recipeVM1.ID, ingredient2);
            // Act
            controllerSubtract.RemoveIngredient(recipeVM1.ID, ingredient2);


            // Assert
            Assert.IsNotNull(ingredient2);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingExistIngredientToNonExistingRecipe()
        {
            // Arrange
            TestRepository<Recipe> repoRecipe = new TestRepository<Recipe>();
            TestRepository<Ingredient> repoIngredient = new TestRepository<Ingredient>();
            RecipesController controllerAttach = new RecipesController(repoRecipe);
            RecipesController controllerSubtract = new RecipesController(repoRecipe);

            string description = "test ReturnIndexViewWhenAttachingExistIngredientToNonExistingRecipe";

            Ingredient ingredient = GetIngredient(repoIngredient, description);

            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)controllerAttach.AttachIngredient(-1, ingredient );
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

            Recipe recipeVM = GetRecipe(Repo,  "test ReturnRecipeEditViewWithErrorMessageWhenAttachingNonExistingIngredientToExistingRrecipe");
            Ingredient ingredient = null;
            // Act  
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.AttachIngredient(recipeVM.ID, ingredient);
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
            TestRepository<Recipe> repoRecipe = new TestRepository<Recipe>();
            RecipesController controller = new RecipesController(repoRecipe);
            Ingredient vm = null;

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.AttachIngredient(-1, vm);
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

            RecipesController controllerAttachIngredient = new RecipesController(Repo);
            RecipesController controllerRemoveIngredient = new RecipesController(Repo);

            Recipe recipeVM = GetRecipe(Repo,  "test ReturnRecipeEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingRecipe");

            Ingredient ingredient = GetIngredient(repoIngredient, "test ReturnRecipeEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingRecipe");
            controllerAttachIngredient.AttachIngredient(recipeVM.ID, ingredient );

            // Act          
            AlertDecoratorResult adr = (AlertDecoratorResult)controllerRemoveIngredient.RemoveIngredient(recipeVM.ID, ingredient );
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
            TestRepository<Recipe> repoRecipe = new TestRepository<Recipe>();
            TestRepository<Ingredient> repoIngredient = new TestRepository<Ingredient>();
            RecipesController controllerDetachIngredient = new RecipesController(repoRecipe);
            string description = "test ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNoExistingRecipe";

            Ingredient ingredient = GetIngredient(repoIngredient, description);
            // Act  
            AlertDecoratorResult adr = (AlertDecoratorResult)controllerDetachIngredient.RemoveIngredient(-1, ingredient );
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
            
            TestRepository<Ingredient> repoIngredient = new TestRepository<Ingredient>();
            IngredientsController  controllerIngredient = new IngredientsController(repoIngredient);
            RecipesController controllerDetachIngredient = new RecipesController(Repo);
            string description = "test ReturnRecipeIndexViewWithWarningWhenDetachingExistingingredientNotAttachedToAnExistingRecipe";
            Ingredient ingredient = GetIngredient(repoIngredient, description);
            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)controllerDetachIngredient.RemoveIngredient(-1, ingredient );
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
            TestRepository<Recipe> repoRecipe = new TestRepository<Recipe>();
            RecipesController controllerRecipe = new RecipesController(repoRecipe);
            RecipesController controllerDetachIngredient = new RecipesController(repoRecipe);
            Recipe recipeVM = GetRecipe(repoRecipe, "test ReturnRecipeEditViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToExistingRecipe");
            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)controllerDetachIngredient.RemoveIngredient(recipeVM.ID, null);
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
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.RemoveIngredient(-1, null);

            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert
            
                Assert.AreEqual("alert-warning", adr.AlertClass);
                Assert.AreEqual("Recipe was not found", adr.Message);
                Assert.AreEqual(1, routeValues.Count);
                Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
            
        }

        internal Ingredient GetIngredient(IRepository<Ingredient> Repo,  string description)
        {
            IngredientsController controller = new IngredientsController(Repo);
            Ingredient ivm = new Ingredient
            {
                Description = description,
                ID = int.MaxValue
            };
            controller.PostEdit(ivm);

            Ingredient ingredient = ((from m in Repo.GetAll()
                                          where m.Description == description
                                          select m).AsQueryable()).FirstOrDefault();
            return ingredient;
        }


        internal Recipe GetRecipe(IRepository<Recipe> Repo,  string description)
        {
            RecipesController controller = new RecipesController(Repo);
            Recipe vm = new Recipe
            {
                Description = description,
                ID = int.MaxValue
            };
            controller.PostEdit(vm);

            Recipe recipeVM = ((from m in Repo.GetAll()
                                  where m.Description == description
                                  select m).AsQueryable()).FirstOrDefault();
            return recipeVM;
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Recipe\";

            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}

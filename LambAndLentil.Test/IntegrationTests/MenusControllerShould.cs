using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Controllers;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IntegrationTests
{

    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("MenusController")]
    public class MenusControllerShould
    {
        [TestMethod]
        public void CreateAnMenu()
        {
            // Arrange
            JSONRepository<Menu, MenuVM> repoMenu = new JSONRepository<Menu, MenuVM>(); ;
            MenusController controller = new MenusController(repoMenu);
            // Act
            ViewResult vr = controller.Create(LambAndLentil.UI.UIViewType.Create);
            MenuVM vm = (MenuVM)vr.Model;
            string modelName = vm.Name;

            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created");
            Assert.AreEqual(DayOfWeek.Sunday, vm.DayOfWeek);
        }

        [TestMethod]
        public void SaveAValidMenu()
        {
            // Arrange
            JSONRepository<Menu, MenuVM> repoMenu = new JSONRepository<Menu, MenuVM>(); ;
            MenusController controller = new MenusController(repoMenu);
            MenuVM vm = new MenuVM();
            vm.Name = "test";
            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit(vm);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;

            try
            {
                // Assert 
                Assert.AreEqual("alert-success", adr.AlertClass);
                Assert.AreEqual(4, routeValues.Count);
                Assert.AreEqual(UIControllerType.Menus.ToString(), routeValues.ElementAt(0).ToString());
                Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(1).ToString());
                Assert.AreEqual("Menus", routeValues.ElementAt(2).ToString());
                Assert.AreEqual(1.ToString(), routeValues.ElementAt(3).ToString());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Clean Up - should run a  delete test to make sure this works  
                Menu menu = repoMenu.GetAllT().Where(m => m.Name == "test").FirstOrDefault();
                controller.DeleteConfirmed(menu.ID);
            }
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedMenuWithNameChange()
        {
            // Arrange
            JSONRepository<Menu, MenuVM> repoMenu = new JSONRepository<Menu, MenuVM>(); ;
            MenusController controller1 = new MenusController(repoMenu);
            MenusController controller2 = new MenusController(repoMenu);
            MenusController controller3 = new MenusController(repoMenu);
            MenusController controller4 = new MenusController(repoMenu);
            MenusController controller5 = new MenusController(repoMenu);
            MenuVM vm = new MenuVM();
            vm.Name = "0000 test";

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM<Menu, MenuVM> listVM = (ListVM<Menu, MenuVM>)view1.Model;
            var result = (from m in listVM.Entities
                          where m.Name == "0000 test"
                          select m).FirstOrDefault();

            MenuVM menu = Mapper.Map<Menu, MenuVM>(result);
            try
            {
                // verify initial value:
                Assert.AreEqual("0000 test", menu.Name);
            }
            catch (Exception)
            {
                throw;
            }
            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = menu.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM<Menu, MenuVM> listVM2 = (ListVM<Menu, MenuVM>)view2.Model;
            var result2 = (from m in listVM2.Entities
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable().FirstOrDefault();


            MenuVM menu2 = Mapper.Map<Menu, MenuVM>(result2);
            try
            {
                // Assert
                Assert.AreEqual("0000 test Edited", menu.Name);
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
        public void SaveEditedMenuWithNameAndDayOfWeekChange()
        {
            // Arrange
            JSONRepository<Menu, MenuVM> repoMenu = new JSONRepository<Menu, MenuVM>(); ;
            MenusController controller1 = new MenusController(repoMenu);
            MenusController controller2 = new MenusController(repoMenu);
            MenusController controller3 = new MenusController(repoMenu);
            MenusController controller4 = new MenusController(repoMenu);
            MenusController controller5 = new MenusController(repoMenu);
            MenuVM vm = new MenuVM();
            vm.Name = "0000 test";


            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM<Menu, MenuVM> listVM = (ListVM<Menu, MenuVM>)view1.Model;
            var result = (from m in listVM.Entities
                          where m.Name == "0000 test"
                          select m).AsQueryable().FirstOrDefault();

            MenuVM menu = Mapper.Map<Menu, MenuVM>(result);
            try
            {
                // verify initial value:
                Assert.AreEqual("0000 test", menu.Name);
            }
            catch (Exception)
            {
                controller3.DeleteConfirmed(menu.ID);
                throw;
            }


            // now edit it
            vm.ID = menu.ID;
            vm.Name = "0000 test Edited";
            vm.DayOfWeek = DayOfWeek.Friday;

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM<Menu, MenuVM> listVM2 = (ListVM<Menu, MenuVM>)view2.Model;
            var result2 = (from m in listVM2.Entities
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable().FirstOrDefault();

            MenuVM menuVM = Mapper.Map<Menu, MenuVM>(result2);

            try
            {
                // Assert
                Assert.AreEqual("0000 test Edited", menuVM.Name);
                Assert.AreEqual(DayOfWeek.Friday, menuVM.DayOfWeek);
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
        public void ActuallyDeleteAMenuFromTheDatabase()
        {
            // Arrange
            JSONRepository<Menu, MenuVM> repoMenu = new JSONRepository<Menu, MenuVM>();
            MenusController controller = new MenusController(repoMenu);
            Menu item = GetMenu(repoMenu, "test ActuallyDeleteAMenuFromTheDatabase");
            //Act
            controller.DeleteConfirmed(item.ID);
            var deletedItem = (from m in repoMenu.GetAllT()
                               where m.Description == item.Description
                               select m).AsQueryable();

            //Assert
            Assert.AreEqual(0, deletedItem.Count());
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedMenuWithDescriptionChange()
        {
            // Arrange
            JSONRepository<Menu, MenuVM> repoMenu = new JSONRepository<Menu, MenuVM>(); ;
            MenusController controller1 = new MenusController(repoMenu);
            MenusController controller2 = new MenusController(repoMenu);
            MenusController controller3 = new MenusController(repoMenu);
            MenusController controller4 = new MenusController(repoMenu);
            MenusController controller5 = new MenusController(repoMenu);
            MenuVM vm = new MenuVM();
            vm.Name = "0000 test";
            vm.Description = "SaveEditedMenuWithDescriptionChange Pre-test";


            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM<Menu, MenuVM> listVM = (ListVM<Menu, MenuVM>)view1.Model;
            var result = (from m in listVM.Entities
                          where m.Name == "0000 test"
                          select m).AsQueryable().FirstOrDefault();

            MenuVM menuVM = Mapper.Map<Menu, MenuVM>(result);
            try
            {
                // verify initial value:
                Assert.AreEqual("SaveEditedMenuWithDescriptionChange Pre-test", menuVM.Description);
            }
            catch (Exception)
            {
                controller4.DeleteConfirmed(menuVM.ID);
                throw;
            }

            // now edit it
            vm.ID = menuVM.ID;
            vm.Name = "0000 test Edited";
            vm.Description = "SaveEditedMenuWithDescriptionChange Post-test";

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM<Menu, MenuVM> listVM2 = (ListVM<Menu, MenuVM>)view2.Model;
            var result2 = (from m in listVM2.Entities
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable().FirstOrDefault();
            menuVM = Mapper.Map<Menu, MenuVM>(result2);

            try
            {
                // Assert
                Assert.AreEqual("0000 test Edited", menuVM.Name);
                Assert.AreEqual("SaveEditedMenuWithDescriptionChange Post-test", menuVM.Description);
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
        public void SaveTheCreationDateOnMenuCreationWithNoParameterCtor()
        {
            // Arrange
            DateTime CreationDate = DateTime.Now;

            // Act
            Menu menu = new Menu();

            // Assert
            Assert.AreEqual(CreationDate.Date, menu.CreationDate.Date);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void ShouldSaveTheCreationDateOnMenuCreationWithDateTimeParameter()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);

            // Act
            Menu menu = new Menu(CreationDate);

            // Assert
            Assert.AreEqual(CreationDate, menu.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnMenuVMCreationWithNoParameterCtor()
        {
            // Arrange
            DateTime CreationDate = DateTime.Now;

            // Act
            MenuVM vm = new MenuVM();

            // Assert
            Assert.AreEqual(CreationDate.Date, vm.CreationDate.Date);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnMenuVMCreationWithDateTimeParameter()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);

            // Act
            MenuVM vm = new MenuVM(CreationDate);

            // Assert
            Assert.AreEqual(CreationDate, vm.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);
            MenuVM vm = new MenuVM(CreationDate);
            vm.Name = "001 Test ";

            JSONRepository<Menu, MenuVM> repoMenu = new JSONRepository<Menu, MenuVM>();
            MenusController controllerEdit = new MenusController(repoMenu);
            MenusController controllerView = new MenusController(repoMenu);
            MenusController controllerDelete = new MenusController(repoMenu);

            // Act
            controllerEdit.PostEdit(vm);
            ViewResult view = controllerView.Index();
            ListVM<Menu, MenuVM> listVM = (ListVM<Menu, MenuVM>)view.Model;
            var result = (from m in listVM.Entities
                          where m.Name == "001 Test "
                          select m).AsQueryable().FirstOrDefault();

            MenuVM menuVM = Mapper.Map<Menu, MenuVM>(result);

            DateTime shouldBeSameDate = menuVM.CreationDate;
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
                controllerDelete.DeleteConfirmed(menuVM.ID);
            }
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            // Arrange
            JSONRepository<Menu, MenuVM> repoMenu = new JSONRepository<Menu, MenuVM>();
            MenusController controllerPost = new MenusController(repoMenu);
            MenusController controllerPost1 = new MenusController(repoMenu);
            MenusController controllerView = new MenusController(repoMenu);
            MenusController controllerDelete = new MenusController(repoMenu);

            MenuVM vm = new MenuVM();
            vm.Name = "002 Test Mod";
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
            controllerPost.PostEdit(vm);
            ViewResult view = controllerView.Index();
            ListVM<Menu, MenuVM> listVM = (ListVM<Menu, MenuVM>)view.Model;
            var result = (from m in listVM.Entities
                          where m.Name == "002 Test Mod"
                          select m).AsQueryable();
            Menu item = result.FirstOrDefault();
            MenuVM menuVM = Mapper.Map<Menu, MenuVM>(item);


            menuVM.Description = "I've been edited to delay a bit";
            controllerPost1.PostEdit(menuVM);

            ViewResult view1 = controllerView.Index();
            listVM = (ListVM<Menu, MenuVM>)view1.Model;
            var result1 = (from m in listVM.Entities
                           where m.Name == "002 Test Mod"
                           select m).AsQueryable().FirstOrDefault();
            menuVM = Mapper.Map<Menu, MenuVM>(result1);

            DateTime shouldBeSameDate = menuVM.CreationDate;
            DateTime shouldBeLaterDate = menuVM.ModifiedDate;
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
                controllerDelete.DeleteConfirmed(menuVM.ID);
            }
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexWithSuccessAttachAnExistingRecipeToAnExistingMenu()
        {
            // Arrange

            JSONRepository<Menu, MenuVM> repoMenu = new JSONRepository<Menu, MenuVM>();
            JSONRepository<Recipe, RecipeVM> repoRecipe = new JSONRepository<Recipe, RecipeVM>();
            MenusController controllerAttach = new MenusController(repoMenu);
            RecipesController controllerAttachI = new RecipesController(repoRecipe);
            MenusController controllerCleanup = new MenusController(repoMenu);


            Menu menu = GetMenu(repoMenu, "test AttachAnExistingRecipeToAnExistingMenu");


            Recipe recipe = new RecipesControllerShould().GetRecipe(repoRecipe, "test AttachAnExistingRecipeToAnExistingMenu");

            // Act
            var x = controllerAttach.AttachRecipe(menu.ID, recipe.ID);

            // Assert 
            Assert.AreEqual(1, menu.Recipes.Count());
            // how do I know the correct recipe was added?
            Assert.AreEqual(recipe.ID, menu.Recipes.First().ID);

            // Cleanup
            RecipesController recipeController = new RecipesController(repoRecipe);

            MenuVM menu1VM = Mapper.Map<Menu, MenuVM>(menu);
            RecipeVM recipe2VM = Mapper.Map<Recipe, RecipeVM>(recipe);
            controllerCleanup.DeleteConfirmed(menu.ID);
            recipeController.DeleteConfirmed(recipe.ID);
        }

        internal Menu GetMenu(JSONRepository<Menu, MenuVM> repo, string description)
        {

            JSONRepository<Menu, MenuVM> repoMenu = new JSONRepository<Menu, MenuVM>();
            MenusController controller = new MenusController(repoMenu);
            MenuVM mvm = new MenuVM();
            mvm.ID = int.MaxValue;
            mvm.Description = description;
            controller.PostEdit(mvm);

            Menu menu = ((from m in repoMenu.GetAllT()
                          where m.Description == description
                          select m).AsQueryable()).FirstOrDefault();
            return menu;
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void AttachAnExistingIngredientToAnExistingMenu()
        {
            // Arrange
            JSONRepository<Menu, MenuVM> repoMenu = new JSONRepository<Menu, MenuVM>();
            JSONRepository<Ingredient, IngredientVM> repoIngredient = new JSONRepository<Ingredient, IngredientVM>();
            MenusController controller = new MenusController(repoMenu);

            Menu menu = GetMenu(repoMenu, "test AttachAnExistingIngredientToAnExistingMenu");
            Ingredient ingredient = new RecipesControllerShould().GetIngredient(repoIngredient, "test AttachAnExistingIngredientToAnExistingMenu");

            // Act
            controller.AttachIngredient(menu.ID, ingredient.ID);
            Menu returnedMenu = (from m in repoMenu.GetAllT()
                                 where m.Description == menu.Description
                                 select m).FirstOrDefault();



            // Assert 
            Assert.AreEqual(1, returnedMenu.Ingredients.Count());
            // how do I know the correct ingredient was added?
            Assert.AreEqual(ingredient.ID, returnedMenu.Ingredients.First().ID);

            // Cleanup
            IngredientsController controllerCleanupIngredient = new IngredientsController(repoIngredient);
            MenusController controllerCleanupMenu = new MenusController(repoMenu);

            MenuVM menuVM = Mapper.Map<Menu, MenuVM>(menu);
            IngredientVM ingredientVM = Mapper.Map<Ingredient, IngredientVM>(ingredient);

            controllerCleanupMenu.DeleteConfirmed(menuVM.ID);
            controllerCleanupIngredient.DeleteConfirmed(ingredientVM.ID);
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void NotDeleteAnIngredientAfterIngredientIsDetachedFromMenu()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToANonExistingMenu()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void  ReturnIndexViewWithWarningWhenAttachingExistIngredientToNonExistingMenu()
        {
            Assert.Fail();
        }
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingNonExistIngredientToNonExistingMenu()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNonExistingMenu()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnMenuEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingMenu()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnMenuEditViewWithWarningMessageWhenAttachingNonExistingIngredientToExistingRMenu()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnMenuEditViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToExistingMenu()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnMenuIndexViewWithWarningWhenDetachingExistingingredientNotAttachedToAnExistingMenu()
        {
            Assert.Fail();
        } 
    }
}

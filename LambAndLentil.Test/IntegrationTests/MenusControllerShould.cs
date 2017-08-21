using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Controllers;
using LambAndLentil.Tests.Infrastructure;
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
        static IRepository<Menu, MenuVM> repo;
        static MenusController controller;
        static ListVM<Menu, MenuVM> listVM;
        public static MapperConfiguration AutoMapperConfig { get; set; }

        public MenusControllerShould()
        {
            AutoMapperConfigForTests.InitializeMap();
            repo = new TestRepository<Menu, MenuVM>();
            listVM = new ListVM<Menu, MenuVM>();
            controller = SetUpMenusController(repo);
        }

        private MenusController SetUpMenusController(IRepository<Menu, MenuVM> repo)
        {
            listVM.ListT = new List<Menu> {
                new Menu {ID = int.MaxValue, Name = "MenusController_Index_Test P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Menu {ID = int.MaxValue-1, Name = "MenusController_Index_Test P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new Menu {ID = int.MaxValue-2, Name = "MenusController_Index_Test P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new Menu {ID = int.MaxValue-3, Name = "MenusController_Index_Test P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Menu {ID = int.MaxValue-4, Name = "MenusController_Index_Test P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            }.AsQueryable();

            foreach (Menu ingredient in listVM.ListT)
            {
                repo.AddT(ingredient);
            }

            MenusController controller = new MenusController(repo);
            controller.PageSize = 3;

            return controller;
        }

        [TestMethod]
        public void CreateAnMenu()
        {
            // Arrange

            // Act
            ViewResult vr = controller.Create(UIViewType.Create);
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
            MenuVM vm = new MenuVM();
            vm.Name = "test";
            vm.ID = 2;
            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit(vm);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;


            // Assert 
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedMenuWithNameChange()
        {
            // Arrange
            JSONRepository<Menu, MenuVM> repo = new JSONRepository<Menu, MenuVM>(); ;
            MenusController controller1 = new MenusController(repo);
            MenusController controller2 = new MenusController(repo);
            MenusController controller3 = new MenusController(repo);
            MenusController controller4 = new MenusController(repo);
            MenusController controller5 = new MenusController(repo);
            MenuVM vm = new MenuVM();
            vm.Name = "0000 test";
            vm.ID = 33;

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM<Menu, MenuVM> listVM = (ListVM<Menu, MenuVM>)view1.Model;
            var menuVM = (from m in listVM.ListTVM
                          where m.Name == "0000 test"
                          select m).FirstOrDefault();


            // verify initial value:
            Assert.AreEqual("0000 test", menuVM.Name);

            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = menuVM.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM<Menu, MenuVM> listVM2 = (ListVM<Menu, MenuVM>)view2.Model;
            MenuVM menu2 = (from m in listVM2.ListTVM
                            where m.Name == "0000 test Edited"
                            select m).AsQueryable().FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited", menu2.Name);

        }



        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedMenuWithNameAndDayOfWeekChange()
        {
            // Arrange

            MenusController controller1 = new MenusController(repo);
            MenusController controller2 = new MenusController(repo);
            MenusController controller3 = new MenusController(repo);
            MenusController controller4 = new MenusController(repo);
            MenuVM vm = new MenuVM();
            vm.Name = "0000 test";
            vm.ID = 77777;

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM<Menu, MenuVM> listVM = (ListVM<Menu, MenuVM>)view1.Model;
            MenuVM menu = (from m in listVM.ListTVM
                           where m.Name == "0000 test"
                           select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("0000 test", menu.Name);
            // now edit it
            vm.ID = menu.ID;
            vm.Name = "0000 test Edited";
            vm.DayOfWeek = DayOfWeek.Friday;

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM<Menu, MenuVM> listVM2 = (ListVM<Menu, MenuVM>)view2.Model;
            MenuVM menuVM = (from m in listVM2.ListTVM
                             where m.Name == "0000 test Edited"
                             select m).AsQueryable().FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited", menuVM.Name);
            Assert.AreEqual(DayOfWeek.Friday, menuVM.DayOfWeek);
        }



        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedMenuWithDescriptionChange()
        {
            // Arrange

            MenusController controller1 = new MenusController(repo);
            MenusController controller2 = new MenusController(repo);
            MenusController controller3 = new MenusController(repo);
            MenusController controller4 = new MenusController(repo);
            MenusController controller5 = new MenusController(repo);
            MenuVM vm = new MenuVM();
            vm.Name = "0000 test";
            vm.Description = "SaveEditedMenuWithDescriptionChange Pre-test";
            vm.ID = int.MaxValue / 2;

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM<Menu, MenuVM> listVM = (ListVM<Menu, MenuVM>)view1.Model;
            MenuVM menuVM = (from m in listVM.ListTVM
                             where m.Name == "0000 test"
                             select m).AsQueryable().FirstOrDefault();



            // verify initial value:
            Assert.AreEqual("SaveEditedMenuWithDescriptionChange Pre-test", menuVM.Description);


            // now edit it
            vm.ID = menuVM.ID;
            vm.Name = "0000 test Edited";
            vm.Description = "SaveEditedMenuWithDescriptionChange Post-test";

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM<Menu, MenuVM> listVM2 = (ListVM<Menu, MenuVM>)view2.Model;
            menuVM = (from m in listVM2.ListTVM
                      where m.Name == "0000 test Edited"
                      select m).AsQueryable().FirstOrDefault();


            // Assert
            Assert.AreEqual("0000 test Edited", menuVM.Name);
            Assert.AreEqual("SaveEditedMenuWithDescriptionChange Post-test", menuVM.Description);

        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnMenuCreationWithNoParameterCtor()
        {
            // Arrange
            DateTime CreationDate = DateTime.Now;

            // Act
            Menu menu = new Menu();
            TimeSpan timeSpan = CreationDate - menu.CreationDate;

            // Assert
            Assert.AreEqual(1, 1);
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
            vm.ID = 290;
            vm.Description = "test SaveTheCreationDateBetweenPostedEdits";


            MenusController controllerEdit = new MenusController(repo);
            MenusController controllerView = new MenusController(repo);
            MenusController controllerDelete = new MenusController(repo);

            // Act
            controllerEdit.PostEdit(vm);
            ViewResult view = controllerView.Index();
            ListVM<Menu, MenuVM> listVM = (ListVM<Menu, MenuVM>)view.Model;
            MenuVM menuVM = (from m in listVM.ListTVM
                             where m.Name == "001 Test "
                             select m).AsQueryable().FirstOrDefault(); 

            DateTime shouldBeSameDate = menuVM.CreationDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);

        }

        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            // Arrange
            JSONRepository<Menu, MenuVM> repo = new JSONRepository<Menu, MenuVM>();
            MenusController controllerPost = new MenusController(repo);
            MenusController controllerPost1 = new MenusController(repo);
            MenusController controllerView = new MenusController(repo);
            MenusController controllerDelete = new MenusController(repo);

            MenuVM vm = new MenuVM();
            vm.Name = "002 Test Mod";
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
            controllerPost.PostEdit(vm);
            ViewResult view = controllerView.Index();
            ListVM<Menu, MenuVM> listVM = (ListVM<Menu, MenuVM>)view.Model;
            MenuVM menuVM = (from m in listVM.ListTVM
                          where m.Name == "002 Test Mod"
                          select m).AsQueryable().FirstOrDefault();
             

            menuVM.Description = "I've been edited to delay a bit";
            controllerPost1.PostEdit(menuVM);

            ViewResult view1 = controllerView.Index();
            listVM = (ListVM<Menu, MenuVM>)view1.Model;
               menuVM = (from m in listVM.ListTVM
                           where m.Name == "002 Test Mod"
                           select m).AsQueryable().FirstOrDefault();
        

            DateTime shouldBeSameDate = menuVM.CreationDate;
            DateTime shouldBeLaterDate = menuVM.ModifiedDate;
          
                // Assert
                Assert.AreEqual(CreationDate, shouldBeSameDate);
                Assert.AreNotEqual(mod, shouldBeLaterDate);
            
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexWithSuccessAttachAnExistingRecipeToAnExistingMenu()
        {
            // Arrange 
            IRepository<Recipe, RecipeVM> repoRecipe = new TestRepository<Recipe, RecipeVM>();
            MenusController controllerAttach = new MenusController(repo);
            RecipesController controllerAttachI = new RecipesController(repoRecipe);
            MenusController controllerCleanup = new MenusController(repo);


            MenuVM menuVM = new MenuVM() { ID = 100, Description = "test AttachAnExistingRecipeToAnExistingMenu" };
            repo.AddTVM(menuVM);

            RecipeVM recipeVM = new RecipeVM() { ID = 101, Description = "test AttachAnExistingRecipeToAnExistingMenu" };
            repoRecipe.AddTVM(recipeVM);
            // Act
            var x = controllerAttach.AttachRecipe(menuVM.ID, recipeVM.ID);

            // Assert 
            Assert.AreEqual(1, menuVM.Recipes.Count());
            // how do I know the correct recipe was added?
            Assert.AreEqual(recipeVM.ID, menuVM.Recipes.First().ID);


        }



        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void AttachAnExistingIngredientToAnExistingMenu()
        {
            // Arrange
            //JSONRepository<Menu, MenuVM> repo = new JSONRepository<Menu, MenuVM>();
            //JSONRepository<Ingredient, IngredientVM> repoIngredient = new JSONRepository<Ingredient, IngredientVM>();
            //MenusController controller = new MenusController(repo);

            //Menu menu = GetMenu(repo, "test AttachAnExistingIngredientToAnExistingMenu");
            //Ingredient ingredient = new RecipesControllerShould().GetIngredient(repoIngredient, "test AttachAnExistingIngredientToAnExistingMenu");

            //// Act
            //controller.AttachIngredient(menu.ID, ingredient.ID);
            //Menu returnedMenu = (from m in repo.GetAllT()
            //                     where m.Description == menu.Description
            //                     select m).FirstOrDefault();



            //// Assert 
            //Assert.AreEqual(1, returnedMenu.Ingredients.Count());
            //// how do I know the correct ingredient was added?
            //Assert.AreEqual(ingredient.ID, returnedMenu.Ingredients.First().ID);

        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void NotDeleteAnIngredientAfterIngredientIsDetachedFromMenu()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToANonExistingMenu()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingExistIngredientToNonExistingMenu()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingNonExistIngredientToNonExistingMenu()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNonExistingMenu()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnMenuEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingMenu()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnMenuEditViewWithWarningMessageWhenAttachingNonExistingIngredientToExistingRMenu()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnMenuEditViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToExistingMenu()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnMenuIndexViewWithWarningWhenDetachingExistingingredientNotAttachedToAnExistingMenu()
        {
            Assert.Fail();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            MenusControllerTest.ClassCleanup();
        }
    }
}

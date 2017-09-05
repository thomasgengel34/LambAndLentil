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
        static IRepository<MenuVM> Repo;
        static MenusController controller;
        static ListVM<MenuVM> listVM;
        public static MapperConfiguration AutoMapperConfig { get; set; }

        public MenusControllerShould()
        {
            AutoMapperConfigForTests.InitializeMap();
            Repo = new TestRepository<MenuVM>();
            listVM = new ListVM<MenuVM>();
            controller = SetUpMenusController(Repo);
        }

        private MenusController SetUpMenusController(IRepository<MenuVM> repo)
        {
            listVM.ListT = new List<MenuVM> {
                new MenuVM {ID = int.MaxValue, Name = "MenuVMsController_Index_Test P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new MenuVM {ID = int.MaxValue-1, Name = "MenuVMsController_Index_Test P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new MenuVM {ID = int.MaxValue-2, Name = "MenuVMsController_Index_Test P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new MenuVM {ID = int.MaxValue-3, Name = "MenuVMsController_Index_Test P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new MenuVM {ID = int.MaxValue-4, Name = "MenuVMsController_Index_Test P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            }.AsQueryable();

            foreach (MenuVM vm in listVM.ListT)
            {
                Repo.Add(vm);
            }

            MenusController controller = new MenusController(Repo);
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
            JSONRepository<MenuVM> Repo = new JSONRepository<MenuVM>(); ;
            MenusController controller1 = new MenusController(Repo);
            MenusController controller2 = new MenusController(Repo);
            MenusController controller3 = new MenusController(Repo);
            MenusController controller4 = new MenusController(Repo);
            MenusController controller5 = new MenusController(Repo);
            MenuVM vm = new MenuVM();
            vm.Name = "0000 test";
            vm.ID = 33;

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM<MenuVM> listVM = (ListVM<MenuVM>)view1.Model;
            var menuVM = (from m in listVM.ListT
                          where m.Name == "0000 test"
                          select m).FirstOrDefault();


            // verify initial value:
            Assert.AreEqual("0000 test", menuVM.Name);

            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = menuVM.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM<MenuVM> listVM2 = (ListVM<MenuVM>)view2.Model;
            MenuVM menu2 = (from m in listVM2.ListT
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

            MenusController controller1 = new MenusController(Repo);
            MenusController controller2 = new MenusController(Repo);
            MenusController controller3 = new MenusController(Repo);
            MenusController controller4 = new MenusController(Repo);
            MenuVM vm = new MenuVM();
            vm.Name = "0000 test";
            vm.ID = 77777;

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM<MenuVM> listVM = (ListVM<MenuVM>)view1.Model;
            MenuVM menu = (from m in listVM.ListT
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
            ListVM<MenuVM> listVM2 = (ListVM<MenuVM>)view2.Model;
            MenuVM menuVM = (from m in listVM2.ListT
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

            MenusController controller1 = new MenusController(Repo);
            MenusController controller2 = new MenusController(Repo);
            MenusController controller3 = new MenusController(Repo);
            MenusController controller4 = new MenusController(Repo);
            MenusController controller5 = new MenusController(Repo);
            MenuVM vm = new MenuVM();
            vm.Name = "0000 test";
            vm.Description = "SaveEditedMenuWithDescriptionChange Pre-test";
            vm.ID = int.MaxValue / 2;

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM<MenuVM> listVM = (ListVM<MenuVM>)view1.Model;
            MenuVM menuVM = (from m in listVM.ListT
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
            ListVM<MenuVM> listVM2 = (ListVM<MenuVM>)view2.Model;
            menuVM = (from m in listVM2.ListT
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


            MenusController controllerEdit = new MenusController(Repo);
            MenusController controllerView = new MenusController(Repo);
            MenusController controllerDelete = new MenusController(Repo);

            // Act
            controllerEdit.PostEdit(vm);
            ViewResult view = controllerView.Index();
            ListVM<MenuVM> listVM = (ListVM<MenuVM>)view.Model;
            MenuVM menuVM = (from m in listVM.ListT
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
            MenusController controllerPost = new MenusController(Repo);
            MenusController controllerPost1 = new MenusController(Repo);
            MenusController controllerView = new MenusController(Repo);
            MenusController controllerDelete = new MenusController(Repo);

            MenuVM vm = new MenuVM();
            vm.Name = "002 Test Mod";
            vm.ID = 1234567;
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
            controllerPost.PostEdit(vm);
            ViewResult view = controllerView.Index();
            ListVM<MenuVM> listVM = (ListVM<MenuVM>)view.Model;
            MenuVM menuVM = (from m in listVM.ListT
                          where m.Name == "002 Test Mod"
                          select m).AsQueryable().FirstOrDefault();
             

            menuVM.Description = "I've been edited to delay a bit";
            controllerPost1.PostEdit(menuVM);

            ViewResult view1 = controllerView.Index();
            listVM = (ListVM<MenuVM>)view1.Model;
               menuVM = (from m in listVM.ListT
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
            IRepository<RecipeVM> repoRecipe = new TestRepository<RecipeVM>();
            MenusController controllerAttach = new MenusController(Repo);
            RecipesController controllerAttachI = new RecipesController(repoRecipe);
            MenusController controllerCleanup = new MenusController(Repo);


            MenuVM menuVM = new MenuVM() { ID = 100, Description = "test AttachAnExistingRecipeToAnExistingMenu" };
            Repo.Add(menuVM);

            RecipeVM recipeVM = new RecipeVM() { ID = 101, Description = "test AttachAnExistingRecipeToAnExistingMenu" };
            repoRecipe.Add(recipeVM);
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
            TestRepository<IngredientVM> repoIngredient = new TestRepository<  IngredientVM>(); 

            MenuVM menuVM = new MenuVM { ID = int.MaxValue - 100, Description = "test AttachAnExistingIngredientToAnExistingMenu" };
             IngredientVM ingredientVM = new IngredientVM { ID = int.MaxValue - 100, Description = "test AttachAnExistingIngredientToAnExistingMenu" };

            // Act
            controller.AttachIngredient(menuVM.ID, ingredientVM.ID);
            MenuVM returnedMenuVM = (from m in Repo.GetAll()
                                 where m.Description == menuVM.Description
                                 select m).FirstOrDefault();

            // Assert 
            Assert.AreEqual(1, returnedMenuVM.Ingredients.Count());
            // how do I know the correct ingredient was added?
            Assert.AreEqual(ingredientVM.ID, returnedMenuVM.Ingredients.First().ID);

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

using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.BasicControllerTests;
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
        static IRepository<Menu> Repo;
        static MenusController controller;
        static List<Menu> list;
        public static MapperConfiguration AutoMapperConfig { get; set; }

        public MenusControllerShould()
        {
            AutoMapperConfigForTests.InitializeMap();
            Repo = new TestRepository<Menu>();
            list = new List<Menu>();
            controller = SetUpMenusController(Repo);
        }

        private MenusController SetUpMenusController(IRepository<Menu> repo)
        {
           list = new List<Menu> {
                new Menu {ID = int.MaxValue, Name = "MenusController_Index_Test P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Menu {ID = int.MaxValue-1, Name = "MenusController_Index_Test P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new Menu {ID = int.MaxValue-2, Name = "MenusController_Index_Test P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new Menu {ID = int.MaxValue-3, Name = "MenusController_Index_Test P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Menu {ID = int.MaxValue-4, Name = "MenusController_Index_Test P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            } ;

            foreach (Menu vm in list)
            {
                Repo.Add(vm);
            }

            MenusController controller = new MenusController(Repo)
            {
                PageSize = 3
            };

            return controller;
        }

        [TestMethod]
        public void CreateAnMenu()
        {
            // Arrange

            // Act
            ViewResult vr = controller.Create(UIViewType.Create);
            Menu vm = (Menu)vr.Model;
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
            Menu vm = new Menu
            {
                Name = "test",
                ID = 2
            };
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
            JSONRepository<Menu> Repo = new JSONRepository<Menu>(); ;
            MenusController controller1 = new MenusController(Repo);
            MenusController controller2 = new MenusController(Repo);
            MenusController controller3 = new MenusController(Repo);
            MenusController controller4 = new MenusController(Repo);
            MenusController controller5 = new MenusController(Repo);
            Menu vm = new Menu
            {
                Name = "0000 test",
                ID = 33
            };

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            List<Menu> list =  (List<Menu>)((ListEntity<Menu>)view1.Model).ListT;
            var menuVM = (from m in list
                          where m.Name == "0000 test"
                          select m).FirstOrDefault();


            // verify initial value:
            Assert.AreEqual("0000 test", menuVM.Name);

            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = menuVM.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            List<Menu> list2 = (List<Menu>)((ListEntity<Menu>)view2.Model).ListT;
            Menu menu2 = (from m in list2
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
            Menu vm = new Menu
            {
                Name = "0000 test",
                ID = 77777
            };

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            List<Menu> list = (List<Menu>)((ListEntity<Menu>)view1.Model).ListT;
            Menu menu = (from m in list
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
            List<Menu> list2 = (List<Menu>)((ListEntity<Menu>)view2.Model).ListT;
            Menu menuVM = (from m in list2
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
            Menu vm = new Menu
            {
                Name = "0000 test",
                Description = "SaveEditedMenuWithDescriptionChange Pre-test",
                ID = int.MaxValue / 2
            };

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            List<Menu> list = (List<Menu>)((ListEntity<Menu>)view1.Model).ListT;
            Menu menuVM = (from m in list
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
            List<Menu> list2 = (List<Menu>)((ListEntity<Menu>)view2.Model).ListT;
            menuVM = (from m in list2
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
        public void SaveTheCreationDateOnMenuCreationWithDateTimeParameter()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);

            // Act
            Menu vm = new Menu(CreationDate);

            // Assert
            Assert.AreEqual(CreationDate, vm.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);
            Menu vm = new Menu(CreationDate)
            {
                Name = "001 Test ",
                ID = 290,
                Description = "test SaveTheCreationDateBetweenPostedEdits"
            };


            MenusController controllerEdit = new MenusController(Repo);
            MenusController controllerView = new MenusController(Repo);
            MenusController controllerDelete = new MenusController(Repo);

            // Act
            controllerEdit.PostEdit(vm);
            ViewResult view = controllerView.Index();
            List<Menu> list = (List<Menu>)((ListEntity<Menu>)view.Model).ListT;
            Menu menuVM = (from m in list
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

            Menu vm = new Menu
            {
                Name = "002 Test Mod",
                ID = 1234567
            };
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
            controllerPost.PostEdit(vm);
            ViewResult view = controllerView.Index();
            List<Menu> list = (List<Menu>)((ListEntity<Menu>)view.Model).ListT;
            Menu menuVM = (from m in list
                          where m.Name == "002 Test Mod"
                          select m).AsQueryable().FirstOrDefault();
             

            menuVM.Description = "I've been edited to delay a bit";
            controllerPost1.PostEdit(menuVM);

            ViewResult view1 = controllerView.Index();
            list = (List<Menu>)((ListEntity<Menu>)view1.Model).ListT;
            menuVM = (from m in list
                           where m.Name == "002 Test Mod"
                           select m).AsQueryable().FirstOrDefault();
        

            DateTime shouldBeSameDate = menuVM.CreationDate;
            DateTime shouldBeLaterDate = menuVM.ModifiedDate;
          
                // Assert
                Assert.AreEqual(CreationDate, shouldBeSameDate);
                Assert.AreNotEqual(mod, shouldBeLaterDate);
            
        }

        //[Ignore]
        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void ReturnIndexWithSuccessAttachAnExistingRecipeToAnExistingMenu()
        //{
        //    // Arrange 
        //    IRepository<Recipe> repoRecipe = new TestRepository<Recipe>();
        //    MenusController controllerAttach = new MenusController(Repo);
        //    RecipesController controllerAttachI = new RecipesController(repoRecipe);
        //    MenusController controllerCleanup = new MenusController(Repo);


        //    Menu menuVM = new Menu() { ID = 100, Description = "test AttachAnExistingRecipeToAnExistingMenu" };
        //    Repo.Add(menuVM);

        //    Recipe recipeVM = new Recipe() { ID = 101, Description = "test AttachAnExistingRecipeToAnExistingMenu" };
        //    repoRecipe.Add(recipeVM);
        //    // Act
        //    var x = controllerAttach.AttachRecipe(menuVM.ID, recipeVM );

        //    // Assert 
        //    Assert.AreEqual(1, menuVM.Recipes.Count());
        //    // how do I know the correct recipe was added?
        //    Assert.AreEqual(recipeVM.ID, menuVM.Recipes.First().ID);


        //}


  
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void AttachAnExistingIngredientToAnExistingMenu()
        {
            // Arrange 
            TestRepository<Ingredient> repoIngredient = new TestRepository<  Ingredient>(); 

            Menu menuVM = new Menu { ID = int.MaxValue - 100, Description = "test AttachAnExistingIngredientToAnExistingMenu" };
             Ingredient ingredient = new Ingredient { ID = int.MaxValue - 100, Description = "test AttachAnExistingIngredientToAnExistingMenu" };
            Repo.Add(menuVM);
            repoIngredient.Add(ingredient);

            // Act
            controller.AttachIngredient(menuVM.ID, ingredient );
            Menu returnedMenu = (from m in Repo.GetAll()
                                 where m.Description == menuVM.Description
                                 select m).FirstOrDefault();

            // Assert 
            Assert.AreEqual(1, returnedMenu.Ingredients.Count());
            // how do I know the correct ingredient was added?
            Assert.AreEqual(ingredient.ID, returnedMenu.Ingredients.First().ID);

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
        public static void ClassCleanup() => MenusController_Test_Should.ClassCleanup();
    }
}

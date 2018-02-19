﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.BaseControllerTests;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{

    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("MenusController")]
    public class MenusControllerShould : MenusController_Test_Should
    {
        private static IGenericController<Menu> Controller1, Controller2, Controller3, Controller4, Controller5;


        public MenusControllerShould()
        {

            Repo = new TestRepository<Menu>();  
            Controller1 = new MenusController(Repo);
            Controller2 = new MenusController(Repo);
            Controller3 = new MenusController(Repo);
            Controller4 = new MenusController(Repo);
            Controller5 = new MenusController(Repo);
            Menu = new Menu
            {
                Name = "0000 test",
                ID = 33
            };
            Repo.Save((Menu)Menu);
            Controller = new MenusController(Repo);
        }


      

        

        [TestMethod]
        public void SaveAValidMenu()
        {
            Menu Menu = new Menu
            {
                Name = "test",
                ID = 2
            };
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.PostEdit(Menu);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;

            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedMenuWithNameChange()
        {
            Menu menu = new Menu() { ID = 6001, Name = "test SaveEditedMenuWithNameChange" };
            Repo.Save(menu);

            menu.Name = "0000 test Edited";
            ActionResult ar = Controller.PostEdit(menu); 
            Menu returnedMenu = Repo.GetById(menu.ID);

            Assert.AreEqual("0000 test Edited", returnedMenu.Name); 
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedMenuWithNameAndDayOfWeekChange()
        {  
             IRepository<Menu> repo = new TestRepository<Menu>();
            IGenericController<Menu> controller = new MenusController(repo);
            Menu menu = new Menu() { ID = 20020, DayOfWeek=DayOfWeek.Sunday };
            repo.Save(menu);
            menu.DayOfWeek = DayOfWeek.Friday;
            ActionResult ar2 = controller.PostEdit(menu);
             Menu returnedMenu = repo.GetById(menu.ID);
            Assert.AreEqual(DayOfWeek.Friday, returnedMenu.DayOfWeek);
        }



        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedMenuWithDescriptionChange()
        {

            Menu.Name = "0000 test";
            Menu.Description = "SaveEditedMenuWithDescriptionChange Pre-test";
            Menu.ID = int.MaxValue / 2;


            ActionResult ar1 = Controller1.PostEdit((Menu)Menu);
            ViewResult view1 = (ViewResult)Controller2.Index();
            List<Menu> ListEntity = (List<Menu>)((ListEntity<Menu>)view1.Model).ListT;
            Menu menu = (from m in ListEntity
                         where m.Name == "0000 test"
                         select m).AsQueryable().FirstOrDefault();



            // verify initial value:
            Assert.AreEqual("SaveEditedMenuWithDescriptionChange Pre-test", menu.Description);


            // now edit it
            Menu.ID = menu.ID;
            Menu.Name = "0000 test Edited";
            Menu.Description = "SaveEditedMenuWithDescriptionChange Post-test";

            ActionResult ar2 = Controller3.PostEdit((Menu)Menu);
            ViewResult view2 = (ViewResult)Controller4.Index();
            List<Menu> ListEntity2 = (List<Menu>)((ListEntity<Menu>)view2.Model).ListT;
            menu = (from m in ListEntity2
                    where m.Name == "0000 test Edited"
                    select m).AsQueryable().FirstOrDefault();



            Assert.AreEqual("0000 test Edited", menu.Name);
            Assert.AreEqual("SaveEditedMenuWithDescriptionChange Post-test", menu.Description);

        }


        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnMenuCreationWithNoParameterCtor()
        {

            DateTime CreationDate = DateTime.Now;


            Menu menu = new Menu() { ID = 2000 };
            TimeSpan timeSpan = CreationDate - menu.CreationDate;

            Assert.AreEqual(CreationDate.Date, menu.CreationDate.Date);
        }


        [TestMethod]
        [TestCategory("Edit")]
        public void ShouldSaveTheCreationDateOnMenuCreationWithDateTimeParameter()
        {

            DateTime CreationDate = new DateTime(2010, 1, 1);


            Menu menu = new Menu(CreationDate);


            Assert.AreEqual(CreationDate, menu.CreationDate);
        }



        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnMenuCreationWithDateTimeParameter()
        {

            DateTime CreationDate = new DateTime(2010, 1, 1);


            Menu Menu = new Menu(CreationDate);


            Assert.AreEqual(CreationDate, Menu.CreationDate);
        }
         
         
         
        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexWithSuccessAttachAnExistingRecipeToAnExistingMenu()
        {
            IRepository<Recipe> repoRecipe = new TestRepository<Recipe>();
            IGenericController<Menu> ControllerAttach = new MenusController(Repo);
            IGenericController<Recipe> ControllerAttachI = new RecipesController(repoRecipe);
            IGenericController<Menu> ControllerCleanup = new MenusController(Repo);


            Menu menu = new Menu() { ID = 100, Description = "test AttachAnExistingRecipeToAnExistingMenu" };
            Repo.Save(menu);

            Recipe recipeVM = new Recipe() { ID = 101, Description = "test AttachAnExistingRecipeToAnExistingMenu" };
            repoRecipe.Save(recipeVM);

            var x = ControllerAttach.Attach(menu, recipeVM);


            Assert.AreEqual(1, menu.Recipes.Count());

            Assert.AreEqual(recipeVM.ID, menu.Recipes.First().ID);
        }
         

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void AttachAnExistingIngredientToAnExistingMenu()
        {
            IRepository<Menu> repository = new TestRepository<Menu>();
            Menu menu = new Menu { ID = 101, Description = "test AttachAnExistingIngredientToAnExistingMenu" };
            repository.Save(menu);

            Ingredient ingredient = new Ingredient { ID = 100, Description = "test AttachAnExistingIngredientToAnExistingMenu" };

            ActionResult ar = Controller.Attach(menu, ingredient);
            Menu returnedMenu = repository.GetById(menu.ID);

            Assert.AreEqual(1, returnedMenu.Ingredients.Count());
            Assert.AreEqual(ingredient.ID, returnedMenu.Ingredients.First().ID);

        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void NotDeleteAnIngredientAfterIngredientIsDetachedFromMenu()
        {
            Ingredient ingredient = new Ingredient { ID = int.MaxValue - 100, Description = "test NotDeleteAnIngredientAfterIngredientIsDetachedFromMenu" };

            Controller.Attach(Menu, ingredient);
            Controller.Detach(Menu, ingredient);

            Assert.IsNotNull(ingredient);
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenDetachingNonExistingIngredientAttachedToANonExistingMenu()
        {
            Menu menu = new Menu() { ID = 700 };
            menu = null;
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Detach(menu, (Ingredient)null);

            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Menu was not found", adr.Message);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        }


 

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNonExistingMenu()
        {
            Menu menu = new Menu();
            menu = null;
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Detach(menu, (Ingredient)null);

            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Menu was not found", adr.Message);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        }
         
    }
}

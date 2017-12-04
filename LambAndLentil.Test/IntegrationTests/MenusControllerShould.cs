using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.BasicControllerTests;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
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
    public class MenusControllerShould : MenusController_Test_Should
    {
        private static IGenericController<Menu> Controller1, Controller2, Controller3, Controller4, Controller5;
       

        public MenusControllerShould()
        {
            Repo = new TestRepository<Menu>(); ;
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
            Controller = new MenusController(Repo);
        }


        [TestMethod]
        public void CreateAnMenu()
        {
            // Arrange

            // Act
            ViewResult vr = Controller.Create(UIViewType.Create);
            Menu Menu = (Menu)vr.Model;
            string modelName = Menu.Name;

            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created");
            Assert.AreEqual(DayOfWeek.Sunday, Menu.DayOfWeek);
        }

        [TestMethod]
        public void SaveAValidMenu()
        {
            // Arrange 
            Menu Menu = new Menu
            {
                Name = "test",
                ID = 2
            };
            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.PostEdit(Menu);
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


            // Act 
            ActionResult ar1 = Controller1.PostEdit((Menu)Menu);
            ViewResult view1 = Controller2.Index();
            List<Menu> ListEntity = (List<Menu>)((ListEntity<Menu>)view1.Model).ListT;
            var menu = (from m in ListEntity
                        where m.Name == "0000 test"
                        select m).FirstOrDefault();


            // verify initial value:
            Assert.AreEqual("0000 test", menu.Name);

            // now edit it
            Menu.Name = "0000 test Edited";
            Menu.ID = menu.ID;
            ActionResult ar2 = Controller3.PostEdit((Menu)Menu);
            ViewResult view2 = Controller4.Index();
            List<Menu> ListEntity2 = (List<Menu>)((ListEntity<Menu>)view2.Model).ListT;
            Menu menu2 = (from m in ListEntity2
                          where m.Name == "0000 test Edited"
                          select m).AsQueryable().FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited", menu2.Name);

        }



        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedMenuWithNameAndDayOfWeekChange()
        {

            // Act   
            Menu.DayOfWeek = DayOfWeek.Friday;

            ActionResult ar2 = Controller3.PostEdit((Menu)Menu);

            IMenu returnedMenu = Repo.GetById(Menu.ID);

            // Assert 
            Assert.AreEqual(DayOfWeek.Friday, returnedMenu.DayOfWeek);
        }



        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedMenuWithDescriptionChange()
        {
            // Arrange 
            Menu.Name = "0000 test";
            Menu.Description = "SaveEditedMenuWithDescriptionChange Pre-test";
            Menu.ID = int.MaxValue / 2; 

            // Act 
            ActionResult ar1 = Controller1.PostEdit((Menu)Menu);
            ViewResult view1 = Controller2.Index();
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
            ViewResult view2 = Controller4.Index();
            List<Menu> ListEntity2 = (List<Menu>)((ListEntity<Menu>)view2.Model).ListT;
            menu = (from m in ListEntity2
                    where m.Name == "0000 test Edited"
                    select m).AsQueryable().FirstOrDefault();


            // Assert
            Assert.AreEqual("0000 test Edited", menu.Name);
            Assert.AreEqual("SaveEditedMenuWithDescriptionChange Post-test", menu.Description);

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
            Menu Menu = new Menu(CreationDate);

            // Assert
            Assert.AreEqual(CreationDate, Menu.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);
            Menu Menu = new Menu(CreationDate)
            {
                Name = "001 Test ",
                ID = 290,
                Description = "test SaveTheCreationDateBetweenPostedEdits"
            };


          
            // Act
            Controller1.PostEdit(Menu);
            ViewResult view = Controller2.Index();
            List<Menu> ListEntity = (List<Menu>)((ListEntity<Menu>)view.Model).ListT;
            Menu menu = (from m in ListEntity
                         where m.Name == "001 Test "
                         select m).AsQueryable().FirstOrDefault();

            DateTime shouldBeSameDate = menu.CreationDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);

        }

        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            Menu.Name = "Test UpdateTheModificationDateBetweenPostedEdits";
            Menu.ID = 6000;
            Repo.Save((Menu)Menu);
            BaseUpdateTheModificationDateBetweenPostedEdits(Repo, Controller, (Menu)Menu);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexWithSuccessAttachAnExistingRecipeToAnExistingMenu()
        {
            // Arrange 
            IRepository<Recipe> repoRecipe = new TestRepository<Recipe>();
            IGenericController<Menu> ControllerAttach = new MenusController(Repo);
            IGenericController<Recipe> ControllerAttachI = new RecipesController(repoRecipe);
            IGenericController<Menu> ControllerCleanup = new MenusController(Repo);


            Menu menu = new Menu() { ID = 100, Description = "test AttachAnExistingRecipeToAnExistingMenu" };
            Repo.Add(menu);

            Recipe recipeVM = new Recipe() { ID = 101, Description = "test AttachAnExistingRecipeToAnExistingMenu" };
            repoRecipe.Add(recipeVM);
            // Act
            var x = ControllerAttach.AttachRecipe(menu.ID, recipeVM);

            // Assert 
            Assert.AreEqual(1, menu.Recipes.Count());
            // how do I know the correct recipe was added?
            Assert.AreEqual(recipeVM.ID, menu.Recipes.First().ID);


        }



        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void AttachAnExistingIngredientToAnExistingMenu()
        {
            // Arrange 
            TestRepository<Ingredient> repoIngredient = new TestRepository<Ingredient>();

            Menu menu = new Menu { ID = int.MaxValue - 100, Description = "test AttachAnExistingIngredientToAnExistingMenu" };
            Ingredient ingredient = new Ingredient { ID = int.MaxValue - 100, Description = "test AttachAnExistingIngredientToAnExistingMenu" };
            Repo.Add(menu);
            repoIngredient.Add(ingredient);

            // Act
            Controller.AttachIngredient(menu.ID, ingredient);
            Menu returnedMenu = (from m in Repo.GetAll()
                                 where m.Description == menu.Description
                                 select m).FirstOrDefault();

            // Assert 
            Assert.AreEqual(1, returnedMenu.Ingredients.Count());
            // how do I know the correct ingredient was added?
            Assert.AreEqual(ingredient.ID, returnedMenu.Ingredients.First().ID);

        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void NotDeleteAnIngredientAfterIngredientIsDetachedFromMenu()
        {
            // Arrange  
            Ingredient ingredient = new Ingredient { ID = int.MaxValue - 100, Description = "test NotDeleteAnIngredientAfterIngredientIsDetachedFromMenu" };

            // Act
            Controller.AttachIngredient(Menu.ID, ingredient);
            Controller.DetachIngredient(Menu.ID, ingredient);

            // Assert
            Assert.IsNotNull(ingredient);
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenDetachingNonExistingIngredientAttachedToANonExistingMenu()
        {
            // Arrange 

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.DetachIngredient(-1, null);

            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert

            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Menu was not found", adr.Message);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        }


        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingExistIngredientToNonExistingMenu()
        {
            // Arrange 

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.AttachIngredient(-1, null);

            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert

            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Menu was not found", adr.Message);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        }


        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingNonExistIngredientToNonExistingMenu()
        {
            // Arrange 

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.AttachIngredient(-1, null);

            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert

            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Menu was not found", adr.Message);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        }


        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNonExistingMenu()
        {
            // Arrange 

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.DetachIngredient(-1, null);

            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;
            var routeValues = rtrr.RouteValues.Values;

            // Assert
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual("Menu was not found", adr.Message);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnMenuEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingMenu() => Assert.Fail();

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnMenuEditViewWithWarningMessageWhenAttachingNonExistingIngredientToExistingRMenu() => Assert.Fail();

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnMenuEditViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToExistingMenu() => Assert.Fail();

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnMenuIndexViewWithWarningWhenDetachingExistingingredientNotAttachedToAnExistingMenu() => Assert.Fail(); 
    }
}

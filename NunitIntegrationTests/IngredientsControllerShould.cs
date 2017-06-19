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
using AutoMapper;

namespace MsTestIntegrationTests
{ 
    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("IngredientsController")]
    public class IngredientsControllerShould
    {
        [TestMethod]
        [TestCategory("Create")]
        public void CreateAnIngredient()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            IngredientsController controller = new IngredientsController(repo);
            // Act
            ViewResult vr = controller.Create(LambAndLentil.UI.UIViewType.Create);
            string modelName = ((IngredientVM)vr.Model).Name;
            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created");
        }

        [TestMethod]
        [TestCategory("Save")]
        public void SaveAValidIngredient()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            IngredientsController controller = new IngredientsController(repo);
            IngredientVM vm = new IngredientVM();
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
                Assert.AreEqual(UIControllerType.Ingredients.ToString(), routeValues.ElementAt(0).ToString());
                Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(1).ToString());
                Assert.AreEqual("Ingredients", routeValues.ElementAt(2).ToString());
                Assert.AreEqual(1.ToString(), routeValues.ElementAt(3).ToString());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Clean Up - should run a  delete test to make sure this works 
                List<Ingredient> items = repo.Ingredients.ToList<Ingredient>();
                Ingredient item = items.Where(m => m.Name == "test").FirstOrDefault();

                // Delete it
                controller.DeleteConfirmed(item.ID);
            }
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedIngredient()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            IngredientsController controller1 = new IngredientsController(repo);
            IngredientsController controller2 = new IngredientsController(repo);
            IngredientsController controller3 = new IngredientsController(repo);
            IngredientsController controller4 = new IngredientsController(repo);
            IngredientsController controller5 = new IngredientsController(repo);
            IngredientVM vm = new IngredientVM();
            vm.Name = "0000 test";

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM listVM = (ListVM)view1.Model;
            var result = (from m in listVM.Ingredients
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            Ingredient ingredient = result.FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("0000 test", ingredient.Name);

            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = ingredient.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM listVM2 = (ListVM)view2.Model;
            var result2 = (from m in listVM2.Ingredients
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable();

            ingredient = result2.FirstOrDefault();

            try
            {
                // Assert
                Assert.AreEqual("0000 test Edited", ingredient.Name);
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
        public void ActuallyDeleteAnIngredientFromTheDatabase()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            IngredientsController editController = new IngredientsController(repo);
            IngredientsController indexController = new IngredientsController(repo);
            IngredientsController deleteController = new IngredientsController(repo);
            IngredientVM vm = new IngredientVM();
            vm.Name = "0000" + new Guid().ToString();
            ActionResult ar = editController.PostEdit(vm);
            ViewResult view = indexController.Index();
            ListVM listVM = (ListVM)view.Model;
            var result = (from m in listVM.Ingredients
                          where m.Name == vm.Name
                          select m).AsQueryable();
            Ingredient item = result.FirstOrDefault();

            //Act
            deleteController.DeleteConfirmed(item.ID);
            var deletedItem = (from m in repo.Ingredients
                               where m.Name == vm.Name
                               select m).AsQueryable();

            //Assert
            Assert.AreEqual(0, deletedItem.Count());
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void ShouldSaveTheCreationDateOnIngredientCreationWithDateTimeParameter()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);

            // Act
            Ingredient ingredient = new Ingredient(CreationDate);

            // Assert
            Assert.AreEqual(CreationDate, ingredient.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnIngredientVMCreationWithNoParameterCtor()
        {
            // Arrange
            DateTime CreationDate = DateTime.Now;

            // Act
            IngredientVM ingredientVM = new IngredientVM();

            // Assert
            Assert.AreEqual(CreationDate.Date, ingredientVM.CreationDate.Date);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnIngredientVMCreationWithDateTimeParameter()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);

            // Act
            IngredientVM ingredientVM = new IngredientVM(CreationDate);

            // Assert
            Assert.AreEqual(CreationDate, ingredientVM.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);
            IngredientVM ingredientVM = new IngredientVM(CreationDate);
            ingredientVM.Name = "001 Test ";

            EFRepository repo = new EFRepository(); ;
            IngredientsController controllerEdit = new IngredientsController(repo);
            IngredientsController controllerView = new IngredientsController(repo);
            IngredientsController controllerDelete = new IngredientsController(repo);

            // Act
            controllerEdit.PostEdit(ingredientVM);
            ViewResult view = controllerView.Index();
            ListVM listVM = (ListVM)view.Model;
            var result = (from m in listVM.Ingredients
                          where m.Name == "001 Test "
                          select m).AsQueryable();

            Ingredient ingredient = result.FirstOrDefault();

            DateTime shouldBeSameDate = ingredient.CreationDate;
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
                controllerDelete.DeleteConfirmed(ingredient.ID);
            }
        }


        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            // Arrange
            EFRepository repo = new EFRepository();
            IngredientsController controllerPost = new IngredientsController(repo);
            IngredientsController controllerPost1 = new IngredientsController(repo);
            IngredientsController controllerView = new IngredientsController(repo);
            IngredientsController controllerView1 = new IngredientsController(repo);
            IngredientsController controllerDelete = new IngredientsController(repo);

            IngredientVM vm = new IngredientVM();
            vm.Name = "002 Test Mod";
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
            controllerPost.PostEdit(vm);

            ViewResult view = controllerView.Index();
            ListVM listVM = (ListVM)view.Model;
            var result = (from m in listVM.Ingredients
                          where m.Name == "002 Test Mod"
                          select m).AsQueryable();
            Ingredient ingredient = result.FirstOrDefault();
            vm = Mapper.Map<Ingredient, IngredientVM>(ingredient);

            vm.Description = "I've been edited to delay a bit";

            controllerPost1.PostEdit(vm);


            ViewResult view1 = controllerView.Index();
            listVM = (ListVM)view1.Model;
            var result1 = (from m in listVM.Ingredients
                           where m.Name == "002 Test Mod"
                           select m).AsQueryable();

            ingredient = result1.FirstOrDefault();

            DateTime shouldBeSameDate = ingredient.CreationDate;
            DateTime shouldBeLaterDate = ingredient.ModifiedDate;

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
                controllerDelete.DeleteConfirmed(ingredient.ID);
            }
        }

        // will need to test that we are not creating a second ingredient. Who is we??

        //[TestMethod]
        //[Ignore]
        //[TestCategory("Edit")]
        //public void IngredientsCtr_CanSaveEditedIngredientVerifyFieldIsEdited()
        //{
        //    // Arrange
        //    IngredientsController controller = SetUpController();

        //    Ingredient ingredient = mock.Object.Ingredients.First();
        //    mock.Setup(c => c.Save(It.IsAny<Ingredient>()));


        //    // leave this failing until I can figure out how to get Moq to work. 

        //    // Act 
        //    AutoMapperConfigForTests.InitializeMap();
        //    IngredientVM ingredientVM = Mapper.Map<Ingredient, IngredientVM>(ingredient);
        //    ingredientVM.Name = "First edited again 2";
        //    var view1 = controller.PostEdit(ingredientVM);

        //    //  IngredientVM p1 = (IngredientVM)view1.Model;

        //    // Assert 
        //    mock.Verify(foo => foo.Save(It.IsAny<Ingredient>()));
        //    //  mock.Verify();
        //    string name = mock.Object.Ingredients.First().Name;
        //    //   Assert.AreEqual("First edited again 2", name);
        //    // Assert.AreEqual(1, testSuccess);
        //}

        //[TestMethod]
        //[TestCategory("Edit")]
        //public void IngredientsCtr_CannotEditNonexistentIngredient()
        //{
        //    // Arrange
        //    IngredientsController controller = SetUpController();
        //    // Act
        //    Ingredient result = (Ingredient)controller.Edit(8).ViewData.Model;
        //    // Assert
        //    Assert.IsNull(result);
        //}
    }
}

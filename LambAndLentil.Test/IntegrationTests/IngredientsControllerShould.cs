using AutoMapper;
using IntegrationTests;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.Infrastructure
{
    [TestClass]
    [Ignore]   // switching to WebApi methods, but something will be needed
    [TestCategory("Integration")]
    [TestCategory("IngredientsController")]
    public class IT_IngredientsControllerShould
    {
        [TestMethod]
        [TestCategory("Create")]
        public void CreateAnIngredient()
        {
            // Arrange
            JSONRepository<Ingredient, IngredientVM> repoIngredient = new JSONRepository<Ingredient, IngredientVM>(); ;
            IngredientsController controller = new IngredientsController(repoIngredient);
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
            JSONRepository<Ingredient, IngredientVM> repoIngredient = new JSONRepository<Ingredient, IngredientVM>(); ;
            IngredientsController controller = new IngredientsController(repoIngredient);
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

                Ingredient item = repoIngredient.GetAllT().Where(m => m.Name == "test").FirstOrDefault();

                // Delete it
                controller.DeleteConfirmed(item.ID);
            }
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedIngredient()
        {
            // Arrange
            JSONRepository<Ingredient, IngredientVM> repoIngredient = new JSONRepository<Ingredient, IngredientVM>(); ;
            IngredientsController controller1 = new IngredientsController(repoIngredient);
            IngredientsController controller2 = new IngredientsController(repoIngredient);
            IngredientsController controller3 = new IngredientsController(repoIngredient);
            IngredientsController controller4 = new IngredientsController(repoIngredient);
            IngredientsController controller5 = new IngredientsController(repoIngredient);
            IngredientVM vm = new IngredientVM();
            vm.Name = "0000 test";

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM<Ingredient, IngredientVM> listVM = (ListVM<Ingredient, IngredientVM>)view1.Model;
            var result = (from m in listVM.Entities
                          where m.Name == "0000 test"
                          select m).AsQueryable().FirstOrDefault();

            IngredientVM ingredientVM = Mapper.Map<Ingredient, IngredientVM>(result);

            // verify initial value:
            Assert.AreEqual("0000 test", ingredientVM.Name);

            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = ingredientVM.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM<Ingredient, IngredientVM> listVM2 = (ListVM<Ingredient, IngredientVM>)view2.Model;
            var result2 = (from m in listVM2.Entities
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable().FirstOrDefault();

            ingredientVM = Mapper.Map<Ingredient, IngredientVM>(result2);

            try
            {
                // Assert
                Assert.AreEqual("0000 test Edited", ingredientVM.Name);
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

            JSONRepository<Ingredient, IngredientVM> repoIngredient = new JSONRepository<Ingredient, IngredientVM>();
            IngredientsController controller = new IngredientsController(repoIngredient);
            var item = new RecipesControllerShould().GetIngredient(repoIngredient, "test ActuallyDeleteAnIngredientFromTheDatabase");

            //Act
            controller.DeleteConfirmed(item.ID);
            var deletedItem = new RecipesControllerShould().GetIngredient(repoIngredient, "test ActuallyDeleteAnIngredientFromTheDatabase");

            //Assert
            Assert.IsNull(deletedItem);
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

            JSONRepository<Ingredient, IngredientVM> repoIngredient = new JSONRepository<Ingredient, IngredientVM>(); ;
            IngredientsController controllerEdit = new IngredientsController(repoIngredient);
            IngredientsController controllerView = new IngredientsController(repoIngredient);
            IngredientsController controllerDelete = new IngredientsController(repoIngredient);

            // Act
            controllerEdit.PostEdit(ingredientVM);
            ViewResult view = controllerView.Index();
            ListVM<Ingredient, IngredientVM> listVM = (ListVM<Ingredient, IngredientVM>)view.Model;
            var result = (from m in listVM.Entities
                          where m.Name == "001 Test "
                          select m).AsQueryable().FirstOrDefault();

            IngredientVM ingredientVM1 = Mapper.Map<Ingredient, IngredientVM>(result);

            DateTime shouldBeSameDate = ingredientVM1.CreationDate;
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
                controllerDelete.DeleteConfirmed(ingredientVM1.ID);
            }
        }


        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            // Arrange
            JSONRepository<Ingredient, IngredientVM> repoIngredient = new JSONRepository<Ingredient, IngredientVM>();
            IngredientsController controllerPost = new IngredientsController(repoIngredient);
            IngredientsController controllerPost1 = new IngredientsController(repoIngredient);
            IngredientsController controllerView = new IngredientsController(repoIngredient);
            IngredientsController controllerView1 = new IngredientsController(repoIngredient);
            IngredientsController controllerDelete = new IngredientsController(repoIngredient);

            IngredientVM vm = new IngredientVM();
            vm.Name = "002 Test Mod";
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
            controllerPost.PostEdit(vm);

            ViewResult view = controllerView.Index();
            ListVM<Ingredient, IngredientVM> listVM = (ListVM<Ingredient, IngredientVM>)view.Model;
            var result = (from m in listVM.Entities
                          where m.Name == "002 Test Mod"
                          select m).AsQueryable().FirstOrDefault();
            IngredientVM ingredientVM = Mapper.Map<Ingredient, IngredientVM>(result);


            ingredientVM.Description = "I've been edited to delay a bit";

            controllerPost1.PostEdit(ingredientVM);


            ViewResult view1 = controllerView.Index();
            listVM = (ListVM<Ingredient, IngredientVM>)view1.Model;
            var result1 = (from m in listVM.Entities
                           where m.Name == "002 Test Mod"
                           select m).AsQueryable().FirstOrDefault();

            IngredientVM ingredientVM2 = Mapper.Map<Ingredient, IngredientVM>(result1);

            DateTime shouldBeSameDate = ingredientVM2.CreationDate;
            DateTime shouldBeLaterDate = ingredientVM2.ModifiedDate;

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
                controllerDelete.DeleteConfirmed(ingredientVM.ID);
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
     //   [AssemblyCleanup]
        //public static void AssemblyCleanup()
        //{

        //    string path = @"../../../\LambAndLentil.Domain\App_Data\JSON\Ingredient\";
        //    File.Delete(String.Concat(path, "0.txt"));

        //    for (int i = 2; i < 8; i++)
        //    {
        //        File.Delete(String.Concat(path, 214748364, i, ".txt"));
        //    }

        //    for (int i = 2; i < 8; i++)
        //    {
        //        path = @"../../../\LambAndLentil.Domain\App_Data\JSON\Recipe\";
        //        File.Delete(String.Concat(path, 214748364, i, ".txt"));
        //    }
        //    try
        //    {
        //        path = @"../../../\LambAndLentil.Domain\App_Data\JSON\TestJSONRepositoryGetByID\";
        //        IEnumerable<string> files = Directory.EnumerateFiles(path);
        //        foreach (string file in files)
        //        {
        //            File.Delete(file);
        //        }

        //        Directory.Delete(path);
        //    }
        //    catch (FileNotFoundException)
        //    {
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    try
        //    {
        //        IEnumerable<string> files = Directory.EnumerateFiles(path);
        //        path = @"../../../\LambAndLentil.Domain\App_Data\JSON\TestReturnCountOfThreeForDirectoryWithThreeFiles\";
        //        files = (from file in Directory.EnumerateFiles(path, "*.txt")
        //                 from line in File.ReadLines(file)
        //                 select file).ToList();

        //        foreach (string file in files)
        //        {
        //            File.Delete(file);
        //        }
        //        Directory.Delete(path);
        //    }
        //    catch (FileNotFoundException)
        //    { 
        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    } 
        //}
    }
}

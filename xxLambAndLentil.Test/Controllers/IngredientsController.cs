using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.HtmlHelpers;
using LambAndLentil.UI.Models;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using Moq;
using System.Web.Routing;
using System.Reflection;
using LambAndLentil.Tests.Infrastructure;
using AutoMapper; 

namespace LambAndLentil.Tests.Controllers
{
    [TestClass]
    public class IngredientsControllerTest
    {
        public IngredientsControllerTest()
        {
            AutoMapperConfigForTests config = new AutoMapperConfigForTests();
        }

        [TestMethod]
        public void IngredientsControllerInheritsFromBaseControllerCorrectly()
        {

            // Arrange
            IngredientsController controller = SetUpController(); 
            // Act 
            controller.PageSize = 4;
    
            var type = typeof(IngredientsController);
            var DoesDisposeExist = type.GetMethod("Dispose");

            // Assert 
            Assert.AreEqual(4, controller.PageSize);
            Assert.IsNotNull(DoesDisposeExist); 
        }

        [TestMethod]
        public void IngredientsControllerIsPublic()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Act
            Type type = controller.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        }

        [TestMethod]
        public void IngredientsControllerAutoMapperIsConfigured()
        {
            AutoMapperConfigForTests config = new AutoMapperConfigForTests();
            //config.AssertConfigurationIsValid();
             
        }

        [TestMethod]
        public void IngredientsControllerIndex()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Act
            ViewResult result = controller.Index(null, null, null, null, 1) as ViewResult;
            ViewResult result1 = controller.Index("foo", "foo", "foo", "foo", 2) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result1);
        }

        [TestMethod]
        public void IngredientsControllerContainsAllIngredients()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            IngredientsListVM ilvm = new IngredientsListVM();
            ilvm.Ingredients = (IEnumerable<Ingredient>)controller.Ingredients;

            // Assert   
            IngredientsListVM result = (IngredientsListVM)(controller.Index(null, null, null, null, 1).Model);


            // Assert
            Assert.AreEqual(result.Ingredients.Count(), 5);
            Assert.AreEqual("P1", result.Ingredients.FirstOrDefault().ShortDescription);
            Assert.AreEqual("P2", result.Ingredients.Skip(1).FirstOrDefault().ShortDescription);
            Assert.AreEqual("P3", result.Ingredients.Skip(2).FirstOrDefault().ShortDescription);
        }

        [TestMethod]
        public void IngredientsControllerCanEditIngredient()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            IngredientVM ingVM = new IngredientVM();
            ingVM.IngredientID = 1;
            ingVM.ShortDescription = "P1";

            // Act 
            IngredientVM p1 = (IngredientVM)controller.Edit(1).ViewData.Model;
            IngredientVM p2 = (IngredientVM)controller.Edit(2).ViewData.Model;
            IngredientVM p3 = (IngredientVM)controller.Edit(3).ViewData.Model;
            // Assert
            Assert.AreEqual(1, p1.IngredientID);
            Assert.AreEqual(2, p2.IngredientID);
            Assert.AreEqual(3, p3.IngredientID);
            Assert.AreEqual("P1", p1.ShortDescription);
            Assert.AreEqual("P2", p2.ShortDescription);
            Assert.AreEqual("A", p1.Maker);
            Assert.AreEqual("B", p2.Maker);
            Assert.AreEqual("A", p3.Maker);


        }

        [TestMethod]
        public void IngredientsControllerCannotEditNonexistentIngredient()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            // Act
            Ingredient result = (Ingredient)controller.Edit(8).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void IngredientsControllerCreate_Simple()
        {
            // Arrange
            IngredientsController controller = SetUpController();


            // Act
            ViewResult result = controller.Create(null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IngredientsDelete()
        {
            // Arrange
            IngredientsController controller = SetUpController();


            // Act
            ViewResult result = controller.Delete(1) as ViewResult;
            ViewResult result1 = controller.Delete(null) as ViewResult;
            ViewResult result2 = controller.Delete(100) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result1.ViewName);
            Assert.AreEqual("Error", result2.ViewName);
        }

        [TestMethod]
        public void IngredientsDeleteConfirmed()
        {
            // Arrange
            IngredientsController controller = SetUpController();


            // Act
            ActionResult result = controller.DeleteConfirmed(1) as ActionResult;
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { controller = "Ingredients", Action = "Index" } } );
            // Assert 
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IngredientsDetails()
        {
            // Arrange
            IngredientsController controller = SetUpController();


            // Act
            ViewResult result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IngredientsEdit()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            ActionResult result = controller.Edit(1) as ActionResult;
            // improve this test when I actually write the method.

            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void IngredientsList()
        {
            // Arrange 
            IngredientsController controller = SetUpController();
            ActionResult result = controller.ListAscend(null, null, null, 1) as ActionResult;
            // improve this test  
            ViewResult result1 = controller.Index("foo", "foo", "foo", "foo", 2) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result1);
        }

        [TestMethod]
        public void IngredientsCanPaginate()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Act
            IngredientsListVM result1 = (IngredientsListVM)controller.ListAscend(null, null, null, 1).Model;
            IngredientsListVM result2 = (IngredientsListVM)controller.ListAscend(null, null, null, 2).Model;

            // Assert
            Ingredient[] ingrArray1 = result1.Ingredients.ToArray();
            Ingredient[] ingrArray2 = result2.Ingredients.ToArray();
            Assert.IsTrue(ingrArray1.Length == 4);
            Assert.IsTrue(ingrArray2.Length == 1);
            Assert.AreEqual(ingrArray1[0].ShortDescription, "P1");
            Assert.AreEqual(ingrArray1[3].ShortDescription, "P4");
            Assert.AreEqual(ingrArray2[0].ShortDescription, "P5");
        }

        [TestMethod]
        public void IngredientsCanGeneratePageLinks()
        {

            // Arrange - define an HTML helper - we need to do this
            // in order to apply the extension method
            HtmlHelper myHelper = null;

            // Arrange - create PagingInfo data
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Arrange - set up the delegate using a lambda expression
            Func<int, string> pageUrlDelegate = i => "Page" + i;
            //  Func<int, string> foo = i => "Page" + i;

            // Act 
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Assert
            Assert.AreEqual(1, 1);
            Assert.AreEqual(result.ToString(), @"<a href=""Page1"">1</a>"
                + @"<a class=""selected"" href=""Page2"">2</a>"
                + @"<a href=""Page3"">3</a>");
        }

        [TestMethod]
        public void IngredientsCanSendPaginationViewModel()
        {

            // Arrange
            IngredientsController controller = SetUpController();

            // Act
            IngredientsListVM resultL = (IngredientsListVM)controller.ListAscend(null, null, null, 2).Model;
            IngredientsListVM resultT = (IngredientsListVM)controller.Index(null, null, null, null, 2).Model;


            // Assert
            PagingInfo pageInfoL = resultL.PagingInfo;
            Assert.AreEqual(2, pageInfoL.CurrentPage);
            Assert.AreEqual(4, pageInfoL.ItemsPerPage);
            Assert.AreEqual(5, pageInfoL.TotalItems);
            Assert.AreEqual(2, pageInfoL.TotalPages);

            PagingInfo pageInfoT = resultL.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(4, pageInfoT.ItemsPerPage);
            Assert.AreEqual(5, pageInfoT.TotalItems);
            Assert.AreEqual(2, pageInfoT.TotalPages);
        }

        [TestMethod]
        public void IngredientCanFilterIngredientsOnMaker()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Action
            //TODO: check all four (ListAscend,ListDescend,IndexAscend,IndexDesc or whatever they are named)
            Ingredient[] result = ((IngredientsListVM)controller.ListAscend("B", "BB", null, 1).Model).Ingredients.ToArray();

            // Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue((result[0].ShortDescription == "P2") && result[0].Maker == "B");
            Assert.IsTrue(result[1].ShortDescription == "P4" && result[0].Maker == "B");
        }

        [TestMethod]
        public void IngredientCanFilterIngredientsOnBrand()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Action

            //TODO: check all four (ListAscend,ListDescend,IndexAscend,IndexDesc or whatever they are named)
            Ingredient[] result = ((IngredientsListVM)controller.ListAscend("B", "BB", null, 1).Model).Ingredients.ToArray();

            // Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue((result[0].ShortDescription == "P2") && result[0].Brand == "BB");
            Assert.IsTrue(result[1].ShortDescription == "P4" && result[0].Brand == "BB");
        }




        /// <summary>
        /// this will allow me to monitor the page size used in case I type over it
        /// </summary>
        [TestMethod]
        public void IngredientsControllerPageSizeHasNotChangedandIsValid()
        {
            // Arrange
            IngredientsController controller = SetUpSimpleController();
            var throwawayL = controller.ListAscend(null, null, null, 1);
            int pagesizeL = controller.PageSize;

            var throwayT = controller.Index(null, null, null, null, 1);
            int pagesizeT = controller.PageSize;
            // Act
            // nothing to see here, just move along

            // Assert
            Assert.AreEqual(pagesizeL, 4);
            Assert.AreEqual(pagesizeT, 8);
        }


        [TestMethod]
        public void IngredientsControllerGenerateMakerSpecificIngredientCount()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            controller.PageSize = 3;

            // Action
            int resA = ((IngredientsListVM)controller.ListAscend("A", "AA", null).Model).PagingInfo.TotalItems;
            int resB = ((IngredientsListVM)controller.ListAscend("B", "BB", null).Model).PagingInfo.TotalItems;
            int resC = ((IngredientsListVM)controller.ListAscend("C", "CC", null).Model).PagingInfo.TotalItems;
            int resAll = ((IngredientsListVM)controller.ListAscend(null, null, null).Model).PagingInfo.TotalItems;

            // Assert
            Assert.AreEqual(resA, 2);
            Assert.AreEqual(resB, 2);
            Assert.AreEqual(resC, 1);
            Assert.AreEqual(resAll, 5);
        }

        [TestMethod]
        /// The repository is not being called on a mock but I verified it is actually being called in regular code. I will leave all the code as is, commented out, for now, until I can really fix this. There is a Pluralsight course on Mocking with Moq now on my agenda. Leave this to fail so I don't forget it.
        public void IngredientsControllerCanSaveValidChanges()
        {
            // - create the mock repository
            Mock<IIngredientRepository> mock = new Mock<IIngredientRepository>();
            mock.Setup(m => m.Ingredients).Returns(new Ingredient[] {
                new Ingredient {IngredientID = 1, ShortDescription = "old sd 1", LongDescription="old LongD1",Maker="old maker A",Category="Dairy"},
                new Ingredient {IngredientID = 2, ShortDescription = "old sd 2", LongDescription="old LongD2",Maker="B",Category="Dairy"},
                new Ingredient {IngredientID = 3, ShortDescription = "old sd 3", LongDescription="old LongD3",Maker="A",Category="Meat"},
                new Ingredient {IngredientID = 4, ShortDescription = "old sd 4", LongDescription="old LongD4",Maker="B",Category="Fruit"},
                new Ingredient {IngredientID = 5, ShortDescription = "old sd 5", LongDescription="old LongD5",Maker="C",Category="Meat"}
            }.AsQueryable());

            // Arrange - create a controller
            IngredientsController controller = new IngredientsController(mock.Object);

            // Arrange - create a view model to change ingredient in repository
            IngredientVM ingVM = new IngredientVM();
            ingVM.IngredientID = 1;
            ingVM.ShortDescription = "edited sd 1";
            ingVM.Maker = "edited maker A";
            ingVM.LongDescription = "edited LongD1";
            ingVM.Category = "edited Dairy";

            //Arrange - create an ingredient
             Ingredient ingredient =  Mapper.Map<IngredientVM, Ingredient>(ingVM);
            mock.Setup(c => c.Save(ingredient)).Verifiable();
          
            // Act
            //Ingredient p3 = controller.Edit(3).ViewData.Model as Ingredient;
            //p3.ShortDescription = "New Short D for p3";
            ActionResult result = controller.Edit(ingVM);
  mock.Verify(x => x.Save(ingredient));
            
            // Assert - verify count is the same
            Assert.AreEqual(5, mock.Object.Ingredients.Count());
            //// Assert - verify changes were made and saved 
            // Assert.AreEqual("edited sd 1", mock.Object.Ingredients.First().ShortDescription);
            Assert.AreEqual("edited LongD1", mock.Object.Ingredients.First().LongDescription);

            //     Assert.AreEqual("m",mock.Object.Ingredients.First().Maker);
            // Assert - verify the repository was called
            //mock.Setup(m => m.SaveIngredient(ingredient)).Verifiable();
             mock.Verify(m => m.Save(ingredient),Times.Once);
            //mock.Verify(m => m.Ts);

            //Assert - check the method result type
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            //Assert.IsTrue(true, "throw away test");
        }

        [TestMethod]
        public void Ingredients_Controller_Can_Save_Valid_Changes()
        {

            // Arrange - create mock repository
            Mock<IIngredientRepository> mock = new Mock<IIngredientRepository>();
            // Arrange - create the controller
            IngredientsController target = new IngredientsController(mock.Object);
            // Arrange - create an ingredientVM
            IngredientVM ingredientVM = new IngredientVM { Name = "Test" };
            Ingredient ingredient = new Ingredient();
            ingredient.Name = ingredientVM.Name;
            // Act - try to save the product
            ActionResult result = target.Edit(ingredientVM);

            //mine
            mock.Setup(foo => foo.Save(ingredient)).Verifiable();

            // Assert - check that the repository was called
            mock.Verify(m => m.Save(ingredient));
            // Assert - check the method result type
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }



        [TestMethod]
        public void IngredientsControllerCanDeleteValidProducts()
        {
            // Arrange - create an ingredient
            Ingredient ingredient = new Ingredient { IngredientID = 2, ShortDescription = "Test" };

            // Arrange - create the mock repository
            Mock<IIngredientRepository> mock = new Mock<IIngredientRepository>();
            mock.Setup(m => m.Ingredients).Returns(new Ingredient[]
            {
                new Ingredient {IngredientID=1,ShortDescription="P1"},
                ingredient,
                new Ingredient {IngredientID=3,ShortDescription="P3"},
            }.AsQueryable());

            // Arrange - create the controller
            IngredientsController controller = new IngredientsController(mock.Object);

            // Act - delete the ingredient
            controller.DeleteConfirmed(ingredient.IngredientID);

            // Assert - ensure that the repository delete method was called with a correct Ingredient
            mock.Verify(m => m.DeleteIngredient(ingredient.IngredientID));

        }

        #region private methods
        private IngredientsController SetUpController()
        {
            // - create the mock repository
            Mock<IIngredientRepository> mock = new Mock<IIngredientRepository>();
            mock.Setup(m => m.Ingredients).Returns(new Ingredient[] {
                new Ingredient {IngredientID = 1, ShortDescription = "P1", Maker="A",Brand="AA"},
                new Ingredient {IngredientID = 2, ShortDescription = "P2", Maker="B",Brand="BB"},
                new Ingredient {IngredientID = 3, ShortDescription = "P3", Maker="A",Brand="AA"},
                new Ingredient {IngredientID = 4, ShortDescription = "P4", Maker="B",Brand="BB"},
                new Ingredient {IngredientID = 5, ShortDescription = "P5", Maker="C",Brand="CC"}
            }.AsQueryable());

            // Arrange - create a controller
            IngredientsController controller = new IngredientsController(mock.Object);
            controller.PageSize = 3;

            return controller;
        }

        private IngredientsController SetUpSimpleController()
        {
            // - create the mock repository
            Mock<IIngredientRepository> mock = new Mock<IIngredientRepository>();


            // Arrange - create a controller
            IngredientsController controller = new IngredientsController(mock.Object);
            // controller.PageSize = 3;

            return controller;

        }
        #endregion
    }
}

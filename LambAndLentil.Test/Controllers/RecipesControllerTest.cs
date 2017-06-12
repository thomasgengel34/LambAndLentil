using LambAndLentil.UI.Controllers;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.UI.Models;
using System.Collections.Generic;
using AutoMapper;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI;

namespace LambAndLentil.Tests.Controllers
{
    [TestClass]
    [TestCategory("RecipesController")]
    public class RecipesControllerTest
    {
        static Mock<IRepository> mock;
        public static MapperConfiguration AutoMapperConfig { get; set; }

        public RecipesControllerTest()
        {
            //    AutoMapperConfig = AutoMapperConfigForTests.AMConfigForTests();
            AutoMapperConfigForTests.InitializeMap();

        }



        [TestMethod]
        public void RecipesCtr_InheritsFromBaseController()
        {
            // Arrange
            RecipesController testController = SetUpController();

            // Act 
            Type baseType = typeof(BaseController);
            bool isBase = baseType.IsInstanceOfType(testController);

            // Assert 
            Assert.AreEqual(isBase, true);
        }



        [TestMethod]
        public void RecipesCtr_IsPublic()
        {
            // Arrange
            RecipesController testController = SetUpController();

            // Act
            Type type = testController.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        }

        [TestMethod]
        public void RecipesCtr_Index()
        {
            // Arrange
            RecipesController controller = SetUpController();

            // Act
            ViewResult result = controller.Index(1) as ViewResult;
            ViewResult result1 = controller.Index(2) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result1);
        }

        [TestMethod]
        public void RecipesCtr_Index_ContainsAllRecipes()
        {
            // Arrange
            RecipesController controller = SetUpController();


            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).Recipes.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM)(view2.Model)).Recipes.Count();

            int count = count1 + count2;

            // Assert
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
            Assert.AreEqual(5, count1);
            Assert.AreEqual(0, count2);
            Assert.AreEqual(5, count);
            Assert.AreEqual("Index", view1.ViewName);
            Assert.AreEqual("Index", view2.ViewName); 
        }

        [TestMethod]
        public void RecipesCtr_Index_CanSendPaginationViewModel()
        {

            // Arrange
            RecipesController controller = SetUpController();

            // Act

            ListVM resultT = (ListVM)((ViewResult)controller.Index(2)).Model;


            // Assert

            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(5, pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }


        [TestMethod]
        public void RecipesCtr_Index_FirstPageIsCorrect()
        {
            // Arrange
            RecipesController controller = SetUpController();
#pragma warning disable IDE0017 // Simplify object initialization
            ListVM ilvm = new  ListVM();
#pragma warning restore IDE0017 // Simplify object initialization
            ilvm.Recipes = (IEnumerable<Recipe>)mock.Object.Recipes;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).Recipes.Count();



            // Assert
            Assert.IsNotNull(view1);
            Assert.AreEqual(5, count1);
            Assert.AreEqual("Index", view1.ViewName);

            Assert.AreEqual("oldRecipe1", ((ListVM)(view1.Model)).Recipes.FirstOrDefault().Name);
            Assert.AreEqual("oldRecipe2", ((ListVM)(view1.Model)).Recipes.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual("oldRecipe3", ((ListVM)(view1.Model)).Recipes.Skip(2).FirstOrDefault().Name);


        }

        [TestMethod]
        public void RecipesCtr_Index_PagingInfoIsCorrect()
        {
            // Arrange
            RecipesController controller = SetUpController();


            // Action
            int totalItems = ((ListVM)((ViewResult)controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = ((ListVM)((ViewResult)controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = ((ListVM)((ViewResult)controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = ((ListVM)((ViewResult)controller.Index()).Model).PagingInfo.TotalPages;



            // Assert
            Assert.AreEqual(5, totalItems);
            Assert.AreEqual(1, currentPage);
            Assert.AreEqual(8, itemsPerPage);
            Assert.AreEqual(1, totalPages);
        }

        [TestMethod]
        // currently we only have one page here
        public void RecipesCtr_Index_SecondPageIsCorrect()
        {
            //// Arrange
            //RecipesController controller = SetUpController();
            //ListVM ilvm = new ListVM();
            //ilvm.Recipes = (IEnumerable<Recipe>)mock.Object.Recipes;

            //// Act
            //ViewResult view  = controller.Index(null, null, null, 2);

            //int count  = ((ListVM)(view.Model)).Recipes.Count(); 

            //// Assert
            //Assert.IsNotNull(view);
            //Assert.AreEqual(0, count );
            //Assert.AreEqual("Index", view.ViewName); 
            // Assert.AreEqual("P5", ((ListVM)(view.Model)).Recipes.FirstOrDefault().Name);
            // Assert.AreEqual( 5, ((ListVM)(view.Model)).Recipes.FirstOrDefault().ID);
            // Assert.AreEqual("C", ((ListVM)(view.Model)).Recipes.FirstOrDefault().Maker);
            // Assert.AreEqual("CC", ((ListVM)(view.Model)).Recipes.FirstOrDefault().Brand);

        }

        [TestMethod]
        public void RecipesCtr_IndexCanPaginate()
        {
            // Arrange
            RecipesController controller = SetUpController();

            // Act
            var result = (ListVM)(controller.Index(1)).Model;



            // Assert
            Recipe[] ingrArray1 = result.Recipes.ToArray();
            Assert.IsTrue(ingrArray1.Length == 5);
            Assert.AreEqual("oldRecipe1", ingrArray1[0].Name);
            Assert.AreEqual("oldRecipe4", ingrArray1[3].Name);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipesCtr_DetailsRecipeIDIsNegative()
        {
            // Arrange
            RecipesController controller = SetUpController();
            //AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No recipe was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIControllerType.Recipes.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipesCtr_DetailsWorksWithValidRecipeID()
        {
            // Arrange
            RecipesController controller = SetUpController();
            //AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(1) as ViewResult;
            // Assert
            Assert.IsNotNull(view);

            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(LambAndLentil.UI.Models.RecipeVM));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipesCtr_DetailsRecipeIDTooHigh()
        {
            // Arrange
            RecipesController controller = SetUpController();
            //AutoMapperConfigForTests.AMConfigForTests();
            ActionResult view = controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No recipe was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIControllerType.Recipes.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipesCtr_DetailsRecipeIDPastIntLimit()
        {
            // Arrange
            RecipesController controller = SetUpController();
            //AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult result = controller.Details(Int16.MaxValue + 1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipesCtr_DetailsRecipeIDIsZero()
        {
            // Arrange
            RecipesController controller = SetUpController();
            //AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No recipe was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIControllerType.Recipes.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Create")]
        public void RecipesCtr_Create()
        {
            // Arrange
            RecipesController controller = SetUpController();
            ViewResult view = controller.Create(UIViewType.Edit);


            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void RecipesCtr_DeleteAFoundRecipe()
        {
            // Arrange
            RecipesController controller = SetUpController();

            // Act 
            var view = controller.Delete(1) as ViewResult;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual(UIViewType.Details.ToString(), view.ViewName);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void RecipesCtr_DeleteAnInvalidRecipe()
        {
            // Arrange
            RecipesController controller = SetUpController();

            // Act 
            var view = controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No recipe was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIControllerType.Recipes.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void RecipesCtr_DeleteConfirmed()
        {
            // Arrange
            RecipesController controller = SetUpController();
            // Act
            ActionResult result = controller.DeleteConfirmed(1) as ActionResult;
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { controller = "Recipes", Action = "Index" } } );
            // Assert 
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void Recipes_Ctr_CanDeleteValidRecipe()
        {
            // Arrange - create an recipe
            Recipe recipe = new Recipe { ID = 2, Name = "Test2" };

            // Arrange - create the mock repository
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.Recipes).Returns(new Recipe[]
            {
                new Recipe {ID=1,Name="Test1"},

                recipe,

                new Recipe {ID=3,Name="Test3"},
            }.AsQueryable());
            mock.Setup(m => m.Delete<Recipe>(It.IsAny<int>())).Verifiable();
            // Arrange - create the controller
            RecipesController controller = new RecipesController(mock.Object);

            // Act - delete the recipe
            ActionResult result = controller.DeleteConfirmed(recipe.ID);

            AlertDecoratorResult adr = (AlertDecoratorResult)result;

            // Assert - ensure that the repository delete method was called with a correct Recipe
            mock.Verify(m => m.Delete<Recipe>(recipe.ID));


            Assert.AreEqual("Test2 has been deleted", adr.Message);
        }

        [TestMethod]
        [TestCategory("Index")]
        // currently we only have one page here
        public void IngredientsCtr_Index_SecondPageIsCorrect()
        {
            //TODO: add enough test ingredients to test the second page
        }


        [TestMethod]
        [TestCategory("Edit")]
        public void RecipesCtr_CanEditRecipe()
        {
            // Arrange
            RecipesController controller = SetUpController();

            Recipe recipe = mock.Object.Recipes.First();
            mock.Setup(c => c.Save(recipe)).Verifiable();
            recipe.Name = "First edited";

            // Act 

            ViewResult view1 = controller.Edit(1);
            RecipeVM p1 = (RecipeVM)view1.Model;
            ViewResult view2 = controller.Edit(2);
            RecipeVM p2 = (RecipeVM)view2.Model;
            ViewResult view3 = controller.Edit(3);
            RecipeVM p3 = (RecipeVM)view3.Model;


            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual(1, p1.ID);
            Assert.AreEqual(2, p2.ID);
            Assert.AreEqual(3, p3.ID);
            Assert.AreEqual("First edited", p1.Name);
            Assert.AreEqual("oldRecipe2", p2.Name); 
        }
         

        [TestMethod]
        [TestCategory("Edit")]
        public void RecipesCtr_CannotEditNonexistentRecipe()
        {
            //    // Arrange
            //    RecipesController controller = SetUpController();
            //    // Act
            //    Recipe result = (Recipe)controller.Edit(8).ViewData.Model;
            //    // Assert
            //    Assert.IsNull(result);
            //}

            //[TestMethod]
            //public void RecipesCtr_CreateReturnsNonNull()
            //{
            //    // Arrange
            //    RecipesController controller = SetUpController();


            //    // Act
            //    ViewResult result = controller.Create(null) as ViewResult;

            //    // Assert
            //    Assert.IsNotNull(result);
        }




        private RecipesController SetUpController()
        {
            // - create the mock repository
            mock = new Mock<IRepository>();
            mock.Setup(m => m.Recipes).Returns(new Recipe[] {
                new Recipe {ID = 1, Name = "oldRecipe1", Description="old Description A", ModifiedByUser="UserA"},
                new Recipe {ID = 2, Name = "oldRecipe2", Description= "old Description B", ModifiedByUser= "UserB" },
                new Recipe {ID = 3, Name = "oldRecipe3", Description= "old Description rA", ModifiedByUser= "UserA" },
                new Recipe {ID = 4, Name = "oldRecipe4", Description= "old Description B", ModifiedByUser= "UserB" },
                new Recipe {ID = 5, Name = "oldRecipe5", Description= "old Description C", ModifiedByUser= "UserC" }
            }.AsQueryable());

            // Arrange - create a controller
 
            RecipesController controller = new RecipesController(mock.Object);
 
            controller.PageSize = 3;
            return controller;
        }

        private RecipesController SetUpSimpleController()
        {
            // - create the mock repository
            Mock<IRepository> mock = new Mock<IRepository>();


            // Arrange - create a controller
            RecipesController controller = new RecipesController(mock.Object);
            // controller.PageSize = 3;

            return controller;
        } 
    }
}

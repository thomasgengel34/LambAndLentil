using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.HtmlHelpers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Tests.Controllers
{
    [TestClass]
    [TestCategory("IngredientsController")]
    public class IngredientsController_Delete_Test
    {
        static Mock<IRepository> mock;
        public static MapperConfiguration AutoMapperConfig { get; set; }
        public IngredientsController_Delete_Test()
        {
            AutoMapperConfigForTests.InitializeMap();
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void IngredientsCtr_Delete_IsCalled()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            mock.Setup(foo => foo.Delete<Ingredient>(It.IsAny<Int32>()));

            // Act 
            var view = controller.DeleteConfirmed(1) as ViewResult;

            // Assert
            mock.Verify(foo => foo.Delete<Ingredient>(It.IsAny<Int32>()));
        }


        [TestMethod]
        [TestCategory("Delete")]
        public void IngredientsCtr_DeleteAFoundIngredient_ViewIsNotNull()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Act 
            var view = controller.Delete(1) as ViewResult;

            // Assert
            Assert.IsNotNull(view); 
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void IngredientsCtr_DeleteAFoundIngredient_ViewNameIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Act 
            var view = controller.Delete(1) as ViewResult;

            // Assert 
            Assert.AreEqual(UIViewType.Details.ToString(), view.ViewName);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void IngredientsCtr_Delete_AnInvalidIngredient_ViewNotNull()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Act 
            var view = controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view); 
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void IngredientsCtr_Delete_AnInvalidIngredient_AlertMessageCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Act 
            var view = controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert 
            Assert.AreEqual("No ingredient was found with that id.", adr.Message); 
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void IngredientsCtr_Delete_AnInvalidIngredient_AlertClassCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Act 
            var view = controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert  
            Assert.AreEqual("alert-danger", adr.AlertClass); 
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void IngredientsCtr_Delete_AnInvalidIngredient_RouteClassCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Act 
            var view = controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert   
            Assert.AreEqual(UIControllerType.Ingredients.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString()); 
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void IngredientsCtr_Delete_AnInvalidIngredient_RouteControllerCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Act 
            var view = controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert    
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString()); 
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void IngredientsCtr_Delete_AnInvalidIngredient_RouteValuesCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();

            // Act 
            var view = controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert    
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void IngredientsCtr_DeleteConfirmed_ResultIsNotNull()
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
        [TestCategory("Delete")]
        public void Ingredients_Ctr_DeleteValidIngredient_DeleteWasCalled()
        {
            // Arrange - create an ingredient
            Ingredient ingredient = new Ingredient { ID = 2, Name = "Test2" };

            // Arrange - create the mock repository
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.Ingredients).Returns(new Ingredient[]
            {
                new Ingredient {ID=1,Name="Test1"},

                ingredient,

                new Ingredient {ID=3,Name="Test3"},
            }.AsQueryable());
            mock.Setup(m => m.Delete<Ingredient>(It.IsAny<int>())).Verifiable();
            // Arrange - create the controller
            IngredientsController controller = new IngredientsController(mock.Object);

            // Act - delete the ingredient
            ActionResult result = controller.DeleteConfirmed(ingredient.ID);

            AlertDecoratorResult adr = (AlertDecoratorResult)result;

            // Assert - ensure that the repository delete method was called with a correct Ingredient
            mock.Verify(m => m.Delete<Ingredient>(ingredient.ID)); 
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void Ingredients_Ctr_DeleteValidIngredient_MessageCorrect()
        {
            // Arrange - create an ingredient
            Ingredient ingredient = new Ingredient { ID = 2, Name = "Test2" };

            // Arrange - create the mock repository
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.Ingredients).Returns(new Ingredient[]
            {
                new Ingredient {ID=1,Name="Test1"},

                ingredient,

                new Ingredient {ID=3,Name="Test3"},
            }.AsQueryable());
            mock.Setup(m => m.Delete<Ingredient>(It.IsAny<int>())).Verifiable();
            // Arrange - create the controller
            IngredientsController controller = new IngredientsController(mock.Object);

            // Act - delete the ingredient
            ActionResult result = controller.DeleteConfirmed(ingredient.ID);

            AlertDecoratorResult adr = (AlertDecoratorResult)result; 
            Assert.AreEqual("Test2 has been deleted", adr.Message);
        }

        private IngredientsController SetUpController()
        {
            mock = new Mock<IRepository>();
            mock.Setup(m => m.Ingredients).Returns(new Ingredient[] {
                new Ingredient {ID = 1, Name = "P1", Maker="Maker1",Brand="BrandAA",AddedByUser="John Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Ingredient {ID = 2, Name = "P2", Maker="Maker2",Brand="BrandB",AddedByUser="Sally Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new Ingredient {ID = 3, Name = "P3", Maker="Maker1",Brand="BrandAA",AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new Ingredient {ID = 4, Name = "P4", Maker="Maker2",Brand="BrandB",AddedByUser="Kyle Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Ingredient {ID = 5, Name = "P5", Maker="Maker3",Brand="BrandC",AddedByUser="John Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            }.AsQueryable());

            IngredientsController controller = new IngredientsController(mock.Object);
            controller.PageSize = 3;

            return controller;
        }

        private IngredientsController SetUpSimpleController()
        {
            Mock<IRepository> mock = new Mock<IRepository>();
            IngredientsController controller = new IngredientsController(mock.Object);
            return controller;
        }
    }
}

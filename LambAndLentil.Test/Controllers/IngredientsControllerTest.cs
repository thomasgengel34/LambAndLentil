using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Tests.Controllers
{
    [TestClass]
    [TestCategory("IngredientsController")]
    public class IngredientsControllerShould
    {
        static Mock<IRepository<Ingredient,IngredientVM>> mock;
        public static MapperConfiguration AutoMapperConfig { get; set; }
        public IngredientsControllerShould()
        {
            AutoMapperConfigForTests.InitializeMap();
            mock = new Mock<IRepository<Ingredient,IngredientVM>>();
        }

        [TestMethod]
        public void  InheritFromBaseControllerCorrectlyPageSizeRight()
        {

            // Arrange
            IngredientsController controller = SetUpController();
            // Act 
            controller.PageSize = 4;

            var type = typeof(IngredientsController);
            var DoesDisposeExist = type.GetMethod("Dispose");

            // Assert 
            Assert.AreEqual(4, controller.PageSize);
        }

        [TestMethod]
        public void  InheritFromBaseControllerCorrectlyDisposeExists()
        {

            // Arrange
            IngredientsController controller = SetUpController();
            // Act 
            controller.PageSize = 4;

            var type = typeof(IngredientsController);
            var DoesDisposeExist = type.GetMethod("Dispose");

            // Assert  
            Assert.IsNotNull(DoesDisposeExist);
        }

        [TestMethod]
        public void BePublic()
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
        public void AutoMapperIsConfigured()
        {
            AutoMapperConfigForTests.InitializeMap();
            MapperConfiguration AutoMapperConfig = AutoMapperConfigForTests.AMConfigForTests();
            AutoMapperConfig.AssertConfigurationIsValid(); 
        }
         
 
  
        [TestMethod]
        [TestCategory("Details")]
        public void  DetailsWorksWithValidIngredientID()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //     AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(1) as ViewResult;
            // Assert
            Assert.IsNotNull(view);

            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(LambAndLentil.UI.Models.IngredientVM));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  DetailsIngredientIDTooHighViewNotNull()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //  AutoMapperConfigForTests.AMConfigForTests();
            ActionResult view = controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  DetailsIngredientIDTooHighMessageRight()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //  AutoMapperConfigForTests.AMConfigForTests();
            ActionResult view = controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert 
            Assert.AreEqual("No ingredient was found with that id.", adr.Message);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  DetailsIngredientIDTooHighAlertClassCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //  AutoMapperConfigForTests.AMConfigForTests();
            ActionResult view = controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert  
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDTooHighCorrectModel()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //  AutoMapperConfigForTests.AMConfigForTests();
            ActionResult view = controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert   
            Assert.AreEqual(UIControllerType.Ingredients.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  DetailsIngredientIDTooHighCorrectController()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //  AutoMapperConfigForTests.AMConfigForTests();
            ActionResult view = controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert    
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  DetailsIngredientIDTooHighCorrectRouteValue()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //  AutoMapperConfigForTests.AMConfigForTests();
            ActionResult view = controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert     
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  DetailsIngredientIDPastIntLimit()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //  AutoMapperConfigForTests.AMConfigForTests(); 

            // Act
            ViewResult result = controller.Details(Int16.MaxValue + 1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  DetailsIngredientIDIsZeroViewIsNotNull()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //    AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  DetailsIngredientIDIsZeroMessageIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //    AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert 
            Assert.AreEqual("No ingredient was found with that id.", adr.Message);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  DetailsIngredientIDIsZeroAlertClassIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //    AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert  
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  DetailsIngredientIDIsZeroReturnModelIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //    AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert   
            Assert.AreEqual(UIControllerType.Ingredients.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  DetailsIngredientIDIsZeroReturnControllerIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //    AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert    
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  DetailsIngredientIDIsZeroReturnRouteValueIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //    AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert     
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  Details_IngredientIDIsNegative_ResultNotNull()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //    AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  Details_IngredientIDIsNegative_MessageCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //    AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert   adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            //Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
            Assert.AreEqual("No ingredient was found with that id.", adr.Message);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  Details_IngredientIDIsNegative_AlertClassCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //    AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert  
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  Details_IngredientIDIsNegative_RouteClassCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //    AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert   
            Assert.AreEqual(UIControllerType.Ingredients.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }


        [TestMethod]
        [TestCategory("Details")]
        public void  Details_IngredientIDIsNegative_ControllerIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //    AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert    
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  Details_IngredientIDIsNegative_RouteValuesCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            //    AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert    
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }
        // the following are not really testable.  I am keeping them to remind me of that.
        //[TestMethod]
        //public void IngredientsCtr_DetailsIngredientIDIsNotANumber() { }

        //[TestMethod]
        //public void IngredientsCtr_DetailsIngredientIDIsNotAInteger() { } 
 

        private IngredientsController SetUpController()
        {
             mock = new Mock<IRepository<Ingredient,IngredientVM>>();
            mock.Setup(m => m.Ingredient).Returns(new Ingredient[] {
                new Ingredient {ID = 1, Name = "P1",  ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Ingredient {ID = 2, Name = "P2" , ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new Ingredient {ID = 3, Name = "P3" },
                new Ingredient {ID = 4, Name = "P4" },
                new Ingredient {ID = 5, Name = "P5" }
            }.AsQueryable());

            IngredientsController controller = new IngredientsController(mock.Object);
            controller.PageSize = 3;

            return controller;
        }

        private IngredientsController SetUpSimpleController()
        {
             mock = new Mock<IRepository<Ingredient, IngredientVM>>();
            IngredientsController controller = new IngredientsController(mock.Object);
            return controller;
        } 
    }
}

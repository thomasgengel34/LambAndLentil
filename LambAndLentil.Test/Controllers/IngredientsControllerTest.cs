using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Tests.Controllers
{

    [TestClass]
    [TestCategory(" IngredientsController")]
    public class IngredientsControllerShould
    {
        static Mock<IRepository<Ingredient, IngredientVM>> mock;
       // static string path = @"../../../\LambAndLentil.Test\App_Data\JSON\Ingredient\";
        private static IRepository<Ingredient, IngredientVM> repo { get; set; }
        public static MapperConfiguration AutoMapperConfig { get; set; }

        public IngredientsControllerShould()
        {
            AutoMapperConfigForTests.InitializeMap();
            mock = new Mock<IRepository<Ingredient, IngredientVM>>();
            repo = new TestRepository<Ingredient, IngredientVM>();
        }

        [TestMethod]
        public void InheritFromBaseControllerCorrectlyPageSizeRight()
        {

            // Arrange
            IngredientsController controller = SetUpControllerWithNoMock();
            // Act 
            controller.PageSize = 4;

            var type = typeof(IngredientsController);
            var DoesDisposeExist = type.GetMethod("Dispose");

            // Assert 
            Assert.AreEqual(4, controller.PageSize);
        }



        [TestMethod]
        public void InheritFromBaseControllerCorrectlyDisposeExists()
        {

            // Arrange
            IngredientsController controller = SetUpControllerWithNoMock();
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
            IngredientsController controller = SetUpControllerWithNoMock();

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
        public void DetailsWorksWithValidIngredientID()
        {
            // Arrange
            IngredientsController controller = new IngredientsController(repo);
            Ingredient ingredient = new Ingredient { ID = 1 };
            repo.SaveT(ingredient);


            // Act
            ActionResult ar = controller.Details(1);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;
            // Assert
            Assert.IsNotNull(ar);

            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(IngredientVM));
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDTooHighViewNotNull()
        {  // not sure what the desired behavior is yet
           // Arrange
            IngredientsController controller = SetUpControllerWithNoMock();
            //  AutoMapperConfigForTests.AMConfigForTests();
            ActionResult view = controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDTooHighMessageRight()
        {// not sure what the desired behavior is yet
         // Arrange
            IngredientsController controller = SetUpControllerWithNoMock();
     
            ActionResult ar = controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            // Assert 
            Assert.AreEqual("No ingredient was found with that id.", adr.Message);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDTooHighAlertClassCorrect()
        {   // not sure what the desired behavior is yet
            // Arrange
            IngredientsController controller = SetUpControllerWithNoMock();
            //  AutoMapperConfigForTests.AMConfigForTests();
            ActionResult view = controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert  
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDTooHighCorrectModel()
        {
            // Arrange
            IngredientsController controller = SetUpControllerWithNoMock();
            //  AutoMapperConfigForTests.AMConfigForTests();
            ActionResult view = controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert   
            Assert.AreEqual(UIControllerType.Ingredients.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDTooHighCorrectController()
        {
            // Arrange
            IngredientsController controller = SetUpControllerWithNoMock();
            //  AutoMapperConfigForTests.AMConfigForTests();
            ActionResult view = controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert    
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDTooHighCorrectRouteValue()
        {
            // Arrange
            IngredientsController controller = SetUpControllerWithNoMock();
            //  AutoMapperConfigForTests.AMConfigForTests();
            ActionResult view = controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert     
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDPastIntLimit()
        {
            // I am not sure how I want this to operate.  Wait until UI is set up and see then.
            // Arrange
            IngredientsController controller = SetUpControllerWithNoMock();
            //  AutoMapperConfigForTests.AMConfigForTests(); 

            // Act
            ViewResult result = controller.Details(int.MaxValue) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDIsZeroViewIsNotNull()
        {
            // Arrange
            IngredientsController controller = new IngredientsController(repo);
            Ingredient ingredient = new Ingredient { ID = 0 };
            repo.SaveT(ingredient);

            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDIsZeroMessageIsCorrect()
        {
            // Arrange
            IngredientsController controller = new IngredientsController(repo);
            Ingredient ingredient = new Ingredient { ID = 0 };
            repo.SaveT(ingredient);


            // Act
            ActionResult ar = controller.Details(0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            // Assert 
            Assert.AreEqual("Something is wrong with the data!", adr.Message);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDIsZeroAlertClassIsCorrect()
        {
            // Arrange
            IngredientsController controller = new IngredientsController(repo);
            Ingredient ingredient = new Ingredient { ID = 0 };
            repo.SaveT(ingredient);


            // Act
            ActionResult ar = controller.Details(0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            // Assert  
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsIngredientIDIsZeroReturnModelIsCorrect()
        {
            // Arrange
            IngredientsController controller = new IngredientsController(repo);
            Ingredient ingredient = new Ingredient { ID = 0 };
            repo.SaveT(ingredient);


            // Act
            ActionResult ar = controller.Details(0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            // Assert    
            Assert.IsNull(adr.Model);
        }


        [TestMethod]
        [TestCategory("Details")]
        public void Details_IngredientIDIsNegative_ResultNotNull()
        {
            // Arrange
            IngredientsController controller = new IngredientsController(repo);
            Ingredient ingredient = new Ingredient { ID = -1 };
            repo.SaveT(ingredient);

            // Act
            ActionResult view = controller.Details(-1);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void Details_IngredientIDIsNegative_MessageCorrect()
        {
            // Arrange
            IngredientsController controller = new IngredientsController(repo);
            Ingredient ingredient = new Ingredient { ID = int.MaxValue };
            repo.SaveT(ingredient);

            // Act
            ActionResult ar = controller.Details(int.MaxValue);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            // Assert   adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            //Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
            Assert.AreEqual("Something is wrong with the data!", adr.Message);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void Details_IngredientIDIsNegative_AlertClassCorrect()
        {
            // Arrange
            IngredientsController controller = new IngredientsController(repo);
            Ingredient ingredient = new Ingredient
            {
                ID = -1,
                Name = "Details_IngredientIDIsNegative_AlertClassCorrect"
            };
            repo.SaveT(ingredient);

            // Act
            ActionResult view = controller.Details(-1);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert  
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }


        // the following are not really testable.  I am keeping them to remind me of that.
        //[TestMethod]
        //public void IngredientsCtr_DetailsIngredientIDIsNotANumber() { }

        //[TestMethod]
        //public void IngredientsCtr_DetailsIngredientIDIsNotAInteger() { } 


        private IngredientsController SetUpControllerWithNoMock()
        {

            mock.Setup(m => m.Ingredient).Returns(new Ingredient[] {
                new Ingredient {ID = 1, Name = "IngredientsControllerTest1",  ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Ingredient {ID = 2, Name = "IngredientsControllerTest2" , ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new Ingredient {ID = 3, Name = "IngredientsControllerTest3" },
                new Ingredient {ID = 4, Name = "IngredientsControllerTest4" },
                new Ingredient {ID = 5, Name = "IngredientsControllerTest5" }
            }.AsQueryable());

            IngredientsController controller = new IngredientsController(repo);
            controller.PageSize = 3;

            return controller;
        }

        //private  IngredientsController<Ingredient, IngredientVM> SetUpSimpleController()
        //{
        //    mock = new Mock<IRepository<Ingredient, IngredientVM>>();
        //     IngredientsController  controller = new  IngredientsController (mock.Object);
        //    return controller;
        //}


        [ClassCleanup()]
        public static void ClassCleanup()
        {
            // TODO: replace this code with Directory methods
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Ingredient\";
            int count = int.MaxValue;
            try
            {

                for (int i = count; i > count - 6; i--)
                {
                    File.Delete(string.Concat(path, i, ".txt"));
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
        [Ignore]
        [TestMethod]
        public void CorrectIngredientsAreBoundInEdit()
        {
            Assert.Fail();
        }
    }
}

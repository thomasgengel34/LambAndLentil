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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Tests.Controllers
{

    [TestClass]
    [TestCategory(" IngredientsController")]
    public class IngredientsControllerShould
    {
        private static IRepository<Ingredient, IngredientVM> repo { get; set; }
        public static MapperConfiguration AutoMapperConfig { get; set; }
        static ListVM<Ingredient, IngredientVM> ilvm;

        public IngredientsControllerShould()
        {
            AutoMapperConfigForTests.InitializeMap(); 
            repo = new TestRepository<Ingredient, IngredientVM>();
            ilvm = new ListVM<Ingredient, IngredientVM>();
        }

        [TestMethod]
        public void InheritFromBaseControllerCorrectlyPageSizeRight()
        {

            // Arrange
            IngredientsController controller =SetUpController();
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
            IngredientsController controller =SetUpController();
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
            IngredientsController controller =SetUpController();

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
            IngredientsController controller =SetUpController();
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
            IngredientsController controller =SetUpController();
     
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
            IngredientsController controller =SetUpController();
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
            IngredientsController controller =SetUpController();
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
            IngredientsController controller =SetUpController();
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
            IngredientsController controller =SetUpController();
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
            IngredientsController controller =SetUpController();
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


        private IngredientsController SetUpController()
        {
             IngredientsController controller = new IngredientsController_Index_Test().SetUpIngredientsController(repo);


            ilvm.ListT = new List<Ingredient> {
                new Ingredient {ID = int.MaxValue, Name = "IngredientsControllerTest1" ,
                    Description="test IngredientsController.Setup", AddedByUser ="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Ingredient {ID = int.MaxValue-1, Name = "IngredientsControllerTest2", 
                    Description="test IngredientsController.Setup",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new Ingredient {ID = int.MaxValue-2, Name = "IngredientsControllerTest3",
                    Description="test IngredientsController.Setup",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new Ingredient {ID = int.MaxValue-3, Name = "IngredientsControllerTest4",
                    Description="test IngredientsController.Setup",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Ingredient {ID = int.MaxValue-4, Name = "IngredientsControllerTest5",
                    Description="test IngredientsController.Setup",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            }.AsQueryable();

            foreach (Ingredient ingredient in ilvm.ListT)
            {
                repo.AddT(ingredient);
            }
            

            controller = new IngredientsController(repo);
            controller.PageSize = 3;

            return controller;
        }



        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Ingredient\";
            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
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

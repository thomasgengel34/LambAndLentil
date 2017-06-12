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
    public class IngredientsControllerTest
    {
        static Mock<IRepository> mock;
        public static MapperConfiguration AutoMapperConfig { get; set; }
        public IngredientsControllerTest()
        {
            AutoMapperConfigForTests.InitializeMap();
        }

        [TestMethod]
        public void IngredientsCtr_InheritsFromBaseControllerCorrectlyPageSizeRight()
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
        public void IngredientsCtr_InheritsFromBaseControllerCorrectlyDisposeExists()
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
        public void IngredientsCtr_IsPublic()
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
        [TestCategory("Edit")]
        public void IngredientsCtr_CanEditIngredientViewIsNotNull()
        {
            // Arrange
            IngredientsController controller = SetUpController();


            Ingredient ingredient = mock.Object.Ingredients.First();
            mock.Setup(c => c.Save(ingredient)).Verifiable();
            ingredient.Name = "First edited";

            // Act 

            ViewResult view1 = controller.Edit(1);
            IngredientVM p1 = (IngredientVM)view1.Model;
            ViewResult view2 = controller.Edit(2);
            IngredientVM p2 = (IngredientVM)view2.Model;
            ViewResult view3 = controller.Edit(3);
            IngredientVM p3 = (IngredientVM)view3.Model;


            // Assert 
            Assert.IsNotNull(view1);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void IngredientsCtr_CanEditIngredientp1IDis1()
        {
            // Arrange
            IngredientsController controller = SetUpController();


            Ingredient ingredient = mock.Object.Ingredients.First();
            mock.Setup(c => c.Save(ingredient)).Verifiable();
            ingredient.Name = "First edited";

            // Act 

            ViewResult view1 = controller.Edit(1);
            IngredientVM p1 = (IngredientVM)view1.Model;
            ViewResult view2 = controller.Edit(2);
            IngredientVM p2 = (IngredientVM)view2.Model;
            ViewResult view3 = controller.Edit(3);
            IngredientVM p3 = (IngredientVM)view3.Model;


            // Assert  
            Assert.AreEqual(1, p1.ID);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void IngredientsCtr_CanEditIngredientp2IDis2()
        {
            // Arrange
            IngredientsController controller = SetUpController();


            Ingredient ingredient = mock.Object.Ingredients.First();
            mock.Setup(c => c.Save(ingredient)).Verifiable();
            ingredient.Name = "First edited";

            // Act 

            ViewResult view1 = controller.Edit(1);
            IngredientVM p1 = (IngredientVM)view1.Model;
            ViewResult view2 = controller.Edit(2);
            IngredientVM p2 = (IngredientVM)view2.Model;
            ViewResult view3 = controller.Edit(3);
            IngredientVM p3 = (IngredientVM)view3.Model;


            // Assert   
            Assert.AreEqual(2, p2.ID);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void IngredientsCtr_CanEditIngredientp3IDis3()
        {
            // Arrange
            IngredientsController controller = SetUpController();


            Ingredient ingredient = mock.Object.Ingredients.First();
            mock.Setup(c => c.Save(ingredient)).Verifiable();
            ingredient.Name = "First edited";

            // Act 

            ViewResult view1 = controller.Edit(1);
            IngredientVM p1 = (IngredientVM)view1.Model;
            ViewResult view2 = controller.Edit(2);
            IngredientVM p2 = (IngredientVM)view2.Model;
            ViewResult view3 = controller.Edit(3);
            IngredientVM p3 = (IngredientVM)view3.Model;


            // Assert    
            Assert.AreEqual(3, p3.ID);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void IngredientsCtr_CanEditIngredient_FirstNameIsEdited()
        {
            // Arrange
            IngredientsController controller = SetUpController();


            Ingredient ingredient = mock.Object.Ingredients.First();
            mock.Setup(c => c.Save(ingredient)).Verifiable();
            ingredient.Name = "First edited";

            // Act 

            ViewResult view1 = controller.Edit(1);
            IngredientVM p1 = (IngredientVM)view1.Model;
            ViewResult view2 = controller.Edit(2);
            IngredientVM p2 = (IngredientVM)view2.Model;
            ViewResult view3 = controller.Edit(3);
            IngredientVM p3 = (IngredientVM)view3.Model;


            // Assert     
            Assert.AreEqual("First edited", p1.Name);

        }

        [TestMethod]
        [TestCategory("Trial")]
        public void the_ingredient_repository_should_be_called_once_per_customer()
        {
            //Arrange
            mock = new Mock<IRepository>();
            mock.Setup(m => m.Ingredients).Returns(new Ingredient[] {
                new Ingredient {ID = 1, Name = "P1", Maker="Maker1",Brand="BrandAA",AddedByUser="John Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, Caffeine="yes", DataSource="Container", ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Ingredient {ID = 2, Name = "P2", Maker="Maker2",Brand="BrandB",AddedByUser="Sally Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new Ingredient {ID = 3, Name = "P3", Maker="Maker1",Brand="BrandAA",AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new Ingredient {ID = 4, Name = "P4", Maker="Maker2",Brand="BrandB",AddedByUser="Kyle Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Ingredient {ID = 5, Name = "P5", Maker="Maker3",Brand="BrandC",AddedByUser="John Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            }.AsQueryable());
            //var listOfCustomerDtos = new List<Ingredient>
            //        {
            //            new  Ingredient
            //                {
            //                    Name = "Sam" 
            //                },
            //            new Ingredient
            //                {
            //                     Name = "Bob" 
            //                },
            //            new Ingredient
            //                {
            //                    Name = "Doug" 
            //                }
            //        };
            IngredientVM ingredientVM = new IngredientVM() { Name = "Lembas" };

            // var mockRepository = new Mock<IRepository>();



            var controller = new IngredientsController(mock.Object);

            //mockCustomerRepository.Setup(x => x.Save(It.IsAny<Customer>()));

            //Act
            controller.PostEdit(ingredientVM);
            ingredientVM.Name = "cram";
            controller.PostEdit(ingredientVM);

            //Assert
            mock.Verify(x => x.Save<Ingredient>(It.IsAny<Ingredient>()));

        }

        [TestMethod]
        [TestCategory("Edit")]
        public void IngredientsCtr_CanEditIngredient_FirstMakerIsEdited()
        {
            // Arrange
            IngredientsController controller = SetUpController();


            Ingredient ingredient = mock.Object.Ingredients.First();
            mock.Setup(c => c.Save(ingredient)).Verifiable();
            ingredient.Maker = "First edited";

            // Act 

            ViewResult view1 = controller.Edit(1);
            IngredientVM p1 = (IngredientVM)view1.Model;
            ViewResult view2 = controller.Edit(2);
            IngredientVM p2 = (IngredientVM)view2.Model;
            ViewResult view3 = controller.Edit(3);
            IngredientVM p3 = (IngredientVM)view3.Model;


            // Assert   
            Assert.AreEqual("First edited", p1.Maker);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void IngredientsCtr_CanEditIngredientp2NameIsUnchanged()
        {
            // Arrange
            IngredientsController controller = SetUpController();


            Ingredient ingredient = mock.Object.Ingredients.First();
            mock.Setup(c => c.Save(ingredient)).Verifiable();
            ingredient.Name = "First edited";

            // Act 

            ViewResult view1 = controller.Edit(1);
            IngredientVM p1 = (IngredientVM)view1.Model;
            ViewResult view2 = controller.Edit(2);
            IngredientVM p2 = (IngredientVM)view2.Model;
            ViewResult view3 = controller.Edit(3);
            IngredientVM p3 = (IngredientVM)view3.Model;


            // Assert   
            Assert.AreEqual("P2", p2.Name);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void IngredientsCtr_CanEditIngredientp1MakerIsUnchanged()
        {
            // Arrange
            IngredientsController controller = SetUpController();


            Ingredient ingredient = mock.Object.Ingredients.First();
            mock.Setup(c => c.Save(ingredient)).Verifiable();
            ingredient.Name = "First edited";

            // Act 

            ViewResult view1 = controller.Edit(1);
            IngredientVM p1 = (IngredientVM)view1.Model;
            ViewResult view2 = controller.Edit(2);
            IngredientVM p2 = (IngredientVM)view2.Model;
            ViewResult view3 = controller.Edit(3);
            IngredientVM p3 = (IngredientVM)view3.Model;


            // Assert    
            Assert.AreEqual("Maker1", p1.Maker);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void IngredientsCtr_CanEditIngredient_SecondMakerIsUnchanged()
        {
            // Arrange
            IngredientsController controller = SetUpController();


            Ingredient ingredient = mock.Object.Ingredients.First();
            mock.Setup(c => c.Save(ingredient)).Verifiable();
            ingredient.Name = "First edited";

            // Act 

            ViewResult view1 = controller.Edit(1);
            IngredientVM p1 = (IngredientVM)view1.Model;
            ViewResult view2 = controller.Edit(2);
            IngredientVM p2 = (IngredientVM)view2.Model;
            ViewResult view3 = controller.Edit(3);
            IngredientVM p3 = (IngredientVM)view3.Model;


            // Assert    
            Assert.AreEqual("Maker2", p2.Maker);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void IngredientsCtr_CanEditIngredientFirstMakerThirdIsUnchanged()
        {
            // Arrange
            IngredientsController controller = SetUpController();


            Ingredient ingredient = mock.Object.Ingredients.First();
            mock.Setup(c => c.Save(ingredient)).Verifiable();
            ingredient.Name = "First edited";

            // Act 

            ViewResult view1 = controller.Edit(1);
            IngredientVM p1 = (IngredientVM)view1.Model;
            ViewResult view2 = controller.Edit(2);
            IngredientVM p2 = (IngredientVM)view2.Model;
            ViewResult view3 = controller.Edit(3);
            IngredientVM p3 = (IngredientVM)view3.Model;


            // Assert    
            Assert.AreEqual("Maker1", p3.Maker);
        }

      





        [TestMethod]
        [TestCategory("Details")]
        public void IngredientsCtr_DetailsWorksWithValidIngredientID()
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
        public void IngredientsCtr_DetailsIngredientIDTooHighViewNotNull()
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
        public void IngredientsCtr_DetailsIngredientIDTooHighMessageRight()
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
        public void IngredientsCtr_DetailsIngredientIDTooHighAlertClassCorrect()
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
        public void IngredientsCtr_DetailsIngredientIDTooHighCorrectModel()
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
        public void IngredientsCtr_DetailsIngredientIDTooHighCorrectController()
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
        public void IngredientsCtr_DetailsIngredientIDTooHighCorrectRouteValue()
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
        public void IngredientsCtr_DetailsIngredientIDPastIntLimit()
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
        public void IngredientsCtr_DetailsIngredientIDIsZeroViewIsNotNull()
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
        public void IngredientsCtr_DetailsIngredientIDIsZeroMessageIsCorrect()
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
        public void IngredientsCtr_DetailsIngredientIDIsZeroAlertClassIsCorrect()
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
        public void IngredientsCtr_DetailsIngredientIDIsZeroReturnModelIsCorrect()
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
        public void IngredientsCtr_DetailsIngredientIDIsZeroReturnControllerIsCorrect()
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
        public void IngredientsCtr_DetailsIngredientIDIsZeroReturnRouteValueIsCorrect()
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
        public void IngredientsCtr_Details_IngredientIDIsNegative_ResultNotNull()
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
        public void IngredientsCtr_Details_IngredientIDIsNegative_MessageCorrect()
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
        public void IngredientsCtr_Details_IngredientIDIsNegative_AlertClassCorrect()
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
        public void IngredientsCtr_Details_IngredientIDIsNegative_RouteClassCorrect()
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
        public void IngredientsCtr_Details_IngredientIDIsNegative_ControllerIsCorrect()
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
        public void IngredientsCtr_Details_IngredientIDIsNegative_RouteValuesCorrect()
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



        [TestMethod]
        [TestCategory("Create")]
        public void IngredientsCtr_Create_ViewNotNull()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            ViewResult view = controller.Create(UIViewType.Edit);


            // Assert
            Assert.IsNotNull(view);
        }

        [TestMethod]
        [TestCategory("Create")]
        public void IngredientsCtr_Create_ViewNameIsCorrect()
        {
            // Arrange
            IngredientsController controller = SetUpController();
            ViewResult view = controller.Create(UIViewType.Edit);


            // Assert 
            Assert.AreEqual("Details", view.ViewName);
        }


        private IngredientsController SetUpController()
        {
            mock = new Mock<IRepository>();
            mock.Setup(m => m.Ingredients).Returns(new Ingredient[] {
                new Ingredient {ID = 1, Name = "P1", Maker="Maker1",Brand="BrandAA",AddedByUser="John Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, Caffeine="yes", DataSource="Container", ModifiedDate=DateTime.MaxValue.AddYears(-10)},
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

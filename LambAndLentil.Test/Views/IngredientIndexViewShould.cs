using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.UI.Controllers;
using System.Web.Mvc;
using AutoMapper;
using LambAndLentil.Tests.Infrastructure;
using System.IO;

namespace LambAndLentil.Test.Views
{
    [Ignore]
    [TestClass]
    public class IngredientIndexViewShould
    {
        private IRepository<Ingredient> Repo;
        private IngredientsController controller;
        public static MapperConfiguration AutoMapperConfig { get; set; }
        public IngredientIndexViewShould()
        {
            AutoMapperConfigForTests.InitializeMap();
            Repo = new TestRepository<Ingredient>(); 
            controller = new IngredientsController(Repo); 
        }

        [TestMethod]
        public void NotReturnServerErrorScreen()
        {
            // Arrange

            //

            // Act
            ViewResult view = controller.Index(1);
            string foo = controller.PartialViewToString("Index", view.Model);

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void HaveProperlyFormattedColumns()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }


        [Ignore]
        [TestMethod]
        public void HaveAutoCompleteSearchTextBoxAndDropDownListThatObtainsCorrectResult()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void CopyAnIngredientToBeModifiedAndRenameItLocally()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void UserCanAddIngredientColumnsToShow()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DisplayErrorMessageIfUserTriesToAddTooManyColumns()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void UserCanRemoveIngredientColumnsToShow()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void UserCanSortAllIngredientColumns()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void UserCanReverseSortAllIngredientColumns()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void UserCanRestoreSortedColumnsToOriginalOrder()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ShowContainerSizeAndUnit()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void HaveColumnNamesCorrectlySplit()
        {   // for example, SodiumNitrite should show as Sodium Nitrite
            //   use Matt's name splitter extension

            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void BeAbleToCompareIngredientsOnAPage()
        {   // total difference in a column between max and min, perhaps some significant combinations of ingredients

            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
   

    public class CustomResult<T> : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            string foo = context.HttpContext.Response.ToString();
        } }

        [Ignore]
        [TestMethod]
    public void AllowUserToShrinkAColumnWithAClickOnTheColumn()
    {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void AllowUserToExpandAShrunkColumnWithAClickOnTheColumn()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void HaveTheTitleOfAShrunkColumnRotated90Degrees()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void HaveTheTitleOfAnUnShrunkColumnRotatedZeroDegrees()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void HaveAShrunkColumnOnlyWideEnoughToSeeTheRotatedTitle()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void HaveAnUnShrunkColumnRestoredToItsPreviousWidth()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void HaveHelpfulNoteAboutShrinkingAndExpandingColumns()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DropDownListsShouldLookGoodInShrunkColumns()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DropDownListsShouldActCorrectlyInShrunkColumns()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void FilterForUSDAStandardDatabaseItems()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void FilterOutEveryIngredientAPersonCannotHave()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

 }
    public static class PartialViewToStringHelper {
        // https://www.red-gate.com/simple-talk/dotnet/asp-net/advanced-uses-razor-views-asp-net-mvc/
        public static string PartialViewToString(this Controller controller, string name, object model)
        {
            controller.ViewData.Model = model;
            var result = ViewEngines.Engines.FindPartialView(controller.ControllerContext, name);

            var writer = new StringWriter();
            var viewCtx = new ViewContext(controller.ControllerContext, result.View,
               controller.ViewData, controller.TempData, writer);
            result.View.Render(viewCtx, writer);
            return writer.GetStringBuilder().ToString();
        }
   }
}



using LambAndLentil.UI.Controllers;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.UI.Models;
using System.Collections.Generic;
using AutoMapper;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI;
using System.IO;

namespace LambAndLentil.Tests.Controllers
{ 
    [TestClass]
    [TestCategory("RecipesController")]
    public class RecipesControllerShould
    {
        // static List<RecipeVM> recipeVMArray { get; set; }
        private static IRepository<RecipeVM> repo { get; set; }
        public static MapperConfiguration AutoMapperConfig { get; set; }
        private static RecipesController controller { get; set; }
        private ListVM<RecipeVM> vm { get; set; }

        public RecipesControllerShould()
        {
            AutoMapperConfigForTests.InitializeMap();
            repo = new TestRepository<RecipeVM>();
            vm = new ListVM<RecipeVM>();
            controller = SetUpController();
        }


        [TestMethod]
        public void InheritFromBaseController()
        {
            // Arrange


            // Act 
            Type baseType = typeof(BaseController<RecipeVM>);
            bool isBase = baseType.IsInstanceOfType(controller);

            // Assert 
            Assert.AreEqual(isBase, true);
        }

        private RecipesController SetUpController()
        {

            vm.ListT = new List<RecipeVM> {
                new RecipeVM {ID = int.MaxValue, Name = "RecipesController_Index_Test P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new RecipeVM {ID = int.MaxValue-1, Name = "RecipesController_Index_Test P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new RecipeVM {ID = int.MaxValue-2, Name = "RecipesController_Index_Test P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new RecipeVM {ID = int.MaxValue-3, Name = "RecipesController_Index_Test P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new RecipeVM {ID = int.MaxValue-4, Name = "RecipesController_Index_Test P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            }.AsQueryable();

            foreach (RecipeVM recipe in vm.ListT)
            {
                repo.Add(recipe);
            }

            controller = new RecipesController(repo);
            controller.PageSize = 3;

            return controller;
        }



        [TestMethod]
        public void BePublic()
        {
            // Arrange


            // Act
            Type type = controller.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        }

        [TestMethod]
        public void ReturnNonNullIndex()
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

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        public void ShowAllRecipesonIndex()
        {
            // Arrange
            RecipesController controller = SetUpController();


            // Act
            ViewResult view1 = controller.Index(1);


            int count1 = ((ListVM<RecipeVM>)view1.Model).ListT.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM<RecipeVM>)view2.Model).ListT.Count();

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
        public void CanSendPaginationViewModelonIndex()
        {

            // Arrange
            RecipesController controller = SetUpController();

            // Act

            ListVM<RecipeVM> resultT = (ListVM<RecipeVM>)((ViewResult)controller.Index(2)).Model;


            // Assert

            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(repo.Count(), pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);

        }

        [Ignore]
        [TestMethod]
        public void Index_FirstPageIsCorrect()
        {

            // Arrange 
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<RecipeVM>)(view1.Model)).ListT.Count();



            // Assert
            Assert.IsNotNull(view1);
            Assert.AreEqual(5, count1);
            Assert.AreEqual("Index", view1.ViewName);

            Assert.AreEqual("RecipesController_Index_Test P1", ((ListVM<RecipeVM>)(view1.Model)).ListT.FirstOrDefault().Name);
            Assert.AreEqual("RecipesController_Index_Test P2", ((ListVM<RecipeVM>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual("RecipesController_Index_Test P3", ((ListVM<RecipeVM>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);


        }

        [Ignore]
        [TestMethod]
        public void Index_PagingInfoIsCorrect()
        {
            // Arrange 

            // Action
            int totalItems = ((ListVM<RecipeVM>)((ViewResult)controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = ((ListVM<RecipeVM>)((ViewResult)controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = ((ListVM<RecipeVM>)((ViewResult)controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = ((ListVM<RecipeVM>)((ViewResult)controller.Index()).Model).PagingInfo.TotalPages;



            // Assert
            Assert.AreEqual(5, totalItems);
            Assert.AreEqual(1, currentPage);
            Assert.AreEqual(8, itemsPerPage);
            Assert.AreEqual(1, totalPages);
        }

        [Ignore]
        [TestMethod]
        // currently we only have one page here 
        public void Index_SecondPageIsCorrect()
        {

        }

        [Ignore]
        [TestMethod]
        public void IndexCanPaginate()
        {
            // Arrange


            // Act
            var result = (ListVM<RecipeVM>)(controller.Index(1)).Model;

            // Assert 
            Assert.IsTrue(result.ListT.Count() == 5);
            Assert.AreEqual("RecipesController_Index_Test P1", result.ListT.First().Name);
            Assert.AreEqual("RecipesController_Index_Test P3", result.ListT.Skip(2).First().Name);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsRecipeIDIsNegative()
        {
            // Arrange

            // Act
            ViewResult view = controller.Details(-1) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Recipe was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());


        }

        [Ignore]
        [TestMethod]
        public void ReturnShortClassNameInErrorMessages()
        { // e.g. "Recipe not found" not "LambAndLentil.Domain.Entities.Recipe not found"
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnFullyDefinedClassNameWhereNeeded()
        { // where is it needed?
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsWorksWithValidRecipeID()
        {
            // Arrange 

            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.Details(int.MaxValue);
            ViewResult view = (ViewResult)adr.InnerResult;
            // Assert
            Assert.IsNotNull(view);

            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(RecipeVM));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsRecipeIDTooHigh()
        {
            // Arrange


            ActionResult view = controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Recipe was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsRecipeIDPastIntLimit()
        {
            // Arrange 

            // Act
            ActionResult result = controller.Details(Int16.MaxValue + 1);

            // Assert
            Assert.IsNotNull(result);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Details")]
        public void DetailsRecipeIDIsZero()
        {
            // Arrange
            RecipesController controller = SetUpController();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Recipe was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Create")]
        public void Create()
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
        public void DeleteAFoundRecipe()
        {
            // Arrange


            // Act 
            var view = controller.Delete(int.MaxValue - 1) as ViewResult;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual(UIViewType.Details.ToString(), view.ViewName);


        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteAnInvalidRecipe()
        {
            // Arrange 
            ClassCleanup();
            // Act 
            ActionResult ar = controller.Delete(int.MaxValue - 6) as ViewResult;   // does not exist
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            // Assert

            Assert.AreEqual("Recipe was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);

            Assert.AreEqual(UIViewType.Index.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());

        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteConfirmed()
        {
            // Arrange

            // Act
            ActionResult result = controller.DeleteConfirmed(int.MaxValue) as ActionResult;
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { controller = "Recipes", Action = "Index" } } );
            // Assert 
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteValidRecipe()
        {
            // Arrange - create an recipe
            RecipeVM recipeVM = new RecipeVM { ID = 2, Name = "Test2" };
            repo.Add(recipeVM);
            int repoCount = repo.Count();

            // Arrange - create the controller
            RecipesController controller = new RecipesController(repo);

            // Act - delete the recipe
            ActionResult result = controller.DeleteConfirmed(recipeVM.ID);

            AlertDecoratorResult adr = (AlertDecoratorResult)result;
            int newRepoCount = repo.Count();
            // Assert
            Assert.AreEqual(repoCount - 1, newRepoCount);
            Assert.AreEqual("Test2 has been deleted", adr.Message);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void EditRecipe()
        {


            // Arrange 

            // Act  

            // Assert  
            Assert.Fail();
        }


        [TestMethod]
        [TestCategory("Edit")]
        public void NotEditNonexistentRecipe()
        {
            // Arrange
            RecipesController controller = SetUpController();
            // Act
            Recipe result = (Recipe)controller.Edit(8).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void RecipesCtr_CreateReturnsNonNull()
        {
            // Arrange
            RecipesController controller = SetUpController();


            // Act
            ViewResult result = controller.Create(UIViewType.Create) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
         

        [Ignore]
        [TestMethod]
        public void FlagAnIngredientFlaggedInAPerson()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void FlagAnIngredientFlaggedInTwoPersons()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void WhenAFlagHasBeenRemovedFromOnePersonStillThereForSecondFlaggedPerson()
        {
            Assert.Fail();
        }


        [Ignore]
        [TestCategory("Copy")]
        [TestMethod]
        public void CopyModifySaveWithANewName()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void CorrectRecipesAreBoundInEdit()
        {
            Assert.Fail();
        }


        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Recipe\";
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
    }
}

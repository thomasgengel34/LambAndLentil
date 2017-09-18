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

namespace LambAndLentil.Test.BasicControllerTests
{ 
    [TestClass]
    [TestCategory("RecipesController")]
    public class RecipesController_Test_Should
    {
        // static ListEntity<Recipe> recipeVMArray { get; set; }
        protected static IRepository<Recipe> Repo { get; set; }
        public static MapperConfiguration AutoMapperConfig { get; set; }
       protected static RecipesController Controller { get; set; }
        protected ListEntity<Recipe> ListEntity { get; set; }

        public RecipesController_Test_Should()
        {
            AutoMapperConfigForTests.InitializeMap();
            Repo = new TestRepository<Recipe>();
            ListEntity = new ListEntity<Recipe>();
            Controller = SetUpController(Repo);
        }



        [TestMethod]
        public void InheritFromBaseController()
        {
            // Arrange


            // Act 
            Type baseType = typeof(BaseController<Recipe>);
            bool isBase = baseType.IsInstanceOfType(Controller);
             
            // Assert 
            Assert.AreEqual(isBase, true);
        }
        
      protected RecipesController SetUpController(IRepository<Recipe>  Repo)  
        {

            ListEntity.ListT = new List<Recipe> {
                new Recipe {ID = int.MaxValue, Name = "RecipesController_Index_Test P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Recipe {ID = int.MaxValue-1, Name = "RecipesController_Index_Test P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new Recipe {ID = int.MaxValue-2, Name = "RecipesController_Index_Test P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new Recipe {ID = int.MaxValue-3, Name = "RecipesController_Index_Test P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Recipe {ID = int.MaxValue-4, Name = "RecipesController_Index_Test P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            } ;

            foreach (Recipe recipe in ListEntity.ListT)
            {
                Repo.Add(recipe);
            }

            Controller = new RecipesController(Repo)
            {
                PageSize = 3
            };

            return Controller;
        }



        [TestMethod]
        public void BePublic()
        {
            // Arrange 

            // Act
            Type type = Controller.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
          
        }

        [TestMethod]
        public void ReturnNonNullIndex()
        {
            // Arrange
            

            // Act
            ViewResult result = Controller.Index(1) as ViewResult;
            ViewResult result1 = Controller.Index(2) as ViewResult;

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
           
            // Act
            ViewResult view1 = Controller.Index(1);


            int count1 = ((ListEntity<Recipe>)view1.Model).ListT.Count();

            ViewResult view2 = Controller.Index(2);

            int count2 = ((ListEntity<Recipe>)view2.Model).ListT.Count();

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
           
            // Act

            ListEntity<Recipe> resultT = (ListEntity<Recipe>)((ViewResult)Controller.Index(2)).Model;


            // Assert

            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(Repo.Count(), pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);

        }

        [Ignore]
        [TestMethod]
        public void Index_FirstPageIsCorrect()
        {

            // Arrange 
            Controller.PageSize = 8;

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = ((ListEntity<Recipe>)(view1.Model)).ListT.Count();



            // Assert
            Assert.IsNotNull(view1);
            Assert.AreEqual(5, count1);
            Assert.AreEqual("Index", view1.ViewName);

            Assert.AreEqual("RecipesController_Index_Test P1", ((ListEntity<Recipe>)(view1.Model)).ListT.FirstOrDefault().Name);
            Assert.AreEqual("RecipesController_Index_Test P2", ((ListEntity<Recipe>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual("RecipesController_Index_Test P3", ((ListEntity<Recipe>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);


        }

        [Ignore]
        [TestMethod]
        public void Index_PagingInfoIsCorrect()
        {
            // Arrange 

            // Action
            int totalItems = ((ListEntity<Recipe>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = ((ListEntity<Recipe>)((ViewResult)Controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = ((ListEntity<Recipe>)((ViewResult)Controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = ((ListEntity<Recipe>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalPages;



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
            var result = (ListEntity<Recipe>)(Controller.Index(1)).Model;

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
            ViewResult view = Controller.Details(-1) as ViewResult;
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
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Details(int.MaxValue);
            ViewResult view = (ViewResult)adr.InnerResult;
            // Assert
            Assert.IsNotNull(view);

            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Recipe));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsRecipeIDTooHigh()
        {
            // Arrange


            ActionResult view = Controller.Details(4000);
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
            ActionResult result = Controller.Details(Int16.MaxValue + 1);

            // Assert
            Assert.IsNotNull(result);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Details")]
        public void DetailsRecipeIDIsZero()
        {
            // Arrange 

            // Act
            ViewResult view = Controller.Details(0) as ViewResult;
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
            ViewResult view = Controller.Create(UIViewType.Edit);


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
            var view = Controller.Delete(int.MaxValue - 1) as ViewResult;

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
            ActionResult ar = Controller.Delete(int.MaxValue - 6) as ViewResult;   // does not exist
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
            ActionResult result = Controller.DeleteConfirmed(int.MaxValue) as ActionResult;
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
            Recipe recipeVM = new Recipe { ID = 2, Name = "Test2" };
            Repo.Add(recipeVM);
            int repoCount = Repo.Count();

            // Arrange - create the controller
            RecipesController controller = new RecipesController(Repo);

            // Act - delete the recipe
            ActionResult result = controller.DeleteConfirmed(recipeVM.ID);

            AlertDecoratorResult adr = (AlertDecoratorResult)result;
            int newRepoCount = Repo.Count();
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

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void NotEditNonexistentRecipe()
        {
            // Arrange
           
            // Act
            Recipe result = (Recipe)((ViewResult)Controller.Edit(8)).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void RecipesCtr_CreateReturnsNonNull()
        {
            // Arrange
          
            // Act
            ViewResult result = Controller.Create(UIViewType.Create) as ViewResult;

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

        [TestMethod]
        public void GetTheClassNameCorrect()
        {
            // Arrange

            // Act


            // Assert
            //  Assert.Fail();
            Assert.AreEqual("LambAndLentil.UI.Controllers.RecipesController", RecipesController_Test_Should.Controller.ToString());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            ClassCleanup();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Recipe\";

            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
    } 

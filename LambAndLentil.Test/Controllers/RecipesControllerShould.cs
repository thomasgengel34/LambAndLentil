﻿using LambAndLentil.UI.Controllers;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
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
using System.IO;

namespace LambAndLentil.Tests.Controllers
{

    [TestClass]
    [TestCategory("RecipesController")]
    public class RecipesControllerShould
    {
        //        static Mock<IRepository<Recipe, RecipeVM>> mock { get; set; }
        //        static MapperConfiguration AutoMapperConfig { get; set; }
        //        static JSONRepository<Recipe, RecipeVM> repo { get; set; }
        //        static List<RecipeVM> recipeVMArray { get; set; }
        private static IRepository<Recipe, RecipeVM> repo { get; set; }
        public static MapperConfiguration AutoMapperConfig { get; set; }
        private static RecipesController controller { get; set; }
    private ListVM<Recipe, RecipeVM> vm { get; set; }

    public RecipesControllerShould()
    {
        AutoMapperConfigForTests.InitializeMap();
        // mock = new Mock<IRepository<Recipe, RecipeVM>>();
        repo = new TestRepository<Recipe, RecipeVM>();
        vm = new ListVM<Recipe, RecipeVM>();
            controller = SetUpController();
    }


    [TestMethod]
    public void InheritFromBaseController()
    {
        // Arrange
         

        // Act 
        Type baseType = typeof(BaseController<Recipe, RecipeVM>);
        bool isBase = baseType.IsInstanceOfType(controller);

        // Assert 
        Assert.AreEqual(isBase, true);
    }

    private RecipesController SetUpController()
    {

        vm.ListT = new List<Recipe> {
                new Recipe {ID = int.MaxValue, Name = "RecipesController_Index_Test P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Recipe {ID = int.MaxValue-1, Name = "RecipesController_Index_Test P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new Recipe {ID = int.MaxValue-2, Name = "RecipesController_Index_Test P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new Recipe {ID = int.MaxValue-3, Name = "RecipesController_Index_Test P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Recipe {ID = int.MaxValue-4, Name = "RecipesController_Index_Test P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            }.AsQueryable();

        foreach (Recipe ingredient in vm.ListT)
        {
            repo.AddT(ingredient);
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
        Type type =controller.GetType();
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

        [TestMethod]
        [TestCategory("Index")]
        public void ShowAllRecipesonIndex()
        {
            // Arrange
            RecipesController controller = SetUpController();


            // Act
            ViewResult view1 = controller.Index(1);


            int count1 = ((ListVM<Recipe, RecipeVM>)view1.Model).ListTVM.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM<Recipe, RecipeVM>)view2.Model).ListTVM.Count();

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

            ListVM<Recipe, RecipeVM> resultT = (ListVM<Recipe, RecipeVM>)((ViewResult)controller.Index(2)).Model;


            // Assert

            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(repo.Count(), pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);
             
        }


        [TestMethod]
        public void Index_FirstPageIsCorrect()
        {
            
            // Arrange 
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<Recipe, RecipeVM>)(view1.Model)).ListTVM.Count();



            // Assert
            Assert.IsNotNull(view1);
            Assert.AreEqual(5, count1);
            Assert.AreEqual("Index", view1.ViewName);

            Assert.AreEqual("RecipesController_Index_Test P1", ((ListVM<Recipe, RecipeVM>)(view1.Model)).ListTVM.FirstOrDefault().Name);
            Assert.AreEqual("RecipesController_Index_Test P2", ((ListVM<Recipe, RecipeVM>)(view1.Model)).ListTVM.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual("RecipesController_Index_Test P3", ((ListVM<Recipe, RecipeVM>)(view1.Model)).ListTVM.Skip(2).FirstOrDefault().Name);


        }

        [TestMethod]
        public void Index_PagingInfoIsCorrect()
        {
            // Arrange 

            // Action
            int totalItems = ((ListVM<Recipe, RecipeVM>)((ViewResult)controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = ((ListVM<Recipe, RecipeVM>)((ViewResult)controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = ((ListVM<Recipe, RecipeVM>)((ViewResult)controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = ((ListVM<Recipe, RecipeVM>)((ViewResult)controller.Index()).Model).PagingInfo.TotalPages;



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

        [TestMethod]
        public void IndexCanPaginate()
        {
            // Arrange
            

            // Act
            var result = (ListVM<Recipe, RecipeVM>)(controller.Index(1)).Model;
             
            // Assert 
            Assert.IsTrue(result.ListTVM.Count() == 5);
            Assert.AreEqual("RecipesController_Index_Test P1", result.ListTVM.First().Name);
            Assert.AreEqual("RecipesController_Index_Test P3", result.ListTVM.Skip(2).First().Name);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsRecipeIDIsNegative()
        {
            // Arrange
          
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

        //        [TestMethod]
        //        [TestCategory("Details")]
        //        public void DetailsWorksWithValidRecipeID()
        //        {
        //            // Arrange
        //            RecipesController controller = SetUpController();
        //            //AutoMapperConfigForTests.AMConfigForTests();


        //            // Act
        //            ViewResult view = controller.Details(1) as ViewResult;
        //            // Assert
        //            Assert.IsNotNull(view);

        //            Assert.AreEqual("Details", view.ViewName);
        //            Assert.IsInstanceOfType(view.Model, typeof(LambAndLentil.UI.Models.RecipeVM));
        //        }

        //        [TestMethod]
        //        [TestCategory("Details")]
        //        public void DetailsRecipeIDTooHigh()
        //        {
        //            // Arrange
        //            RecipesController controller = SetUpController();
        //            //AutoMapperConfigForTests.AMConfigForTests();
        //            ActionResult view = controller.Details(4000);
        //            AlertDecoratorResult adr = (AlertDecoratorResult)view;
        //            // Assert
        //            Assert.IsNotNull(view);
        //            Assert.AreEqual("No recipe was found with that id.", adr.Message);
        //            Assert.AreEqual("alert-danger", adr.AlertClass);
        //            Assert.AreEqual(UIControllerType.Recipes.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        //            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
        //            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        //        }

        //        [TestMethod]
        //        [TestCategory("Details")]
        //        public void DetailsRecipeIDPastIntLimit()
        //        {
        //            // Arrange
        //            RecipesController controller = SetUpController();
        //            //AutoMapperConfigForTests.AMConfigForTests();


        //            // Act
        //            ViewResult result = controller.Details(Int16.MaxValue + 1) as ViewResult;

        //            // Assert
        //            Assert.IsNotNull(result);
        //        }

        //        [TestMethod]
        //        [TestCategory("Details")]
        //        public void DetailsRecipeIDIsZero()
        //        {
        //            // Arrange
        //            RecipesController controller = SetUpController();
        //            //AutoMapperConfigForTests.AMConfigForTests();


        //            // Act
        //            ViewResult view = controller.Details(0) as ViewResult;
        //            AlertDecoratorResult adr = (AlertDecoratorResult)view;

        //            // Assert
        //            Assert.IsNotNull(view);
        //            Assert.AreEqual("No recipe was found with that id.", adr.Message);
        //            Assert.AreEqual("alert-danger", adr.AlertClass);
        //            Assert.AreEqual(UIControllerType.Recipes.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        //            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
        //            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        //        }

        //        [TestMethod]
        //        [TestCategory("Create")]
        //        public void Create()
        //        {
        //            // Arrange
        //            RecipesController controller = SetUpController();
        //            ViewResult view = controller.Create(UIViewType.Edit);


        //            // Assert
        //            Assert.IsNotNull(view);
        //            Assert.AreEqual("Details", view.ViewName);
        //        }

        //        [TestMethod]
        //        [TestCategory("Delete")]
        //        public void DeleteAFoundRecipe()
        //        {
        //            // Arrange
        //            RecipesController controller = SetUpJSONController();

        //            // Act 
        //            var view = controller.Delete(int.MaxValue - 1) as ViewResult;

        //            // Assert
        //            Assert.IsNotNull(view);
        //            Assert.AreEqual(UIViewType.Details.ToString(), view.ViewName);

        //            // Cleanup
        //            SetUpJSONController(false);
        //        }

        //        [TestMethod]
        //        [TestCategory("Delete")]
        //        public void DeleteAnInvalidRecipe()
        //        {
        //            // Arrange
        //            RecipesController controller = SetUpJSONController();

        //            // Act 
        //            var view = controller.Delete(int.MaxValue - 6) as ViewResult;   // does not exist
        //            AlertDecoratorResult adr = (AlertDecoratorResult)view;

        //            // Assert
        //            Assert.IsNotNull(view);
        //            Assert.AreEqual("No recipe was found with that id.", adr.Message);
        //            Assert.AreEqual("alert-danger", adr.AlertClass);
        //            Assert.AreEqual(UIControllerType.Recipes.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        //            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
        //            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));

        //            // Cleanup
        //            SetUpJSONController(false);
        //        }

        //        [TestMethod]
        //        [TestCategory("Delete")]
        //        public void DeleteConfirmed()
        //        {
        //            // Arrange
        //            RecipesController controller = SetUpController();
        //            // Act
        //            ActionResult result = controller.DeleteConfirmed(1) as ActionResult;
        //            // improve this test when I do some route tests to return a more exact result
        //            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { controller = "Recipes", Action = "Index" } } );
        //            // Assert 
        //            Assert.IsNotNull(result);
        //        }

        //        [TestMethod]
        //        [TestCategory("Delete")]
        //        public void DeleteValidRecipe()
        //        {
        //            // Arrange - create an recipe
        //            RecipeVM recipeVM = new RecipeVM { ID = 2, Name = "Test2" };

        //            // Arrange - create the mock repository
        //            mock = new Mock<IRepository<Recipe, RecipeVM>>();
        //            mock.Setup(m => m.Recipe).Returns(new RecipeVM[]
        //            {
        //                new RecipeVM {ID=1,Name="Test1"},

        //                recipeVM,

        //                new RecipeVM {ID=3,Name="Test3"},
        //            }.AsQueryable());
        //            mock.Setup(m => m.RemoveTVM(It.IsAny<RecipeVM>())).Verifiable();
        //            // Arrange - create the controller
        //            RecipesController controller = new RecipesController(mock.Object);

        //            // Act - delete the recipe
        //            ActionResult result = controller.DeleteConfirmed(recipeVM.ID);

        //            AlertDecoratorResult adr = (AlertDecoratorResult)result;

        //            // Assert - ensure that the repository delete method was called with a correct Recipe
        //            mock.Verify(m => m.RemoveTVM(recipeVM));


        //            Assert.AreEqual("Test2 has been deleted", adr.Message);
        //        }


        //        [TestMethod]
        //        [TestCategory("Edit")]
        //        public void EditRecipe()
        //        {
        //            //  mock = new Mock<IRepository<Recipe,RecipeVM>>();

        //            // Arrange
        //            RecipesController controller = SetUpJSONController();
        //            //  Recipe recipe = mock.Object.Recipe.Cast<Recipe>().First();
        //            //    RecipeVM recipeVM = mock.Object.Recipe.Cast<RecipeVM>().First();
        //            // RecipeVM recipeVM = Mapper.Map<Recipe, RecipeVM>(recipe);

        //            RecipeVM recipeVM = repo.GetById(int.MaxValue - 1);
        //            //   mock.Setup(c => c.Save(recipeVM)).Verifiable(); 
        //            recipeVM.Name = "First edited";

        //            // Act 

        //            ViewResult view1 = controller.Edit(int.MaxValue - 1);
        //            RecipeVM p1 = (RecipeVM)view1.Model;
        //            ViewResult view2 = controller.Edit(int.MaxValue - 2);
        //            RecipeVM p2 = (RecipeVM)view2.Model;
        //            ViewResult view3 = controller.Edit(int.MaxValue - 3);
        //            RecipeVM p3 = (RecipeVM)view3.Model;


        //            // Assert 
        //            Assert.IsNotNull(view1);
        //            Assert.AreEqual(int.MaxValue - 1, p1.ID);
        //            Assert.AreEqual(int.MaxValue - 2, p2.ID);
        //            Assert.AreEqual(int.MaxValue - 3, p3.ID);
        //            Assert.AreEqual("First edited", p1.Name);
        //            Assert.AreEqual("oldRecipe2", p2.Name);

        //            // Cleanup
        //            SetUpJSONController(false);
        //        }


        //        [TestMethod]
        //        [TestCategory("Edit")]
        //        public void NotEditNonexistentRecipe()
        //        {
        //            //    // Arrange
        //            //    RecipesController controller = SetUpController();
        //            //    // Act
        //            //    Recipe result = (Recipe)controller.Edit(8).ViewData.Model;
        //            //    // Assert
        //            //    Assert.IsNull(result);
        //            //}

        //            //[TestMethod]
        //            //public void RecipesCtr_CreateReturnsNonNull()
        //            //{
        //            //    // Arrange
        //            //    RecipesController controller = SetUpController();


        //            //    // Act
        //            //    ViewResult result = controller.Create(null) as ViewResult;

        //            //    // Assert
        //            //    Assert.IsNotNull(result);
        //        }




        //        private RecipesController SetUpController()
        //        {
        //            // - create the mock repository
        //            mock = new Mock<IRepository<Recipe, RecipeVM>>();
        //            mock.Setup(m => m.Recipe).Returns(new Recipe[] {
        //                new Recipe {ID = 1, Name = "oldRecipe1", Description="old Description A", ModifiedByUser="UserA"},
        //                new Recipe {ID = 2, Name = "oldRecipe2", Description= "old Description B", ModifiedByUser= "UserB" },
        //                new Recipe {ID = 3, Name = "oldRecipe3", Description= "old Description rA", ModifiedByUser= "UserA" },
        //                new Recipe {ID = 4, Name = "oldRecipe4", Description= "old Description B", ModifiedByUser= "UserB" },
        //                new Recipe {ID = 5, Name = "oldRecipe5", Description= "old Description C", ModifiedByUser= "UserC" }
        //            }.AsQueryable());

        //            // Arrange - create a controller

        //            RecipesController controller = new RecipesController(mock.Object);

        //            controller.PageSize = 3;
        //            return controller;
        //        }

        //        private RecipesController SetUpSimpleController()
        //        {
        //            // - create the mock repository
        //            mock = new Mock<IRepository<Recipe, RecipeVM>>();


        //            // Arrange - create a controller
        //            RecipesController controller = new RecipesController(mock.Object);
        //            // controller.PageSize = 3;

        //            return controller;
        //        }

        //        private RecipesController SetUpJSONController(bool setupNotDelete = true)
        //        {
        //            //JSONRepository<Recipe, RecipeVM> repo = new JSONRepository<Recipe, RecipeVM>();
        //            List<RecipeVM> recipeVMArray = new List<RecipeVM>{
        //                new RecipeVM {ID = int.MaxValue-1, Name = "oldRecipe1", Description="old Description A", ModifiedByUser="UserA"},
        //                new RecipeVM {ID = int.MaxValue-2, Name = "oldRecipe2", Description= "old Description B", ModifiedByUser= "UserB" },
        //                new RecipeVM {ID = int.MaxValue-3, Name = "oldRecipe3", Description= "old Description rA", ModifiedByUser= "UserA" },
        //                new RecipeVM {ID =int.MaxValue-4, Name = "oldRecipe4", Description= "old Description B", ModifiedByUser= "UserB" },
        //                new RecipeVM {ID = int.MaxValue-5, Name = "oldRecipe5", Description= "old Description C", ModifiedByUser= "UserC" }
        //            };

        //            foreach (RecipeVM recipeVM in recipeVMArray)
        //            {
        //                if (setupNotDelete)
        //                {
        //                    repo.Add(recipeVM);
        //                }
        //                else
        //                {
        //                    repo.RemoveTVM(recipeVM);
        //                }

        //            }

        //            // Arrange - create a controller

        //            RecipesController controller = new RecipesController(repo);

        //            controller.PageSize = 3;
        //            return controller;
        //        }


        //        [ClassCleanup()]
        //        public static void ClassCleanup()
        //        {
        //            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Domain\App_Data\JSON\Recipe\";
        //            int count = int.MaxValue;
        //            try
        //            {

        //                for (int i = count; i > count-6; i--)
        //                {
        //                    File.Delete(string.Concat(path, i, ".txt"));
        //                } 

        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }

        //        }

        //        [TestMethod]
        //        public void FlagAnIngredientFlaggedInAPerson()
        //        {
        //            Assert.Fail();
        //        }


        //        [TestMethod]
        //        public void FlagAnIngredientFlaggedInTwoPersons()
        //        {
        //            Assert.Fail();
        //        }

        //        [TestMethod]
        //        public void WhenAFlagHasBeenRemovedFromOnePersonStillThereForSecondFlaggedPerson()
        //        {
        //            Assert.Fail();
        //        }


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

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

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory(" IngredientsController")]
    public class IngredientsController_Test_Should
    {
        protected static IRepository<IngredientVM> Repo { get; set; }
        public static MapperConfiguration AutoMapperConfig { get; set; }
        static ListVM<IngredientVM> ilvm;
        static IngredientsController controller;
        static IngredientVM ingredientVM;

        public IngredientsController_Test_Should()
        {
            AutoMapperConfigForTests.InitializeMap();
            Repo = new TestRepository<IngredientVM>();
            ilvm = new ListVM<IngredientVM>();
            controller = SetUpController(Repo);
            ingredientVM = new IngredientVM { ID = 1 };
            Repo.Save(ingredientVM);
        }

        [TestMethod]
        public void InheritFromBaseControllerCorrectlyPageSizeRight()
        {

            // Arrange
        
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
         

            // Act
            Type type = controller.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        }

     

       protected IngredientsController SetUpController(IRepository<IngredientVM> repo)
        {


            ilvm.ListT = new List<IngredientVM> {
                new IngredientVM {ID = int.MaxValue, Name = "IngredientsControllerTest1" ,
                    Description="test IngredientsController.Setup", AddedByUser ="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new IngredientVM {ID = int.MaxValue-1, Name = "IngredientsControllerTest2",
                    Description="test IngredientsController.Setup",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new IngredientVM {ID = int.MaxValue-2, Name = "IngredientsControllerTest3",
                    Description="test IngredientsController.Setup",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new IngredientVM {ID = int.MaxValue-3, Name = "IngredientsControllerTest4",
                    Description="test IngredientsController.Setup",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new IngredientVM {ID = int.MaxValue-4, Name = "IngredientsControllerTest5",
                    Description="test IngredientsController.Setup",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            }.AsQueryable();

            foreach (IngredientVM item in ilvm.ListT)
            {
                Repo.Add(item);
            }


            controller = new IngredientsController(Repo);
            controller.PageSize = 3;

            return controller;
        }

        
        [TestMethod]
        public void GetTheClassNameCorrect()
        {
            // Arrange

            // Act


            // Assert 
           Assert.AreEqual("LambAndLentil.UI.Controllers.IngredientsController", IngredientsController_Test_Should.controller.ToString()); 
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

      
    }
}

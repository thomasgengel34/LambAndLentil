using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory(" IngredientsController")]
    public class IngredientsController_Test_Should
    {
        protected static IRepository<Ingredient> Repo { get; set; }
        public static MapperConfiguration AutoMapperConfig { get; set; }
        protected static ListEntity<Ingredient> list;
        protected static IngredientsController controller;
        protected static Ingredient ingredient;

        public IngredientsController_Test_Should()
        {
            AutoMapperConfigForTests.InitializeMap();
            ingredient = new Ingredient();
            Repo = new TestRepository<Ingredient>();
            list = new ListEntity<Ingredient>();
            controller = SetUpController(Repo);
            ingredient = new Ingredient { ID = 1, Description = "IngredientsController_Test_Should" };
            Repo.Save(ingredient);
        }





        protected IngredientsController SetUpController(IRepository<Ingredient> repo)
        {


            list.ListT = new List<Ingredient> {
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

            foreach (Ingredient item in list.ListT)
            {
                Repo.Add(item);
            }


            controller = new IngredientsController(Repo)
            {
                PageSize = 3
            };

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


    }
}

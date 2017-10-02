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

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("RecipesController")]
    public class RecipesController_Test_Should
    { 
        protected static IRepository<Recipe> Repo { get; set; } 
        protected static RecipesController Controller { get; set; }
        protected ListEntity<Recipe> ListEntity { get; set; }

        public RecipesController_Test_Should()
        { 
            Repo = new TestRepository<Recipe>();
            ListEntity = new ListEntity<Recipe>();
            Controller = SetUpController(Repo);
         
        }



    

        protected RecipesController SetUpController(IRepository<Recipe> Repo)
        {

            ListEntity.ListT = new List<Recipe> {
                new Recipe {ID = int.MaxValue, Name = "RecipesController_Index_Test P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Recipe {ID = int.MaxValue-1, Name = "RecipesController_Index_Test P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new Recipe {ID = int.MaxValue-2, Name = "RecipesController_Index_Test P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new Recipe {ID = int.MaxValue-3, Name = "RecipesController_Index_Test P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Recipe {ID = int.MaxValue-4, Name = "RecipesController_Index_Test P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            };

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

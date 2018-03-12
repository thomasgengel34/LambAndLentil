using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Test.BasicTests;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.JSONTests
{
    [TestClass]
    public class JSONrepositoryShouldReturnCountOfThreeForDirectoryWithThreeFiles
    {
        static string path = @"../../../\LambAndLentil.Test\App_Data\JSON\Ingredient\";
        private static IRepository<Ingredient> repo;
        private static IGenericController<Ingredient> controller;
        private Ingredient item;

        public JSONrepositoryShouldReturnCountOfThreeForDirectoryWithThreeFiles()
        {
            repo = new TestRepository<Ingredient>(path);
        }


        [TestMethod]
        public void ReturnCountOfThreeForDirectoryWithThreeFiles()
        {
            BaseControllerTest<Ingredient>.SetUpForTests(out repo, out controller, out item);
            int count = repo.Count();

            repo.Save(new Ingredient() { ID = 1, Name = "Ichi" });
            repo.Save(new Ingredient() { ID = 2, Name = "Ni" });
            repo.Save(new Ingredient() { ID = 3, Name = "San" });


            int filesInDirectoryCount = Directory.EnumerateFiles(path).Count(); 
            
            Assert.AreEqual(count + 3, filesInDirectoryCount); 
        }


        [ClassCleanup()]
        public static void ClassCleanup()
        {
            try
            {
                IEnumerable<string> files = Directory.EnumerateFiles(path);
                foreach (string file in files)
                {
                    File.Delete(file);
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

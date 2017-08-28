using AutoMapper;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LambAndLentil.Test.JSONTests
{
    [TestClass]
    public class JSONRepositoryShouldReturnCountOfThreeForDirectoryWithThreeFiles
    {
        public static MapperConfiguration AutoMapperConfig { get; set; }
        static string path = @"../../../\LambAndLentil.Test\App_Data\JSON\Ingredient\";
        private static TestRepository< IngredientVM> repo { get; set; }

        public JSONRepositoryShouldReturnCountOfThreeForDirectoryWithThreeFiles()
        {
            AutoMapperConfigForTests.InitializeMap();
            repo = new TestRepository< IngredientVM>(); 
        }


        [TestMethod]
        public void ReturnCountOfThreeForDirectoryWithThreeFiles()
        {
            // Arrange
            ClassCleanup();


            // Act
            repo.Add(new IngredientVM() { ID = 1, Name = "Ichi" });
            repo.Add(new IngredientVM() { ID = 2, Name = "Ni" });
            repo.Add(new IngredientVM() { ID = 3, Name = "San" });


            int count = repo.Count();
            int filesInDirectoryCount = Directory.EnumerateFiles(path).Count();
            // Assert
            Assert.AreEqual(3, count);
            Assert.AreEqual(3, filesInDirectoryCount);

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

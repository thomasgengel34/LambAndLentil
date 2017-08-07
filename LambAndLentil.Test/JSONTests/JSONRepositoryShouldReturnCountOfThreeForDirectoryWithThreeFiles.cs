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
        static string path = @"../../../\LambAndLentil.Domain\App_Data\JSON\TestReturnCountOfThreeForDirectoryWithThreeFiles\";
        private static JSONRepository<Ingredient, IngredientVM> repo { get; set; }

        public JSONRepositoryShouldReturnCountOfThreeForDirectoryWithThreeFiles()
        {
            AutoMapperConfigForTests.InitializeMap();
            repo = new JSONRepository<Ingredient, IngredientVM>();
            Directory.CreateDirectory(path);
        }


        [TestMethod]
        public void ReturnCountOfThreeForDirectoryWithThreeFiles()
        {
            // Arrange
            // nothing


            // Act
            repo.AddT(new Ingredient() { ID = 1, Name = "Ichi" });
            repo.AddT(new Ingredient() { ID = 2, Name = "Ni" });
            repo.AddT(new Ingredient() { ID = 3, Name = "San" });


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
                Directory.Delete(path);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

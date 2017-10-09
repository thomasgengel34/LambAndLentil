using AutoMapper;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace LambAndLentil.Test.JSONTests
{
    [TestClass]
    public class JSONRepositoryGetByIDShould
    {
        public static MapperConfiguration AutoMapperConfig { get; set; }
        static string path = @"../../../\LambAndLentil.Domain\App_Data\JSON\TestJSONRepositoryGetByID\";

        private static JSONRepository<Ingredient> Repo { get; set; }


        public JSONRepositoryGetByIDShould()
        {
            AutoMapperConfigForTests.InitializeMap();
            Repo = new JSONRepository<Ingredient>();
            Directory.CreateDirectory(path);
        }
        [Ignore]
        [TestMethod]
        public void ReturnCorrectEntityForAValidIngredientID()
        {
            // Arrange
            Ingredient vm = new Ingredient()
            {
                ID = int.MaxValue,
                Name = "test ReturnCorrectEntityForAValidIngredientID Name",
                Description = "test ReturnCorrectEntityForAValidIngredientID Description"
            };
            Repo.Add(vm);

            // Act
            Ingredient returnedListEntity = Repo.GetById(vm.ID);

            //Assert
            Assert.AreEqual(vm.ID, returnedListEntity.ID);
            Assert.AreEqual(vm.Name, returnedListEntity.Name);
            Assert.AreEqual(vm.Description, returnedListEntity.Description);
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

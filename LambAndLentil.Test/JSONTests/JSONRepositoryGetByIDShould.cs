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

        private static JSONRepository<Ingredient, IngredientVM> repo { get; set; }


        public JSONRepositoryGetByIDShould()
        {
            AutoMapperConfigForTests.InitializeMap();
            repo = new JSONRepository<Ingredient, IngredientVM>();
            Directory.CreateDirectory(path);
        }

        [TestMethod]
        public void ReturnCorrectEntityForAValidIngredientID()
        {
            // Arrange
            IngredientVM vm = new IngredientVM()
            {
                ID = int.MaxValue,
                Name = "test ReturnCorrectEntityForAValidIngredientID Name",
                Description = "test ReturnCorrectEntityForAValidIngredientID Description"
            };
            repo.Add(vm);

            // Act
            IngredientVM returnedVm = repo.GetById(vm.ID);

            //Assert
            Assert.AreEqual(vm.ID, returnedVm.ID);
            Assert.AreEqual(vm.Name, returnedVm.Name);
            Assert.AreEqual(vm.Description, returnedVm.Description);
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

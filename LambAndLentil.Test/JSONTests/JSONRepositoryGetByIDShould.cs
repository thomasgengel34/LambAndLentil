//using AutoMapper;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace LambAndLentil.Test.JSONTests
{
    [TestClass]
    public class JSONRepositoryGetByIDShould
    {
       // public static MapperConfiguration AutoMapperConfig { get; set; }
        static string path = @"../../../\LambAndLentil.Test\App_Data\JSON\TestJSONRepositoryGetByID\";

        private static TestRepository<Ingredient> Repo { get; set; }


        public JSONRepositoryGetByIDShould()
        {
            AutoMapperConfigForTests.InitializeMap();
            Repo = new TestRepository<Ingredient>();
            Directory.CreateDirectory(path);
        }
       
        [TestMethod]
        public void ReturnCorrectEntityForAValidIngredientID()
        {
             Ingredient ingredient = new Ingredient()
            {
                ID = int.MaxValue,
                Name = "test ReturnCorrectEntityForAValidIngredientID Name",
                Description = "test ReturnCorrectEntityForAValidIngredientID Description"
            };
            Repo.Add(ingredient);
             
            Ingredient returnedListEntity = Repo.GetById(ingredient.ID);

            //Assert
            Assert.AreEqual(ingredient.ID, returnedListEntity.ID);
            Assert.AreEqual(ingredient.Name, returnedListEntity.Name);
            Assert.AreEqual(ingredient.Description, returnedListEntity.Description);
        }
         

        [ClassCleanup()]
        public static void ClassCleanup()
        { 
                IEnumerable<string> files = Directory.EnumerateFiles(path);
                foreach (string file in files)
                {
                    File.Delete(file);
                }
                Directory.Delete(path); 
        } 
    }
}

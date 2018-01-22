using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.JSONTests
{
    [TestClass]
    public class JSONRepositoryShouldReturnCountOfThreeForDirectoryWithThreeFiles
    { 
        static string path = @"../../../\LambAndLentil.Test\App_Data\JSON\Ingredient\";
        private static TestRepository< Ingredient> Repo { get; set; }

        public JSONRepositoryShouldReturnCountOfThreeForDirectoryWithThreeFiles()
        { 
            Repo = new TestRepository< Ingredient>(); 
        }


        [TestMethod]
        public void ReturnCountOfThreeForDirectoryWithThreeFiles()
        { 
            ClassCleanup();
             
            Repo.Save(new Ingredient() { ID = 1, Name = "Ichi" });
            Repo.Save(new Ingredient() { ID = 2, Name = "Ni" });
            Repo.Save(new Ingredient() { ID = 3, Name = "San" }); 

            int count = Repo.Count();
            int filesInDirectoryCount = Directory.EnumerateFiles(path).Count();
         
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

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
    public class JSONrepositoryShouldReturnCountOfThreeForDirectoryWithThreeFiles
    { 
        static string path = @"../../../\LambAndLentil.Test\App_Data\JSON\Ingredient\";
        private static TestRepository< Ingredient> repo { get; set; }

        public JSONrepositoryShouldReturnCountOfThreeForDirectoryWithThreeFiles()
        { 
            repo = new TestRepository< Ingredient>(); 
        }


        [TestMethod]
        public void ReturnCountOfThreeForDirectoryWithThreeFiles()
        { 
            ClassCleanup();
             
            repo.Save(new Ingredient() { ID = 1, Name = "Ichi" });
            repo.Save(new Ingredient() { ID = 2, Name = "Ni" });
            repo.Save(new Ingredient() { ID = 3, Name = "San" }); 

            int count = repo.Count();
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

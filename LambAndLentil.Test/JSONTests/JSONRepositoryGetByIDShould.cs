
using System.Collections.Generic;
using System.IO;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.JSONTests
{
    [TestClass]
    public class JSONrepositoryGetByIDShould
    {
       // public static MapperConfiguration AutoMapperConfig { get; set; }
        static string path = @"../../../\LambAndLentil.Test\App_Data\JSON\TestJSONrepositoryGetByID\";

        private static TestRepository<Ingredient> repo { get; set; }


        public JSONrepositoryGetByIDShould()
        { 
            repo = new TestRepository<Ingredient>();
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
            repo.Save(ingredient);
             
            Ingredient returnedListEntity = repo.GetById(ingredient.ID);

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

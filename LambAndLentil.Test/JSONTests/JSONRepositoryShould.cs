using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace LambAndLentil.Test.JSONTests
{
   
    [TestClass]
    public class JSONRepositoryShould
    {
        public static MapperConfiguration AutoMapperConfig { get; set; }
        static string path = @"../../../\LambAndLentil.Test\App_Data\JSON\TestReturnCountOfThreeForDirectoryWithThreeFiles\";
         
        public JSONRepositoryShould()
        {
            AutoMapperConfigForTests.InitializeMap();
        }

        [TestMethod]
        public void SaveOneIngredient()
        {
            // Arrange
            IRepository< Ingredient> Repo = new TestRepository< Ingredient>();
            //IngredientsController<Ingredient,Ingredient> Controller = new IngredientsController<Ingredient,Ingredient>(Repo);
            Ingredient ingredient = new Ingredient
            {
                ID = int.MaxValue,
                Name = "test SaveOneIngredient"
            };

            // Act
            Repo.Save(ingredient);

            Ingredient returnedIngredient = Repo.GetById(ingredient.ID);

            // Assert
            Assert.AreEqual(ingredient.ID, returnedIngredient.ID);
            Assert.AreEqual(ingredient.Name, returnedIngredient.Name);

        }
        [Ignore]
        [TestMethod]
        public void SaveOneRecipe()
        {
            // Arrange

            // Act


            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void SaveOneMenu()
        {
            // Arrange
            IRepository<Menu> Repo = new TestRepository<Menu>();
            Menu menu = new Menu
            {
                ID = 1405,
                Name = "SaveOneMenuTest ",
                ModifiedDate = new DateTime(1990, 12, 12),
                CreationDate = new DateTime(2003, 1, 2)
            };
            string file = "";
            // Act

            Repo.Add(menu);
            file = @"../../../\LambAndLentil.Test\App_Data\JSON\Menu\" + menu.ID + ".txt";
            StreamReader sr = new StreamReader(file);
            string theFile = "";
            string input;
            while ((input = sr.ReadLine()) != null)
            {
                theFile += input;
            }
            sr.Close();

            // Assert
            string text =  "{\"ID\":1405,\"MealType\":0,\"DayOfWeek\":0,\"Diners\":0,\"Ingredients\":[],\"Recipes\":[],\"Name\":\"SaveOneMenuTest \",\"Description\":\"not yet described\",\"CreationDate\":\"2003-01-02T00:00:00\",\"ModifiedDate\":\"1990-12-12T00:00:00\",\"AddedByUser\":\"PFW\\\\Poncho\",\"ModifiedByUser\":\"PFW\\\\Poncho\"}";
/* "{\"MealType\":0,\"DayOfWeek\":0,\"Diners\":0,\"ID\":0,\"Name\":\"SaveOneMenu Test \",\"Description\":\"not yet described\",\"CreationDate\":\"2003-01-02T00:00:00\",\"ModifiedDate\":\"1990-12-12T00:00:00\",\"AddedByUser\":\"PFW\\\\Poncho\",\"ModifiedByUser\":\"PFW\\\\Poncho\",\"Recipes\":null,\"Ingredients\":null,\"Menus\":null,\"Plans\":null,\"ShoppingLists\":null,\"Persons\":null}";*/

    string textNoLineBreaks = Regex.Replace(text, @"\r\n?|\n", "");
                string theFileNoLineBreaks = Regex.Replace(theFile, @"\r\n?|\n", "");
                Assert.AreEqual(textNoLineBreaks, theFileNoLineBreaks);
            
        }
        
        [TestMethod]
        public void SaveOnePlan()
        {
            // Arrange
            IRepository<Plan> Repo = new TestRepository<Plan>();
            Plan menu = new Plan
            {
                ID = 1405,
                Name = "SaveOnePlanTest ",
                ModifiedDate = new DateTime(1990, 12, 12),
                CreationDate = new DateTime(2003, 1, 2)
            };
            string file = "";
            // Act

            Repo.Add(menu);
            file = @"../../../\LambAndLentil.Test\App_Data\JSON\Plan\" + menu.ID + ".txt";
            StreamReader sr = new StreamReader(file);
            string theFile = "";
            string input;
            while ((input = sr.ReadLine()) != null)
            {
                theFile += input;
            }
            sr.Close();

            // Assert
            string text = "{\"ID\":1405,\"Ingredients\":[],\"Recipes\":[],\"Menus\":[],\"Name\":\"SaveOnePlanTest \",\"Description\":\"not yet described\",\"CreationDate\":\"2003-01-02T00:00:00\",\"ModifiedDate\":\"1990-12-12T00:00:00\",\"AddedByUser\":\"PFW\\\\Poncho\",\"ModifiedByUser\":\"PFW\\\\Poncho\"}";
         

            string textNoLineBreaks = Regex.Replace(text, @"\r\n?|\n", "");
            string theFileNoLineBreaks = Regex.Replace(theFile, @"\r\n?|\n", "");
            Assert.AreEqual(textNoLineBreaks, theFileNoLineBreaks);

        }
        
        [TestMethod]
        public void SaveOneShoppingList()
        {
            // Arrange
            IRepository<ShoppingList> Repo = new TestRepository<ShoppingList>();
            ShoppingList menu = new ShoppingList
            {
                ID = 1405,
                Name = "SaveOneShoppingListTest ",
                ModifiedDate = new DateTime(1990, 12, 12),
                CreationDate = new DateTime(2003, 1, 2)
            };
            string file = "";
            // Act

            Repo.Add(menu);
            file = @"../../../\LambAndLentil.Test\App_Data\JSON\ShoppingList\" + menu.ID + ".txt";
            StreamReader sr = new StreamReader(file);
            string theFile = "";
            string input;
            while ((input = sr.ReadLine()) != null)
            {
                theFile += input;
            }
            sr.Close();

            // Assert
            string text = "{\"Recipes\":[],\"Ingredients\":[],\"Menus\":[],\"Plans\":[],\"ID\":1405,\"Date\":\"0001-01-01T00:00:00\",\"Author\":null,\"Name\":\"SaveOneShoppingListTest \",\"Description\":\"not yet described\",\"CreationDate\":\"2003-01-02T00:00:00\",\"ModifiedDate\":\"1990-12-12T00:00:00\",\"AddedByUser\":\"PFW\\\\Poncho\",\"ModifiedByUser\":\"PFW\\\\Poncho\"}";


            string textNoLineBreaks = Regex.Replace(text, @"\r\n?|\n", "");
            string theFileNoLineBreaks = Regex.Replace(theFile, @"\r\n?|\n", "");
            Assert.AreEqual(textNoLineBreaks, theFileNoLineBreaks);

        }
        
        [TestMethod]
        public void SaveOnePerson()
        {
            // Arrange
            IRepository<Person> Repo = new TestRepository<Person>();
            Person menu = new Person
            {
                ID = 1405,
                FirstName = "SaveOnePersonTest ",
                LastName="",
                ModifiedDate = new DateTime(1990, 12, 12),
                CreationDate = new DateTime(2003, 1, 2)
            };
            string file = "";
            // Act

            Repo.Add(menu);
            file = @"../../../\LambAndLentil.Test\App_Data\JSON\Person\" + menu.ID + ".txt";
            StreamReader sr = new StreamReader(file);
            string theFile = "";
            string input;
            while ((input = sr.ReadLine()) != null)
            {
                theFile += input;
            }
            sr.Close();

            // Assert
            string text = "{\"ID\":1405,\"FirstName\":\"SaveOnePersonTest \",\"LastName\":\"\",\"FullName\":\"Newly Created\",\"Weight\":0.0,\"MinCalories\":0,\"MaxCalories\":0,\"NoGarlic\":false,\"Recipes\":[],\"Ingredients\":[],\"Menus\":[],\"Plans\":[],\"ShoppingLists\":[],\"Name\":\"Newly Created\",\"Description\":\"not yet described\",\"CreationDate\":\"2003-01-02T00:00:00\",\"ModifiedDate\":\"1990-12-12T00:00:00\",\"AddedByUser\":\"PFW\\\\Poncho\",\"ModifiedByUser\":\"PFW\\\\Poncho\"}";


            string textNoLineBreaks = Regex.Replace(text, @"\r\n?|\n", "");
            string theFileNoLineBreaks = Regex.Replace(theFile, @"\r\n?|\n", "");
            Assert.AreEqual(textNoLineBreaks, theFileNoLineBreaks);

        }

        [TestMethod]
        public void ReturnZeroCountForEmptyDirectory()
        {
            // Arrange
            IRepository<TestReturnZeroCountForEmptyDirectoryVM> Repo = new TestRepository<TestReturnZeroCountForEmptyDirectoryVM>();
           string path = @"../../../\LambAndLentil.Test\App_Data\JSON\TestReturnZeroCountForEmptyDirectory\";
         
            try
            {
                Directory.CreateDirectory(path);

                // Act
                int count = Repo.Count();

                // Assert
                Assert.AreEqual(0, count);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // clean up
                Directory.Delete(path);
            }
        }
         

    

        private class TestReturnZeroCountForEmptyDirectory : BaseEntity, IEntity
        {
            public int ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        }

        private class TestReturnZeroCountForEmptyDirectoryVM : BaseEntity, IEntity
        {
            public int ID { get; set; }
        }


        private class GetRidOfMe : IDisposable
        {

           
            private bool disposedValue = false; // To detect redundant calls

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: dispose managed state (managed objects).
                    }

                    // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                    // TODO: set large fields to null.

                    disposedValue = true;
                }
            }

            // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
            // ~GetRidOfMe() {
            //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            //   Dispose(false);
            // }

            // This code added to correctly implement the disposable pattern.
            public void Dispose()
            {
                // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
                Dispose(true);
                // TODO: uncomment the following line if the finalizer is overridden above.
                // GC.SuppressFinalize(this);
            }
         

        }

        [Ignore]
        [TestMethod]
        public void NotThrowAnErrorForAValidModel()
        {
            // Arrange

            // Act


            // Assert
            Assert.Fail();
        }



        [Ignore]
        [TestMethod]
        public void ThrowAnErrorForAnInvalidModel()
        {
            // Arrange

            // Act


            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void BubbleUpErrorToCallingMethodForAnInvalidModel()
        {
            // Arrange

            // Act


            // Assert
            Assert.Fail();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\";
            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}

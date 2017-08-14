using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            IRepository<Ingredient, IngredientVM> repo = new TestRepository<Ingredient, IngredientVM>();
            //IngredientsController<Ingredient,IngredientVM> controller = new IngredientsController<Ingredient,IngredientVM>(repo);
            Ingredient ingredient = new Ingredient();
            ingredient.ID = int.MaxValue;
            ingredient.Name = "test SaveOneIngredient";

            // Act
            repo.SaveT(ingredient);

            Ingredient returnedIngredient = repo.GetTById(ingredient.ID);

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
        [Ignore]
        [TestMethod]
        public void SaveOneMenu()
        {
            // Arrange
            IRepository<Menu, MenuVM> repo = new JSONRepository<Menu, MenuVM>();
            MenuVM menuVM = new MenuVM();
            menuVM.Name = "SaveOneMenu Test ";
            menuVM.ModifiedDate = new DateTime(1990, 12, 12);
            menuVM.CreationDate = new DateTime(2003, 1, 2);
            string file = "";
            // Act
            try
            {
                repo.AddTVM(menuVM);
                file = @"../../../\LambAndLentil.Domain\App_Data\JSON\Menu\" + menuVM.Name + ".txt";
                StreamReader sr = new StreamReader(file);
                string theFile = "";
                string input;
                while ((input = sr.ReadLine()) != null)
                {
                    theFile += input;
                }
                sr.Close();

                // Assert
                string text = "{\"MealType\":0,\"DayOfWeek\":0,\"Diners\":0,\"ID\":0,\"Name\":\"SaveOneMenu Test \",\"Description\":\"not yet described\",\"CreationDate\":\"2003-01-02T00:00:00\",\"ModifiedDate\":\"1990-12-12T00:00:00\",\"AddedByUser\":\"PFW\\\\Poncho\",\"ModifiedByUser\":\"PFW\\\\Poncho\",\"Recipes\":null,\"Ingredients\":null,\"Menus\":null,\"Plans\":null,\"ShoppingLists\":null,\"Persons\":null}";
                string textNoLineBreaks = Regex.Replace(text, @"\r\n?|\n", "");
                string theFileNoLineBreaks = Regex.Replace(theFile, @"\r\n?|\n", "");
                Assert.AreEqual(textNoLineBreaks, theFileNoLineBreaks);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // TODO: cleanup
                File.Delete(file);
            }
        }
        [Ignore]
        [TestMethod]
        public void SaveOnePlan()
        {
            // Arrange

            // Act


            // Assert
            Assert.Fail();
        }
        [Ignore]
        [TestMethod]
        public void SaveOneShoppingList()
        {
            // Arrange

            // Act


            // Assert
            Assert.Fail();
        }
        [Ignore]
        [TestMethod]
        public void SaveOnePerson()
        {
            // Arrange

            // Act


            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnZeroCountForEmptyDirectory()
        {
            // Arrange
            IRepository<TestReturnZeroCountForEmptyDirectory, TestReturnZeroCountForEmptyDirectoryVM> repo = new TestRepository<TestReturnZeroCountForEmptyDirectory, TestReturnZeroCountForEmptyDirectoryVM>();
           string path = @"../../../\LambAndLentil.Test\App_Data\JSON\TestReturnZeroCountForEmptyDirectory\";
         
            try
            {
                Directory.CreateDirectory(path);

                // Act
                int count = repo.Count();

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

        private class TestReturnZeroCountForEmptyDirectoryVM : BaseVM, IEntity
        {

        }
         

        private class GetRidOfMe : IDisposable
        {

            #region IDisposable Support
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
            #endregion

        }

      
    }
}

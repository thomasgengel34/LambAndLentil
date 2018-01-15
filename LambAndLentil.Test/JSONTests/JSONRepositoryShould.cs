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
       // static string path = @"../../../\LambAndLentil.Test\App_Data\JSON\TestReturnCountOfThreeForDirectoryWithThreeFiles\";
         
        public JSONRepositoryShould()
        {
        //    AutoMapperConfigForTests.InitializeMap();
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

            Menu returnedMenu = Repo.GetById(menu.ID);

            // Assert
            Assert.IsNotNull(returnedMenu);
            Assert.AreEqual(menu.Name, returnedMenu.Name);
            
        }
        
        [TestMethod]
        public void SaveOneShoppingList()
        {
            // Arrange
            IRepository<ShoppingList> Repo = new TestRepository<ShoppingList>();
            ShoppingList plan = new ShoppingList
            {
                ID = 1405,
                Name = "SaveOneShoppingListTest ",
                ModifiedDate = new DateTime(1990, 12, 12),
                CreationDate = new DateTime(2003, 1, 2)
            };
            string file = "";
            // Act

            Repo.Add(plan);
            file = @"../../../\LambAndLentil.Test\App_Data\JSON\ShoppingList\" + plan.ID + ".txt";
            StreamReader sr = new StreamReader(file);
            string theFile = "";
            string input;
            while ((input = sr.ReadLine()) != null)
            {
                theFile += input;
            }
            sr.Close();
            ShoppingList returnedShoppingList = Repo.GetById(plan.ID);

            // Assert
            Assert.IsNotNull(returnedShoppingList);
            Assert.AreEqual(plan.Name, returnedShoppingList.Name);
        }
         
        
        [TestMethod]
        public void SaveOnePerson()
        {
            // Arrange
            IRepository<Person> Repo = new TestRepository<Person>();
            Person person = new Person
            {
                ID = 1405,
                FirstName = "SaveOnePersonTest ",
                LastName="",
                ModifiedDate = new DateTime(1990, 12, 12),
                CreationDate = new DateTime(2003, 1, 2)
            };
            string file = "";
            // Act

            Repo.Add(person);
            file = @"../../../\LambAndLentil.Test\App_Data\JSON\Person\" + person.ID + ".txt";
            StreamReader sr = new StreamReader(file);
            string theFile = "";
            string input;
            while ((input = sr.ReadLine()) != null)
            {
                theFile += input;
            }
            sr.Close();

            Person returnedPerson = Repo.GetById(person.ID);

            // Assert
            Assert.IsNotNull(returnedPerson);
            Assert.AreEqual(person.Name, returnedPerson.Name);
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
            public List<Ingredient> Ingredients { get; set; }
            string IEntity.AddedByUser { get; set; }
            DateTime IEntity.CreationDate { get; set; }
            int IEntity.ID { get; set; }
            string IEntity.ModifiedByUser { get; set; }
            DateTime IEntity.ModifiedDate { get; set; }
            string IEntity.Name { get; set; }
            string IEntity.Description { get; set; }
            string IEntity.IngredientsList { get; set; }
            List<Ingredient> IEntity.Ingredients { get; set; }

            public bool ParentCanHaveChild(IPossibleChildren parent) => throw new NotImplementedException(); 
            int IEntity.GetCountOfChildrenOnParent(IEntity parent) => throw new NotImplementedException();
            bool IEntity.ParentCanHaveChild(IEntity parent) => throw new NotImplementedException();
            void IEntity.ParentRemoveAllChildrenOfAType(IEntity  parent, IEntity child) => throw new NotImplementedException();
            IEntity  IEntity.RemoveSelectionFromChildren<TChild>(IEntityChildClassIngredients parent, List<TChild> selected) => throw new NotImplementedException();
            IEntity IEntity.RemoveSelectionFromChildren<TChild>(IEntityChildClassRecipes parent, List<TChild> selected) => throw new NotImplementedException();
        }

        private class TestReturnZeroCountForEmptyDirectoryVM : BaseEntity, IEntity
        { 
            string IEntity.AddedByUser { get; set; }
            DateTime IEntity.CreationDate { get; set; }
            int IEntity.ID { get; set; }
            string IEntity.ModifiedByUser { get; set; }
            DateTime IEntity.ModifiedDate { get; set; }
            string IEntity.Name { get; set; }
            string IEntity.Description { get; set; }
            string IEntity.IngredientsList { get; set; }
            List<Ingredient> IEntity.Ingredients { get; set; }

            public bool ParentCanHaveChild(IPossibleChildren parent) => throw new NotImplementedException(); 
            int IEntity.GetCountOfChildrenOnParent(IEntity parent) => throw new NotImplementedException();
            bool IEntity.ParentCanHaveChild(IEntity parent) => throw new NotImplementedException();
            void IEntity.ParentRemoveAllChildrenOfAType(IEntity  parent, IEntity child) => throw new NotImplementedException();
            IEntity IEntity.RemoveSelectionFromChildren<TChild>(IEntityChildClassIngredients parent, List<TChild> selected) => throw new NotImplementedException();
            IEntity IEntity.RemoveSelectionFromChildren<TChild>(IEntityChildClassRecipes parent, List<TChild> selected) => throw new NotImplementedException();
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

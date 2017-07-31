using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using LambAndLentil.Domain.Concrete;
using AutoMapper;
using System.IO;
using System.Text.RegularExpressions;
using LambAndLentil.Tests.Infrastructure;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;
using System.Diagnostics;

namespace LambAndLentil.Test.JSONTests
{
    [TestClass]
    public class JSONRepositoryShould
    {
        public static MapperConfiguration AutoMapperConfig { get; set; }
        static string path = @"../../../\LambAndLentil.Domain\App_Data\JSON\TestReturnCountOfThreeForDirectoryWithThreeFiles\";
        public JSONRepositoryShould()
        {
            AutoMapperConfigForTests.InitializeMap();
        }
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
                repo.Add(menuVM);
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


        [TestMethod]
        public void ReturnZeroCountForEmptyDirectory()
        {
            // Arrange
            IRepository<TestReturnZeroCountForEmptyDirectory, TestReturnZeroCountForEmptyDirectoryVM> repo = new JSONRepository<TestReturnZeroCountForEmptyDirectory, TestReturnZeroCountForEmptyDirectoryVM>();
            string path = @"../../../\LambAndLentil.Domain\App_Data\JSON\TestReturnZeroCountForEmptyDirectory\";
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

        [TestMethod]
        public void ReturnCountOfThreeForDirectoryWithThreeFiles()
        {
            // Arrange
            Process x = Process.GetCurrentProcess();
         
            try
            {
                using (GetRidOfMe g = new GetRidOfMe())
                {
                    Process y = Process.GetCurrentProcess();
                    CreateThreeFiles(path);
                    g.Dispose();
                    y.Close();
                }
                using (GetRidOfMe g1 = new GetRidOfMe())
                {
                    Process z = Process.GetCurrentProcess();
                    IRepository<TestReturnCountOfThreeForDirectoryWithThreeFiles, TestReturnCountOfThreeForDirectoryWithThreeFilesVM> repo = new JSONRepository<TestReturnCountOfThreeForDirectoryWithThreeFiles, TestReturnCountOfThreeForDirectoryWithThreeFilesVM>();
                    // Act
                    int count = repo.Count(); 
                    // Assert
                    Assert.AreEqual(3, count);
                    g1.Dispose();
                    z.Close();
                }
                x.Close();
                x.WaitForExit();
            }
            catch (Exception)
            {
                Process w = Process.GetCurrentProcess();
                w.Close();
                throw;
            }
            finally
            {
                
                // clean up
                try
                {
                  
                  
                 
                   x.WaitForExit();
                    // int interval = 1000;
                    //Thread.Sleep(interval);
                  
                    //var files = (from file in Directory.EnumerateFiles(path, "*.txt" )
                    //            from line in File.ReadLines(file)
                    //            select file).ToList(); 

                    //foreach (string file in files)
                    //{  
                    //    File.Delete(file);
                    //}
                }
                catch (UnauthorizedAccessException UAEx)
                {
                    Console.WriteLine(UAEx.Message);
                }
                catch (PathTooLongException PathEx)
                {
                    Console.WriteLine(PathEx.Message);
                }
                catch (Exception  )
                {
                    throw;
                }

            }
        }
        [TestCleanup()]
        public void Cleanup()
        {
            File.Delete(path + "Ni.txt");
            File.Delete(path + "Ichi.txt");
            File.Delete(path + "San.txt");
            Directory.Delete(path);
        }
        private static void CreateThreeFiles(string path)
        {
            Directory.CreateDirectory(path);
            File.Create(path + "Ichi.txt");
            File.Create(path + "Ni.txt");
            File.Create(path + "San.txt");
        }

        private class TestReturnZeroCountForEmptyDirectory : BaseEntity, IEntity
        {
            public int ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        }

        private class TestReturnZeroCountForEmptyDirectoryVM : BaseVM, IEntity
        {

        }

        private class TestReturnCountOfThreeForDirectoryWithThreeFiles : BaseEntity, IEntity
        {
            public int ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        }

        private class TestReturnCountOfThreeForDirectoryWithThreeFilesVM : BaseVM, IEntity
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

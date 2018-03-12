using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicTests
{
   public class BaseControllerTest<T>
      where T : BaseEntity, IEntity,  new()
    {
        internal static IGenericController<T> controller;
        internal static IRepository<T> repo;
        internal static ListEntity<T> ListEntity;
        internal static T item; 
        internal List<T> list;
        internal static string className;
         

        public static void SetUpForTests(out IRepository<T> repo, out IGenericController<T> controller, out T item)
        {
            repo = new TestRepository<T>();
            controller = BaseControllerTestFactory();
            item = new T { ID = 1000, AddedByUser = "Not Changed", ModifiedByUser = "Original" };
            repo.Save(item, repo.FullPath);
        }
        [ClassInitialize]
        private void BaseControllerTestSetup()
        {
            SetUpForTests(out repo, out controller, out item);
            ListEntity.ListT = SetUpRepository(); 
            controller.PageSize = 3; 
        }

        internal static IGenericController<T> BaseControllerTestFactory()
        {
            Type type = typeof(T);

            if (typeof(T) == typeof(Ingredient))
            {
                return (IGenericController<T>)(new IngredientsController(new TestRepository<Ingredient>()));
            }
            else if (typeof(T) == typeof(Recipe))
            {
                return (IGenericController<T>)(new RecipesController(new TestRepository<Recipe>()));
            }
            else if (typeof(T) == typeof(Menu))
            {
                return (IGenericController<T>)(new MenusController(new TestRepository<Menu>()));
            }
            else if (typeof(T) == typeof(Plan))
            {
                return (IGenericController<T>)(new PlansController(new TestRepository<Plan>()));
            }
            else if (typeof(T) == typeof(Person))
            {
                return (IGenericController<T>)(new PersonsController(new TestRepository<Person>()));
            }
            else if (typeof(T) == typeof(ShoppingList))
            {
                return (IGenericController<T>)(new ShoppingListsController(new TestRepository<ShoppingList>()));
            }
            else throw new Exception();
        }
         
      

        internal List<T> SetUpRepository()
        { 
             list = new List<T> {
                new T {ID = int.MaxValue, Name ="ControllerTest1" ,
                    Description="test Tscontroller.Setup", AddedByUser ="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new T {ID = int.MaxValue-1, Name =  "ControllerTest2",
                    Description="test Tscontroller.Setup",  AddedByUser="Sally Doe",  ModifiedByUser="Mordecai", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new T {ID = int.MaxValue-2, Name = "ControllerTest3",
                    Description="test Tscontroller.Setup",  AddedByUser="Sue Doe", ModifiedByUser="Milton", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new T {ID = int.MaxValue-3, Name =  "ControllerTest4",
                    Description="test Tscontroller.Setup",  AddedByUser="Kyle Doe" ,ModifiedByUser="Michaelangelo", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new T {ID = int.MaxValue-4, Name =  "ControllerTest5",
                    Description="test Tscontroller.Setup",  AddedByUser="Buck Doe",  ModifiedByUser="Maurice", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            };

            foreach (T item in list)
            {
                repo.Save(item, repo.FullPath);
            }
            return list;
        }


        [TestCleanup]
       internal static void TestCleanup() => ClassCleanup();

        [ClassCleanup()]
        internal static void ClassCleanup()
        {
            Type type = typeof(T);
            className = type.Name.Split('.').Last();
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\" + className + @"\";

            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                 File.Delete(file);
            }
        } 
    }
}

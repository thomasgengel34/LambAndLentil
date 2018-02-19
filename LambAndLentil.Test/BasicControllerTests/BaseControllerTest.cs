using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BaseControllerTests
{
    public class BaseControllerTest<T>
      where T : BaseEntity, IEntity,  new()
    {
        internal static IGenericController<T> Controller { get; set; }   // TODO: convert these to private fields if possible
        internal static IRepository<T> Repo { get; set; }
        internal static ListEntity<T> ListEntity;
        internal static T item; 
        internal List<T> list;
        internal static string className;

        public BaseControllerTest()
        {
            BaseControllerTestSetup();
        }

         internal static void SetUpForTests(out IRepository<T> repo, out IGenericController<T> controller, out T item)
        {
            repo = new TestRepository<T>();
            controller = BaseControllerTestFactory(typeof(T));
            item = new T { ID = 1000, AddedByUser = "Not Changed", ModifiedByUser = "Original" };
            repo.Save(item);
        }
        [ClassInitialize]
        private void BaseControllerTestSetup()
        {
            Type type = typeof(T);
            className = type.Name.Split('.').Last();
            ClassCleanup();
            Repo = new TestRepository<T>();
            ListEntity = new ListEntity<T>
            {
                ListT = new List<T>()
            };
            item = new T
            {
                ID = 1,
                Name = "Name from BasicController_Test",
                Description = "BasicController_Test",
                CreationDate = new DateTime(2001, 2, 2),
                Ingredients = new List<Ingredient>()
            };
            Repo.Save(item);
            ListEntity.ListT = SetUpRepository();


            Controller = BaseControllerTestFactory(typeof(T));
            Controller.PageSize = 3;
           
        }


   internal static IGenericController<T> BaseControllerTestFactory( Type T)
        {
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
                    Description="test TsController.Setup", AddedByUser ="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new T {ID = int.MaxValue-1, Name =  "ControllerTest2",
                    Description="test TsController.Setup",  AddedByUser="Sally Doe",  ModifiedByUser="Mordecai", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new T {ID = int.MaxValue-2, Name = "ControllerTest3",
                    Description="test TsController.Setup",  AddedByUser="Sue Doe", ModifiedByUser="Milton", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new T {ID = int.MaxValue-3, Name =  "ControllerTest4",
                    Description="test TsController.Setup",  AddedByUser="Kyle Doe" ,ModifiedByUser="Michaelangelo", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new T {ID = int.MaxValue-4, Name =  "ControllerTest5",
                    Description="test TsController.Setup",  AddedByUser="Buck Doe",  ModifiedByUser="Maurice", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            };

            foreach (T item in list)
            {
                Repo.Save(item);
            }
            return list;
        }


        [TestCleanup]
       internal static void TestCleanup() => ClassCleanup();

        [ClassCleanup()]
        public static void ClassCleanup()
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

using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LambAndLentil.Test.BasicControllerTests
{ 
      public class BaseControllerTest<T>
        where T : BaseEntity, IEntity, new()
    {
        public static IRepository<T> Repo { get; set; } 
        public static ListEntity<T> ListEntity;
        public static T item;
        internal static string ClassName {get; private set;}

        public BaseControllerTest()
        { 
            Repo = new TestRepository<T>();
            ListEntity = new ListEntity<T>
            {
                ListT = new List<T>()
            };
            item = new T
            {
                ID = 1,
                Name="Name from BasicController_Test",
                Description = "BasicController_Test",
                CreationDate= new DateTime(2001,2,2)  ,
                 
            };
            Repo.Save(item);
            ListEntity.ListT = SetUpRepository();
            ClassName = typeof(T).ToString().Split('.').Last();  
        }

      public List<T> SetUpRepository()
        {
            string t = typeof(T).ToString();
            List<T> list = new List<T> {
                new T {ID = int.MaxValue, Name =t+ " ControllerTest1" ,
                    Description="test TsController.Setup", AddedByUser ="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new T {ID = int.MaxValue-1, Name = t+ " ControllerTest2",
                    Description="test TsController.Setup",  AddedByUser="Sally Doe",  ModifiedByUser="Mordecai", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new T {ID = int.MaxValue-2, Name = t+ " ControllerTest3",
                    Description="test TsController.Setup",  AddedByUser="Sue Doe", ModifiedByUser="Milton", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new T {ID = int.MaxValue-3, Name = t+ " ControllerTest4",
                    Description="test TsController.Setup",  AddedByUser="Kyle Doe" ,ModifiedByUser="Michaelangelo", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new T {ID = int.MaxValue-4, Name = t+ " ControllerTest5",
                    Description="test TsController.Setup",  AddedByUser="Buck Doe",  ModifiedByUser="Maurice", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            };

            foreach (T item in list)
            {
                Repo.Add(item);
            }
            return list;
        }
        [TestCleanup]
       public void TestCleanup()
        {
            ClassCleanup();
        }

        [ClassCleanup()]
       public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\"+ClassName+@"\";

            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}

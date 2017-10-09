using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambAndLentil.Test.BasicControllerTests
{
    public class BaseControllerTest<T>
        where T:BaseEntity, IEntity, new()
    {
        public static IRepository<T> Repo { get; set; }
        // public static MapperConfiguration AutoMapperConfig { get; set; }
        public static ListEntity<T> ListEntity;
        public static T item;

        public BaseControllerTest()
        {
            // AutoMapperConfigForTests.InitializeMap();
            Repo = new TestRepository<T>();
            ListEntity = new ListEntity<T>();
            item = new T { ID = 1, Description = "BasicController_Test" };
            Repo.Save(item);
        }

        public List<T> SetUpRepository()
        {
            string t =typeof(T).ToString();
            List<T> list = new List<T> {
                new T {ID = int.MaxValue, Name =t+ " ControllerTest1" ,
                    Description="test TsController.Setup", AddedByUser ="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new T {ID = int.MaxValue-1, Name = "TsControllerTest2",
                    Description="test TsController.Setup",  AddedByUser="Sally Doe",  ModifiedByUser="Mordecai", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new T {ID = int.MaxValue-2, Name = "TsControllerTest3",
                    Description="test TsController.Setup",  AddedByUser="Sue Doe", ModifiedByUser="Milton", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new T {ID = int.MaxValue-3, Name = "TsControllerTest4",
                    Description="test TsController.Setup",  AddedByUser="Kyle Doe" ,ModifiedByUser="Michaelangelo", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new T {ID = int.MaxValue-4, Name = "TsControllerTest5",
                    Description="test TsController.Setup",  AddedByUser="Buck Doe",  ModifiedByUser="Maurice", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            } ;

            foreach (T item in list)
            {
                Repo.Add(item);
            }
            return list;
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void MyTestMethod()
        {

        }
    }
}

using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]  
    [TestCategory("PersonsController")]
    public class PersonsController_Test_Should
    {
        protected static IRepository<Person> Repo { get; set; }
        protected static MapperConfiguration AutoMapperConfig { get; set; }
       protected static ListEntity<Person> list;
        protected static PersonsController Controller { get; set; }

        public PersonsController_Test_Should()
        {
            Repo = new TestRepository<Person>();
            list = new ListEntity<Person>();
            Controller = SetUpController(Repo);
        }

        internal PersonsController SetUpController(IRepository<Person> Repo)
        {
            list.ListT= new List <Person> {
                new Person{ID = int.MaxValue, Name = "PersonsController_Index_Test P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Person{ID = int.MaxValue-1, Name = "PersonsController_Index_Test P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new Person{ID = int.MaxValue-2, Name = "PersonsController_Index_Test P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new Person{ID = int.MaxValue-3, Name = "PersonsController_Index_Test P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Person{ID = int.MaxValue-4, Name = "PersonsController_Index_Test P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            } ;

            foreach (Person person in list.ListT )
            {
                Repo.Add(person);
            }

            Controller = new PersonsController(Repo)
            {
                PageSize = 3
            };

            return Controller;
        }

        

        [TestCleanup()]
        public void TestCleanup()
        {
            ClassCleanup();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Person\";

            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}

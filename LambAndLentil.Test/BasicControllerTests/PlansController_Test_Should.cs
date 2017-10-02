using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("PlansController")]
    public class PlansController_Test_Should
    {

        protected static IRepository<Plan> Repo { get; set; }
        protected static MapperConfiguration AutoMapperConfig { get; set; }
        protected static ListEntity<Plan> list;
        protected static PlansController Controller { get; set; }

        public PlansController_Test_Should()
        {
            AutoMapperConfigForTests.InitializeMap();
            Repo = new TestRepository<Plan>();
            list = new ListEntity<Plan>();
            Controller = SetUpController(Repo);
        }

       internal PlansController SetUpController(IRepository<Plan> Repo)
        {
            list.ListT  = new List<Plan> {
                new Plan{ID = int.MaxValue, Name = "PlansController_Index_Test P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Plan{ID = int.MaxValue-1, Name = "PlansController_Index_Test P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new Plan{ID = int.MaxValue-2, Name = "PlansController_Index_Test P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new Plan{ID = int.MaxValue-3, Name = "PlansController_Index_Test P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Plan{ID = int.MaxValue-4, Name = "PlansController_Index_Test P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            };

            foreach (Plan plan in list.ListT )
            {
                Repo.Add(plan);
            }

            Controller = new PlansController(Repo)
            {
                PageSize = 3
            };

            return Controller;
        }
         

        [TestCleanup]
        [TestMethod]
        public void MyTestMethod()
        {
            ClassCleanup();
        }


        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Plan\";

            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using System.Linq;
using LambAndLentil.UI.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using AutoMapper;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI;
using LambAndLentil.UI.HtmlHelpers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.Domain.Concrete;
using System.IO;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("MenusController")]
    public class MenusController_Test_Should
    {
        protected static IRepository<Menu> Repo { get; set; }
        public static MapperConfiguration AutoMapperConfig { get; set; }
        protected static ListEntity<Menu> ListEntity { get; set; }
        protected static MenusController Controller { get; set; }
        protected static Menu menuVM;

        public MenusController_Test_Should()
        {
            AutoMapperConfigForTests.InitializeMap();
            Repo = new TestRepository<Menu>();
            ListEntity = new ListEntity<Menu>();
            Controller = SetUpController(Repo);
            menuVM = new Menu { ID = 1, Description = "MenusController_Test_Should" };
            Repo.Save(menuVM);
        }

        protected internal MenusController SetUpController(IRepository<Menu> repo)
        {
            ListEntity.ListT = new List<Menu> {
                        new Menu {ID = int.MaxValue, Name = "MenusController_Index_Test P1" ,Description="test MenusController.Setup",AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                        new Menu {ID = int.MaxValue-1, Name = "MenusController_Index_Test P2",Description="test MenusController.Setup",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                        new Menu {ID = int.MaxValue-2, Name = "MenusController_Index_Test P3", Description="test MenusController.Setup", AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                        new Menu {ID = int.MaxValue-3, Name = "MenusController_Index_Test P4", Description="test MenusController.Setup", AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                        new Menu {ID = int.MaxValue-4, Name = "MenusController_Index_Test P5", Description="test MenusController.Setup", AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
                    };

            foreach (Menu item in ListEntity.ListT)
            {
                Repo.Add(item);
            }

            Controller = new MenusController(Repo)
            {
                PageSize = 3
            };

            return Controller;
        }



        [TestCleanup()]
        public void TestCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Menu\";

            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            } 
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            new MenusController_Test_Should().TestCleanup();
        }
    }
}

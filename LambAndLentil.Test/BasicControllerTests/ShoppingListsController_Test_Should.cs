using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
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
    [TestCategory("ShoppingListsController")]
    public class  ShoppingListsController_Test_Should
    {
        protected static IRepository<ShoppingList> Repo { get; set; }
       protected static MapperConfiguration AutoMapperConfig { get; set; }
        private static ListEntity<ShoppingList> list;
        protected static ShoppingListsController Controller { get; set; }

        public  ShoppingListsController_Test_Should()
        {
            AutoMapperConfigForTests.InitializeMap();
            Repo = new TestRepository<ShoppingList>();
            list = new ListEntity<ShoppingList>();
            Controller = SetUpController(Repo);
        }

        protected ShoppingListsController SetUpController(IRepository<ShoppingList> Repo)
        {

            list.ListT  = new List<ShoppingList> {
                new ShoppingList {ID = int.MaxValue, Name = "ShoppingListsController_Index_Test P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new ShoppingList {ID = int.MaxValue-1, Name = "ShoppingListsController_Index_Test P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new ShoppingList {ID = int.MaxValue-2, Name = "ShoppingListsController_Index_Test P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new ShoppingList {ID = int.MaxValue-3, Name = "ShoppingListsController_Index_Test P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new ShoppingList {ID = int.MaxValue-4, Name = "ShoppingListsController_Index_Test P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            }.AsQueryable() ;

            foreach (ShoppingList ingredient in list.ListT)
            {
                Repo.Add(ingredient);
            }

            Controller = new ShoppingListsController(Repo)
            {
                PageSize = 3
            };

            return Controller;
        }
         
        [TestCleanup]
        public void  TestCleanup()
        {
            ClassCleanup();
        }


        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\ShoppingList\";
            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }

        }
    }
}

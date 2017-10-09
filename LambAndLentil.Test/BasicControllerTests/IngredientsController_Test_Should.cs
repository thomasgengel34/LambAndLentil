//using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory(" IngredientsController")]
    public class IngredientsController_Test_Should:BaseControllerTest<Ingredient>
    { 
        public static IngredientsController Controller; 

        public IngredientsController_Test_Should()
        {   
            ListEntity.ListT=SetUpRepository();
            Controller.PageSize = 3;
        }
    

        [TestCleanup()]
        public void TestCleanup()
        {
            ClassCleanup();
        }



        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Ingredient\";
            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }


    }
}

using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("IngredientsController")]
    [TestCategory("Index")]
    [Ignore]
    public class IngredientsController_GetIngredients_Test:IngredientsController_Test_Should
    { 

        public IngredientsController_GetIngredients_Test()
        { 
        }


        [TestMethod]
        public void MyTestMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();

        }
    }
}

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
    public class MenusController_Test_Misc: MenusController_Test_Should
    {
       
        public MenusController_Test_Misc()
        {
            Menu = new Menu { ID = 1000, Name = "Original Name" };
            Repo.Save(Menu);
        }

        [TestMethod]
        public void InheritsFromBaseControllerCorrectly()
        {

            Assert.IsNotNull("this is a placeholder");
        }

        [TestMethod]
        public void IsPublic()
        {
            // Arrange 

            // Act
            Type type = Controller.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(true, isPublic);
        }
         

        [Ignore]
        [TestMethod]
        public void FlagAnIngredientFlaggedInAPerson()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void FlagAnIngredientFlaggedInTwoPersons()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void WhenAFlagHasBeenRemovedFromOnePersonStillThereForSecondFlaggedPerson()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("Copy")]
        [TestMethod]
        public void CopyModifySaveWithANewName()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void CorrectMenuPropertiesAreBoundInEdit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetTheClassNameCorrect()
        {
            // Arrange

            // Act


            // Assert
            //  Assert.Fail();
            Assert.AreEqual("LambAndLentil.UI.Controllers.MenusController", MenusController_Test_Should.Controller.ToString());
        }


      

      

        [TestMethod]
        public void ShouldEditName()
        {
            // Arrange

            // Act
            Menu.Name = "Name is changed";
            Controller.PostEdit(Menu);
            ReturnedMenu = Repo.GetById(1000);

            // Assert
            Assert.AreEqual("Name is changed", ReturnedMenu.Name);
        }
    }
}

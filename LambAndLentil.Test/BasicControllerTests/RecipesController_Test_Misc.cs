﻿using LambAndLentil.UI.Controllers;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.UI.Models;
using System.Collections.Generic;
using AutoMapper;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI;
using System.IO;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("RecipesController")]
    public class RecipesController_Test_Misc:RecipesController_Test_Should
    {
    
         

        [TestMethod]
        public void InheritFromBaseController()
        {
            // Arrange


            // Act 
            Type baseType = typeof(BaseController<Recipe>);
            bool isBase = baseType.IsInstanceOfType(Controller);

            // Assert 
            Assert.AreEqual(isBase, true);
        }

        protected RecipesController SetUpController(IRepository<Recipe> Repo)
        {

             ListEntity.ListT = new List<Recipe> {
                new Recipe {ID = int.MaxValue, Name = "RecipesController_Index_Test P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Recipe {ID = int.MaxValue-1, Name = "RecipesController_Index_Test P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new Recipe {ID = int.MaxValue-2, Name = "RecipesController_Index_Test P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new Recipe {ID = int.MaxValue-3, Name = "RecipesController_Index_Test P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Recipe {ID = int.MaxValue-4, Name = "RecipesController_Index_Test P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            };

            foreach (Recipe recipe in  ListEntity.ListT)
            {
                Repo.Add(recipe);
            }

            Controller = new RecipesController(Repo)
            {
                PageSize = 3
            };

            return Controller;
        }



        [TestMethod]
        public void BePublic()
        { // this is the one I just worked on.
            // Arrange 

            // Act
            Type type = Controller.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
             
        }

        [TestMethod]
        public void ReturnNonNullIndex()
        {
            // Arrange


            // Act
            ViewResult result = Controller.Index(1) as ViewResult;
            ViewResult result1 = Controller.Index(2) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result1);

        }

        
       

         [Ignore]
        [TestMethod]
        public void ReturnShortClassNameInErrorMessages()
        { // e.g. "Recipe not found" not "LambAndLentil.Domain.Entities.Recipe not found"
            Assert.Fail();
        }

         [Ignore]
        [TestMethod]
        public void ReturnFullyDefinedClassNameWhereNeeded()
        { // where is it needed?
            Assert.Fail();
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
        public void CorrectRecipesAreBoundInEdit()
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
            Assert.AreEqual("LambAndLentil.UI.Controllers.RecipesController", RecipesController_Test_Should.Controller.ToString());
        }

        private class FakeRepository : TestRepository<Recipe> { }


        [TestMethod]
        [ExpectedException(typeof(Exception), "Fake Repostory")]
        public void ReturnsErrorWithUnknownRepository()
        {
            // Arrange
            FakeRepository fakeRepo = new FakeRepository();
            RecipesController fController = new RecipesController(fakeRepo);
            // Act
            ActionResult ar = fController.BaseAttach(fakeRepo, int.MaxValue, new Ingredient());
            // Assert

        } 
    }
}

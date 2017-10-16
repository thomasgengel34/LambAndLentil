using AutoMapper;
using IntegrationTests;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Controllers;
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
using LambAndLentil.Tests.Infrastructure;
using System.Security.Principal;
using LambAndLentil.Test.BasicControllerTests;

namespace LambAndLentil.Test.Infrastructure
{
    [TestClass]
    // switching to WebApi methods, but something will be needed
    [TestCategory("Integration")]
    [TestCategory("IngredientsController")]
    public class IngredientsControllerShould:IngredientsController_Test_Should
    { 
        public IngredientsControllerShould()
        { 
        }

         


        [TestMethod]
        [TestCategory("Create")]
        public void CreateAnIngredient()
        {
            // Arrange

            // Act
            ViewResult vr = Controller.Create(UIViewType.Create);
            string modelName = ((Ingredient)vr.Model).Name;
            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created");
        }

        [TestMethod]
        [TestCategory("Save")]
        public void SaveAValidIngredientAndReturnIndexView()
        {
            // Arrange
            Ingredient vm = new Ingredient
            {
                ID = 3000,
                Name = "SaveAValidIngredientAndReturnIndexView"
            };

            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.PostEdit(vm);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;

            // Assert 

            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(0).ToString());

        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedIngredient()
        {
            // Arrange
            IngredientsController indexController = new IngredientsController(Repo);
            IngredientsController Controller2 = new IngredientsController(Repo);
            IngredientsController Controller3 = new IngredientsController(Repo);


            Ingredient vm = new Ingredient
            {
                Name = "0000 test",
                ID = int.MaxValue - 100,
                Description = "test IngredientsControllerShould.SaveEditedIngredient"
            };

            // Act 
            ActionResult ar1 = Controller.PostEdit(vm);


            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = 7777;
            ActionResult ar2 = Controller2.PostEdit(vm);
            ViewResult view2 = Controller3.Index();
            List<Ingredient> ListEntity2 = (List<Ingredient>)(( ListEntity<Ingredient>)view2.Model).ListT;
            Ingredient vm3 = (from m in ListEntity2
                              where m.Name == "0000 test Edited"
                              select m).AsQueryable().FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited", vm3.Name);
            Assert.AreEqual(7777, vm3.ID);

        }

        //[TestMethod]
        //[TestCategory("DeleteConfirmed")]
        //public void ActuallyDeleteAnIngredientFromTheDatabase()
        //{
        //    // Arrange   
        //    Controller = new IngredientsController_Index_Test().SetUpIngredientsController(Repo);
        //    Ingredient item = Repo.GetById(int.MaxValue);
        //    int countInRepo = Repo.Count();
        //    //Act
        //    Controller.DeleteConfirmed(item.ID);
        //    int count = Repo.Count();

        //    //Assert
        //    Assert.AreEqual(countInRepo - 1, count);
        //}

        [TestMethod]
        [TestCategory("Edit")]
        public void ShouldSaveTheCreationDateOnIngredientCreationWithDateTimeParameter()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);

            // Act
            Ingredient ingredient = new Ingredient(CreationDate);

            // Assert
            Assert.AreEqual(CreationDate, ingredient.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnIngredientCreationWithNoParameterCtor()
        {
            // Arrange
            DateTime CreationDate = DateTime.Now;

            // Act
            Ingredient ingredient = new Ingredient();

            // Assert
            Assert.AreEqual(CreationDate.Date, ingredient.CreationDate.Date);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnIngredientCreationWithDateTimeParameter()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);

            // Act
            Ingredient ingredient = new Ingredient(CreationDate);

            // Assert
            Assert.AreEqual(CreationDate, ingredient.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);
            Ingredient ingredient = new Ingredient(CreationDate)
            {
                ID = int.MaxValue - 200,
                Name = "test IngredientsControllerShould.SaveTheCreationDateBetweenPostedEdits"
            };


            IngredientsController ControllerEdit = new IngredientsController(Repo);
            IngredientsController ControllerView = new IngredientsController(Repo);
            IngredientsController ControllerDelete = new IngredientsController(Repo);

            // Act
            ControllerEdit.PostEdit(ingredient);
            ViewResult view = ControllerView.Index();
            List<Ingredient> ListEntity= (List<Ingredient>)(( ListEntity<Ingredient>)view.Model).ListT;
            Ingredient returnedListEntity = Repo.GetById(ingredient.ID);
            DateTime shouldBeSameDate = returnedListEntity.CreationDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);


        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            // Arrange 
            IngredientsController ControllerPost = new IngredientsController(Repo);
            IngredientsController ControllerPost1 = new IngredientsController(Repo);
            Ingredient vm = new Ingredient
            {
                ID = int.MaxValue - 300,
                Name = "002 Test Mod",
                Description = "test IngredientsControllerShould.UpdateTheModificationDateBetweenPostedEdits"
            };
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
            ControllerPost.PostEdit(vm);

            vm.Description += "I've been edited to delay a bit";

            ControllerPost1.PostEdit(vm);

            Ingredient returnedVM = Repo.GetById(vm.ID);

            DateTime shouldBeSameDate = returnedVM.CreationDate;
            DateTime shouldBeLaterDate = returnedVM.ModifiedDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);
            Assert.AreNotEqual(mod, shouldBeLaterDate);

        }

        [Ignore]
        [TestMethod]
        public void NotCreateASecondElementOnEditingOneElement()
        {
            Assert.Fail();
        }



        [TestMethod]
        [TestCategory("Edit")]
        public void ReturnIndexWithWarningForNonexistentIngredient()
        {
            // Arrange

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Edit(1000);
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            // Assert  
            Assert.AreEqual(UIViewType.Index.ToString(), rdr.RouteValues.ElementAt(0).Value.ToString());
            Assert.AreEqual("Ingredient was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        }

        [Ignore]
        [TestMethod]
        public void HaveIDBoundInCreateActionMethod()
        {
            //Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void HaveIDBoundInPostEditActionMethod()
        {
            //Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveNameBoundInCreateActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveNameBoundInPostEditActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }


        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveDescriptionBoundInCreateActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveDescriptionBoundInPostEditActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveRecipeBoundInCreateActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveRecipeBoundInPostEditActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveCreationDateBoundInCreateActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveCreationDateBoundInPostEditActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveModifiedDateBoundInCreateActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveModifiedDateBoundInPostEditActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveAddedByUserBoundInPostEditActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveAddedByUserBoundInCreateActionMethod()
        {
            // Arrange

            // Act
            Ingredient ingredient = new Ingredient();
            string addedByUser = WindowsIdentity.GetCurrent().Name;
            // Assert
            Assert.AreEqual(addedByUser, ingredient.AddedByUser);
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveModifiedByUserBoundInPostEditActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveModifiedByUserBoundInCreateActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveRecipesBoundInPostEditActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveRecipesBoundInCreateActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }


        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveIngredientsBoundInPostEditActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveIngredientsBoundInCreateActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void HaveIngredientsListBoundInPostEditActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }


        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveMenusBoundInCreateActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveMenusBoundInPostEditActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }


        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HavePlansBoundInCreateActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HavePlansBoundInPostEditActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }


        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveShoppingListsBoundInCreateActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveShoppingListsBoundInPostEditActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HavePersonsBoundInCreateActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HavePersonsBoundInPostEditActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }  
    }
}

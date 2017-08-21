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

namespace LambAndLentil.Test.Infrastructure
{
    [TestClass]
    // switching to WebApi methods, but something will be needed
    [TestCategory("Integration")]
    [TestCategory("IngredientsController")]
    public class IngredientsControllerShould
    {
        static IRepository<Ingredient, IngredientVM> repo;
        static IngredientsController controller;
        static ListVM<Ingredient, IngredientVM> ilvm;
        public static MapperConfiguration AutoMapperConfig  { get; set; }

        public IngredientsControllerShould()
        {
            AutoMapperConfigForTests.InitializeMap();
            repo = new TestRepository<Ingredient, IngredientVM>();
            ilvm = new ListVM<Ingredient, IngredientVM>();
            controller =  SetUpIngredientsController(repo);
        }


        public IngredientsController SetUpIngredientsController(IRepository<Ingredient, IngredientVM> repo)
        {
            ilvm.ListT = new List<Ingredient> {
                new Ingredient {ID = int.MaxValue, Name = "IngredientsController_Index_Test P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Ingredient {ID = int.MaxValue-1, Name = "IngredientsController_Index_Test P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new Ingredient {ID = int.MaxValue-2, Name = "IngredientsController_Index_Test P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new Ingredient {ID = int.MaxValue-3, Name = "IngredientsController_Index_Test P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new Ingredient {ID = int.MaxValue-4, Name = "IngredientsController_Index_Test P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            }.AsQueryable();

            foreach (Ingredient ingredient in ilvm.ListT)
            {
                repo.AddT(ingredient);
            }

            IngredientsController controller = new IngredientsController(repo);
            controller.PageSize = 3;

            return controller;
        }


        [TestMethod]
        [TestCategory("Create")]
        public void CreateAnIngredient()
        {
            // Arrange

            // Act
            ViewResult vr = controller.Create(UIViewType.Create);
            string modelName = ((IngredientVM)vr.Model).Name;
            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created");
        }

        [TestMethod]
        [TestCategory("Save")]
        public void SaveAValidIngredientAndReturnIndexView()
        {
            // Arrange
            IngredientVM vm = new IngredientVM();
            vm.Name = "test";

            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit(vm);
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
            IngredientsController indexController = new IngredientsController(repo);
            IngredientsController controller2 = new IngredientsController(repo);
            IngredientsController controller3 = new IngredientsController(repo);


            IngredientVM vm = new IngredientVM();
            vm.Name = "0000 test";
            vm.ID = int.MaxValue - 100;
            vm.Description = "test IngredientsControllerShould.SaveEditedIngredient";

            // Act 
            ActionResult ar1 = controller.PostEdit(vm);


            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = 7777;
            ActionResult ar2 = controller2.PostEdit(vm);
            ViewResult view2 = controller3.Index();
            ListVM<Ingredient, IngredientVM> listVM2 = (ListVM<Ingredient, IngredientVM>)view2.Model;
            IngredientVM vm3 = (from m in listVM2.ListTVM
                                where m.Name == "0000 test Edited"
                                select m).AsQueryable().FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited", vm3.Name);
            Assert.AreEqual(7777, vm3.ID);

        }

        [TestMethod]
        [TestCategory("DeleteConfirmed")]
        public void ActuallyDeleteAnIngredientFromTheDatabase()
        {
            // Arrange   
            controller = new IngredientsController_Index_Test().SetUpIngredientsController(repo);
            Ingredient item = repo.GetTById(int.MaxValue);
            int countInRepo = repo.Count();
            //Act
            controller.DeleteConfirmed(item.ID);
            int count = repo.Count();

            //Assert
            Assert.AreEqual(countInRepo - 1, count);
        }

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
        public void SaveTheCreationDateOnIngredientVMCreationWithNoParameterCtor()
        {
            // Arrange
            DateTime CreationDate = DateTime.Now;

            // Act
            IngredientVM ingredientVM = new IngredientVM();

            // Assert
            Assert.AreEqual(CreationDate.Date, ingredientVM.CreationDate.Date);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateOnIngredientVMCreationWithDateTimeParameter()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);

            // Act
            IngredientVM ingredientVM = new IngredientVM(CreationDate);

            // Assert
            Assert.AreEqual(CreationDate, ingredientVM.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);
            IngredientVM ingredientVM = new IngredientVM(CreationDate);
            ingredientVM.ID = int.MaxValue - 200;
            ingredientVM.Name = "test IngredientsControllerShould.SaveTheCreationDateBetweenPostedEdits";


            IngredientsController controllerEdit = new IngredientsController(repo);
            IngredientsController controllerView = new IngredientsController(repo);
            IngredientsController controllerDelete = new IngredientsController(repo);

            // Act
            controllerEdit.PostEdit(ingredientVM);
            ViewResult view = controllerView.Index();
            ListVM<Ingredient, IngredientVM> listVM = (ListVM<Ingredient, IngredientVM>)view.Model;
            IngredientVM returnedVm = repo.GetTVMById(ingredientVM.ID);
            DateTime shouldBeSameDate = returnedVm.CreationDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);


        }


        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            // Arrange 
            IngredientsController controllerPost = new IngredientsController(repo);
            IngredientsController controllerPost1 = new IngredientsController(repo); 
            IngredientVM vm = new IngredientVM();
            vm.ID = int.MaxValue - 300;
            vm.Name = "002 Test Mod";
            vm.Description = "test IngredientsControllerShould.UpdateTheModificationDateBetweenPostedEdits";
            DateTime CreationDate = vm.CreationDate;
            DateTime mod = vm.ModifiedDate;

            // Act
            controllerPost.PostEdit(vm);

            vm.Description += "I've been edited to delay a bit";

            controllerPost1.PostEdit(vm);

            IngredientVM returnedVM = repo.GetTVMById(vm.ID);

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
        public void IngredientsCtr_CannotEditNonexistentIngredient()
        {
            // Arrange

            // Act 
            ViewResult view = controller.Edit(8);
            object Model = view.Model;

            // Assert 
            Assert.IsNotNull(view);
            Assert.IsNull(Model);

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

        [Ignore]
        [TestCategory("BaseEntiity Property")]
        [TestMethod]
        public void HaveAddedByUserBoundInCreateActionMethod()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
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

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            IngredientsController_Index_Test.ClassCleanup();
        }
    }
}

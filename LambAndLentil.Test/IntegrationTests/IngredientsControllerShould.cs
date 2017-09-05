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
        static IRepository<IngredientVM> Repo;
        static IngredientsController controller;
        static ListVM<IngredientVM> ilvm;
        public static MapperConfiguration AutoMapperConfig  { get; set; }

        public IngredientsControllerShould()
        {
            AutoMapperConfigForTests.InitializeMap();
            Repo = new TestRepository<IngredientVM>();
            ilvm = new ListVM<IngredientVM>();
            controller =  SetUpIngredientsController(Repo);
        }


        public IngredientsController SetUpIngredientsController(IRepository<IngredientVM> repo)
        {
            ilvm.ListT = new List<IngredientVM> {
                new IngredientVM {ID = int.MaxValue, Name = "IngredientsController_Index_Test P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new IngredientVM {ID = int.MaxValue-1, Name = "IngredientsController_Index_Test P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new IngredientVM {ID = int.MaxValue-2, Name = "IngredientsController_Index_Test P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new IngredientVM {ID = int.MaxValue-3, Name = "IngredientsController_Index_Test P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new IngredientVM {ID = int.MaxValue-4, Name = "IngredientsController_Index_Test P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            }.AsQueryable();

            foreach (IngredientVM item in ilvm.ListT)
            {
                Repo.Add(item);
            }

             IngredientsController controller = new IngredientsController(Repo);
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
            IngredientsController indexController = new IngredientsController(Repo);
            IngredientsController controller2 = new IngredientsController(Repo);
            IngredientsController controller3 = new IngredientsController(Repo);


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
            ListVM<IngredientVM> listVM2 = (ListVM<IngredientVM>)view2.Model;
            IngredientVM vm3 = (from m in listVM2.ListT 
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
        //    controller = new IngredientsController_Index_Test().SetUpIngredientsController(Repo);
        //    Ingredient item = Repo.GetById(int.MaxValue);
        //    int countInRepo = Repo.Count();
        //    //Act
        //    controller.DeleteConfirmed(item.ID);
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


            IngredientsController controllerEdit = new IngredientsController(Repo);
            IngredientsController controllerView = new IngredientsController(Repo);
            IngredientsController controllerDelete = new IngredientsController(Repo);

            // Act
            controllerEdit.PostEdit(ingredientVM);
            ViewResult view = controllerView.Index();
            ListVM<IngredientVM> listVM = (ListVM<IngredientVM>)view.Model;
            IngredientVM returnedVm = Repo.GetById(ingredientVM.ID);
            DateTime shouldBeSameDate = returnedVm.CreationDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);


        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            // Arrange 
            IngredientsController controllerPost = new IngredientsController(Repo);
            IngredientsController controllerPost1 = new IngredientsController(Repo); 
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

            IngredientVM returnedVM = Repo.GetById(vm.ID);

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
         AlertDecoratorResult adr =(AlertDecoratorResult)controller.Edit(1000);
           //ViewResult view = (ViewResult)adr.InnerResult;
            //     object Model = view.Model;

            // Assert 
            //    Assert.IsNotNull(view);
            //    Assert.AreEqual(UIViewType.Index, adr.RouteValues.ElementAt(0).Value.ToString());
            // Assert.AreEqual("Ingredient was not found", rdr..Message);
            Assert.Fail();
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

        [TestCleanup]
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

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.UI.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using LambAndLentil.Domain.Entities;
using AutoMapper;
using LambAndLentil.Tests.Infrastructure;
using System.Linq;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI;
using LambAndLentil.Domain.Concrete;
using System.IO;

namespace LambAndLentil.Tests.Controllers
{

    [TestClass]
    [TestCategory("PersonsController")]
    public class PersonsControllerTest
    {
        private static IRepository<PersonVM> Repo { get; set; }
        public static MapperConfiguration AutoMapperConfig { get; set; }
        private static ListVM<PersonVM> listVM;
        private static PersonsController controller { get; set; }

        public PersonsControllerTest()
        {
            //AutoMapperConfig = AutoMapperConfigForTests.AMConfigForTests();
            //AutoMapperConfigForTests.InitializeMap();
            Repo = new TestRepository<PersonVM>();
            listVM = new ListVM<PersonVM>();
            controller = SetUpController();
        }

        private PersonsController SetUpController()
        {
            listVM.ListT = new List<PersonVM> {
                new PersonVM{ID = int.MaxValue, Name = "PersonsController_Index_Test P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new PersonVM{ID = int.MaxValue-1, Name = "PersonsController_Index_Test P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new PersonVM{ID = int.MaxValue-2, Name = "PersonsController_Index_Test P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new PersonVM{ID = int.MaxValue-3, Name = "PersonsController_Index_Test P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new PersonVM{ID = int.MaxValue-4, Name = "PersonsController_Index_Test P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            }.AsQueryable();

            foreach (PersonVM person in listVM.ListT)
            {
                Repo.Add(person);
            }

            controller = new PersonsController(Repo);
            controller.PageSize = 3;

            return controller;
        }

        [TestMethod]
        public void InheritsFromBaseControllerCorrectly()
        {


            // Arrange
            PersonsController controller = SetUpController();
            // Act 
            controller.PageSize = 4;

            var type = typeof(PersonsController);
            var DoesDisposeExist = type.GetMethod("Dispose");

            // Assert 
            Assert.AreEqual(4, controller.PageSize);
            Assert.IsNotNull(DoesDisposeExist);
        }

        [TestMethod]
        public void PersonsCtr_IsPublic()
        {
            // Arrange


            // Act
            Type type = controller.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void PersonsCtr_Index()
        {
            // Arrange
            PersonsController controller2 = new PersonsController(Repo);

            // Act
            ViewResult view1 = controller.Index(1) as ViewResult;
            ViewResult view2 = controller2.Index(2) as ViewResult;

            // Assert
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllPersons()
        {
            // Arrange 
            var controller2 = new PersonsController(Repo);

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<PersonVM>)(view1.Model)).ListT.Count();

            ViewResult view2 = controller2.Index(2);

            int count2 = ((ListVM<PersonVM>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
            Assert.AreEqual(5, count1);
            Assert.AreEqual(0, count2);
            Assert.AreEqual(5, count);
            Assert.AreEqual("Index", view1.ViewName);
            Assert.AreEqual("Index", view2.ViewName);

            Assert.AreEqual("PersonsController_Index_Test P1", ((ListVM<PersonVM>)(view1.Model)).ListT.FirstOrDefault().Name);
            Assert.AreEqual("PersonsController_Index_Test P2", ((ListVM<PersonVM>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual("PersonsController_Index_Test P3", ((ListVM<PersonVM>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);
            Assert.AreEqual("PersonsController_Index_Test P5", ((ListVM<PersonVM>)(view1.Model)).ListT.Skip(4).FirstOrDefault().Name);

        }


        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsCorrect()
        {
            // Arrange 
            controller.PageSize = 8;


            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<PersonVM>)(view1.Model)).ListT.Count();

            // Assert
            Assert.IsNotNull(view1);
            Assert.AreEqual(5, count1);
            Assert.AreEqual("Index", view1.ViewName);

            Assert.AreEqual("PersonsController_Index_Test P1", ((ListVM<PersonVM>)(view1.Model)).ListT.FirstOrDefault().Name);
            Assert.AreEqual("PersonsController_Index_Test P2", ((ListVM<PersonVM>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual("PersonsController_Index_Test P3", ((ListVM<PersonVM>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);


        }

        [Ignore] // currently we only have one page here
        [TestMethod]
        [TestCategory("Index")]
        public void SecondPageIsCorrect()
        {
            // Arrange

            //// Act

            //// Assert 
        }

        [TestMethod]
        [TestCategory("Index")]
        public void PersonsCtr_Index_CanSendPaginationViewModel()
        {
            // Arrange 

            // Act 
            ListVM<PersonVM> resultT = (ListVM<PersonVM>)((ViewResult)controller.Index(2)).Model;

            // Assert
            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(5, pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void PagingInfoIsCorrect()
        {
            // Arrange 

            // Action
            int totalItems = ((ListVM<PersonVM>)((ViewResult)controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = ((ListVM<PersonVM>)((ViewResult)controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = ((ListVM<PersonVM>)((ViewResult)controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = ((ListVM<PersonVM>)((ViewResult)controller.Index()).Model).PagingInfo.TotalPages;

            // Assert
            Assert.AreEqual(5, totalItems);
            Assert.AreEqual(1, currentPage);
            Assert.AreEqual(8, itemsPerPage);
            Assert.AreEqual(1, totalPages);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate()
        {
            // Arrange

            // Act
            var result = (ListVM<PersonVM>)(controller.Index(1)).Model;
            var listVM = result.ListT;

            // Assert 
            Assert.IsTrue(listVM.Count() == 5);
            Assert.AreEqual("PersonsController_Index_Test P1", listVM.FirstOrDefault().Name);
            Assert.AreEqual("PersonsController_Index_Test P3", listVM.Skip(2).FirstOrDefault().Name);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipeIDIsNegative()
        {
            // Arrange

            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Person was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Details")]
        public void WorksWithValidRecipeID()
        {
            // Arrange 

            // Act 
            ActionResult ar = controller.Details(int.MaxValue);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            // Assert
            Assert.IsNotNull(view);

            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(PersonVM));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipeIDTooHigh()
        {
            // Arrange

            // Act
            ActionResult view = controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Person was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipeIDPastIntLimit()
        {
            // Arrange

            // Act
            ViewResult result = controller.Details(Int16.MaxValue + 1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsRecipeIDIsZero()
        {
            // Arrange 

            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Person was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Create")]
        public void Create()
        {
            // Arrange

            // Act         
            ViewResult view = controller.Create(UIViewType.Edit);

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteAFoundPerson()
        {
            // Arrange 

            // Act  
            ActionResult ar = controller.Delete(int.MaxValue - 1);
            string viewName = ((ViewResult)ar).ViewName;

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual(UIViewType.Details.ToString(), viewName);
        }



        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteAnInvalidPerson()
        {
            // Arrange 
            var view = controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Person was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual(UIViewType.Index.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteConfirmed()
        {
            // Arrange
            PersonVM person = new PersonVM { ID = 1 };
            Repo.Add(person);
            // Act
            ActionResult result = controller.DeleteConfirmed(1) as ActionResult;
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { controller = "Persons", Action = "Index" } } );
            // Assert 
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void CanDeleteValidPerson()
        {

            // Arrange  
            int initialCount = Repo.Count();
            PersonVM pVM = new PersonVM("John", "Doe") { ID = 100, Description = "test CanDeleteValidPerson" };
            Repo.Add(pVM);
            int newCount = Repo.Count();

            // Act - delete the person
            ActionResult result = controller.DeleteConfirmed(pVM.ID);

            AlertDecoratorResult adr = (AlertDecoratorResult)result;

            // Assert
            Assert.AreEqual("John Doe has been deleted", adr.Message);
            Assert.AreEqual(initialCount, newCount - 1);
            Assert.AreEqual(initialCount, Repo.Count());
        }

        [Ignore]   // brought in Ingredient edit methods instead of using this
        [TestMethod]
        [TestCategory("Edit")]
        public void CanEditPerson()
        {
            // Arrange
            var controller2 = new PersonsController(Repo);
            PersonVM pVM = new PersonVM("Kermit", "Frog") { ID = 1492, Description = "test CanEditPerson" };

            // Act  
            ViewResult view1 = (ViewResult)controller.Edit(1492);
            PersonVM p1 = (PersonVM)view1.Model;
            ViewResult view2 = (ViewResult)controller2.Edit(2);
            PersonVM p2 = (PersonVM)view2.Model;
            


            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual(1, p1.ID);
            Assert.AreEqual(2, p2.ID);
            
            Assert.AreEqual("First edited", p1.Name);
            Assert.AreEqual("Old Name 2", p2.Name);
        }

        [Ignore]
        [TestMethod]
        public void CanEditPersonsName()
        {
            Assert.Fail();
        }

        [Ignore]   // look into why this is not working
        [TestMethod]
        [TestCategory("Edit")]
        public void CanEditPerson2()
        {
            // Arrange
            PersonVM menuVM = new PersonVM
            {
                ID = 1,
                Name = "test PersonControllerTest.CanEditPerson",
                Description = "test PersonControllerTest.CanEditPerson"
            };
            Repo.Save(menuVM);

            // Act 
            menuVM.Name = "Name has been changed";

            ViewResult view1 = (ViewResult)controller.Edit(1);

            var returnedPersonVM = (PersonVM)(view1.Model);


            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual("Name has been changed", returnedPersonVM.Name);
            //Assert.AreEqual(menuVM.Description, returnedPersonVm.Description);
            //Assert.AreEqual(menuVM.CreationDate, returnedPersonVm.CreationDate);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedPerson()
        {
            // Arrange
            PersonsController indexController = new PersonsController(Repo);
            PersonsController controller2 = new PersonsController(Repo);
            PersonsController controller3 = new PersonsController(Repo);


            PersonVM vm = new PersonVM();
            vm.Name = "0000 test";
            vm.ID = int.MaxValue - 100;
            vm.Description = "test PersonsControllerShould.SaveEditedPerson";

            // Act 
            ActionResult ar1 = controller.PostEdit(vm);


            // now edit it
            vm.ModifiedByUser = "0000 test Edited";
            vm.ID = 7777;
            ActionResult ar2 = controller2.PostEdit(vm);
            ViewResult view2 = controller3.Index();
            ListVM<PersonVM> listVM2 = (ListVM<PersonVM>)view2.Model;
            PersonVM vm3 = (from m in listVM2.ListT
                            where m.ID == 7777
                            select m).AsQueryable().FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited", vm3.ModifiedByUser);
            Assert.AreEqual(7777, vm3.ID);

        }

        [Ignore]  // look into why this is not working
        [TestMethod]
        [TestCategory("Edit")]
        public void CanPostEditPerson()
        {
            // Arrange
            PersonVM menuVM = new PersonVM
            {
                ID = 1,
                Name = "test PersonControllerTest.CanEditPerson",
                Description = "test PersonControllerTest.CanEditPerson"
            };
            Repo.Add(menuVM);

            // Act 
            menuVM.Name = "Name has been changed";

            ViewResult view1 = (ViewResult)controller.Edit(1);

            PersonVM returnedPersonVm = Repo.GetById(1);

            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual("Name has been changed", returnedPersonVm.Name);
            Assert.AreEqual(menuVM.Description, returnedPersonVm.Description);
            Assert.AreEqual(menuVM.CreationDate, returnedPersonVm.CreationDate);
        }


        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void CannotEditNonexistentPerson()
        {
            // Arrange

            // Act
            PersonVM result = (PersonVM)((ViewResult)controller.Edit(8)).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }



        [TestMethod]
        public void CreateReturnsNonNull()
        {
            // Arrange 

            // Act
            ViewResult result = controller.Create(UIViewType.Create) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


        [Ignore]
        [TestMethod]
        public void AssignAnIngredientToAPerson()
        {
            Assert.Fail();
        }


        [Ignore]
        [TestMethod]
        public void UnAssignAnIngredientFromAPerson()
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
        public void CorrectPersonsAreBoundInEdit()
        {
            Assert.Fail();
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            ClassCleanup();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Person\";

            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}

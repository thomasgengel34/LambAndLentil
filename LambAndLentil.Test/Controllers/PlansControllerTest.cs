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
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI;
using LambAndLentil.Domain.Concrete;
using System.IO; 

namespace LambAndLentil.Tests.Controllers
{
    [Ignore]
    [TestClass] 
    [TestCategory("PlansController")]
    public class PlansControllerTest
    {
         
        private static IRepository<PlanVM> repo { get; set; }
        public static MapperConfiguration AutoMapperConfig { get; set; }
        private static ListVM<PlanVM> listVM;
        private static PlansController controller { get; set; }

        public PlansControllerTest()
        { 
            AutoMapperConfigForTests.InitializeMap();
            repo = new TestRepository<PlanVM>();
            listVM = new ListVM<PlanVM>();
            controller = SetUpController();
        }

        private PlansController  SetUpController()
        {
            listVM.ListT = new List<PlanVM> {
                new PlanVM{ID = int.MaxValue, Name = "PlansController_Index_Test P1" ,AddedByUser="John Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue, ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new PlanVM{ID = int.MaxValue-1, Name = "PlansController_Index_Test P2",  AddedByUser="Sally Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(20), ModifiedDate=DateTime.MaxValue.AddYears(-20)},
                new PlanVM{ID = int.MaxValue-2, Name = "PlansController_Index_Test P3",  AddedByUser="Sue Doe", ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(30), ModifiedDate=DateTime.MaxValue.AddYears(-30)},
                new PlanVM{ID = int.MaxValue-3, Name = "PlansController_Index_Test P4",  AddedByUser="Kyle Doe" ,ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(40), ModifiedDate=DateTime.MaxValue.AddYears(-10)},
                new PlanVM{ID = int.MaxValue-4, Name = "PlansController_Index_Test P5",  AddedByUser="John Doe",  ModifiedByUser="Richard Roe", CreationDate=DateTime.MinValue.AddYears(50), ModifiedDate=DateTime.MaxValue.AddYears(-100)}
            }.AsQueryable();

            foreach (PlanVM plan in listVM.ListT)
            {
                repo.Add(plan);
            }

            controller = new PlansController(repo);
            controller.PageSize = 3;

            return controller;
        }

        [TestMethod]
        public void  IsPublic()
        {
            // Arrange 

            // Act
            Type type = controller.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        }




        [TestMethod]
        public void InheritsFromBaseControllerCorrectly()
        {

            // Arrange
           
            // Act 
            controller.PageSize = 4;

            var type = typeof(PlansController);
            var DoesDisposeExist = type.GetMethod("Dispose");

            // Assert 
            Assert.AreEqual(4, controller.PageSize);
            Assert.IsNotNull(DoesDisposeExist);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void  Index()
        {
            // Arrange
            

            // Act
            ViewResult result = controller.Index(1) as ViewResult;
             

            // Assert
            Assert.IsNotNull(result);
            
        }

        [TestMethod]
        [TestCategory("Index")]
        public void  ContainsAllListTVM()
        {
            // Arrange
           
            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<PlanVM>)(view1.Model)).ListT.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM<PlanVM>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
            Assert.AreEqual(5, count1);
            Assert.AreEqual(0, count2);
            Assert.AreEqual(5, count);
            Assert.AreEqual("Index", view1.ViewName);
            Assert.AreEqual("Index", view2.ViewName);

            //Assert.AreEqual("P1", ((ListVM<Plan,PlanVM>)(view1.Model)).ListTVM.FirstOrDefault().Name);
            //Assert.AreEqual("P2", ((ListVM<Plan,PlanVM>)(view1.Model)).ListTVM.Skip(1).FirstOrDefault().Name);
            //Assert.AreEqual("P3", ((ListVM<Plan,PlanVM>)(view1.Model)).ListTVM.Skip(2).FirstOrDefault().Name);
            //Assert.AreEqual("P5", ((ListVM<Plan,PlanVM>)(view2.Model)).ListTVM.FirstOrDefault().Name);

        }


        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsCorrect()
        {
            // Arrange
          
            ListVM<PlanVM> illistVM = new ListVM<PlanVM>(); 
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM<PlanVM>)(view1.Model)).ListT.Count();



            // Assert
            Assert.IsNotNull(view1);
            Assert.AreEqual(5, count1);
            Assert.AreEqual("Index", view1.ViewName);

            Assert.AreEqual("Old Name 1", ((ListVM<PlanVM>)(view1.Model)).ListT.FirstOrDefault().Name);
            Assert.AreEqual("Old Name 2", ((ListVM<PlanVM>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual("Old Name 3", ((ListVM<PlanVM>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);


        }

        [Ignore]
        [TestMethod]
        [TestCategory("Index")]
        // currently we only have one page here
        public void ListTVMCtr_Index_SecondPageIsCorrect()
        { 

        }

        [TestMethod]
        [TestCategory("Index")]
        public void  CanSendPaginationViewModel()
        {

            // Arrange
            

            // Act

            ListVM<PlanVM> resultT = (ListVM<PlanVM>)((ViewResult)controller.Index(2)).Model;


            // Assert

            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(5, pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void  PagingInfoIsCorrect()
        {
            // Arrange
           


            // Action
            int totalItems = ((ListVM<PlanVM>)((ViewResult)controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = ((ListVM<PlanVM>)((ViewResult)controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = ((ListVM<PlanVM>)((ViewResult)controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = ((ListVM<PlanVM>)((ViewResult)controller.Index()).Model).PagingInfo.TotalPages;



            // Assert
            Assert.AreEqual(5, totalItems);
            Assert.AreEqual(1, currentPage);
            Assert.AreEqual(8, itemsPerPage);
            Assert.AreEqual(1, totalPages);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void  CanPaginate()
        {
            // Arrange
            

            // Act
            var result = (ListVM<PlanVM>)(controller.Index(1)).Model;



            // Assert 
            Assert.IsTrue(result.ListT.Count() == 5);
            Assert.AreEqual("Old Name 1", result.ListT.First().Name);
            Assert.AreEqual("Old Name 4", result.ListT.Skip(2).First().Name);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipeIDIsNegative()
        {
            // Arrange
            
            // AutoMapperConfigForTests.AMConfigForTests();
            AutoMapperConfigForTests.InitializeMap();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No plan was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIControllerType.Plans.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  WorksWithValidRecipeID()
        {
            // Arrange 
            AutoMapperConfigForTests.InitializeMap();


            // Act
            ViewResult view = controller.Details(1) as ViewResult;
            // Assert
            Assert.IsNotNull(view);

            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(LambAndLentil.UI.Models.PlanVM));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  RecipeIDTooHigh()
        {
            // Arrange 
            AutoMapperConfigForTests.InitializeMap();
            ActionResult view = controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No plan was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIControllerType.Plans.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  RecipeIDPastIntLimit()
        {
            // Arrange
             
            AutoMapperConfigForTests.InitializeMap();


            // Act
            ViewResult result = controller.Details(Int16.MaxValue + 1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void  RecipeIDIsZero()
        {
            // Arrange 
            AutoMapperConfigForTests.InitializeMap();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No plan was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIControllerType.Plans.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Create")]
        public void Create()
        {
            // Arrange
            
            ViewResult view = controller.Create(UIViewType.Edit);


            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteAFoundPlan()
        {
            // Arrange
            

            // Act 
            var view = controller.Delete(1) as ViewResult;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual(UIViewType.Details.ToString(), view.ViewName);
        }



        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteAnInvalidPlan()
        {
            // Arrange
             

            // Act 
            var view = controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No plan was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIControllerType.Plans.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteConfirmed()
        {
            // Arrange
            
            // Act
            ActionResult result = controller.DeleteConfirmed(1) as ActionResult;
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { controller = "ListTVM", Action = "Index" } } );
            // Assert 
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void CanDeleteValidPlan()
        {
            // Arrange  
            PlanVM pVM = new PlanVM() { ID = 6000, Name = "test CanDeleteValidPlan" };
            int count = repo.Count();
            repo.Add(pVM);
            int countPlus = repo.Count();
            // Act - delete the plan
                ActionResult result = controller.DeleteConfirmed(pVM.ID);
            int countEnding = repo.Count();
            AlertDecoratorResult adr = (AlertDecoratorResult)result;

            // Assert 
            Assert.AreEqual("Test2 has been deleted", adr.Message);
            Assert.AreEqual(count, countEnding);
            Assert.AreEqual(count + 1, countPlus);
        }


        [TestMethod]
        [TestCategory("Edit")]
        public void CanEditPlan()
        {
            // Arrange
             

            // Act 

            ViewResult view1 = controller.Edit(1);
            PlanVM p1 = (PlanVM)view1.Model;
            ViewResult view2 = controller.Edit(2);
            PlanVM p2 = (PlanVM)view2.Model;
            ViewResult view3 = controller.Edit(3);
            PlanVM p3 = (PlanVM)view3.Model;


            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual(1, p1.ID);
            Assert.AreEqual(2, p2.ID);
            Assert.AreEqual(3, p3.ID);
            Assert.AreEqual("First edited", p1.Name);
            Assert.AreEqual("Old Name 2", p2.Name);
        }



        [TestMethod]
        [TestCategory("Edit")]
        public void CannotEditNonexistentPlan()
        {
            // Arrange
            
            // Act
            Plan result = (Plan)controller.Edit(8).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ListTVMCtr_CreateReturnsNonNull()
        {
            // Arrange
          

            // Act
            ViewResult result = controller.Create(UIViewType.Create) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
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
        [TestMethod]
        public void ComputesCorrectTotalCalories()
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
        public void CorrectPlanElementsAreBoundInEdit()
        {
            Assert.Fail();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            string path = @"C:\Dev\TGE\LambAndLentil\LambAndLentil.Test\App_Data\JSON\Plan\";

            IEnumerable<string> files = Directory.EnumerateFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }

        }
    }
}

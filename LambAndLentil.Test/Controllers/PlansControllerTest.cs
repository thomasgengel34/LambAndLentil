using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using LambAndLentil.Domain.Abstract;
using Moq;
using LambAndLentil.Domain.Entities;
using System.Linq;
 
using LambAndLentil.UI.Models;
using System.Web.Mvc;
using System.Collections.Generic; 
using AutoMapper;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI;

namespace LambAndLentil.Tests.Controllers
{
    [TestClass]
    [TestCategory("PlansController")]
    public class PlansControllerTest
    {
        static Mock<IRepository> mock;
        public static MapperConfiguration AutoMapperConfig { get; set; }

        public PlansControllerTest()
        {
            //AutoMapperConfig = AutoMapperConfigForTests.AMConfigForTests();
            AutoMapperConfigForTests.InitializeMap();

        }

        [TestMethod]
    public void PlansCtr_IsPublic()
    {
        // Arrange
        PlansController testController = SetUpSimpleController();

        // Act
        Type type = testController.GetType();
        bool isPublic = type.IsPublic;

        // Assert 
        Assert.AreEqual(isPublic, true);
    }



  
        [TestMethod]
        public void PlansCtr_InheritsFromBaseControllerCorrectly()
        {

            // Arrange
            PlansController controller = SetUpSimpleController();
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
        public void PlansCtr_Index()
        {
            // Arrange
            PlansController controller = SetUpController();

            // Act
            ViewResult result = controller.Index(1) as ViewResult;
            ViewResult result1 = controller.Index(2) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void PlansCtr_Index_ContainsAllPlans()
        {
            // Arrange
            PlansController controller = SetUpController();
            ListVM ilvm = new ListVM();
            ilvm.Plans = (IEnumerable<Plan>)mock.Object.Plans;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).Plans.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM)(view2.Model)).Plans.Count();

            int count = count1 + count2;

            // Assert
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
            Assert.AreEqual(5, count1);
            Assert.AreEqual(0, count2);
            Assert.AreEqual(5, count);
            Assert.AreEqual("Index", view1.ViewName);
            Assert.AreEqual("Index", view2.ViewName);

            //Assert.AreEqual("P1", ((ListVM)(view1.Model)).Plans.FirstOrDefault().Name);
            //Assert.AreEqual("P2", ((ListVM)(view1.Model)).Plans.Skip(1).FirstOrDefault().Name);
            //Assert.AreEqual("P3", ((ListVM)(view1.Model)).Plans.Skip(2).FirstOrDefault().Name);
            //Assert.AreEqual("P5", ((ListVM)(view2.Model)).Plans.FirstOrDefault().Name);

        }


        [TestMethod]
        [TestCategory("Index")]
        public void PlansCtr_Index_FirstPageIsCorrect()
        {
            // Arrange
            PlansController controller = SetUpController();
            ListVM ilvm = new ListVM();
            ilvm.Plans = (IEnumerable<Plan>)mock.Object.Plans;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).Plans.Count();



            // Assert
            Assert.IsNotNull(view1);
            Assert.AreEqual(5, count1);
            Assert.AreEqual("Index", view1.ViewName);

            Assert.AreEqual("Old Name 1", ((ListVM)(view1.Model)).Plans.FirstOrDefault().Name);
            Assert.AreEqual("Old Name 2", ((ListVM)(view1.Model)).Plans.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual("Old Name 3", ((ListVM)(view1.Model)).Plans.Skip(2).FirstOrDefault().Name);


        }


        [TestMethod]
        [TestCategory("Index")]
        // currently we only have one page here
        public void PlansCtr_Index_SecondPageIsCorrect()
        {
            //// Arrange
            //PlansController controller = SetUpController();
            //ListVM ilvm = new ListVM();
            //ilvm.Plans = (IEnumerable<Plan>)mock.Object.Plans;

            //// Act
            //ViewResult view  = controller.Index(null, null, null, 2);

            //int count  = ((ListVM)(view.Model)).Plans.Count(); 

            //// Assert
            //Assert.IsNotNull(view);
            //Assert.AreEqual(0, count );
            //Assert.AreEqual("Index", view.ViewName); 
            // Assert.AreEqual("P5", ((ListVM)(view.Model)).Plans.FirstOrDefault().Name);
            // Assert.AreEqual( 5, ((ListVM)(view.Model)).Plans.FirstOrDefault().ID);
            // Assert.AreEqual("C", ((ListVM)(view.Model)).Plans.FirstOrDefault().Maker);
            // Assert.AreEqual("CC", ((ListVM)(view.Model)).Plans.FirstOrDefault().Brand);

        }

        [TestMethod]
        [TestCategory("Index")]
        public void PlansCtr_Index_CanSendPaginationViewModel()
        {

            // Arrange
            PlansController controller = SetUpController();

            // Act

            ListVM resultT = (ListVM)((ViewResult)controller.Index(2)).Model;


            // Assert

            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(5, pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void PlansCtr_Index_PagingInfoIsCorrect()
        {
            // Arrange
            PlansController controller = SetUpController();


            // Action
            int totalItems = ((ListVM)((ViewResult)controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = ((ListVM)((ViewResult)controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = ((ListVM)((ViewResult)controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = ((ListVM)((ViewResult)controller.Index()).Model).PagingInfo.TotalPages;



            // Assert
            Assert.AreEqual(5, totalItems);
            Assert.AreEqual(1, currentPage);
            Assert.AreEqual(8, itemsPerPage);
            Assert.AreEqual(1, totalPages);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void PlansCtr_IndexCanPaginate()
        {
            // Arrange
            PlansController controller = SetUpController();

            // Act
            var result = (ListVM)(controller.Index(1)).Model;



            // Assert
            Plan[] ingrArray1 = result.Plans.ToArray();
            Assert.IsTrue(ingrArray1.Length == 5);
            Assert.AreEqual("Old Name 1", ingrArray1[0].Name);
            Assert.AreEqual("Old Name 4", ingrArray1[3].Name);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void PlansCtr_DetailsRecipeIDIsNegative()
        {
            // Arrange
            PlansController controller = SetUpController();
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
        public void PlansCtr_DetailsWorksWithValidRecipeID()
        {
            // Arrange
            PlansController controller = SetUpController();
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
        public void PlansCtr_DetailsRecipeIDTooHigh()
        {
            // Arrange
            PlansController controller = SetUpController();
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
        public void PlansCtr_DetailsRecipeIDPastIntLimit()
        {
            // Arrange
            PlansController controller = SetUpController();
            AutoMapperConfigForTests.InitializeMap();


            // Act
            ViewResult result = controller.Details(Int16.MaxValue + 1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void PlansCtr_DetailsRecipeIDIsZero()
        {
            // Arrange
            PlansController controller = SetUpController();
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
        public void PlansCtr_Create()
        {
            // Arrange
            PlansController controller = SetUpController();
            ViewResult view = controller.Create(UIViewType.Edit);


            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Edit", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void PlansCtr_DeleteAFoundPlan()
        {
            // Arrange
            PlansController controller = SetUpController();

            // Act 
            var view = controller.Delete(1) as ViewResult;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual(UIViewType.Details.ToString(), view.ViewName);
        }



        [TestMethod]
        [TestCategory("Delete")]
        public void PlansCtr_DeleteAnInvalidPlan()
        {
            // Arrange
            PlansController controller = SetUpController();

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
        public void PlansCtr_DeleteConfirmed()
        {
            // Arrange
            PlansController controller = SetUpController();
            // Act
            ActionResult result = controller.DeleteConfirmed(1) as ActionResult;
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { controller = "Plans", Action = "Index" } } );
            // Assert 
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void Plans_Ctr_CanDeleteValidPlan()
        {
            // Arrange - create an plan
            Plan plan = new Plan { ID = 2, Name = "Test2" };

            // Arrange - create the mock repository
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.Plans).Returns(new Plan[]
            {
                new Plan {ID=1,Name="Test1"},

                plan,

                new Plan {ID=3,Name="Test3"},
            }.AsQueryable());
            mock.Setup(m => m.Delete<Plan>(It.IsAny<int>())).Verifiable();
            // Arrange - create the controller
            PlansController controller = new PlansController(mock.Object);

            // Act - delete the plan
            ActionResult result = controller.DeleteConfirmed(plan.ID);

            AlertDecoratorResult adr = (AlertDecoratorResult)result;

            // Assert - ensure that the repository delete method was called with a correct Plan
            mock.Verify(m => m.Delete<Plan>(plan.ID));


            Assert.AreEqual("Test2 has been deleted", adr.Message);
        }


        [TestMethod]
        [TestCategory("Edit")]
        public void PlansCtr_CanEditPlan()
        {
            // Arrange
            PlansController controller = SetUpController();

            Plan plan = mock.Object.Plans.First();
            mock.Setup(c => c.Save(plan)).Verifiable();
            plan.Name = "First edited";

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
        public void PlansCtr_CanSaveEditedPlan()
        {
            // Arrange
            PlansController controller = SetUpController();

            Plan plan = mock.Object.Plans.First();

           // mock.Setup(c => c.foobar()).Verifiable();
            // mock.Setup(c => c.Save(It.IsAny<Plan>())).Verifiable();
            // mock.Setup(c => c.DeletePlan(1)).Verifiable();
            //  mock.Setup(c => c.foo(It.IsAny<Plan>())).Verifiable();
          //  mock.Setup(c => c.bar("hello")).Verifiable();

            // leave this failing until I can figure out how to get Moq to work. 

            // Act 
            AutoMapperConfigForTests.InitializeMap();
            PlanVM planVM = Mapper.Map<Plan, PlanVM>(plan);
            planVM.Name = "First edited";
            var view1 = controller.Edit(planVM);

            //  PlanVM p1 = (PlanVM)view1.Model;

            // Assert
          //  mock.Verify(x => x.foobar());
            // mock.Verify(x => x.bar("hello"));
            //      mock.Verify(x => x.foo(It.IsAny<Plan>()) , Times.Once);
            //   mock.Verify(x => x.DeletePlan(1) , Times.Once);
            mock.Verify(x => x.Save(It.IsAny<Plan>()), Times.Once);   //it is called when you run it normally. False negative.  Go around this by checking whether the actual value changed. 
            string name = mock.Object.Plans.First().Name;

            Assert.IsNotNull(view1);
            Assert.AreEqual("First edited", name);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void PlansCtr_CannotEditNonexistentPlan()
        {
            //    // Arrange
            //    PlansController controller = SetUpController();
            //    // Act
            //    Plan result = (Plan)controller.Edit(8).ViewData.Model;
            //    // Assert
            //    Assert.IsNull(result);
            //}

            //[TestMethod]
            //public void PlansCtr_CreateReturnsNonNull()
            //{
            //    // Arrange
            //    PlansController controller = SetUpController();


            //    // Act
            //    ViewResult result = controller.Create(null) as ViewResult;

            //    // Assert
            //    Assert.IsNotNull(result);
        }


        private PlansController SetUpController()
        {
            // - create the mock repository
            mock = new Mock<IRepository>();
            mock.Setup(m => m.Plans).Returns(new Plan[] {
                new Plan {ID = 1, Name = "Old Name 1" },
                new Plan {ID = 2, Name = "Old Name 2" },
                new Plan {ID = 3, Name = "Old Name 3" },
                new Plan {ID = 4, Name = "Old Name 4", },
                new Plan {ID = 5, Name = "Old Name 5" }
            }.AsQueryable());

            // Arrange - create a controller
            PlansController controller = new PlansController(mock.Object);
            controller.PageSize = 3;

            return controller;
        }



        private PlansController SetUpSimpleController()
        {
            // - create the mock repository
            Mock<IRepository> mock = new Mock<IRepository>();


            // Arrange - create a controller
            PlansController controller = new PlansController(mock.Object);
            // controller.PageSize = 3;

            return controller;
        } 
    }
}

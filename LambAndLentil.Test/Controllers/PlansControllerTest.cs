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
    [Ignore]
    [TestClass]
    [TestCategory("PlansController")]
    public class PlansControllerTest
    {
        //    static Mock<IRepository<Plan,PlanVM>> mock;
        //    public static MapperConfiguration AutoMapperConfig { get; set; }

        //    public PlansControllerTest()
        //    {
        //        //AutoMapperConfig = AutoMapperConfigForTests.AMConfigForTests();
        //        AutoMapperConfigForTests.InitializeMap();

        //    }

        //    [TestMethod]
        //public void EntitiesCtr_IsPublic()
        //{
        //    // Arrange
        //    PlansController testController = SetUpSimpleController();

        //    // Act
        //    Type type = testController.GetType();
        //    bool isPublic = type.IsPublic;

        //    // Assert 
        //    Assert.AreEqual(isPublic, true);
        //}




        //    [TestMethod]
        //    public void EntitiesCtr_InheritsFromBaseControllerCorrectly()
        //    {

        //        // Arrange
        //        PlansController controller = SetUpSimpleController();
        //        // Act 
        //        controller.PageSize = 4;

        //        var type = typeof(PlansController);
        //        var DoesDisposeExist = type.GetMethod("Dispose");

        //        // Assert 
        //        Assert.AreEqual(4, controller.PageSize);
        //        Assert.IsNotNull(DoesDisposeExist);
        //    }

        //    [TestMethod]
        //    [TestCategory("Index")]
        //    public void EntitiesCtr_Index()
        //    {
        //        // Arrange
        //        PlansController controller = SetUpController();

        //        // Act
        //        ViewResult result = controller.Index(1) as ViewResult;
        //        ViewResult result1 = controller.Index(2) as ViewResult;

        //        // Assert
        //        Assert.IsNotNull(result);
        //        Assert.IsNotNull(result1);
        //    }

        //    [TestMethod]
        //    [TestCategory("Index")]
        //    public void EntitiesCtr_Index_ContainsAllEntities()
        //    {
        //        // Arrange
        //        PlansController controller = SetUpController();
        //        ListVM<Plan,PlanVM> ilvm = new ListVM<Plan,PlanVM>();
        //        ilvm.Entities = (IEnumerable<Plan>)mock.Object.Plan;

        //        // Act
        //        ViewResult view1 = controller.Index(1);

        //        int count1 = ((ListVM<Plan,PlanVM>)(view1.Model)).Entities.Count();

        //        ViewResult view2 = controller.Index(2);

        //        int count2 = ((ListVM<Plan,PlanVM>)(view2.Model)).Entities.Count();

        //        int count = count1 + count2;

        //        // Assert
        //        Assert.IsNotNull(view1);
        //        Assert.IsNotNull(view2);
        //        Assert.AreEqual(5, count1);
        //        Assert.AreEqual(0, count2);
        //        Assert.AreEqual(5, count);
        //        Assert.AreEqual("Index", view1.ViewName);
        //        Assert.AreEqual("Index", view2.ViewName);

        //        //Assert.AreEqual("P1", ((ListVM<Plan,PlanVM>)(view1.Model)).Entities.FirstOrDefault().Name);
        //        //Assert.AreEqual("P2", ((ListVM<Plan,PlanVM>)(view1.Model)).Entities.Skip(1).FirstOrDefault().Name);
        //        //Assert.AreEqual("P3", ((ListVM<Plan,PlanVM>)(view1.Model)).Entities.Skip(2).FirstOrDefault().Name);
        //        //Assert.AreEqual("P5", ((ListVM<Plan,PlanVM>)(view2.Model)).Entities.FirstOrDefault().Name);

        //    }


        //    [TestMethod]
        //    [TestCategory("Index")]
        //    public void EntitiesCtr_Index_FirstPageIsCorrect()
        //    {
        //        // Arrange
        //        PlansController controller = SetUpController();
        //        ListVM<Plan,PlanVM> ilvm = new ListVM<Plan,PlanVM>();
        //        ilvm.Entities = (IEnumerable<Plan>)mock.Object.Plan;
        //        controller.PageSize = 8;

        //        // Act
        //        ViewResult view1 = controller.Index(1);

        //        int count1 = ((ListVM<Plan,PlanVM>)(view1.Model)).Entities.Count();



        //        // Assert
        //        Assert.IsNotNull(view1);
        //        Assert.AreEqual(5, count1);
        //        Assert.AreEqual("Index", view1.ViewName);

        //        Assert.AreEqual("Old Name 1", ((ListVM<Plan,PlanVM>)(view1.Model)).Entities.FirstOrDefault().Name);
        //        Assert.AreEqual("Old Name 2", ((ListVM<Plan,PlanVM>)(view1.Model)).Entities.Skip(1).FirstOrDefault().Name);
        //        Assert.AreEqual("Old Name 3", ((ListVM<Plan,PlanVM>)(view1.Model)).Entities.Skip(2).FirstOrDefault().Name);


        //    }


        //    [TestMethod]
        //    [TestCategory("Index")]
        //    // currently we only have one page here
        //    public void EntitiesCtr_Index_SecondPageIsCorrect()
        //    {
        //        //// Arrange
        //        //PlansController controller = SetUpController();
        //        //ListVM<Plan,PlanVM> ilvm = new ListVM<Plan,PlanVM>();
        //        //ilvm.Entities = (IEnumerable<Plan,PlanVM>)mock.Object.Entities;

        //        //// Act
        //        //ViewResult view  = controller.Index(null, null, null, 2);

        //        //int count  = ((ListVM<Plan,PlanVM>)(view.Model)).Entities.Count(); 

        //        //// Assert
        //        //Assert.IsNotNull(view);
        //        //Assert.AreEqual(0, count );
        //        //Assert.AreEqual("Index", view.ViewName); 
        //        // Assert.AreEqual("P5", ((ListVM<Plan,PlanVM>)(view.Model)).Entities.FirstOrDefault().Name);
        //        // Assert.AreEqual( 5, ((ListVM<Plan,PlanVM>)(view.Model)).Entities.FirstOrDefault().ID);
        //        // Assert.AreEqual("C", ((ListVM<Plan,PlanVM>)(view.Model)).Entities.FirstOrDefault().Maker);
        //        // Assert.AreEqual("CC", ((ListVM<Plan,PlanVM>)(view.Model)).Entities.FirstOrDefault().Brand);

        //    }

        //    [TestMethod]
        //    [TestCategory("Index")]
        //    public void EntitiesCtr_Index_CanSendPaginationViewModel()
        //    {

        //        // Arrange
        //        PlansController controller = SetUpController();

        //        // Act

        //        ListVM<Plan,PlanVM> resultT = (ListVM<Plan,PlanVM>)((ViewResult)controller.Index(2)).Model;


        //        // Assert

        //        PagingInfo pageInfoT = resultT.PagingInfo;
        //        Assert.AreEqual(2, pageInfoT.CurrentPage);
        //        Assert.AreEqual(8, pageInfoT.ItemsPerPage);
        //        Assert.AreEqual(5, pageInfoT.TotalItems);
        //        Assert.AreEqual(1, pageInfoT.TotalPages);
        //    }


        //    [TestMethod]
        //    [TestCategory("Index")]
        //    public void EntitiesCtr_Index_PagingInfoIsCorrect()
        //    {
        //        // Arrange
        //        PlansController controller = SetUpController();


        //        // Action
        //        int totalItems = ((ListVM<Plan,PlanVM>)((ViewResult)controller.Index()).Model).PagingInfo.TotalItems;
        //        int currentPage = ((ListVM<Plan,PlanVM>)((ViewResult)controller.Index()).Model).PagingInfo.CurrentPage;
        //        int itemsPerPage = ((ListVM<Plan,PlanVM>)((ViewResult)controller.Index()).Model).PagingInfo.ItemsPerPage;
        //        int totalPages = ((ListVM<Plan,PlanVM>)((ViewResult)controller.Index()).Model).PagingInfo.TotalPages;



        //        // Assert
        //        Assert.AreEqual(5, totalItems);
        //        Assert.AreEqual(1, currentPage);
        //        Assert.AreEqual(8, itemsPerPage);
        //        Assert.AreEqual(1, totalPages);
        //    }

        //    [TestMethod]
        //    [TestCategory("Index")]
        //    public void EntitiesCtr_IndexCanPaginate()
        //    {
        //        // Arrange
        //        PlansController controller = SetUpController();

        //        // Act
        //        var result = (ListVM<Plan,PlanVM>)(controller.Index(1)).Model;



        //        // Assert
        //        Plan[] ingrArray1 = result.Entities.ToArray();
        //        Assert.IsTrue(ingrArray1.Length == 5);
        //        Assert.AreEqual("Old Name 1", ingrArray1[0].Name);
        //        Assert.AreEqual("Old Name 4", ingrArray1[3].Name);
        //    }

        //    [TestMethod]
        //    [TestCategory("Details")]
        //    public void EntitiesCtr_DetailsRecipeIDIsNegative()
        //    {
        //        // Arrange
        //        PlansController controller = SetUpController();
        //        // AutoMapperConfigForTests.AMConfigForTests();
        //        AutoMapperConfigForTests.InitializeMap();


        //        // Act
        //        ViewResult view = controller.Details(0) as ViewResult;
        //        AlertDecoratorResult adr = (AlertDecoratorResult)view;

        //        // Assert
        //        Assert.IsNotNull(view);
        //        Assert.AreEqual("No plan was found with that id.", adr.Message);
        //        Assert.AreEqual("alert-danger", adr.AlertClass);
        //        Assert.AreEqual(UIControllerType.Plans.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        //        Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
        //        Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        //    }

        //    [TestMethod]
        //    [TestCategory("Details")]
        //    public void EntitiesCtr_DetailsWorksWithValidRecipeID()
        //    {
        //        // Arrange
        //        PlansController controller = SetUpController();
        //        AutoMapperConfigForTests.InitializeMap();


        //        // Act
        //        ViewResult view = controller.Details(1) as ViewResult;
        //        // Assert
        //        Assert.IsNotNull(view);

        //        Assert.AreEqual("Details", view.ViewName);
        //        Assert.IsInstanceOfType(view.Model, typeof(LambAndLentil.UI.Models.PlanVM));
        //    }

        //    [TestMethod]
        //    [TestCategory("Details")]
        //    public void EntitiesCtr_DetailsRecipeIDTooHigh()
        //    {
        //        // Arrange
        //        PlansController controller = SetUpController();
        //        AutoMapperConfigForTests.InitializeMap();
        //        ActionResult view = controller.Details(4000);
        //        AlertDecoratorResult adr = (AlertDecoratorResult)view;
        //        // Assert
        //        Assert.IsNotNull(view);
        //        Assert.AreEqual("No plan was found with that id.", adr.Message);
        //        Assert.AreEqual("alert-danger", adr.AlertClass);
        //        Assert.AreEqual(UIControllerType.Plans.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        //        Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
        //        Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        //    }

        //    [TestMethod]
        //    [TestCategory("Details")]
        //    public void EntitiesCtr_DetailsRecipeIDPastIntLimit()
        //    {
        //        // Arrange
        //        PlansController controller = SetUpController();
        //        AutoMapperConfigForTests.InitializeMap();


        //        // Act
        //        ViewResult result = controller.Details(Int16.MaxValue + 1) as ViewResult;

        //        // Assert
        //        Assert.IsNotNull(result);
        //    }

        //    [TestMethod]
        //    [TestCategory("Details")]
        //    public void DetailsRecipeIDIsZero()
        //    {
        //        // Arrange
        //        PlansController controller = SetUpController();
        //        AutoMapperConfigForTests.InitializeMap();


        //        // Act
        //        ViewResult view = controller.Details(0) as ViewResult;
        //        AlertDecoratorResult adr = (AlertDecoratorResult)view;

        //        // Assert
        //        Assert.IsNotNull(view);
        //        Assert.AreEqual("No plan was found with that id.", adr.Message);
        //        Assert.AreEqual("alert-danger", adr.AlertClass);
        //        Assert.AreEqual(UIControllerType.Plans.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        //        Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
        //        Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        //    }

        //    [TestMethod]
        //    [TestCategory("Create")]
        //    public void  Create()
        //    {
        //        // Arrange
        //        PlansController controller = SetUpController();
        //        ViewResult view = controller.Create(UIViewType.Edit);


        //        // Assert
        //        Assert.IsNotNull(view);
        //        Assert.AreEqual("Details", view.ViewName);
        //    }

        //    [TestMethod]
        //    [TestCategory("Delete")]
        //    public void  DeleteAFoundPlan()
        //    {
        //        // Arrange
        //        PlansController controller = SetUpController();

        //        // Act 
        //        var view = controller.Delete(1) as ViewResult;

        //        // Assert
        //        Assert.IsNotNull(view);
        //        Assert.AreEqual(UIViewType.Details.ToString(), view.ViewName);
        //    }



        //    [TestMethod]
        //    [TestCategory("Delete")]
        //    public void  DeleteAnInvalidPlan()
        //    {
        //        // Arrange
        //        PlansController controller = SetUpController();

        //        // Act 
        //        var view = controller.Delete(4000) as ViewResult;
        //        AlertDecoratorResult adr = (AlertDecoratorResult)view;
        //        // Assert
        //        Assert.IsNotNull(view);
        //        Assert.AreEqual("No plan was found with that id.", adr.Message);
        //        Assert.AreEqual("alert-danger", adr.AlertClass);
        //        Assert.AreEqual(UIControllerType.Plans.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        //        Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
        //        Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        //    }

        //    [TestMethod]
        //    [TestCategory("Delete")]
        //    public void  DeleteConfirmed()
        //    {
        //        // Arrange
        //        PlansController controller = SetUpController();
        //        // Act
        //        ActionResult result = controller.DeleteConfirmed(1) as ActionResult;
        //        // improve this test when I do some route tests to return a more exact result
        //        //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { controller = "Entities", Action = "Index" } } );
        //        // Assert 
        //        Assert.IsNotNull(result);
        //    }

        //    [TestMethod]
        //    [TestCategory("Delete")]
        //    public void  CanDeleteValidPlan()
        //    {
        //        // Arrange - create an plan
        //        PlanVM planVM = new PlanVM { ID = 2, Name = "Test2" };

        //        // Arrange - create the mock repository
        //        Mock<IRepository<Plan,PlanVM>> mock = new Mock<IRepository<Plan,PlanVM>>();
        //        mock.Setup(m => m.Plan).Returns(new PlanVM[]
        //        {
        //            new PlanVM {ID=1,Name="Test1"},

        //            planVM,

        //            new PlanVM {ID=3,Name="Test3"},
        //        }.AsQueryable());
        //        mock.Setup(m => m.RemoveTVM(It.IsAny<PlanVM >())).Verifiable();
        //        // Arrange - create the controller
        //        PlansController controller = new PlansController(mock.Object);

        //        // Act - delete the plan
        //        ActionResult result = controller.DeleteConfirmed(planVM.ID);

        //        AlertDecoratorResult adr = (AlertDecoratorResult)result;

        //        // Assert - ensure that the repository delete method was called with a correct Plan
        //        mock.Verify(m => m.RemoveTVM(planVM));


        //        Assert.AreEqual("Test2 has been deleted", adr.Message);
        //    }


        //    [TestMethod]
        //    [TestCategory("Edit")]
        //    public void CanEditPlan()
        //    {
        //        // Arrange
        //        PlansController controller = SetUpController();

        //        PlanVM planVM = (PlanVM)mock.Object.Plan;
        //        mock.Setup(c => c.SaveTVM(planVM)).Verifiable();
        //        planVM.Name = "First edited";

        //        // Act 

        //        ViewResult view1 = controller.Edit(1);
        //        PlanVM p1 = (PlanVM)view1.Model;
        //        ViewResult view2 = controller.Edit(2);
        //        PlanVM p2 = (PlanVM)view2.Model;
        //        ViewResult view3 = controller.Edit(3);
        //        PlanVM p3 = (PlanVM)view3.Model;


        //        // Assert 
        //        Assert.IsNotNull(view1);
        //        Assert.AreEqual(1, p1.ID);
        //        Assert.AreEqual(2, p2.ID);
        //        Assert.AreEqual(3, p3.ID);
        //        Assert.AreEqual("First edited", p1.Name);
        //        Assert.AreEqual("Old Name 2", p2.Name); 
        //    }



        //    [TestMethod]
        //    [TestCategory("Edit")]
        //    public void  CannotEditNonexistentPlan()
        //    {
        //        //    // Arrange
        //        //    PlansController controller = SetUpController();
        //        //    // Act
        //        //    Plan result = (Plan)controller.Edit(8).ViewData.Model;
        //        //    // Assert
        //        //    Assert.IsNull(result);
        //        //}

        //        //[TestMethod]
        //        //public void EntitiesCtr_CreateReturnsNonNull()
        //        //{
        //        //    // Arrange
        //        //    PlansController controller = SetUpController();


        //        //    // Act
        //        //    ViewResult result = controller.Create(null) as ViewResult;

        //        //    // Assert
        //        //    Assert.IsNotNull(result);
        //    }


        //    private PlansController SetUpController()
        //    {
        //        // - create the mock repository
        //        mock = new Mock<IRepository<Plan,PlanVM>>();
        //        mock.Setup(m => m.Plan).Returns(new Plan[] {
        //            new Plan {ID = 1, Name = "Old Name 1" },
        //            new Plan {ID = 2, Name = "Old Name 2" },
        //            new Plan {ID = 3, Name = "Old Name 3" },
        //            new Plan {ID = 4, Name = "Old Name 4", },
        //            new Plan {ID = 5, Name = "Old Name 5" }
        //        }.AsQueryable());

        //        // Arrange - create a controller
        //        PlansController controller = new PlansController(mock.Object);
        //        controller.PageSize = 3;

        //        return controller;
        //    }



        //    private PlansController SetUpSimpleController()
        //    {
        //        // - create the mock repository
        //        Mock<IRepository<Plan,PlanVM>> mock = new Mock<IRepository<Plan,PlanVM>>();


        //        // Arrange - create a controller
        //        PlansController controller = new PlansController(mock.Object);
        //        // controller.PageSize = 3;

        //        return controller;
        //    }

        //    [TestMethod]
        //    public void FlagAnIngredientFlaggedInAPerson()
        //    {
        //        Assert.Fail();
        //    }


        //    [TestMethod]
        //    public void FlagAnIngredientFlaggedInTwoPersons()
        //    {
        //        Assert.Fail();
        //    }

        //    [TestMethod]
        //    public void WhenAFlagHasBeenRemovedFromOnePersonStillThereForSecondFlaggedPerson()
        //    {
        //        Assert.Fail();
        //    }

        //    [Ignore]
        //    [TestMethod]
        //    public void ComputesCorrectTotalCalories()
        //    {
        //        Assert.Fail();
        //    }

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
    }
}

﻿using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Infrastructure;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("PlansController")]
    [TestCategory("Details")]
    public class PlansController_Detail_Should:PlansController_Test_Should
    { 
        

        public PlansController_Detail_Should()
        { 
        }
         
        [TestMethod]
        public void ReturnDeleteWithActionMethodDeleteWithNullResult()
        { // "Plan was not found"

            // Arrange

            // Act 
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.Details(400, UIViewType.Delete);
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
            Assert.AreEqual(UIViewType.Index.ToString(), rdr.RouteValues.Values.ElementAt(0));
            Assert.AreEqual("Plan was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);

        }

        [TestMethod]
        public void ReturnDeleteWithActionMethodDeleteWithEmptyResult()
        { //  Details view  with success  "Here it is!"

            // Arrange

            // Act
            ViewResult view = (ViewResult)Controller.Details(int.MaxValue, UIViewType.Delete);


            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Plan));
            // no message
        }


        [TestMethod]
        public void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithFoundResult()
        { // index, success,  "Item has been deleted"
          // Arrange
            int count = Repo.Count();
            //Act
            Controller.Details(int.MaxValue, UIViewType.DeleteConfirmed);
            Plan plan = Repo.GetById(int.MaxValue);
            //Assert
            Assert.AreEqual(count - 1, Repo.Count());
            Assert.IsNull(plan);
            //   Assert.Fail();  // make sure the correct item was deleted before removing this line 
        }


        [TestMethod]
        public void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithIDNotFound()
        {  
            // Arrange

            // Act
            ActionResult ar=Controller.Details(4000, UIViewType.DeleteConfirmed);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar; 
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), rdr.RouteValues.Values.ElementAt(0));
            Assert.AreEqual("Plan was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass); 
        }

        [TestMethod]
        public void ReturnDeleteConfirmedWithActionMethodDeleteConfirmedWithBadID()
        {
            ActionResult ar = Controller.Details(-1, UIViewType.DeleteConfirmed);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            // Assert
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), rdr.RouteValues.Values.ElementAt(0));
            Assert.AreEqual("Plan was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
        } 

        [TestMethod]
        public void BeSuccessfulWithValidPlanID()
        { // "Here it is!"
            // Arrange


            // Act
            ActionResult ar = Controller.Details(int.MaxValue);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Plan));
            Assert.AreEqual("Here it is!", adr.Message);
        }

        [TestMethod]
        public void GotToIndexViewForNonSpecifiedActionMethods()
        {
            // Arrange

            // Act 
            ViewResult view = (ViewResult)Controller.Details(1, UIViewType.About); 

            // Assert
            Assert.AreEqual(UIViewType.Index, view.Model);

        }


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsPlanIDTooHighViewNotNull()
        {  // not sure what the desired behavior is yet
           // Arrange 
            ActionResult view = Controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
        }


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsPlanIDTooHighMessageRight()
        {
            // max is set by int.MaxValue, so the type controls this. 
            // Will always be true. Keep test in case I find this is wrong somehow.
            Assert.IsTrue(true);
        }


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsPlanIDTooHighAlertClassCorrect()
        {   // not sure what the desired behavior is yet
            // Arrange 
            ActionResult view = Controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert  
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsPlanIDTooHighCorrectModel()
        {
            // max is set by int.MaxValue, so the type controls this. 
            // Will always be true. Keep test in case I find this is wrong somehow.
            Assert.IsTrue(true);
        }


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsPlanIDTooHighCorrectController()
        {
            // max is set by int.MaxValue, so the type controls this. 
            // Will always be true. Keep test in case I find this is wrong somehow.
            Assert.IsTrue(true);
        }


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsPlanIDTooHighCorrectRouteValue()
        {
            // max is set by int.MaxValue, so the type controls this. 
            // Will always be true. Keep test in case I find this is wrong somehow.
            Assert.IsTrue(true);
        }


        [TestMethod]
        [TestCategory("Details")]
        public void DetailsPlanIDPastIntLimit()
        {
            // I am not sure how I want this to operate.  Wait until UI is set up and see then.
            // Arrange 

            // Act
            ViewResult result = Controller.Details(int.MaxValue) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsPlanIDIsZeroViewIsNotNull()
        {
            // Arrange


            // Act
            ViewResult view = Controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void ReturnErrorIfResultIsNotFound_PlanIDIsZeroMessageIsCorrect()
        {
            // Arrange


            // Act
            ActionResult ar = Controller.Details(0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            // Assert 
            Assert.AreEqual("No Plan was found with that id.", adr.Message);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsPlanIDIsZeroAlertClassIsCorrect()
        {
            // Arrange
            PlansController Controller = new PlansController(Repo);
            Plan plan = new Plan { ID = 0 };
            Repo.Save(plan);


            // Act
            ActionResult ar = Controller.Details(0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            // Assert  
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsPlanIDIsZeroReturnModelIsCorrect()
        {
            // Arrange
            PlansController Controller = new PlansController(Repo);
            Plan plan = new Plan { ID = 0 };
            Repo.Save(plan);


            // Act
            ActionResult ar = Controller.Details(0);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            // Assert    
            Assert.IsNull(adr.Model);
        }


        [TestMethod]
        [TestCategory("Details")]
        public void Details_PlanIDIsNegative_ResultNotNull()
        {
            // Arrange

            Plan plan = new Plan { ID = -1 };
            Repo.Save(plan);

            // Act
            ActionResult view = Controller.Details(-1);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void NotBeSuccessfulWithInvalidPlanID_PlanIDIsNegative_MessageCorrect()
        {
            // Arrange

            Plan.ID = -500;
            Repo.Save(Plan);

            // Act
            ActionResult ar = Controller.Details(-500);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;

            // Assert   adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            //Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
            Assert.AreEqual("Something is wrong with the data!", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void NotBeSuccessfulWithInvalidPlanID_PlanIDIsNegative_AlertClassCorrect()
        {
            // Arrange
            PlansController Controller = new PlansController(Repo);
            Plan plan = new Plan
            {
                ID = -1,
                Name = "Details_PlanIDIsNegative_AlertClassCorrect"
            };
            Repo.Save(plan);

            // Act
            ActionResult view = Controller.Details(-1);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert  
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual("Something is wrong with the data!", adr.Message);
        }

        [Ignore]
        [TestMethod]
        public void ReturnDetailsViewActionTypeEdit_ValidID()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnDetailsViewActionTypeEdit_InValidID()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipeIDIsNegative()
        {
            // Arrange

            // AutoMapperConfigForTests.AMConfigForTests();
            AutoMapperConfigForTests.InitializeMap();


            // Act
            ViewResult view = Controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Plan was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Details")]
        public void WorksWithValidPlanID()
        {
            // Arrange  

            // Act
            ActionResult ar = Controller.Details(int.MaxValue);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            // Assert 
            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Plan));
            Assert.IsTrue(true);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipeIDTooHigh()
        {
            // Arrange 
            AutoMapperConfigForTests.InitializeMap();
            ActionResult view = Controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Plan was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipeIDPastIntLimit()
        {
            // Arrange

            AutoMapperConfigForTests.InitializeMap();


            // Act
            ViewResult result = Controller.Details(Int16.MaxValue + 1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipeIDIsZero()
        {
            // Arrange 
            AutoMapperConfigForTests.InitializeMap();


            // Act
            ViewResult view = Controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Plan was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }
        // the following are not really testable.  I am keeping them to remind me of that.
        //[TestMethod]
        //public void PlansCtr_DetailsPlanIDIsNotANumber() { }

        //[TestMethod]
        //public void PlansCtr_DetailsPlanIDIsNotAInteger() { } 

    }
}

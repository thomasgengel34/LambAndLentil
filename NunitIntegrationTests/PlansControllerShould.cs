using LambAndLentil.Domain.Concrete;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Web.Mvc;
using System;
using System.Linq;

using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using System.Collections.Generic;

using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using LambAndLentil.UI.Infrastructure.Alerts;

namespace NunitIntegrationTests
{

    [TestFixture]
    [TestCategory("Integration")]
    public class PlansControllerShould
    {
        [Test]
        public void CreateAnPlan()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            PlansController controller = new PlansController(repo);
            // Act
            ViewResult vr = controller.Create(LambAndLentil.UI.UIViewType.Create);
            PlanVM vm = (PlanVM)vr.Model;
            string modelName = vm.Name;

            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created"); 
        }

        [Test]
        public void SaveAValidPlan()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            PlansController controller = new PlansController(repo);
            PlanVM vm = new PlanVM();
            vm.Name = "test";
            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit(vm);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;


            // Assert 
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(4, routeValues.Count);
            Assert.AreEqual(UIControllerType.Plans.ToString(), routeValues.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(1).ToString());
            Assert.AreEqual("Plans", routeValues.ElementAt(2).ToString());
            Assert.AreEqual(1.ToString(), routeValues.ElementAt(3).ToString());

            // Clean Up - should run a  delete test to make sure this works 
            List<Plan> plans = repo.Plans.ToList<Plan>();
            Plan plan = plans.Where(m => m.Name == "test").FirstOrDefault();

            // Delete it
            controller.DeleteConfirmed(plan.ID);
        }

        [Test]
        [TestCategory("Edit")]
        public void SaveEditedPlanWithNameChange()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            PlansController controller1 = new PlansController(repo);
            PlansController controller2 = new PlansController(repo);
            PlansController controller3 = new PlansController(repo);
            PlansController controller4 = new PlansController(repo);
            PlansController controller5 = new PlansController(repo);
            PlanVM vm = new PlanVM();
            vm.Name = "0000 test";

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM listVM = (ListVM)view1.Model;
            var result = (from m in listVM.Plans
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            Plan ingredient = result.FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("0000 test", ingredient.Name);

            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = ingredient.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM listVM2 = (ListVM)view2.Model;
            var result2 = (from m in listVM2.Plans
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable();

            ingredient = result2.FirstOrDefault();


            // Assert
            Assert.AreEqual("0000 test Edited", ingredient.Name);

            // clean up
            // TO DO: write a test to make sure this happens.
            controller5.DeleteConfirmed(vm.ID);
        }


         

        [Test]
        [TestCategory("Edit")]
        public void SaveEditedPlanWithDescriptionChange()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            PlansController controller1 = new PlansController(repo);
            PlansController controller2 = new PlansController(repo);
            PlansController controller3 = new PlansController(repo);
            PlansController controller4 = new PlansController(repo);
            PlansController controller5 = new PlansController(repo);
            PlanVM vm = new PlanVM();
            vm.Name = "0000 test";
            vm.Description = "SaveEditedPlanWithDescriptionChange Pre-test";


            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM listVM = (ListVM)view1.Model;
            var result = (from m in listVM.Plans
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            Plan plan = result.FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("SaveEditedPlanWithDescriptionChange Pre-test", plan.Description);

            // now edit it
            vm.ID = plan.ID;
            vm.Name = "0000 test Edited";
            vm.Description = "SaveEditedPlanWithDescriptionChange Post-test";

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM listVM2 = (ListVM)view2.Model;
            var result2 = (from m in listVM2.Plans
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable();

            plan = result2.FirstOrDefault();


            // Assert
            Assert.AreEqual("0000 test Edited", plan.Name);
            Assert.AreEqual("SaveEditedPlanWithDescriptionChange Post-test", plan.Description);

            // clean up 
            controller5.DeleteConfirmed(vm.ID);
        }


        [Test]
        [TestCategory("DeleteConfirmed")]
        public void ActuallyDeleteAPlanFromTheDatabase()
        {
            // Arrange
            EFRepository repo = new EFRepository(); ;
            PlansController editController = new PlansController(repo);
            PlansController indexController = new PlansController(repo);
            PlansController deleteController = new PlansController(repo);
            PlanVM vm = new PlanVM();
            vm.Name = "0000" + new Guid().ToString();
            ActionResult ar = editController.PostEdit(vm);
            ViewResult view = indexController.Index();
            ListVM listVM = (ListVM)view.Model;
            var result = (from m in listVM.Plans
                          where m.Name == vm.Name
                          select m).AsQueryable();
            Plan item = result.FirstOrDefault();

            //Act
            deleteController.DeleteConfirmed(item.ID);
            var deletedItem = (from m in repo.Plans
                               where m.Name == vm.Name
                               select m).AsQueryable();

            //Assert
            Assert.AreEqual(0, deletedItem.Count());
        }
        [Test]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);
            PlanVM planVM = new PlanVM(CreationDate);
            planVM.Name = "001 Test ";

            EFRepository repo = new EFRepository(); ;
            PlansController controllerEdit = new PlansController(repo);
            PlansController controllerView = new PlansController(repo);
            PlansController controllerDelete = new PlansController(repo);

            // Act
            controllerEdit.PostEdit(planVM);
            ViewResult view = controllerView.Index();
            ListVM listVM = (ListVM)view.Model;
            var result = (from m in listVM.Plans
                          where m.Name == "001 Test "
                          select m).AsQueryable();

            Plan plan = result.FirstOrDefault();

            DateTime shouldBeSameDate = plan.CreationDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);

            // Cleanup
            controllerDelete.DeleteConfirmed(plan.ID);
        }
    }
} 

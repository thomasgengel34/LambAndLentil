using AutoMapper;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MsTestIntegrationTests
{

    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("PlansController")]
    public class PlansControllerShould
    {
        [TestMethod]
        public void CreateAnPlan()
        {
            // Arrange
             EFRepository<Plan,PlanVM> repo = new  EFRepository<Plan,PlanVM>(); ;
            PlansController controller = new PlansController();
            // Act
            ViewResult vr = controller.Create(UIViewType.Create);
            PlanVM vm = (PlanVM)vr.Model;
            string modelName = vm.Name;

            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created");
        }

        [TestMethod]
        public void SaveAValidPlan()
        {
            // Arrange
             EFRepository<Plan,PlanVM> repo = new  EFRepository<Plan,PlanVM>(); ;
            PlansController controller = new PlansController();
            PlanVM vm = new PlanVM();
            vm.Name = "test";
            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit(vm);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;

            try
            {
                // Assert 
                Assert.AreEqual("alert-success", adr.AlertClass);
                Assert.AreEqual(4, routeValues.Count);
                Assert.AreEqual(UIControllerType.Plans.ToString(), routeValues.ElementAt(0).ToString());
                Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(1).ToString());
                Assert.AreEqual("Plans", routeValues.ElementAt(2).ToString());
                Assert.AreEqual(1.ToString(), routeValues.ElementAt(3).ToString());
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                // Clean Up - should run a  delete test to make sure this works  
                Plan plan = repo.GetAll().Where(m => m.Name == "test").FirstOrDefault();
                controller.DeleteConfirmed(plan.ID);
            }
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedPlanWithNameChange()
        {
            // Arrange
             EFRepository<Plan,PlanVM> repo = new  EFRepository<Plan,PlanVM>(); ;
            PlansController controller1 = new PlansController();
            PlansController controller2 = new PlansController();
            PlansController controller3 = new PlansController();
            PlansController controller4 = new PlansController();
            PlansController controller5 = new PlansController();
            PlanVM vm = new PlanVM();
            vm.Name = "0000 test";

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM<Plan,PlanVM> listVM = (ListVM<Plan, PlanVM>)view1.Model;
            var result = (from m in listVM.Entities
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            Plan plan = result.FirstOrDefault(); 
            PlanVM item = Mapper.Map<Plan, PlanVM>(plan);
            try
            {
                // verify initial value:
                Assert.AreEqual("0000 test", item.Name);
            }
            catch (Exception)
            {
                controller5.DeleteConfirmed(vm.ID);
                throw;
            }
            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = item.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM<Plan, PlanVM> listVM2 = (ListVM<Plan, PlanVM>)view2.Model;
            var result2 = (from m in listVM2.Entities
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable();
 
            Plan plan2 = result2.FirstOrDefault();
            PlanVM item2 = Mapper.Map<Plan, PlanVM>(plan2);
            try
            {
                // Assert
                Assert.AreEqual("0000 test Edited", item2.Name);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                // clean up 
                controller5.DeleteConfirmed(item2.ID);
            }
        }




        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedPlanWithDescriptionChange()
        {
            // Arrange
             EFRepository<Plan,PlanVM> repo = new  EFRepository<Plan,PlanVM>(); ;
            PlansController controller1 = new PlansController();
            PlansController controller2 = new PlansController();
            PlansController controller3 = new PlansController();
            PlansController controller4 = new PlansController();
            PlansController controller5 = new PlansController();
            PlanVM vm = new PlanVM();
            vm.Name = "0000 test";
            vm.Description = "SaveEditedPlanWithDescriptionChange Pre-test";


            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            ListVM<Plan, PlanVM> listVM = (ListVM<Plan, PlanVM>)view1.Model;
            var result = (from m in listVM.Entities
                          where m.Name == "0000 test"
                          select m).AsQueryable();

            Plan  x = result.FirstOrDefault(); 
            PlanVM plan = Mapper.Map<Plan, PlanVM>(x);

            try
            {
                // verify initial value:
                Assert.AreEqual("SaveEditedPlanWithDescriptionChange Pre-test", plan.Description);
            }
            catch (Exception)
            {
                // clean up 
                controller5.DeleteConfirmed(vm.ID);
                throw;
            }


            // now edit it
            vm.ID = plan.ID;
            vm.Name = "0000 test Edited";
            vm.Description = "SaveEditedPlanWithDescriptionChange Post-test";

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            ListVM<Plan, PlanVM> listVM2 = (ListVM<Plan, PlanVM>)view2.Model;
            var result2 = (from m in listVM2.Entities
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable();

            
            Plan item = result2.FirstOrDefault();
            PlanVM planVM = Mapper.Map<Plan, PlanVM>(item);

            try
            {
                // Assert
                Assert.AreEqual("0000 test Edited", planVM.Name);
                Assert.AreEqual("SaveEditedPlanWithDescriptionChange Post-test", planVM.Description);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // clean up 
                controller5.DeleteConfirmed(vm.ID);
            }
        }


        [TestMethod]
        [TestCategory("DeleteConfirmed")]
        public void ActuallyDeleteAPlanFromTheDatabase()
        {
            // Arrange
             EFRepository<Plan,PlanVM> repo = new  EFRepository<Plan,PlanVM>(); ;
            PlansController editController = new PlansController();
            PlansController indexController = new PlansController();
            PlansController deleteController = new PlansController();
            PlanVM vm = new PlanVM();
            vm.Name = "0000" + new Guid().ToString();
            ActionResult ar = editController.PostEdit(vm);
            ViewResult view = indexController.Index();
            ListVM<Plan, PlanVM> listVM = (ListVM<Plan, PlanVM>)view.Model;
            var result = (from m in listVM.Entities
                          where m.Name == vm.Name
                          select m).AsQueryable();
            
            Plan plan = result.FirstOrDefault();
            PlanVM item = Mapper.Map<Plan, PlanVM>(plan);

            //Act
            deleteController.DeleteConfirmed(item.ID);
            var deletedItem = (from m in repo.GetAll()
                               where m.Name == vm.Name
                               select m).AsQueryable();

            //Assert
            Assert.AreEqual(0, deletedItem.Count());
        }
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);
            PlanVM planVM = new PlanVM(CreationDate);
            planVM.Name = "001 Test ";

             EFRepository<Plan,PlanVM> repo = new  EFRepository<Plan,PlanVM>(); ;
            PlansController controllerEdit = new PlansController();
            PlansController controllerView = new PlansController();
            PlansController controllerDelete = new PlansController();

            // Act
            controllerEdit.PostEdit(planVM);
            ViewResult view = controllerView.Index();
            ListVM<Plan, PlanVM> listVM = (ListVM<Plan, PlanVM>)view.Model;
            var result = (from m in listVM.Entities
                          where m.Name == "001 Test "
                          select m).AsQueryable(); 
            Plan item = result.FirstOrDefault();
            PlanVM plan = Mapper.Map<Plan, PlanVM>(item);

            DateTime shouldBeSameDate = plan.CreationDate;
            try
            {
                // Assert
                Assert.AreEqual(CreationDate, shouldBeSameDate);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Cleanup
                controllerDelete.DeleteConfirmed(plan.ID);
            }
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            // Arrange
             EFRepository<Plan,PlanVM> repo = new  EFRepository<Plan,PlanVM>();
            PlansController controllerPost = new PlansController();
            PlansController controllerView = new PlansController();
            PlansController controllerDelete = new PlansController();

            PlanVM planVM = new PlanVM();
            planVM.Name = "002 Test Mod";
            DateTime CreationDate = planVM.CreationDate;
            DateTime mod = planVM.ModifiedDate;

            // Act
            controllerPost.PostEdit(planVM);
            ViewResult view = controllerView.Index();
            ListVM<Plan, PlanVM> listVM = (ListVM<Plan, PlanVM>)view.Model;
            var result = (from m in listVM.Entities
                          where m.Name == "002 Test Mod"
                          select m).AsQueryable();

            Plan item = result.FirstOrDefault();
            PlanVM plan = Mapper.Map<Plan, PlanVM>(item);

            DateTime shouldBeSameDate = plan.CreationDate;
            DateTime shouldBeLaterDate = plan.ModifiedDate;
            try
            {
                // Assert
                Assert.AreEqual(CreationDate, shouldBeSameDate);
                Assert.AreNotEqual(mod, shouldBeLaterDate);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Cleanup
                controllerDelete.DeleteConfirmed(plan.ID);
            }
        }
    }
}

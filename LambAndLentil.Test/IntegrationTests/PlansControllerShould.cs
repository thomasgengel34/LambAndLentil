using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("PlansController")]
    public class PlansControllerShould
    {
        static IRepository<Plan> Repo;
        static PlansController controller;
        static Plan planVM;

        public PlansControllerShould()
        {
            Repo = new TestRepository<Plan>();
            controller = new PlansController(Repo);
            planVM = new Plan
            {
                ID = 1000,
                Description = "test PlanControllerShould"
            };
        }

        [TestMethod]
        public void CreateAnPlan()
        {
            // Arrange 

            // Act
            ViewResult vr = controller.Create(UIViewType.Create);
            Plan vm = (Plan)vr.Model;
            string modelName = vm.Name;

            // Assert 
            Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
            Assert.AreEqual(modelName, "Newly Created");
        }

        [Ignore]
        [TestMethod]
        public void SaveAValidPlan()
        {
            // Arrange  

            // Act
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit(planVM);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;

            // Assert 
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(4, routeValues.Count);
            Assert.AreEqual(UIControllerType.Plans.ToString(), routeValues.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(1).ToString());
            Assert.AreEqual("Plans", routeValues.ElementAt(2).ToString());
            Assert.AreEqual(1.ToString(), routeValues.ElementAt(3).ToString());
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedPlanWithNameChange()
        {
            // Arrange 
            PlansController controller1 = new PlansController(Repo);
            PlansController controller2 = new PlansController(Repo);
            PlansController controller3 = new PlansController(Repo);
            PlansController controller4 = new PlansController(Repo);
            PlansController controller5 = new PlansController(Repo);
            Plan vm = new Plan
            {
                Name = "0000 test"
            };

            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            List<Plan> list = (List<Plan>)view1.Model;
            Plan item = (from m in list
                           where m.Name == "0000 test"
                           select m).AsQueryable().First();


            // verify initial value:
            Assert.AreEqual("0000 test", item.Name);

            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = item.ID;
            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            List<Plan> list2 = (List<Plan>)view2.Model;
            Plan planVM2 = (from m in list2
                              where m.Name == "0000 test Edited"
                              select m).AsQueryable().First();

            // Assert
            Assert.AreEqual("0000 test Edited", planVM2.Name);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedPlanWithDescriptionChange()
        {
            // Arrange 
            PlansController controller1 = new PlansController(Repo);
            PlansController controller2 = new PlansController(Repo);
            PlansController controller3 = new PlansController(Repo);
            PlansController controller4 = new PlansController(Repo);
            PlansController controller5 = new PlansController(Repo);
            Plan vm = new Plan
            {
                Name = "0000 test",
                Description = "SaveEditedPlanWithDescriptionChange Pre-test"
            };


            // Act 
            ActionResult ar1 = controller1.PostEdit(vm);
            ViewResult view1 = controller2.Index();
            List<Plan> list = (List<Plan>)view1.Model;
            Plan planVM = (from m in list
                             where m.Name == "0000 test"
                             select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("SaveEditedPlanWithDescriptionChange Pre-test", planVM.Description);



            // now edit it

            planVM.Name = "0000 test Edited";
            planVM.Description = "SaveEditedPlanWithDescriptionChange Post-test";

            ActionResult ar2 = controller3.PostEdit(vm);
            ViewResult view2 = controller4.Index();
            List<Plan> list2 = (List<Plan>)view2.Model;
            planVM = (from m in list2
                           where m.Name == "0000 test Edited"
                           select m).AsQueryable().FirstOrDefault(); 

            // Assert
            Assert.AreEqual("0000 test Edited", planVM.Name);
            Assert.AreEqual("SaveEditedPlanWithDescriptionChange Post-test", planVM.Description);
        }

        
        [TestMethod]
        [TestCategory("DeleteConfirmed")]
        public void ActuallyDeleteAPlanFromTheDatabase()
        {
            // Arrange 
           
            //Act
            controller.DeleteConfirmed(planVM.ID);
            var deletedItem = (from m in Repo.GetAll()
                               where m.Description == planVM.Description
                               select m).AsQueryable();

            //Assert
            Assert.AreEqual(0, deletedItem.Count());
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveTheCreationDateBetweenPostedEdits()
        {
            // Arrange
            DateTime CreationDate = new DateTime(2010, 1, 1);
            Plan planVM = new Plan(CreationDate)
            {
                Name = "001 Test "
            };


            PlansController controllerEdit = new PlansController(Repo);
            PlansController controllerView = new PlansController(Repo);
            PlansController controllerDelete = new PlansController(Repo);

            // Act
            controllerEdit.PostEdit(planVM);
            ViewResult view = controllerView.Index();
            List<Plan> list = (List<Plan>)view.Model;
            planVM = (from m in list
                          where m.Name == "001 Test "
                          select m).AsQueryable().First();
           

            DateTime shouldBeSameDate = planVM.CreationDate;
           
                // Assert
                Assert.AreEqual(CreationDate, shouldBeSameDate);
            
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            // Arrange 
            PlansController controllerPost = new PlansController(Repo);
            PlansController controllerView = new PlansController(Repo);
            PlansController controllerDelete = new PlansController(Repo);

            Plan planVM = new Plan
            {
                Name = "002 Test Mod"
            };
            DateTime CreationDate = planVM.CreationDate;
            DateTime mod = planVM.ModifiedDate;

            // Act
            controllerPost.PostEdit(planVM);
            ViewResult view = controllerView.Index();
            List<Plan> list = (List<Plan>)view.Model;
            planVM = (from m in list
                          where m.Name == "002 Test Mod"
                          select m).AsQueryable().First();

            
            DateTime shouldBeSameDate = planVM.CreationDate;
            DateTime shouldBeLaterDate = planVM.ModifiedDate;
             
                // Assert
                Assert.AreEqual(CreationDate, shouldBeSameDate);
                Assert.AreNotEqual(mod, shouldBeLaterDate);
             
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void AttachAnExistingIngredientToAnExistingPlan()
        {
            // Arrange
            
           IRepository<Ingredient> repoIngredient = new TestRepository<Ingredient>();
            PlansController controller = new PlansController(Repo);

            planVM.Description = "test AttachAnExistingIngredientToAnExistingPlan";
            Ingredient ingredient = new Ingredient
            {
                ID = 100,
                Description = "test AttachAnExistingIngredientToAnExistingPlan"
            };

            // Act
            controller.AttachIngredient(planVM.ID, ingredient );
            Plan returnedPlan = (from m in Repo.GetAll()
                                 where m.Description == planVM.Description
                                 select m).FirstOrDefault(); 

            // Assert 
            Assert.AreEqual(1, returnedPlan.Ingredients.Count());
            // how do I know the correct ingredient was added?
            Assert.AreEqual(ingredient.ID, returnedPlan.Ingredients.First().ID); 
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void NotDeleteAnIngredientAfterIngredientIsDetachedFromPlan()
        {
            Assert.Fail();
        }

 [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToANonExistingPlan()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingExistIngredientToNonExistingPlan()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenAttachingNonExistIngredientToNonExistingPlan()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNonExistingPlan()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnPlanEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingPlan()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnPlanEditViewWithWarningMessageWhenAttachingNonExistingIngredientToExistingRPlan()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnPlanEditViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToExistingPlan()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void ReturnPlanIndexViewWithWarningWhenDetachingExistingingredientNotAttachedToAnExistingPlan()
        {
            Assert.Fail();
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            PlansController_Test_Should.ClassCleanup();
        }
    }
}

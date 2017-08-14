using AutoMapper;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IntegrationTests
{
    [Ignore]
    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("PlansController")]
    public class PlansControllerShould
    {
        //[TestMethod]
        //public void CreateAnPlan()
        //{
        //    // Arrange
        //     IRepository<Plan,PlanVM> planRepo = new  JSONRepository<Plan,PlanVM>(); ;
        //    PlansController controller = new PlansController(planRepo);
        //    // Act
        //    ViewResult vr = controller.Create(UIViewType.Create);
        //    PlanVM vm = (PlanVM)vr.Model;
        //    string modelName = vm.Name;

        //    // Assert 
        //    Assert.AreEqual(vr.ViewName, UIViewType.Details.ToString());
        //    Assert.AreEqual(modelName, "Newly Created");
        //}

        //[TestMethod]
        //public void SaveAValidPlan()
        //{
        //    // Arrange
        //     IRepository<Plan,PlanVM> planRepo = new  JSONRepository<Plan,PlanVM>(); ;
        //    PlansController controller = new PlansController(planRepo);
        //    PlanVM vm = new PlanVM();
        //    vm.Name = "test";
        //    // Act
        //    AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit(vm);
        //    RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

        //    var routeValues = rtrr.RouteValues.Values;

        //    try
        //    {
        //        // Assert 
        //        Assert.AreEqual("alert-success", adr.AlertClass);
        //        Assert.AreEqual(4, routeValues.Count);
        //        Assert.AreEqual(UIControllerType.Plans.ToString(), routeValues.ElementAt(0).ToString());
        //        Assert.AreEqual(UIViewType.BaseIndex.ToString(), routeValues.ElementAt(1).ToString());
        //        Assert.AreEqual("Plans", routeValues.ElementAt(2).ToString());
        //        Assert.AreEqual(1.ToString(), routeValues.ElementAt(3).ToString());
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    finally
        //    {
        //        // Clean Up - should run a  delete test to make sure this works  
        //        Plan plan = planRepo.GetAllT().Where(m => m.Name == "test").FirstOrDefault();
        //        controller.DeleteConfirmed(plan.ID);
        //    }
        //}

        //[TestMethod]
        //[TestCategory("Edit")]
        //public void SaveEditedPlanWithNameChange()
        //{
        //    // Arrange
        //    IRepository<Plan,PlanVM> planRepo = new  JSONRepository<Plan,PlanVM>(); ;
        //    PlansController controller1 = new PlansController(planRepo);
        //    PlansController controller2 = new PlansController(planRepo);
        //    PlansController controller3 = new PlansController(planRepo);
        //    PlansController controller4 = new PlansController(planRepo);
        //    PlansController controller5 = new PlansController(planRepo);
        //    PlanVM vm = new PlanVM();
        //    vm.Name = "0000 test";

        //    // Act 
        //    ActionResult ar1 = controller1.PostEdit(vm);
        //    ViewResult view1 = controller2.Index();
        //    ListVM<Plan,PlanVM> listVM = (ListVM<Plan, PlanVM>)view1.Model;
        //    var result = (from m in listVM.Entities
        //                  where m.Name == "0000 test"
        //                  select m).AsQueryable();

        //    Plan plan = result.FirstOrDefault(); 
        //    PlanVM item = Mapper.Map<Plan, PlanVM>(plan);
        //    try
        //    {
        //        // verify initial value:
        //        Assert.AreEqual("0000 test", item.Name);
        //    }
        //    catch (Exception)
        //    {
        //        controller5.DeleteConfirmed(vm.ID);
        //        throw;
        //    }
        //    // now edit it
        //    vm.Name = "0000 test Edited";
        //    vm.ID = item.ID;
        //    ActionResult ar2 = controller3.PostEdit(vm);
        //    ViewResult view2 = controller4.Index();
        //    ListVM<Plan, PlanVM> listVM2 = (ListVM<Plan, PlanVM>)view2.Model;
        //    var result2 = (from m in listVM2.Entities
        //                   where m.Name == "0000 test Edited"
        //                   select m).AsQueryable();
 
        //    Plan plan2 = result2.FirstOrDefault();
        //    PlanVM item2 = Mapper.Map<Plan, PlanVM>(plan2);
        //    try
        //    {
        //        // Assert
        //        Assert.AreEqual("0000 test Edited", item2.Name);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    finally
        //    {
        //        // clean up 
        //        controller5.DeleteConfirmed(item2.ID);
        //    }
        //}




        //[TestMethod]
        //[TestCategory("Edit")]
        //public void SaveEditedPlanWithDescriptionChange()
        //{
        //    // Arrange
        //    IRepository<Plan,PlanVM> planRepo = new  JSONRepository<Plan,PlanVM>(); ;
        //    PlansController controller1 = new PlansController(planRepo);
        //    PlansController controller2 = new PlansController(planRepo);
        //    PlansController controller3 = new PlansController(planRepo);
        //    PlansController controller4 = new PlansController(planRepo);
        //    PlansController controller5 = new PlansController(planRepo);
        //    PlanVM vm = new PlanVM();
        //    vm.Name = "0000 test";
        //    vm.Description = "SaveEditedPlanWithDescriptionChange Pre-test";


        //    // Act 
        //    ActionResult ar1 = controller1.PostEdit(vm);
        //    ViewResult view1 = controller2.Index();
        //    ListVM<Plan, PlanVM> listVM = (ListVM<Plan, PlanVM>)view1.Model;
        //    var result = (from m in listVM.Entities
        //                  where m.Name == "0000 test"
        //                  select m).AsQueryable();

        //    Plan  x = result.FirstOrDefault(); 
        //    PlanVM plan = Mapper.Map<Plan, PlanVM>(x);

        //    try
        //    {
        //        // verify initial value:
        //        Assert.AreEqual("SaveEditedPlanWithDescriptionChange Pre-test", plan.Description);
        //    }
        //    catch (Exception)
        //    {
        //        // clean up 
        //        controller5.DeleteConfirmed(vm.ID);
        //        throw;
        //    }


        //    // now edit it
        //    vm.ID = plan.ID;
        //    vm.Name = "0000 test Edited";
        //    vm.Description = "SaveEditedPlanWithDescriptionChange Post-test";

        //    ActionResult ar2 = controller3.PostEdit(vm);
        //    ViewResult view2 = controller4.Index();
        //    ListVM<Plan, PlanVM> listVM2 = (ListVM<Plan, PlanVM>)view2.Model;
        //    var result2 = (from m in listVM2.Entities
        //                   where m.Name == "0000 test Edited"
        //                   select m).AsQueryable();

            
        //    Plan item = result2.FirstOrDefault();
        //    PlanVM planVM = Mapper.Map<Plan, PlanVM>(item);

        //    try
        //    {
        //        // Assert
        //        Assert.AreEqual("0000 test Edited", planVM.Name);
        //        Assert.AreEqual("SaveEditedPlanWithDescriptionChange Post-test", planVM.Description);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        // clean up 
        //        controller5.DeleteConfirmed(vm.ID);
        //    }
        //}


        //[TestMethod]
        //[TestCategory("DeleteConfirmed")]
        //public void ActuallyDeleteAPlanFromTheDatabase()
        //{
        //    // Arrange
        //     JSONRepository<Plan,PlanVM> planRepo = new  JSONRepository<Plan,PlanVM>(); ;
            
        //    PlansController controller = new PlansController(planRepo);
        //    Plan item= GetPlan(planRepo, "test ActuallyDeleteAPlanFromTheDatabase");

        //    //Act
        //    controller.DeleteConfirmed(item.ID);
        //    var deletedItem = (from m in planRepo.GetAllT()
        //                       where m.Description == item.Description
        //                       select m).AsQueryable();

        //    //Assert
        //    Assert.AreEqual(0, deletedItem.Count());
        //}
        //[TestMethod]
        //[TestCategory("Edit")]
        //public void SaveTheCreationDateBetweenPostedEdits()
        //{
        //    // Arrange
        //    DateTime CreationDate = new DateTime(2010, 1, 1);
        //    PlanVM planVM = new PlanVM(CreationDate);
        //    planVM.Name = "001 Test ";

        //     IRepository<Plan,PlanVM> planRepo = new  JSONRepository<Plan,PlanVM>(); ;
        //    PlansController controllerEdit = new PlansController(planRepo);
        //    PlansController controllerView = new PlansController(planRepo);
        //    PlansController controllerDelete = new PlansController(planRepo);

        //    // Act
        //    controllerEdit.PostEdit(planVM);
        //    ViewResult view = controllerView.Index();
        //    ListVM<Plan, PlanVM> listVM = (ListVM<Plan, PlanVM>)view.Model;
        //    var result = (from m in listVM.Entities
        //                  where m.Name == "001 Test "
        //                  select m).AsQueryable(); 
        //    Plan item = result.FirstOrDefault();
        //    PlanVM plan = Mapper.Map<Plan, PlanVM>(item);

        //    DateTime shouldBeSameDate = plan.CreationDate;
        //    try
        //    {
        //        // Assert
        //        Assert.AreEqual(CreationDate, shouldBeSameDate);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        // Cleanup
        //        controllerDelete.DeleteConfirmed(plan.ID);
        //    }
        //}

        //[TestMethod]
        //[TestCategory("Edit")]
        //public void UpdateTheModificationDateBetweenPostedEdits()
        //{
        //    // Arrange
        //     IRepository<Plan,PlanVM> planRepo = new  JSONRepository<Plan,PlanVM>();
        //    PlansController controllerPost = new PlansController(planRepo);
        //    PlansController controllerView = new PlansController(planRepo);
        //    PlansController controllerDelete = new PlansController(planRepo);

        //    PlanVM planVM = new PlanVM();
        //    planVM.Name = "002 Test Mod";
        //    DateTime CreationDate = planVM.CreationDate;
        //    DateTime mod = planVM.ModifiedDate;

        //    // Act
        //    controllerPost.PostEdit(planVM);
        //    ViewResult view = controllerView.Index();
        //    ListVM<Plan, PlanVM> listVM = (ListVM<Plan, PlanVM>)view.Model;
        //    var result = (from m in listVM.Entities
        //                  where m.Name == "002 Test Mod"
        //                  select m).AsQueryable();

        //    Plan item = result.FirstOrDefault();
        //    PlanVM plan = Mapper.Map<Plan, PlanVM>(item);

        //    DateTime shouldBeSameDate = plan.CreationDate;
        //    DateTime shouldBeLaterDate = plan.ModifiedDate;
        //    try
        //    {
        //        // Assert
        //        Assert.AreEqual(CreationDate, shouldBeSameDate);
        //        Assert.AreNotEqual(mod, shouldBeLaterDate);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        // Cleanup
        //        controllerDelete.DeleteConfirmed(plan.ID);
        //    }
        //}

        //internal Plan GetPlan(JSONRepository<Plan, PlanVM> repo, string description)
        //{

        //    JSONRepository<Plan, PlanVM> repoPlan = new JSONRepository<Plan, PlanVM>();
        //    PlansController controller = new PlansController(repoPlan);
        //    PlanVM  vm = new PlanVM();
        //     vm.ID = int.MaxValue;
        //     vm.Description = description;
        //    controller.PostEdit( vm);

        //    Plan plan = ((from m in repoPlan.GetAllT()
        //                  where m.Description == description
        //                  select m).AsQueryable()).FirstOrDefault();
        //    return plan;
        //}

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void AttachAnExistingIngredientToAnExistingPlan()
        //{
        //    // Arrange
        //    JSONRepository<Plan, PlanVM> repoPlan = new JSONRepository<Plan, PlanVM>();
        //    JSONRepository<Ingredient, IngredientVM> repoIngredient = new JSONRepository<Ingredient, IngredientVM>();
        //    PlansController controller = new PlansController(repoPlan);

        //    Plan menu = GetPlan(repoPlan, "test AttachAnExistingIngredientToAnExistingPlan");
        //    Ingredient ingredient = new RecipesControllerShould().GetIngredient(repoIngredient, "test AttachAnExistingIngredientToAnExistingPlan");

        //    // Act
        //    controller.AttachIngredient(menu.ID, ingredient.ID);
        //    Plan returnedPlan = (from m in repoPlan.GetAllT()
        //                         where m.Description == menu.Description
        //                         select m).FirstOrDefault();



        //    // Assert 
        //    Assert.AreEqual(1, returnedPlan.Ingredients.Count());
        //    // how do I know the correct ingredient was added?
        //    Assert.AreEqual(ingredient.ID, returnedPlan.Ingredients.First().ID);

        //    // Cleanup
        //     IngredientsController<Ingredient, IngredientVM> controllerCleanupIngredient = new  IngredientsController<Ingredient, IngredientVM>(repoIngredient);
        //    PlansController controllerCleanupPlan = new PlansController(repoPlan);

        //    PlanVM menuVM = Mapper.Map<Plan, PlanVM>(menu);
        //    IngredientVM ingredientVM = Mapper.Map<Ingredient, IngredientVM>(ingredient);

        //    controllerCleanupPlan.DeleteConfirmed(menuVM.ID);
        //    controllerCleanupIngredient.DeleteConfirmed(ingredientVM.ID);
        //} 

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void NotDeleteAnIngredientAfterIngredientIsDetachedFromPlan()
        //{
        //    Assert.Fail();
        //}
         

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void ReturnIndexViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToANonExistingPlan()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void ReturnIndexViewWithWarningWhenAttachingExistIngredientToNonExistingPlan()
        //{
        //    Assert.Fail();
        //}
        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void ReturnIndexViewWithWarningWhenAttachingNonExistIngredientToNonExistingPlan()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void ReturnIndexViewWithWarningWhenDetachingExistingIngredientAttachedToNonExistingPlan()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void ReturnPlanEditViewWithSuccessMessageWhenDetachingExistingIngredientFromExistingPlan()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void ReturnPlanEditViewWithWarningMessageWhenAttachingNonExistingIngredientToExistingRPlan()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void ReturnPlanEditViewWithWarningMessageWhenDetachingNonExistingIngredientAttachedToExistingPlan()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //[TestCategory("Attach-Detach")]
        //public void ReturnPlanIndexViewWithWarningWhenDetachingExistingingredientNotAttachedToAnExistingPlan()
        //{
        //    Assert.Fail();
        //}
    }
}

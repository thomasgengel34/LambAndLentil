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
    public class PlansControllerShould:BaseControllerTest<Plan>
    { 
        static PlansController Controller;
        static Plan Plan;

        public PlansControllerShould()
        {
            Repo = new TestRepository<Plan>();
            Controller = new PlansController(Repo);
            Plan = new Plan
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
            ViewResult vr = Controller.Create(UIViewType.Create);
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
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.PostEdit(Plan);
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
            PlansController Controller1 = new PlansController(Repo);
            PlansController Controller2 = new PlansController(Repo);
            PlansController Controller3 = new PlansController(Repo);
            PlansController Controller4 = new PlansController(Repo);
            PlansController Controller5 = new PlansController(Repo);
            Plan vm = new Plan
            {
                Name = "0000 test"
            };

            // Act 
            ActionResult ar1 = Controller1.PostEdit(vm);
            ViewResult view1 = Controller2.Index();
            List<Plan> ListEntity = (List<Plan>)view1.Model;
            Plan item = (from m in ListEntity
                         where m.Name == "0000 test"
                         select m).AsQueryable().First();


            // verify initial value:
            Assert.AreEqual("0000 test", item.Name);

            // now edit it
            vm.Name = "0000 test Edited";
            vm.ID = item.ID;
            ActionResult ar2 = Controller3.PostEdit(vm);
            ViewResult view2 = Controller4.Index();
            List<Plan> ListEntity2 = (List<Plan>)view2.Model;
            Plan Plan2 = (from m in ListEntity2
                            where m.Name == "0000 test Edited"
                            select m).AsQueryable().First();

            // Assert
            Assert.AreEqual("0000 test Edited", Plan2.Name);
        }

        [Ignore]
        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedPlanWithDescriptionChange()
        {
            // Arrange 
            PlansController Controller1 = new PlansController(Repo);
            PlansController Controller2 = new PlansController(Repo);
            PlansController Controller3 = new PlansController(Repo);
            PlansController Controller4 = new PlansController(Repo);
            PlansController Controller5 = new PlansController(Repo);
            Plan vm = new Plan
            {
                Name = "0000 test",
                Description = "SaveEditedPlanWithDescriptionChange Pre-test"
            };


            // Act 
            ActionResult ar1 = Controller1.PostEdit(vm);
            ViewResult view1 = Controller2.Index();
            List<Plan> ListEntity = (List<Plan>)view1.Model;
            Plan Plan = (from m in ListEntity
                           where m.Name == "0000 test"
                           select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("SaveEditedPlanWithDescriptionChange Pre-test", Plan.Description);



            // now edit it

            Plan.Name = "0000 test Edited";
            Plan.Description = "SaveEditedPlanWithDescriptionChange Post-test";

            ActionResult ar2 = Controller3.PostEdit(vm);
            ViewResult view2 = Controller4.Index();
            List<Plan> ListEntity2 = (List<Plan>)view2.Model;
            Plan = (from m in ListEntity2
                      where m.Name == "0000 test Edited"
                      select m).AsQueryable().FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited", Plan.Name);
            Assert.AreEqual("SaveEditedPlanWithDescriptionChange Post-test", Plan.Description);
        }


        [TestMethod]
        [TestCategory("DeleteConfirmed")]
        public void ActuallyDeleteAPlanFromTheDatabase()
        {
            // Arrange 

            //Act
            Controller.DeleteConfirmed(Plan.ID);
            var deletedItem = (from m in Repo.GetAll()
                               where m.Description == Plan.Description
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
            Plan Plan = new Plan(CreationDate)
            {
                Name = "001 Test "
            };


            PlansController ControllerEdit = new PlansController(Repo);
            PlansController ControllerView = new PlansController(Repo);
            PlansController ControllerDelete = new PlansController(Repo);

            // Act
            ControllerEdit.PostEdit(Plan);
            ViewResult view = ControllerView.Index();
            List<Plan> ListEntity = (List<Plan>)view.Model;
            Plan = (from m in ListEntity
                      where m.Name == "001 Test "
                      select m).AsQueryable().First();


            DateTime shouldBeSameDate = Plan.CreationDate;

            // Assert
            Assert.AreEqual(CreationDate, shouldBeSameDate);

        }

        
        [TestMethod]
        [TestCategory("Edit")]
        public void UpdateTheModificationDateBetweenPostedEdits()
        {
            Plan.Name = "Test UpdateTheModificationDateBetweenPostedEdits";
            Plan.ID = 6000;
            Repo.Save(Plan);
            BaseUpdateTheModificationDateBetweenPostedEdits(Repo, Controller, Plan);
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void AttachAnExistingIngredientToAnExistingPlan()
        {
            // Arrange

            IRepository<Ingredient> repoIngredient = new TestRepository<Ingredient>();
            PlansController Controller = new PlansController(Repo);

            Plan.Description = "test AttachAnExistingIngredientToAnExistingPlan";
            Ingredient ingredient = new Ingredient
            {
                ID = 100,
                Description = "test AttachAnExistingIngredientToAnExistingPlan"
            };
            Repo.Update(Plan, Plan.ID);
            repoIngredient.Save(ingredient);
            // Act
            Controller.AttachIngredient(Plan.ID, ingredient);
            Plan returnedPlan = (from m in Repo.GetAll()
                                 where m.Description == Plan.Description
                                 select m).FirstOrDefault();

            // Assert 
            Assert.AreEqual(1, returnedPlan.Ingredients.Count());
            // how do I know the correct ingredient was added?
            Assert.AreEqual(ingredient.ID, returnedPlan.Ingredients.First().ID);
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void AttachAnExistingRecipeToAnExistingPlan()
        {
            // Arrange

            IRepository<Recipe> repoRecipe = new TestRepository<Recipe>();
            PlansController Controller = new PlansController(Repo);

            Plan.Description = "test AttachAnExistingRecipeToAnExistingPlan";
            Recipe recipe = new Recipe
            {
                ID = 100,
                Description = "test AttachAnExistingRecipeToAnExistingPlan"
            };
            Repo.Update(Plan, Plan.ID);
            repoRecipe.Save(recipe);
            // Act
            Controller.AttachRecipe(Plan.ID, recipe);
            Plan returnedPlan = (from m in Repo.GetAll()
                                 where m.Description == Plan.Description
                                 select m).FirstOrDefault();

            // Assert 
            Assert.AreEqual(1, returnedPlan.Recipes.Count());
            // how do I know the correct recipe was added?
            Assert.AreEqual(recipe.ID, returnedPlan.Recipes.First().ID);
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
    }
}

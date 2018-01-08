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
        
        static IPlan Plan;

        public PlansControllerShould()
        {
            Plan = new Plan
            {
                ID = 1000,
                Description = "test PlanControllerShould"
            };
            Repo.Save((Plan)Plan);

            Controller = new PlansController(Repo);
        }

        [TestMethod]
        public void CreateAnPlan()
        {
            // Arrange 

            // Act
            ViewResult vr = (ViewResult)Controller.Create(UIViewType.Create);
            Plan Plan = (Plan)vr.Model;
            string modelName = Plan.Name;

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
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.PostEdit((Plan)Plan);
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
            IGenericController<Plan> Controller1 = new PlansController(Repo);
            IGenericController<Plan> Controller2 = new PlansController(Repo);
            IGenericController<Plan> Controller3 = new PlansController(Repo);
            IGenericController<Plan> Controller4 = new PlansController(Repo);
            IGenericController<Plan> Controller5 = new PlansController(Repo);
            IPlan Plan = new Plan
            {
                Name = "0000 test"
            };

            Repo.Save((Plan)Plan);

            // Act 
            ActionResult ar1 = Controller1.PostEdit((Plan)Plan);
            ViewResult view1 = (ViewResult)Controller2.Index();
            List<Plan> ListEntity = (List<Plan>)view1.Model;
            Plan item = (from m in ListEntity
                         where m.Name == "0000 test"
                         select m).AsQueryable().First();


            // verify initial value:
            Assert.AreEqual("0000 test", item.Name);

            // now edit it
            Plan.Name = "0000 test Edited";
            Plan.ID = item.ID;
            ActionResult ar2 = Controller3.PostEdit((Plan)Plan);
            ViewResult view2 = (ViewResult)Controller4.Index();
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
            IGenericController<Plan> Controller1 = new PlansController(Repo);
            IGenericController<Plan> Controller2 = new PlansController(Repo);
            IGenericController<Plan> Controller3 = new PlansController(Repo);
            IGenericController<Plan> Controller4 = new PlansController(Repo);
            IGenericController<Plan> Controller5 = new PlansController(Repo);
             Plan = new Plan
            {
                Name = "0000 test",
                Description = "SaveEditedPlanWithDescriptionChange Pre-test"
            };


            // Act 
            ActionResult ar1 = Controller1.PostEdit((Plan)Plan);
            ViewResult view1 = (ViewResult)Controller2.Index();
            List<Plan> ListEntity = (List<Plan>)view1.Model;
             Plan = (from m in ListEntity
                           where m.Name == "0000 test"
                           select m).AsQueryable().FirstOrDefault();

            // verify initial value:
            Assert.AreEqual("SaveEditedPlanWithDescriptionChange Pre-test", Plan.Description);



            // now edit it

            Plan.Name = "0000 test Edited";
            Plan.Description = "SaveEditedPlanWithDescriptionChange Post-test";

            ActionResult ar2 = Controller3.PostEdit((Plan)Plan);
            ViewResult view2 = (ViewResult)Controller4.Index();
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
            IPlan Plan = new Plan(CreationDate)
            {
                Name = "001 Test "
            };


            IGenericController<Plan> ControllerEdit = new PlansController(Repo);
            IGenericController<Plan> ControllerView = new PlansController(Repo);
            IGenericController<Plan> ControllerDelete = new PlansController(Repo);

            // Act
            ControllerEdit.PostEdit((Plan)Plan);
            ViewResult view = (ViewResult)ControllerView.Index();
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
            Repo.Save((Plan)Plan);
            BaseUpdateTheModificationDateBetweenPostedEdits(Repo, Controller, (Plan)Plan);
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void AttachAnExistingIngredientToAnExistingPlan()
        { 
            Ingredient  ingredient = new Ingredient() { ID = 100 };
            int foo = ingredient.ID;
           
            Controller.Attach(Repo,Plan.ID, (Ingredient)ingredient );
            IEntityChildClassIngredients returnedPlan = Repo.GetById(Plan.ID);

          
            Assert.AreEqual(1, returnedPlan.Ingredients.Count());
            // how do I know the correct ingredient was added?
            Assert.AreEqual(foo, returnedPlan.Ingredients.First().ID);
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void AttachAnExistingRecipeToAnExistingPlan()
        { 
            IRepository<Recipe> repoRecipe = new TestRepository<Recipe>(); 

            Plan.Description = "test AttachAnExistingRecipeToAnExistingPlan";
            IRecipe recipe = new Recipe
            {
                ID = 100,
                Description = "test AttachAnExistingRecipeToAnExistingPlan"
            };
            Repo.Update((Plan)Plan, Plan.ID);
            repoRecipe.Save((Recipe)recipe);
            // Act
            Controller.Attach(Repo,Plan.ID, (Recipe)recipe );
            Plan returnedPlan = Repo.GetById(Plan.ID);

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

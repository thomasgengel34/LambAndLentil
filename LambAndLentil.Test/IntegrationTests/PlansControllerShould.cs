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

namespace LambAndLentil.Test.BaseControllerTests
{

    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("PlansController")]
    public class PlansControllerShould:BaseControllerTest<Plan>
    {
        
        static  Plan Plan;

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
         
        [Ignore]
        [TestMethod]
        public void SaveAValidPlan()
        { 
            AlertDecoratorResult adr = (AlertDecoratorResult)Controller.PostEdit((Plan)Plan);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;
             
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(4, routeValues.Count);
            Assert.AreEqual(UIControllerType.Plans.ToString(), routeValues.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(1).ToString());
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
            Plan Plan = new Plan
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
         
         

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void AttachAnExistingIngredientToAnExistingPlan()
        { 
            Ingredient  ingredient = new Ingredient() { ID = 100 };
         
           
            Controller.Attach(Plan, ingredient);
            IEntity returnedPlan = Repo.GetById(Plan.ID);

          
            Assert.AreEqual(1, returnedPlan.Ingredients.Count()); 
            Assert.AreEqual(ingredient.ID, returnedPlan.Ingredients.First().ID);
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void AttachAnExistingRecipeToAnExistingPlan()
        { 
            IRepository<Recipe> repoRecipe = new TestRepository<Recipe>(); 

            Plan.Description = "test AttachAnExistingRecipeToAnExistingPlan";
             Recipe recipe = new Recipe
            {
                ID = 100,
                Description = "test AttachAnExistingRecipeToAnExistingPlan"
            };
            Repo.Update(Plan, Plan.ID);
            repoRecipe.Save(recipe);
          
            Controller.Attach(Plan , recipe);
            Plan returnedPlan = Repo.GetById(Plan.ID); 

            Assert.AreEqual(1, returnedPlan.Recipes.Count()); 
            Assert.AreEqual(recipe.ID, returnedPlan.Recipes.First().ID);
        } 
    }
}

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
    internal class PlansControllerShould:BaseControllerTest<Plan>
    {
        
        static  Plan Plan;

        public PlansControllerShould()
        {
            Plan = new Plan
            {
                ID = 1000,
                Description = "test PlanControllerShould"
            };
            repo.Save((Plan)Plan);

            controller = new PlansController(repo);
        }
         
        [Ignore]
        [TestMethod]
        public void SaveAValidPlan()
        { 
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit((Plan)Plan);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;
             
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(4, routeValues.Count);
            Assert.AreEqual(UIControllerType.Plans.ToString(), routeValues.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(1).ToString());
            Assert.AreEqual("Plans", routeValues.ElementAt(2).ToString());
            Assert.AreEqual(1.ToString(), routeValues.ElementAt(3).ToString());
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
            repo.Update(Plan, Plan.ID);
            repoRecipe.Save(recipe);
          
            controller.Attach(Plan , recipe);
            Plan returnedPlan = repo.GetById(Plan.ID); 

            Assert.AreEqual(1, returnedPlan.Recipes.Count()); 
            Assert.AreEqual(recipe.ID, returnedPlan.Recipes.First().ID);
        } 
    }
}

using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI;
using System.Collections.Generic;
using LambAndLentil.UI.Models;
using LambAndLentil.Test.TestObjects;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestCategory("PlansController")]
    [TestCategory("Attach")]  
    [TestClass]
    public class PlansController_Attach_Should:PlansController_Test_Should
    { 

        [TestMethod]
        public void ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild()
        { 
            Plan plan= new Plan
            {
                ID = int.MaxValue,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };
            IRepository<Plan> mRepo = new TestRepository<Plan>();
            mRepo.Save(plan);
            Ingredient ingredient = new Ingredient
            {
                ID = 1492,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };

          
            ActionResult ar = Controller.Attach(plan, ingredient );
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;
             
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("Ingredient was Successfully Attached!", adr.Message);
            Assert.AreEqual(int.MaxValue, rdr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual("Edit", rdr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual("Details", rdr.RouteValues.ElementAt(2).Value.ToString());
        }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidChild()
        { 
            Assert.Fail();
        }
 
        [TestMethod]
        public void SuccessfullyAttachChild()
        { 
            Ingredient child = new Ingredient() { ID = 3000, Name = "SuccessfullyAttachChild" };
            TestRepository<Ingredient> IngredientRepo = new TestRepository<Ingredient>();
            IngredientRepo.Save(child);

           
            Controller.Attach(Plan, child );
            ReturnedPlan = Repo.GetById(Plan.ID);
            
            Assert.AreEqual("SuccessfullyAttachChild", ReturnedPlan.Ingredients.Last().Name);
        }
          

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachASetOfIngredientChildren()
        {
            Plan plan = new Plan() { ID = 4000 };
            Ingredient ingredient1 = new Ingredient() { ID = 2001 };
            Ingredient ingredient2 = new Ingredient() { ID = 2002 };
            Ingredient ingredient3 = new Ingredient() { ID = 2003 };
            Ingredient ingredient4 = new Ingredient() { ID = 2004 };
            Ingredient ingredient5 = new Ingredient() { ID = 2005 };
            plan.Ingredients = new List<Ingredient>();
            List<Ingredient> listOfIngredients = new List<Ingredient>()
            {
                ingredient1,
                ingredient2,
                ingredient3,
                ingredient4,
                ingredient5
            };
            plan.Ingredients.AddRange(listOfIngredients); 
            int initialIngredientCount = plan.Ingredients.Count(); 

            var setToSelect = new HashSet<int> { ingredient1.ID,ingredient3.ID };
            List<Ingredient> list = plan.Ingredients.Where(t => setToSelect.Contains(t.ID)).ToList();
            List<IEntity> selected = new List<IEntity>();
            selected.AddRange(list);
            Controller.DetachASetOf(plan,selected);
            Plan returnedPlan = Repo.GetById(plan.ID);

            Assert.AreEqual(initialIngredientCount - 2, returnedPlan.Ingredients.Count());
        }

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachRecipeChild()
        { 
            Assert.Fail();
        }

        
        //[TestMethod]
        //public void SuccessfullyAttachChild()
        //{
        //    IGenericController<Plan> DetachController = new PlansController(Repo);
        //    BaseSuccessfullyDetachChild(Repo, Controller, DetachController, UIControllerType.Plans);
        //}

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachChild() => 
            Assert.Fail();

     
    }
}

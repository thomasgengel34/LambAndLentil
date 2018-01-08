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
        public void ReturnsIndexWithWarningWithUnknownParentID() => BaseReturnsIndexWithWarningWithUnknownParentID(Repo, Controller);

        [TestMethod]
        public void ReturnsIndexWithWarningWithNullParent() => BaseReturnsIndexWithWarningWithNullParent(Repo, Controller);

        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownChildID() => BaseReturnsDetailWithWarningWithUnknownChildID(Plan, Repo, Controller);

        [TestMethod]
        public void ReturnsDetailWithWarningIfAttachingNullChild() => BaseReturnsDetailWithWarningIfAttachingNullChild(Plan, Repo, Controller);

        [TestMethod]
        public void ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild()
        {
            // Arrange
            Plan menu = new Plan
            {
                ID = int.MaxValue,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };
            IRepository<Plan> mRepo = new TestRepository<Plan>();
            mRepo.Add(menu);
            Ingredient ingredient = new Ingredient
            {
                ID = 1492,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };

            // Act
            ActionResult ar = Controller.Attach(Repo,int.MaxValue, ingredient );
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

            //Assert
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
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
 
        [TestMethod]
        public void SuccessfullyAttachChild()
        {
             
            Ingredient child = new Ingredient() { ID = 3000, Name = "SuccessfullyAttachChild" };
            TestRepository<Ingredient> IngredientRepo = new TestRepository<Ingredient>();
            IngredientRepo.Save(child);

           
            Controller.Attach(Repo,Plan.ID, child );
            ReturnedPlan = Repo.GetById(Plan.ID);
            
            Assert.AreEqual("SuccessfullyAttachChild", ReturnedPlan.Ingredients.Last().Name);
        }
          

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachASetOfIngredientChildren()
        { 
             Plan plan = new TestPlan().CreatePlan(); 
            plan = new TestPlan().AddIngredientChildrenToPlan(plan);
            
            Repo.Save((Plan)plan);
            int initialIngredientCount = plan.Ingredients.Count();

            int firstIDtoRemove = int.MaxValue - 1;
            int secondIDtoRemove = int.MaxValue - 3;


            var setToSelect = new HashSet<int> { firstIDtoRemove, secondIDtoRemove };
            List<Ingredient> selected = plan.Ingredients.Where(t => setToSelect.Contains(t.ID)).ToList();
            Controller.DetachASetOf(plan.ID, selected);
            Plan returnedPlan = Repo.GetById(plan.ID);

            // Assert
            Assert.AreEqual(initialIngredientCount - 2, returnedPlan.Ingredients.Count());
        }

        [TestMethod]
        public void DetachTheLastIngredientChild()
        {
            BaseDetachTheLastIngredientChild(Repo, Controller, Plan);
        }

        [TestMethod] 
        public void DetachAllIngredientChildren()=>       
            BaseDetachAllIngredientChildren(Repo, Controller ); 



        [TestMethod]
        public void SuccessfullyAttachRecipeChild()
        {
            BaseSuccessfullyAttachRecipeChild(Plan, Controller);
        }

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachRecipeChild()
        {
            // Arrange

            // Act

            // Assert
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
            // Arrange

            // Act

            // Assert
            Assert.Fail();

        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild() => BaseReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild(Repo, Controller, Plan.ID);
    }
}

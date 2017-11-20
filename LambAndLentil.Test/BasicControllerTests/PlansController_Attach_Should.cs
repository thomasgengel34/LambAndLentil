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

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestCategory("PlansController")]
    [TestCategory("Attach")]  
    [TestClass]
    public class PlansController_Attach_Should:PlansController_Test_Should
    { 
        [TestMethod]
        public void ReturnsIndexWithWarningWithUnknownParentID()
        {
            BaseReturnsIndexWithWarningWithUnknownParentID(Repo, Controller);
        }

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
            ActionResult ar = Controller.AttachIngredient(int.MaxValue, ingredient);
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
        public void SuccessfullyAttachIngredientChild()
        {
            // Arrange
            Ingredient child = new Ingredient() { ID = 3000, Name = "SuccessfullyAttachIngredientChild" };
            TestRepository<Ingredient> IngredientRepo = new TestRepository<Ingredient>();
            IngredientRepo.Save(child);

            // Act
            Controller.AttachIngredient(Plan.ID, child);
            ReturnedPlan = Repo.GetById(Plan.ID);
            // Assert
            //  Assert.AreEqual("Default", Ingredient.Ingredients.Last().Name);
            Assert.AreEqual("SuccessfullyAttachIngredientChild", ReturnedPlan.Ingredients.Last().Name);
        }
         
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void SuccessfullyDetachFirstIngredientChild()
        {
            IGenericController<Plan> DetachController = new PlansController(Repo);
            BaseSuccessfullyDetachIngredientChild(Repo, Controller, DetachController, UIControllerType.Plans, 0); 
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachASetOfIngredientChildren()
        {
            // Arrange 
            Plan.Ingredients.Add(new Ingredient { ID = 4005, Name = "Butter" });
            Plan.Ingredients.Add(new Ingredient { ID = 4006, Name = "Cayenne Pepper" });
            Plan.Ingredients.Add(new Ingredient { ID = 4007, Name = "Cheese" });
            Plan.Ingredients.Add(new Ingredient { ID = 4008, Name = "Chopped Green Pepper" });
            Repo.Save((Plan)Plan);
            int initialIngredientCount = Plan.Ingredients.Count();

            // Act
            var setToSelect = new HashSet<int> { 4006, 4008 };
            List<Ingredient> selected = Plan.Ingredients.Where(t => setToSelect.Contains(t.ID)).ToList();
            Controller.DetachAllIngredients(Plan.ID, selected);
            Plan returnedPlan = Repo.GetById(Plan.ID);

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
            BaseDetachAllIngredientChildren(Repo, Controller, Plan); 



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

        
        [TestMethod]
        public void SuccessfullyAttachMenuChild()
        {
            IGenericController<Plan> DetachController = new PlansController(Repo);
            BaseSuccessfullyDetachMenuChild(Repo, Controller, DetachController, UIControllerType.Plans);
        }

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachMenuChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild() => BaseReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild(Repo, Controller, Plan.ID);
    }
}

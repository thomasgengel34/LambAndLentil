using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using System.Web.Mvc;
using LambAndLentil.UI.Infrastructure.Alerts;
using System.Linq;

namespace LambAndLentil.Test.BasicControllerTests
{
    
    [TestClass]
    public class PersonsController_Attach_Should:PersonsController_Test_Should
    {
        [Ignore]
        [TestMethod]
        public void ReturnsErrorWithUnknownRepository()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnsIndexWithWarningWithUnknownParentID()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnsIndexWithWarningWithNullParent()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownChildID()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWithWarningWithNullChild()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild()
        {
            // Arrange
            Person menu = new Person
            {
                ID = int.MaxValue,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };
            IRepository<Person> mRepo = new TestRepository<Person>();
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
            Controller.AttachIngredient(Person.ID, child);
            ReturnedPerson = Repo.GetById(Person.ID);
            // Assert
            //  Assert.AreEqual("Default", Ingredient.Ingredients.Last().Name);
            Assert.AreEqual("SuccessfullyAttachIngredientChild", ReturnedPerson.Ingredients.Last().Name);
        }

       
        [TestMethod]
        public void SuccessfullyDetachIngredientChild()
        {

        }

        [Ignore]
        [TestMethod]
        public void SuccessfullyAttachRecipeChild()
        {

        }

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachRecipeChild()
        {

        }

        [Ignore]
        [TestMethod]
        public void SuccessfullyAttachMenuChild()
        {

        }

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachMenuChild()
        {

        }

        [Ignore]
        [TestMethod]
        public void SuccessfullyAttachPlanChild()
        {

        }

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachPlanChild()
        {

        }

        [Ignore]
        [TestMethod]
        public void SuccessfullyAttachShoppingListChild()
        {

        }

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachShoppingListChild()
        {

        }
    }
}

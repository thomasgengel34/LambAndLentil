using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using System.Web.Mvc;
using LambAndLentil.UI.Infrastructure.Alerts;
using System.Linq;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using System.Collections.Generic;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    public class PersonsController_Attach_Should : PersonsController_Test_Should
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



        [TestMethod]
        public void ReturnsIndexWithWarningWithNullParent() => BaseReturnsIndexWithWarningWithNullParent(Repo, Controller);

        [TestMethod]
        public void ReturnsIndexWithWarningWithUnknownParentID() => BaseReturnsIndexWithWarningWithUnknownParentID(Repo, Controller);

        [TestMethod]
        public void ReturnsDetailWithWarningIfAttachingNullChild() => BaseReturnsDetailWithWarningIfAttachingNullChild(Person, Repo, Controller);

        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownChildID() => BaseReturnsDetailWithWarningWithUnknownChildID(Person, Repo, Controller);

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


        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild() =>
            BaseReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild(Repo, Controller, Person.ID);



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
        public void SuccessfullyAttachRecipeChild()
        {
            BaseSuccessfullyAttachRecipeChild(Person, Controller);
        }

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachRecipeChild()
        {

        }

        [TestMethod]
        public void SuccessfullyAttachMenuChild() => BaseSuccessfullyAttachMenuChild(Person, Controller);



        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachMenuChild()
        {
            IGenericController<Person> DetachController = new PersonsController(Repo);
            BaseSuccessfullyDetachMenuChild(Repo, Controller, DetachController, UIControllerType.Persons);
        }

        [TestMethod]
        public void SuccessfullyAttachPlanChild()
        {
            BaseSuccessfullyAttachPlanChild(Person, Controller);
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

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void SuccessfullyDetachFirstIngredientChild()
        {
            IGenericController<Person> DetachController = new PersonsController(Repo);
            BaseSuccessfullyDetachIngredientChild(Repo, Controller, DetachController, UIControllerType.ShoppingLists, 0);
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachASetOfIngredientChildren()
        {
            // Arrange 
            Person.Ingredients.Add(new Ingredient { ID = 4005, Name = "Butter" });
            Person.Ingredients.Add(new Ingredient { ID = 4006, Name = "Cayenne Pepper" });
            Person.Ingredients.Add(new Ingredient { ID = 4007, Name = "Cheese" });
            Person.Ingredients.Add(new Ingredient { ID = 4008, Name = "Chopped Green Pepper" });
            Repo.Save((Person)Person);
            int initialIngredientCount = Person.Ingredients.Count();

            // Act
            var setToSelect = new HashSet<int> { 4006, 4008 };
            List<Ingredient> selected = Person.Ingredients.Where(t => setToSelect.Contains(t.ID)).ToList();
            Controller.DetachAllIngredients(Person.ID, selected);
            Person returnedPerson = Repo.GetById(Person.ID);

            // Assert
            Assert.AreEqual(initialIngredientCount - 2, returnedPerson.Ingredients.Count());
        }

        [TestMethod]
        public void DetachTheLastIngredientChild()
        {
            BaseDetachTheLastIngredientChild(Repo, Controller, Person);
        }

        [TestMethod]
        public void DetachAllIngredientChildren()
        {
            BaseDetachAllIngredientChildren(Repo, Controller, Person);
        }


    }
}

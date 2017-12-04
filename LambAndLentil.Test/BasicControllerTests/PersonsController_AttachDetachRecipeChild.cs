using System;
using System.Collections.Generic;
using System.Linq;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class PersonsController_AttachDetachRecipeChild : PersonsController_Test_Should
    {
       
          [Ignore]
        [TestMethod]
        public void SuccessfullyAttachRecipeChild()
        {
             IGenericController<Person> DetachController = (IGenericController<Person>)(new PersonsController(Repo));
            BaseSuccessfullyDetachRecipeChild(Repo, Controller, DetachController, UIControllerType.Persons);
        }



        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachFirstRecipeChild()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

       
        [TestMethod]
        public void SuccessfullyDetachASetOfRecipeChildren()
        {
            // Arrange 
            Person.Recipes.Add(new Recipe { ID = 4005, Name = "Butter" });
            Person.Recipes.Add(new Recipe { ID = 4006, Name = "Cayenne Pepper" });
            Person.Recipes.Add(new Recipe { ID = 4007, Name = "Cheese" });
            Person.Recipes.Add(new Recipe { ID = 4008, Name = "Chopped Green Pepper" });
            Repo.Save((Person)Person);
            int initialRecipeCount = Person.Recipes.Count();

            // Act
            var setToSelect = new HashSet<int> { 4006, 4008 };
            List<Recipe> selected = Person.Recipes.Where(t => setToSelect.Contains(t.ID)).ToList();
            Controller.DetachAllRecipes(Person.ID, selected);
            Person returnedPerson = Repo.GetById(Person.ID);

            // Assert
            Assert.AreEqual(initialRecipeCount - 2, returnedPerson.Recipes.Count());
        }

        [TestMethod]
        public void SuccessfullyDetachtheLastRecipeChild() => BaseDetachTheLastRecipeChild(Repo, Controller, Person);
       

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachAllRecipeChildren()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWithWarningIfAttachingNullRecipeChild() { }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownRecipeChildID() { }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild() { }
    }
}

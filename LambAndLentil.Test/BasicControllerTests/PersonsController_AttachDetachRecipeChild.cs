﻿using System;
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
             IGenericController<Person> DetachController = new PersonsController(Repo);
            BaseSuccessfullyDetachRecipeChild( Controller, DetachController);
        }



        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachFirstRecipeChild()
        { 
            Assert.Fail();
        }

       
        [TestMethod]
        public void SuccessfullyDetachASetOfRecipeChildren()
        { 
            Person.Recipes.Add(new Recipe { ID = 4005, Name = "Butter" });
            Person.Recipes.Add(new Recipe { ID = 4006, Name = "Cayenne Pepper" });
            Person.Recipes.Add(new Recipe { ID = 4007, Name = "Cheese" });
            Person.Recipes.Add(new Recipe { ID = 4008, Name = "Chopped Green Pepper" });
            Repo.Save((Person)Person);
            int initialRecipeCount = Person.Recipes.Count();
             
            var setToSelect = new HashSet<int> { 4006, 4008 };
            List<Recipe> selected = Person.Recipes.Where(t => setToSelect.Contains(t.ID)).ToList();
            Controller.DetachASetOf(Person.ID, selected);
            Person returnedPerson = Repo.GetById(Person.ID);
             
            Assert.AreEqual(initialRecipeCount - 2, returnedPerson.Recipes.Count());
        }

        [TestMethod]
        public void SuccessfullyDetachtheLastRecipeChild() => BaseDetachTheLastRecipeChild(Repo, Controller, Person);
       

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachAllRecipeChildren()
        { 
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

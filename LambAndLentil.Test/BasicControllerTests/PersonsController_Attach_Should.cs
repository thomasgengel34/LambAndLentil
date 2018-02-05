﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{

    [TestClass]
    public class PersonsController_Attach_Should : PersonsController_Test_Should
    {
        [Ignore]
        [TestMethod]
        public void ReturnsErrorWithUnknownRepository() =>
           
            Assert.Fail();
         
        [TestMethod]
        public void ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild()
        { 
            Person person = new Person
            {
                ID = int.MaxValue,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };
            IRepository<Person> mRepo = new TestRepository<Person>();
            mRepo.Save(person);
            Ingredient ingredient = new Ingredient
            {
                ID = 1492,
                Description = "test ReturnsDetailWhenAttachingWithSuccessWithValidParentandValidChild"
            };

            
            ActionResult ar = Controller.Attach(person, ingredient );
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            RedirectToRouteResult rdr = (RedirectToRouteResult)adr.InnerResult;

           
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual("Ingredient was Successfully Attached!", adr.Message);
            Assert.AreEqual(int.MaxValue, rdr.RouteValues.ElementAt(0).Value);
            Assert.AreEqual("Edit", rdr.RouteValues.ElementAt(1).Value.ToString());
            Assert.AreEqual("Details", rdr.RouteValues.ElementAt(2).Value.ToString());
        }

         


        [TestMethod]
        public void SuccessfullyAttachChild()
        { 
            Ingredient child = new Ingredient() { ID = 3000, Name = "SuccessfullyAttachChild" };
            TestRepository<Ingredient> IngredientRepo = new TestRepository<Ingredient>();
            IngredientRepo.Save(child);
             
            Controller.Attach(Person, child );
            ReturnedPerson = Repo.GetById(Person.ID);
          
            Assert.AreEqual("SuccessfullyAttachChild", ReturnedPerson.Ingredients.Last().Name);
        }
         

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachRecipeChild()
        {

        }

        //[TestMethod]
        //public void SuccessfullyAttachRecipeChild() => BaseSuccessfullyAttachChild(Person, Controller);

  

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
        public void SuccessfullyDetachIngredientChild()
        {

        }

        [Ignore]
        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void SuccessfullyDetachFirstIngredientChild()
        {
            //IGenericController<Person> DetachController = (IGenericController<Person>)(new PersonsController(Repo));
            //BaseSuccessfullyDetachChild(Repo, Controller, DetachController, UIControllerType.ShoppingLists, 0);
        }

        [TestMethod]
        [TestCategory("Attach-Detach")]
        public void DetachASetOfIngredientChildren()
        { 
            //Person.Ingredients.Add(new Ingredient { ID = 4005, Name = "Butter" });
            //Person.Ingredients.Add(new Ingredient { ID = 4006, Name = "Cayenne Pepper" });
            //Person.Ingredients.Add(new Ingredient { ID = 4007, Name = "Cheese" });
            //Person.Ingredients.Add(new Ingredient { ID = 4008, Name = "Chopped Green Pepper" });
            //Repo.Save((Person)Person);
            //int initialIngredientCount = Person.Ingredients.Count();
             
            //var setToSelect = new HashSet<int> { 4006, 4008 };
            //List<IEntity> selected = Person.Ingredients.Where(t => setToSelect.Contains(t.ID)).ToList();
            //Controller.DetachASetOf(Person, selected);
            //Person returnedPerson = Repo.GetById(Person.ID);
             
            //Assert.AreEqual(initialIngredientCount - 2, returnedPerson.Ingredients.Count());
        }
         
    }
}

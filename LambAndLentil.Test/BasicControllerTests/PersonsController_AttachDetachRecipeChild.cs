using System;
using System.Collections.Generic;
using System.Linq;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace  LambAndLentil.Test.BaseControllerTests
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    internal class PersonsController_AttachDetachRecipeChild : PersonsController_Test_Should
    {
         
       
        [TestMethod]
        public void SuccessfullyDetachASetOfRecipeChildren()
        { 
            //Person.Recipes.Add(new Recipe { ID = 4005, Name = "Butter" });
            //Person.Recipes.Add(new Recipe { ID = 4006, Name = "Cayenne Pepper" });
            //Person.Recipes.Add(new Recipe { ID = 4007, Name = "Cheese" });
            //Person.Recipes.Add(new Recipe { ID = 4008, Name = "Chopped Green Pepper" });
            //repo.Save((Person)Person);
            //int initialRecipeCount = Person.Recipes.Count();
             
            //var setToSelect = new HashSet<int> { 4006, 4008 };
            //List<Recipe> selected = Person.Recipes.Where(t => setToSelect.Contains(t.ID)).ToList();
            //controller.DetachASetOf(Person, selected);
            //Person returnedPerson = repo.GetById(Person.ID);
             
            //Assert.AreEqual(initialRecipeCount - 2, returnedPerson.Recipes.Count());
        }
           
    }
}

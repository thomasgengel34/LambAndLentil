using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace  LambAndLentil.Test.BaseControllerTests
{
    [TestClass]
    [TestCategory("Attach-Detach")]
   internal class PersonsController_AttachDetachShoppingListChild : PersonsController_Test_Should
    {
  
        [TestMethod]
        public void SuccessfullyDetachASetOfShoppingListChildren()
        { 
            //// Arrange 
            //Person.ShoppingLists.Add(new ShoppingList { ID = 4005, Name = "Butter" });
            //Person.ShoppingLists.Add(new ShoppingList { ID = 4006, Name = "Cayenne Pepper" });
            //Person.ShoppingLists.Add(new ShoppingList { ID = 4007, Name = "Cheese" });
            //Person.ShoppingLists.Add(new ShoppingList { ID = 4008, Name = "Chopped Green Pepper" });
            //repo.Save((Person)Person);
            //int initialShoppingListCount = Person.ShoppingLists.Count();

            //// Act
            //var setToSelect = new HashSet<int> { 4006, 4008 };
            //List<IEntity> selected = Person.ShoppingLists.Where(t => setToSelect.Contains(t.ID)).ToList();
            //controller.DetachASetOf(Person, selected);
            //Person returnedPerson = repo.GetById(Person.ID);

            //// Assert
            //Assert.AreEqual(initialShoppingListCount - 2, returnedPerson.ShoppingLists.Count());
            
        }
         
         
    }
}

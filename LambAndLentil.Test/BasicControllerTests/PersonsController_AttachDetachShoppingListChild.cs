using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class PersonsController_AttachDetachShoppingListChild : PersonsController_Test_Should
    {

        [Ignore]
        [TestMethod]
        public void SuccessfullyAttachShoppingListChild()
        {
            // Arrange
            ShoppingList child = new ShoppingList() { ID = 3000, Name = "SuccessfullyAttachShoppingListChild" };
            TestRepository<ShoppingList> ShoppingListRepo = new TestRepository<ShoppingList>();
            ShoppingListRepo.Save(child);

            // Act
            Controller.AttachShoppingList(Person.ID, child);
            ReturnedPerson = Repo.GetById(Person.ID);
            // Assert
            //  Assert.AreEqual("Default", ShoppingList.ShoppingLists.Last().Name);
            Assert.AreEqual("SuccessfullyAttachShoppingListChild", ReturnedPerson.ShoppingLists.Last().Name);
        }

           [Ignore]
        [TestMethod]
        public void SuccessfullyDetachFirstShoppingListChild()
        {
           
        }

      
        [TestMethod]
        public void SuccessfullyDetachASetOfShoppingListChildren()
        { 
            // Arrange 
            Person.ShoppingLists.Add(new ShoppingList { ID = 4005, Name = "Butter" });
            Person.ShoppingLists.Add(new ShoppingList { ID = 4006, Name = "Cayenne Pepper" });
            Person.ShoppingLists.Add(new ShoppingList { ID = 4007, Name = "Cheese" });
            Person.ShoppingLists.Add(new ShoppingList { ID = 4008, Name = "Chopped Green Pepper" });
            Repo.Save((Person)Person);
            int initialShoppingListCount = Person.ShoppingLists.Count();

            // Act
            var setToSelect = new HashSet<int> { 4006, 4008 };
            List<ShoppingList> selected = Person.ShoppingLists.Where(t => setToSelect.Contains(t.ID)).ToList();
            Controller.DetachAllShoppingLists(Person.ID, selected);
            Person returnedPerson = Repo.GetById(Person.ID);

            // Assert
            Assert.AreEqual(initialShoppingListCount - 2, returnedPerson.ShoppingLists.Count());
            
        }

        [TestMethod]
        public void  DetachtheLastShoppingListChild() => BaseDetachTheLastShoppingListChild(Repo, Controller, Person);

        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachAllShoppingListChildren()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWithWarningIfAttachingNullShoppingListChild() { }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownShoppingListChildID() { }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild() { }
    }
}

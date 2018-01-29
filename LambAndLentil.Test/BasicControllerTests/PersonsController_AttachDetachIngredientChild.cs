using System.Collections.Generic;
using System.Linq;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class PersonsController_AttachDetachChild : PersonsController_Test_Should
    {
       
          
        [TestMethod]
        public void SuccessfullyAttachChild()
        { 
            Ingredient child = new Ingredient() { ID = 3000, Name = "SuccessfullyAttachChild" };
            TestRepository<Ingredient> IngredientRepo = new TestRepository<Ingredient>();
            IngredientRepo.Save(child);
             
            Controller.Attach(Person.ID, child );
            ReturnedPerson = Repo.GetById(Person.ID);
         
            Assert.AreEqual("SuccessfullyAttachChild", ReturnedPerson.Ingredients.Last().Name);
        }



        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachFirstIngredientChild()
        { 
            Assert.Fail();
        }

       
        [TestMethod]
        public void SuccessfullyDetachASetOfIngredientChildren()
        { 
            Person.Ingredients.Add(new Ingredient { ID = 4005, Name = "Butter" });
            Person.Ingredients.Add(new Ingredient { ID = 4006, Name = "Cayenne Pepper" });
            Person.Ingredients.Add(new Ingredient { ID = 4007, Name = "Cheese" });
            Person.Ingredients.Add(new Ingredient { ID = 4008, Name = "Chopped Green Pepper" });
            Repo.Save((Person)Person);
            int initialIngredientCount = Person.Ingredients.Count();

            // Act
            var setToSelect = new HashSet<int> { 4006, 4008 };
            List<Ingredient> selected = Person.Ingredients.Where(t => setToSelect.Contains(t.ID)).ToList();
            Controller.DetachASetOf(Person.ID, selected);
            Person returnedPerson = Repo.GetById(Person.ID);

            // Assert
            Assert.AreEqual(initialIngredientCount - 2, returnedPerson.Ingredients.Count());
        }
         

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWithWarningIfAttachingNullIngredientChild() { }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWithWarningWithUnknownIngredientChildID() { }

        [Ignore]
        [TestMethod]
        public void ReturnsDetailWhenDetachingWithSuccessWithValidParentandValidIngredientChild() { }
    }
}

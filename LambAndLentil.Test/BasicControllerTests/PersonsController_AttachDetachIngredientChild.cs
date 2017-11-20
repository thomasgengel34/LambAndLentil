using System.Collections.Generic;
using System.Linq;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("Attach-Detach")]
    public class PersonsController_AttachDetachIngredientChild : PersonsController_Test_Should
    {
       
          
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



        [Ignore]
        [TestMethod]
        public void SuccessfullyDetachFirstIngredientChild()
        {
            // Arrange

            // Act

            //Assert
            Assert.Fail();
        }

       
        [TestMethod]
        public void SuccessfullyDetachASetOfIngredientChildren()
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
        public void SuccessfullyDetachtheLastIngredientChild()
        {
            BaseDetachTheLastIngredientChild(Repo, Controller, Person);
        }

        
        [TestMethod]
        public void  DetachAllIngredientChildren()=> 
            BaseDetachAllIngredientChildren(Repo, Controller, Person); 

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

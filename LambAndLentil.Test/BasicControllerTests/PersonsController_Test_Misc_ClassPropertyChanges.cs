using LambAndLentil.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("PersonsController")]
    [TestCategory("Misc")]
    public class PersonsController_ClassPropertyChanges : PersonsController_Test_Should
    {
        public Person Entity { get; set; }
        public Person ReturnedEntity { get; set; }

        public PersonsController_ClassPropertyChanges()
        {
           Entity = new Person {
               ID = 1000,
               Name = "Original Name",
               Description = "Description has changed" 
        };

            Repo.Save(Entity);
        }

        [TestMethod]
        public void ShouldEditFirstNameOnlyAndFullNameIsChangedButLastNameIsNot()
        { 
            Entity.FirstName = "Changed";
             
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(1000); 

            Assert.AreEqual("Changed Created", ReturnedEntity.Name);
            // Name should always be fed from and equal to FullName
            Assert.AreEqual("Changed Created", ReturnedEntity.FullName);
            Assert.AreEqual("Changed", ReturnedEntity.FirstName);
            Assert.AreEqual("Created", ReturnedEntity.LastName);

        }

        [TestMethod]
        public void ShouldEditLastNameOnlyAndFullNameIsChangedBuFirstNameIsNot()
        { 
            Entity.LastName = "Altered";
             
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(1000);
             
            Assert.AreEqual("Newly Altered", ReturnedEntity.Name);
            // Name should always be fed from and equal to FullName
            Assert.AreEqual("Newly Altered", ReturnedEntity.FullName);
            Assert.AreEqual("Newly", ReturnedEntity.FirstName);
            Assert.AreEqual("Altered", ReturnedEntity.LastName);

        }

        [TestMethod]
        public void ShouldEditFirstAndLastNamesAndFullNameIsChanged()
        { 
            Entity.FirstName = "Changed";
            Entity.LastName = "Altered";
             
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(1000);
             
            Assert.AreEqual("Changed Altered", ReturnedEntity.Name);
            // Name should always be fed from and equal to FullName
            Assert.AreEqual("Changed Altered", ReturnedEntity.FullName);
            Assert.AreEqual("Changed", ReturnedEntity.FirstName);
            Assert.AreEqual("Altered", ReturnedEntity.LastName);

        }

        [TestMethod]
        public void ShouldEditNameAndFirstNameAndLastNameChangeToEmptyStrings()
        { 
            Entity.Name = "Changed";
             
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(1000);
             
            Assert.AreEqual("Changed", ReturnedEntity.Name);
            // Name should always be fed from and equal to FullName
            Assert.AreEqual("Changed", ReturnedEntity.FullName);
            Assert.AreEqual("", ReturnedEntity.FirstName);
            Assert.AreEqual("", ReturnedEntity.LastName);
        }

        [TestMethod]
        public void ShouldEditFullNameAndFirstNameAndLastNameChangeToEmptyStrings()
        { 
            Entity.FullName = "Changed";
             
            Controller.PostEdit(Entity);
            ReturnedEntity = Repo.GetById(1000);
             
            Assert.AreEqual("Changed", ReturnedEntity.Name);
            // Name should always be fed from and equal to FullName
            Assert.AreEqual("Changed", ReturnedEntity.FullName);
            Assert.AreEqual("", ReturnedEntity.FirstName);
            Assert.AreEqual("", ReturnedEntity.LastName);
        }
          

        public void ShouldEditMaxCalories()
        { 
            Assert.Fail();
        }

        public void ShouldEditMinCalories()
        { 
            Assert.Fail();
        }

        public void ShouldEditWeight()
        { 
            Assert.Fail();
        }

        public void WeightCannotBeLessThanZero()
        { 
            Assert.Fail();
        }

        public void WeightCannotBeMoreThanOneThousand()
        { 
            Assert.Fail();
        }

        public void CanEditNoGarlic()
        { 
            Assert.Fail();
        }
    }
}

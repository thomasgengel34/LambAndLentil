using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.Domain.Entities;
using System.Web.Mvc;

namespace LambAndLentil.Test.BaseControllerTests
{
    [TestCategory("PersonsController")]
    [TestCategory("PostEdit")] 
    [TestClass]
    internal class PersonsController_PostEdit_Should:PersonsController_Test_Should
    {
       
        [TestMethod] 
        public void CanEditPerson2()
        { 
            Person person = new Person
            {
                ID = 1,
                FirstName = "test PersonControllerTest.CanEditPerson",
                LastName = "",
                Description = "test PersonControllerTest.CanEditPerson"
            };
            repo.Save(person);
             
            person.FirstName = "Name has been changed";
            person.LastName = ""; 
           ActionResult view1 = controller.PostEdit(person);

            Person returnedPerson = repo.GetById(person.ID);
             
            Assert.IsNotNull(view1);
            Assert.AreEqual("Name has been changed ", returnedPerson.Name);
            //Assert.AreEqual(person.Description, returnedPersonListEntity.Description);
            //Assert.AreEqual(person.CreationDate, returnedPersonListEntity.CreationDate);
        }

    
    }
} 

using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.Test.BasicControllerTests
{
    [TestClass]
    [TestCategory("PersonsController")]
    [TestCategory("Misc")]
    public class PersonsController_Test_Should_Misc : PersonsController_Test_Should
    {
        [TestMethod]
        public void InheritsFromBaseControllerCorrectly()
        {
            // Arrange

            // Act 
            Controller.PageSize = 4;

            var type = typeof(PersonsController);
            var DoesDisposeExist = type.GetMethod("Dispose");

            // Assert 
            Assert.AreEqual(4, Controller.PageSize);
            Assert.IsNotNull(DoesDisposeExist);
        }

        [TestMethod]
        public void IsPublic()
        {
            // Arrange


            // Act
            Type type = Controller.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void Index()
        {
            // Arrange
            PersonsController Controller2 = new PersonsController(Repo);

            // Act
            ViewResult view1 = Controller.Index(1) as ViewResult;
            ViewResult view2 = Controller2.Index(2) as ViewResult;

            // Assert
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void ContainsAllPersons()
        {
            // Arrange 
            var Controller2 = new PersonsController(Repo);

            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<Person>)(view1.Model)).ListT.Count();

            ViewResult view2 = Controller2.Index(2);

            int count2 = (( ListEntity<Person>)(view2.Model)).ListT.Count();

            int count = count1 + count2;

            // Assert
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
            Assert.AreEqual(5, count1);
            Assert.AreEqual(0, count2);
            Assert.AreEqual(5, count);
            Assert.AreEqual("Index", view1.ViewName);
            Assert.AreEqual("Index", view2.ViewName);

            Assert.AreEqual("PersonsController_Index_Test P1 ", (( ListEntity<Person>)(view1.Model)).ListT.FirstOrDefault().Name);
            Assert.AreEqual("PersonsController_Index_Test P2 ", (( ListEntity<Person>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual("PersonsController_Index_Test P3 ", (( ListEntity<Person>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);
            Assert.AreEqual("PersonsController_Index_Test P5 ", (( ListEntity<Person>)(view1.Model)).ListT.Skip(4).FirstOrDefault().Name);

        }


        [TestMethod]
        [TestCategory("Index")]
        public void FirstPageIsCorrect()
        {
            // Arrange 
            Controller.PageSize = 8;


            // Act
            ViewResult view1 = Controller.Index(1);

            int count1 = (( ListEntity<Person>)(view1.Model)).ListT.Count();

            // Assert
            Assert.IsNotNull(view1);
            Assert.AreEqual(5, count1);
            Assert.AreEqual("Index", view1.ViewName);

            Assert.AreEqual("PersonsController_Index_Test P1 ", (( ListEntity<Person>)(view1.Model)).ListT.FirstOrDefault().Name);
            Assert.AreEqual("PersonsController_Index_Test P2 ", (( ListEntity<Person>)(view1.Model)).ListT.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual("PersonsController_Index_Test P3 ", (( ListEntity<Person>)(view1.Model)).ListT.Skip(2).FirstOrDefault().Name);


        }

        [Ignore] // currently we only have one page here
        [TestMethod]
        [TestCategory("Index")]
        public void SecondPageIsCorrect()
        {
            // Arrange

            //// Act

            //// Assert 
        }

        [TestMethod]
        [TestCategory("Index")]
        public void Index_CanSendPaginationViewModel()
        {
            // Arrange 

            // Act 
             ListEntity<Person> resultT = ( ListEntity<Person>)((ViewResult)Controller.Index(2)).Model;

            // Assert
            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(5, pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void PagingInfoIsCorrect()
        {
            // Arrange 

            // Action
            int totalItems = (( ListEntity<Person>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = (( ListEntity<Person>)((ViewResult)Controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = (( ListEntity<Person>)((ViewResult)Controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = (( ListEntity<Person>)((ViewResult)Controller.Index()).Model).PagingInfo.TotalPages;

            // Assert
            Assert.AreEqual(5, totalItems);
            Assert.AreEqual(1, currentPage);
            Assert.AreEqual(8, itemsPerPage);
            Assert.AreEqual(1, totalPages);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void CanPaginate()
        {
            // Arrange

            // Act
            var result = ( ListEntity<Person>)(Controller.Index(1)).Model;
            var ListEntity= result.ListT;

            // Assert 
            Assert.IsTrue(ListEntity.Count() == 5);
            Assert.AreEqual("PersonsController_Index_Test P1 ", ListEntity.FirstOrDefault().Name);
            Assert.AreEqual("PersonsController_Index_Test P3 ", ListEntity.Skip(2).FirstOrDefault().Name);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipeIDIsNegative()
        {
            // Arrange

            // Act
            ViewResult view = Controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Person was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Details")]
        public void WorksWithValidRecipeID()
        {
            // Arrange 

            // Act 
            ActionResult ar = Controller.Details(int.MaxValue);
            AlertDecoratorResult adr = (AlertDecoratorResult)ar;
            ViewResult view = (ViewResult)adr.InnerResult;

            // Assert
            Assert.IsNotNull(view);

            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(Person));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipeIDTooHigh()
        {
            // Arrange

            // Act
            ActionResult view = Controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Person was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Details")]
        public void RecipeIDPastIntLimit()
        {
            // Arrange

            // Act
            ViewResult result = Controller.Details(Int16.MaxValue + 1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void DetailsRecipeIDIsZero()
        {
            // Arrange 

            // Act
            ViewResult view = Controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No Person was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Create")]
        public void Create()
        {
            // Arrange

            // Act         
            ViewResult view = Controller.Create(UIViewType.Edit);

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteAFoundPerson()
        {
            // Arrange 

            // Act  
            ActionResult ar = Controller.Delete(int.MaxValue - 1);
            string viewName = ((ViewResult)ar).ViewName;

            // Assert
            Assert.IsNotNull(ar);
            Assert.AreEqual(UIViewType.Details.ToString(), viewName);
        }



        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteAnInvalidPerson()
        {
            // Arrange 
            var view = Controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Person was not found", adr.Message);
            Assert.AreEqual("alert-warning", adr.AlertClass);
            Assert.AreEqual(UIViewType.Index.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void DeleteConfirmed()
        {
            // Arrange
            Person person = new Person { ID = 1 };
            Repo.Add(person);
            // Act
            ActionResult result = Controller.DeleteConfirmed(1) as ActionResult;
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { Controller = "Persons", Action = "Index" } } );
            // Assert 
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void CanDeleteValidPerson()
        {

            // Arrange  
            int initialCount = Repo.Count();
            Person person = new Person("John", "Doe") { ID = 100, Description = "test CanDeleteValidPerson" };
            Repo.Add(person);
            int newCount = Repo.Count();

            // Act - delete the person
            ActionResult result = Controller.DeleteConfirmed(person.ID);

            AlertDecoratorResult adr = (AlertDecoratorResult)result;

            // Assert
            Assert.AreEqual("John Doe has been deleted", adr.Message);
            Assert.AreEqual(initialCount, newCount - 1);
            Assert.AreEqual(initialCount, Repo.Count());
        }

        [Ignore]   // brought in Ingredient edit methods instead of using this
        [TestMethod]
        [TestCategory("Edit")]
        public void EditPerson()
        {
            // Arrange
            var Controller2 = new PersonsController(Repo);
            Person pVM = new Person("Kermit", "Frog") { ID = 1492, Description = "test CanEditPerson" };

            // Act  
            ViewResult view1 = (ViewResult)Controller.Edit(1492);
            Person p1 = (Person)view1.Model;
            ViewResult view2 = (ViewResult)Controller2.Edit(2);
            Person p2 = (Person)view2.Model;



            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual(1, p1.ID);
            Assert.AreEqual(2, p2.ID);

            Assert.AreEqual("First edited", p1.Name);
            Assert.AreEqual("Old Name 2", p2.Name);
        }


        [TestMethod]
        public void PostEditPersonsFullName()
        {
            // Arrange
            Person person = new Person { ID = 1000, FirstName = "Jon", LastName = "Johns", Description = "PostEditPersonsFullName" };
            Repo.Add(person);

            // Act
            person.FirstName = "Reynard";
            person.LastName = "Finkelstein";
            Controller.PostEdit(person);
            Repo.Add(person);
            Person newPerson = Repo.GetById(1000);
            //Assert
            Assert.AreEqual("Reynard Finkelstein", person.Name);
        }

        [TestMethod]
        public void PostEditFirstName()
        {
            // Arrange
            Person person = new Person { ID = 1001, FirstName = "Jon", LastName = "Johns", Description = "PostEditPersonsFirstName" };
            Repo.Add(person);

            // Act
            person.FirstName = "Reynard"; 
            Controller.PostEdit(person);
            Repo.Add(person);
            Person newPerson = Repo.GetById(1000);
            //Assert
            Assert.AreEqual("Reynard Johns", person.Name);
        }
  

        [TestMethod]
        public void PostEditLastName()
        {
            // Arrange
            Person person = new Person { ID = 1001, FirstName = "Jon", LastName = "Johns", Description = "PostEditPersonsLastName" };
            Repo.Add(person);

            // Act
            person.LastName = "Luc";
            Controller.PostEdit(person);
            Repo.Add(person);
            Person newPerson = Repo.GetById(1000);
            //Assert
            Assert.AreEqual("Jon Luc", person.Name);
        }

   

        [TestMethod]
        [TestCategory("Edit")]
        public void SaveEditedPerson()
        {
            // Arrange
            PersonsController indexController = new PersonsController(Repo);
            PersonsController Controller2 = new PersonsController(Repo);
            PersonsController Controller3 = new PersonsController(Repo);


            Person person = new Person
            {
                FirstName = "0000 test",
                LastName="",
                ID = int.MaxValue - 100,
                Description = "test PersonsControllerShould.SaveEditedPerson"
            };

            // Act 
            ActionResult ar1 = Controller.PostEdit(person);


            // now edit it
            person.FirstName = "0000 test Edited";
            person.LastName = "";
            person.ID = 7777;
            ActionResult ar2 = Controller2.PostEdit(person);
            ViewResult view2 = Controller3.Index();
             ListEntity<Person> ListEntity2 = ( ListEntity<Person>)view2.Model;
            Person person3 = (from m in ListEntity2.ListT
                          where m.ID == 7777
                          select m).AsQueryable().FirstOrDefault();

            // Assert
            Assert.AreEqual("0000 test Edited ", person3.Name);
            Assert.AreEqual(7777, person3.ID);

        }

        [Ignore]  // look into why this is not working
        [TestMethod]
        [TestCategory("Edit")]
        public void CanPostEditPerson()
        {
            // Arrange
            Person person = new Person
            {
                ID = 1,
                 FirstName = "test PersonControllerTest.CanEditPerson",
                LastName="",
                Description = "test PersonControllerTest.CanEditPerson"
            };
            Repo.Add(person);

            // Act 
            person.FirstName = "Name has been changed";
            Repo.Add(person);
            ViewResult view1 = (ViewResult)Controller.Edit(1);

            Person returnedPersonListEntity = Repo.GetById(1);

            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual("Name has been changed", returnedPersonListEntity.Name);
            Assert.AreEqual(person.Description, returnedPersonListEntity.Description);
            Assert.AreEqual(person.CreationDate, returnedPersonListEntity.CreationDate);
        }



        [TestMethod]
        [TestCategory("Edit")]
        public void CannotEditNonexistentPerson()
        {
            // Arrange

            // Act
            Person result = (Person)((ViewResult)Controller.Edit(8)).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }



        [TestMethod]
        public void CreateReturnsNonNull()
        {
            // Arrange 

            // Act
            ViewResult result = Controller.Create(UIViewType.Create) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }





        [Ignore]
        [TestMethod]
        public void UnAssignAnIngredientFromAPerson()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestCategory("Copy")]
        [TestMethod]
        public void CopyModifySaveWithANewName()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void CorrectPersonsAreBoundInEdit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetTheClassNameCorrect()
        {
            // Arrange

            // Act


            // Assert
            //  Assert.Fail();
            Assert.AreEqual("LambAndLentil.UI.Controllers.PersonsController", PersonsController_Test_Should.Controller.ToString());
        }

    }
}

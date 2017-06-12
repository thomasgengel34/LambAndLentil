using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambAndLentil.UI.Controllers;
using Moq;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.UI.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using LambAndLentil.Domain.Entities;
using AutoMapper;
using LambAndLentil.Tests.Infrastructure;
using System.Linq;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI;

namespace LambAndLentil.Tests.Controllers
{
    [TestClass]
    [TestCategory("PersonsController")]
    public class PersonsControllerTest

    {
        static Mock<IRepository> mock;
        public static MapperConfiguration AutoMapperConfig { get; set; }
        public PersonsControllerTest()
        {
            //AutoMapperConfig = AutoMapperConfigForTests.AMConfigForTests();

        }

        [TestMethod]
        public void PersonsCtr_InheritsFromBaseControllerCorrectly()
        {


            // Arrange
            PersonsController controller = SetUpSimpleController();
            // Act 
            controller.PageSize = 4;

            var type = typeof(PersonsController);
            var DoesDisposeExist = type.GetMethod("Dispose");

            // Assert 
            Assert.AreEqual(4, controller.PageSize);
            Assert.IsNotNull(DoesDisposeExist);
        }

        [TestMethod]
        public void PersonsCtr_IsPublic()
        {
            // Arrange
            PersonsController testController = SetUpSimpleController();

            // Act
            Type type = testController.GetType();
            bool isPublic = type.IsPublic;

            // Assert 
            Assert.AreEqual(isPublic, true);
        }




        [TestMethod]
        [TestCategory("Index")]
        public void PersonsCtr_Index()
        {
            // Arrange
            PersonsController controller = SetUpController();

            // Act
            ViewResult result = controller.Index(1) as ViewResult;
            ViewResult result1 = controller.Index(2) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result1);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void PersonsCtr_Index_ContainsAllPersons()
        {
            // Arrange
            PersonsController controller = SetUpController();
            ListVM ilvm = new ListVM();
            ilvm.Persons = (IEnumerable<Person>)mock.Object.Persons;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).Persons.Count();

            ViewResult view2 = controller.Index(2);

            int count2 = ((ListVM)(view2.Model)).Persons.Count();

            int count = count1 + count2;

            // Assert
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
            Assert.AreEqual(5, count1);
            Assert.AreEqual(0, count2);
            Assert.AreEqual(5, count);
            Assert.AreEqual("Index", view1.ViewName);
            Assert.AreEqual("Index", view2.ViewName);

            //Assert.AreEqual("P1", ((ListVM)(view1.Model)).Persons.FirstOrDefault().Name);
            //Assert.AreEqual("P2", ((ListVM)(view1.Model)).Persons.Skip(1).FirstOrDefault().Name);
            //Assert.AreEqual("P3", ((ListVM)(view1.Model)).Persons.Skip(2).FirstOrDefault().Name);
            //Assert.AreEqual("P5", ((ListVM)(view2.Model)).Persons.FirstOrDefault().Name);

        }


        [TestMethod]
        [TestCategory("Index")]
        public void PersonsCtr_Index_FirstPageIsCorrect()
        {
            // Arrange
            PersonsController controller = SetUpController();
            ListVM ilvm = new ListVM();
            ilvm.Persons = (IEnumerable<Person>)mock.Object.Persons;
            controller.PageSize = 8;

            // Act
            ViewResult view1 = controller.Index(1);

            int count1 = ((ListVM)(view1.Model)).Persons.Count();



            // Assert
            Assert.IsNotNull(view1);
            Assert.AreEqual(5, count1);
            Assert.AreEqual("Index", view1.ViewName);

            Assert.AreEqual("Old Name 1", ((ListVM)(view1.Model)).Persons.FirstOrDefault().Name);
            Assert.AreEqual("Old Name 2", ((ListVM)(view1.Model)).Persons.Skip(1).FirstOrDefault().Name);
            Assert.AreEqual("Old Name 3", ((ListVM)(view1.Model)).Persons.Skip(2).FirstOrDefault().Name);


        }


        [TestMethod]
        [TestCategory("Index")]
        // currently we only have one page here
        public void PersonsCtr_Index_SecondPageIsCorrect()
        {
            //// Arrange
            //PersonsController controller = SetUpController();
            //ListVM ilvm = new ListVM();
            //ilvm.Persons = (IEnumerable<Person>)mock.Object.Persons;

            //// Act
            //ViewResult view  = controller.Index(null, null, null, 2);

            //int count  = ((ListVM)(view.Model)).Persons.Count(); 

            //// Assert
            //Assert.IsNotNull(view);
            //Assert.AreEqual(0, count );
            //Assert.AreEqual("Index", view.ViewName); 
            // Assert.AreEqual("P5", ((ListVM)(view.Model)).Persons.FirstOrDefault().Name);
            // Assert.AreEqual( 5, ((ListVM)(view.Model)).Persons.FirstOrDefault().ID);
            // Assert.AreEqual("C", ((ListVM)(view.Model)).Persons.FirstOrDefault().Maker);
            // Assert.AreEqual("CC", ((ListVM)(view.Model)).Persons.FirstOrDefault().Brand);

        }

        [TestMethod]
        [TestCategory("Index")]
        public void PersonsCtr_Index_CanSendPaginationViewModel()
        {

            // Arrange
            PersonsController controller = SetUpController();

            // Act

            ListVM resultT = (ListVM)((ViewResult)controller.Index(2)).Model;


            // Assert

            PagingInfo pageInfoT = resultT.PagingInfo;
            Assert.AreEqual(2, pageInfoT.CurrentPage);
            Assert.AreEqual(8, pageInfoT.ItemsPerPage);
            Assert.AreEqual(5, pageInfoT.TotalItems);
            Assert.AreEqual(1, pageInfoT.TotalPages);
        }


        [TestMethod]
        [TestCategory("Index")]
        public void PersonsCtr_Index_PagingInfoIsCorrect()
        {
            // Arrange
            PersonsController controller = SetUpController();


            // Action
            int totalItems = ((ListVM)((ViewResult)controller.Index()).Model).PagingInfo.TotalItems;
            int currentPage = ((ListVM)((ViewResult)controller.Index()).Model).PagingInfo.CurrentPage;
            int itemsPerPage = ((ListVM)((ViewResult)controller.Index()).Model).PagingInfo.ItemsPerPage;
            int totalPages = ((ListVM)((ViewResult)controller.Index()).Model).PagingInfo.TotalPages;



            // Assert
            Assert.AreEqual(5, totalItems);
            Assert.AreEqual(1, currentPage);
            Assert.AreEqual(8, itemsPerPage);
            Assert.AreEqual(1, totalPages);
        }

        [TestMethod]
        [TestCategory("Index")]
        public void PersonsCtr_IndexCanPaginate()
        {
            // Arrange
            PersonsController controller = SetUpController();

            // Act
            var result = (ListVM)(controller.Index(1)).Model;



            // Assert
            Person[] ingrArray1 = result.Persons.ToArray();
            Assert.IsTrue(ingrArray1.Length == 5);
            Assert.AreEqual("Old Name 1", ingrArray1[0].Name);
            Assert.AreEqual("Old Name 4", ingrArray1[3].Name);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void PersonsCtr_DetailsRecipeIDIsNegative()
        {
            // Arrange
            PersonsController controller = SetUpController();
            //AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No person was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIControllerType.Persons.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void PersonsCtr_DetailsWorksWithValidRecipeID()
        {
            // Arrange
            PersonsController controller = SetUpController();
            //AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(1) as ViewResult;
            // Assert
            Assert.IsNotNull(view);

            Assert.AreEqual("Details", view.ViewName);
            Assert.IsInstanceOfType(view.Model, typeof(LambAndLentil.UI.Models.PersonVM));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void PersonsCtr_DetailsRecipeIDTooHigh()
        {
            // Arrange
            PersonsController controller = SetUpController();
            //AutoMapperConfigForTests.AMConfigForTests();
            ActionResult view = controller.Details(4000);
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No person was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIControllerType.Persons.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Details")]
        public void PersonsCtr_DetailsRecipeIDPastIntLimit()
        {
            // Arrange
            PersonsController controller = SetUpController();
          //  AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult result = controller.Details(Int16.MaxValue + 1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Details")]
        public void PersonsCtr_DetailsRecipeIDIsZero()
        {
            // Arrange
            PersonsController controller = SetUpController();
            //AutoMapperConfigForTests.AMConfigForTests();


            // Act
            ViewResult view = controller.Details(0) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No person was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIControllerType.Persons.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Create")]
        public void PersonsCtr_Create()
        {
            // Arrange
            PersonsController controller = SetUpController();
            ViewResult view = controller.Create(UIViewType.Edit);


            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("Details", view.ViewName);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void PersonsCtr_DeleteAFoundPerson()
        {
            // Arrange
            PersonsController controller = SetUpController();

            // Act 
            var view = controller.Delete(1) as ViewResult;

            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual(UIViewType.Details.ToString(), view.ViewName);
        }



        [TestMethod]
        [TestCategory("Delete")]
        public void PersonsCtr_DeleteAnInvalidPerson()
        {
            // Arrange
            PersonsController controller = SetUpController();

            // Act 
            var view = controller.Delete(4000) as ViewResult;
            AlertDecoratorResult adr = (AlertDecoratorResult)view;
            // Assert
            Assert.IsNotNull(view);
            Assert.AreEqual("No person was found with that id.", adr.Message);
            Assert.AreEqual("alert-danger", adr.AlertClass);
            Assert.AreEqual(UIControllerType.Persons.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(0).ToString());
            Assert.AreEqual(UIViewType.BaseIndex.ToString(), ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(1).ToString());
            Assert.AreEqual(1, ((RedirectToRouteResult)adr.InnerResult).RouteValues.Values.ElementAt(3));
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void PersonsCtr_DeleteConfirmed()
        {
            // Arrange
            PersonsController controller = SetUpController();
            // Act
            ActionResult result = controller.DeleteConfirmed(1) as ActionResult;
            // improve this test when I do some route tests to return a more exact result
            //RedirectToRouteResult x = new RedirectToRouteResult("default",new  RouteValueDictionary { new Route( { controller = "Persons", Action = "Index" } } );
            // Assert 
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Delete")]
        public void Persons_Ctr_CanDeleteValidPerson()
        {
            // Arrange - create an person
            Person person = new Person { ID = 2, Name = "Test2" };

            // Arrange - create the mock repository
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.Persons).Returns(new Person[]
            {
                new Person {ID=1,Name="Test1"},

                person,

                new Person {ID=3,Name="Test3"},
            }.AsQueryable());
            mock.Setup(m => m.Delete<Person>(It.IsAny<int>())).Verifiable();
            // Arrange - create the controller
            PersonsController controller = new PersonsController(mock.Object);

            // Act - delete the person
            ActionResult result = controller.DeleteConfirmed(person.ID);

            AlertDecoratorResult adr = (AlertDecoratorResult)result;

            // Assert - ensure that the repository delete method was called with a correct Person
            mock.Verify(m => m.Delete<Person>(person.ID));


            Assert.AreEqual("Test2 has been deleted", adr.Message);
        }

        [TestMethod]
        [TestCategory("Edit")]
        public void PersonsCtr_CanEditPerson()
        {
            // Arrange
            PersonsController controller = SetUpController();

            Person person = mock.Object.Persons.First();
            mock.Setup(c => c.Save(person)).Verifiable();
            person.Name = "First edited";

            // Act 

            ViewResult view1 = controller.Edit(1);
            PersonVM p1 = (PersonVM)view1.Model;
            ViewResult view2 = controller.Edit(2);
            PersonVM p2 = (PersonVM)view2.Model;
            ViewResult view3 = controller.Edit(3);
            PersonVM p3 = (PersonVM)view3.Model;


            // Assert 
            Assert.IsNotNull(view1);
            Assert.AreEqual(1, p1.ID);
            Assert.AreEqual(2, p2.ID);
            Assert.AreEqual(3, p3.ID);
            Assert.AreEqual("First edited", p1.Name);
            Assert.AreEqual("Old Name 2", p2.Name);
        }

        

        [TestMethod]
        [TestCategory("Edit")]
        public void PersonsCtr_CannotEditNonexistentPerson()
        {
            //    // Arrange
            //    PersonsController controller = SetUpController();
            //    // Act
            //    Person result = (Person)controller.Edit(8).ViewData.Model;
            //    // Assert
            //    Assert.IsNull(result);
            //}

            //[TestMethod]
            //public void PersonsCtr_CreateReturnsNonNull()
            //{
            //    // Arrange
            //    PersonsController controller = SetUpController();


            //    // Act
            //    ViewResult result = controller.Create(null) as ViewResult;

            //    // Assert
            //    Assert.IsNotNull(result);
        }





        private PersonsController SetUpController()
        {
            // - create the mock repository
            mock = new Mock<IRepository>();
            mock.Setup(m => m.Persons).Returns(new Person[] {
                new Person {ID = 1, Name = "Old Name 1" },
                new Person {ID = 2, Name = "Old Name 2" },
                new Person {ID = 3, Name = "Old Name 3" },
                new Person {ID = 4, Name = "Old Name 4", },
                new Person {ID = 5, Name = "Old Name 5" }
            }.AsQueryable());

            // Arrange - create a controller
            PersonsController controller = new PersonsController(mock.Object);
            controller.PageSize = 3;

            return controller;
        }



        private PersonsController SetUpSimpleController()
        {
            // - create the mock repository
            mock = new Mock<IRepository>();


            // Arrange - create a controller
            PersonsController controller = new PersonsController(mock.Object);
            // controller.PageSize = 3;

            return controller;
        } 
    }
}

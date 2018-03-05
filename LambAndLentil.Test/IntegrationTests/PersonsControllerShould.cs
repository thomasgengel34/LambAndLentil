using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.Tests.Controllers;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace  LambAndLentil.Test.BaseControllerTests
{

    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("PersonController")]
    internal class PersonsControllerShould : BaseControllerTest<Person>
    {
        static IGenericController<Person> Controller,Controller1, Controller2, Controller3, Controller4, Controller5;
        static IPerson Person { get; set; }

        public PersonsControllerShould()
        {
            Person = new Person
            {
                FirstName = "0000",
                LastName = "test",
                Description = "test ControllerShould",
                ID = 1000
            };
            repo = new TestRepository<Person>();
            Controller = new PersonsController(repo);
            Controller1 = new PersonsController(repo);
            Controller2 = new PersonsController(repo);
            Controller3 = new PersonsController(repo);
            Controller4 = new PersonsController(repo);
            Controller5 = new PersonsController(repo);
            repo.Save((Person)Person);
        }

         

         
        [TestMethod]
        public void SaveAValidPerson()
        { 
            AlertDecoratorResult adr = (AlertDecoratorResult)controller.PostEdit((Person)Person);
            RedirectToRouteResult rtrr = (RedirectToRouteResult)adr.InnerResult;

            var routeValues = rtrr.RouteValues.Values;
             
            Assert.AreEqual("alert-success", adr.AlertClass);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual(UIViewType.Index.ToString(), routeValues.ElementAt(0).ToString());
        }
         

         
        internal Person GetPerson(IRepository<Person> repo, string description)
        {
            IGenericController<Person> Controller = new PersonsController(repo);
            IPerson Person = new Person
            {
                Description = description,
                ID = int.MaxValue
            };
            controller.PostEdit((Person)Person);

            Person = ((from m in repo.GetAll()
                              where m.Description == description
                              select m).AsQueryable()).FirstOrDefault();
            return ((Person)Person);
        }
    }
}

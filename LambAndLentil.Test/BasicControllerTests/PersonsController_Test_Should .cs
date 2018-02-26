﻿using AutoMapper;
using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Concrete;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI;
using LambAndLentil.UI.Controllers;
using LambAndLentil.UI.Infrastructure.Alerts;
using LambAndLentil.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace  LambAndLentil.Test.BaseControllerTests
{
    [TestClass]  
    [TestCategory("PersonsController")]
    public class PersonsController_Test_Should:BaseControllerTest<Person>
    {
          
        internal static IPerson Person { get; set; }
         internal static IPerson ReturnedPerson { get; set; }

        public PersonsController_Test_Should()
        {
            controller = new PersonsController(repo)
            {
                PageSize = 3
            };

            Person = new Person()
            {
                ID = 1234567,
                Description = "PersonsController_Test_Should Ctor",
                Ingredients = new List<Ingredient>(),
                Recipes = new List<Recipe>(),
                Menus = new List<Menu>(),
                Plans = new List<Plan>(),
                ShoppingLists=new  List<ShoppingList>(),
                FirstName="Richard",
                LastName="Roe",
                FullName="",
                Name="not modified in controller",
                MaxCalories=5000,
                MinCalories=2000,
                Weight=150,
                NoGarlic=false
            };
            repo.Save((Person)Person);
        } 
    }
}

using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LambAndLentil.UI.Controllers
{ 
    public class PersonsController : PersonsGenericController<Person>
    {
        public PersonsController(IRepository<Person> repository) : base(repository) => Repo = repository;


    } 
}

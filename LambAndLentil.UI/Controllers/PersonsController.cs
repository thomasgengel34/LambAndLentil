using LambAndLentil.Domain.Abstract;
using LambAndLentil.Domain.Entities;
using LambAndLentil.UI.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System;
using AutoMapper;
using Microsoft.Web.Mvc;
using System.Collections.Generic;

namespace LambAndLentil.UI.Controllers
{
    public class PersonsController : BaseController
    { 
        public PersonsController(IRepository repo):base(repo)
        {
            List<Person>persons = repository.Persons.ToList<Person>();
            Persons = new Persons(persons);
        }


        // GET: Persons
        public ViewResult Index(int page=1)
        {
            ViewResult view = BaseIndex(UIControllerType.Persons, page);
            return View(view.ViewName, view.Model);
        }

        // GET: Persons/Details/5
        public ActionResult Details(int id = 1)
        {
            return BaseDetails<Person,PersonsController,PersonVM>(UIControllerType.Persons, id);
        }
         

        // GET: Persons/Create 
        public ViewResult Create(UIViewType actionMethod)
        {
            ViewBag.ActionMethod = actionMethod;
            return BaseCreate<PersonVM>(actionMethod);
        }

        // GET: Persons/Edit/5
        public ViewResult Edit(int id = 1)
        {
            return BaseEdit<Person, PersonsController, PersonVM>(UIControllerType.Persons, id);
        }



        // POST: Persons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonsID,Name,Ingredients")] PersonVM personVM)
        {
            return BasePostEdit<Person, PersonsController, PersonVM>(personVM);
        }


        // GET: Persons/Delete/5
        public ActionResult Delete(int id = 1)
        {
            return BaseDelete<Person, PersonsController, PersonVM>(UIControllerType.Persons, id);
        }

        // POST: Persons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return BaseDeleteConfirmed<Person, PersonsController>(UIControllerType.Persons, id);
        }
        }
}
